using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL;
using System.Web.Mvc;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO dao = new CategoryDAO();
        public bool AddCategory(CategoryDTO model)
        {
            E_Category category = new E_Category();
            category.CategoryName = model.CategoryName;
            category.addDate = DateTime.Now;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;
            int ID = dao.AddCategory(category);
            LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, ID);
            return true;

            
        }

        public static List<SelectListItem> GetCategoriesForDropdown()
        {
            return (List<SelectListItem>)CategoryDAO.GetCategoriesForDropdown();
        }

        public List<CategoryDTO> GetCategoryList()
        {
            return dao.GetCategoryList();
        }

        public bool UpdateCategory(CategoryDTO model)
        {
            dao.UpdateCategory(model);
            LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, model.ID);
            return true;
        }
        PostBLL postbll = new PostBLL();
        public List<PostImageDTO> DeleteCategory(int ID)
        {
            List<E_Post> postlist = dao.DeleteCategory(ID);
            LogDAO.AddLog(General.ProcessType.CategoryDelete, General.TableName.Category, ID);
            List<PostImageDTO> imagelist = new List<PostImageDTO>();
            foreach (var item in postlist)
            {
                List<PostImageDTO> imagelist2 = postbll.DeletePost(item.ID);
                LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post,item.ID);
                foreach (var item2 in imagelist2)
                {
                    imagelist.Add(item2);
                }

            }
            return imagelist;
        }

    }
}
