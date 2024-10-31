using CQRSandMediatR.DTOs;
using CQRSandMediatR.Repositories;
using CQRSandMediatR.Entities;
using MediatR;

namespace CQRSandMediatR.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDetailDto>>
    {
        private readonly IGenericRepository<User> _repository;

        public GetAllUsersQueryHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<List<UserDetailDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            return users.Select(e =>
                new UserDetailDto
                {
                    Id = e.Id,
                    Firstname = e.Firstname,
                    Lastname = e.Lastname,
                    Email = e.Email
                }
            ).ToList();
        }
    }
}
