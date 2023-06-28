using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO 
    {
        public int AddVideo(E_Video video)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_Video.Add(video);
                Db.SaveChanges();
                }
                
                return video.ID;
            }
            catch ( Exception ex)
            {

                throw ex;
            }
        }

        public List<VideoDTO> GetVideoList()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {List<E_Video> vlist = Db.E_Video.Where(x => x.isDeleted == false).OrderBy(x => x.addDate).ToList();
            
            foreach (var item in vlist)
            {
                VideoDTO dto = new VideoDTO();
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.ID = item.ID;
                dto.AddDate = item.addDate;
                dtolist.Add(dto);
            }

            }
            
            return dtolist;
        }

        public void UpdateVideo(VideoDTO model)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {E_Video video = Db.E_Video.First(x => x.ID == model.ID);
            video.VideoPath = model.VideoPath;
            video.OriginalVideoPath = model.OriginalVideoPath;
            video.Title = model.Title;
            video.LastUpdateDate = DateTime.Now;
            video.LastUpdateUserID = UserStatic.UserID;
            Db.SaveChanges();

            }
            
        }

        public void DeleteVideo(int ID)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_Video video = Db.E_Video.First(x => x.ID == ID);
                video.isDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                Db.SaveChanges();
            }
                }
               
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
