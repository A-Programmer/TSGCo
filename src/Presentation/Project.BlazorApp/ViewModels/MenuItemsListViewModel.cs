
using Project.BlazorApp.Models;

namespace Project.BlazorApp.ViewModels
{
    public class MenuItemsListViewModel
    {
        public MenuItem MenuItem { get; set; }
        public IList<MenuItemsListViewModel> Children { get; set; }
    }
}