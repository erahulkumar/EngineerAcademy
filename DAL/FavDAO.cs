using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FavDAO
    {
        public FavDTO GetFav()
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_FavLogoTitle fav = Db.E_FavLogoTitle.First();
            FavDTO dto = new FavDTO();
            dto.ID = fav.ID;
            dto.Title = fav.Title;
            dto.Logo = fav.Logo;
            dto.Fav = fav.Fav;
            return dto;
            }
            
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {E_FavLogoTitle fav = Db.E_FavLogoTitle.First();
                FavDTO dto = new FavDTO();
                dto.ID = fav.ID;
                dto.Fav = fav.Fav;
                dto.Logo = fav.Logo;
                fav.Title = model.Title;
                if (model.Logo != null)
                    fav.Logo = model.Logo;
                if (model.Fav != null)
                    fav.Fav = model.Fav;
                Db.SaveChanges();
                return dto;

                }
                
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
