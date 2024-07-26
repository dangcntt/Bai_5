using Project.Net8.Models.Core;

namespace Project.Net8.Interface.Core
{
    public interface IDiaGioiHanhChinhService
    {
        Task<dynamic> Create(DiaGioiHanhChinhModel model);
        Task<dynamic> Update(DiaGioiHanhChinhModel model);
        
        Task<dynamic> GetListChildByCode(string code);
        
        
        
        Task<dynamic> GetListByLevel(int level);
        
        
        
        
    }
}