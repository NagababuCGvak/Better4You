using System;
using System.Collections.Generic;
using System.Linq;
using Better4You.Core.Repositories;
using Better4You.Meal.Config;
using Better4You.Meal.EntityModel;
using Better4You.UserManagment.EntityModel;
using Better4You.Meal.ViewModel;
using Tar.ViewModel;

namespace Better4You.Meal.Business.Implementation
{
    public class MealMenuFacade:IMealMenuFacade
    {
        public IConfigRepository Repository { get; set; }

        public List<MealMenuListItemView> GetByFilter1(MealMenuFilterView filter, int pageSize, int pageIndex, string orderByField, bool orderByAsc, out int totalCount)
        {


            var query1 = Repository.Query<MealMenu>();

            //var menuss = { filter.MenuId.Value }
            var menu = filter.MenuId;
            if (filter.MenuId == null)
                menu = 0;
            var query = from schools in Repository.Query<School>()
                        join mmo in Repository.Query<MealMenuOrder>() on schools.Id equals mmo.School.Id
                        join mmoi in Repository.Query<MealMenuOrderItem>() on mmo.Id equals mmoi.MealMenuOrder.Id
                        join mm in Repository.Query<MealMenu>() on mmoi.MealMenu.Id equals mm.Id
                        join m in Repository.Query<Menu>() on mm.Menu.Id equals m.Id
                        where (mm.ValidDate >= filter.StartDate && mm.ValidDate <= filter.EndDate) && mm.MealType == filter.MealTypeId
                         && mm.RecordStatus == (long)RecordStatuses.Active && schools.Menu == menu && mmoi.RecordStatus == (long)RecordStatuses.Active
                        select
                          new
                          {
                              schoolName = schools.Name,
                              totalCount = mmoi.TotalCount,
                              menuName = m.Name,
                              menuID = m.Id,
                              MealMenuId = mm.Id,
                              validDate = mm.ValidDate,
                              menuTypeId = m.MenuType,
                              breakfast = schools.BreakfastOVSType,
                              FServiceType = schools.FoodServiceType,
                              SType = schools.SchoolType,
                              mealType = mm.MealType,
                              LunchOVS = schools.LunchOVSType,
                              ModifiedAt = mm.ModifiedAt,
                              Status = mm.RecordStatus,
                              Menusid = mm.Menu.Id,
                              AdditionalFruit = m.AdditionalFruit,
                              AdditionalVeg = m.AdditionalVeg,
                              CreatedAt = m.CreatedAt,
                              CreatedBy = m.CreatedBy,
                              CreatedByFullName = m.CreatedByFullName,
                              MenuType = m.MenuType,
                              ModifiedAt1 = m.ModifiedAt,
                              ModifiedBy = m.ModifiedBy,
                              ModifiedByFullName = m.ModifiedByFullName,
                              ModifiedReason = m.ModifiedReason,
                              Name = m.Name,
                              RecordStatus = m.RecordStatus,
                              SchoolType = m.SchoolType
                          };
            var queryResuls = query.AsEnumerable().Distinct().ToList();

            var query2 = Repository.Query<Menu>();

            //if (filter.MealTypeId > 0)
            //    query = query.Where(d => d.MealType == filter.MealTypeId);
            //if (filter.MenuId > 0)
            //    query = query.Where(d => d.Menu.Id == filter.MenuId);
            //if (filter.RecordStatusId > 0)
            //    query = query.Where(d => d.RecordStatus == filter.RecordStatusId);
            //if (filter.StartDate.HasValue)
            //    query = query.Where(d => d.ValidDate >= filter.StartDate);
            //if (filter.EndDate.HasValue)
            //    query = query.Where(d => d.ValidDate <= filter.EndDate);
            //if (filter.MenuId.HasValue)
            //    query = query.Where(d => new[]{filter.MenuId.Value}.Contains(d.menuID));


            totalCount = query.Count();

            switch (orderByField)
            {
                case "Id":
                    query = orderByAsc ? query.OrderBy(c => c.MealMenuId) : query.OrderByDescending(c => c.MealMenuId);
                    break;
                case "MealType":
                    query = orderByAsc ? query.OrderBy(c => c.mealType) : query.OrderByDescending(c => c.mealType);
                    break;
                case "ValidDate":
                    query = orderByAsc ? query.OrderBy(c => c.validDate) : query.OrderByDescending(c => c.validDate);
                    break;
                case "ModifiedAt":
                    query = orderByAsc ? query.OrderBy(c => c.ModifiedAt) : query.OrderByDescending(c => c.ModifiedAt);
                    break;
                default:
                    query = orderByAsc ? query.OrderBy(c => new { c.MealMenuId }) : query.OrderByDescending(c => new { c.MealMenuId });
                    break;
            }

            if (pageSize > 0) query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            List<Menu> menus = new List<Menu>();

            var mealMenuList = query.AsEnumerable().Select(m => new MealMenuListItemView
            {
                Id = m.MealMenuId,
                MealType = Lookups.GetItem<MealTypes>(m.mealType),
                ValidDate = m.validDate,
                RecordStatus = Lookups.GetItem<RecordStatuses>(m.Status),
                Menu = new MenuView
                {
                    Id = m.menuID,
                    AdditionalFruit = m.AdditionalFruit,
                    AdditionalVeg = m.AdditionalVeg,
                    CreatedAt = m.CreatedAt,
                    CreatedBy = m.CreatedBy,
                    CreatedByFullName = m.CreatedByFullName,
                    MenuType  = Lookups.MenuTypeList.FirstOrDefault(k => k.Id == m.MenuType),
                    // Foods = Lookups.GetItem <FoodTypes>(m.Foods),
                    //MenuType = new GeneralItemView { Text =  ,
                    //    Value = Lookups.GetItem<MenuTypes>(m.MenuType).Value , Id =
                    //    Lookups.GetItem<MenuTypes>(m.MenuType).Id
                    //},
                    ModifiedAt = m.ModifiedAt1,
                    ModifiedBy = m.ModifiedBy,
                    ModifiedByFullName = m.ModifiedByFullName,
                    ModifiedReason = m.ModifiedReason,
                    Name = m.Name,
                    RecordStatus = Lookups.GetItem<RecordStatuses>(m.Status),
                    SchoolType = Lookups.GetItem<MenuSchoolTypes>(m.SchoolType)
                }
                }).ToList();
            return mealMenuList;
        }

