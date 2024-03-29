﻿using Microsoft.Ajax.Utilities;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.THIRD_SERVICE;

namespace WebApplication1.Controllers
{
    //[NoDirectAccess]
    public class KANBAN_MASTERController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();
        private sliderContext sliderdb = new sliderContext();
        private capacityContext capacitydb = new capacityContext();
        private IbsuinessContext ibctc = new IbsuinessContext();


        // GET: KANBAN_MASTER
        public ActionResult Index(string SearchPart, string SearchModel, string SearchBin, string SearchPartName, string SearchLine, string SearchbinType, string SearchLoc, string SearchSliderAddress,
            string SearchRemarks, string SearchSupplier, string SearchProcess, string SearchKittingSlider, string role)
        {

            ViewBag.role = role;


            var kanbanMaster = from s in db.KANBAN_MASTER.Select(p => new KANBAN_MASTER_index
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                PART_NAME = p.PART_NAME,
                MODEL = p.MODEL,
                LINE = p.LINE,
                OUTPUT = p.OUTPUT,
                USAGE = p.USAGE,
                PROCESS = p.PROCESS,
                QTY_PER_TRAY = p.QTY_PER_TRAY,
                TARY_PER_BIN = p.TARY_PER_BIN,
                QTY_PER_BIN = p.QTY_PER_BIN,
                BIN_TYPE = p.BIN_TYPE,
                LOCATION = p.LOCATION,
                SLIDER_ADDRESS = p.SLIDER_ADDRESS,
                KITTING_SLIDER = p.KITTING_SLIDER,
                STORE_KANBAN_QTY = p.STORE_KANBAN_QTY,
                PROD_KANBAN_QTY = p.PROD_KANBAN_QTY,
                BASIC_FINISH_DATE = p.BASIC_FINISH_DATE,
                REVISION = p.REVISION,
                BIN_NUMBER_END = p.BIN_NUMBER_END,
                CYCLE_TIME = p.CYCLE_TIME,
                REMARKS = p.REMARKS,
                SUPPLIER = p.SUPPLIER
            })
                               select s;

            if (!String.IsNullOrEmpty(SearchKittingSlider))
            {
                kanbanMaster = kanbanMaster.Where(s => s.KITTING_SLIDER.Contains(SearchKittingSlider));

            }

            if (!String.IsNullOrEmpty(SearchProcess))
            {
                kanbanMaster = kanbanMaster.Where(s => s.PROCESS.Contains(SearchProcess));

            }

            if (!String.IsNullOrEmpty(SearchSupplier))
            {
                kanbanMaster = kanbanMaster.Where(s => s.SUPPLIER.Contains(SearchSupplier));

            }
            if (!String.IsNullOrEmpty(SearchRemarks))
            {
                kanbanMaster = kanbanMaster.Where(s => s.REMARKS.Contains(SearchRemarks));

            }
            if (!String.IsNullOrEmpty(SearchSliderAddress))
            {
                kanbanMaster = kanbanMaster.Where(s => s.SLIDER_ADDRESS.Contains(SearchSliderAddress));

            }
            if (!String.IsNullOrEmpty(SearchLoc))
            {
                kanbanMaster = kanbanMaster.Where(s => s.LOCATION.Contains(SearchLoc));

            }
            if (!String.IsNullOrEmpty(SearchbinType))
            {
                kanbanMaster = kanbanMaster.Where(s => s.BIN_TYPE.Contains(SearchbinType));

            }
            if (!String.IsNullOrEmpty(SearchPart))
            {
                kanbanMaster = kanbanMaster.Where(s => s.PART_NO.Contains(SearchPart));

            }
            if (!String.IsNullOrEmpty(SearchModel))
            {
                kanbanMaster = kanbanMaster.Where(s => s.MODEL.Contains(SearchModel));

            }

            if (!String.IsNullOrEmpty(SearchBin))
            {
                kanbanMaster = kanbanMaster.Where(s => s.BIN_NUMBER_END.Contains(SearchBin));

            }

            if (!String.IsNullOrEmpty(SearchPartName))
            {
                kanbanMaster = kanbanMaster.Where(s => s.PART_NAME.Contains(SearchPartName));

            }

            if (!String.IsNullOrEmpty(SearchLine))
            {
                kanbanMaster = kanbanMaster.Where(s => s.LINE.Contains(SearchLine));

            }

