using CQRSandMediatR.DTOs;
using MediatR;

namespace CQRSandMediatR.Queries
{
    public class GetAllUsersQuery: IRequest<List<UserDetailDto>>
    {


    }
}
