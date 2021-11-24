using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class Report_data
    {

        public int get_A0_Process(List<WebApplication1.Models.KB_CARD_STATS> kb)
        {
            int result = 0;

            foreach(var item in kb)
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

        public String get_bins(sliderContext room,string[] racks_lists)
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
                foreach(var items in get_racks_and_bins_grouped)
                {
                    bin_result = items.BIN;
                }
            }


            return bin_result;
        }
        public int[] room_results(List<WebApplication1.Models.SUPERMARKET_SLIDER_REPORT> room)
        {
            
            int total_room1 = 0;
            int total_use_room1 = 0;
            int total_empty_room1 = 0;

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
            }

            int[] results = { total_room1, total_use_room1, total_empty_room1 };



            return results;
        }
    }
}