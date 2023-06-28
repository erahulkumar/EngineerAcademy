using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : AuthenticatorController
    {
        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }
        MetaBLL bll = new MetaBLL();
        public ActionResult AddMeta()
        {
            MetaDTO dto = new MetaDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult AddMeta(MetaDTO model)
        {
            if(ModelState.IsValid)
            {
                if(bll.AddMeta(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
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
            MetaDTO newmodel = new MetaDTO();
            return View(newmodel);
        }
        public ActionResult MetaList()
        {
            List<MetaDTO> model = new List<MetaDTO>();
            model = bll.GetMetaData();
            return View(model);
        }
        public ActionResult UpdateMeta(int ID)
        {
            MetaDTO model = new MetaDTO();
            model = bll.GetMetaWithID(ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO model)
        {
            if(ModelState.IsValid)
            {
                if(bll.UpdateMeta(model))
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
        public JsonResult DeleteMeta(int ID)
        {
            bll.DeleteMeta(ID);
            return Json("");
        }
    }
}