            return View(kanbanMaster.OrderBy(s => s.ID).ToList());
        }

        // GET: KANBAN_MASTER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            var pACKAGING_INFO = from s in db.PACKAGING_INFO.Select(p => new
            {
                ID = p.ID,
                PHOTO_A = p.PHOTO_A,
                PHOTO_B = p.PHOTO_B,
                PHOTO_C = p.PHOTO_C,
                PART_NUMBER = p.PART_NUMBER,
                EXT_PCK_METHOD = p.EXT_PCK_METHOD,
                INT_PACK_METHOD = p.INT_PACK_METHOD,
                EXT_PCK_HEIGHT = p.EXT_PCK_HEIGHT,
                EXT_PCK_WIDTH = p.EXT_PCK_WIDTH,
                EXT_PCK_LENGTH = p.EXT_PCK_LENGTH,
                INT_PCK_HEIGHT = p.INT_PCK_HEIGHT,
                INT_PCK_WIDTH = p.INT_PCK_WIDTH,
                INT_PCK_LENGTH = p.INT_PCK_LENGTH,
                INT_PCK_QTY = p.INT_PCK_QTY,
                TOTAL_NUMBER_OF_INT = p.TOTAL_NUMBER_OF_INT,
                PCK_TOTAL_QTY = p.PCK_TOTAL_QTY,
                WEIGHT_KG = p.WEIGHT_KG,
                REMARKS = p.REMARKS,
                SAVE = p.SAVE
            })
                                  select s;

            var pkginfo = pACKAGING_INFO.Where(i => i.PART_NUMBER.Equals(kANBAN_MASTER.PART_NO)).ToList();
            PACKAGING_INFO pkginfosingle = new PACKAGING_INFO();

            foreach (var i in pkginfo)
            {
                pkginfosingle.ID = i.ID;
                pkginfosingle.PHOTO_A = i.PHOTO_A;
                pkginfosingle.PHOTO_B = i.PHOTO_B;
                pkginfosingle.PHOTO_C = i.PHOTO_C;
                pkginfosingle.PART_NUMBER = i.PART_NUMBER;
                pkginfosingle.EXT_PCK_METHOD = i.EXT_PCK_METHOD;
                pkginfosingle.INT_PACK_METHOD = i.INT_PACK_METHOD;
                pkginfosingle.EXT_PCK_HEIGHT = i.EXT_PCK_HEIGHT;
                pkginfosingle.EXT_PCK_WIDTH = i.EXT_PCK_WIDTH;
                pkginfosingle.EXT_PCK_LENGTH = i.EXT_PCK_LENGTH;
                pkginfosingle.INT_PCK_HEIGHT = i.INT_PCK_HEIGHT;
                pkginfosingle.INT_PCK_WIDTH = i.INT_PCK_WIDTH;
                pkginfosingle.INT_PCK_LENGTH = i.INT_PCK_LENGTH;
                pkginfosingle.INT_PCK_QTY = i.INT_PCK_QTY;
                pkginfosingle.TOTAL_NUMBER_OF_INT = i.TOTAL_NUMBER_OF_INT;
                pkginfosingle.PCK_TOTAL_QTY = i.PCK_TOTAL_QTY;
                pkginfosingle.WEIGHT_KG = i.WEIGHT_KG;
                pkginfosingle.REMARKS = i.REMARKS;
                pkginfosingle.SAVE = i.SAVE;
            }
            ViewBag.PKGINFO = pkginfosingle;
            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }


        public ActionResult Chart()
        {
            //set room 1 data
            var r1_rack_in_clause = new string[] { "RACK 1A", "Rack 1A", "RACK 1B", "Rack 1B", "RACK 1C", "Rack 1C", "RACK 1D", "Rack 1D", "RACK 1E", "Rack 1E", "RACK 1F", "Rack 1F",
            "RACK 1G","Rack 1G","RACK 1H","Rack 1H","RACK 1J","Rack 1J","RACK 1K","Rack 1K","RACK 1L","Rack 1L","RACK 1M","Rack 1M"};
            var r1_status_in_clause = new string[] { "EOL", "MOVING", "SLOW MOVING", "EMPTY", "NON MOVING" };

            var room1 = (from c in sliderdb.SUPERMARKET_SLIDER
                         where r1_rack_in_clause.Contains(c.RACK)
                         group c by new { c.RACK, c.STATUS } into grouping
                         select new SUPERMARKET_SLIDER_REPORT
                         {
                             STATUS = grouping.Key.STATUS,
                             RACK = grouping.Key.RACK,
                             COUNT = grouping.Count()
                         }).ToList();



            Report_data rooms_results = new Report_data();
            int[] room1_res = rooms_results.room_results(room1);

            ViewBag.Room1 = room1_res[0].ToString();
            ViewBag.Room1Use = room1_res[1].ToString();
            ViewBag.Room1Empty = room1_res[2].ToString();

            //set room 2 data
            var r2_rack_in_clause = new string[] { "RACK 2A", "RACK 2B", "RACK 2C", "RACK 2D", "RACK 2E", "RACK 2F", "RACK 2G", "RACK 2H", "RACK 2J", "RACK 2K", "RACK 2M", "RACK 2N", "RACK 2P", "RACK 2Q",
                                                   "Rack 2A", "Rack 2B", "Rack 2C", "Rack 2D", "Rack 2E", "Rack 2F", "Rack 2G", "Rack 2H", "Rack 2J", "Rack 2K", "Rack 2M", "Rack 2N", "Rack 2P", "Rack 2Q"};
            var r2_status_in_clause = new string[] { "EOL", "MOVING", "SLOW MOVING", "EMPTY", "NON MOVING" };

            var room2 = (from c in sliderdb.SUPERMARKET_SLIDER
                         where r2_rack_in_clause.Contains(c.RACK)
                         group c by new { c.RACK, c.STATUS } into grouping
                         select new SUPERMARKET_SLIDER_REPORT
                         {
                             STATUS = grouping.Key.STATUS,
                             RACK = grouping.Key.RACK,
                             COUNT = grouping.Count()
                         }).ToList();

            int[] room2_res = rooms_results.room_results(room2);
            ViewBag.Room2 = room2_res[0].ToString();
            ViewBag.Room2Use = room2_res[1].ToString();
            ViewBag.Room2Empty = room2_res[2].ToString();

            //set A0 Room data
            var AO_rack_in_clause = new string[] { "RACK SA", "RACK SB", "RACK SC", "RACK SD" };
            var A0_room = (from c in sliderdb.SUPERMARKET_SLIDER
                           where AO_rack_in_clause.Contains(c.RACK)
                           group c by new { c.RACK, c.STATUS } into grouping
                           select new SUPERMARKET_SLIDER_REPORT
                           {
                               STATUS = grouping.Key.STATUS,
                               RACK = grouping.Key.RACK,
                               COUNT = grouping.Count()
                           }).ToList();

            int[] A0_room_res = rooms_results.room_results(A0_room);
            ViewBag.A0Room = A0_room_res[0].ToString();
            ViewBag.A0RoomUse = A0_room_res[1].ToString();
            ViewBag.A0RoomEmpty = A0_room_res[2].ToString();

            //set total data
            var total = (from c in sliderdb.SUPERMARKET_SLIDER
                         group c by new { c.RACK, c.STATUS } into grouping
                         select new SUPERMARKET_SLIDER_REPORT
                         {
                             STATUS = grouping.Key.STATUS,
                             RACK = grouping.Key.RACK,
                             COUNT = grouping.Count()
                         }).ToList();

            int[] total_res = rooms_results.room_results(total);
            ViewBag.total_res = total_res[0].ToString();
            ViewBag.totalUse = total_res[1].ToString();
            ViewBag.totalEmpty = total_res[2].ToString();

            //Calculate Bin type room 1& 2 B4P /BE rack 1A/1B/1D
            var rack_supermarket = new string[] { "Rack 1A", "Rack 1B", "Rack 1C", "Rack 1D", "Rack 1E", "Rack 1F", "Rack 1G", "Rack 1H",
                "Rack 1J", "Rack 1K", "Rack 1L", "Rack 1M", "Rack 2A", "Rack 2B", "Rack 2C", "Rack 2D", "Rack 2E", "Rack 2F", "Rack 2G", "Rack 2H",
                "Rack 2J", "Rack 2K", "Rack 2M", "Rack 2N", "Rack 2P","Rack 2Q" };

            var bintyp_r1_Bintype_in_clause = new string[] { "BE", "B1P" };

            var bintype_room_1_2_part_staus = (from c in sliderdb.SUPERMARKET_SLIDER
                                               where bintyp_r1_Bintype_in_clause.Contains(c.BIN) &&
                                               rack_supermarket.Contains(c.RACK)
                                               group c by new { c.RACK, c.BIN, c.STATUS } into grouping
                                               select new SUPERMARKET_SLIDER_REPORT_BIN_type
                                               {
                                                   BIN = grouping.Key.BIN,
                                                   RACK = grouping.Key.RACK,
                                                   STATUS = grouping.Key.STATUS
                                               }).ToList();

            List<SUPERMARKET_SLIDER_REPORT_BIN> bin_type_rack_status_rack_1A_1B_2D = rooms_results.get_rack_from_bin(bintype_room_1_2_part_staus);
            string[] list_ofracks_from_bins = rooms_results.get_racks(bin_type_rack_status_rack_1A_1B_2D);
            ViewBag.racksof_BE_DB = list_ofracks_from_bins[0];

            var bintyp_r1_rack_in_clause = list_ofracks_from_bins[1].Split(',');

            var bintype_BE_B1P = (from c in sliderdb.SUPERMARKET_SLIDER
                                  where bintyp_r1_Bintype_in_clause.Contains(c.BIN) &&
                                  rack_supermarket.Contains(c.RACK)
                                  group c by new { c.STATUS } into grouping
                                  select new SUPERMARKET_SLIDER_REPORT_CALCULATION
                                  {
                                      STATUS = grouping.Key.STATUS,
                                      COUNT = grouping.Count()
                                  }).ToList();

            var use_and_empty_bin_type_BE_B1P = rooms_results.use_and_empty_calculation(bintype_BE_B1P);


            ViewBag.use_rack_1A_1B_2D = use_and_empty_bin_type_BE_B1P[0].ToString();
            ViewBag.empty_rack_1A_1B_2D = use_and_empty_bin_type_BE_B1P[1].ToString();

            //Calculate Bin type room 1& 2 DB rack 1C/1D/1E/2D/2N/2P and bin type BD

            var bintyp_r2_Bintype_in_clause = new string[] { "BD", "BE/BD", "BD/BE" };

            var bintype_r2_part_staus = (from c in sliderdb.SUPERMARKET_SLIDER
                                         where bintyp_r2_Bintype_in_clause.Contains(c.BIN) &&
                                         rack_supermarket.Contains(c.RACK)
                                         group c by new { c.RACK, c.BIN, c.STATUS } into grouping
                                         select new SUPERMARKET_SLIDER_REPORT_BIN_type
                                         {
                                             BIN = grouping.Key.BIN,
                                             RACK = grouping.Key.RACK,
                                             STATUS = grouping.Key.STATUS
                                         }).ToList();
            List<SUPERMARKET_SLIDER_REPORT_BIN> bin_type_BD = rooms_results.get_rack_from_bin(bintype_r2_part_staus);
            string[] list_ofracks_from_bins_BD_BE = rooms_results.get_racks(bin_type_BD);
            ViewBag.racksof_BD = list_ofracks_from_bins_BD_BE[0];


            var bintype_BD_BD_BE = (from c in sliderdb.SUPERMARKET_SLIDER
                                    where bintyp_r2_Bintype_in_clause.Contains(c.BIN) &&
                                      rack_supermarket.Contains(c.RACK)
                                    group c by new { c.STATUS } into grouping
                                    select new SUPERMARKET_SLIDER_REPORT_CALCULATION
                                    {
                                        STATUS = grouping.Key.STATUS,
                                        COUNT = grouping.Count()
                                    }).ToList();

            var use_and_empty_bin_type_BD_BD_BE = rooms_results.use_and_empty_calculation(bintype_BD_BD_BE);


            ViewBag.use_rack_1C_1D_1E_2D_2N_2P = use_and_empty_bin_type_BD_BD_BE[0].ToString();
            ViewBag.empty_rack_1C_1D_1E_2D_2N_2P = use_and_empty_bin_type_BD_BD_BE[1].ToString();

            //Calculate Bin type room 1& 2 DB rack 2A/2B/2C bin type BC / B10P
            var bintyp_r3_Bintype_in_clause = new string[] { "BC", "BC/B10P", "B10P" };

            var bintype_room_1_3_part_staus = (from c in sliderdb.SUPERMARKET_SLIDER
                                               where bintyp_r3_Bintype_in_clause.Contains(c.BIN) &&
                                               rack_supermarket.Contains(c.RACK)
                                               group c by new { c.RACK, c.BIN, c.STATUS } into grouping
                                               select new SUPERMARKET_SLIDER_REPORT_BIN_type
                                               {
                                                   BIN = grouping.Key.BIN,
                                                   RACK = grouping.Key.RACK,
                                                   STATUS = grouping.Key.STATUS
                                               }).ToList();
            List<SUPERMARKET_SLIDER_REPORT_BIN> bin_type_BC_B10P = rooms_results.get_rack_from_bin(bintype_room_1_3_part_staus);

            string[] list_ofracks_BC_B10P = rooms_results.get_racks(bin_type_BC_B10P);
            ViewBag.racksof_BC_B10P = list_ofracks_BC_B10P[0];

            var bintype_BC_B10P = (from c in sliderdb.SUPERMARKET_SLIDER
                                   where bintyp_r3_Bintype_in_clause.Contains(c.BIN) &&
                                     rack_supermarket.Contains(c.RACK)
                                   group c by new { c.STATUS } into grouping
                                   select new SUPERMARKET_SLIDER_REPORT_CALCULATION
                                   {
                                       STATUS = grouping.Key.STATUS,
                                       COUNT = grouping.Count()
                                   }).ToList();

            var use_and_empty_bin_type_BC_B10P = rooms_results.use_and_empty_calculation(bintype_BC_B10P);
            ViewBag.use_rack_2A_2B_2C = use_and_empty_bin_type_BC_B10P[0].ToString();
            ViewBag.empty_rack_2A_2B_2C = use_and_empty_bin_type_BC_B10P[1].ToString();

            //Calculate Bin type room 1& 2 DB rack 2E/2F/2G/2H/2M


            var bintyp_c1_r1_Bintype_in_clause = new string[] { "B1P/B2P/B3P" };
            var bintype_room_C1_1_part_staus = (from c in sliderdb.SUPERMARKET_SLIDER
                                                where bintyp_c1_r1_Bintype_in_clause.Contains(c.BIN) &&
                                                rack_supermarket.Contains(c.RACK)
                                                group c by new { c.RACK, c.BIN, c.STATUS } into grouping
                                                select new SUPERMARKET_SLIDER_REPORT_BIN_type
                                                {
                                                    BIN = grouping.Key.BIN,
                                                    RACK = grouping.Key.RACK,
                                                    STATUS = grouping.Key.STATUS
                                                }).ToList();

            List<SUPERMARKET_SLIDER_REPORT_BIN> bin_type_B1P_B2P_B3P = rooms_results.get_rack_from_bin(bintype_room_C1_1_part_staus);
            string[] list_ofracks_B1P_B2P_B3P = rooms_results.get_racks(bin_type_B1P_B2P_B3P);
            ViewBag.racksof_B1P_B2P_B3P = list_ofracks_B1P_B2P_B3P[0];

            var bintype_B1P_B2P_B3P = (from c in sliderdb.SUPERMARKET_SLIDER
                                       where bintyp_c1_r1_Bintype_in_clause.Contains(c.BIN) &&
                                         rack_supermarket.Contains(c.RACK)
                                       group c by new { c.STATUS } into grouping
                                       select new SUPERMARKET_SLIDER_REPORT_CALCULATION
                                       {
                                           STATUS = grouping.Key.STATUS,
                                           COUNT = grouping.Count()
                                       }).ToList();

            var use_and_empty_bin_type_B1P_B2P_B3P = rooms_results.use_and_empty_calculation(bintype_B1P_B2P_B3P);

            ViewBag.use_rack_2E_2F_2G_2H_2M = use_and_empty_bin_type_B1P_B2P_B3P[0].ToString();
            ViewBag.empty_rack_2E_2F_2G_2H_2M = use_and_empty_bin_type_B1P_B2P_B3P[1].ToString();

            //Calculate Bin type room 1& 2 DB rack 1F/1G/1H/1J/1K/1L/1M/2J/2K/2Q
            var bintyp_c1_r2_Bintype_in_clause = new string[] { "BA/BB", "BA/BB/B6P/B7P/B9P", "BS/B9P", "BA/BB/BS" };
            var bintype_room_C1_2_part_staus = (from c in sliderdb.SUPERMARKET_SLIDER
                                                where bintyp_c1_r2_Bintype_in_clause.Contains(c.BIN) &&
                                                rack_supermarket.Contains(c.RACK)
                                                group c by new { c.RACK, c.BIN, c.STATUS } into grouping
                                                select new SUPERMARKET_SLIDER_REPORT_BIN_type
                                                {
                                                    BIN = grouping.Key.BIN,
                                                    RACK = grouping.Key.RACK,
                                                    STATUS = grouping.Key.STATUS
                                                }).ToList();
            List<SUPERMARKET_SLIDER_REPORT_BIN> bin_type_BA_BB_B6P = rooms_results.get_rack_from_bin(bintype_room_C1_2_part_staus);
            string[] list_ofracks_BA_BB_B6P = rooms_results.get_racks(bin_type_BA_BB_B6P);
            ViewBag.racksof_BA_BB_B6P = list_ofracks_BA_BB_B6P[0];

            var bintype_BA_BB_BA_BB_B6P_B7P_B9P = (from c in sliderdb.SUPERMARKET_SLIDER
                                                   where bintyp_c1_r2_Bintype_in_clause.Contains(c.BIN) &&
                                                     rack_supermarket.Contains(c.RACK)
                                                   group c by new { c.STATUS } into grouping
                                                   select new SUPERMARKET_SLIDER_REPORT_CALCULATION
                                                   {
                                                       STATUS = grouping.Key.STATUS,
                                                       COUNT = grouping.Count()
                                                   }).ToList();

            var use_and_empty_bin_type_BA_BB_BA_BB_B6P_B7P_B9P = rooms_results.use_and_empty_calculation(bintype_BA_BB_BA_BB_B6P_B7P_B9P);

            ViewBag.use_rack_1F_1G_1H_1J_1K_1L_1M_2J_2K_2Q = use_and_empty_bin_type_BA_BB_BA_BB_B6P_B7P_B9P[0].ToString();
            ViewBag.empty_rack_1F_1G_1H_1J_1K_1L_1M_2J_2K_2Q = use_and_empty_bin_type_BA_BB_BA_BB_B6P_B7P_B9P[1].ToString();

            //calculate Bin type SA / SD A0 ROOM
            var A0_room_racks = new string[] { "RACK SA", "RACK SB", "Rack SA", "Rack SB", "RACK SC", "RACK SC", "Rack SD", "Rack SD" };
            var bintyp_c2_r1_Bintype_in_clause = new string[] { "BC" };

            var bintype_room_A0_Room_r1 = (from c in sliderdb.SUPERMARKET_SLIDER
                                           where bintyp_c2_r1_Bintype_in_clause.Contains(c.BIN) &&
                                           A0_room_racks.Contains(c.RACK)
                                           group c by new { c.RACK, c.BIN, c.STATUS } into grouping
                                           select new SUPERMARKET_SLIDER_REPORT_BIN_type
                                           {
                                               BIN = grouping.Key.BIN,
                                               RACK = grouping.Key.RACK,
                                               STATUS = grouping.Key.STATUS
                                           }).ToList();

            List<SUPERMARKET_SLIDER_REPORT_BIN> bin_type_A0_Room_r1 = rooms_results.get_rack_from_bin(bintype_room_A0_Room_r1);
            string[] list_ofracks_a0room_1 = rooms_results.get_racks(bin_type_A0_Room_r1);
            ViewBag.racksof_A0_ROOM_1 = list_ofracks_a0room_1[0];

            var bintype_BC = (from c in sliderdb.SUPERMARKET_SLIDER
                              where bintyp_c2_r1_Bintype_in_clause.Contains(c.BIN) &&
                                A0_room_racks.Contains(c.RACK)
                              group c by new { c.STATUS } into grouping
                              select new SUPERMARKET_SLIDER_REPORT_CALCULATION
                              {
                                  STATUS = grouping.Key.STATUS,
                                  COUNT = grouping.Count()
                              }).ToList();

            var use_and_empty_bin_type_BC = rooms_results.use_and_empty_calculation(bintype_BC);
            ViewBag.use_rack_SA_SB = use_and_empty_bin_type_BC[0].ToString();
            ViewBag.empty_rack_SA_SB = use_and_empty_bin_type_BC[1].ToString();

            //calculate Bin type room small part SC / SD
            var bintyp_c2_r2_Bintype_in_clause = new string[] { "BD" };
            var bintype_room_A0_Room_r2 = (from c in sliderdb.SUPERMARKET_SLIDER
                                           where bintyp_c2_r2_Bintype_in_clause.Contains(c.BIN) &&
                                           A0_room_racks.Contains(c.RACK)
                                           group c by new { c.RACK, c.BIN, c.STATUS } into grouping
                                           select new SUPERMARKET_SLIDER_REPORT_BIN_type
                                           {
                                               BIN = grouping.Key.BIN,
                                               RACK = grouping.Key.RACK,
                                               STATUS = grouping.Key.STATUS
                                           }).ToList();
            List<SUPERMARKET_SLIDER_REPORT_BIN> bin_type_A0_Room_r2 = rooms_results.get_rack_from_bin(bintype_room_A0_Room_r2);
            string[] list_ofracks_a0room_2 = rooms_results.get_racks(bin_type_A0_Room_r2);
            ViewBag.racksof_A0_ROOM_2 = list_ofracks_a0room_2[0];

            var bintype_BD = (from c in sliderdb.SUPERMARKET_SLIDER
                              where bintyp_c2_r2_Bintype_in_clause.Contains(c.BIN) &&
                                A0_room_racks.Contains(c.RACK)
                              group c by new { c.STATUS } into grouping
                              select new SUPERMARKET_SLIDER_REPORT_CALCULATION
                              {
                                  STATUS = grouping.Key.STATUS,
                                  COUNT = grouping.Count()
                              }).ToList();

            var use_and_empty_bin_type_BD = rooms_results.use_and_empty_calculation(bintype_BD);

            ViewBag.use_rack_SC_SD = use_and_empty_bin_type_BD[0].ToString();
            ViewBag.empty_rack_SC_SD = use_and_empty_bin_type_BD[1].ToString();


            var bin_count = (from c in sliderdb.SUPERMARKET_SLIDER
                             where rack_supermarket.Contains(c.RACK)
                             group c by new { c.BIN } into grouping
                             select new BIN_TYPE
                             {
                                 BIN = grouping.Key.BIN,
                                 COUNT = grouping.Count()
                             }).ToList();

            ViewBag.bc = bin_count;

            var process_class = (from c in db.KANBAN_MASTER
                                 group c by new { c.PROCESS } into grouping
                                 select new KB_CARD_STATS
                                 {
                                     PROCESS = grouping.Key.PROCESS,
                                     COUNT = grouping.Count()
                                 }).ToList();

            var processclass_Clause = new string[] { "A0 H0", "SUB", "A0", "H0" };
            ViewBag.A0PROCESSCLASS = rooms_results.get_A0_Process(process_class);
            ViewBag.H0PROCESSCLASS = rooms_results.get_H0_Process(process_class);
            ViewBag.A0H0PROCESSCLASS = rooms_results.get_A0H0_Process(process_class);
            ViewBag.SUBPROCESSCLASS = rooms_results.get_SUB_Process(process_class);
            var totalallprocessclass = process_class.Where(f => processclass_Clause.Contains(f.PROCESS)).Select(f => f.COUNT).DefaultIfEmpty().Sum();
            ViewBag.totalallprocessclass = totalallprocessclass;



            var racks_in_room_1 = (from c in sliderdb.SUPERMARKET_SLIDER
                                   where c.RACK.Contains("Rack 1")
                                   orderby c.RACK
                                   group c by new { c.RACK, c.STATUS } into grouping
                                   select new RCKS_LISTS
                                   {
                                       STATUS = grouping.Key.STATUS,
                                       RACK = grouping.Key.RACK,
                                       COUNT = grouping.Count()
                                   }).ToList();



            var list_of_racks = racks_in_room_1.Select(x => x.RACK).Distinct().ToArray();
            ViewBag.racksinroom1 = list_of_racks;
            ViewBag.racks_in_room_1 = racks_in_room_1;

            var totalinallracks = racks_in_room_1.GroupBy(x => x.RACK).Select(cl => new RCKS_TOT
            {
                RACK = cl.First().RACK,
                COUNT = cl.Sum(co => co.COUNT)
            }).ToList();

            ViewBag.totalallracks = totalinallracks;

            //calculate racks in room 2
            var rackin_room2_clause = new string[] { "RACK 2A", "RACK 2B",  "RACK 2C",  "RACK 2D",  "RACK 2E",  "RACK 2F",  "RACK 2G",  "RACK 2H",  "RACK 2I",  "RACK 2J",  "RACK 2K",  "RACK 2L",  "RACK 2M",  "RACK 2N",  "RACK 2O",  "RACK 2P",  "RACK 2Q",
                                                        "Rack 2A",  "Rack 2B",  "Rack 2C",  "Rack 2D",  "Rack 2E",  "Rack 2F",  "Rack 2G",  "Rack 2H",  "Rack 2I",  "Rack 2J",  "Rack 2K",  "Rack 2L",  "Rack 2M",  "Rack 2N",  "Rack 2O",  "Rack 2P",  "Rack 2Q" };
            var racks_in_room_2 = (from c in sliderdb.SUPERMARKET_SLIDER
                                   where rackin_room2_clause.Contains(c.RACK)
                                   orderby c.RACK
                                   group c by new { c.RACK, c.STATUS } into grouping
                                   select new RCKS_LISTS
                                   {
                                       STATUS = grouping.Key.STATUS,
                                       RACK = grouping.Key.RACK,
                                       COUNT = grouping.Count()
                                   }).ToList();

            var list_of_racks2 = racks_in_room_2.Select(x => x.RACK).Distinct().ToArray();
            ViewBag.racksinroom2 = list_of_racks2;
            ViewBag.racks_in_room_2 = racks_in_room_2;

            var totalinallracks2 = racks_in_room_2.GroupBy(x => x.RACK).Select(cl => new RCKS_TOT
            {
                RACK = cl.First().RACK,
                COUNT = cl.Sum(co => co.COUNT)
            }).ToList();

            ViewBag.totalallracks2 = totalinallracks2;

            var racks_bin = (from c in sliderdb.SUPERMARKET_SLIDER
                             orderby c.RACK
                             group c by new { c.RACK, c.BIN } into grouping
                             select new
                             {
                                 RACK = grouping.Key.RACK,
                                 BIN = grouping.Key.BIN
                             }).ToList();


            var racks_COLOR = (from c in sliderdb.SUPERMARKET_SLIDER
                               orderby c.RACK
                               group c by new { c.RACK, c.COLOR } into grouping
                               select new
                               {
                                   RACK = grouping.Key.RACK,
                                   COLOR = grouping.Key.COLOR
                               }).ToList();

            IList<BIN_RACK_COLOR> binrackcolor1 = new List<BIN_RACK_COLOR>();


            var allracks = racks_bin.OrderBy(s => s.RACK).Select(x => x.RACK).Distinct().ToArray();

            foreach (var i in allracks)
            {
                BIN_RACK_COLOR itm = new BIN_RACK_COLOR();
                string temp_bin = "";
                foreach (var k in racks_bin)
                {

                    if (i == k.RACK)
                    {
                        temp_bin = temp_bin + k.BIN + Environment.NewLine;

                    }

                }
                itm.BIN = temp_bin;
                itm.RACK = i;


                string temp_color = "";
                string result_color = "";
                foreach (var l in racks_COLOR)
                {
                    if (i == l.RACK)
                    {
                        temp_color = "";
                        switch (l.COLOR)
                        {
                            case ("bluekanbancolor"):
                                temp_color = "BLUE";
                                break;
                            case ("greykanbancolor"):
                                temp_color = "GREY";
                                break;
                            case ("purplekanbancolor"):
                                temp_color = "PINK";
                                break;
                            case ("greenkanbancolor"):
                                temp_color = "GREEN";
                                break;
                        }
                        if (!result_color.Contains(temp_color))
                        {
                            result_color = result_color + temp_color + Environment.NewLine;
                        }

                    }

                }
                itm.COLOR = result_color;
                result_color = "";
                binrackcolor1.Add(itm);

            }

            ViewBag.condensed_racks_bin_color = binrackcolor1.Where(x => x.RACK.Contains("Rack 2"));
            ViewBag.condensed_racks_bin_color2 = binrackcolor1.Where(x => x.RACK.Contains("Rack 1"));

            var racks_in_room_subwrk = (from c in sliderdb.SUPERMARKET_SLIDER
                                        where c.AREA == "Subwork"
                                        orderby c.RACK
                                        group c by new { c.RACK, c.STATUS } into grouping
                                        select new RCKS_LISTS
                                        {
                                            STATUS = grouping.Key.STATUS,
                                            RACK = grouping.Key.RACK,
                                            COUNT = grouping.Count()
                                        }).ToList();

            ViewBag.racksinsubworkcount = racks_in_room_subwrk;


            var R_IN_SUB = (from c in sliderdb.SUPERMARKET_SLIDER
                            orderby c.RACK
                            where c.AREA == "Subwork"
                            group c by new { c.RACK } into grouping
                            select new
                            {
                                RACK = grouping.Key.RACK
                            }).ToArray();

            ViewBag.racksinsubwork = R_IN_SUB.OrderBy(x => x.RACK).Select(x => x.RACK).Distinct().ToArray();

            var racksubworkcolor = (from c in sliderdb.SUPERMARKET_SLIDER
                                    orderby c.RACK
                                    where c.AREA == "Subwork"
                                    group c by new { c.RACK, c.BIN } into grouping
                                    select new SUBWORK_BIN_RACK
                                    {
                                        RACK = grouping.Key.RACK,
                                        BIN = grouping.Key.BIN
                                    }).ToArray();



            IList<SUBWORK_BIN_RACK> subworkbin = new List<SUBWORK_BIN_RACK>();

            foreach (var i in R_IN_SUB)
            {
                SUBWORK_BIN_RACK itm = new SUBWORK_BIN_RACK();

                string tmp_bin = "";
                foreach (var j in racksubworkcolor)
                {
                    if (i.RACK.ToString() == j.RACK.ToString())
                    {
                        tmp_bin = tmp_bin + j.BIN + Environment.NewLine;
                    }
                }
                itm.BIN = tmp_bin;
                itm.RACK = i.RACK;
                subworkbin.Add(itm);

            }

            ViewBag.condensed_racks_bin_subwaork = subworkbin;

            var totalinallrackswubwork = racks_in_room_subwrk.GroupBy(x => x.RACK).Select(cl => new RCKS_TOT
            {
                RACK = cl.First().RACK,
                COUNT = cl.Sum(co => co.COUNT)
            }).ToList();

            ViewBag.totalallsubworkrack = totalinallrackswubwork;

            var bintyp_A0_ROOM_rack_in_clause = new string[] { "RACK SA", "RACK SB", "RACK SC", "RACK SD", "Rack SA", "Rack SB", "Rack SC", "Rack SD" };

            var R_IN_A0 = (from c in sliderdb.SUPERMARKET_SLIDER
                           orderby c.RACK
                           where bintyp_A0_ROOM_rack_in_clause.Contains(c.RACK)
                           group c by new { c.RACK } into grouping
                           select new
                           {
                               RACK = grouping.Key.RACK
                           }).ToArray();

            ViewBag.racksinA0 = R_IN_A0.OrderBy(x => x.RACK).Select(x => x.RACK).Distinct().ToArray();

            var rackA0color = (from c in sliderdb.SUPERMARKET_SLIDER
                               orderby c.RACK
                               where bintyp_A0_ROOM_rack_in_clause.Contains(c.RACK)
                               group c by new { c.RACK, c.BIN } into grouping
                               select new SUBWORK_BIN_RACK
                               {
                                   RACK = grouping.Key.RACK,
                                   BIN = grouping.Key.BIN
                               }).ToArray();

            IList<SUBWORK_BIN_RACK> A0bin = new List<SUBWORK_BIN_RACK>();

            foreach (var i in R_IN_A0)
            {
                SUBWORK_BIN_RACK itm = new SUBWORK_BIN_RACK();

                string tmp_bin = "";
                foreach (var j in rackA0color)
                {
                    if (i.RACK.ToString() == j.RACK.ToString())
                    {
                        tmp_bin = tmp_bin + j.BIN + Environment.NewLine;
                    }
                }
                itm.BIN = tmp_bin;
                itm.RACK = i.RACK;
                A0bin.Add(itm);

            }

            ViewBag.condensed_racks_bin_A0 = A0bin;

            var racks_in_room_A0 = (from c in sliderdb.SUPERMARKET_SLIDER
                                    where bintyp_A0_ROOM_rack_in_clause.Contains(c.RACK)
                                    orderby c.RACK
                                    group c by new { c.RACK, c.STATUS } into grouping
                                    select new RCKS_LISTS
                                    {
                                        STATUS = grouping.Key.STATUS,
                                        RACK = grouping.Key.RACK,
                                        COUNT = grouping.Count()
                                    }).ToList();
            ViewBag.racksinA0count = racks_in_room_A0;

            var totalinallrackA0 = racks_in_room_A0.GroupBy(x => x.RACK).Select(cl => new RCKS_TOT
            {
                RACK = cl.First().RACK,
                COUNT = cl.Sum(co => co.COUNT)
            }).ToList();

            ViewBag.totalallA0rack = totalinallrackA0;


            return View();
        }

        public ActionResult PieChart()
        {
            var results = (from c in sliderdb.SUPERMARKET_SLIDER
                           group c by new { c.STATUS } into grouping
                           select new
                           {
                               STATUS = grouping.Key.STATUS,
                               COUNT = grouping.Count()
                           }).ToList();



            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SecondPieChart()
        {

            var newresults = (from kb in db.KANBAN_MASTER
                              where kb.SLIDER_ADDRESS != null
                              group kb by new { kb.MODEL } into grouping
                              select new
                              {
                                  CUSTOMER = grouping.Key.MODEL,
                                  COUNT = grouping.Count()
                              }).ToList();


            List<KANBAN_MASTER_MODEL_COUNT> KMMC_L = new List<KANBAN_MASTER_MODEL_COUNT>();

            foreach (var p in newresults)
            {
                KANBAN_MASTER_MODEL_COUNT kmmc = new KANBAN_MASTER_MODEL_COUNT();
                if (p.CUSTOMER.Contains('/'))
                {
                    kmmc.CUSTOMER = "COMMON";
                    kmmc.COUNT = p.COUNT;
                }
                else
                {
                    kmmc.CUSTOMER = p.CUSTOMER;
                    kmmc.COUNT = p.COUNT;
                }
                KMMC_L.Add(kmmc);
            }

            var result = KMMC_L.GroupBy(l => l.CUSTOMER).Select(cl => new KANBAN_MASTER_MODEL_COUNT
            {
                CUSTOMER = cl.First().CUSTOMER,
                COUNT = cl.Sum(c => c.COUNT)
            }).ToList();


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET : SliderAddress
        public ActionResult sliderAddress(string ddlRacks, string role)
        {


            ViewBag.role = role;
            if (role == null)
            {
                ViewBag.role = TempData["role"];
            }

            var RACKS_LISTS = from rk in sliderdb.SUPERMARKET_SLIDER select rk;
            var racks_only = RACKS_LISTS.Select(x => x.RACK).Distinct().ToList();

            ViewBag.rack_lists = racks_only.OrderBy(rk => rk).ToList();
            ViewBag.Rack_name = ddlRacks;

            if (!String.IsNullOrEmpty(ddlRacks))
            {
                IEnumerable<WebApplication1.Models.SUPERMARKET_SLIDER_JOINED> _sliderhoinesd = GeSlider_jpined().ToList();

                _sliderhoinesd = _sliderhoinesd.OrderBy(x => x.SLIDER_ADDRESS).Where(s => s.RACK == ddlRacks).ToList();
                return View(_sliderhoinesd);
            }
            else
            {
                IEnumerable<WebApplication1.Models.SUPERMARKET_SLIDER_JOINED> _sliderhoinesd = GeSlider_jpined().ToList();

                _sliderhoinesd = _sliderhoinesd.Where(s => s.RACK == "Rack 1A").ToList();
                return View(_sliderhoinesd);
            }


        }

        public IEnumerable<SUPERMARKET_SLIDER_JOINED> GeSlider_jpined()
        {
            var kanbanMaster = from s in db.KANBAN_MASTER.Select(p => new KANBAN_MASTER_index
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                PART_NAME = p.PART_NAME,
                MODEL = p.MODEL,
                LINE = p.LINE,
                OUTPUT = p.OUTPUT,
                USAGE = p.USAGE,
                PROCESS = p.PROCESS,
                QTY_PER_TRAY = p.QTY_PER_TRAY,
                TARY_PER_BIN = p.TARY_PER_BIN,
                QTY_PER_BIN = p.QTY_PER_BIN,
                BIN_TYPE = p.BIN_TYPE,
                LOCATION = p.LOCATION,
                SLIDER_ADDRESS = p.SLIDER_ADDRESS,
                KITTING_SLIDER = p.KITTING_SLIDER,
                STORE_KANBAN_QTY = p.STORE_KANBAN_QTY,
                PROD_KANBAN_QTY = p.PROD_KANBAN_QTY,
                BASIC_FINISH_DATE = p.BASIC_FINISH_DATE,
                REVISION = p.REVISION,
                BIN_NUMBER_END = p.BIN_NUMBER_END,
                CYCLE_TIME = p.CYCLE_TIME,
                REMARKS = p.REMARKS,
                SUPPLIER = p.SUPPLIER
            })
                               select s;

            var Kanbanmasterlist = kanbanMaster.ToList();
            var sliderAddresses = sliderdb.SUPERMARKET_SLIDER.ToList();

            List<SUPERMARKET_SLIDER_JOINED> new_join = new List<SUPERMARKET_SLIDER_JOINED>();


            foreach (var k in sliderAddresses)
            {
                int found_count = 0;

                foreach (var j in Kanbanmasterlist)
                {
                    if (k.SLIDER_ADDRESS == null || j.SLIDER_ADDRESS == null)
                    {

                    }
                    else if(j.PART_NO == k.PART_NUMBER) {
                        string[] kbSlider = k.SLIDER_ADDRESS.Split(' ');
                        foreach (var i in kbSlider)
                        {
                            if (k.PART_NUMBER == j.PART_NO && j.SLIDER_ADDRESS.Trim().Contains(i.Trim()))
                            {
                                SUPERMARKET_SLIDER_JOINED l = new SUPERMARKET_SLIDER_JOINED();
                                l.ID = k.ID;
                                l.PART_NUMBER = k.PART_NUMBER;
                                l.RACK = k.RACK;
                                l.SLIDER_ADDRESS = i;
                                l.STATUS = k.STATUS;
                                l.AREA = k.AREA;
                                l.BIN = k.BIN;
                                l.PROCESS = j.PROCESS;
                                l.COLOR = k.COLOR;
                                new_join.Add(l);
                                found_count++;

                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {


                    }

                }
                if (found_count == 0)
                {
                    SUPERMARKET_SLIDER_JOINED l = new SUPERMARKET_SLIDER_JOINED();
                    l.ID = k.ID;
                    l.PART_NUMBER = k.PART_NUMBER;
                    l.RACK = k.RACK;
                    l.SLIDER_ADDRESS = k.SLIDER_ADDRESS;
                    l.STATUS = k.STATUS;
                    l.AREA = k.AREA;
                    l.BIN = k.BIN;
                    l.PROCESS = string.Empty;
                    l.COLOR = k.COLOR;
                    new_join.Add(l);
                }

            }



            return new_join.OrderByDescending(x => x.SLIDER_ADDRESS);
        }

        // GET: KANBAN_MASTER/Create
        public ActionResult Create()
        {
            var model_list = (from e in capacitydb.SUPERMARKET_LINE_CAPACITY
                              group e by new { e.MODEL } into g
                              select new
                              {
                                  MODEL = g.Key.MODEL
                              }).ToList();
            List<SelectListItem> models = new List<SelectListItem>();
            foreach (var k in model_list)
            {
                models.Add(new SelectListItem { Text = k.MODEL.ToString(), Value = k.MODEL.ToString() });
            }
            models.Add(new SelectListItem { Text = "EMPTY", Value = "EMPTY" });
            ViewBag.model_lists = models;

            return View();
        }

        public byte[] Stream2Bytes(Stream stream, int chunkSize = 1024)
        {
            if (stream == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "stream");
            }

            if (chunkSize < 1)
            {
                throw new System.ArgumentException("Parameter must be greater than zero", "chunkSize");
            }

            if (chunkSize > 1024 * 64)
            {
                throw new System.ArgumentException(String.Format("Parameter must be less or equal {0}", 1024 * 64), "chunkSize");
            }

            List<byte> buffers = new List<byte>();

            using (BinaryReader br = new BinaryReader(stream))
            {
                byte[] chunk = br.ReadBytes(chunkSize);

                while (chunk.Length > 0)
                {
                    buffers.AddRange(chunk);
                    chunk = br.ReadBytes(chunkSize);
                }
            }
            return buffers.ToArray();

        }
        private int calculate_prod_kanban_qty(decimal line_output, decimal bin_qty, decimal per_usage)
        {
            int result = 0;

            if (bin_qty > 100 && line_output != 0)
            {
                result = 3;

            }
            else if (bin_qty > 100 && line_output == 0)
            {
                result = 0;
            }
            else
            {
                decimal output_perhr = (line_output / 10.5m) / bin_qty;
                decimal stock_60min = output_perhr * per_usage;

                if (line_output == 0)
                {
                    result = 0;
                }
                else
                {
                    result = int.Parse(Math.Floor(stock_60min).ToString()) + 2;
                }


            }

            return result;
        }

        private int calculate_store_kanban_qty(decimal line_output, decimal bin_qty, decimal per_usage)
        {
            int result = 0;

            decimal output_perhr = line_output / 10.5m;
            decimal output_perhr_half = (line_output / 10.5m) / 2.0m;

            decimal SUPERMARKET_STOCK_90_MIN = ((output_perhr + output_perhr_half) / bin_qty) * per_usage;
            result = (int)Math.Ceiling(SUPERMARKET_STOCK_90_MIN);

            if (line_output == 0)
            {
                result = 0;
            }
            else
            {
                result = result + 5;
            }


            return result;
        }
        // POST: KANBAN_MASTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,BIN_NUMBER_END,SUPPLIER,REMARKS,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                kANBAN_MASTER.PHOTO = data;
            }
            else
            {
                kANBAN_MASTER.PHOTO = null;
            }

            string[] lines_output = kANBAN_MASTER.LINE.ToString().Split();

            decimal total_prod_output = 0;

            foreach (var lin in lines_output)
            {
                SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_H0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.H0 == lin).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();
                SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_A0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.A0 == lin).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();
                if (LINE_CAPACITY_DB_H0 != null && LINE_CAPACITY_DB_A0 == null)
                {
                    total_prod_output = total_prod_output + LINE_CAPACITY_DB_H0.CAPACITY;

                }
                else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 == null)
                {
                    total_prod_output = total_prod_output + LINE_CAPACITY_DB_A0.CAPACITY;
                }
                else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 != null)
                {
                    total_prod_output = total_prod_output + LINE_CAPACITY_DB_A0.CAPACITY + LINE_CAPACITY_DB_H0.CAPACITY;
                }
                else
                {

                }
            }


            decimal line_output_decimal = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());
            decimal STORE_OUTPUT_DECIMAL = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());
            decimal bin_qty_decimal = decimal.Parse(kANBAN_MASTER.QTY_PER_BIN.ToString());
            decimal per_usage_decimal = decimal.Parse(kANBAN_MASTER.USAGE.ToString());


            kANBAN_MASTER.PROD_KANBAN_QTY = calculate_prod_kanban_qty(total_prod_output,
                bin_qty_decimal, per_usage_decimal);

            kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(total_prod_output,
                bin_qty_decimal, per_usage_decimal);

            string BIN_NUMBER = kANBAN_MASTER.BIN_NUMBER_END;

            SUPERMARKET_SLIDER slider_address_data = new SUPERMARKET_SLIDER();



            int bin_num = 0;
            try
            {
                string[] bin_split = BIN_NUMBER.Split('S');
                bin_num = int.Parse(bin_split[1]);
                bin_num = bin_num + kANBAN_MASTER.STORE_KANBAN_QTY;
                kANBAN_MASTER.BIN_NUMBER_END = "BS0" + bin_num;
            }
            catch { kANBAN_MASTER.BIN_NUMBER_END = ""; }


            if (ModelState.IsValid)
            {
                db.KANBAN_MASTER.Add(kANBAN_MASTER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kANBAN_MASTER);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderAddressEdit([Bind(Include = "ID,PHOTO,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,BIN_NUMBER_END,REMARKS,SUPPLIER,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                kANBAN_MASTER.PHOTO = data;
            }
            else
            {
                if (TempData["TempModel"] != null)
                {
                    byte[] photo_temp = (byte[])TempData["TempModel"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }
                else
                {
                    byte[] photo_temp = (byte[])Session["photo"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }

            }
            string[] lines_output = kANBAN_MASTER.LINE.ToString().Split();

            decimal total_prod_output = 0;

            foreach (var lin in lines_output)
            {
                SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_H0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.H0 == lin).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();
                SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_A0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.A0 == lin).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();
                if (LINE_CAPACITY_DB_H0 != null && LINE_CAPACITY_DB_A0 == null)
                {
                    total_prod_output = total_prod_output + LINE_CAPACITY_DB_H0.CAPACITY;

                }
                else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 == null)
                {
                    total_prod_output = total_prod_output + LINE_CAPACITY_DB_A0.CAPACITY;
                }
                else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 != null)
                {
                    total_prod_output = total_prod_output + LINE_CAPACITY_DB_A0.CAPACITY + LINE_CAPACITY_DB_H0.CAPACITY;
                }
                else
                {

                }
            }

            decimal line_output_decimal = lines_output.Length;
            decimal lineoutput_decimal_store = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());

            decimal bin_qty_decimal = decimal.Parse(kANBAN_MASTER.QTY_PER_BIN.ToString());
            decimal per_usage_decimal = decimal.Parse(kANBAN_MASTER.USAGE.ToString());

            kANBAN_MASTER.PROD_KANBAN_QTY = calculate_prod_kanban_qty(total_prod_output,
                bin_qty_decimal, per_usage_decimal);

            kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(total_prod_output,
                bin_qty_decimal, per_usage_decimal);

            string BIN_NUMBER = kANBAN_MASTER.BIN_NUMBER_END;

            int bin_num = 0;
            try
            {
                string[] bin_split = BIN_NUMBER.Split('S');
                bin_num = int.Parse(bin_split[1]);
                bin_num = bin_num + kANBAN_MASTER.STORE_KANBAN_QTY;
                kANBAN_MASTER.BIN_NUMBER_END = "BS0" + bin_num;
            }
            catch { kANBAN_MASTER.BIN_NUMBER_END = ""; }





            if (ModelState.IsValid)
            {
                db.Entry(kANBAN_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = kANBAN_MASTER.ID });
            }
            return View(kANBAN_MASTER);
        }
        // GET: KANBAN_MASTER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model_list = (from e in capacitydb.SUPERMARKET_LINE_CAPACITY
                              group e by new { e.MODEL } into g
                              select new
                              {
                                  MODEL = g.Key.MODEL
                              }).ToList();
            List<SelectListItem> models = new List<SelectListItem>();

            models.Add(new SelectListItem { Text = "<--add model-->", Value = "DUMMY", Selected = true });

            foreach (var k in model_list)
            {
                models.Add(new SelectListItem { Text = k.MODEL.ToString(), Value = k.MODEL.ToString(), Selected = false });
            }
            models.Add(new SelectListItem { Text = "EMPTY", Value = "EMPTY", Selected = false });
            ViewBag.model_lists = models;

            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            TempData["TempModel"] = kANBAN_MASTER.PHOTO;
            Session["photo"] = kANBAN_MASTER.PHOTO;

            kANBAN_MASTER.MODEL = "DUMMY";

            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }

        // GET: KANBAN_MASTER/Details/5
        public ActionResult SliderAddressDetails(string sliderAddress)
        {
            if (sliderAddress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kANBAN_MASTER2 = (from s in db.KANBAN_MASTER where s.SLIDER_ADDRESS.Contains(sliderAddress) select s).ToList();

            KANBAN_MASTER kANBAN_MASTER = new KANBAN_MASTER();

            foreach (var a in kANBAN_MASTER2)
            {
                kANBAN_MASTER.BASIC_FINISH_DATE = a.BASIC_FINISH_DATE;
                kANBAN_MASTER.BIN_NUMBER_END = a.BIN_NUMBER_END;
                kANBAN_MASTER.BIN_TYPE = a.BIN_TYPE;
                kANBAN_MASTER.CYCLE_TIME = a.CYCLE_TIME;
                kANBAN_MASTER.ID = a.ID;
                kANBAN_MASTER.KITTING_SLIDER = a.KITTING_SLIDER;
                kANBAN_MASTER.LINE = a.LINE;
                kANBAN_MASTER.LOCATION = a.LOCATION;
                kANBAN_MASTER.MODEL = a.MODEL;
                kANBAN_MASTER.OUTPUT = a.OUTPUT;
                kANBAN_MASTER.PART_NAME = a.PART_NAME;
                kANBAN_MASTER.PART_NO = a.PART_NO;
                kANBAN_MASTER.PHOTO = a.PHOTO;
                kANBAN_MASTER.PROCESS = a.PROCESS;
                kANBAN_MASTER.PROD_KANBAN_QTY = a.PROD_KANBAN_QTY;
                kANBAN_MASTER.QTY_PER_BIN = a.QTY_PER_BIN;
                kANBAN_MASTER.QTY_PER_TRAY = a.QTY_PER_TRAY;
                kANBAN_MASTER.REMARKS = a.REMARKS;
                kANBAN_MASTER.REVISION = a.REVISION;
                kANBAN_MASTER.SLIDER_ADDRESS = a.SLIDER_ADDRESS;
                kANBAN_MASTER.STORE_KANBAN_QTY = a.STORE_KANBAN_QTY;
                kANBAN_MASTER.SUPPLIER = a.SUPPLIER;
                kANBAN_MASTER.TARY_PER_BIN = a.TARY_PER_BIN;
                kANBAN_MASTER.USAGE = a.USAGE;

            }
            TempData["TempModel"] = kANBAN_MASTER.PHOTO;
            Session["photo"] = kANBAN_MASTER.PHOTO;
            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }
        // POST: KANBAN_MASTER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PHOTO,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,BIN_NUMBER_END,REMARKS,SUPPLIER,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                kANBAN_MASTER.PHOTO = data;
            }
            else
            {
                if (TempData["TempModel"] != null)
                {
                    byte[] photo_temp = (byte[])TempData["TempModel"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }
                else
                {
                    byte[] photo_temp = (byte[])Session["photo"];
                    kANBAN_MASTER.PHOTO = photo_temp;
                }

            }
            var model_kanban_master = (from s in db.KANBAN_MASTER where s.ID == kANBAN_MASTER.ID select new { MODEL = s.MODEL }).ToList();

            string model_temp = model_kanban_master[0].MODEL.ToString();

            if (kANBAN_MASTER.MODEL == "EMPTY")
            {
                kANBAN_MASTER.MODEL = "";
            }
            else if (kANBAN_MASTER.MODEL == "DUMMY")
            {
                kANBAN_MASTER.MODEL = model_temp;
            }
            else
            {
                int J = model_temp.Split('/').Length;
                if (model_temp.Length != 0)
                {
                    kANBAN_MASTER.MODEL = kANBAN_MASTER.MODEL + '/' + model_temp;
                }


            }


            //string model_temp = model_kanban_master

            string[] lines_output = kANBAN_MASTER.LINE.ToString().Split();
            decimal total_prod_output = 0;
            string[] all_model = kANBAN_MASTER.MODEL.Split('/');
            foreach (var mods in all_model)
            {

                foreach (var lin in lines_output)
                {
                    SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_H0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.H0 == lin && s.MODEL == mods).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();
                    SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_A0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.A0 == lin && s.MODEL == mods).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();
                    if (LINE_CAPACITY_DB_H0 != null && LINE_CAPACITY_DB_A0 == null)
                    {
                        total_prod_output = total_prod_output + LINE_CAPACITY_DB_H0.CAPACITY;

                    }
                    else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 == null)
                    {
                        total_prod_output = total_prod_output + LINE_CAPACITY_DB_A0.CAPACITY;
                    }
                    else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 != null)
                    {
                        total_prod_output = total_prod_output + LINE_CAPACITY_DB_A0.CAPACITY + LINE_CAPACITY_DB_H0.CAPACITY;
                    }
                    else
                    {

                    }
                }

            }
            decimal line_output_decimal = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());
            decimal bin_qty_decimal = decimal.Parse(kANBAN_MASTER.QTY_PER_BIN.ToString());
            decimal per_usage_decimal = decimal.Parse(kANBAN_MASTER.USAGE.ToString());
            decimal lineoutput_decimal_store = decimal.Parse(kANBAN_MASTER.OUTPUT.ToString());

            kANBAN_MASTER.PROD_KANBAN_QTY = calculate_prod_kanban_qty(total_prod_output,
                bin_qty_decimal, per_usage_decimal);

            kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(total_prod_output,
                bin_qty_decimal, per_usage_decimal);

            string BIN_NUMBER = kANBAN_MASTER.BIN_NUMBER_END;

            int bin_num = 0;

            try
            {
                if (kANBAN_MASTER.MODEL.Contains('/'))
                {

                    List<String> mod_sort = new List<string>();
                    var mod_sort_temp = kANBAN_MASTER.MODEL.Split('/');

                    foreach (var item in mod_sort_temp)
                    {
                        mod_sort.Add(item);
                    }

                    mod_sort.Sort();


                    kANBAN_MASTER.MODEL = "";
                    foreach (var k in mod_sort)
                    {
                        if (k != "")
                        {
                            kANBAN_MASTER.MODEL = kANBAN_MASTER.MODEL + "/" + k;

                        }


                    }
                    kANBAN_MASTER.MODEL = kANBAN_MASTER.MODEL.Remove(0, 1);

                }
            }
            catch
            {

            }

            if (ModelState.IsValid)
            {
                db.Entry(kANBAN_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = kANBAN_MASTER.ID });
            }
            return View(kANBAN_MASTER);
        }





        // GET: KANBAN_MASTER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(kANBAN_MASTER);
        }

        // POST: KANBAN_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            db.KANBAN_MASTER.Remove(kANBAN_MASTER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //GET : KANBAN_MASTER/CreateProdKanban
        public ActionResult CreateProdKanban(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);


            if (kANBAN_MASTER == null)
            {
                return HttpNotFound();
            }

            return View(kANBAN_MASTER);
        }

        [HttpGet]
        public FileStreamResult CreateProdKanbanPdf(int? id, [Bind(Include = "ID,PHOTO,PART_NO,PART_NAME,MODEL,LINE,OUTPUT,USAGE,PROCESS,QTY_PER_TRAY,TARY_PER_BIN,QTY_PER_BIN,BIN_TYPE,LOCATION,SLIDER_ADDRESS,KITTING_SLIDER,STORE_KANBAN_QTY,PROD_KANBAN_QTY,BASIC_FINISH_DATE,REVISION,CYCLE_TIME")] KANBAN_MASTER kANBAN_MASTER, string kanban_color)
        {
            KANBAN_MASTER kANBAN_MASTER_db = db.KANBAN_MASTER.Find(kANBAN_MASTER.ID);

            kANBAN_MASTER_db.LINE = kANBAN_MASTER.LINE;
            SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_H0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.H0 == kANBAN_MASTER.LINE.ToString()).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();
            SUPERMARKET_LINE_CAPACITY LINE_CAPACITY_DB_A0 = capacitydb.SUPERMARKET_LINE_CAPACITY.Where(s => s.A0 == kANBAN_MASTER.LINE.ToString()).FirstOrDefault<SUPERMARKET_LINE_CAPACITY>();

            decimal line_output_decimal = decimal.Parse(kANBAN_MASTER_db.USAGE.ToString());
            decimal bin_qty_decimal = decimal.Parse(kANBAN_MASTER_db.QTY_PER_BIN.ToString());
            decimal per_usage_decimal = decimal.Parse(kANBAN_MASTER_db.USAGE.ToString());

            if (LINE_CAPACITY_DB_H0 != null && LINE_CAPACITY_DB_A0 == null)
            {

                decimal lineoutput_decimal_store = decimal.Parse(LINE_CAPACITY_DB_H0.H0.ToString());

                kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(lineoutput_decimal_store, bin_qty_decimal, per_usage_decimal);
            }
            else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 == null)
            {
                decimal lineoutput_decimal_store = decimal.Parse(LINE_CAPACITY_DB_A0.A0.ToString());

                kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(lineoutput_decimal_store, bin_qty_decimal, per_usage_decimal);
            }
            else if (LINE_CAPACITY_DB_A0 != null && LINE_CAPACITY_DB_H0 != null)
            {
                decimal lineoutput_decimal_store_A0 = decimal.Parse(LINE_CAPACITY_DB_A0.A0.ToString());
                decimal lineoutput_decimal_store_H0 = decimal.Parse(LINE_CAPACITY_DB_H0.H0.ToString());
                decimal both = lineoutput_decimal_store_A0 + lineoutput_decimal_store_H0;

                kANBAN_MASTER.STORE_KANBAN_QTY = calculate_store_kanban_qty(both, bin_qty_decimal, per_usage_decimal);
            }
            else
            {

            }


            if (kanban_color == "FG")
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_FG_kanban(kANBAN_MASTER_db);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }
            else if (kanban_color == "SUB_ST")
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_Production_kanban(kANBAN_MASTER_db, kanban_color);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }
            else
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_Production_kanban(kANBAN_MASTER_db, kanban_color);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }

        }

        [HttpGet]
        public ActionResult update_slider()
        {


            var kanbanMaster = from s in db.KANBAN_MASTER.Select(p => new KANBAN_MASTER_index
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                PART_NAME = p.PART_NAME,
                MODEL = p.MODEL,
                LINE = p.LINE,
                OUTPUT = p.OUTPUT,
                USAGE = p.USAGE,
                PROCESS = p.PROCESS,
                QTY_PER_TRAY = p.QTY_PER_TRAY,
                TARY_PER_BIN = p.TARY_PER_BIN,
                QTY_PER_BIN = p.QTY_PER_BIN,
                BIN_TYPE = p.BIN_TYPE,
                LOCATION = p.LOCATION,
                SLIDER_ADDRESS = p.SLIDER_ADDRESS,
                KITTING_SLIDER = p.KITTING_SLIDER,
                STORE_KANBAN_QTY = p.STORE_KANBAN_QTY,
                PROD_KANBAN_QTY = p.PROD_KANBAN_QTY,
                BASIC_FINISH_DATE = p.BASIC_FINISH_DATE,
                REVISION = p.REVISION,
                BIN_NUMBER_END = p.BIN_NUMBER_END,
                CYCLE_TIME = p.CYCLE_TIME,
                REMARKS = p.REMARKS,
                SUPPLIER = p.SUPPLIER
            })
                               select s;

            var sUPERMARKET_SLIDER = sliderdb.SUPERMARKET_SLIDER.ToList();
            SUPERMARKET_SLIDER smk_upd = new SUPERMARKET_SLIDER();



            foreach (var m in sUPERMARKET_SLIDER)
            {
                int p1 = 0;
                foreach (var k in kanbanMaster)
                {
                    DateTime basic_fin_date = Convert.ToDateTime(k.BASIC_FINISH_DATE);
                    DateTime date_tdy = DateTime.Now;

                    double value_betweet = basic_fin_date.Subtract(date_tdy).Days / (365.2425 / 12);


                    double days_span = value_betweet;

                    if (!(k.SLIDER_ADDRESS is null))
                    {
                        if (k.SLIDER_ADDRESS.Contains(m.SLIDER_ADDRESS))
                        {

                            if (m.PART_NUMBER != "EMPTY" && m.MULTIPLE_PLART == true)
                            {
                                string part_num = m.PART_NUMBER + " " + k.PART_NO;
                                smk_upd.PART_NUMBER = part_num;
                            }
                            else
                            {
                                smk_upd.PART_NUMBER = k.PART_NO;
                            }
                            //smk_upd.PART_NUMBER = k.PART_NO;
                            smk_upd.ID = m.ID;
                            smk_upd.RACK = m.RACK;
                            smk_upd.SLIDER_ADDRESS = m.SLIDER_ADDRESS;
                            smk_upd.MULTIPLE_PLART = m.MULTIPLE_PLART;
                            if (days_span >= 0 && days_span < 4)
                            {
                                smk_upd.STATUS = "SLOW MOVING";
                            }
                            else if (days_span >= 4)
                            {
                                smk_upd.STATUS = "MOVING";
                            }
                            else if (days_span < 0)
                            {
                                smk_upd.STATUS = "EOL";
                            }

                            smk_upd.BIN = m.BIN;
                            smk_upd.AREA = m.AREA;
                            smk_upd.COLOR = m.COLOR;
                            update_rows_slider_kanban(smk_upd);
                            p1++;
                        }
                    }

                }
                if (p1 == 0)
                {
                    smk_upd.PART_NUMBER = "EMPTY";
                    smk_upd.ID = m.ID;
                    smk_upd.RACK = m.RACK;
                    smk_upd.SLIDER_ADDRESS = m.SLIDER_ADDRESS;
                    smk_upd.STATUS = "EMPTY";
                    smk_upd.BIN = m.BIN;
                    smk_upd.AREA = m.AREA;
                    smk_upd.COLOR = m.COLOR;
                    update_rows_slider_kanban(smk_upd);
                }
            }
            return RedirectToAction("sliderAddress");
        }

        [HttpGet]
        public FileStreamResult CreateStoreKanbanPdf(int? id)
        {

            KANBAN_MASTER kANBAN_MASTER = db.KANBAN_MASTER.Find(id);
            var bsBin = from s in db.BS_BIN_REGISTER.Select(p => new BSBINDATA
            {
                ID = p.ID,
                PART_NO = p.PART_NO,
                STORAGE_BIN = p.STORAGE_BIN,
            })
                        select s;


            if (kANBAN_MASTER.PROCESS == "H0-SB" || kANBAN_MASTER.PROCESS.Contains("H0-SB"))
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_Store_kanban_Large(kANBAN_MASTER,bsBin.Where(x=> x.PART_NO.Equals(kANBAN_MASTER.PART_NO)).ToList());
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }
            else
            {
                CreateKanbanPDF CKPDF = new CreateKanbanPDF();
                PdfDocument document = CKPDF.create_Store_kanban(kANBAN_MASTER);
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                return File(stream, "application/pdf");
            }

        }
        [HttpGet]
        public FileStreamResult workaraKanbanPdf(string part_number, string work_ara, string quantity)
        {
            var kANBAN_MASTER2 = (from s in db.KANBAN_MASTER where s.PART_NO.Contains(part_number) select s).ToList();

            KANBAN_MASTER kANBAN_MASTER = new KANBAN_MASTER();
            kANBAN_MASTER = kANBAN_MASTER2[0];

            CreateKanbanPDF CKPDF = new CreateKanbanPDF();
            PdfDocument document = CKPDF.createrm_work_ara_kanban(kANBAN_MASTER, work_ara, quantity);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, "application/pdf");
        }

        public ActionResult GENERATE_WORKARA_KANBAN()
        {
            return View();
        }
        public ActionResult CreateSliderKanban()
        {
            return View();
        }

        public ActionResult CreateRackJWAPdf()
        {
            return View();
        }

        public ActionResult COLORCHANGER()
        {
            return View();
        }

        [HttpGet]
        public FileStreamResult CreateSLIDERKanbanPdf(string PN1, string PN2, string PN3, string PN4, string PN5, string PN6, string PN7, string PN8, string PN9)
        {
            int lenpn1 = PN1.Length;
            int lenpn2 = PN2.Length;
            int lenpn3 = PN3.Length;
            int lenpn4 = PN4.Length;
            int lenpn5 = PN5.Length;
            int lenpn6 = PN6.Length;
            int lenpn7 = PN7.Length;
            int lenpn8 = PN8.Length;
            int lenpn9 = PN9.Length;

            List<string> part_numbers = new List<string>();
            CreateKanbanPDF CKPDF = new CreateKanbanPDF();
            PdfDocument document = new PdfDocument();

            if (lenpn1 != 0)
            {
                part_numbers.Add(PN1);
            }

            if (lenpn2 != 0)
            {
                part_numbers.Add(PN2);
            }
            if (lenpn3 != 0)
            {
                part_numbers.Add(PN3);
            }

            if (lenpn4 != 0)
            {
                part_numbers.Add(PN4);
            }

            if (lenpn5 != 0)
            {
                part_numbers.Add(PN5);
            }

            if (lenpn6 != 0)
            {
                part_numbers.Add(PN6);
            }
            if (lenpn7 != 0)
            {
                part_numbers.Add(PN7);
            }
            if (lenpn8 != 0)
            {
                part_numbers.Add(PN8);
            }
            if (lenpn9 != 0)
            {
                part_numbers.Add(PN9);
            }

            int i = 1;
            List<string> empty_part_num = new List<string>();
            List<string> not_empty_part_num = new List<string>();
            IEnumerable<WebApplication1.Models.KANBAN_MASTER> kANBAN_MASTER = new List<KANBAN_MASTER>();

            foreach (var prts in part_numbers)
            {


                if (prts.Contains('$') == true)
                {

                    empty_part_num.Add(prts);
                }
                else
                {

                    not_empty_part_num.Add(prts);

                }

            }

            if (part_numbers.Count > 0)
            {
                kANBAN_MASTER = db.KANBAN_MASTER.Where(x => not_empty_part_num.Contains(x.PART_NO));

            }

            document = CKPDF.create_slider_kanban(kANBAN_MASTER.ToList(), empty_part_num);

            //PdfDocument document = new PdfDocument();
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, "application/pdf");
        }

        [HttpGet]
        public FileStreamResult CreateRackJWAPdfG(string PN1, string PN2, string PN3, string PN4, string PN5, string PN6, string PN7,
            string PN8,string ddlColor)
        {
            int lenpn1 = PN1.Length;
            int lenpn2 = PN2.Length;
            int lenpn3 = PN3.Length;
            int lenpn4 = PN4.Length;
            int lenpn5 = PN5.Length;
            int lenpn6 = PN6.Length;
            int lenpn7 = PN7.Length;
            int lenpn8 = PN8.Length;

            List<string> part_numbers = new List<string>();
            CreateKanbanPDF CKPDF = new CreateKanbanPDF();
            PdfDocument document = new PdfDocument();

            if (lenpn1 != 0)
            {
                part_numbers.Add(PN1);
            }

            if (lenpn2 != 0)
            {
                part_numbers.Add(PN2);
            }
            if (lenpn3 != 0)
            {
                part_numbers.Add(PN3);
            }

            if (lenpn4 != 0)
            {
                part_numbers.Add(PN4);
            }

            if (lenpn5 != 0)
            {
                part_numbers.Add(PN5);
            }

            if (lenpn6 != 0)
            {
                part_numbers.Add(PN6);
            }
            if (lenpn7 != 0)
            {
                part_numbers.Add(PN7);
            }
            if (lenpn8 != 0)
            {
                part_numbers.Add(PN8);
            }

            List<string> empty_part_num = new List<string>();
            List<string> not_empty_part_num = new List<string>();
            List<KANBAN_MASTER> kANBAN_MASTER = new List<KANBAN_MASTER>();

            foreach (var prts in part_numbers)
            {


                if (prts.Contains('$') == true)
                {

                    empty_part_num.Add(prts);
                }
                else
                {

                    not_empty_part_num.Add(prts);

                }

            }

            if (part_numbers.Count > 0)
            {
                kANBAN_MASTER = db.KANBAN_MASTER.Where(x => not_empty_part_num.Contains(x.PART_NO)).ToList();

            }

            document = CKPDF.create_slider_kanbanWARJ(kANBAN_MASTER, empty_part_num,ddlColor);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, "application/pdf");
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            return (System.Drawing.Image)(new Bitmap(imgToResize, size));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        private void update_rows_slider_kanban(SUPERMARKET_SLIDER sUPER_SLIDER)
        {
            if (ModelState.IsValid)
            {
                var existingEntity = sliderdb.SUPERMARKET_SLIDER.Find(sUPER_SLIDER.ID);

                //sliderdb.Entry(sUPER_SLIDER).State = EntityState.Modified;
                sliderdb.Entry(existingEntity).CurrentValues.SetValues(sUPER_SLIDER);
                sliderdb.SaveChanges();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        private void update_rows_slider_kanban_create_kanban(SUPERMARKET_SLIDER sUPER_SLIDER)
        {
            if (ModelState.IsValid)
            {
                var existingEntity = sliderdb.SUPERMARKET_SLIDER.Find(sUPER_SLIDER.ID);

                //sliderdb.Entry(sUPER_SLIDER).State = EntityState.Modified;
                sliderdb.Entry(existingEntity).CurrentValues.SetValues(sUPER_SLIDER);
                sliderdb.SaveChanges();
            }

        }

        private THIRD_SERVICE.ThirdServiceSoapClient Ts;

        public ActionResult GENERATE_RM_WAREHOUSE_STOCKS(string storageBins)
        {



            return View();

        }

        private List<string> generateRacks()
        {
            var list = new List<string>();



            return list;
        }

        public ActionResult WM2002(string rackTyp)
        {
            List<WM2002DATA> data = new List<WM2002DATA>();
            DataTable dtSAP = new DataTable();
            dtSAP.Columns.Add("Material", typeof(string));
            dtSAP.Columns.Add("Location", typeof(string));
            dtSAP.Columns.Add("Batch", typeof(string));
            dtSAP.Columns.Add("Quantity", typeof(string));
            STOCK_FROM_MATERIAL.get_part_quantity_from_binSoapClient C = new STOCK_FROM_MATERIAL.get_part_quantity_from_binSoapClient();
            var DataFromSAP = C.Transit_stock_2002("");

            var dataFromDB = db.RACKS_WAREHOUSE.ToList();
            ViewBag.listOfRacks = (from s in dataFromDB.Select(p => p.RACK_TYPE) select s).Distinct().ToList();


            if (rackTyp.IsNullOrWhiteSpace())
            {
                int empty = 0;
                rackTyp = "Rack F";
                var rackNumbersList = (from s in dataFromDB orderby s.RACK_NUMBER where s.RACK_TYPE.Equals(rackTyp) select s).ToList();
                var cols = rackNumbersList.Select(p => p.RACK_NUMBER.Substring(1, 2)).Distinct().ToList();
                var rows = rackNumbersList.Select(p => p.RACK_NUMBER.Substring(3, 1)).Distinct().ToList();
                foreach (var i in rackNumbersList)
                {
                    DataRow[] dr = DataFromSAP.Select("bin = '" + i.RACK_NUMBER + "'");
                    if (dr.Length <= 0)
                    {
                        empty++;
                    }
                    foreach (DataRow t in dr)
                    {
                        WM2002DATA temp = new WM2002DATA();
                        var dbCC = dataFromDB.Where(x => x.RACK_NUMBER.Equals(t.ItemArray[6].ToString())).Select(x => x.CYCLE_COUNT_DATE).ToList();
                        temp.MATERIAL = t.ItemArray[0].ToString();
                        temp.BIN = t.ItemArray[6].ToString();
                        temp.BATCH = t.ItemArray[5].ToString();
                        temp.QUANTITY = long.Parse(t.ItemArray[1].ToString());
                        temp.CYCLE_COUNT = dbCC[0].ToString("dd/MM/yyyy");
                        DateTime datelastmvtDT = DateTime.ParseExact(t.ItemArray[2].ToString(), "yyyy-MM-dd", null);
                        // Calculate the time difference
                        TimeSpan timeDifference = DateTime.Now - datelastmvtDT;
                        if (timeDifference.Days > 30)
                        {
                            temp.COLOR = "RED";
                        }
                        else
                        {
                            temp.COLOR = "GREEN";
                        }

                        data.Add(temp);
                    }

                }
                ViewBag.numberOfRows = cols;
                ViewBag.numberOfCols = rows;
                ViewBag.RackType = data[0].BIN.Substring(0, 1);
                ViewBag.EmptyCount = empty;

            }
            else
            {
                int empty = 0;
                var rackNumbersList = (from s in dataFromDB orderby s.RACK_NUMBER where s.RACK_TYPE.Equals(rackTyp) select s).ToList();
                var cols = rackNumbersList.Select(p => p.RACK_NUMBER.Substring(1, 2)).Distinct().ToList();
                var rows = rackNumbersList.Select(p => p.RACK_NUMBER.Substring(3, 1)).Distinct().ToList();
                foreach (var i in rackNumbersList)
                {
                    DataRow[] dr = DataFromSAP.Select("bin = '" + i.RACK_NUMBER + "'");
                    if (dr.Length <= 0)
                    {
                        empty++;
                    }
                    foreach (DataRow t in dr)
                    {
                        WM2002DATA temp = new WM2002DATA();
                        var dbCC = dataFromDB.Where(x => x.RACK_NUMBER.Equals(t.ItemArray[6].ToString())).Select(x => x.CYCLE_COUNT_DATE).ToList();
                        temp.MATERIAL = t.ItemArray[0].ToString();
                        temp.BIN = t.ItemArray[6].ToString();
                        temp.BATCH = t.ItemArray[5].ToString();
                        temp.QUANTITY = long.Parse(t.ItemArray[1].ToString());
                        temp.CYCLE_COUNT = dbCC[0].ToString("dd/MM/yyyy");
                        DateTime datelastmvtDT = DateTime.ParseExact(t.ItemArray[2].ToString(), "yyyy-MM-dd", null);
                        // Calculate the time difference
                        TimeSpan timeDifference = DateTime.Now - datelastmvtDT;
                        if (timeDifference.Days > 30)
                        {
                            temp.COLOR = "RED";
                        }
                        else
                        {
                            temp.COLOR = "GREEN";
                        }

                        data.Add(temp);
                    }

                }
                ViewBag.numberOfRows = cols;
                ViewBag.numberOfCols = rows;
                ViewBag.RackType = data[0].BIN.Substring(0, 1);
                ViewBag.EmptyCount = empty;
            }

            return View(data);
        }

    }
}
