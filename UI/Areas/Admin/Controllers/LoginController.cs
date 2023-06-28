using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBLL userbll = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            if (model.Username!=null && model.Password!=null)
            {
                UserDTO user = userbll.GetUserWithUsernameAndPassword(model);
                if (user.ID!=0)
                {
                   UserStatic.UserID = user.ID;
                   UserStatic.isAdmin = user.isAdmin;
                   UserStatic.NameSurname = user.Name;
                   UserStatic.ImagePath = user.ImagePath;
                   LogBLL.AddLog(General.ProcessType.Login,General.TableName.Login,user.ID);
                   return RedirectToAction("PostList", "Post");
                }
                else
                    return View(model);
            }
            else
                return View(model);

        }
        
    }
}