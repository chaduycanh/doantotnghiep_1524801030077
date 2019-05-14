using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;
using Xceed.Words.NET;
using Shared.Utility;
namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class ExtratimeActiveTemplateController : AdminController
    {
        ExtratimeActiveTemplateServices _extaSrv = new ExtratimeActiveTemplateServices();
        // GET: ExtratimeActiveTemplate
        public ActionResult Index()
        {
            ViewBag.ListData = _extaSrv.GetAllData();
            return View();
        }
        public ActionResult GetAll()
        {
            return Json(new { jsData = _extaSrv.GetAllData() });
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        public ActionResult Save(string KindActive, string Date, string Hour, string Content, string Location, string Participant)
        {
            try
            {
                ExtratimeActive data = new ExtratimeActive();
                data.Date = Convert.ToDateTime(Date);
                data.Hour = Hour;
                data.Content = Content;
                data.Location = Location;
                data.Participant = Participant;
                data.KindActive = Convert.ToInt64(KindActive);
                //string a = data["Date"].ToString();
                _extaSrv.Save(data);
                return Json(new { returnData = true, jsData = _extaSrv.GetAllData() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult Edit(string Id)
        {
            var data = _extaSrv.GetDataById(Convert.ToInt64(Id));
            return Json(new { returnData = data });
        }
        public ActionResult Delete(string Id)
        {
            try
            {
                var data = _extaSrv.Delete(Convert.ToInt64(Id));
                return Json(new { returnData = true, jsData = _extaSrv.GetAllData() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult Update(string Date, string Hour,
                                   string Content, string Location,
                                   string Participant, string Id,
                                   string KindActive)
        {
            try
            {
                ExtratimeActive data = new ExtratimeActive();
                data.PID = Convert.ToInt64(Id);
                data.Date = Convert.ToDateTime(Date);
                data.Hour = Hour;
                data.Content = Content;
                data.Location = Location;
                data.Participant = Participant;
                //string a = data["Date"].ToString();
                _extaSrv.Update(data);

                return Json(new { returnData = true, jsData = _extaSrv.GetAllData() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        //public class UserMail
        //{

        //}
        public ActionResult SendMail(DateTime fromDate, DateTime toDate, string week)
        {
            try
            {
                string fileName = "Tuan" + week + "_" + fromDate.Year.ToString();
                string path = Server.MapPath("~/Template/" + fileName + ".docx");
                var modalActive = _extaSrv.GetDataExtraActiveToSendMail(fromDate, toDate);
                GenerateDocument(modalActive, fromDate, toDate,week);
                List< UserMail> temp = new List<UserMail>();
                foreach(var item in modalActive)
                {
                    var x = _extaSrv.GetDataJoinerToSendMail(item.PID);
                    foreach(var q in x )
                    {
                        UserMail v = new UserMail();
                        v.email = _extaSrv.GetEmail(q.TeacherCode);
                        v.name = _extaSrv.GetName(q.TeacherCode);
                        if (temp!=null)
                        {
                            int kt = 0;
                            foreach(var w in temp)
                            {
                                if(w.email==v.email)
                                {
                                    kt = 1;
                                    break;
                                }
                            }
                            if(kt==0)
                            {
                                temp.Add(v);
                            }
                        }
                        else
                        {
                            temp.Add(v);
                        }
                    }
                }
                var kq = _extaSrv.SendMail(week, fromDate.Year.ToString(), path, temp);
                return Json(new { returnData = kq });
            }
            catch (Exception ex)
            {

                return Json(new { returnData = false });
            }

        }

        public void GenerateDocument(List<ExtratimeActive> table, DateTime fromDate, DateTime toDate,string week)
        {


            DocX document = null;
            string fileName = "Tuan" + week +"_"+ fromDate.Year.ToString();
            document = DocX.Create(Server.MapPath("~/Template/"+ fileName + ".docx"), DocumentTypes.Document);

            //Image img = document.AddImage(Server.MapPath("~/Images/mvc.png"));

            //Picture pic = img.CreatePicture(100, 100);


            //Paragraph picturepara = document.InsertParagraph();
            //picturepara.Alignment = Alignment.center;
            // picturepara.Append("                                ");
            //picturepara.AppendPicture(pic).Alignment = Alignment.center;

            var headLineFormat = new Formatting();
            headLineFormat.FontFamily = new Xceed.Words.NET.Font("Times New Roman");
            headLineFormat.Size = 14;
            headLineFormat.Position = 12;

            string headlineText = "LỊCH LÀM VIỆC CỦA KHOA KỸ THUẬT- CÔNG NGHỆ";
            string smallheadlineText = "Tuần thứ "+week+" (Từ ngày "+FunctionUtility.FotmatDateTime(fromDate) +" đến ngày "+ FunctionUtility.FotmatDateTime(toDate)+" )";
            document.InsertParagraph(headlineText, false, headLineFormat).Alignment = Alignment.center;
            var smallheadLineFormat = new Formatting();
            //headLineFormat.FontFamily = new System.Drawing.FontFamily("Arial Black");
            smallheadLineFormat.Size = 12;
            smallheadLineFormat.FontFamily = new Xceed.Words.NET.Font("Times New Roman");
            document.InsertParagraph(smallheadlineText, false, headLineFormat).Alignment = Alignment.center;
            int indexTemp = 0;
            int kTemp = 1;
            for (DateTime date = fromDate; date < toDate; date = date.AddDays(1.0))
            {

                kTemp = kTemp + 1;
                int ktTemp = 0;
                foreach (var item in table)
                {

                    if (item.Date == date)
                    {
                        kTemp = kTemp + 1;
                        ktTemp = 1;
                    }
                }
                if (ktTemp == 0)
                {
                    kTemp = kTemp + 1;
                    if (indexTemp == 6)
                    {
                        break;
                    }
                }

            }

            Table tbl = document.AddTable(kTemp, 4);
            tbl.Alignment = Alignment.center;

            //tbl.Design = TableDesign.LightGridAccent2;

            tbl.Rows[0].Cells[0].Paragraphs.First().Append("Thời gian");
            tbl.Rows[0].Cells[1].Paragraphs.First().Append("Nội dung");
            tbl.Rows[0].Cells[2].Paragraphs.First().Append("Thành phần ");
            tbl.Rows[0].Cells[3].Paragraphs.First().Append("Địa điểm");

            //for (int i = 1; i <= 4; i++)
            //{
            //    for (int j = 0; j <= 3; j++)
            //    {
            //        tbl.Rows[i].Cells[j].Paragraphs.First().Append("(" + i + "," + j + ")");
            //    }
            //}
            int index = 0;
            int k = 1;

            for (DateTime date = fromDate; date < toDate; date = date.AddDays(1.0))
            {
                index = index + 1;
                tbl.Rows[k].Cells[0].Paragraphs.First().Append(date.ToShortDateString());
                tbl.Rows[k].MergeCells(0, 3);
                k = k + 1;
                int kt = 0;
                foreach (var item in table)
                {
                
                    if (item.Date == date)
                    {
                        string joiner = string.Empty;
                        tbl.Rows[k].Cells[0].Paragraphs.First().Append(item.Hour);
                        tbl.Rows[k].Cells[1].Paragraphs.First().Append(item.Content);
                        foreach(var tempItem in _extaSrv.GetDataJoinerToSendMail(item.PID))
                        {
                            joiner = joiner+ "_"+_extaSrv.GetName(tempItem.TeacherCode)+" "; 
                        }
                        tbl.Rows[k].Cells[2].Paragraphs.First().Append(joiner);
                        tbl.Rows[k].Cells[3].Paragraphs.First().Append(item.Location);
                        k = k + 1;
                        kt = 1;
                    }                   
                }
                if (kt == 0)
                {
                    tbl.Rows[k].Cells[0].Paragraphs.First().Append("");
                    tbl.Rows[k].Cells[1].Paragraphs.First().Append("");
                    tbl.Rows[k].Cells[2].Paragraphs.First().Append("");
                    tbl.Rows[k].Cells[3].Paragraphs.First().Append("");
                    k = k + 1;
                    //if(index==6)
                    //{
                    //    break;
                    //}
                }

            }


            //tbl.Rows[1].MergeCells(0, 3);
            document.InsertTable(tbl);

            // For  Farsi, Arabic and Urdu.
            // document.SetDirection(Direction.RightToLeft); 

            document.Save();


            MemoryStream ms = new MemoryStream();
            document.SaveAs(ms);
            // document.Save(ms, SaveFormat.Docx);
            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            ms.Dispose();
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=report.docx");
            //Response.AddHeader("Content-Length", byteArray.Length.ToString());
            //Response.ContentType = "application/msword";
            //Response.BinaryWrite(byteArray);
            //Response.End();
        }
        #region 
        //private void CreateDocument()
        //{
        //    try
        //    {
        //        //Create an instance for word app  
        //        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

        //        //Set animation status for word application  
        //        winword.ShowAnimation = false;

        //        //Set status for word application is to be visible or not.  
        //        winword.Visible = false;

        //        //Create a missing variable for missing value  
        //        object missing = System.Reflection.Missing.Value;

        //        //Create a new document  
        //        Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

        //        //Add header into the document  
        //        //foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
        //        //{
        //        //    //Get the header range and add the header details.  
        //        //    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //        //    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
        //        //    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //        //    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
        //        //    headerRange.Font.Size = 10;
        //        //    headerRange.Text = "Header text goes here";
        //        //}

        //        //Add the footers into the document  
        //        //foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
        //        //{
        //        //    //Get the footer range and add the footer details.  
        //        //    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //        //    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
        //        //    footerRange.Font.Size = 10;
        //        //    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //        //    footerRange.Text = "Footer text goes here";
        //        //}

        //        //adding text to document  
        //        document.Content.SetRange(0, 0);
        //        document.Content.Text = "LỊCH LÀM VIỆC CỦA KHOA KỸ THUẬT- CÔNG NGHỆ " + Environment.NewLine
        //            + "Tuần thứ 4 từ ngày 27/01/2019 tới ngày 04/02/2019";
        //        //Add paragraph with Heading 1 style  
        //        Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
        //        object styleHeading1 = "Heading 1";
        //        para1.Range.set_Style(ref styleHeading1);
        //        para1.Range.Text = "Para 1 text";
        //        para1.Range.InsertParagraphAfter();

        //        //Add paragraph with Heading 2 style  
        //        Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
        //        object styleHeading2 = "Heading 2";
        //        para2.Range.set_Style(ref styleHeading2);
        //        para2.Range.Text = "Para 2 text";
        //        para2.Range.InsertParagraphAfter();

        //        //Create a 5X5 table and insert some dummy record  
        //        Table firstTable = document.Tables.Add(para1.Range, 5, 5, ref missing, ref missing);

        //        firstTable.Borders.Enable = 1;
        //        foreach (Row row in firstTable.Rows)
        //        {
        //            foreach (Cell cell in row.Cells)
        //            {
        //                //Header row  
        //                if (cell.RowIndex == 1)
        //                {
        //                    cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
        //                    cell.Range.Font.Bold = 1;
        //                    //other format properties goes here  
        //                    cell.Range.Font.Name = "verdana";
        //                    cell.Range.Font.Size = 10;
        //                    //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                              
        //                    cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
        //                    //Center alignment for the Header cells  
        //                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
        //                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

        //                }
        //                //Data row  
        //                else
        //                {
        //                    cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
        //                }
        //            }
        //        }

        //        //Save the document  
        //        var path = Path.Combine(Server.MapPath("~/Template/"), "temp1.docx").ToString();
        //        object filename = path;// @"c:\temp1.docx";
        //        document.SaveAs2(ref filename);
        //        document.Close(ref missing, ref missing, ref missing);
        //        document = null;
        //        winword.Quit(ref missing, ref missing, ref missing);
        //        winword = null;
        //        //MessageBox.Show("Document created successfully !");
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //    }
        //}
        #endregion
    }
}