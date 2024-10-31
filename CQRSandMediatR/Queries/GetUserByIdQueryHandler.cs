using CQRSandMediatR.DTOs;
using CQRSandMediatR.Entities;
using CQRSandMediatR.Repositories;
using MediatR;

namespace CQRSandMediatR.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailDto>
    {
        private readonly IGenericRepository<User> _repository;

        public GetUserByIdQueryHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<UserDetailDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            var dto = new UserDetailDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email
            };

            return dto;
            
        }
    }
}
