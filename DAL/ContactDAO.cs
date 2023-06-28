using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class ContactDAO
    {
        public void AddContact(E_Contact contact)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_Contact.Add(contact);
                Db.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContactDTO> UnreadMessages()
        {
            List<ContactDTO> dtolist = new List<ContactDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            List<E_Contact> list = Db.E_Contact.Where(x => x.isRead == false && x.isDeleted == false).OrderByDescending(x => x.addDate).ToList();
            foreach (var item in list)
            {
                ContactDTO dto = new ContactDTO();
                dto.ID = item.ID;
                dto.Subject = item.Subject;
                dto.Name = item.NameSurName;
                dto.Email = item.Email;
                dto.Message = item.Message;
                dto.AddDate = item.addDate;
                dto.isRead = item.isRead;
                dtolist.Add(dto);
            }
            }
            
            return dtolist;
        }

        public List<ContactDTO> GetAllMessages()
        {
            List<ContactDTO> dtolist = new List<ContactDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {List<E_Contact> list = Db.E_Contact.Where(x => x.isDeleted == false).OrderByDescending(x => x.addDate).ToList();
            foreach (var item in list)
            {
                ContactDTO dto = new ContactDTO();
                dto.ID = item.ID;
                dto.Subject = item.Subject;
                dto.Name = item.NameSurName;
                dto.Email = item.Email;
                dto.Message = item.Message;
                dto.AddDate = item.addDate;
                dto.isRead = item.isRead;
                dtolist.Add(dto);
            }

            }
            
            return dtolist;
        }

        public void DeleteMessage(int ID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Contact contact = Db.E_Contact.First(x => x.ID == ID);
            contact.isDeleted = true;
            contact.DeletedDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            contact.LastUpdateUserID = UserStatic.UserID;
            Db.SaveChanges();
            }
            
        }

        public void ReadMessage(int ID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {E_Contact contact = Db.E_Contact.First(x => x.ID == ID);
            contact.isRead = true;
            contact.ReadUserID = UserStatic.UserID;
            contact.LastUpdateUserID = UserStatic.UserID;
            contact.LastUpdateDate = DateTime.Now;
            Db.SaveChanges();

            }
            
        }
    }
}
