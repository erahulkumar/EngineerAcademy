using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.Entity.Validation;
namespace DAL
{
    public class UserDAO 
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)

        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
             UserDTO dto = new UserDTO();
            //var list = Db.E_User;
            //List<E_User> list = Db.E_User.Where(x => x.Username == model.Username && x.Password == model.Password).ToList();
            E_User user = Db.E_User.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user != null && user.ID != 0)
            {
                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Name = user.NameSurName;
                dto.Password = user.Password;
                dto.ImagePath = user.ImagePath;
                dto.isAdmin = user.isAdmin;
            }
            return dto;
            //return the all info to used dto.
            }
            
        }

        public UserDTO GetUserWithID(int ID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {E_User user = Db.E_User.First(x => x.ID == ID);
            UserDTO dto = new UserDTO();
            dto.ID = user.ID;
            dto.Name = user.NameSurName;
            dto.Username = user.Username;
            dto.Password = user.Password;
            dto.isAdmin = user.isAdmin;
            dto.Email = user.Email;
            dto.ImagePath = user.ImagePath;
            return dto;

            }
            
        }

        public int AddUser(E_User user)
        {
            
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                Db.E_User.Add(user);
                Db.SaveChanges();
                }
                
                return user.ID;
            }
            catch (Exception ex)
            {

                throw ex;
                
            }
            
        }

        public string DeleteUser(int ID)
        {
            try
            {
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_User user = Db.E_User.First(x => x.ID == ID);
                user.isDeleted = true;
                user.DeletedDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                user.LastUpdateDate = DateTime.Now;
                Db.SaveChanges();
                return user.ImagePath;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                string oldImagePath = "";
                using (ENGINEERSEntities Db = new ENGINEERSEntities())
                {
                E_User user = Db.E_User.First(x => x.ID == model.ID);
                oldImagePath = user.ImagePath;
                user.NameSurName = model.Name;
                user.Username = model.Username;
                if (model.ImagePath != null)
                    user.ImagePath = model.ImagePath;
                user.Email = model.Email;
                user.LastUpdateUserID = UserStatic.UserID;
                user.LastUpdateDate = DateTime.Now;
                user.isAdmin = model.isAdmin;
                Db.SaveChanges();
               

                }
                 return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserDTO> GetUserList()
        {List<UserDTO> dtolist = new List<UserDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            List<E_User> user = Db.E_User.Where(x => x.isDeleted == false || x.isDeleted == null).OrderBy(x => x.addDate).ToList();
            
            foreach (var item in user)
            {
                UserDTO dto = new UserDTO
                {
                    Username = item.Username,
                    Name = item.NameSurName,
                    ImagePath = item.ImagePath,
                    Email = item.Email,
                    ID = item.ID
                };
                dtolist.Add(dto);

            }
            }
            
            return dtolist;
        }
    }
}
