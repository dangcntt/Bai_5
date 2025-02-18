using Project.Net8.Interface.Auth;
using Project.Net8.Interface.Common;
using Project.Net8.Interface.Core;
using Project.Net8.Interface.Major;
using Project.Net8.Interface.Permission;
using Project.Net8.Service.Auth;
using Project.Net8.Service.Common;
using Project.Net8.Service.Core;
using Project.Net8.Service.Major;
using Project.Net8.Service.Permission;
using UserService = Project.Net8.Service.Core.UserService;

namespace Project.Net8.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<DataContext>();

            #region Auth 
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IIdentityService, IdentityService>();
            #endregion

            #region Common

            services.AddScoped<ICommonService, CommonService>();

            #endregion
            
            #region Core 



            services.AddScoped<IMenuService, MenuService>();
       

            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped<IAPIService, APIService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDiaGioiHanhChinhService, DiaGioiHanhChinhService>();


           
       
           
           
            
         

            #endregion


            #region Nghiệp vụ  
           
            services.AddScoped<IMenuCitizenService, MenuCitizenService>();
            
            services.AddScoped<INewsService, NewsService>();
            
            
            services.AddScoped<IBaiThiService, BaiThiService>();

            services.AddScoped<ITheDiemService, TheDiemService>();

            #endregion


        }
    }
}
