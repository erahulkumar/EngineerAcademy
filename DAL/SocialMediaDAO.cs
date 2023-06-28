using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace DAL
{
    public class SocialMediaDAO
    {
        //write sql operation here
        public int AddSocialMedia(E_SocialMedia social)
        {
            try
            {
                using(ENGINEERSEntities Db=new ENGINEERSEntities())
                {
                Db.E_SocialMedia.Add(social);
                Db.SaveChanges();
                }
                
                return social.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SocialMediaDTO> GetSocialMedia()
        {List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            List<E_SocialMedia> list = Db.E_SocialMedia.Where(x => x.isDeleted == false).ToList();
            

                        foreach (var item in list)
                        {
                            SocialMediaDTO dto = new SocialMediaDTO();
                            dto.Name = item.Name;
                            dto.Link = item.Link;
                            dto.ImagePath = item.ImagePath;
                            dto.ID = item.ID;
                            dtolist.Add(dto);
                        }
            }
            
            return dtolist;
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_SocialMedia socialmedia = Db.E_SocialMedia.First(x => x.ID==ID);
            SocialMediaDTO dto = new SocialMediaDTO();
            dto.ID = socialmedia.ID;
            dto.Name = socialmedia.Name;
            dto.Link = socialmedia.Link;
            dto.Size = socialmedia.Size;
            dto.ImagePath = socialmedia.ImagePath;
            return dto;
            }
            
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            try
            { string oldimagepath = "";
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_SocialMedia social = Db.E_SocialMedia.First(x => x.ID == model.ID);
                oldimagepath= social.ImagePath;
                social.Name = model.Name;
                social.Link = model.Link;
                social.Size = model.Size;
                if (model.ImagePath != null)
                    social.ImagePath = model.ImagePath;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                Db.SaveChanges();
                
                }
                return oldimagepath;
                
            }
            catch (Exception ex)
            {

               throw ex;
                
            }
            
        }

        public string DeleteSocialMedia(int ID)
        {
            try
            {
                string deleteimage = "";
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_SocialMedia social = Db.E_SocialMedia.First(x => x.ID == ID);
                deleteimage = social.ImagePath;
                social.isDeleted = true;
                social.DeletedDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                social.LastUpdateDate = DateTime.Now;
                }
                
                return deleteimage;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
