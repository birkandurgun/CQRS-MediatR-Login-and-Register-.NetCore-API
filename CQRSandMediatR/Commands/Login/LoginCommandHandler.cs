using CQRSandMediatR.Authentication;
using CQRSandMediatR.Entities;
using CQRSandMediatR.PasswordHash;
using CQRSandMediatR.Repositories;
using MediatR;
using System.Security.Claims;

namespace CQRSandMediatR.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IGenericRepository<User> _repository;
        private readonly TokenManager _tokenManager;
        private readonly IConfiguration _configuration;
        private readonly int _iteration = 5;
        public LoginCommandHandler(IGenericRepository<User> repository,
            TokenManager tokenManager, IConfiguration configuration) { 
            _repository = repository;
            _tokenManager = tokenManager;
            _configuration = configuration;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(u => u.Email == request.Email);

            if (user == null)
                return new LoginResponse { AuthenticationResult = false };

            var userSalt = user.PasswordSalt;
            var requestPasswordHash = PasswordHasher.Hash(request.Password, userSalt, _configuration["SecuritySettings:Pepper"],_iteration);

            if (user == null || !requestPasswordHash.Equals(user.PasswordHash))
                return new LoginResponse { AuthenticationResult = false };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var authToken = _tokenManager.GenerateAccessToken(claims);
            var refreshToken = _tokenManager.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(double.Parse(_configuration["JwtSettings:RefreshTokenExpirationDays"]));
            await _repository.UpdateAsync(user);
            await _repository.SaveChangesAsync();

            return new LoginResponse
            {
                AuthenticationResult = true,
                AuthToken = authToken,
                RefreshToken = refreshToken,
                AccessTokenExpireDate = DateTime.UtcNow.AddDays(double.Parse(_configuration["JwtSettings:AccessTokenExpirationDays"]))
            };
        }
    }
}
