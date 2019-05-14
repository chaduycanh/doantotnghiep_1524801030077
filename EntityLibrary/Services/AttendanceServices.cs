using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class AttendanceServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        public dynamic Save(PersonalExtratimeActive data)
        {
            try
            {
                entity.PersonalExtratimeActives.Add(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic Delete(long Id)
        {
            try
            {

                var data = entity.PersonalExtratimeActives.Where(p => p.Pid == Id).FirstOrDefault();
                if (data.Status != 0)
                {
                    return false;
                }
                entity.PersonalExtratimeActives.Remove(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic GetFile(long Id)
        {
            try
            {

                var data = entity.PersonalExtratimeActives.Where(p => p.Pid == Id).FirstOrDefault();
                return data.Proof;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public dynamic DiemDanh(Attendance data)
        {
            try
            {
                var model = entity.Attendances.Where(p => p.Pid == data.Pid).FirstOrDefault();
                if (model.Lock == 1)
                {
                    return false;
                }
                model.Status = data.Status;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<dynamic> GetAllData()
        {
            try
            {
                List<dynamic> temp = new List<dynamic>();
                var data = (from a in entity.Attendances
                            join b in entity.ExtratimeActives
                            on a.ActiveCode equals b.PID
                            join c in entity.UserLogins
                            on a.TeacherCode equals c.Code
                            select new
                            {
                                pid = a.Pid,
                                techercode = a.TeacherCode,
                                teacherName = c.FullName,
                                day = b.Date,
                                hour = b.Hour,
                                status = a.Status,
                                lockAtt = a.Lock,
                                activeCode = a.ActiveCode
                            }).ToList();
                foreach (var tempData in data)
                {
                    temp.Add(tempData);
                }
               // var list = entity.PersonalExtratimeActives.ToList();
                return temp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<dynamic> SearchActiveToAttById(long id)
        {
            try
            {
                List<dynamic> temp = new List<dynamic>();
                var data = (from a in entity.Attendances
                            join b in entity.ExtratimeActives
                            on a.ActiveCode equals b.PID
                            join c in entity.UserLogins
                            on a.TeacherCode equals c.Code
                            where a.ActiveCode == id
                            select new
                            {
                                pid = a.Pid,
                                techercode = a.TeacherCode,
                                teacherName = c.FullName,
                                day = b.Date,
                                hour = b.Hour,
                                status = a.Status,
                                lockAtt = a.Lock,
                                activeCode = a.ActiveCode
                            }).ToList();
                foreach (var tempData in data)
                {
                    temp.Add(tempData);
                }
                return temp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ExtratimeActive> SearchActiveToAttByDate(DateTime date)
        {
            try
            {
                //var model = entity.ExtratimeActives.ToList();
                var model = entity.ExtratimeActives.Where(p => EntityFunctions.TruncateTime(p.Date) == EntityFunctions.TruncateTime(date)).ToList();
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public dynamic UpdateFile(PersonalExtratimeActive data)
        {
            try
            {
                var model = entity.PersonalExtratimeActives.Where(p => p.Pid == data.Pid).FirstOrDefault();
                if (model.Status != 0)
                {
                    return false;
                }
                model.Proof = model.Proof + data.Proof;
                //entity.PersonalExtratimeActives.Add(data);
                //entity.Entry(data).State= System.Data.Entity.EntityState.Modified;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic DeleteFile(PersonalExtratimeActive data)
        {
            try
            {
                var model = entity.PersonalExtratimeActives.Where(p => p.Pid == data.Pid).FirstOrDefault();
                if (model.Status != 0)
                {
                    return false;
                }
                string url = string.Empty;

                var listProof = model.Proof.Remove(model.Proof.Length - 1).Split(';');
                foreach (var item in listProof)
                {
                    if (item != data.Proof)
                    {
                        url = url + item + ";";
                    }
                }
                model.Proof = url;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public DataTable GetDataTableToPrint(long code)
        {
            string sql = "select a.TeacherCode as N'Mã giảng viên',b.FullName as N'Họ và tên',case when a.[Status] = 1 then N'Có' when a.[Status] = 0 then N'Vắng' END As 'Điểm danh' "
                        + "from Attendance a  join UserLogin b on a.TeacherCode = b.Code join ExtratimeActive c on a.ActiveCode = c.PID where ActiveCode="+code;
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
            connection.Close() ;
            return dt;
        }
        public dynamic GetDataActice(long code)
        {
            return entity.ExtratimeActives.Where(p => p.PID == code).FirstOrDefault();
        }
    }
}
