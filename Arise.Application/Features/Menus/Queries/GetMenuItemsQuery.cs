using Arise.Application.Common.Results;
using MediatR;

namespace Arise.Application.Features.Menus.Queries
{
    public class GetMenuItemsQuery : IRequest<Result<List<GetMenuItemsResponse>>> { }
}
