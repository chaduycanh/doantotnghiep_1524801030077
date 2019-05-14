using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class StatisticaServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();

        public dynamic Check(long Id,int status)
        {
            try
            {
               
                var data = entity.PersonalExtratimeActives.Where(p => p.Pid == Id).FirstOrDefault();
                data.Status = status;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<PersonalExtratimeActive> GetAllData()
        {
            try
            {
                var list = entity.PersonalExtratimeActives.Where(p=>p.Status !=1 && p.Status != -1).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PersonalExtratimeActive GetDataById(long id)
        {
            try
            {
                var list = entity.PersonalExtratimeActives.Where(p=>p.Pid==id).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable GetDataTableThongkecanhan(string code,int year)
        {
            string sql = "select b.Content as N'Nội dung',c.[Value] as N'Định mức theo quy định (Giờ thực tế)',1 as N'Số lượng',1.29 as N'Tổng', "
             + " b.[Date] as 'Ghi chú'  from Attendance a "
            + "join ExtratimeActive b on a.ActiveCode = b.PID "
            + "join WorkingDefine c on b.KindActive = c.Pid "
            + "where a.TeacherCode = '"+ code + "' and a.[Status]=1 and b.[Date]= "+year
            + "union"
            + "(select d.Content as N'Nội dung', e.[Value] as N'Định mức theo quy định (Giờ thực tế)',1 as N'Số lượng',1.29 as N'Tổng', "
                       + "  d.[Date] as 'Ghi chú' from PersonalExtratimeActive d "
            + "join WorkingDefine e on d.KindActive = e.Pid "
             + "where d.[Status]= 0 and d.TeacherCode= '"+ code+ "' and year(d.[Date])="+ year+")";
            var connection = (System.Data.SqlClient.SqlConnection)entity.Database.Connection;

            if (connection != null && connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var dt = new DataTable();

            using (var com = new System.Data.SqlClient.SqlDataAdapter(sql, connection))
            {
                com.Fill(dt);
            }
            connection.Close();
            return dt;
        }
        public UserLogin GetDataUser(string code)
        {
            try
            {
                var data = entity.UserLogins.Where(p => p.Code == code).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetRoleName(string code)
        {
            try
            {
                var data = entity.Roles.Where(p => p.Code == code).FirstOrDefault();
                return data.Name;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<DataChartActive> GetDataChartDay()
        {
            try
            {
                List<DataChartActive> temp = new List<DataChartActive>();

                var data = (from a in entity.Attendances
                            join b in entity.ExtratimeActives
                            on a.ActiveCode equals b.PID
                            join c in entity.UserLogins
                            on a.TeacherCode equals c.Code
                            join d in entity.Roles
                            on c.Role equals d.Code
                            select new
                            {
                                pid = a.Pid,
                                fullName = c.FullName,
                                role = d.Name,
                                date = b.Date,
                                msgv = c.Code,
                                status = a.Status
                            }).ToList();
      
                foreach (var tempData in data)
                {
                    DataChartActive q = new DataChartActive();
                    q.ActiveCode = Convert.ToInt32(tempData.pid);
                    q.DateActive = Convert.ToDateTime(tempData.date);
                    q.TeacherCode = tempData.msgv.ToString();
                    q.TeacherName = tempData.fullName.ToString();
                    q.Status = Convert.ToInt32(tempData.status);
                    temp.Add(q);
                }
        
                //var aa = data[0].pid;
                //int n = data.Count();
                //for (int i = 0; i < n; i++)
                //{
                //    DataChartActive q = new DataChartActive();
                //    q.ActiveCode = Convert.ToInt32(data[i].pid);

                //    //temp.Add(tempData);
                //}
                return temp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
