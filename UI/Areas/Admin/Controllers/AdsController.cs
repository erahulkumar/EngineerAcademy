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
    public class AdsController : AuthenticatorController
    {
        // GET: Admin/Ads
        AdsBLL bll = new AdsBLL();
        public ActionResult AddAds()
        {
            AdsDTO dto = new AdsDTO();
            return View(dto);

        }
        [HttpPost]
        public ActionResult AddAds(AdsDTO model)
        {
            if (model.AdsImage == null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedfile = model.AdsImage;
                Bitmap UserImage = new Bitmap(postedfile.InputStream);
                //Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == "git")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    string filename = uniquenumber + postedfile.FileName;
                    UserImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
                    model.ImagePath = filename;
                    bll.AddAds(model);
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model = new AdsDTO();

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
        public ActionResult AdsList()
        {
            List<AdsDTO> dtolist = new List<AdsDTO>();
            dtolist = bll.GetAdsList();
            return View(dtolist);
        }
        public ActionResult UpdateAds(int ID)
        {
            AdsDTO dto = bll.GetAdsWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public ActionResult UpdateAds(AdsDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if (model.AdsImage != null)
                {
                    HttpPostedFileBase postedfile = model.AdsImage;
                    Bitmap UserImage = new Bitmap(postedfile.InputStream);
                    //Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == "git")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        string filename = uniquenumber + postedfile.FileName;
                        UserImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
                        model.ImagePath = filename;
                    }

                }
                string oldImagePath = bll.UpdateAds(model);
                if (model.AdsImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + oldImagePath));
                    }
                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;
            }
            return View(model);
        }
        public JsonResult DeleteAds(int ID)
        {
            string deleteImage = bll.DeleteAds(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + deleteImage)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + deleteImage));
            }
            return Json("");
        }
    }
}