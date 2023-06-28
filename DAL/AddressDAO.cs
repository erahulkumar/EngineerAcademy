using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddressDAO
    {
        public int AddAddress(E_Address adds)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_Address.Add(adds);
                Db.SaveChanges();
                }
                
                return adds.ID;
            }
            catch (Exception ex)
            {
              throw ex;
            }
        }

        public List<AddressDTO> GetAddresses()
        { 
            List<AddressDTO> dtolist = new List<AddressDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {


                List<E_Address> AddsList = Db.E_Address.Where(x => x.isDeleted == false).OrderBy(x => x.addDate).ToList();

                foreach (var item in AddsList)
                {
                    AddressDTO dto = new AddressDTO();
                    dto.ID = item.ID;
                    dto.AddressContent = item.Address;
                    dto.Email = item.Email;
                    dto.Phone = item.Phone;
                    dto.MapPathLarge = item.MapPathLarge;
                    dto.MapPathSmall = item.MapPathSmall;
                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public void UpdateAddress(AddressDTO model)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {E_Address ads = Db.E_Address.First(x => x.ID == model.ID);
                ads.Address = model.AddressContent;
                ads.Email = model.Email;
                ads.Phone = model.Phone;
                ads.MapPathLarge = model.MapPathLarge;
                ads.MapPathSmall = model.MapPathSmall;
                Db.SaveChanges();

                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteAddress(int ID)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {//delete Address ID is parmanent
                var tan = Db.E_Address.Find(ID);
                Db.E_Address.Remove(tan);
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
