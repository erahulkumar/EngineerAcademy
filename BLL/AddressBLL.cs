using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AddressBLL
    {
        AddressDAO dao = new AddressDAO();
        public bool AddAddress(AddressDTO model)
        {
            E_Address adds = new E_Address();
            adds.Address = model.AddressContent;
            adds.Email = model.Email;
            adds.Phone = model.Phone;
            adds.MapPathLarge = model.MapPathLarge;
            adds.MapPathSmall = model.MapPathSmall;
            adds.addDate = DateTime.Now;
            adds.LastUpdateDate = DateTime.Now;
            adds.LastUpdateUserID = UserStatic.UserID;
            int ID = dao.AddAddress(adds);
            LogDAO.AddLog(General.ProcessType.AddressAdd, General.TableName.Address, ID);
            return true;
        }

        public List<AddressDTO> GetAddresses()
        {
            return dao.GetAddresses();
        }

        public bool UpdateAddress(AddressDTO model)
        {
            dao.UpdateAddress(model);
            LogDAO.AddLog(General.ProcessType.AddressUpdate, General.TableName.Address, model.ID);
            return true;
        }

        public void DeleteAddress(int ID)
        {
            dao.DeleteAddress(ID);
            LogDAO.AddLog(General.ProcessType.AdderssDelete, General.TableName.Address, ID);
        }
    }
}
