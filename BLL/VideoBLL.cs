using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class VideoBLL
    {
        VideoDAO dao = new VideoDAO();
        public bool AddVideo(VideoDTO model)
        {
            E_Video video = new E_Video();
            video.Title = model.Title;
            video.VideoPath = model.VideoPath;
            video.OriginalVideoPath = model.OriginalVideoPath;
            video.addDate = DateTime.Now;
            video.LastUpdateUserID = UserStatic.UserID;
            video.LastUpdateDate = DateTime.Now;
            int ID = dao.AddVideo(video);
            LogDAO.AddLog(General.ProcessType.VideoAdd, General.TableName.Video, ID);
            return true;
        }

        public List<VideoDTO> GetVideoList()
        {
            return dao.GetVideoList();
        }

        public bool UpdateVideo(VideoDTO model)
        {
            dao.UpdateVideo(model);
            LogDAO.AddLog(General.ProcessType.VideoUpdate, General.TableName.Video, model.ID);
            return true;
        }

        public void DeleteVideo(int ID)
        {
            dao.DeleteVideo(ID);
            LogDAO.AddLog(General.ProcessType.VideoDelete, General.TableName.Video, ID);
        }
    }
}
