using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class Report_data
    {

        public int get_A0_Process(List<WebApplication1.Models.KB_CARD_STATS> kb)
        {
            int result = 0;

            foreach (var item in kb)
            {
                if (item.PROCESS == "A0")
                {
                    result = item.COUNT;
                }
            }

            return result;
        }

        public int get_SUB_Process(List<WebApplication1.Models.KB_CARD_STATS> kb)
        {
            int result = 0;

            foreach (var item in kb)
            {
                if (item.PROCESS == "SUB")
                {
                    result = item.COUNT;
                }
            }

            return result;
        }

        public int get_A0H0_Process(List<WebApplication1.Models.KB_CARD_STATS> kb)
        {
            int result = 0;

            foreach (var item in kb)
            {
                if (item.PROCESS == "A0 H0")
                {
                    result = item.COUNT;
                }
            }

            return result;
        }

        public int get_H0_Process(List<WebApplication1.Models.KB_CARD_STATS> kb)
        {
            int result = 0;

            foreach (var item in kb)
            {
                if (item.PROCESS == "H0")
                {
                    result = item.COUNT;
                }
            }

            return result;
        }

        public List<WebApplication1.Models.SUPERMARKET_SLIDER_REPORT_BIN> get_rack_from_bin(List<WebApplication1.Models.SUPERMARKET_SLIDER_REPORT_BIN_type> bin_part)
        {
            List<WebApplication1.Models.SUPERMARKET_SLIDER_REPORT_BIN_b> spmkt_reprt = new List<Models.SUPERMARKET_SLIDER_REPORT_BIN_b>();



            foreach (var i in bin_part)
            {
                WebApplication1.Models.SUPERMARKET_SLIDER_REPORT_BIN_b temp = new Models.SUPERMARKET_SLIDER_REPORT_BIN_b();
                temp.BIN = i.BIN;
                temp.RACK = i.RACK;
                temp.STATUS = i.STATUS;

                spmkt_reprt.Add(temp);
            }

            var spmtrpt_aggr = (from e in spmkt_reprt
                                group e by new { e.RACK, e.BIN, e.STATUS } into grouping
                                select new WebApplication1.Models.SUPERMARKET_SLIDER_REPORT_BIN
                                {
                                    RACK = grouping.Key.RACK,
                                    BIN = grouping.Key.BIN,
                                    STATUS = grouping.Key.STATUS,
                                    COUNT = grouping.Count()
                                }).ToList();



            return spmtrpt_aggr;
        }

        public String get_bins(sliderContext room, string[] racks_lists)
        {
            var get_racks_and_bins_grouped = (from c in room.SUPERMARKET_SLIDER
                                              where racks_lists.Contains(c.RACK)
                                              group c by new { c.BIN } into grouping
                                              select new
                                              {
                                                  BIN = grouping.Key.BIN,
                                                  COUNT = grouping.Count()
                                              }).ToList();
            String bin_result = "";
            int i = 0;
            if (get_racks_and_bins_grouped.Count > 1)
            {
                foreach (var items in get_racks_and_bins_grouped)
                {
                    if (i >= 2)
                    {
                        bin_result = bin_result + " \n " + items.BIN;
                        i = 0;
                    }
                    else
                    {
                        bin_result = items.BIN + @" / " + bin_result;
                    }
                    i++;


                }
            }
            else
            {
                foreach (var items in get_racks_and_bins_grouped)
                {
                    bin_result = items.BIN;
                }
            }

            if (bin_result.Substring(bin_result.Length - 2, 2).Contains("/"))
            {

            }
            return bin_result;
        }
        public int[] room_results(List<WebApplication1.Models.SUPERMARKET_SLIDER_REPORT> room)
        {

            int total_room1 = 0;
            int total_use_room1 = 0;
            int total_empty_room1 = 0;

            int test = 0;


            foreach (var item in room)
            {
                total_room1 = total_room1 + item.COUNT;
                if (item.STATUS == "EOL" || item.STATUS == "MOVING" || item.STATUS == "SLOW MOVING")
                {
                    total_use_room1 = total_use_room1 + item.COUNT;
                }
                else
                {
                    total_empty_room1 = total_empty_room1 + item.COUNT;
                }
                test = test + item.COUNT;
            }

            int[] results = { total_room1, total_use_room1, total_empty_room1 };



            return results;
        }

        public string[] get_racks(List<WebApplication1.Models.SUPERMARKET_SLIDER_REPORT_BIN> bin_type_rack_status_rack)
        {
            var only_Racks = (from c in bin_type_rack_status_rack
                              group c by new { c.RACK } into g
                              select new
                              {
                                  RACK = g.Key.RACK
                              }).ToList();
            String racks_of_lists = "";
            String racks_full = "";
            int i = 0;
            foreach (var h in only_Racks)
            {


                if (racks_full == "")
                {
                    racks_full = h.RACK;
                    racks_of_lists = h.RACK.Substring(5, 2);
                }
                else
                {
                    racks_full = racks_full + "," + h.RACK;
                    racks_of_lists = racks_of_lists + " / " + h.RACK.Substring(5, 2);


                }

            }
            string[] res = { racks_of_lists, racks_full };

            return res;
        }

        public int[] use_and_empty_calculation(List<Models.SUPERMARKET_SLIDER_REPORT_CALCULATION> CALCULATION)
        {
            int total_use = 0;
            int total_empty = 0;

            foreach (var P in CALCULATION)
            {
                if (P.STATUS == "EMPTY")
                {
                    total_empty = total_empty + P.COUNT;
                }
                else
                {
                    total_use = total_use + P.COUNT;
                }
            }

            int[] k = { total_use, total_empty };


            return k;

        }

    }
}