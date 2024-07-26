using DTC.DefaultRepository.FromBodyModels;
using Project.Net8.Models.Major;
using Project.Net8.Models.PagingParam;

namespace Project.Net8.Interface.Major
{
    public interface ITheDiemService
    {
        Task<dynamic> Create(TheDiemModel model);
        Task<dynamic> Update(TheDiemModel model);

    }
}