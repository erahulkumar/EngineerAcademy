using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
namespace UI.Areas.Admin.Controllers
{
    public class CategoryController : AuthenticatorController
    {
        // GET: Admin/Category
        CategoryBLL bll = new CategoryBLL();
        public ActionResult CategoryList()
        {
            List<CategoryDTO> dtolist = new List<CategoryDTO>();
            dtolist = bll.GetCategoryList();
            return View(dtolist);

        }
        public ActionResult UpdateCategory(int ID)
        {
            List<CategoryDTO> dtolist = new List<CategoryDTO>();
            dtolist = bll.GetCategoryList();
            CategoryDTO dto = new CategoryDTO();
            dto = dtolist.First(x => x.ID == ID);
            return View(dto);
            
        }
        [HttpPost]
        public ActionResult UpdateCategory(CategoryDTO model)
        {
            if(ModelState.IsValid)
            {
                if(bll.UpdateCategory(model))
                {
                    ViewBag.ProcessState = General.Message.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }

            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }

        public ActionResult AddCategory()
        {
            CategoryDTO dto = new CategoryDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryDTO model)
        {
            if(ModelState.IsValid)
            {
              if(bll.AddCategory(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model = new CategoryDTO();
                }
              else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }
        public JsonResult DeleteCategory(int ID)
        {
            List<PostImageDTO> postimagelist = bll.DeleteCategory(ID);
            foreach (var item in postimagelist)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath));
                }
            }
            return Json("");
        }
    }
}