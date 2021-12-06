using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class KANBAN_MASTER
    {
        public int ID { get; set; }
        public byte[] PHOTO { get; set; }
        public string PART_NO { get; set; }
        public string PART_NAME { get; set; }
        public string MODEL { get; set; }
        public string LINE { get; set; }
        public int OUTPUT { get; set; }
        public int USAGE { get; set; }
        public string PROCESS { get; set; }
        public int QTY_PER_TRAY { get; set; }
        public int TARY_PER_BIN { get; set; }
        public int QTY_PER_BIN { get; set; }
        public string BIN_TYPE { get; set; }
        public string LOCATION { get; set; }
        public string SLIDER_ADDRESS { get; set; }
        public string KITTING_SLIDER { get; set; }
        public int STORE_KANBAN_QTY { get; set; }
        public int PROD_KANBAN_QTY { get; set; }
        public DateTime BASIC_FINISH_DATE { get; set; }
        public string REVISION { get; set; }
        public string BIN_NUMBER_END { get; set; }
        public int CYCLE_TIME { get; set; }
        public string REMARKS { get; set; }
        public string SUPPLIER { get; set; }
    }

    public class KANBAN_MASTER_index
    {
        public Int32 ID { get; set; }
        public String PART_NO { get; set; }
        public String PART_NAME { get; set; }
        public String MODEL { get; set; }
        public String LINE { get; set; }
        public Int32 OUTPUT { get; set; }
        public Int32 USAGE { get; set; }
        public String PROCESS { get; set; }
        public Int32 QTY_PER_TRAY { get; set; }
        public Int32 TARY_PER_BIN { get; set; }
        public Int32 QTY_PER_BIN { get; set; }
        public String BIN_TYPE { get; set; }
        public String LOCATION { get; set; }
        public String SLIDER_ADDRESS { get; set; }
        public String KITTING_SLIDER { get; set; }
        public Int32 STORE_KANBAN_QTY { get; set; }
        public Int32 PROD_KANBAN_QTY { get; set; }
        public DateTime BASIC_FINISH_DATE { get; set; }
        public String REVISION { get; set; }
        public string BIN_NUMBER_END { get; set; }
        public string REMARKS { get; set; }
        public string SUPPLIER { get; set; }
        public Int32 CYCLE_TIME { get; set; }


    }

    public class SUPERMARKET_SLIDER_JOINED
    {
        public Int32 ID { get; set; }
        public String SLIDER_ADDRESS { get; set; }
        public String PART_NUMBER { get; set; }
        public String STATUS { get; set; }
        public String RACK { get; set; }
        public String AREA { get; set; }
        public String BIN { get; set; }
        public String PROCESS { get; set; }
        public string COLOR { get; set; }
        public bool MULTIPLE_PLART { get; set; }
    }
    public class SUPERMARKET_RACKS
    {
        public Int32 ID { get; set; }
        public String RACK { get; set; }
    }
    public class SUPERMARKET_LINE_CAPACITY
    {
        public Int32 ID { get; set; }
        public String CUSTOMER { get; set; }
        public String H0 { get; set; }
        public String A0 { get; set; }
        public String MODEL { get; set; }
        public Int32 CAPACITY { get; set; }
        public Int32 MINUTE_30 { get; set; }
        public Int32 MINUTE_60 { get; set; }
        public Int32 MINUTE_90 { get; set; }
    }

    public class SUPERMARKET_SLIDER_REPORT
    {
        public string STATUS { get; set; }
        public string RACK { get; set; }
        public int COUNT { get; set; }
    }
    public class SUPERMARKET_SLIDER
    {
        public Int32 ID { get; set; }
        public String SLIDER_ADDRESS { get; set; }
        public String PART_NUMBER { get; set; }
        public String STATUS { get; set; }
        public String RACK { get; set; }
        public String AREA { get; set; }
        public String BIN { get; set; }
        public string COLOR { get; set; }
        public bool MULTIPLE_PLART { get; set; }
    }

    public class KB_CARD_STATS
    {

        public String PROCESS { get; set; }
        public int COUNT { get; set; }
    }
    public class RCKS_LISTS
    {
        public String RACK { get; set; }
        public String STATUS { get; set; }
        public int COUNT { get; set; }
    }
    public class RCKS_TOT
    {
        public String RACK { get; set; }
        public int COUNT { get; set; }
    }
    public class BIN_TYPE
    {
       
        public String BIN { get; set; }
        public int COUNT { get; set; }
    }

    public class BIN_RACK_COLOR
    {

        public String RACK { get; set; }
        public String BIN { get; set; }
        public String COLOR { get; set; }
    }

    public class SUBWORK_BIN_RACK
    {

        public String RACK { get; set; }
        public String BIN { get; set; }
    }

    [Table("LOGIN_INFO")]
    public class LOGIN_INFO
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int UserId { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
    }

}