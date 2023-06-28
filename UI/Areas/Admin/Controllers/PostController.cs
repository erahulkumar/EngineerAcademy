using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using DTO;
using BLL;
namespace UI.Areas.Admin.Controllers
{
    public class PostController : AuthenticatorController
    {
        // GET: Admin/Post
        PostBLL bll = new PostBLL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PostList()
        {
            CountDTO countdto = new CountDTO();
            countdto = bll.GetAllCounts();
            ViewData["AllCounts"] = countdto;
            List<PostDTO> postlist = new List<PostDTO>();
            postlist = bll.GetPostList();
            return View(postlist);
        }
        public ActionResult AddPost()
        {
            PostDTO model = new PostDTO();
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO model)
        {
            
            if(model.PostImage[0]==null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                foreach (var item in model.PostImage)
                {
                    // Bitmap bitmap = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if(ext!=".jpeg" && ext!=".png" && ext!=".jpg")
                    {
                        ViewBag.ProcessState = General.Message.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropdown();
                        return View(model);
                    }
                }
                List<PostImageDTO> postimagelist = new List<PostImageDTO>();
                foreach (var postedfile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postedfile.InputStream);
                    Bitmap resizeimage = new Bitmap(image, 750, 422);
                    string filename = "";
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedfile.FileName;
                    resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    postimagelist.Add(dto);
                }
                
                model.PostImages = postimagelist;
                if(bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
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
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }
        public ActionResult UpdatePost(int ID)
        {
            PostDTO model = new PostDTO();
            model = bll.GetPostWithID(ID);
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            model.isUpdate = true;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDTO model)
        {
            IEnumerable<SelectListItem> selectlist = CategoryBLL.GetCategoriesForDropdown();
            if(ModelState.IsValid)
            {
                if(model.PostImage[0]!=null)
                {
                    foreach (var item in model.PostImage)
                    {
                        // Bitmap bitmap = new Bitmap(item.InputStream);
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".jpeg" && ext != ".png" && ext != ".jpg")
                        {
                            ViewBag.ProcessState = General.Message.ExtensionError;
                            model.Categories = CategoryBLL.GetCategoriesForDropdown();
                            return View(model);
                        }
                    }
                    List<PostImageDTO> postimagelist = new List<PostImageDTO>();
                    foreach (var postedfile in model.PostImage)
                    {
                        Bitmap image = new Bitmap(postedfile.InputStream);
                        Bitmap resizeimage = new Bitmap(image, 750, 422);
                        string uniquenumber = Guid.NewGuid().ToString();
                        string filename = uniquenumber + postedfile.FileName;
                        resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = filename;
                        postimagelist.Add(dto);
                    }
                    model.PostImages = postimagelist;
                }
                if(bll.UpdatePost(model))
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
            model = bll.GetPostWithID(model.ID);
            model.Categories = selectlist;
            model.isUpdate = true;
            return View(model);
        }
        public JsonResult DeletePostImage(int ID)
        {
            string deleteimagepath = bll.DeletePostImage(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + deleteimagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + deleteimagepath));
            }
            return Json("");
        }
        public JsonResult DeletePost(int ID)
        {
            List<PostImageDTO> imagelist = bll.DeletePost(ID);
            foreach (var item in imagelist)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath));
                }
            }
            return Json("");
        }
       public JsonResult GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto = bll.GetCounts();
            return Json(dto,JsonRequestBehavior.AllowGet);
        }
    }
}