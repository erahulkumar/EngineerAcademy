using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO 
    {
        //write sql operation
        public int AddAds(E_Ads ads)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_Ads.Add(ads);
                Db.SaveChanges();
                }
                
                return ads.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AdsDTO> GetAdsList()
        {List<AdsDTO> dtolist = new List<AdsDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {List<E_Ads> list = Db.E_Ads.Where(x => x.isDeleted == false).OrderBy(x=>x.addDate).ToList();
            
            foreach (var item in list)
            {
                AdsDTO dto = new AdsDTO();
                dto.Name = item.Name;
                dto.Link = item.Link;
                dto.ImagePath = item.ImagePath;
                dto.ImageSize = item.Size;
                dto.ID = item.ID;
                dtolist.Add(dto);
            }

            }
            
            return dtolist;
        }

        public string UpdateAds(AdsDTO model)
        {
            try
            {   
                string oldImagePath = "";
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                    E_Ads ads = Db.E_Ads.First(x => x.ID == model.ID);
                    oldImagePath= ads.ImagePath;
                    ads.Name = model.Name;
                    ads.Link = model.Link;
                    if (model.ImagePath != null)
                        ads.ImagePath = model.ImagePath;
                    ads.Size = model.ImageSize;
                    ads.LastUpdateUserID = UserStatic.UserID;
                    ads.LastUpdateDate = DateTime.Now;
                    Db.SaveChanges();

                }
                
                return oldImagePath;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteAds(int ID)
        {
            try
            {
                string deleteimage = "";
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_Ads ads = Db.E_Ads.First(x => x.ID == ID);
                deleteimage = ads.ImagePath;
                ads.isDeleted = true;
                ads.DeletedDate = DateTime.Now;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
                Db.SaveChanges();
                }
                
                return deleteimage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Ads ads = Db.E_Ads.First(x => x.ID == ID);
            AdsDTO dto = new AdsDTO();
            dto.ID = ads.ID;
            dto.Name = ads.Name;
            dto.Link = ads.Link;
            dto.ImagePath = ads.ImagePath;
            dto.ImageSize = ads.Size;
            return dto;
            }
            
        }
    }
}
