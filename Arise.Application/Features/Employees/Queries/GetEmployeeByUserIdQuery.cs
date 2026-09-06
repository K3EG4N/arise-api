using Arise.Application.Common.Results;
using MediatR;

namespace Arise.Application.Features.Employees.Queries
{
    public class GetEmployeeByUserIdQuery : IRequest<Result<GetEmployeeByUserIdResponse>>
    {
        public Guid UserId { get; set; }

        public GetEmployeeByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
