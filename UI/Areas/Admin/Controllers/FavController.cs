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
    public class FavController : AuthenticatorController
    {
        // GET: Admin/Fav
        FavBLL bll = new FavBLL();
        public ActionResult UpdateFav()
        {
            FavDTO dto = new FavDTO();
            dto = bll.GetFav();
            return View(dto);
        }
        [HttpPost]

        public ActionResult UpdateFav(FavDTO model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            else
            {
                if(model.FavImage!=null)
                {
                    string favname = "";
                    HttpPostedFileBase postedfilefav = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedfilefav.InputStream);
                    Bitmap resizefavImage = new Bitmap(FavImage, 100, 100);
                    string ext = Path.GetExtension(postedfilefav.FileName);
                    if(ext==".jpg" ||ext==".png"||ext==".jpeg"||ext==".git")
                    {
                        string favuniquenumber = Guid.NewGuid().ToString();
                        favname = favuniquenumber + postedfilefav.FileName;
                        resizefavImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favname));
                        model.Fav = favname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Message.ExtensionError;
                    }
                }
                if (model.LogoImage != null)
                {
                    string logoname = "";
                    HttpPostedFileBase postedfilelog = model.LogoImage;
                    Bitmap logImage = new Bitmap(postedfilelog.InputStream);
                    Bitmap resizelogImage = new Bitmap(logImage, 100, 100);
                    string ext = Path.GetExtension(postedfilelog.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".git")
                    {
                        string loguniquenumber = Guid.NewGuid().ToString();
                        logoname = loguniquenumber + postedfilelog.FileName;
                        resizelogImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + logoname));
                        model.Logo = logoname;

                    }
                    else
                    {
                        ViewBag.ProcessState = General.Message.ExtensionError;
                    }
                }
                FavDTO returndto = new FavDTO();
                returndto = bll.UpdateFav(model);
                if(model.FavImage!=null)
                {
                    if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/"+returndto.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav));
                    }    
                }
                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo));
                    }
                }
                ViewBag.ProcessState = General.Message.UpdateSuccess;
            }
            return View(model);
        }
    }
}