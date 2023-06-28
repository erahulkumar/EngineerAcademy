using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace DAL
{
    public class PostDAO
    {
        public int AddPost(E_Post post)
        {
            try
            {
                using(ENGINEERSEntities Db=new ENGINEERSEntities())
                {
                    Db.E_Post.Add(post);
                    Db.SaveChanges();
                }
                
                return post.ID;
            }
            catch (DbEntityValidationException ex)
            {
                Console.Write(ex);
                
            }
            return 0;
        }

        public int AddImage(E_PostImage item)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_PostImage.Add(item);
                Db.SaveChanges();
                }
                return item.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddTag(E_PostTag item)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_PostTag.Add(item);
                Db.SaveChanges();
                }
                
                return item.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostDTO> GetPostList()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                var postlist = (from p in Db.E_Post.Where(x => x.isDeleted == false)
                            join c in Db.E_Category on p.CategoryID equals c.ID
                            select new
                            {
                                ID=p.ID,
                                Title=p.Title,
                                categoryname=c.CategoryName,
                                AddDate=p.addDate
                            }).OrderByDescending(x=>x.AddDate).ToList();
            
                foreach (var item in postlist)
                {
                    PostDTO dto = new PostDTO();
                    dto.Title = item.Title;
                    dto.ID = item.ID;
                    dto.CategoryName = item.categoryname;
                    dto.AddDate = item.AddDate;
                    dtolist.Add(dto);
                }
            }
            
            return dtolist;
        }

        public CountDTO GetAllCounts()
        {
            CountDTO dto = new CountDTO();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                dto.PostCount = Db.E_Post.Where(x => x.isDeleted == false).Count();
            dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false).Count();
            dto.MessageCount = Db.E_Contact.Where(x => x.isDeleted == false).Count();
            dto.ViewCount = Db.E_Post.Where(x => x.isDeleted == false).Sum(x => x.ViewCount);
            
            }
            return dto;
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                var list = (from c in Db.E_Comment.Where(x => x.isDeleted == false)
                        join p in Db.E_Post on c.PostID equals p.ID
                        select new
                        {
                            ID = c.ID,
                            PostTitle = p.Title,
                            Email = c.Email,
                            Content = c.CommentContent,
                            AddDate = c.addDate,
                            isapproved=c.isApproved
                        }
                      ).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.AddDate = item.AddDate;
                    dto.CommentContent = item.Content;
                    dto.isApproved = item.isapproved;
                    dtolist.Add(dto);
                }
            }
            
            return dtolist;
        }

        public void DeleteComment(int ID)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_Comment cmt = Db.E_Comment.First(x => x.ID == ID);
                cmt.isDeleted = true;
                cmt.DeletedDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                cmt.LastUpdateDate = DateTime.Now;
                Db.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void ApproveComment(int ID)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_Comment cmt = Db.E_Comment.First(x => x.ID == ID);
                cmt.isApproved = true;
                cmt.ApproveUserID = UserStatic.UserID;
                cmt.ApproveDate = DateTime.Now;
                cmt.LastUpdateDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                Db.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public List<CommentDTO> GetComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                var list = (from c in Db.E_Comment.Where(x => x.isDeleted == false && x.isApproved == false)
                        join p in Db.E_Post on c.PostID equals p.ID
                        select new
                        {
                            ID=c.ID,
                            PostTitle=p.Title,
                            Email=c.Email,
                            Content=c.CommentContent,
                            AddDate=c.addDate
                        }
                      ).OrderBy(x=>x.AddDate).ToList();
            foreach (var item in list)
            {
                CommentDTO dto = new CommentDTO();
                dto.ID = item.ID;
                dto.PostTitle = item.PostTitle;
                dto.Email = item.Email;
                dto.AddDate = item.AddDate;
                dto.CommentContent = item.Content;
                dtolist.Add(dto);
            }
            }
            
            return dtolist;
        }

        public void AddComment(E_Comment comment)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_Comment.Add(comment);
                Db.SaveChanges();
                }
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostDTO> GetHotNews()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                var postlist = (from p in Db.E_Post.Where(x => x.isDeleted == false && x.Area1 == true)
                            join c in Db.E_Category on p.CategoryID equals c.ID
                            select new
                            {
                                ID = p.ID,
                                Title = p.Title,
                                categoryname = c.CategoryName,
                                AddDate = p.addDate,
                                seolink = p.SeoLink
                            }).OrderByDescending(x => x.AddDate).Take(8).ToList();
            
            foreach (var item in postlist)
            {
                PostDTO dto = new PostDTO();
                dto.Title = item.Title;
                dto.ID = item.ID;
                dto.CategoryName = item.categoryname;
                dto.AddDate = item.AddDate;
                dto.SeoLink = item.seolink;
                dtolist.Add(dto);
            }
            }
            
            return dtolist;
        }

        public int GetCommentCount()
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                return Db.E_Comment.Where(x => x.isDeleted == false && x.isApproved == false).Count();
            }
         }

        public int GetMessageCount()
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                return Db.E_Contact.Where(x => x.isDeleted == false && x.isRead == false).Count();
        
            }
        }

        public PostDTO GetPostWithID(int ID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Post post = Db.E_Post.First(x => x.ID == ID);
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.Language = post.LanguageName;
            dto.Notification = post.Notification;
            dto.Area1 = post.Area1;
            dto.Area2 = post.Area2;
            dto.Area3 = post.Area3;
            dto.CategoryID = post.CategoryID;
            return dto;
            }
            

        }

        public List<PostImageDTO> GetPostImagesWithID(int PostID)
        {
            List<PostImageDTO> dtolist = new List<PostImageDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            List<E_PostImage> postimagelist = Db.E_PostImage.Where(x => x.isDeleted == false && x.PostID ==PostID).ToList();
            
            foreach (var item in postimagelist)
            {
                PostImageDTO dto = new PostImageDTO();
                dto.ID = item.ID;
                dto.ImagePath = item.ImagePath;
                dtolist.Add(dto);
            }
            }
            
            return dtolist;
        }

        public List<E_PostTag> GetPostTagsWithID(int PostID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                return Db.E_PostTag.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();

            }
            
        }

        public void UpdatePost(PostDTO model)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Post post = Db.E_Post.First(x => x.ID == model.ID);
            post.Title = model.Title;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.CategoryID = model.CategoryID;
            post.LanguageName = model.Language;
            post.LastUpdateUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            post.Notification = model.Notification;
            post.PostContent = model.PostContent;
            post.ShortContent = model.ShortContent;
            post.SeoLink = model.SeoLink;
            post.Slider = model.Slider;
            Db.SaveChanges();
            }
            
        }

        public string DeletePostImage(int ID)
        {
            try
            {
                string deletepostimage = "";
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_PostImage postImage = Db.E_PostImage.First(x => x.ID == ID);
                deletepostimage = postImage.ImagePath;
                postImage.isDeleted = true;
                postImage.DeletedDate = DateTime.Now;
                postImage.LastUpdateUserID = UserStatic.UserID;
                postImage.LastUpdateDate = DateTime.Now;
                Db.SaveChanges();
                
                }
                return deletepostimage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
                List<PostImageDTO> dtolist = new List<PostImageDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                E_Post post = Db.E_Post.First(x => x.ID == ID);
                post.isDeleted = true;
                post.DeletedDate = DateTime.Now;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                Db.SaveChanges();
                //now delete the postimage every one image
                List<E_PostImage> imagelist = Db.E_PostImage.Where(x => x.PostID == ID).ToList();
                
                foreach (var item in imagelist)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = item.ImagePath;
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                    item.LastUpdateDate = DateTime.Now;
                    dtolist.Add(dto);
                }
                Db.SaveChanges();
            }
               
                return dtolist;
        }

        public void DeleteTags(int PostID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            List<E_PostTag> list = Db.E_PostTag.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();
            foreach (var item in list)
            {
                item.isDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.LastUpdateDate = DateTime.Now;
                item.LastUpdateUserID = UserStatic.UserID;
            }
            Db.SaveChanges();
            }
            
        }
    }
}
