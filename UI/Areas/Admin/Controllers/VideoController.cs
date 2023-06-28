using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
namespace UI.Areas.Admin.Controllers
{
    public class VideoController : AuthenticatorController
    {
        // GET: Admin/Video
        VideoBLL bll = new VideoBLL();
        public ActionResult VideoList()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            dtolist = bll.GetVideoList();
            return View(dtolist);
        }
        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDTO model)
        {
            //<iframe width="560" height="315" src="https://www.youtube.com/embed/9kz-GF7XoEs" frameborder="0"  allowfullscreen></iframe>
            //https://www.youtube.com/watch?v=9kz-GF7XoEs
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string margelink = "https://www.youtube.com/embed/";
                margelink += path;
                model.VideoPath = String.Format(@"<iframe width=""300"" height=""315"" src=""{0}"" frameborder=""0""  allowfullscreen></iframe>",margelink);
                if(bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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
        public ActionResult UpdateVideo(int ID)
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            dtolist = bll.GetVideoList();
            VideoDTO dto = new VideoDTO();
            dto = dtolist.First(x => x.ID == ID);
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateVideo(VideoDTO model)
        {
            if(ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string margelink = "https://www.youtube.com/embed/";
                margelink += path;
                model.VideoPath = String.Format(@"<iframe width=""300"" height=""315"" src=""{0}"" frameborder=""0""  allowfullscreen></iframe>", margelink);
                if(bll.UpdateVideo(model))
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
        public JsonResult DeleteVideo(int ID)
        {
            bll.DeleteVideo(ID);
            return Json("");
        }

    }
}