using DTC.DefaultRepository.Constants;
using DTC.DefaultRepository.Exceptions;
using DTC.DefaultRepository.Helpers;
using DTC.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Project.Net8.Installers;
using Project.Net8.Interface.Major;
using Project.Net8.Models.Major;
using Project.Net8.Service.Base;
using Project.Net8.ViewModels;
using Project.Net8.ViewModels.MenuCitizen;

namespace Project.Net8.Service.Major
{
    public class MenuCitizenService : BaseService, IMenuCitizenService
    {
        private DataContext _context;
        private BaseMongoDb<MenuCitizenModel, string> BaseMongoDb;
        public MenuCitizenService(
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<MenuCitizenModel, string>(_context.MENUCITIZEN);
        }

        public async Task<dynamic> Create(MenuCitizenModel model)
        {
            try
            {
                if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
                var menu = _context.MENUCITIZEN.Find(x => !x.IsDeleted &&  (x.Name == model.Name || x.UnsignedName == FormatterString.
                ConvertToUnsign(model.Name))).FirstOrDefault();
            if (menu != default)
            {
                throw new ResponseMessageException()
                    .WithException(DefaultCode.DATA_EXISTED);
            }
            var entity = new MenuCitizenModel
            {
                Id = BsonObjectId.GenerateNewId().ToString(),
                Name = model.Name,
                Path = model.Path,
                Icon = model.Icon,
                ParentId = model.ParentId,
                Sort = model.Sort,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
            };
            if (model.ParentId != null)
            {
                var level = _context.MENUCITIZEN.Find(x => x.Id == model.ParentId).FirstOrDefault();
                if (level == null)
                    throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage("Đơn vị cha không đúng !");
                entity.Level= level.Level + 1; 
            }
            var result = await BaseMongoDb.CreateAsync(entity);
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException()
                    .WithException(DefaultCode.CREATE_FAILURE);
            }
            return entity;
            }
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.ResultString).WithDetail(e.Error);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("is not a valid 24 digit hex string."))
                {
                    throw new ResponseMessageException().WithException(DefaultCode.ID_NOT_CORRECT_FORMAT);
                }

                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(ex.Message);
            }
        }


        #region Cap Nhat Menu 

           public async Task<dynamic> Update(MenuCitizenModel model)
        {
            try{
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            var entity = _context.MENUCITIZEN.Find(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException()
                    .WithException(DefaultCode.DATA_NOT_FOUND);
            
            var checkName = _context.MENUCITIZEN.Find(x => !x.IsDeleted &&
                                                           x.Id != model.Id
                                                           && (x.Name == model.Name || x.UnsignedName ==
                                                               FormatterString.ConvertToUnsign(model.Name).Replace(" " ,""))
            ).FirstOrDefault();

            

            if (checkName != default)
                throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);
            entity.Name = model.Name;
            entity.Path = model.Path;
            entity.Icon = model.Icon;
            entity.ParentId = model.ParentId;
            entity.Level = model.Level;
            entity.Sort = model.Sort;
            entity.ModifiedAt = DateTime.Now;
            entity.SoLuongTin = model.SoLuongTin;
            entity.IsMenuCitizen = model.IsMenuCitizen;

            if (model.ParentId != null)
            {
                var menu = _context.MENUCITIZEN.Find(x => x.Id == model.ParentId).FirstOrDefault();
                if (menu == null)
                    throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage("Đơn vị cha không đúng !");
                entity.Level= menu.Level + 1; 
            }
            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
                throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
            return entity;
            }
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.ResultString).WithDetail(e.Error);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("is not a valid 24 digit hex string."))
                {
                    throw new ResponseMessageException().WithException(DefaultCode.ID_NOT_CORRECT_FORMAT);
                }
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.Message);
            }
        }
           

        #endregion
     
        
        
        
        
        
        public async Task<List<MenuCitizenTreeVM>> GetTreeList()
        {
            var listDonVi = await _context.MENUCITIZEN.Find(_ => _.IsDeleted != true && _.IsMenuCitizen).ToListAsync();
            var parents = listDonVi.Where(x => x.ParentId == null || x.ParentId.Equals("")).OrderByDescending(x=>x.Sort).ToList();
            List<MenuCitizenTreeVM> list = new List<MenuCitizenTreeVM>();
            foreach (var item in parents)
            {
                MenuCitizenTreeVM donVi = new MenuCitizenTreeVM(item);
                list.Add(donVi);
                GetLoopItem(ref list, listDonVi, donVi);
            }
            return list;
        }

    

        private List<MenuTreeVM> GetLoopItem(ref List<MenuCitizenTreeVM> list, List<MenuCitizenModel> items, MenuCitizenTreeVM target)
        {
            
            var menu =  items.Where(x=>x.ParentId == target.Id).OrderByDescending(x=>x.Sort).ToList();
            if (menu.Count > 0)
            {
                target.Children = new List<MenuCitizenTreeVM>();
                foreach (var item in menu)
                {
                    MenuCitizenTreeVM itemDV = new MenuCitizenTreeVM(item);
                    target.Children.Add(itemDV);
                    // target.SubItems.Add(itemDV);
                    GetLoopItem(ref list, items, itemDV);
                }
            }
            return null;
        }
        

        
      
        private async Task <List<MenuCitizenModel>> UpdateMenu(List<MenuCitizenModel> list , MenuCitizenModel menu )
        {

            int index = list.FindIndex(x => x.Id == menu.Id);
            list[index] = menu;
            return list;
        }


        
        private List<NavMenuCitizenVM> GetLoopItemSubItems(ref List<NavMenuCitizenVM> listMenuVM, List<MenuCitizenModel> listMenu, NavMenuCitizenVM target)
        {
            var parents = listMenu.Where(x => x.ParentId == target.Id && !x.IsDeleted).OrderBy(x=>x.Sort).ToList();
            if (parents.Count == 0)
                return null;
            List<NavMenuCitizenVM> list = new List<NavMenuCitizenVM>();
            foreach (var item in parents)
            {
                if (target.Children == default)
                    target.Children = new List<NavMenuCitizenVM>();
                if (item != null && item.Id != null)
                {
                    NavMenuCitizenVM donVi = new NavMenuCitizenVM(item);
                    item.IsChecked = true;
                    UpdateMenu(listMenu, item);
                    target.Children.Add(donVi);
                    GetLoopItemSubItems(ref list, listMenu, donVi);  
                }
             
            }
            return null;
        }
        
        
        public async Task<dynamic> GetTreeFlatten()
        {
            List<MenuCongDanTreeVMGetAll> list = new List<MenuCongDanTreeVMGetAll>();
         
            var listDonVi = await _context.MENUCITIZEN.Find(x  => !x.IsDeleted).ToListAsync();
            var parents = listDonVi.Where(x => x.ParentId == null || x.ParentId.Equals("")).ToList();
            var listId = new List<String>();
            foreach (var item in parents)
            {
                    
                MenuCongDanTreeVMGetAll menu = new MenuCongDanTreeVMGetAll(item);
                list.Add(menu);
                GetLoopItemGetAll(ref list,listDonVi, menu);
            }
            return list;
        }
        
        
        private void  GetLoopItemGetAll(ref List<MenuCongDanTreeVMGetAll> list, List<MenuCitizenModel> items, MenuCongDanTreeVMGetAll target)
        {
            try
            {
                var coquan = items.FindAll((item) => item.ParentId == target.Id).OrderBy(x=>x.Name).ToList();
                if (coquan.Count > 0)
                {
                    foreach (var item in coquan)
                    {
                        MenuCongDanTreeVMGetAll itemDV = new MenuCongDanTreeVMGetAll(item);
                        list.Add(itemDV);
                        GetLoopItemGetAll(ref list, items, itemDV);
                    }
                }
            }
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ResultString);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("is not a valid 24 digit hex string."))
                {
                    throw new ResponseMessageException().WithException(DefaultCode.ID_NOT_CORRECT_FORMAT);
                }
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(ex.Message);
            }
        }





    }
}