using DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public class CategoryDAO
    {
        public int AddCategory(E_Category category)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_Category.Add(category);
                Db.SaveChanges();
                }
                
                return category.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CategoryDTO> GetCategoryList()
        {
            List<CategoryDTO> dtolist = new List<CategoryDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {List<E_Category> categories = Db.E_Category.Where(x => x.isDeleted == false).OrderBy(x => x.addDate).ToList();
            
            foreach (var item in categories)
            {
                CategoryDTO dto = new CategoryDTO();
                dto.ID = item.ID;
                dto.CategoryName = item.CategoryName;
                dtolist.Add(dto);
            }

            }
            
            return dtolist;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
             IEnumerable<SelectListItem> categoryList = Db.E_Category.Where(x => x.isDeleted == false).OrderByDescending(x => x.addDate).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = SqlFunctions.StringConvert((double)x.ID)
            }).ToList();
            return categoryList;
            }
            
        }

        public List<E_Post> DeleteCategory(int ID)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_Category category = Db.E_Category.First(x => x.ID == ID);
                category.isDeleted = true;
                category.DeletedDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;
                category.LastUpdateDate = DateTime.Now;
                Db.SaveChanges();
                List<E_Post> postlist = Db.E_Post.Where(x => x.isDeleted == false && x.CategoryID == ID).ToList();
                return postlist;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

     

        public void UpdateCategory(CategoryDTO model)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Category category = Db.E_Category.First(x => x.ID == model.ID);
            CategoryDTO dto = new CategoryDTO();
            category.CategoryName = model.CategoryName;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;
            Db.SaveChanges();
            }
            
        }
    }
}
