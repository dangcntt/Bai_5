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
    public class NewsService : BaseService, INewsService
    {
        private DataContext _context;
        private BaseMongoDb< NewsModel, string> BaseMongoDb;
        protected ProjectionDefinition<NewsModel, BsonDocument> projectionDefinition = Builders<NewsModel>.Projection
            .Exclude("ModifiedAt")
            .Exclude("CreatedBy")
            .Exclude("ModifiedBy")
            .Exclude("IsDeleted")
            .Exclude("CreatedAtString")
            .Exclude("PasswordSalt")
            .Exclude("PasswordHash")
            .Exclude("Sort")
            .Exclude("UnsignedName");
        public NewsService(
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<NewsModel, string>(_context.NEWS);
        }


        public async Task<dynamic> Create(NewsModel model)
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

        public async Task<dynamic> Update(NewsModel model)
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

        public async Task<dynamic> GetByMenuId(PagingParamDefault pagingParam)
        {
           try
            {
                if (pagingParam == default)
                    throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

                if (pagingParam.MenuId == default)
                    throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

                PagingModel<dynamic> result = new PagingModel<dynamic>();
                var builder = Builders<NewsModel>.Filter;
                var filter = builder.Empty;
                filter = builder.And(filter, builder.Where(x => !x.IsDeleted && x.IsPublish));
                MenuCitizenModel menu = _context.MENUCITIZEN.Find(x => !x.IsDeleted && x.Id == pagingParam.MenuId).FirstOrDefault();
                if (menu != null && menu.SoLuongTin == 1)
                {
                    return _context.NEWS.Find(x => !x.IsDeleted && x.IsPublish && x.Menu.Id == pagingParam.MenuId).SortByDescending(x => x.CreatedAt).FirstOrDefault();
                }
                else
                {
                    filter = builder.And(filter, builder.Where(x => x.Menu.Id == pagingParam.MenuId));
                    
                }
                
                var tasks = new List<Task<long>>();
                Task<long> task_total = null;
                Task<List<NewsModel>> task_data = null;

                await Task.Run(() =>
                {
                    task_total = _context.NEWS.CountDocumentsAsync(filter);
                    task_data = _context.NEWS.Aggregate(new AggregateOptions()
                        {
                            AllowDiskUse = true
                        })
                        .Match((filter))
                        .SortByDescending(e => e.PublicationDate)
                        .ThenByDescending(e => e.Sort)
                        .Skip(pagingParam.Skip)
                        .Limit(pagingParam.Limit)
                        .ToListAsync();
                });
                await Task.WhenAll(task_data, task_total);
                result.TotalRows = task_total.Result;
                result.Data = task_data.Result;

                return result;
            }
           catch (ResponseMessageException e)
           {
               throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.ResultString).WithDetail(e.Error);
           }
           catch (Exception e)
           {
               throw ExceptionError.Exception(e);
           }
            return null;
        }
        
        
        public async Task<dynamic> GetByServiceId(IdFromBodyModel pagingParam)
        {
           try
            {
                if (pagingParam == default)
                    throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
                var builder = Builders<NewsModel>.Filter;
                var filter = builder.Empty;
                filter = builder.And(filter, builder.Where(x => !x.IsDeleted && x.IsPublish));
                return  _context.NEWS.Aggregate(new AggregateOptions()
                        {
                            AllowDiskUse = true
                        })
                        .Match((filter))
                        .SortByDescending(e => e.PublicationDate)
                        .ThenByDescending(e => e.Sort)
                        .ToListAsync();
            }
           catch (ResponseMessageException e)
           {
               throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.ResultString).WithDetail(e.Error);
           }
           catch (Exception e)
           {
               throw ExceptionError.Exception(e);
           }
            return null;
        }
        
    }
}