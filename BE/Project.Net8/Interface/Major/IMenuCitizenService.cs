using Project.Net8.Models.Major;
using Project.Net8.ViewModels.MenuCitizen;

namespace Project.Net8.Interface.Major
{
    public interface IMenuCitizenService
    {
        Task<List<MenuCitizenTreeVM>> GetTreeList();
        
        
       
        
        Task<dynamic> GetTreeFlatten();
        
        Task<dynamic> Create(MenuCitizenModel model);
        
        
        Task<dynamic> Update(MenuCitizenModel model);
        
    }
}