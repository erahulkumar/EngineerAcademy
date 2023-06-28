using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class ContactBLL
    {
        ContactDAO dao = new ContactDAO();
        public bool AddContact(GeneralDTO model)
        {
            E_Contact contact = new E_Contact();
            contact.Subject = model.Subject;
            contact.NameSurName = model.Name;
            contact.Email = model.Email;
            contact.Message = model.Message;
            contact.addDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            dao.AddContact(contact);
            return true;
        }

        public void ReadMessage(int ID)
        {
            dao.ReadMessage(ID);
            LogDAO.AddLog(General.ProcessType.ContactRead, General.TableName.Contact, ID);

        }

        public void DeleteMessage(int ID)
        {
            dao.DeleteMessage(ID);
            LogDAO.AddLog(General.ProcessType.ContactDelete, General.TableName.Contact, ID);

        }

        public List<ContactDTO> GetAllMessages()
        {
            return dao.GetAllMessages();
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            return dao.UnreadMessages();
        }

    }
}
