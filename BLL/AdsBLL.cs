using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AdsBLL
    {
        AdsDAO dao = new AdsDAO();
        public void AddAds(AdsDTO model)
        {
            E_Ads ads = new E_Ads();
            ads.Name = model.Name;
            ads.Link = model.Link;
            ads.ImagePath = model.ImagePath;
            ads.Size = model.ImageSize;
            ads.addDate = DateTime.Now;
            ads.LastUpdateUserID = UserStatic.UserID;
            ads.LastUpdateDate = DateTime.Now;
            int ID = dao.AddAds(ads);
            LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Ads, ID);
        }

        public List<AdsDTO> GetAdsList()
        {
            return dao.GetAdsList();
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            return dao.GetAdsWithID(ID);
        }

        public string UpdateAds(AdsDTO model)
        {
            string oldImagePath = dao.UpdateAds(model);
            LogDAO.AddLog(General.ProcessType.AdsUpdate, General.TableName.Ads, model.ID);
            return oldImagePath;
        }

        public string DeleteAds(int ID)
        {
            string deleteimage=dao.DeleteAds(ID);
            LogDAO.AddLog(General.ProcessType.AdsDelete, General.TableName.Ads, ID);
            return deleteimage;
        }
    }
}
