using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MetaDAO
    {
        public int AddMeta(E_Meta meta)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_Meta.Add(meta);
                    Db.SaveChanges();
                }
                return meta.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> metalist = new List<MetaDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            List<E_Meta> list = Db.E_Meta.Where(x => x.isDeleted == false).OrderBy(x => x.addDate).ToList();
            foreach (var item in list)
            {
                MetaDTO dto = new MetaDTO();//DTO-MetaDTO.cs is define the variable of E_Meta.
                dto.MetaID = item.ID;
                dto.Name = item.Name;
                dto.MetaContent = item.MetaContent;
                metalist.Add(dto);

            }
            }
            
            return metalist;
        }

        public MetaDTO GetMetaWithID(int ID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Meta meta = Db.E_Meta.First(x => x.ID == ID);
            MetaDTO dto = new MetaDTO();
            dto.MetaID = meta.ID;
            dto.Name = meta.Name;
            dto.MetaContent = meta.MetaContent;
            return dto;
            }
            
        }

        public void UpdateMeta(MetaDTO model)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_Meta meta = Db.E_Meta.First(x => x.ID == model.MetaID);
                meta.Name = model.Name;
                meta.MetaContent = model.MetaContent;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;

                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteMeta(int ID)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_Meta meta = Db.E_Meta.First(x => x.ID == ID);
                meta.isDeleted = true;
                meta.DeletedDate = DateTime.Now;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;
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
