using CQRSandMediatR.DTOs;
using MediatR;

namespace CQRSandMediatR.Queries
{
    public class GetUserByIdQuery: IRequest<UserDetailDto>
    {
        public Guid Id { get; set; }
    }
}
