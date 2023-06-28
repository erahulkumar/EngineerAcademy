using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class PostBLL
    {
        PostDAO dao = new PostDAO();
        public bool AddPost(PostDTO model)
        {
            E_Post post = new E_Post();
            post.Title = model.Title;
            post.PostContent = model.PostContent;
            post.ShortContent = model.ShortContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;
            post.SeoLink = SeoLink.GenerateUrl(model.Title);
            post.LanguageName = model.Language;
            post.addDate = DateTime.Now;
            post.AddUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            post.LastUpdateUserID = UserStatic.UserID;
            int ID = dao.AddPost(post);
            if(ID!=0)
            {
                LogDAO.AddLog(General.ProcessType.PostAdd, General.TableName.Post, ID);
                //end Post table operation
                //call the method to save post image in db
                SavePostImage(model.PostImages, ID);
                //now create a AddTag method
                AddTag(model.TagText, ID);
                return true;
            }
            else
            {
                return false;
            }
            //return true;
        }

        public CountDTO GetAllCounts()
        {
            return dao.GetAllCounts();
        }

        public List<CommentDTO> GetAllComments()
        {
            return dao.GetAllComments();
        }

        public void DeleteComment(int ID)
        {
            dao.DeleteComment(ID);
            LogDAO.AddLog(General.ProcessType.CommentDelete, General.TableName.Comment, ID);

        }

        public void ApproveComment(int ID)
        {
            dao.ApproveComment(ID);
            LogDAO.AddLog(General.ProcessType.CommentApprove, General.TableName.Comment, ID);

        }

        public List<CommentDTO> GetComments()
        {
            return dao.GetComments();
        }

        public bool AddComment(GeneralDTO model)
        {
            E_Comment comment = new E_Comment();
            comment.PostID = model.PostID;
            comment.NameSurName = model.Name;
            comment.Email = model.Email;
            comment.CommentContent = model.Message;
            comment.addDate = DateTime.Now;
            dao.AddComment(comment);
            return true;
        }

        public List<PostDTO> GetPostList()
        {
            return dao.GetPostList();
        }

        private void AddTag(string tagText, int PostID)
        {
            string[] tags;
            tags = tagText.Split(',');
            List<E_PostTag> taglist = new List<E_PostTag>();
            foreach (var item in tags)
            {
                E_PostTag tag = new E_PostTag();
                tag.PostID = PostID;
                tag.TagContent = item;
                tag.addDate = DateTime.Now;
                tag.LastUpdateDate = DateTime.Now;
                tag.LastUpdateUserID = UserStatic.UserID;
                taglist.Add(tag);
            }
            foreach (var item in taglist)
            {
                int tagID = dao.AddTag(item);
                LogDAO.AddLog(General.ProcessType.TagAdd, General.TableName.Tag, tagID);            }
            }

        void SavePostImage(List<PostImageDTO> list,int PostID)
        {
            //start PostImage table Operation
            List<E_PostImage> imagelist = new List<E_PostImage>();
            foreach (var item in list)
            {
                E_PostImage image = new E_PostImage();
                image.PostID = PostID;
                image.ImagePath = item.ImagePath;
                image.addDate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserID;
                image.LastUpdateDate = DateTime.Now;
                imagelist.Add(image);
            }
            foreach (var item in imagelist)
            {
                int imageID = dao.AddImage(item);
                LogDAO.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID);

            }
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            dto=dao.GetPostWithID(ID);
            dto.PostImages = dao.GetPostImagesWithID(ID);
            List<E_PostTag> taglist = dao.GetPostTagsWithID(ID);
            string tagvalue = "";
            foreach (var item in taglist)
            {
                tagvalue += item.TagContent;
                tagvalue += ",";
            }
            dto.TagText = tagvalue;
            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            LogDAO.AddLog(General.ProcessType.PostUpdate, General.TableName.Post, model.ID);
            if(model.PostImages!=null)
            {
                SavePostImage(model.PostImages, model.ID);
            }
            dao.DeleteTags(model.ID);
            AddTag(model.TagText, model.ID);
            return true;
        }

        public string DeletePostImage(int ID)
        {
            string deleteimagepath = dao.DeletePostImage(ID);
            LogDAO.AddLog(General.ProcessType.ImageDelete, General.TableName.Image, ID);
            return deleteimagepath;


        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> imagelist = dao.DeletePost(ID);
            LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post,ID);
            return imagelist;
        }

        public CountDTO GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto.MessageCount = dao.GetMessageCount();
            dto.CommentCount = dao.GetCommentCount();
            return dto;
        }
    }
}
