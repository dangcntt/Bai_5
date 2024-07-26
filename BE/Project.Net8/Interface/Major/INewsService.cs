using DTC.DefaultRepository.FromBodyModels;
using Project.Net8.Models.Major;
using Project.Net8.Models.PagingParam;

namespace Project.Net8.Interface.Major
{
    public interface INewsService
    {
        Task<dynamic> Create(NewsModel model);
        Task<dynamic> Update(NewsModel model);
        
        Task<dynamic> GetByMenuId(PagingParamDefault pagingParam);
        
        
        
        Task<dynamic> GetByServiceId(IdFromBodyModel pagingParam);
        
        
    }
}