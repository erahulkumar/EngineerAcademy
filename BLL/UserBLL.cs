using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class UserBLL
    {
        UserDAO userdao = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userdao.GetUserWithUsernameAndPassword(model);
            return dto;
        }

        public UserDTO GetUserWithID(int ID)
        {
            return userdao.GetUserWithID(ID);
        }

        public bool AddUser(UserDTO model)
        {
            E_User user = new E_User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email   ;
            user.ImagePath = model.ImagePath;
            user.NameSurName = model.Name;
            user.isDeleted = false;
            user.addDate = DateTime.Now;
            user.isAdmin = model.isAdmin;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            int ID = userdao.AddUser(user);
            LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID);
            return true;
        }

        public List<UserDTO> GetUserList()
        {
            // List<UserDTO> dtolist = new List<UserDTO>();
            //dtolist =userdao.GetUserList();
            //return dtolist;
            return userdao.GetUserList();
        }

        public string UpdateUser(UserDTO model)
        {
            string oldImagePath = userdao.UpdateUser(model);
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID);
            return oldImagePath;
        }

        public string DeleteUser(int ID)
        {
            string imagepath = userdao.DeleteUser(ID);
            LogDAO.AddLog(General.ProcessType.UserDelete, General.TableName.User,ID);
            return imagepath;
        }
    }
}
