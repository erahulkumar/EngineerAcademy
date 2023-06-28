using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;
namespace UI.Areas.Admin.Controllers
{
    public class AddressController : AuthenticatorController
    {
        // GET: Admin/Address
        AddressBLL bll = new AddressBLL();
        public ActionResult AddressList()
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddresses();
            return View(list);
        }
        public ActionResult UpdateAddress(int ID)
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddresses();
            AddressDTO dto = list.First(x=>x.ID==ID);
            return View(dto);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if(ModelState.IsValid)
            {
                if(bll.UpdateAddress(model))
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
        public ActionResult AddAddress()
        {
            AddressDTO dto = new AddressDTO();
            return View(dto);
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if(bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
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
        public JsonResult DeleteAddress(int ID)
        {
            bll.DeleteAddress(ID);
            return Json("");
        }
    }
}