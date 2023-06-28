using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
namespace UI.Areas.Admin.Controllers
{
    public class CommentController : AuthenticatorController
    {
        // GET: Admin/Comment
        PostBLL bll = new PostBLL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UnapprovedComment()
        {
            List<CommentDTO> commentlist = new List<CommentDTO>();
            commentlist = bll.GetComments();
            return View(commentlist);
        }
        public ActionResult AllComments()
        {
            List<CommentDTO> commentlist = bll.GetAllComments();
            return View(commentlist);
        }
        public ActionResult ApproveComment(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("UnapprovedComment", "Comment");
        }
        public ActionResult ApproveComment2(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("UnapprovedComment", "Comment");
        }
        public JsonResult DeleteComment(int ID)
        {
            bll.DeleteComment(ID);
            return Json("");
        }
    }
}