namespace Arise.Application.Features.Menus.Queries
{
    public class GetMenuItemsResponse
    {
        public string Title { get; set; } = null!;
        public List<MenuItemDTO> Items { get; set; } = [];
    }

    public class MenuItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public List<MenuItemDTO>? Children { get; set; }
    }
}
