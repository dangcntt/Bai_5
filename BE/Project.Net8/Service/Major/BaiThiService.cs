using DTC.DefaultRepository.Constants;
using DTC.DefaultRepository.Exceptions;
using DTC.DefaultRepository.FromBodyModels;
using DTC.DefaultRepository.Helpers;
using DTC.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Project.Net8.Constants;
using Project.Net8.Installers;
using Project.Net8.Interface.Major;
using Project.Net8.Models.Major;
using Project.Net8.Models.PagingParam;
using Project.Net8.Service.Base;

namespace Project.Net8.Service.Major
{
    public class BaiThiService : BaseService, IBaiThiService
    {
        private DataContext _context;
        private BaseMongoDb<BaiThiModel, string> BaseMongoDb;
        protected ProjectionDefinition<BaiThiModel, BsonDocument> projectionDefinition = Builders<BaiThiModel>.Projection
            .Exclude("ModifiedAt")
            .Exclude("CreatedBy")
            .Exclude("ModifiedBy")
            .Exclude("IsDeleted")
            .Exclude("CreatedAtString")
            .Exclude("PasswordSalt")
            .Exclude("PasswordHash")
            .Exclude("Sort")
            .Exclude("UnsignedName");
        public BaiThiService(
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<BaiThiModel, string>(_context.BAITHI);
        }


        public async Task<dynamic> Create(BaiThiModel model)
        {
             try
            {
                if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            var result = await BaseMongoDb.CreateAsync(model);
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException()
                    .WithException(DefaultCode.CREATE_FAILURE);
            }
            return model;
            }
             catch (ResponseMessageException e)
             {
                 throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.ResultString).WithDetail(e.Error);
             }
             catch (Exception e)
             {
                 throw ExceptionError.Exception(e);
             }
        }

        public async Task<dynamic> Update(BaiThiModel model)
        {
            try
            {
                if (model == default)
                    throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
                var result = await BaseMongoDb.UpdateAsync(model);
                if (result.Entity.Id == default || !result.Success)
                {
                    throw new ResponseMessageException()
                        .WithException(DefaultCode.CREATE_FAILURE);
                }
                return model;
            }
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.ResultString).WithDetail(e.Error);
            }
            catch (Exception e)
            {
                throw ExceptionError.Exception(e);
            }
        }


        
    }
}