        public List<MealMenuListItemView> GetByFilter(MealMenuFilterView filter, int pageSize, int pageIndex, string orderByField, bool orderByAsc, out int totalCount)
        {

            var query = Repository.Query<MealMenu>();
            if (filter.MealTypeId > 0)
                query = query.Where(d => d.MealType == filter.MealTypeId);
            if (filter.RecordStatusId > 0)
                query = query.Where(d => d.RecordStatus == filter.RecordStatusId);
            if (filter.StartDate.HasValue)
                query = query.Where(d => d.ValidDate >= filter.StartDate);
            if (filter.EndDate.HasValue)
                query = query.Where(d => d.ValidDate <= filter.EndDate);
            if (filter.MenuId.HasValue)
                query = query.Where(d => new[] { filter.MenuId.Value }.Contains(d.Menu.Id));


            totalCount = query.Count();

            switch (orderByField)
            {
                case "Id":
                    query = orderByAsc ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id);
                    break;
                case "MealType":
                    query = orderByAsc ? query.OrderBy(c => c.MealType) : query.OrderByDescending(c => c.MealType);
                    break;
                case "ValidDate":
                    query = orderByAsc ? query.OrderBy(c => c.ValidDate) : query.OrderByDescending(c => c.ValidDate);
                    break;
                case "ModifiedAt":
                    query = orderByAsc ? query.OrderBy(c => c.ModifiedAt) : query.OrderByDescending(c => c.ModifiedAt);
                    break;
                default:
                    query = orderByAsc ? query.OrderBy(c => new { c.Id }) : query.OrderByDescending(c => new { c.Id });
                    break;
            }

            if (pageSize > 0) query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var mealMenuList = query.AsEnumerable().Select(d => d.ToView<MealMenuListItemView>()).ToList();

            return mealMenuList;
        }


        public MealMenuView Get(long id)
        {
            return Repository.GetById<MealMenu>(id).ToView<MealMenuView>();
        }

        public MealMenuView Create(MealMenuView view)
        {
            return Update(view);
        }

        public MealMenuView Update(MealMenuView view)
        {
            var dtNow = DateTime.Now;
            MealMenu model;
            MealMenu duplicatedMealMenu = null;

            if(view.Id>0)
            {
                model = Repository.GetById<MealMenu>(view.Id);
            }
            else
            {
                duplicatedMealMenu = (from mealMenu in Repository.Query<MealMenu>()
                                        where
                                        mealMenu.MealType == view.MealType.Id
                                        && mealMenu.ValidDate == view.ValidDate.Date
                                        && mealMenu.Menu.Id == view.Menu.Id
                                        && mealMenu.RecordStatus == (long)RecordStatuses.Active
                                      select mealMenu).FirstOrDefault();
                if (duplicatedMealMenu == null)
                {
                    model = new MealMenu
                    {
                        Menu = Repository.GetById<Menu>(view.Menu.Id),
                            ModifiedAt = dtNow,
                            ModifiedReason = view.ModifiedReason,
                            ModifiedBy = view.ModifiedBy,
                            ModifiedByFullName = view.ModifiedByFullName,
                            RecordStatus = (long)RecordStatuses.Active 
                    };
                }
                else
                {
                    model = duplicatedMealMenu;
                }
            }
            if (duplicatedMealMenu == null)
            {
                model.MealType = (int)view.MealType.Id;
                model.ValidDate = view.ValidDate.Date;
                if (model.Id == 0)
                {
                    model.CreatedAt = dtNow;
                    model.CreatedBy = view.ModifiedBy;
                    model.CreatedByFullName = view.ModifiedByFullName;
                }

                Repository.Update(model);
            }
            return model.ToView<MealMenuView>();
            
        }

        public void Delete(MealMenuView view)
        {
            
            var model = Repository.GetById<MealMenu>(view.Id);
            model.ModifiedAt = DateTime.Now;
            model.ModifiedBy = view.ModifiedBy;
            model.ModifiedByFullName = view.ModifiedByFullName;
            model.ModifiedReason = view.ModifiedReason;
            model.RecordStatus = (long)RecordStatuses.Deleted;
            Repository.Update(model);
            
        }
    }
}
