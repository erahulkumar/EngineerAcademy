using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : AuthenticatorController
    {
        // GET: Admin/SocialMedia
        SocialMediaBLL bll = new SocialMediaBLL();
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if (model.SocialImage == null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                HttpPostedFileBase postedfile = model.SocialImage;
                Bitmap SocialMedia = new Bitmap(postedfile.InputStream);
                string ext = Path.GetExtension(postedfile.FileName).ToLower();
                if (ext==".jpg" || ext==".png" || ext==".gif" || ext==".jpeg")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    string filename = uniquenumber + postedfile.FileName;
                    //string SaveImagePath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/"+filename));
                    SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/"+filename));
                    model.ImagePath = filename; 
                    if(bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Message.AddSuccess;
                        model = new SocialMediaDTO();
                        ModelState.Clear();
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
        
        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = bll.GetSocialMedia();

            return View(dtolist);
             
        }

        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);
            return View(dto);

        }
        [HttpPost]

        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if (model.SocialImage != null)
                {
                    HttpPostedFileBase postedfile = model.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedfile.InputStream);
                    string ext = Path.GetExtension(postedfile.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        string filename = uniquenumber + postedfile.FileName;
                        //string SaveImagePath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/"+filename));
                        SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/" + filename));
                        model.ImagePath = filename;
                    }
                }
                string oldimagepath = bll.UpdateSocialMedia(model);
                if(model.SocialImage!=null)
                {
                    if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/" + oldimagepath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/" + oldimagepath));
                    }
                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;
            }
            return View(model);
        }
        public JsonResult DeleteSocialMedia(int ID)
        {
            string deleteimage = bll.DeleteSocialMedia(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/" + deleteimage)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImage/" + deleteimage));
            }
            return Json("");
        }


    }
}