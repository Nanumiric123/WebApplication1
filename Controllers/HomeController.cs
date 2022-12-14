using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.WH_PART_INFO_local_ws_second;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private SqlConnection cnn;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GENERATESMTRACKLABEL()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult KANBAN_CALCULATION()
        {
            return View();
        }

        public ActionResult SLIDER_INFORMATION()
        {
            return View();
        }

        public ActionResult KANBAN_COLOR_FLOW()
        {
            return View();
        }

        public ActionResult KANBAN_CARD_INFORMATION()
        {
            return View();
        }

        public ActionResult BIN_ID_REGISTRATION()
        {
            return View();
        }

        public ActionResult RECEIVING()
        {
            return View();
        }
        //
        public ActionResult RECEIVING_LABELLING()
        {
            return View();
        }
        public ActionResult PCB_LABELLING()
        {
            return View();
        }
        public ActionResult STOCK_OUT()
        {
            return View();
        }
        public ActionResult STOCK_IN()
        {
            return View();
        }
        public ActionResult KITTING()
        {
            return View();
        }
        public ActionResult SUPERMARKET()
        {
            return View();
        }
        public ActionResult DROP_IN()
        {
            return View();
        }
        public ActionResult PIECHART()
        {
            return View();
        }

        public ActionResult GENERATE_PICKINGLIST_WAREHOUSE()
        {
            
            return View();
        }
        private WebApplication1Context db = new WebApplication1Context();
        public ActionResult PULLLISTANDSCANNED()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebApplication1Context2"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            string sqlquery = "SELECT [Item],SUM([Quantity]) AS [TOTAL_SCAN_QUANTITY] FROM [IBusiness].[dbo].[SY_TransferOrderLog] WHERE [Tcode] = 'T07' " +
                "AND [LogOn] BETWEEN @dateFrom AND @dateTo GROUP BY [Item],[Tcode]";
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, cnn);
            DateTime now = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            DateTime lastDayOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            SqlParameter paradateTo = new SqlParameter();
            paradateTo.ParameterName = "@dateTo";
            paradateTo.Value = lastDayOfMonth.ToString("yyyy-MM-dd") + " 00:00:00";
            paradateTo.SqlDbType = SqlDbType.DateTime;
            SqlParameter paradateFrom = new SqlParameter();

            paradateFrom.ParameterName = "@dateFrom";
            paradateFrom.Value = firstDayOfMonth.ToString("yyyy-MM-dd") + " 00:00:00";
            paradateFrom.SqlDbType = SqlDbType.DateTime;

            da.SelectCommand.Parameters.Add(paradateFrom);
            da.SelectCommand.Parameters.Add(paradateTo);

            DataTable dtScanned = new DataTable();

            try
            {
                cnn.Open();
                da.Fill(dtScanned);
            }
            catch
            {

            }
            finally
            {
                cnn.Close();
            }

            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("PART_NO", typeof(string));
            dtResult.Columns.Add("PULLLISTQUANTITY", typeof(int));
            dtResult.Columns.Add("SCAN_QUANTITY", typeof(int));
            dtResult.Columns.Add("DIFF", typeof(int));
            dtResult.Columns.Add("Reason", typeof(string));

            DataTable dtpullistqty = new DataTable();
            dtpullistqty.Columns.Add("PART_NO", typeof(string));
            dtpullistqty.Columns.Add("QUANTITY", typeof(int));

            var startdate = DateTime.Now;
            var enddate = DateTime.Now.AddDays(-1);
            var monthlyPullList = db.SMT_PULLLIST
                .Where(x => x.DATE_TIME >= firstDayOfMonth && x.DATE_TIME <= lastDayOfMonth)
                .Where(x => x.PRINTED == "1")
                .GroupBy(k => k.PART_NUMBER).Select(grp => new
                {
                    PART_NUMBER = grp.Key,
                    TOTAL_QUANTITY = grp.Sum(i => i.SHORTAGE_QTY),
                    TOTAL_REEL_QTY = grp.Sum(i => i.REF_NUM_REEL)
                }).ToList();

            foreach (var items in monthlyPullList)
            {
                DataRow[] scannedrows = dtScanned.Select("Item = '" + items.PART_NUMBER + "'");
                if (scannedrows.Length != 0)
                {
                    DataRow nr = dtResult.NewRow();
                    nr["PART_NO"] = items.PART_NUMBER;
                    nr["PULLLISTQUANTITY"] = items.TOTAL_REEL_QTY * getMaterialQuantityPerReel(items.PART_NUMBER);
                    nr["SCAN_QUANTITY"] = (int)scannedrows[0].ItemArray[1];
                    int diff = (items.TOTAL_REEL_QTY * getMaterialQuantityPerReel(items.PART_NUMBER)) - (int)scannedrows[0].ItemArray[1];
                    nr["DIFF"] = diff;
                    if (diff > 0)
                    {
                        nr["Reason"] = "Pull list quantity > scanned quantity";
                    }
                    else if (diff == 0)
                    {
                        nr["Reason"] = "Pull list quantity = scanned quantity";
                    }
                    else
                    {
                        nr["Reason"] = "Pull list quantity < scanned quantity";
                    }
                    dtResult.Rows.Add(nr);
                }
                else
                {
                    DataRow nr = dtResult.NewRow();
                    nr["PART_NO"] = items.PART_NUMBER;
                    nr["PULLLISTQUANTITY"] = items.TOTAL_REEL_QTY * getMaterialQuantityPerReel(items.PART_NUMBER);
                    nr["SCAN_QUANTITY"] = 0;
                    int diff = (items.TOTAL_REEL_QTY * getMaterialQuantityPerReel(items.PART_NUMBER)) - 0;
                    nr["DIFF"] = diff;
                    if (diff > 0)
                    {
                        nr["Reason"] = "Pull list quantity > scanned quantity";
                    }
                    else if (diff == 0)
                    {
                        nr["Reason"] = "Pull list quantity = scanned quantity";
                    }
                    else
                    {
                        nr["Reason"] = "Pull list quantity < scanned quantity";
                    }
                    dtResult.Rows.Add(nr);
                }

            }



            var listResult = dtResult.Select().OrderByDescending(x => x.Field<int>("DIFF")).ToList();


            ViewBag.statisticsData = listResult;

            return View();
        }
        public int getMaterialQuantityPerReel(string mat)
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(@"Data Source=172.16.206.20;Initial Catalog=IBusinessTest;User ID=Data_writer;Password=Pasmy@2791381230");
            }
            SqlCommand cmd = new SqlCommand("SELECT [MATERIAL],[REEL] FROM [IBusinessTest].[dbo].[SMT2006TRIGERRING] WHERE [MATERIAL] = @material", cnn);
            SqlParameter matPara = new SqlParameter();
            matPara.ParameterName = "@material";
            matPara.Value = mat;
            matPara.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(matPara);
            int resultQtyPerReel = 0;
            try
            {
                cnn.Open();
                using (cmd)
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string resultMaterial = reader.GetString(0);
                            Int32 resultQty = reader.GetInt32(1);
                            resultQtyPerReel = resultQty;
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                cnn.Close();
            }
            return resultQtyPerReel;
        }

        public ActionResult PullListIndex(string SearchPartNum,string SearchBadgeNum,string SearchDateTime)
        {
            if (!String.IsNullOrEmpty(SearchPartNum))
            {
                var smtpullist = db.SMT_PULLLIST.Where(x => x.PART_NUMBER.Contains(SearchPartNum)).OrderByDescending(x => x.DATE_TIME).Take(20).ToList();
                return View(smtpullist);

            }
            else if (!String.IsNullOrEmpty(SearchBadgeNum))
            {
                var smtpullist = db.SMT_PULLLIST.Where(x => x.BADGE.Contains(SearchBadgeNum)).OrderByDescending(x => x.DATE_TIME).Take(20).ToList();
                return View(smtpullist);
            }
            else if (!String.IsNullOrEmpty(SearchDateTime))
            {
                DateTime dateTime;
                if (DateTime.TryParseExact(SearchDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {

                }
                else
                {
                    dateTime = DateTime.Now;
                }
                var smtpullist = db.SMT_PULLLIST.Where(x => x.DATE_TIME < dateTime).OrderByDescending(x => x.DATE_TIME).Take(20).ToList();
                return View(smtpullist);
            }
            else
            {
                var smtpullist = db.SMT_PULLLIST.OrderByDescending(x => x.DATE_TIME).Take(20).ToList();
                return View(smtpullist);
            }

            
        }

        public FileStreamResult GenerateSMTPullList()
        {
            var smtpullist = from d in db.SMT_PULLLIST
                             where d.PRINTED == "0"
                             select d;

            var pulllistlist = smtpullist.ToList();

            DataTable PullDat = new DataTable();

            foreach (var items in pulllistlist)
            {

            }




            GeneratePullListPDF plpdf = new GeneratePullListPDF();
            PdfDocument document = new PdfDocument();
            if (PullDat.Rows.Count > 0)
            {
                var tempList = PullDat.AsEnumerable().OrderBy(row => row["REF_LOC"]).CopyToDataTable();
                document = plpdf.generatePullList(tempList);
            }
            else
            {
                document = plpdf.generatePullList(PullDat);
            }






            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);

            return File(stream, "application/pdf");
        }

        public FileStreamResult GENERATE_PICKINGLIST_PDF_WAREHOUSE(string Text1, string Text2, string Text3, string Text4, string Text5,
            string Text6, string Text7, string Text8, string Text9, string Text10)
        {
            string PO1 = Text1;
            string PO2 = Text2;
            string PO3 = Text3;
            string PO4 = Text4;
            string PO5 = Text5;
            string PO6 = Text6;
            string PO7 = Text7;
            string PO8 = Text8;
            string PO9 = Text9;
            string P10 = Text10;

            string[] text = { PO1, PO2, PO3, PO4, PO5, PO6, PO7, PO8, PO9, P10 };

            PdfDocument document = new PdfDocument();



            if (text != null)
            {


                CreateKanbanPDF ckpdf = new CreateKanbanPDF();
                document = ckpdf.createWHpickingList(text);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);

                return File(stream, "application/pdf");
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);

                return File(stream, "application/pdf");
            }

        }

        public ActionResult Login(int? id)
        {
            ViewBag.denied = null;
            if (id == 1)
            {
                TempData["ID"] = "KANBAN";
            }
            else if (id == 2)
            {
                TempData["ID"] = "SUPERMARKET_SLIDER";
            }
            else
            {
                TempData["ID"] = "COLOR";
            }

            return View();
        }

        public ActionResult LoginError(int? id)
        {
            return View();
        }

        private IbsuinessContext ibctc = new IbsuinessContext();
        public ActionResult Check_authority_Login(string Text1, string Password1, int? id)
        {

            string username = Text1;
            string pass = Password1;
            string completeRoute = TempData["ID"].ToString();
            string role = null;

            var log_ingo = ibctc.LOGIN_INFO.Where(c => c.User == username && c.Password == pass).Select(x => x.Group).ToList();



            foreach (var i in log_ingo)
            {
                if (i.Contains("Administrator") || i.Contains("Kanban Master Group"))
                {
                    role = i;
                }
                else
                {
                    role = "others";
                }
            }

            if (role == "Administrator" || role == "Kanban Master Group")
            {
                if (completeRoute == "KANBAN")
                {
                    return RedirectToAction("Index", "KANBAN_MASTER");
                }
                else if (completeRoute == "SUPERMARKET_SLIDER")
                {
                    return RedirectToAction("sliderAddress", "KANBAN_MASTER");
                }
                else
                {
                    return RedirectToAction("Index", "SUPERMARKET_SLIDER");
                }

            }
            else if (role == "others")
            {
                if (completeRoute == "KANBAN")
                {
                    return RedirectToAction("Index", "KANBAN_MASTER", new { role = "supervisor" });
                }
                else if (completeRoute == "SUPERMARKET_SLIDER")
                {
                    return RedirectToAction("sliderAddress", "KANBAN_MASTER", new { role = "supervisor" });
                }
                else
                {
                    return RedirectToAction("Index", "SUPERMARKET_SLIDER", new { role = "supervisor" });
                }
            }
            else
            {
                return RedirectToAction("LoginError", "Home");
            }

        }

        [HttpGet]
        public FileStreamResult smtlabelpdf2(string PN1, string PN2, string PN3, string PN4, string PN5, string PN6, string PN7,
                       string RACK1, string RACK2, string RACK3, string RACK4, string RACK5, string RACK6, string RACK7)
        {
            List<RACK_MATERIAL> RL = new List<RACK_MATERIAL>();
            if (PN1.Length > 0 && RACK1.Length > 0 || PN1.Length > 0 || RACK1.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN1;
                rm.RACK = RACK1;
                RL.Add(rm);
            }
            if (PN2.Length > 0 && RACK2.Length > 0 || PN2.Length > 0 || RACK2.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN2;
                rm.RACK = RACK2;
                RL.Add(rm);
            }
            if (PN3.Length > 0 && RACK3.Length > 0 || PN3.Length > 0 || RACK3.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN3;
                rm.RACK = RACK3;
                RL.Add(rm);
            }
            if (PN4.Length > 0 && RACK4.Length > 0 || PN4.Length > 0 || RACK4.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN4;
                rm.RACK = RACK4;
                RL.Add(rm);
            }
            if (PN5.Length > 0 && RACK5.Length > 0 || PN5.Length > 0 || RACK5.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN5;
                rm.RACK = RACK5;
                RL.Add(rm);
            }
            if (PN6.Length > 0 && RACK6.Length > 0 || PN6.Length > 0 || RACK6.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN6;
                rm.RACK = RACK6;
                RL.Add(rm);
            }
            if (PN7.Length > 0 && RACK7.Length > 0 || PN7.Length > 0 || RACK7.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN7;
                rm.RACK = RACK7;
                RL.Add(rm);
            }


            CreateKanbanPDF CKPDF = new CreateKanbanPDF();
            PdfDocument document = CKPDF.create_smt_bin_label(RL);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, "application/pdf");
        }

        [HttpGet]
        public FileStreamResult smtlabelpdf(string PN1, string PN2, string PN3, string PN4, string PN5, string PN6, string PN7)
        {

            Local_Ws_secondSoapClient client = new Local_Ws_secondSoapClient();

            List<RACK_MATERIAL> RL = new List<RACK_MATERIAL>();
            if (PN1.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN1;
                rm.RACK = client.GETMaterialLocMFRPN(PN1);
                RL.Add(rm);
            }
            if (PN2.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN2;
                rm.RACK = client.GETMaterialLocMFRPN(PN2);
                RL.Add(rm);
            }
            if (PN3.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN3;
                rm.RACK = client.GETMaterialLocMFRPN(PN3);
                RL.Add(rm);
            }
            if (PN4.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN4;
                rm.RACK = client.GETMaterialLocMFRPN(PN4);
                RL.Add(rm);
            }
            if (PN5.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN5;
                rm.RACK = client.GETMaterialLocMFRPN(PN5);
                RL.Add(rm);
            }
            if (PN6.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN6;
                rm.RACK = client.GETMaterialLocMFRPN(PN6);
                RL.Add(rm);
            }
            if (PN7.Length > 0)
            {
                RACK_MATERIAL rm = new RACK_MATERIAL();
                rm.MATERIAL = PN7;
                rm.RACK = client.GETMaterialLocMFRPN(PN7);
                RL.Add(rm);
            }


            CreateKanbanPDF CKPDF = new CreateKanbanPDF();
            PdfDocument document = CKPDF.create_smt_bin_label(RL);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, "application/pdf");
        }

        public ActionResult GenerateSMTStoreRackLabel()
        {
            return View();
        }

    }
}