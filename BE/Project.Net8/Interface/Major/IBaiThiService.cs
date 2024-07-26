using DTC.DefaultRepository.FromBodyModels;
using Project.Net8.Models.Major;
using Project.Net8.Models.PagingParam;

namespace Project.Net8.Interface.Major
{
    public interface IBaiThiService
    {
        Task<dynamic> Create(BaiThiModel model);
        Task<dynamic> Update(BaiThiModel model);

    }
}