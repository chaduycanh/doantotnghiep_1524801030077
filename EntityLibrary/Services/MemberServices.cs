using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class MemberServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        #region cate
        public bool SaveCate(Role data)
        {
            try
            {
                entity.Roles.Add(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool DeleteCate(long Id)
        {
            try
            {
                var data = entity.Roles.Where(p => p.Pid == Id).FirstOrDefault();
                entity.Roles.Remove(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic UpdateCate(Role data)
        {
            try
            {
                var modal = entity.Roles.Where(p => p.Pid == data.Pid).FirstOrDefault();
                modal.Name = data.Name;
                //entity.Roles.Add(data);
                //entity.Entry(data).State= System.Data.Entity.EntityState.Modified;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<Role> GetAllCate()
        {
            try
            {
                var list = entity.Roles.ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Role GetDataById(long id)
        {
            try
            {
                var list = entity.Roles.Where(p=>p.Pid==id).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region detail
        public bool SaveDetail(UserLogin data)
        {
            try
            {
                entity.UserLogins.Add(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<dynamic> GetAllDetail()
        {
            try
            {
                List<dynamic> temp = new List<dynamic>();

                var data = (from p in entity.UserLogins
                            join e in entity.Roles
                            on p.Role equals e.Code
                            where p.Role != "Admin"
                            select new
                            {
                                pid = p.PID,
                                msgv = p.Code,
                                dateofbirth = p.DayOfBirth,
                                gioitinh = p.Sex==1?"Nam":"Nữ",
                                fullName = p.FullName,
                                role = e.Name,
                            }).ToList();
                foreach (var tempData in data)
                {
                    temp.Add(tempData);
                }
                //var list = entity.WorkingDefines.ToList();
                return temp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteDetail(long Id)
        {
            try
            {
                var data = entity.UserLogins.Where(p => p.PID == Id).FirstOrDefault();
                entity.UserLogins.Remove(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic UpdateDetail(UserLogin data)
        {
            try
            {
                entity.UserLogins.Add(data);
                entity.Entry(data).State = System.Data.Entity.EntityState.Modified;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public dynamic GetDataDetailById(long id)
        {
            try
            {
                var list = entity.UserLogins.Where(p => p.PID == id).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
