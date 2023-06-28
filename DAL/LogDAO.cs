using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DTO;
namespace DAL
{
    public class LogDAO
    {
        public static void AddLog(int ProceesType, string TableName, int ProcessID)
        {
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {
            E_Log_Table log = new E_Log_Table();
            //Object create the E_Log_table to the database
            log.UserID = UserStatic.UserID;
            log.ProcessType = ProceesType;
            log.ProcessID = ProcessID;
            log.ProcessCategoryType = TableName;
            log.ProcessDate = DateTime.Now;
            log.IPAdress = HttpContext.Current.Request.UserHostAddress;//Get the IP addressfrom to current Host User.
            Db.E_Log_Table.Add(log);//Add data into used of db in to E_Log_table
            Db.SaveChanges();//Applied the changed.
            }
            
        }

        public List<LogDTO> GetLogs()
        {
            List<LogDTO> dtolist = new List<LogDTO>();
            using (ENGINEERSEntities Db = new ENGINEERSEntities())
            {var list = (from l in Db.E_Log_Table
                        join u in Db.E_User on l.UserID equals u.ID
                        join p in Db.E_ProcessType on l.ProcessType equals p.ID
                        select new
                        {
                            ID=l.ID,
                            UserName=u.Username,
                            TableName=l.ProcessCategoryType,
                            TableID=l.ProcessID,
                            ProcessName=p.ProcessName,
                            ProcessDate=l.ProcessDate,
                            ipAddress=l.IPAdress
                        }).OrderByDescending(x=>x.ProcessDate).ToList();
            foreach (var item in list)
            {
                LogDTO dto = new LogDTO();
                dto.ID = item.ID;
                dto.UserName = item.UserName;
                dto.TableID = item.TableID;
                dto.TableName = item.TableName;
                dto.ProcessName = item.ProcessName;
                dto.ProcessDate = item.ProcessDate;
                dto.IpAddress = item.ipAddress;
                dtolist.Add(dto);
            }

            }
            
            return dtolist;
        }
    }
}
