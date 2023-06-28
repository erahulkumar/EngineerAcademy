using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO
    {
        public List<PostDTO> GetSliderPost()
        {
            List<PostDTO> postlist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {var list = (from p in Db.E_Post.Where(x => x.Slider == true && x.isDeleted == false)
                        join c in Db.E_Category on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title=p.Title,
                            categoryName=c.CategoryName,
                            seolink=p.SeoLink,
                            viewcount=p.ViewCount,
                            adddate=p.addDate

                        }
                      ).OrderByDescending(x=>x.adddate).Take(8).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                E_PostImage image = Db.E_PostImage.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.adddate;
                postlist.Add(dto);
            }

            }
            
            return postlist;
        }

        public List<VideoDTO> GetAllVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {List<E_Video> listvideo = Db.E_Video.Where(x => x.isDeleted == false).OrderByDescending(x => x.addDate).ToList();
            foreach (var item in listvideo)
            {
                VideoDTO list = new VideoDTO();
                list.ID = item.ID;
                list.Title = item.Title;
                list.VideoPath = item.VideoPath;
                list.OriginalVideoPath = item.OriginalVideoPath;
                list.AddDate = item.addDate;
                dtolist.Add(list);
            }

            }
            
            return dtolist;
        }

        public List<PostDTO> GetCategoryPostList(int categoryID)
        {
            List<PostDTO> postlist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            var list = (from p in Db.E_Post.Where(x => x.isDeleted == false && x.CategoryID==categoryID)
                        join c in Db.E_Category on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            adddate = p.addDate

                        }
                      ).OrderByDescending(x => x.adddate).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                E_PostImage image = Db.E_PostImage.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.adddate;
                postlist.Add(dto);
            }
            }
            
            return postlist;
        }

        public List<PostDTO> GetSearchPost(string searchText)
        {
            List<PostDTO> postlist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                var list = (from p in Db.E_Post.Where(x => x.isDeleted == false && (x.Title.Contains(searchText)||x.PostContent.Contains(searchText)))
                        join c in Db.E_Category on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            adddate = p.addDate

                        }
                      ).OrderByDescending(x => x.adddate).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                E_PostImage image = Db.E_PostImage.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.adddate;
                postlist.Add(dto);
            }

            }
            
            return postlist;
        }

        public PostDTO GetPostDetail(int ID)
        {
            List<CommentDTO> commentdtolist = new List<CommentDTO>();
            List<PostImageDTO> imagedtolist = new List<PostImageDTO>();
            List<TagDTO> taglist = new List<TagDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Post post = Db.E_Post.First(x => x.ID == ID);
            post.ViewCount++;
            Db.SaveChanges();
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.Language = post.LanguageName;
            dto.CategoryID = post.CategoryID;
            dto.CategoryName = (Db.E_Category.First(x => x.ID == dto.CategoryID)).CategoryName;
            List<E_PostImage> images = Db.E_PostImage.Where(x => x.isDeleted == false && x.PostID == ID).ToList();
            
            foreach (var item in images)
            {
                PostImageDTO imagedto = new PostImageDTO();
                imagedto.ID = item.ID;
                imagedto.ImagePath = item.ImagePath;
                imagedtolist.Add(imagedto);
            }
            dto.PostImages = imagedtolist;
            dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).Count();
            List<E_Comment> comments = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).ToList();
            
            foreach (var item in comments)
            {
                CommentDTO commentdto = new CommentDTO();
                commentdto.ID = item.ID;
                commentdto.AddDate = item.addDate;
                commentdto.CommentContent = item.CommentContent;
                commentdto.Name = item.NameSurName;
                commentdto.Email = item.Email;
                commentdtolist.Add(commentdto);
            }
            dto.CommentList = commentdtolist;
            List<E_PostTag> tags = Db.E_PostTag.Where(x => x.isDeleted == false && x.PostID == ID).ToList();
            
            foreach (var item in tags)
            {
                TagDTO tagdto = new TagDTO();
                tagdto.ID = item.ID;
                tagdto.TagContent = item.TagContent;
                taglist.Add(tagdto);
            }
            dto.TagList = taglist;
            return dto;

            }
            
        }

        public List<VideoDTO> GetVideos()
        {

            List<VideoDTO> dtolist = new List<VideoDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            List<E_Video> listvideo = Db.E_Video.Where(x => x.isDeleted == false).OrderByDescending(x => x.addDate).Take(3).ToList();
            foreach (var item in listvideo)
            {
                VideoDTO list = new VideoDTO();
                list.ID = item.ID;
                list.Title = item.Title;
                list.VideoPath = item.VideoPath;
                list.OriginalVideoPath = item.OriginalVideoPath;
                list.AddDate = item.addDate;
                dtolist.Add(list);
            }
            }
            
            return dtolist;
        }

        public List<PostDTO> GetMostViewedPost()
        {
            List<PostDTO> postlist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
                var list = (from p in Db.E_Post.Where(x => x.isDeleted == false)
                        join c in Db.E_Category on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            adddate = p.addDate

                        }
                      ).OrderByDescending(x => x.viewcount).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                E_PostImage image = Db.E_PostImage.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.adddate;
                postlist.Add(dto);
            }
            return postlist;

            }
            
        }

        public List<PostDTO> GetPopularPost()
        {
            List<PostDTO> postlist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {var list = (from p in Db.E_Post.Where(x => x.isDeleted == false && x.Area2==true)
                        join c in Db.E_Category on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            adddate = p.addDate

                        }
                      ).OrderByDescending(x => x.adddate).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                E_PostImage image = Db.E_PostImage.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.adddate;
                postlist.Add(dto);
            }

            }
            return postlist;
        }

        public List<PostDTO> GetBreakingPost()
        {
            List<PostDTO> postlist = new List<PostDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {var list = (from p in Db.E_Post.Where(x => x.Slider == false && x.isDeleted == false)
                        join c in Db.E_Category on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            adddate = p.addDate

                        }
                      ).OrderByDescending(x => x.adddate).Take(8).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                E_PostImage image = Db.E_PostImage.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = Db.E_Comment.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.adddate;
                postlist.Add(dto);
            }

            }
            
            return postlist;
        }
    }
}
