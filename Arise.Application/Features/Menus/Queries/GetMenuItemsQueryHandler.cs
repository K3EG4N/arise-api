using Arise.Application.Common.Results;
using Arise.Application.Interfaces;
using Arise.Domain.Entities;
using MediatR;

namespace Arise.Application.Features.Menus.Queries
{
    public class GetMenuItemsQueryHandler : IRequestHandler<GetMenuItemsQuery, Result<List<GetMenuItemsResponse>>>
    {
        private readonly IRepository<MenuItemRole> _menuItemRoleRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMenuItemsQueryHandler(
            IRepository<MenuItemRole> menuItemRoleRepository,
            ICurrentUserService currentUserService
        )
        {
            _menuItemRoleRepository = menuItemRoleRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<GetMenuItemsResponse>>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var roles = _currentUserService.Roles.ToList();

            if (roles.Count == 0)
                return Result<List<GetMenuItemsResponse>>.Success([]);

            var menuItemRoles = await _menuItemRoleRepository.GetAllAsync(
                mir => roles.Contains(mir.Role!.Code)
                       && mir.MenuItem!.DeletedAt == null,
                includes: [mir => mir.MenuItem!.Menu!, mir => mir.Role!]);

            var result = menuItemRoles
                .Select(mir => mir.MenuItem!)
                .Distinct()
                .GroupBy(mi => mi.Menu!)
                .OrderBy(g => g.Key.Order)
                .Select(g =>
                {
                    var itemsByParent = g.ToLookup(i => i.ParentId);

                    MenuItemDTO MapToDto(MenuItem item) => new()
                    {
                        Id = item.MenuItemId,
                        Name = item.Name,
                        Url = item.Url,
                        Icon = item.Icon,
                        Children = itemsByParent[item.MenuItemId]
                            .OrderBy(c => c.Order)
                            .Select(MapToDto)
                            .ToList()
                    };

                    return new GetMenuItemsResponse
                    {
                        Title = g.Key.Name,
                        Items = itemsByParent[null]
                            .OrderBy(i => i.Order)
                            .Select(MapToDto)
                            .ToList()
                    };
                })
                .ToList();

            return Result<List<GetMenuItemsResponse>>.Success(result);

        }
    }
}
