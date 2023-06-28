using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO dao = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO model)
        {
            E_SocialMedia social = new E_SocialMedia();
            social.Name = model.Name;
            social.Link = model.Link;
            social.ImagePath = model.ImagePath;
            social.Size = model.Size;
            //Size remove the db table column
            //social.isDeleted = false;
            social.addDate = DateTime.Now;
            social.LastUpdateUserID = UserStatic.UserID;
            social.LastUpdateDate = DateTime.Now;
            int ID = dao.AddSocialMedia(social);
            LogDAO.AddLog(General.ProcessType.SocialAdd, General.TableName.Social, ID);
            return true;
        }

        public List<SocialMediaDTO> GetSocialMedia()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = dao.GetSocialMedia();
            return dtolist;
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMediaDTO dto = dao.GetSocialMediaWithID(ID);
            return dto;
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            //old image path update
            string oldimagepath = dao.UpdateSocialMedia(model);
            LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, model.ID);
            return oldimagepath;
        }

        public string DeleteSocialMedia(int ID)
        {
            string deleteimage = dao.DeleteSocialMedia(ID);
            LogDAO.AddLog(General.ProcessType.SocialDelete, General.TableName.Social, ID);
            return deleteimage;

        }
    }
}
