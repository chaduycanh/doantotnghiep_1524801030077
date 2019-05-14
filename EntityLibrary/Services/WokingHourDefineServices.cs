using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class WokingHourDefineServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        #region cate
        public bool SaveCate(WorkingDefineCate data)
        {
            try
            {
                entity.WorkingDefineCates.Add(data);
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
                var data = entity.WorkingDefineCates.Where(p => p.Pid == Id).FirstOrDefault();
                entity.WorkingDefineCates.Remove(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic UpdateCate(WorkingDefineCate data)
        {
            try
            {
                entity.WorkingDefineCates.Add(data);
                entity.Entry(data).State= System.Data.Entity.EntityState.Modified;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<WorkingDefineCate> GetAllCate()
        {
            try
            {
                var list = entity.WorkingDefineCates.ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public WorkingDefineCate GetDataById(long id)
        {
            try
            {
                var list = entity.WorkingDefineCates.Where(p=>p.Pid==id).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region detail
        public bool SaveDetail(WorkingDefine data)
        {
            try
            {
                entity.WorkingDefines.Add(data);
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

                var data = (from p in entity.WorkingDefines
                            join e in entity.WorkingDefineCates
                            on p.CatePid equals e.Pid
                            select new
                            {
                                pid = p.Pid,
                                content = p.Content,
                                value = p.Value,
                                cate = e.NameCate
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
                var data = entity.WorkingDefines.Where(p => p.Pid == Id).FirstOrDefault();
                entity.WorkingDefines.Remove(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic UpdateDetail(WorkingDefine data)
        {
            try
            {
                entity.WorkingDefines.Add(data);
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
                var list = entity.WorkingDefines.Where(p => p.Pid == id).FirstOrDefault();
                return list;
                //dynamic temp = new dynamic();

                //var data = (from p in entity.WorkingDefines
                //            join e in entity.WorkingDefineCates
                //            on p.CatePid equals e.Pid
                //            where p.Pid==id
                //            select new
                //            {
                //                pid = p.Pid,
                //                content = p.Content,
                //                value = p.Value,
                //                cate = e.NameCate
                //            }).FirstOrDefault();
                ////foreach (var tempData in data)
                ////{
                ////    temp.Add(tempData);
                ////}
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
