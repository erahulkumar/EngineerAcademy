using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
using System.IO;
using System.Drawing;

namespace UI.Areas.Admin.Controllers
{
    public class UserController : AuthenticatorController
    {
        // GET: Admin/User
        UserBLL bll = new UserBLL();
        public ActionResult UpdateUser(int ID)
        {
            UserDTO dto = new UserDTO();
            dto = bll.GetUserWithID(ID);
            return View(dto);
        }
        [HttpPost]

        public ActionResult UpdateUser(UserDTO model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if(model.UserImage!=null)
                {
                    HttpPostedFileBase postedfile = model.UserImage;
                    Bitmap UserImage = new Bitmap(postedfile.InputStream);
                    Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == "git")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        string filename = uniquenumber + postedfile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename));
                        model.ImagePath = filename;
                    }
                    
                }
                string oldImagePath = bll.UpdateUser(model);
                if(model.UserImage!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePath));
                    }   
                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;
            }
            return View(model);
        }
        public ActionResult AddUser()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if(model.UserImage==null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                HttpPostedFileBase postedfile = model.UserImage;
                Bitmap UserImage = new Bitmap(postedfile.InputStream);
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                if(ext==".jpg"||ext==".png"||ext==".jpeg"||ext=="git")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    string filename = uniquenumber + postedfile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename));
                    model.ImagePath = filename;
                    if(bll.AddUser(model))
                    {
                        ViewBag.ProcessState = General.Message.AddSuccess;
                        ModelState.Clear();//Clear Model element
                        model = new UserDTO();
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Message.GeneralError;
                    }

                }
                else
                {
                    ViewBag.ProcessState = General.Message.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }
        public ActionResult UserList()
        {
            List<UserDTO> dtolist = new List<UserDTO>();
            dtolist = bll.GetUserList();
            return View(dtolist);
        }
        public JsonResult DeleteUser(int ID)
        {
            string imagepath = bll.DeleteUser(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagepath));
            }
            return Json("");
        }
    }
}