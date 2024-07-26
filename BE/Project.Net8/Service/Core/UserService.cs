using DTC.MongoDB;
using MongoDB.Driver;
using Project.Net8.Installers;
using Project.Net8.Interface.Permission;
using Project.Net8.Models.Core;
using Project.Net8.Models.Permission;
using Project.Net8.Service.Base;

namespace Project.Net8.Service.Core
{
    public class UserService : BaseService, IUserService
    {
        private DataContext _context;
        private BaseMongoDb<User, string> BaseMongoDb;
        IMongoCollection<User> _collectionUser;
        IMongoCollection<DonVi> _collectionDonVi;
      public UserService(
            DataContext context,
       
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<User, string>(_context.USERS);
          _collectionUser = context.USERS;
        }


        public Task<dynamic> Create(User model)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> Update(User model)
        {
            throw new NotImplementedException();
        }
    }
}
