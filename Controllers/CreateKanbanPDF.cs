using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using ZXing;



namespace WebApplication1
{
    public class CreateKanbanPDF
    {
        public PdfDocument create_Production_kanban(KANBAN_MASTER kANBAN_MASTER, string kanban_color)
        {
            int prod_Qty = kANBAN_MASTER.PROD_KANBAN_QTY;

            //set kanban box sizes
            double width = XUnit.FromMillimeter(79);
            double height = XUnit.FromMillimeter(60);
            double barcode_box_height = XUnit.FromCentimeter(1.5);
            double barcode_box_width = XUnit.FromCentimeter(1.5);
            double pn_title_box_height = XUnit.FromCentimeter(0.5);
            double pn_title_box_width = XUnit.FromCentimeter(0.7);
            double des_title_box_height = XUnit.FromCentimeter(0.5);
            double des_title_box_width = XUnit.FromCentimeter(0.7);
            double qty_title_box_height = XUnit.FromCentimeter(0.5);
            double qty_title_box_width = XUnit.FromCentimeter(0.7);
            double pn_text_box_height = XUnit.FromCentimeter(0.5);
            double pn_text_box_width = XUnit.FromCentimeter(4.7);
            double supermarket_section_height = XUnit.FromCentimeter(1.0);
            double supermarket_section_width = XUnit.FromCentimeter(1.0);
            double qty_value_box_height = XUnit.FromCentimeter(0.5);
            double qty_value_box_width = (pn_text_box_width + supermarket_section_width) / 3;
            double bintype_text_box_height = XUnit.FromCentimeter(0.5);
            double bintype_text_box_width = (pn_text_box_width + supermarket_section_width) / 3;
            double bintype_value_box_height = XUnit.FromCentimeter(0.5);
            double bintype_value_box_width = (pn_text_box_width + supermarket_section_width) / 3;
            double line_text_box_height = XUnit.FromMillimeter(10);
            double line_text_box_width = XUnit.FromMillimeter(15); //barcode box width
            double line_value_box_height = XUnit.FromMillimeter(10); // double of pn title text box
            double line_value_box_width = XUnit.FromCentimeter(1.4);
            double slider_no_text_box_width = barcode_box_width;
            double slider_no_text_box_height = XUnit.FromCentimeter(1.7);
            double safety_part_text_box_width = barcode_box_width; // copy width from barcode box
            double safety_part_text_box_height = XUnit.FromMillimeter(4.5); //copy height of line text box box height
            double appr_part_text_box_height = XUnit.FromMillimeter(4.5); //copy height of line text box box height
            double appr_part_text_box_width = barcode_box_width; // copy width from barcode box
            double high_consump_text_box_height = XUnit.FromMillimeter(4.5); //copy height of line text box box height
            double high_consump_text_box_width = barcode_box_width;  // copy width from barcode box
            double slider_no_value_box_width = XUnit.FromMillimeter(4.5);
            double slider_no_value_box_height = XUnit.FromMillimeter(4.5);
            double kanbancard_text_box_height = XUnit.FromMillimeter(4.5);
            double kanbancard_text_box_width = barcode_box_width;
            double safety_part_value_box_width = line_value_box_width; // copy width from slider no value
            double safety_part_value_box_height = XUnit.FromMillimeter(4.5); //copy height of line text box box height
            double appr_part_value_box_width = line_value_box_width;
            double appr_part_value_box_height = XUnit.FromMillimeter(4.5); //copy height of line text box box height
            double high_consump_value_box_height = XUnit.FromMillimeter(4.5); //copy height of line text box box height
            double high_consump_value_box_width = line_value_box_width;
            double kanbancard_value_box_one_width = high_consump_value_box_width / 2;
            double kanbancard_value_box_one_height = XUnit.FromMillimeter(4.5);
            double kanbancard_value_box_two_width = kanbancard_value_box_one_width;
            double kanbancard_value_box_two_height = XUnit.FromMillimeter(4.5);
            int kanban_image_height = 145;
            int kanban_image_width = 155;
            double kitting_slider_width = XUnit.FromMillimeter(15);
            double kitting_slider_height = XUnit.FromMillimeter(13.5);
            double kitting_slider_value_width = line_value_box_width;
            double kitting_slider_value_height = XUnit.FromMillimeter(13.5);


            int col_count = 0;
            int kanban_per_page_count = 1;
            int kanban_of_number = 1;
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Size = PdfSharp.PageSize.A4;





            //Set kanban boxes  position
            double box_horizontal_position = 20.0;
            double barcode_box_horizontal_position = 20.0;
            double pn_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
            double bacode_horizontal_position = 23.0;
            double des_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
            double qty_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
            double pn_text_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
            double pn_des_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
            double supermarket_section_horizontal_position = pn_text_box_horizontal_position + pn_text_box_width;
            double qty_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width + pn_title_box_width;
            double bintype_text_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
            double bintype_value_box_horizontal_position = bintype_text_box_horizontal_position + bintype_text_box_width;
            double line_text_box_horizontal_position = 20.0;
            double plannercode_text_box_horizontal_position = 20.0;
            double line_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
            double slider_no_text_box_horizontal_position = 20.0;
            double safety_part_text_box_horizontal_position = 20.0;
            double appr_part_text_box_horizontal_position = 20.0;
            double high_consump_text_box_horizontal_position = 20.0;
            double slider_no_value_box_horizontal_position = slider_no_text_box_horizontal_position + slider_no_text_box_width;
            double kanbancard_text_box_horizontal_position = 20.0;
            double safety_part_value_box_horizontal_position = safety_part_text_box_width + safety_part_text_box_horizontal_position;
            double appr_part_value_box_horizontal_position = appr_part_text_box_horizontal_position + appr_part_text_box_width;
            double high_consump_value_box_horizontal_position = high_consump_text_box_horizontal_position + high_consump_text_box_width;
            double kanbancard_value_box_one_horizontal_position = kanbancard_text_box_horizontal_position + kanbancard_text_box_width;
            double kanbancard_value_box_two_horizontal_position = kanbancard_value_box_one_horizontal_position + kanbancard_value_box_one_width;
            double model_text_horizontal_position = kanbancard_value_box_two_horizontal_position + kanbancard_value_box_two_width;
            double revision_text_horizontal_position = width;
            double kanban_image_horizontal_position = line_value_box_horizontal_position + line_value_box_width + 5;
            double kitting_slider_horizontal_position = 20.0;
            double kitting_slider_value_horizontal_position = kitting_slider_horizontal_position + kitting_slider_width;

            double box_vertical_position = 20.0;
            double barcode_box_vertical_position = 20.0;
            double bacode_vertical_position = 23.0;
            double pn_title_box_vertical_position = 20.0;
            double des_title_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height;
            double qty_title_box_vertical_positon = des_title_box_vertical_position + des_title_box_height;
            double pn_text_box_vertical_position = 20;
            double pn_des_box_vertical_position = pn_text_box_vertical_position + pn_text_box_height;
            double supermarket_section_vertical_position = 20;
            double qty_value_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height + des_title_box_height;
            double bintype_text_box_vertical_position = qty_value_box_vertical_position;
            double bintype_value_box_vertical_position = bintype_text_box_vertical_position;
            double line_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
            double plannercode_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_text_box_height;
            double line_value_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
            double slider_no_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_value_box_height;
            double kitting_slider_vertical_position = slider_no_text_box_vertical_position + slider_no_text_box_height;
            double safety_part_text_box_vertical_position = slider_no_text_box_vertical_position + slider_no_text_box_height;
            double appr_part_text_box_vertical_position = safety_part_text_box_vertical_position + safety_part_text_box_height;
            double high_consump_text_box_vertical_position = appr_part_text_box_vertical_position + appr_part_text_box_height;
            double slider_no_value_box_vertical_position = line_value_box_vertical_position + line_value_box_height;
            double kitting_slider_value_vertical_position = slider_no_value_box_vertical_position + slider_no_value_box_height;
            double kanbancard_text_box_vertical_position = high_consump_text_box_vertical_position + high_consump_text_box_height;
            double safety_part_value_box_vertical_position = safety_part_text_box_vertical_position;
            double appr_part_value_box_vertical_position = appr_part_text_box_vertical_position;
            double high_consump_value_box_vertical_position = high_consump_text_box_vertical_position;
            double kanbancard_value_box_one_vertical_position = kanbancard_text_box_vertical_position;
            double kanbancard_value_box_two_vertical_position = kanbancard_text_box_vertical_position;
            double model_text_vertical_position = kanbancard_value_box_two_vertical_position + (kanbancard_value_box_two_height / 2);
            double revision_text_vertical_position = model_text_vertical_position;
            double kanban_image_vertical_position = bintype_text_box_vertical_position + bintype_text_box_height + 5;



            XColor kanbanColor = XColor.FromArgb(255, 112, 217);

            XGraphics gfx = XGraphics.FromPdfPage(page);

            if (kanban_color == "Subwork" && kANBAN_MASTER.PROCESS == "SUB" || kanban_color == "Subwork" && kANBAN_MASTER.PROCESS == "SUBWORK" || kanban_color == "Subwork" && kANBAN_MASTER.PROCESS.Contains("SUB") ||
                kanban_color == "Subwork" && kANBAN_MASTER.PROCESS.Contains("SUBWORK"))
            {
                kanbanColor = XColor.FromArgb(255, 255, 255);
            }
            else if (kanban_color == "SUB_ST")
            {
                kanbanColor = XColor.FromArgb(195, 195, 195);
            }
            else if (kanban_color == "Prod" && kANBAN_MASTER.PROCESS == "SUB" || kanban_color == "Prod" && kANBAN_MASTER.PROCESS == "SUBWORK" || kanban_color == "Prod" && kANBAN_MASTER.PROCESS.Contains("SUB") ||
                kanban_color == "Prod" && kANBAN_MASTER.PROCESS.Contains("SUBWORK"))
            {
                kanbanColor = XColor.FromArgb(255, 153, 51);
            }
            else if (kANBAN_MASTER.PROCESS == "H0-SB" || kANBAN_MASTER.PROCESS == "H0-SK" || kANBAN_MASTER.PROCESS == "H0-1" ||
            kANBAN_MASTER.PROCESS == "H0-2" || kANBAN_MASTER.PROCESS == "H0-3" || kANBAN_MASTER.PROCESS == "H0" || kANBAN_MASTER.PROCESS == "H0-4")
            {
                kanbanColor = XColor.FromArgb(184, 204, 228);
            }
            else if (kANBAN_MASTER.PROCESS == "A0" || kANBAN_MASTER.PROCESS.Contains("A0"))
            {
                kanbanColor = XColor.FromArgb(230, 230, 12);
            }
            else if (kANBAN_MASTER.PROCESS == "H0-J" || kANBAN_MASTER.PROCESS == "A0-J" || kANBAN_MASTER.PROCESS.Contains("H0-J") || kANBAN_MASTER.PROCESS.Contains("A0-J"))
            {
                kanbanColor = XColor.FromArgb(26, 149, 38);
            }
            else
            {
                kanbanColor = XColor.FromArgb(255, 112, 217);
            }


            XBrush brush = new XSolidBrush(kanbanColor);

            if (kANBAN_MASTER.PART_NAME.Length > 20)
            {
                kANBAN_MASTER.PART_NAME = kANBAN_MASTER.PART_NAME.Remove(20, kANBAN_MASTER.PART_NAME.Length - 20);
            }

            for (int i = 0; i < prod_Qty; i++)
            {

                line_value_box_height = pn_title_box_height * 2;
                line_value_box_width = XUnit.FromCentimeter(1.4);

                slider_no_value_box_width = line_value_box_width;
                slider_no_value_box_height = XUnit.FromCentimeter(1.7);

                slider_no_value_box_height = XUnit.FromCentimeter(1.7);
                if (kanban_per_page_count == 10)
                {
                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    page.Size = PdfSharp.PageSize.A4;
                    gfx = XGraphics.FromPdfPage(page);
                    kanban_per_page_count = 1;
                    col_count = 0;
                    //reset both position
                    box_horizontal_position = 20.0;
                    box_horizontal_position = 20.0;
                    barcode_box_horizontal_position = 20.0;
                    bacode_horizontal_position = 23.0;
                    pn_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    des_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    qty_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    pn_text_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    pn_des_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    supermarket_section_horizontal_position = pn_text_box_horizontal_position + pn_text_box_width;
                    qty_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width + pn_title_box_width;
                    bintype_text_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
                    bintype_value_box_horizontal_position = bintype_text_box_horizontal_position + bintype_text_box_width;
                    line_text_box_horizontal_position = 20.0;
                    plannercode_text_box_horizontal_position = 20.0;
                    line_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    slider_no_text_box_horizontal_position = 20.0;
                    safety_part_text_box_horizontal_position = 20.0;
                    appr_part_text_box_horizontal_position = 20.0;
                    high_consump_text_box_horizontal_position = 20.0;
                    slider_no_value_box_horizontal_position = slider_no_text_box_horizontal_position + slider_no_text_box_width;
                    kanbancard_text_box_horizontal_position = 20.0;
                    safety_part_value_box_horizontal_position = safety_part_text_box_width + safety_part_text_box_horizontal_position;
                    appr_part_value_box_horizontal_position = appr_part_text_box_horizontal_position + appr_part_text_box_width;
                    high_consump_value_box_horizontal_position = high_consump_text_box_horizontal_position + high_consump_text_box_width;
                    kanbancard_value_box_one_horizontal_position = kanbancard_text_box_horizontal_position + kanbancard_text_box_width;
                    kanbancard_value_box_two_horizontal_position = kanbancard_value_box_one_horizontal_position + kanbancard_value_box_one_width;
                    model_text_horizontal_position = kanbancard_value_box_two_horizontal_position + kanbancard_value_box_two_width;
                    revision_text_horizontal_position = width;
                    kanban_image_horizontal_position = line_value_box_horizontal_position + line_value_box_width + 5;
                    kitting_slider_horizontal_position = 20.0;
                    kitting_slider_value_horizontal_position = kitting_slider_horizontal_position + kitting_slider_width;

                    box_vertical_position = 20.0;
                    barcode_box_vertical_position = 20.0;
                    bacode_vertical_position = 23.0;
                    pn_title_box_vertical_position = 20.0;
                    des_title_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height;
                    qty_title_box_vertical_positon = des_title_box_vertical_position + des_title_box_height;
                    pn_text_box_vertical_position = 20;
                    pn_des_box_vertical_position = pn_text_box_vertical_position + pn_text_box_height;
                    supermarket_section_vertical_position = 20;
                    qty_value_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height + des_title_box_height;
                    bintype_text_box_vertical_position = qty_value_box_vertical_position;
                    bintype_value_box_vertical_position = bintype_text_box_vertical_position;
                    line_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
                    plannercode_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_text_box_height;
                    line_value_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
                    slider_no_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_value_box_height;
                    safety_part_text_box_vertical_position = slider_no_text_box_vertical_position + slider_no_text_box_height;
                    appr_part_text_box_vertical_position = safety_part_text_box_vertical_position + safety_part_text_box_height;
                    high_consump_text_box_vertical_position = appr_part_text_box_vertical_position + appr_part_text_box_height;
                    slider_no_value_box_vertical_position = line_value_box_vertical_position + line_value_box_height;
                    kanbancard_text_box_vertical_position = high_consump_text_box_vertical_position + high_consump_text_box_height;
                    safety_part_value_box_vertical_position = safety_part_text_box_vertical_position;
                    appr_part_value_box_vertical_position = appr_part_text_box_vertical_position;
                    high_consump_value_box_vertical_position = high_consump_text_box_vertical_position;
                    kanbancard_value_box_one_vertical_position = kanbancard_text_box_vertical_position;
                    kanbancard_value_box_two_vertical_position = kanbancard_text_box_vertical_position;
                    model_text_vertical_position = kanbancard_value_box_two_vertical_position + (kanbancard_value_box_two_height / 2);
                    revision_text_vertical_position = model_text_vertical_position;
                    kanban_image_vertical_position = bintype_text_box_vertical_position + bintype_text_box_height + 5;
                    kitting_slider_vertical_position = slider_no_text_box_vertical_position + slider_no_text_box_height;
                    kitting_slider_value_vertical_position = slider_no_value_box_vertical_position + slider_no_value_box_height;

                }

                if (col_count == 3)
                {
                    //reset horizontal position
                    box_horizontal_position = 20.0;
                    barcode_box_horizontal_position = 20.0;
                    bacode_horizontal_position = 23.0;
                    pn_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    des_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    qty_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    pn_text_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    pn_des_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    supermarket_section_horizontal_position = pn_text_box_horizontal_position + pn_text_box_width;
                    qty_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width + pn_title_box_width;
                    bintype_text_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
                    bintype_value_box_horizontal_position = bintype_text_box_horizontal_position + bintype_text_box_width;
                    line_text_box_horizontal_position = 20.0;
                    plannercode_text_box_horizontal_position = 20.0;
                    line_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    slider_no_text_box_horizontal_position = 20.0;
                    safety_part_text_box_horizontal_position = 20.0;
                    appr_part_text_box_horizontal_position = 20.0;
                    high_consump_text_box_horizontal_position = 20.0;
                    slider_no_value_box_horizontal_position = slider_no_text_box_horizontal_position + slider_no_text_box_width;
                    kanbancard_text_box_horizontal_position = 20.0;

                    safety_part_value_box_horizontal_position = safety_part_text_box_horizontal_position + safety_part_text_box_width;

                    appr_part_value_box_horizontal_position = appr_part_text_box_horizontal_position + appr_part_text_box_width;
                    high_consump_value_box_horizontal_position = high_consump_text_box_horizontal_position + high_consump_text_box_width;
                    kanbancard_value_box_one_horizontal_position = kanbancard_text_box_horizontal_position + kanbancard_text_box_width;
                    kanbancard_value_box_two_horizontal_position = kanbancard_value_box_one_horizontal_position + kanbancard_value_box_one_width;
                    model_text_horizontal_position = kanbancard_value_box_two_horizontal_position + kanbancard_value_box_two_width;
                    revision_text_horizontal_position = width;
                    kanban_image_horizontal_position = line_value_box_horizontal_position + line_value_box_width + 5;
                    kitting_slider_horizontal_position = 20.0;
                    kitting_slider_value_horizontal_position = kitting_slider_horizontal_position + kitting_slider_width;

                    // set the position to duplicate the designs to other kanban cards vertically
                    box_vertical_position = box_vertical_position + 15 + height;
                    barcode_box_vertical_position = barcode_box_vertical_position + 15 + height;
                    bacode_vertical_position = bacode_vertical_position + 15 + height;
                    pn_title_box_vertical_position = pn_title_box_vertical_position + 15 + height;
                    des_title_box_vertical_position = des_title_box_vertical_position + 15 + height;
                    qty_title_box_vertical_positon = qty_title_box_vertical_positon + 15 + height;
                    pn_text_box_vertical_position = pn_text_box_vertical_position + 15 + height;
                    pn_des_box_vertical_position = pn_des_box_vertical_position + 15 + height;
                    supermarket_section_vertical_position = supermarket_section_vertical_position + 15 + height;
                    qty_value_box_vertical_position = qty_value_box_vertical_position + 15 + height;
                    bintype_text_box_vertical_position = bintype_text_box_vertical_position + 15 + height;
                    bintype_value_box_vertical_position = bintype_value_box_vertical_position + 15 + height;
                    line_text_box_vertical_position = line_text_box_vertical_position + 15 + height;
                    plannercode_text_box_vertical_position = plannercode_text_box_vertical_position + 15 + height;
                    line_value_box_vertical_position = line_value_box_vertical_position + 15 + height;
                    slider_no_text_box_vertical_position = slider_no_text_box_vertical_position + 15 + height;
                    safety_part_text_box_vertical_position = safety_part_text_box_vertical_position + 15 + height;
                    appr_part_text_box_vertical_position = appr_part_text_box_vertical_position + 15 + height;
                    high_consump_text_box_vertical_position = high_consump_text_box_vertical_position + 15 + height;
                    slider_no_value_box_vertical_position = slider_no_value_box_vertical_position + 15 + height;
                    kanbancard_text_box_vertical_position = kanbancard_text_box_vertical_position + 15 + height;
                    safety_part_value_box_vertical_position = safety_part_value_box_vertical_position + 15 + height;
                    appr_part_value_box_vertical_position = appr_part_value_box_vertical_position + 15 + height;
                    high_consump_value_box_vertical_position = high_consump_value_box_vertical_position + 15 + height;
                    kanbancard_value_box_one_vertical_position = kanbancard_value_box_one_vertical_position + 15 + height;
                    kanbancard_value_box_two_vertical_position = kanbancard_value_box_two_vertical_position + 15 + height;
                    model_text_vertical_position = model_text_vertical_position + 15 + height;
                    revision_text_vertical_position = revision_text_vertical_position + 15 + height;
                    kanban_image_vertical_position = kanban_image_vertical_position + 15 + height;
                    kitting_slider_vertical_position = kitting_slider_vertical_position + 15 + height;
                    kitting_slider_value_vertical_position = kitting_slider_value_vertical_position + 15 + height;

                    //reset count
                    col_count = 0;
                }

                var QCwriter = new BarcodeWriter();
                QCwriter.Format = BarcodeFormat.QR_CODE;
                QCwriter.Options.Height = 155;
                QCwriter.Options.Width = 155;
                QCwriter.Options.Margin = 0;
                var result = QCwriter.Write(kANBAN_MASTER.PART_NO + "$" + kANBAN_MASTER.LINE);
                var barcodeBitmap = new Bitmap(result);
                barcodeBitmap.SetResolution(300.0F, 300.0F);

                string second_barcode = kANBAN_MASTER.PART_NO + "$" + kANBAN_MASTER.LINE + "$" + (i + 1).ToString() + "/" + prod_Qty;
                var second_writer = new BarcodeWriter();
                second_writer.Format = BarcodeFormat.QR_CODE;
                second_writer.Options.Height = 135;
                second_writer.Options.Width = 135;
                second_writer.Options.Margin = 0;
                var second_barcode_result = second_writer.Write(second_barcode);
                var secondbitmap = new Bitmap(second_barcode_result);
                secondbitmap.SetResolution(400.0F, 400.0F);



                XTextFormatter tf = new XTextFormatter(gfx);
                System.Drawing.Image imagex = barcodeBitmap;
                System.Drawing.Image seconds_barcode_image = secondbitmap;


                MemoryStream ms = new MemoryStream(kANBAN_MASTER.PHOTO);
                System.Drawing.Image images = new Bitmap(ms);

                XPen pen = new XPen(XColors.Black, 0.5);
                XImage img = XImage.FromGdiPlusImage(imagex);
                XImage sndimg = XImage.FromGdiPlusImage(seconds_barcode_image);
                System.Drawing.Image photo = resizeImage(images, new Size(kanban_image_width, kanban_image_height));
                XImage pho = XImage.FromGdiPlusImage(photo);

                var bounds = new XRect(gfx.PageOrigin, gfx.PageSize);
                XPoint points = new XPoint(pn_title_box_horizontal_position + 10, pn_title_box_vertical_position + 7);



                //draw boxes

                //draw card box
                gfx.DrawRectangle(pen, box_horizontal_position, box_vertical_position, width, height);

                //draw barcode box and barcode
                gfx.DrawRectangle(pen, barcode_box_horizontal_position, barcode_box_vertical_position, barcode_box_width, barcode_box_height);
                gfx.DrawImage(img, bacode_horizontal_position, bacode_vertical_position);
                gfx.DrawImage(pho, kanban_image_horizontal_position, kanban_image_vertical_position);
                string max_kanban_Qty = prod_Qty.ToString();


                //Draw a box containing first kanban value
                gfx.DrawRectangle(pen, kanbancard_value_box_two_horizontal_position, kanbancard_value_box_two_vertical_position, kanbancard_value_box_two_width, kanbancard_value_box_two_height);
                points = new XPoint(kanbancard_value_box_two_horizontal_position + (kanbancard_value_box_two_width / 2), kanbancard_value_box_two_vertical_position + (kanbancard_value_box_two_height / 2));
                gfx.DrawString(max_kanban_Qty, new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                int add_pos = 0;
                int model_font_size = 12;

                if (kANBAN_MASTER.MODEL.Length > 8 && kANBAN_MASTER.MODEL.Length < 14)
                {
                    add_pos = 15;
                }
                else if (kANBAN_MASTER.MODEL.Length >= 14)
                {
                    add_pos = 26;
                    model_font_size = 10;
                }

                XPoint point_model_name = new XPoint(model_text_horizontal_position + 5, model_text_vertical_position);
                gfx.DrawString(kANBAN_MASTER.MODEL, new XFont("Arial", model_font_size, XFontStyle.Bold), XBrushes.Black, point_model_name, XStringFormats.CenterLeft);

                XPoint revision_text_point = new XPoint(revision_text_horizontal_position + 20, revision_text_vertical_position);
                gfx.DrawString(kANBAN_MASTER.REVISION, new XFont("Arial", 11, XFontStyle.Bold), XBrushes.Red, revision_text_point, XStringFormats.CenterRight);

                //Draw a box containing first kanban value
                gfx.DrawRectangle(pen, kanbancard_value_box_one_horizontal_position, kanbancard_value_box_one_vertical_position, kanbancard_value_box_one_width, kanbancard_value_box_one_height);
                points = new XPoint(kanbancard_value_box_one_horizontal_position + (kanbancard_value_box_one_width / 2), kanbancard_value_box_one_vertical_position + (kanbancard_value_box_one_height / 2));


                gfx.DrawString(kanban_of_number.ToString(), new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);
                kanban_of_number++;

                //Draw a box containing kanbancard text
                gfx.DrawRectangle(pen, brush, kanbancard_text_box_horizontal_position, kanbancard_text_box_vertical_position, kanbancard_text_box_width, kanbancard_text_box_height);
                points = new XPoint(kanbancard_text_box_horizontal_position + (kanbancard_text_box_width / 2), kanbancard_text_box_vertical_position + (kanbancard_text_box_height / 2));
                gfx.DrawString("Kanban Card", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //design kanban
                //draw the P/N box with text
                gfx.DrawRectangle(pen, brush, pn_title_box_horizontal_position, pn_title_box_vertical_position, pn_title_box_width, pn_title_box_height);
                points = new XPoint(pn_title_box_horizontal_position + 2, pn_title_box_vertical_position + (pn_title_box_height / 2));
                gfx.DrawString("P/N", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.CenterLeft);

                //Draw the Des box with text
                gfx.DrawRectangle(pen, brush, des_title_box_horizontal_position, des_title_box_vertical_position, des_title_box_width, des_title_box_height);
                points = new XPoint(des_title_box_horizontal_position + 2, des_title_box_vertical_position + (des_title_box_height / 2));
                gfx.DrawString("Des", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.CenterLeft);

                //Draw Qty : box with text
                gfx.DrawRectangle(pen, brush, qty_title_box_horizontal_position, qty_title_box_vertical_positon, qty_title_box_width, qty_title_box_height);
                points = new XPoint(qty_title_box_horizontal_position + 2, qty_title_box_vertical_positon + (qty_title_box_height / 2));
                gfx.DrawString("Qty:", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.CenterLeft);

                //Draw a box containing the part number of the kanban
                gfx.DrawRectangle(pen, brush, pn_text_box_horizontal_position, pn_text_box_vertical_position, pn_text_box_width, pn_text_box_height);
                points = new XPoint(pn_text_box_horizontal_position + 2, pn_text_box_vertical_position + (pn_text_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.PART_NO, new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.CenterLeft);



                //Draw a box containing the part number description
                gfx.DrawRectangle(pen, brush, pn_des_box_horizontal_position, pn_des_box_vertical_position, pn_text_box_width, pn_text_box_height);
                points = new XPoint(pn_des_box_horizontal_position + 2, pn_des_box_vertical_position + (pn_text_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.PART_NAME, new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.CenterLeft);

                //Draw a box containing the quantity description
                gfx.DrawRectangle(pen, brush, supermarket_section_horizontal_position, supermarket_section_vertical_position, supermarket_section_width, supermarket_section_height);
                points = new XPoint(supermarket_section_horizontal_position + 2, supermarket_section_vertical_position + 2);
                //gfx.DrawString(kANBAN_MASTER.BIN_TYPE, new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.Center);
                gfx.DrawImage(sndimg, points);

                //Draw a box containing the quantity
                gfx.DrawRectangle(pen, brush, qty_value_box_horizontal_position, qty_value_box_vertical_position, qty_value_box_width, qty_value_box_height);
                points = new XPoint(qty_value_box_horizontal_position + 25, qty_value_box_vertical_position + (qty_value_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.QTY_PER_BIN.ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing the bin tyoe description
                gfx.DrawRectangle(pen, brush, bintype_text_box_horizontal_position, bintype_text_box_vertical_position, bintype_text_box_width, bintype_text_box_height);
                points = new XPoint(bintype_text_box_horizontal_position + 25, bintype_text_box_vertical_position + (bintype_text_box_height / 2));
                gfx.DrawString("Bin type", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing the bin type
                gfx.DrawRectangle(pen, brush, bintype_value_box_horizontal_position, bintype_value_box_vertical_position, bintype_value_box_width, bintype_value_box_height);
                points = new XPoint(bintype_value_box_horizontal_position + 25, bintype_value_box_vertical_position + (bintype_value_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.BIN_TYPE, new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing line title description
                gfx.DrawRectangle(pen, brush, line_text_box_horizontal_position, line_text_box_vertical_position, line_text_box_width, line_text_box_height);
                points = new XPoint(line_text_box_horizontal_position + (line_text_box_width - 5), line_text_box_vertical_position + (line_text_box_height / 2));
                gfx.DrawString("Line : ", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.CenterRight);

                int number_of_lines = 0;
                string line_string = "";
                int font_size = 8;
                if (kANBAN_MASTER.LINE == null)
                {
                    line_string = "";
                }
                else
                {
                    line_string = kANBAN_MASTER.LINE;
                    if (line_string.Length > 4)
                    {
                        font_size = 6;
                        string[] lineses = line_string.Split(' ');
                        lineses = lineses.Where(x => x != "").ToArray();
                        line_string = "";
                        for (int j = 0; j < lineses.Length; j++)
                        {
                            if (j % 2 == 0)
                            {
                                line_string = line_string + lineses[j] + "\t";

                            }
                            else
                            {
                                line_string = line_string + lineses[j] + System.Environment.NewLine;
                            }

                        }
                    }
                    number_of_lines = line_string.Length;
                }


                //Draw a box containing slider no value
                gfx.DrawRectangle(pen, line_value_box_horizontal_position, line_value_box_vertical_position, line_value_box_width, line_value_box_height);
                points = new XPoint(line_value_box_horizontal_position + 20, line_value_box_vertical_position + (line_value_box_height / 2));
                if (number_of_lines > 4)
                {
                    line_value_box_height = line_value_box_height / 1.5;
                    line_value_box_width = line_value_box_width - 2.5;
                    font_size = 5;
                }
                XRect rect = new XRect(line_value_box_horizontal_position + line_value_box_width / 4, line_value_box_vertical_position + line_value_box_height / 4, line_value_box_width, line_value_box_height);
                tf.DrawString(line_string, new XFont("Arial", font_size), XBrushes.Black, rect, XStringFormats.TopLeft);


                //Draw a box containing slider no: text
                gfx.DrawRectangle(pen, brush, slider_no_text_box_horizontal_position, slider_no_text_box_vertical_position, slider_no_text_box_width, slider_no_text_box_height);
                points = new XPoint(slider_no_text_box_horizontal_position + (slider_no_text_box_width - 5), slider_no_text_box_vertical_position + (slider_no_text_box_height / 2));
                gfx.DrawString("Slider No:", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.CenterRight);

                string slider_Address = "";
                int number_of_sliders = 0;
                font_size = 6;
                if (kANBAN_MASTER.SLIDER_ADDRESS == null)
                {
                    slider_Address = "";
                }
                else
                {
                    slider_Address = kANBAN_MASTER.SLIDER_ADDRESS;
                    string[] addresses = slider_Address.Split(' ');
                    if (addresses.Length >= 5)
                    {

                        slider_Address = "";
                        for (int j = 0; j < addresses.Length; j++)
                        {
                            if (j % 2 == 0)
                            {
                                slider_Address = slider_Address + addresses[j] + "\t";
                            }
                            else
                            {
                                slider_Address = slider_Address + addresses[j] + System.Environment.NewLine;
                            }
                        }
                        number_of_sliders = addresses.Length;
                        font_size = 6;
                    }
                    else
                    {
                        slider_Address = "";
                        font_size = 8;
                        for (int j = 0; j < addresses.Length; j++)
                        {
                            slider_Address = slider_Address + addresses[j] + System.Environment.NewLine;
                        }
                        number_of_sliders = addresses.Length;
                    }

                }


                //Draw a box containing slider address value
                gfx.DrawRectangle(pen, slider_no_value_box_horizontal_position, slider_no_value_box_vertical_position, slider_no_value_box_width, slider_no_value_box_height);
                points = new XPoint(slider_no_value_box_horizontal_position + (slider_no_value_box_width / 2), slider_no_value_box_vertical_position + (slider_no_value_box_height / 2));
                if (number_of_sliders > 4)
                {
                    slider_no_value_box_height = slider_no_value_box_height / 1.5;
                    slider_no_value_box_width = slider_no_value_box_width / 1.5;
                    font_size = 5;
                }
                rect = new XRect(slider_no_value_box_horizontal_position + slider_no_value_box_width / 4, slider_no_value_box_vertical_position + slider_no_value_box_height / 4, slider_no_value_box_width, slider_no_value_box_height);
                tf.DrawString(slider_Address, new XFont("Arial", font_size, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                if (kanban_color != "SUB_ST")
                {
                    //Draw a box containing safety part text
                    gfx.DrawRectangle(pen, brush, safety_part_text_box_horizontal_position, safety_part_text_box_vertical_position, safety_part_text_box_width, safety_part_text_box_height);
                    points = new XPoint(safety_part_text_box_horizontal_position + 20, safety_part_text_box_vertical_position + (safety_part_text_box_height / 2));
                    gfx.DrawString("Safety Part:", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                    //Draw a box containing appr part text
                    gfx.DrawRectangle(pen, brush, appr_part_text_box_horizontal_position, appr_part_text_box_vertical_position, appr_part_text_box_width, appr_part_text_box_height);
                    points = new XPoint(appr_part_text_box_horizontal_position + (appr_part_text_box_width / 2), appr_part_text_box_vertical_position + (appr_part_text_box_height / 2));
                    gfx.DrawString("Appr Part", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                    //Draw a box containing high consump value
                    gfx.DrawRectangle(pen, high_consump_value_box_horizontal_position, high_consump_value_box_vertical_position, high_consump_value_box_width, high_consump_value_box_height);
                    points = new XPoint(high_consump_value_box_horizontal_position + (high_consump_value_box_width / 2), high_consump_value_box_vertical_position + (high_consump_value_box_height / 2));
                    gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                    //Draw a box containing High consump texyt
                    gfx.DrawRectangle(pen, brush, high_consump_text_box_horizontal_position, high_consump_text_box_vertical_position, high_consump_text_box_width, high_consump_text_box_height);
                    points = new XPoint(high_consump_text_box_horizontal_position + (high_consump_text_box_width / 2), high_consump_text_box_vertical_position + (appr_part_text_box_height / 2));
                    gfx.DrawString("High Consump", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                    //Draw a box containing safetypart value text
                    gfx.DrawRectangle(pen, safety_part_value_box_horizontal_position, safety_part_value_box_vertical_position, safety_part_value_box_width, safety_part_value_box_height);
                    points = new XPoint(safety_part_value_box_horizontal_position + (safety_part_value_box_width / 2), safety_part_value_box_vertical_position + (safety_part_value_box_height / 2));
                    gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                    //Draw a box containing app part value text
                    gfx.DrawRectangle(pen, appr_part_value_box_horizontal_position, appr_part_value_box_vertical_position, appr_part_value_box_width, appr_part_value_box_height);
                    points = new XPoint(appr_part_value_box_horizontal_position + (appr_part_value_box_width / 2), appr_part_value_box_vertical_position + (appr_part_value_box_height / 2));
                    gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                    //Draw a box containing app part value text
                    gfx.DrawRectangle(pen, appr_part_value_box_horizontal_position, appr_part_value_box_vertical_position, appr_part_value_box_width, appr_part_value_box_height);
                    points = new XPoint(appr_part_value_box_horizontal_position + (appr_part_value_box_width / 2), appr_part_value_box_vertical_position + (appr_part_value_box_height / 2));
                    gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);


                }
                else
                {
                    //Draw a box containing Kitting Slider
                    gfx.DrawRectangle(pen, brush, kitting_slider_horizontal_position, safety_part_text_box_vertical_position, kitting_slider_width, kitting_slider_height);

                    points = new XPoint(kitting_slider_horizontal_position + (kitting_slider_width / 2), safety_part_text_box_vertical_position + (kitting_slider_height / 2));
                    gfx.DrawString("Kitting Slider", new XFont("Arial", 7), XBrushes.Black, points, XStringFormats.Center);


                    //Draw a box containing Kitting Slider value
                    gfx.DrawRectangle(pen, kitting_slider_value_horizontal_position, safety_part_text_box_vertical_position, kitting_slider_value_width, kitting_slider_value_height);

                    points = new XPoint(kitting_slider_value_horizontal_position + (kitting_slider_value_width / 2), safety_part_text_box_vertical_position + (kitting_slider_value_height / 2));
                    gfx.DrawString(kANBAN_MASTER.KITTING_SLIDER, new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);
                }


                //creates 3 pieces of kanban before going next line
                col_count++;


                kanban_per_page_count++;
                //set horizontal position
                box_horizontal_position = box_horizontal_position + 30 + width;
                barcode_box_horizontal_position = barcode_box_horizontal_position + 30 + width;
                bacode_horizontal_position = bacode_horizontal_position + 30 + width;
                pn_title_box_horizontal_position = pn_title_box_horizontal_position + 30 + width;
                des_title_box_horizontal_position = des_title_box_horizontal_position + 30 + width;
                qty_title_box_horizontal_position = qty_title_box_horizontal_position + 30 + width;
                pn_text_box_horizontal_position = pn_text_box_horizontal_position + 30 + width;
                pn_des_box_horizontal_position = pn_des_box_horizontal_position + 30 + width;
                supermarket_section_horizontal_position = supermarket_section_horizontal_position + 30 + width;
                qty_value_box_horizontal_position = qty_value_box_horizontal_position + 30 + width;
                bintype_text_box_horizontal_position = bintype_text_box_horizontal_position + 30 + width;
                bintype_value_box_horizontal_position = bintype_value_box_horizontal_position + 30 + width;
                line_text_box_horizontal_position = line_text_box_horizontal_position + 30 + width;
                plannercode_text_box_horizontal_position = plannercode_text_box_horizontal_position + 30 + width;
                line_value_box_horizontal_position = line_value_box_horizontal_position + 30 + width;
                slider_no_text_box_horizontal_position = slider_no_text_box_horizontal_position + 30 + width;
                safety_part_text_box_horizontal_position = safety_part_text_box_horizontal_position + 30 + width;
                appr_part_text_box_horizontal_position = appr_part_text_box_horizontal_position + 30 + width;
                high_consump_text_box_horizontal_position = high_consump_text_box_horizontal_position + 30 + width;
                slider_no_value_box_horizontal_position = slider_no_value_box_horizontal_position + 30 + width;
                kanbancard_text_box_horizontal_position = kanbancard_text_box_horizontal_position + 30 + width;
                safety_part_value_box_horizontal_position = safety_part_value_box_horizontal_position + 30 + width;
                appr_part_value_box_horizontal_position = appr_part_value_box_horizontal_position + 30 + width;
                high_consump_value_box_horizontal_position = high_consump_value_box_horizontal_position + 30 + width;
                kanbancard_value_box_one_horizontal_position = kanbancard_value_box_one_horizontal_position + 30 + width;
                kanbancard_value_box_two_horizontal_position = kanbancard_value_box_two_horizontal_position + 30 + width;
                model_text_horizontal_position = model_text_horizontal_position + 30 + width;
                revision_text_horizontal_position = width + 30 + revision_text_horizontal_position;
                kanban_image_horizontal_position = width + kanban_image_horizontal_position + 30;
                kitting_slider_horizontal_position = width + kitting_slider_horizontal_position + 30;
                kitting_slider_value_horizontal_position = width + 30 + kitting_slider_value_horizontal_position;

            }

            return document;
        }

        public PdfDocument create_Store_kanban(KANBAN_MASTER kANBAN_MASTER)
        {
            int store_Qty = kANBAN_MASTER.STORE_KANBAN_QTY;

            //set kanban box sizes
            double width = XUnit.FromMillimeter(79);
            double height = XUnit.FromMillimeter(59);
            double barcode_box_height = XUnit.FromMillimeter(15);
            double barcode_box_width = XUnit.FromMillimeter(15);
            double pn_title_box_height = XUnit.FromMillimeter(5);
            double pn_title_box_width = XUnit.FromMillimeter(7);
            double des_title_box_height = XUnit.FromMillimeter(5);
            double des_title_box_width = XUnit.FromMillimeter(7);
            double qty_title_box_height = XUnit.FromMillimeter(5);
            double qty_title_box_width = XUnit.FromMillimeter(7);
            double pn_text_box_height = XUnit.FromMillimeter(5);
            double pn_text_box_width = XUnit.FromMillimeter(47);
            double supermarket_section_height = XUnit.FromMillimeter(10);
            double supermarket_section_width = XUnit.FromMillimeter(10);
            double qty_value_box_height = XUnit.FromMillimeter(5);
            double qty_value_box_width = XUnit.FromMillimeter(19); //(pn_text_box_width + supermarket_section_width) / 3;
            double bintype_text_box_height = XUnit.FromMillimeter(5); ;
            double bintype_text_box_width = XUnit.FromMillimeter(19); //(pn_text_box_width + supermarket_section_width) / 3;
            double bintype_value_box_height = XUnit.FromMillimeter(5); //XUnit.FromCentimeter(0.5);
            double bintype_value_box_width = XUnit.FromMillimeter(19); //(pn_text_box_width + supermarket_section_width) / 3;
            double line_text_box_height = XUnit.FromMillimeter(11.5); //pn_title_box_height; // copy P/N box height
            double line_text_box_width = XUnit.FromMillimeter(15); //barcode_box_width; //barcode box width
            double line_value_box_height = XUnit.FromMillimeter(11.5); //pn_title_box_height * 2; // double of pn title text box
            double line_value_box_width = XUnit.FromMillimeter(15); //XUnit.FromCentimeter(1.4);
            double slider_no_text_box_width = XUnit.FromMillimeter(15); //plannercode_text_box_width;
            double slider_no_text_box_height = XUnit.FromMillimeter(16); //XUnit.FromCentimeter(1.7);
            double slider_no_value_box_width = XUnit.FromMillimeter(15); //line_value_box_width;
            double slider_no_value_box_height = XUnit.FromMillimeter(16);//XUnit.FromCentimeter(1.7);
            double kanbancard_text_box_height = XUnit.FromMillimeter(4.5); //high_consump_text_box_height;
            double kanbancard_text_box_width = XUnit.FromMillimeter(15); //high_consump_text_box_width;
            double kanbancard_value_box_one_width = XUnit.FromMillimeter(7.5);
            double kanbancard_value_box_one_height = XUnit.FromMillimeter(4.5);
            double kanbancard_value_box_two_width = XUnit.FromMillimeter(7.5);
            double kanbancard_value_box_two_height = XUnit.FromMillimeter(4.5);
            double kitting_slider_width = XUnit.FromMillimeter(15);
            double kitting_slider_height = XUnit.FromMillimeter(12);
            double kitting_slider_value_width = XUnit.FromMillimeter(15);
            double kitting_slider_value_height = XUnit.FromMillimeter(12);

            int kanban_image_height = 145;
            int kanban_image_width = 175;

            int col_count = 0;
            int kanban_per_page_count = 1;
            int kanban_of_number = 1;
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Size = PdfSharp.PageSize.A4;



            if (kANBAN_MASTER.PART_NAME.Length > 20)
            {
                kANBAN_MASTER.PART_NAME = kANBAN_MASTER.PART_NAME.Remove(20, kANBAN_MASTER.PART_NAME.Length - 20);
            }

            //Set kanban boxes  position
            double box_horizontal_position = 20.0;
            double barcode_box_horizontal_position = 20.0;
            double pn_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
            double bacode_horizontal_position = 23.0;
            double des_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
            double qty_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
            double pn_text_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
            double pn_des_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
            double supermarket_section_horizontal_position = pn_text_box_horizontal_position + pn_text_box_width;
            double qty_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width + qty_title_box_width;
            double bintype_text_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
            double bintype_value_box_horizontal_position = bintype_text_box_horizontal_position + bintype_text_box_width;
            double line_text_box_horizontal_position = 20.0;
            double plannercode_text_box_horizontal_position = 20.0;
            double line_value_box_horizontal_position = line_text_box_horizontal_position + line_text_box_width;
            double slider_no_text_box_horizontal_position = 20.0;
            double safety_part_text_box_horizontal_position = 20.0;
            double appr_part_text_box_horizontal_position = 20.0;
            double high_consump_text_box_horizontal_position = 20.0;
            double slider_no_value_box_horizontal_position = slider_no_text_box_horizontal_position + slider_no_text_box_width;
            double kanbancard_text_box_horizontal_position = 20.0;
            double kanbancard_value_box_one_horizontal_position = kanbancard_text_box_horizontal_position + kanbancard_text_box_width;
            double kanbancard_value_box_two_horizontal_position = kanbancard_value_box_one_horizontal_position + kanbancard_value_box_one_width;
            double model_text_horizontal_position = kanbancard_value_box_two_horizontal_position + kanbancard_value_box_two_width;
            double revision_text_horizontal_position = width;
            double kanban_image_horizontal_position = slider_no_value_box_horizontal_position + slider_no_value_box_width + 3;
            double kitting_slider_horizontal_position = 20.0;
            double kitting_slider_value_horizontal_position = kitting_slider_horizontal_position + kitting_slider_width;

            double box_vertical_position = 20.0;
            double barcode_box_vertical_position = 20.0;
            double bacode_vertical_position = 23.0;
            double pn_title_box_vertical_position = 20.0;
            double des_title_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height;
            double qty_title_box_vertical_positon = des_title_box_vertical_position + des_title_box_height;
            double pn_text_box_vertical_position = 20;
            double pn_des_box_vertical_position = pn_text_box_vertical_position + pn_text_box_height;
            double supermarket_section_vertical_position = 20;
            double qty_value_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height + des_title_box_height;
            double bintype_text_box_vertical_position = qty_value_box_vertical_position;
            double bintype_value_box_vertical_position = bintype_text_box_vertical_position;
            double line_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
            double plannercode_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_text_box_height;
            double line_value_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
            double slider_no_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_value_box_height;
            double slider_no_value_box_vertical_position = line_value_box_vertical_position + line_value_box_height;
            double kanban_image_vertical_position = bintype_text_box_vertical_position + bintype_text_box_height + 3;
            double kitting_slider_vertical_position = slider_no_text_box_vertical_position + slider_no_text_box_height;
            double kanbancard_text_box_vertical_position = kitting_slider_vertical_position + kitting_slider_height;
            double kanbancard_value_box_one_vertical_position = kanbancard_text_box_vertical_position;
            double kanbancard_value_box_two_vertical_position = kanbancard_text_box_vertical_position;
            double model_text_vertical_position = kanbancard_value_box_two_vertical_position + (kanbancard_value_box_two_height / 2);
            double revision_text_vertical_position = model_text_vertical_position;
            double kitting_slider_value_vertical_position = slider_no_value_box_vertical_position + slider_no_value_box_height;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XColor kanbanColor = XColor.FromArgb(192, 215, 155);
            string process = kANBAN_MASTER.PROCESS;

            if (process.Contains("H0-SK") || process == "H0-SK")
            {
                kanbanColor = XColor.FromArgb(255, 102, 0);
            }
            else if (process.Contains("SUB") || process == "SUB" || process.Contains("SUBWORK") || process == "SUBWORK")
            {
                kanbanColor = XColor.FromArgb(195, 195, 195);
            }
            else if (process == "A0")
            {
                kanbanColor = XColor.FromArgb(241, 182, 255);
            }
            else if (process.Contains("A0-J") || process == "A0-J" || process.Contains("H0-J") || process == "H0-J")
            {
                kanbanColor = XColor.FromArgb(255, 112, 217);
            }

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options.Height = 155;
            QCwriter.Options.Width = 155;
            QCwriter.Options.Margin = 0;
            var result = QCwriter.Write(kANBAN_MASTER.PART_NO);
            var barcodeBitmap = new Bitmap(result);
            barcodeBitmap.SetResolution(300.0F, 300.0F);

            for (int i = 0; i < store_Qty; i++)
            {
                /*
                line_value_box_height = pn_title_box_height * 2;
                line_value_box_width = XUnit.FromCentimeter(1.4);

                slider_no_value_box_width = line_value_box_width;
                slider_no_value_box_height = XUnit.FromCentimeter(1.7);

                slider_no_value_box_height = XUnit.FromCentimeter(1.7); */
                if (kanban_per_page_count == 10)
                {
                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    page.Size = PdfSharp.PageSize.A4;
                    gfx = XGraphics.FromPdfPage(page);
                    kanban_per_page_count = 1;
                    col_count = 0;
                    //reset both position
                    box_horizontal_position = 20.0;
                    box_horizontal_position = 20.0;
                    barcode_box_horizontal_position = 20.0;
                    bacode_horizontal_position = 23.0;
                    pn_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    des_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    qty_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    pn_text_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    pn_des_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    supermarket_section_horizontal_position = pn_text_box_horizontal_position + pn_text_box_width;
                    qty_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width + qty_title_box_width;
                    bintype_text_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
                    bintype_value_box_horizontal_position = bintype_text_box_horizontal_position + bintype_text_box_width;
                    line_text_box_horizontal_position = 20.0;
                    plannercode_text_box_horizontal_position = 20.0;
                    line_value_box_horizontal_position = line_text_box_horizontal_position + line_text_box_width;
                    slider_no_text_box_horizontal_position = 20.0;
                    safety_part_text_box_horizontal_position = 20.0;
                    appr_part_text_box_horizontal_position = 20.0;
                    high_consump_text_box_horizontal_position = 20.0;
                    slider_no_value_box_horizontal_position = slider_no_text_box_horizontal_position + slider_no_text_box_width;
                    kitting_slider_horizontal_position = 20.0;
                    kanbancard_text_box_horizontal_position = 20.0;
                    kanbancard_value_box_one_horizontal_position = kanbancard_text_box_horizontal_position + kanbancard_text_box_width;
                    kanbancard_value_box_two_horizontal_position = kanbancard_value_box_one_horizontal_position + kanbancard_value_box_one_width;
                    model_text_horizontal_position = kanbancard_value_box_two_horizontal_position + kanbancard_value_box_two_width;
                    revision_text_horizontal_position = width;
                    kanban_image_horizontal_position = slider_no_value_box_horizontal_position + slider_no_value_box_width + 3;
                    kitting_slider_value_horizontal_position = kitting_slider_horizontal_position + kitting_slider_width;

                    box_vertical_position = 20.0;
                    barcode_box_vertical_position = 20.0;
                    bacode_vertical_position = 23.0;
                    pn_title_box_vertical_position = 20.0;
                    des_title_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height;
                    qty_title_box_vertical_positon = des_title_box_vertical_position + des_title_box_height;
                    pn_text_box_vertical_position = 20;
                    pn_des_box_vertical_position = pn_text_box_vertical_position + pn_text_box_height;
                    supermarket_section_vertical_position = 20;
                    qty_value_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height + des_title_box_height;
                    bintype_text_box_vertical_position = qty_value_box_vertical_position;
                    bintype_value_box_vertical_position = bintype_text_box_vertical_position;
                    line_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
                    plannercode_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_text_box_height;
                    line_value_box_vertical_position = barcode_box_vertical_position + barcode_box_height;
                    slider_no_text_box_vertical_position = barcode_box_vertical_position + barcode_box_height + line_value_box_height;
                    slider_no_value_box_vertical_position = line_value_box_vertical_position + line_value_box_height;
                    kanban_image_vertical_position = bintype_text_box_vertical_position + bintype_text_box_height + 3;
                    kitting_slider_vertical_position = slider_no_text_box_vertical_position + slider_no_text_box_height;
                    kanbancard_text_box_vertical_position = kitting_slider_vertical_position + kitting_slider_height;
                    kanbancard_value_box_one_vertical_position = kanbancard_text_box_vertical_position;
                    kanbancard_value_box_two_vertical_position = kanbancard_text_box_vertical_position;
                    model_text_vertical_position = kanbancard_value_box_two_vertical_position + (kanbancard_value_box_two_height / 2);
                    revision_text_vertical_position = model_text_vertical_position;
                    kitting_slider_value_vertical_position = slider_no_value_box_vertical_position + slider_no_value_box_height;

                }

                if (col_count == 3)
                {
                    //reset horizontal position
                    box_horizontal_position = 20.0;
                    barcode_box_horizontal_position = 20.0;
                    bacode_horizontal_position = 23.0;
                    pn_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    des_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    qty_title_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width;
                    pn_text_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    pn_des_box_horizontal_position = barcode_box_horizontal_position + pn_title_box_horizontal_position;
                    supermarket_section_horizontal_position = pn_text_box_horizontal_position + pn_text_box_width;
                    qty_value_box_horizontal_position = barcode_box_horizontal_position + barcode_box_width + qty_title_box_width;
                    bintype_text_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
                    bintype_value_box_horizontal_position = bintype_text_box_horizontal_position + bintype_text_box_width;
                    line_text_box_horizontal_position = 20.0;
                    plannercode_text_box_horizontal_position = 20.0;
                    line_value_box_horizontal_position = line_text_box_width + line_text_box_horizontal_position;
                    slider_no_text_box_horizontal_position = 20.0;
                    safety_part_text_box_horizontal_position = 20.0;
                    appr_part_text_box_horizontal_position = 20.0;
                    high_consump_text_box_horizontal_position = 20.0;
                    slider_no_value_box_horizontal_position = slider_no_text_box_horizontal_position + slider_no_text_box_width;
                    kitting_slider_horizontal_position = 20.0;
                    kanbancard_text_box_horizontal_position = 20.0;
                    kanbancard_value_box_one_horizontal_position = kanbancard_text_box_horizontal_position + kanbancard_text_box_width;
                    kanbancard_value_box_two_horizontal_position = kanbancard_value_box_one_horizontal_position + kanbancard_value_box_one_width;
                    model_text_horizontal_position = kanbancard_value_box_two_horizontal_position + kanbancard_value_box_two_width;
                    revision_text_horizontal_position = width;
                    kanban_image_horizontal_position = slider_no_value_box_horizontal_position + slider_no_value_box_width + 3;
                    kitting_slider_value_horizontal_position = kitting_slider_horizontal_position + kitting_slider_width;

                    // set the position to duplicate the designs to other kanban cards vertically
                    box_vertical_position = box_vertical_position + 15 + height;
                    barcode_box_vertical_position = barcode_box_vertical_position + 15 + height;
                    bacode_vertical_position = bacode_vertical_position + 15 + height;
                    pn_title_box_vertical_position = pn_title_box_vertical_position + 15 + height;
                    des_title_box_vertical_position = des_title_box_vertical_position + 15 + height;
                    qty_title_box_vertical_positon = qty_title_box_vertical_positon + 15 + height;
                    pn_text_box_vertical_position = pn_text_box_vertical_position + 15 + height;
                    pn_des_box_vertical_position = pn_des_box_vertical_position + 15 + height;
                    supermarket_section_vertical_position = supermarket_section_vertical_position + 15 + height;
                    qty_value_box_vertical_position = qty_value_box_vertical_position + 15 + height;
                    bintype_text_box_vertical_position = bintype_text_box_vertical_position + 15 + height;
                    bintype_value_box_vertical_position = bintype_value_box_vertical_position + 15 + height;
                    line_text_box_vertical_position = line_text_box_vertical_position + 15 + height;
                    plannercode_text_box_vertical_position = plannercode_text_box_vertical_position + 15 + height;
                    line_value_box_vertical_position = line_value_box_vertical_position + 15 + height;
                    slider_no_text_box_vertical_position = slider_no_text_box_vertical_position + 15 + height;
                    slider_no_value_box_vertical_position = slider_no_value_box_vertical_position + 15 + height;
                    kitting_slider_vertical_position = kitting_slider_vertical_position + 15 + height;
                    kanbancard_text_box_vertical_position = kanbancard_text_box_vertical_position + 15 + height;
                    kanbancard_value_box_one_vertical_position = kanbancard_value_box_one_vertical_position + 15 + height;
                    kanbancard_value_box_two_vertical_position = kanbancard_value_box_two_vertical_position + 15 + height;
                    model_text_vertical_position = model_text_vertical_position + 15 + height;
                    revision_text_vertical_position = revision_text_vertical_position + 15 + height;
                    kanban_image_vertical_position = kanban_image_vertical_position + 15 + height;
                    kitting_slider_value_vertical_position = kitting_slider_value_vertical_position + 15 + height;


                    //reset count
                    col_count = 0;
                }


                XTextFormatter tf = new XTextFormatter(gfx);


                MemoryStream ms = new MemoryStream(kANBAN_MASTER.PHOTO);
                System.Drawing.Image images = new Bitmap(ms);

                XPen pen = new XPen(XColors.Black, 0.5);
                System.Drawing.Image imagex = barcodeBitmap;
                XImage img = XImage.FromGdiPlusImage(imagex);
                System.Drawing.Image photo = resizeImage(images, new Size(kanban_image_width, kanban_image_height));
                XImage pho = XImage.FromGdiPlusImage(photo);

                var bounds = new XRect(gfx.PageOrigin, gfx.PageSize);
                XPoint points = new XPoint(pn_title_box_horizontal_position + 10, pn_title_box_vertical_position + 7);




                XBrush brush = new XSolidBrush(kanbanColor);
                //draw boxes

                //draw card box
                gfx.DrawRectangle(pen, box_horizontal_position, box_vertical_position, width, height);

                //draw barcode box and barcode
                gfx.DrawRectangle(pen, barcode_box_horizontal_position, barcode_box_vertical_position, barcode_box_width, barcode_box_height);
                gfx.DrawImage(img, bacode_horizontal_position, bacode_vertical_position);
                gfx.DrawImage(pho, kanban_image_horizontal_position, kanban_image_vertical_position);
                string max_kanban_Qty = store_Qty.ToString();

                //Draw a box containing first kanban value
                gfx.DrawRectangle(pen, kanbancard_value_box_two_horizontal_position, kanbancard_value_box_two_vertical_position, kanbancard_value_box_two_width, kanbancard_value_box_two_height);
                points = new XPoint(kanbancard_value_box_two_horizontal_position + (kanbancard_value_box_two_width / 2), kanbancard_value_box_two_vertical_position + (kanbancard_value_box_two_height / 2));
                gfx.DrawString(max_kanban_Qty, new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                int add_pos = 0;
                if (kANBAN_MASTER.MODEL.Length > 8 && kANBAN_MASTER.MODEL.Length < 14)
                {
                    add_pos = 16;
                }
                else if (kANBAN_MASTER.MODEL.Length >= 14)
                {
                    add_pos = 26;
                }
                XPoint point_model_name = new XPoint(model_text_horizontal_position + 1, model_text_vertical_position);
                gfx.DrawString(kANBAN_MASTER.MODEL, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, point_model_name, XStringFormats.CenterLeft);

                XPoint revision_text_point = new XPoint(revision_text_horizontal_position - 1, revision_text_vertical_position);
                gfx.DrawString(kANBAN_MASTER.REVISION, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Red, revision_text_point, XStringFormats.Center);

                //Draw a box containing first kanban value
                gfx.DrawRectangle(pen, kanbancard_value_box_one_horizontal_position, kanbancard_value_box_one_vertical_position, kanbancard_value_box_one_width, kanbancard_value_box_one_height);
                points = new XPoint(kanbancard_value_box_one_horizontal_position + (kanbancard_value_box_one_width / 2), kanbancard_value_box_one_vertical_position + (kanbancard_value_box_one_height / 2));


                gfx.DrawString(kanban_of_number.ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
                kanban_of_number++;

                //Draw a box containing Kitting Slider
                gfx.DrawRectangle(pen, brush, kitting_slider_horizontal_position, kitting_slider_vertical_position, kitting_slider_width, kitting_slider_height);
                points = new XPoint(kitting_slider_horizontal_position + (kitting_slider_width / 2), kitting_slider_vertical_position + (kitting_slider_height / 2));
                gfx.DrawString("Kitting Slider", new XFont("Arial", 7), XBrushes.Black, points, XStringFormats.Center);


                //Draw a box containing Kitting Slider value
                gfx.DrawRectangle(pen, kitting_slider_value_horizontal_position, kitting_slider_value_vertical_position, kitting_slider_value_width, kitting_slider_value_height);
                points = new XPoint(kitting_slider_value_horizontal_position + (kitting_slider_value_width / 2), kitting_slider_value_vertical_position + (kitting_slider_value_height / 2));
                gfx.DrawString(kANBAN_MASTER.KITTING_SLIDER, new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);

                /*
                 *  //Draw a box containing high consump value
                gfx.DrawRectangle(pen, high_consump_value_box_horizontal_position, high_consump_value_box_vertical_position, high_consump_value_box_width, high_consump_value_box_height);
                points = new XPoint(high_consump_value_box_horizontal_position + (high_consump_value_box_width / 2), high_consump_value_box_vertical_position + (high_consump_value_box_height / 2));
                gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing app part value text
                gfx.DrawRectangle(pen, appr_part_value_box_horizontal_position, appr_part_value_box_vertical_position, appr_part_value_box_width, appr_part_value_box_height);
                points = new XPoint(appr_part_value_box_horizontal_position + (appr_part_value_box_width / 2), appr_part_value_box_vertical_position + (appr_part_value_box_height / 2));
                gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing app part value text
                gfx.DrawRectangle(pen, appr_part_value_box_horizontal_position, appr_part_value_box_vertical_position, appr_part_value_box_width, appr_part_value_box_height);
                points = new XPoint(appr_part_value_box_horizontal_position + (appr_part_value_box_width / 2), appr_part_value_box_vertical_position + (appr_part_value_box_height / 2));
                gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing safetypart value text
                gfx.DrawRectangle(pen, safety_part_value_box_horizontal_position, safety_part_value_box_vertical_position, safety_part_value_box_width, safety_part_value_box_height);
                points = new XPoint(safety_part_value_box_horizontal_position + (safety_part_value_box_width / 2), safety_part_value_box_vertical_position + (safety_part_value_box_height / 2));
                gfx.DrawString("", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);
                
                //Draw a box containing planner code description
                gfx.DrawRectangle(pen, brush, plannercode_text_box_horizontal_position, plannercode_text_box_vertical_position, plannercode_text_box_width, plannercode_text_box_height);
                points = new XPoint(plannercode_text_box_horizontal_position + 20, plannercode_text_box_vertical_position + (plannercode_text_box_height / 2));
                gfx.DrawString("Planner code.", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing safety part text
                gfx.DrawRectangle(pen, brush, safety_part_text_box_horizontal_position, safety_part_text_box_vertical_position, safety_part_text_box_width, safety_part_text_box_height);
                points = new XPoint(safety_part_text_box_horizontal_position + 20, safety_part_text_box_vertical_position + (safety_part_text_box_height / 2));
                gfx.DrawString("Safety Part:", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing appr part text
                gfx.DrawRectangle(pen, brush, appr_part_text_box_horizontal_position, appr_part_text_box_vertical_position, appr_part_text_box_width, appr_part_text_box_height);
                points = new XPoint(appr_part_text_box_horizontal_position + 1, appr_part_text_box_vertical_position + (appr_part_text_box_height / 2));
                gfx.DrawString("Appr Part", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.BaseLineLeft);


                //Draw a box containing High consump texyt
                gfx.DrawRectangle(pen, brush, high_consump_text_box_horizontal_position, high_consump_text_box_vertical_position, high_consump_text_box_width, high_consump_text_box_height);
                points = new XPoint(high_consump_text_box_horizontal_position + (high_consump_text_box_width / 2), high_consump_text_box_vertical_position + (appr_part_text_box_height / 2));
                gfx.DrawString("High Consump", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);
                
                 */
                //Draw a box containing kanbancard text
                gfx.DrawRectangle(pen, brush, kanbancard_text_box_horizontal_position, kanbancard_text_box_vertical_position, kanbancard_text_box_width, kanbancard_text_box_height);
                points = new XPoint(kanbancard_text_box_horizontal_position + 1, kanbancard_text_box_vertical_position + (kanbancard_text_box_height / 2));
                gfx.DrawString("Kanban Card", new XFont("Arial", 7), XBrushes.Black, points, XStringFormats.CenterLeft);

                //design kanban
                //draw the P/N box with text
                gfx.DrawRectangle(pen, brush, pn_title_box_horizontal_position, pn_title_box_vertical_position, pn_title_box_width, pn_title_box_height);
                points = new XPoint(pn_title_box_horizontal_position + 1, pn_title_box_vertical_position + (pn_title_box_height / 2));
                gfx.DrawString("P/N", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.CenterLeft);

                //Draw the Des box with text
                gfx.DrawRectangle(pen, brush, des_title_box_horizontal_position, des_title_box_vertical_position, des_title_box_width, des_title_box_height);
                points = new XPoint(des_title_box_horizontal_position + 1, des_title_box_vertical_position + (des_title_box_height / 2));
                gfx.DrawString("Des", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.CenterLeft);

                //Draw Qty : box with text
                gfx.DrawRectangle(pen, brush, qty_title_box_horizontal_position, qty_title_box_vertical_positon, qty_title_box_width, qty_title_box_height);
                points = new XPoint(qty_title_box_horizontal_position + 1, qty_title_box_vertical_positon + (qty_title_box_height / 2));
                gfx.DrawString("Qty:", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.CenterLeft);

                //Draw a box containing the part number of the kanban
                gfx.DrawRectangle(pen, brush, pn_text_box_horizontal_position, pn_text_box_vertical_position, pn_text_box_width, pn_text_box_height);
                //points = new XPoint(pn_text_box_horizontal_position + (pn_text_box_width / 2) - 5, pn_text_box_vertical_position + (pn_text_box_height / 2));
                points = new XPoint(pn_text_box_horizontal_position + 1, pn_text_box_vertical_position + pn_text_box_height / 2);
                gfx.DrawString(kANBAN_MASTER.PART_NO, new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.CenterLeft);



                //Draw a box containing the part number description
                gfx.DrawRectangle(pen, brush, pn_des_box_horizontal_position, pn_des_box_vertical_position, pn_text_box_width, pn_text_box_height);
                points = new XPoint(pn_des_box_horizontal_position + 1, pn_des_box_vertical_position + (pn_text_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.PART_NAME, new XFont("Arial", 11), XBrushes.Black, points, XStringFormats.CenterLeft);

                string slider_add_part = "";
                double slider_address_height_addition = (supermarket_section_height / 2);

                if (string.IsNullOrEmpty(kANBAN_MASTER.SLIDER_ADDRESS) == false)
                {
                    try
                    {
                        slider_add_part = kANBAN_MASTER.SLIDER_ADDRESS.Substring(0, 2);
                    }
                    catch
                    {
                        slider_add_part = "N/A";
                    }


                }
                else
                {
                    slider_add_part = "";
                }

                //Draw a box containing the quantity description
                gfx.DrawRectangle(pen, brush, supermarket_section_horizontal_position, supermarket_section_vertical_position, supermarket_section_width, supermarket_section_height);
                points = new XPoint(supermarket_section_horizontal_position + (supermarket_section_width / 2), supermarket_section_vertical_position + slider_address_height_addition);
                gfx.DrawString(slider_add_part, new XFont("Arial", 20, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.Center);


                //Draw a box containing the quantity
                gfx.DrawRectangle(pen, brush, qty_value_box_horizontal_position, qty_value_box_vertical_position, qty_value_box_width, qty_value_box_height);
                points = new XPoint(qty_value_box_horizontal_position + (qty_value_box_width / 2), qty_value_box_vertical_position + (qty_value_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.QTY_PER_BIN.ToString(), new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing the bin tyoe description
                gfx.DrawRectangle(pen, brush, bintype_text_box_horizontal_position, bintype_text_box_vertical_position, bintype_text_box_width, bintype_text_box_height);
                points = new XPoint(bintype_text_box_horizontal_position + (bintype_text_box_width / 2), bintype_text_box_vertical_position + (bintype_text_box_height / 2));
                gfx.DrawString("Bin type", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing the bin type
                gfx.DrawRectangle(pen, brush, bintype_value_box_horizontal_position, bintype_value_box_vertical_position, bintype_value_box_width, bintype_value_box_height);
                points = new XPoint(bintype_value_box_horizontal_position + (bintype_value_box_width / 2), bintype_value_box_vertical_position + (bintype_value_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.BIN_TYPE, new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);

                //Draw a box containing line title description
                gfx.DrawRectangle(pen, brush, line_text_box_horizontal_position, line_text_box_vertical_position, line_text_box_width, line_text_box_height);
                points = new XPoint(line_text_box_horizontal_position + 1, line_text_box_vertical_position + (line_text_box_height / 2));
                gfx.DrawString("Line : ", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.CenterLeft);


                int number_of_lines = 0;
                string line_string = "";
                int font_size = 12;
                if (kANBAN_MASTER.LINE == null)
                {
                    line_string = "";
                }
                else
                {
                    line_string = kANBAN_MASTER.LINE;

                    if (line_string.Length > 13)
                    {
                        font_size = 8;
                        string[] lineses = line_string.Split(' ');
                        line_string = "";
                        lineses = lineses.Where(x => x != "").ToArray();
                        if (lineses.Length > 7)
                        {
                            font_size = 6;
                        }
                        else if (lineses.Length < 7 && lineses.Length < 2)
                        {
                            font_size = 9;
                        }

                        for (int j = 0; j < lineses.Length; j++)
                        {
                            //line_string = line_string + lineses[j] + System.Environment.NewLine;
                            if (j % 2 != 0)
                            {
                                line_string = line_string + lineses[j] + System.Environment.NewLine;
                            }
                            else
                            {
                                line_string = line_string + lineses[j] + "\t";

                            }

                        }
                    }
                    number_of_lines = line_string.Length;
                }
                XStringFormat format = new XStringFormat();
                format.Alignment = XStringAlignment.Center;

                //Draw a box containing slider no value
                gfx.DrawRectangle(pen, line_value_box_horizontal_position, line_value_box_vertical_position, line_value_box_width, line_value_box_height);
                //points = new XPoint(line_value_box_horizontal_position + 20, line_value_box_vertical_position + (line_value_box_height / 2));
                /*
                if (number_of_lines > 4)
                {
                    line_value_box_height = line_value_box_height / 1.5;
                    line_value_box_width = line_value_box_width - 2.5;
                    font_size = 5;
                }*/
                XRect rect = new XRect(line_value_box_horizontal_position + (line_value_box_width / 14), line_value_box_vertical_position + (line_value_box_height / 8), line_value_box_width, line_value_box_height);
                //XRect rect = new XRect(line_value_box_horizontal_position, line_value_box_vertical_position + (line_value_box_height / 10), line_value_box_width, line_value_box_height);

                tf.DrawString(line_string, new XFont("Arial", font_size), XBrushes.Black, rect, XStringFormats.TopLeft);


                //Draw a box containing slider no: text
                gfx.DrawRectangle(pen, brush, slider_no_text_box_horizontal_position, slider_no_text_box_vertical_position, slider_no_text_box_width, slider_no_text_box_height);
                points = new XPoint(slider_no_text_box_horizontal_position, slider_no_text_box_vertical_position + (slider_no_text_box_height / 2));
                gfx.DrawString("Slider No:", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.CenterLeft);


                string slider_Address = "";
                int number_of_sliders = 0;
                double slider_vertical_Address = slider_no_value_box_vertical_position;

                font_size = 12;
                if (kANBAN_MASTER.SLIDER_ADDRESS == null)
                {
                    slider_Address = "";
                }
                else
                {
                    slider_Address = kANBAN_MASTER.SLIDER_ADDRESS;
                    string[] addresses = slider_Address.Split(' ');
                    if (addresses.Length >= 3)
                    {
                        font_size = 8;
                        slider_Address = "";
                        for (int j = 0; j < addresses.Length; j++)
                        {
                            if (j % 2 == 0)
                            {
                                slider_Address = slider_Address + addresses[j] + "\t";
                            }
                            else
                            {
                                slider_Address = slider_Address + addresses[j] + System.Environment.NewLine;
                            }
                        }
                        number_of_sliders = addresses.Length;
                        slider_vertical_Address = slider_vertical_Address + slider_no_value_box_height / 8;
                    }
                    else
                    {
                        slider_vertical_Address = slider_vertical_Address + slider_no_value_box_height / 4;
                        slider_Address = "";
                        for (int j = 0; j < addresses.Length; j++)
                        {
                            slider_Address = slider_Address + addresses[j] + System.Environment.NewLine;
                        }
                        number_of_sliders = addresses.Length;
                    }

                }


                //Draw a box containing slider address value
                gfx.DrawRectangle(pen, slider_no_value_box_horizontal_position, slider_no_value_box_vertical_position, slider_no_value_box_width, slider_no_value_box_height);
                points = new XPoint(slider_no_value_box_horizontal_position + (slider_no_value_box_width / 2), slider_no_value_box_vertical_position + (slider_no_value_box_height / 2));
                /*
                if (number_of_sliders > 4)
                {
                    slider_no_value_box_height = slider_no_value_box_height / 1.5;
                    slider_no_value_box_width = slider_no_value_box_width / 1.5;
                    font_size = 5;
                }
                */

                rect = new XRect(slider_no_value_box_horizontal_position + (slider_no_value_box_width / 12), slider_vertical_Address, slider_no_value_box_width, slider_no_value_box_height);
                tf.DrawString(slider_Address, new XFont("Arial", font_size, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                //gfx.DrawString(slider_Address, new XFont("Arial", font_size, XFontStyle.Bold), brush, rect, format);

                //creates 3 pieces of kanban before going next line
                col_count++;


                kanban_per_page_count++;
                //set horizontal position
                box_horizontal_position = box_horizontal_position + 30 + width;
                barcode_box_horizontal_position = barcode_box_horizontal_position + 30 + width;
                bacode_horizontal_position = bacode_horizontal_position + 30 + width;
                pn_title_box_horizontal_position = pn_title_box_horizontal_position + 30 + width;
                des_title_box_horizontal_position = des_title_box_horizontal_position + 30 + width;
                qty_title_box_horizontal_position = qty_title_box_horizontal_position + 30 + width;
                pn_text_box_horizontal_position = pn_text_box_horizontal_position + 30 + width;
                pn_des_box_horizontal_position = pn_des_box_horizontal_position + 30 + width;
                supermarket_section_horizontal_position = supermarket_section_horizontal_position + 30 + width;
                qty_value_box_horizontal_position = qty_value_box_horizontal_position + 30 + width;
                bintype_text_box_horizontal_position = bintype_text_box_horizontal_position + 30 + width;
                bintype_value_box_horizontal_position = bintype_value_box_horizontal_position + 30 + width;
                line_text_box_horizontal_position = line_text_box_horizontal_position + 30 + width;
                plannercode_text_box_horizontal_position = plannercode_text_box_horizontal_position + 30 + width;
                line_value_box_horizontal_position = line_value_box_horizontal_position + 30 + width;
                slider_no_text_box_horizontal_position = slider_no_text_box_horizontal_position + 30 + width;
                safety_part_text_box_horizontal_position = safety_part_text_box_horizontal_position + 30 + width;
                appr_part_text_box_horizontal_position = appr_part_text_box_horizontal_position + 30 + width;
                high_consump_text_box_horizontal_position = high_consump_text_box_horizontal_position + 30 + width;
                slider_no_value_box_horizontal_position = slider_no_value_box_horizontal_position + 30 + width;
                kitting_slider_horizontal_position = width + kitting_slider_horizontal_position + 30;
                kanbancard_text_box_horizontal_position = kanbancard_text_box_horizontal_position + 30 + width;
                kanbancard_value_box_one_horizontal_position = kanbancard_value_box_one_horizontal_position + 30 + width;
                kanbancard_value_box_two_horizontal_position = kanbancard_value_box_two_horizontal_position + 30 + width;
                model_text_horizontal_position = model_text_horizontal_position + 30 + width;
                revision_text_horizontal_position = width + 30 + revision_text_horizontal_position;
                kanban_image_horizontal_position = width + kanban_image_horizontal_position + 30;
                kitting_slider_value_horizontal_position = width + 30 + kitting_slider_value_horizontal_position;
            }


            return document;
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            return (System.Drawing.Image)(new Bitmap(imgToResize, size));
        }

        public PdfDocument create_Store_kanban_Large(KANBAN_MASTER kANBAN_MASTER,List<BSBINDATA> bdt)
        {
            int store_Qty = 0;

            if (bdt.Count > 0)
            {
                store_Qty = bdt.Count;
            }
            else
            {
                store_Qty = kANBAN_MASTER.STORE_KANBAN_QTY;
            }


            int kanban_image_height = 200;
            int kanban_image_width = 300;

            int col_count = 0;
            int kanban_per_page_count = 0;
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Size = PdfSharp.PageSize.A4;



            //set kanban box sizes
            double width = XUnit.FromMillimeter(119);
            double barcode_box_height = XUnit.FromMillimeter(29);
            double pn_title_box_height = XUnit.FromMillimeter(7);
            double des_title_box_height = XUnit.FromMillimeter(7);
            double des_text_box_height = XUnit.FromMillimeter(7);
            double qty_title_box_height = XUnit.FromMillimeter(7);
            double pn_text_box_height = XUnit.FromMillimeter(7);
            double bin_number_value_box_height = XUnit.FromMillimeter(7);
            double bin_number_barcode_box_height = des_text_box_height + qty_title_box_height;
            double qty_value_box_height = XUnit.FromMillimeter(7);
            double bin_type_text_box_height = XUnit.FromMillimeter(7);
            double bin_type_value_box_height = XUnit.FromMillimeter(7);
            double part_number_barcode_box_height = XUnit.FromMillimeter(26.5);
            double slider_number_title_box_height = XUnit.FromMillimeter(17.5);
            double slider_number_value_box_height = XUnit.FromMillimeter(17.5);
            double line_title_box_height = XUnit.FromMillimeter(7);
            double line_value_box_height = XUnit.FromMillimeter(7);
            double supplier_text_box_height = XUnit.FromMillimeter(7);
            double supplier_value_box_height = XUnit.FromMillimeter(7);
            double kanban_card_title_box_height = XUnit.FromMillimeter(7);
            double kanban_card_value_box_1_height = XUnit.FromMillimeter(7);
            double kanban_card_value_box_2_height = XUnit.FromMillimeter(7);

            double height = XUnit.FromMillimeter(86.3);
            double barcode_box_width = XUnit.FromMillimeter(29);
            double pn_title_box_width = XUnit.FromMillimeter(16);
            double des_title_box_width = XUnit.FromMillimeter(16);
            double qty_title_box_width = XUnit.FromMillimeter(16);
            double pn_text_box_width = XUnit.FromMillimeter(84.9);
            double des_text_box_width = pn_text_box_width;
            double bin_number_value_box_width = width - pn_text_box_width - pn_title_box_width;
            double bin_number_barcode_box_width = bin_number_value_box_width;
            double qty_value_box_width = XUnit.FromMillimeter(14.9);
            double bin_type_text_box_width = XUnit.FromMillimeter(35);
            double bin_type_value_box_width = XUnit.FromMillimeter(35);
            double part_number_barcode_box_width = XUnit.FromMillimeter(29.5);
            double slider_number_title_box_width = XUnit.FromMillimeter(29.5 / 2);
            double slider_number_value_box_width = XUnit.FromMillimeter(29.5 / 2);
            double line_title_box_width = slider_number_value_box_width;
            double line_value_box_width = line_title_box_width;
            double supplier_text_box_width = line_title_box_width;
            double supplier_value_box_width = line_value_box_width;
            double kanban_card_title_box_width = supplier_text_box_width;
            double kanban_card_value_box_1_width = supplier_value_box_width / 2;
            double kanban_card_value_box_2_width = supplier_value_box_width / 2;


            double main_box_horizontal_position = 20;
            double pn_title_box_horizontal_position = main_box_horizontal_position;
            double des_text_box_horizontal_position = pn_title_box_horizontal_position;
            double qty_title_box_horizontal_position = des_text_box_horizontal_position;
            double pn_value_box_horizontal_position = pn_title_box_horizontal_position + pn_title_box_width;
            double des_box_horizontal_position = des_text_box_horizontal_position + des_title_box_width;
            double bin_number_value_box_horizontal_position = pn_value_box_horizontal_position + pn_text_box_width;
            double qty_value_box_horizontal_position = qty_title_box_horizontal_position + qty_title_box_width;
            double bin_type_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
            double bin_type_value_box_horizontal_position = bin_type_box_horizontal_position + bin_type_text_box_width;
            double barcode_bin_number_horizontal_position = des_box_horizontal_position + des_text_box_width + 6;
            double barcode_bin_number_box_horizontal_position = des_box_horizontal_position + des_text_box_width;
            double part_number_barcode_box_horizontal_position = main_box_horizontal_position;
            double slider_number_title_box_horizontal_position = part_number_barcode_box_horizontal_position;
            double slider_number_value_box_horizontal_position = slider_number_title_box_horizontal_position + slider_number_title_box_width;
            double part_number_barcode_horizontal_position = part_number_barcode_box_horizontal_position + 3;
            double line_Title_box_horizontal_position = slider_number_title_box_horizontal_position;
            double line_value_horizontal_position = slider_number_value_box_horizontal_position;
            double supplier_text_box_horizontal_position = line_Title_box_horizontal_position;
            double supplier_value_horizontal_position = slider_number_value_box_horizontal_position;
            double kanban_card_text_box_horizontal_position = supplier_text_box_horizontal_position;
            double kanban_card_value_box_1_horizontal_position = kanban_card_text_box_horizontal_position + kanban_card_title_box_width;
            double kanban_card_value_box_2_horizontal_position = kanban_card_value_box_1_horizontal_position + kanban_card_value_box_1_width;
            double kanban_image_horizontal_position = kanban_card_value_box_2_horizontal_position + kanban_card_value_box_2_width + 10;
            double revision_text_horizontal_position = main_box_horizontal_position + width;
            double model_text_horizontal_position = kanban_card_value_box_2_horizontal_position + kanban_card_value_box_2_width;



            double main_box_vertical_position = 20;
            double pn_title_box_vertical_position = main_box_vertical_position;
            double des_text_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height;
            double qty_title_box_vertical_position = des_text_box_vertical_position + des_title_box_height;
            double pn_value_box_vertical_position = main_box_vertical_position;
            double des_box_vertical_position = des_text_box_vertical_position;
            double bin_number_value_box_vertical_position = main_box_vertical_position;
            double qty_value_box_vertical_position = des_box_vertical_position + des_text_box_height;
            double bin_type_box_vertical_position = des_box_vertical_position + des_text_box_height;
            double bin_type_value_box_vertical_position = bin_type_box_vertical_position;
            double barcode_bin_number_vertical_position = bin_number_value_box_vertical_position + bin_number_value_box_height + 1;
            double barcode_bin_number_box_vertical_position = bin_number_value_box_vertical_position + bin_number_value_box_height;
            double part_number_barcode_box_vertical_position = qty_title_box_vertical_position + qty_title_box_height;
            double slider_number_title_box_vertical_position = part_number_barcode_box_vertical_position + part_number_barcode_box_height;
            double slider_number_value_box_vertical_position = slider_number_title_box_vertical_position;
            double part_number_barcode_vertical_position = part_number_barcode_box_vertical_position + 3;
            double line_title_box_vertical_position = slider_number_title_box_vertical_position + slider_number_title_box_height;
            double line_value_box_vertical_position = slider_number_value_box_vertical_position + slider_number_value_box_height;
            double supplier_text_box_vertical_position = line_title_box_vertical_position + line_title_box_height;
            double supplier_value_vertical_position = line_value_box_vertical_position + line_value_box_height;
            double kanban_card_text_box_vertical_position = supplier_text_box_vertical_position + supplier_text_box_height;
            double kanban_card_value_box_1_vertical_position = supplier_value_vertical_position + supplier_value_box_height;
            double kanban_card_value_box_2_vertical_position = supplier_value_vertical_position + supplier_value_box_height;
            double kanban_image_vertical_position = bin_type_box_vertical_position + bin_type_text_box_height + 10;
            double revision_text_vertical_position = main_box_horizontal_position + height;
            double model_text_vertical_position = main_box_vertical_position + height - 7;


            XColor kanbanColor = XColor.FromArgb(192, 215, 155);

            XBrush brush = new XSolidBrush(kanbanColor);

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options.Height = 155;
            QCwriter.Options.Width = 155;
            QCwriter.Options.Margin = 0;
            var result = QCwriter.Write(kANBAN_MASTER.PART_NO);
            var barcodeBitmap = new Bitmap(result);
            barcodeBitmap.SetResolution(155.0F, 155.0F);



            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);



            System.Drawing.Image part_number_barcode = barcodeBitmap;
            XImage part__number_barcode_image = XImage.FromGdiPlusImage(part_number_barcode);


            //kanban photo
            MemoryStream ms = new MemoryStream(kANBAN_MASTER.PHOTO);
            System.Drawing.Image images = new Bitmap(ms);
            System.Drawing.Image photo = resizeImage(images, new Size(kanban_image_width, kanban_image_height));
            XImage pho = XImage.FromGdiPlusImage(photo);

            XPen pen = new XPen(XColors.Black, 0.5);
            XPen pen2 = new XPen(XColors.Black, 2);


            var bounds = new XRect(gfx.PageOrigin, gfx.PageSize);
            XPoint points = new XPoint();
            string bin_number = kANBAN_MASTER.BIN_NUMBER_END;
            int bin_num = 0;
            for (int i = 0; i < store_Qty; i++)
            {

                if (kanban_per_page_count > 3)
                {
                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    page.Size = PdfSharp.PageSize.A4;
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);
                    kanban_per_page_count = 0;
                    col_count = 0;
                    //reset both position
                    main_box_horizontal_position = 20;
                    pn_title_box_horizontal_position = main_box_horizontal_position;
                    des_text_box_horizontal_position = pn_title_box_horizontal_position;
                    qty_title_box_horizontal_position = des_text_box_horizontal_position;
                    pn_value_box_horizontal_position = pn_title_box_horizontal_position + pn_title_box_width;
                    des_box_horizontal_position = des_text_box_horizontal_position + des_title_box_width;
                    bin_number_value_box_horizontal_position = pn_value_box_horizontal_position + pn_text_box_width;
                    qty_value_box_horizontal_position = qty_title_box_horizontal_position + qty_title_box_width;
                    bin_type_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
                    bin_type_value_box_horizontal_position = bin_type_box_horizontal_position + bin_type_text_box_width;
                    barcode_bin_number_horizontal_position = des_box_horizontal_position + des_text_box_width + 6;
                    barcode_bin_number_box_horizontal_position = des_box_horizontal_position + des_text_box_width;
                    part_number_barcode_box_horizontal_position = main_box_horizontal_position;
                    slider_number_title_box_horizontal_position = part_number_barcode_box_horizontal_position;
                    slider_number_value_box_horizontal_position = slider_number_title_box_horizontal_position + slider_number_title_box_width;
                    part_number_barcode_horizontal_position = part_number_barcode_box_horizontal_position + 3;
                    line_Title_box_horizontal_position = slider_number_title_box_horizontal_position;
                    line_value_horizontal_position = slider_number_value_box_horizontal_position;
                    supplier_text_box_horizontal_position = line_Title_box_horizontal_position;
                    supplier_value_horizontal_position = slider_number_value_box_horizontal_position;
                    kanban_card_text_box_horizontal_position = supplier_text_box_horizontal_position;
                    kanban_card_value_box_1_horizontal_position = kanban_card_text_box_horizontal_position + kanban_card_title_box_width;
                    kanban_card_value_box_2_horizontal_position = kanban_card_value_box_1_horizontal_position + kanban_card_value_box_1_width;
                    kanban_image_horizontal_position = kanban_card_value_box_2_horizontal_position + kanban_card_value_box_2_width + 10;
                    revision_text_horizontal_position = main_box_horizontal_position + width;
                    model_text_horizontal_position = kanban_card_value_box_2_horizontal_position + kanban_card_value_box_2_width;


                    main_box_vertical_position = 20;
                    pn_title_box_vertical_position = main_box_vertical_position;
                    des_text_box_vertical_position = pn_title_box_vertical_position + pn_title_box_height;
                    qty_title_box_vertical_position = des_text_box_vertical_position + des_title_box_height;
                    pn_value_box_vertical_position = main_box_vertical_position;
                    des_box_vertical_position = des_text_box_vertical_position;
                    bin_number_value_box_vertical_position = main_box_vertical_position;
                    qty_value_box_vertical_position = des_box_vertical_position + des_text_box_height;
                    bin_type_box_vertical_position = des_box_vertical_position + des_text_box_height;
                    bin_type_value_box_vertical_position = bin_type_box_vertical_position;
                    barcode_bin_number_vertical_position = bin_number_value_box_vertical_position + bin_number_value_box_height + 1;
                    barcode_bin_number_box_vertical_position = bin_number_value_box_vertical_position + bin_number_value_box_height;
                    part_number_barcode_box_vertical_position = qty_title_box_vertical_position + qty_title_box_height;
                    slider_number_title_box_vertical_position = part_number_barcode_box_vertical_position + part_number_barcode_box_height;
                    slider_number_value_box_vertical_position = slider_number_title_box_vertical_position;
                    part_number_barcode_vertical_position = part_number_barcode_box_vertical_position + 3;
                    line_title_box_vertical_position = slider_number_title_box_vertical_position + slider_number_title_box_height;
                    line_value_box_vertical_position = slider_number_value_box_vertical_position + slider_number_value_box_height;
                    supplier_text_box_vertical_position = line_title_box_vertical_position + line_title_box_height;
                    supplier_value_vertical_position = line_value_box_vertical_position + line_value_box_height;
                    kanban_card_text_box_vertical_position = supplier_text_box_vertical_position + supplier_text_box_height;
                    kanban_card_value_box_1_vertical_position = supplier_value_vertical_position + supplier_value_box_height;
                    kanban_card_value_box_2_vertical_position = supplier_value_vertical_position + supplier_value_box_height;
                    kanban_image_vertical_position = bin_type_box_vertical_position + bin_type_text_box_height + 10;
                    revision_text_vertical_position = main_box_horizontal_position + height;
                    model_text_vertical_position = main_box_vertical_position + height - 7;

                }

                if (col_count == 2)
                {
                    //reset horizontal position
                    main_box_horizontal_position = 20;
                    pn_title_box_horizontal_position = main_box_horizontal_position;
                    des_text_box_horizontal_position = pn_title_box_horizontal_position;
                    qty_title_box_horizontal_position = des_text_box_horizontal_position;
                    pn_value_box_horizontal_position = pn_title_box_horizontal_position + pn_title_box_width;
                    des_box_horizontal_position = des_text_box_horizontal_position + des_title_box_width;
                    bin_number_value_box_horizontal_position = pn_value_box_horizontal_position + pn_text_box_width;
                    qty_value_box_horizontal_position = qty_title_box_horizontal_position + qty_title_box_width;
                    bin_type_box_horizontal_position = qty_value_box_horizontal_position + qty_value_box_width;
                    bin_type_value_box_horizontal_position = bin_type_box_horizontal_position + bin_type_text_box_width;
                    barcode_bin_number_horizontal_position = des_box_horizontal_position + des_text_box_width + 6;
                    barcode_bin_number_box_horizontal_position = des_box_horizontal_position + des_text_box_width;
                    part_number_barcode_box_horizontal_position = main_box_horizontal_position;
                    slider_number_title_box_horizontal_position = part_number_barcode_box_horizontal_position;
                    slider_number_value_box_horizontal_position = slider_number_title_box_horizontal_position + slider_number_title_box_width;
                    part_number_barcode_horizontal_position = part_number_barcode_box_horizontal_position + 3;
                    line_Title_box_horizontal_position = slider_number_title_box_horizontal_position;
                    line_value_horizontal_position = slider_number_value_box_horizontal_position;
                    supplier_text_box_horizontal_position = line_Title_box_horizontal_position;
                    supplier_value_horizontal_position = slider_number_value_box_horizontal_position;
                    kanban_card_text_box_horizontal_position = supplier_text_box_horizontal_position;
                    kanban_card_value_box_1_horizontal_position = kanban_card_text_box_horizontal_position + kanban_card_title_box_width;
                    kanban_card_value_box_2_horizontal_position = kanban_card_value_box_1_horizontal_position + kanban_card_value_box_1_width;
                    kanban_image_horizontal_position = kanban_card_value_box_2_horizontal_position + kanban_card_value_box_2_width + 10;
                    revision_text_horizontal_position = main_box_horizontal_position + width;
                    model_text_horizontal_position = kanban_card_value_box_2_horizontal_position + kanban_card_value_box_2_width;


                    // set the position to duplicate the designs to other kanban cards vertically
                    main_box_vertical_position = main_box_vertical_position + 15 + height;
                    pn_title_box_vertical_position = pn_title_box_vertical_position + 15 + height;
                    des_text_box_vertical_position = des_text_box_vertical_position + 15 + height;
                    qty_title_box_vertical_position = qty_title_box_vertical_position + 15 + height;
                    pn_value_box_vertical_position = pn_value_box_vertical_position + 15 + height;
                    des_box_vertical_position = des_box_vertical_position + 15 + height;
                    bin_number_value_box_vertical_position = bin_number_value_box_vertical_position + 15 + height;
                    qty_value_box_vertical_position = qty_value_box_vertical_position + 15 + height;
                    bin_type_box_vertical_position = bin_type_box_vertical_position + 15 + height;
                    bin_type_value_box_vertical_position = bin_type_value_box_vertical_position + 15 + height;
                    barcode_bin_number_vertical_position = barcode_bin_number_vertical_position + 15 + height;
                    barcode_bin_number_box_vertical_position = barcode_bin_number_box_vertical_position + 15 + height;
                    part_number_barcode_box_vertical_position = part_number_barcode_box_vertical_position + 15 + height;
                    slider_number_title_box_vertical_position = slider_number_title_box_vertical_position + 15 + height;
                    slider_number_value_box_vertical_position = slider_number_value_box_vertical_position + 15 + height;
                    part_number_barcode_vertical_position = part_number_barcode_vertical_position + 15 + height;
                    line_title_box_vertical_position = line_title_box_vertical_position + 15 + height;
                    line_value_box_vertical_position = line_value_box_vertical_position + 15 + height;
                    supplier_text_box_vertical_position = supplier_text_box_vertical_position + 15 + height;
                    supplier_value_vertical_position = supplier_value_vertical_position + 15 + height;
                    kanban_card_text_box_vertical_position = kanban_card_text_box_vertical_position + 15 + height;
                    kanban_card_value_box_1_vertical_position = kanban_card_value_box_1_vertical_position + 15 + height;
                    kanban_card_value_box_2_vertical_position = kanban_card_value_box_2_vertical_position + 15 + height;
                    kanban_image_vertical_position = kanban_image_vertical_position + 15 + height;
                    revision_text_vertical_position = revision_text_vertical_position + 15 + height;
                    model_text_vertical_position = model_text_vertical_position + 15 + height;

                    //reset count
                    col_count = 0;
                }



                //draw the main box
                gfx.DrawRectangle(pen2, main_box_horizontal_position, main_box_vertical_position, width, height);
                double font_size = 10.0;
                string slider_Address = "";
                int number_of_sliders = 0;
                if (kANBAN_MASTER.SLIDER_ADDRESS == null)
                {
                    slider_Address = "";
                }
                else
                {
                    slider_Address = kANBAN_MASTER.SLIDER_ADDRESS;
                    string[] addresses = slider_Address.Split(' ');
                    if (addresses.Length >= 5)
                    {
                        font_size = 7.0;
                        slider_Address = "";
                        for (int j = 0; j < addresses.Length; j++)
                        {
                            if (j % 2 == 0)
                            {
                                slider_Address = slider_Address + addresses[j] + "\t";
                            }
                            else
                            {
                                slider_Address = slider_Address + addresses[j] + System.Environment.NewLine;
                            }
                        }
                        number_of_sliders = addresses.Length;
                    }
                    else
                    {
                        slider_Address = "";
                        for (int j = 0; j < addresses.Length; j++)
                        {
                            slider_Address = slider_Address + addresses[j] + System.Environment.NewLine;
                        }
                        number_of_sliders = addresses.Length;
                    }

                }

                if (bdt.Count>0)
                {
                    bin_number = bdt[i].STORAGE_BIN.ToString();
                }
                else
                {
                    try
                    {
                        string[] bin_split = kANBAN_MASTER.BIN_NUMBER_END.Split('S');
                        bin_num = int.Parse(bin_split[1]);
                        bin_num = bin_num - i;
                        bin_number = "BS0" + bin_num;
                    }
                    catch { bin_number = ""; }
                }



                var bin_number_qr_code = QCwriter.Write(bin_number);
                var barcode_bin_numberBitmap = new Bitmap(bin_number_qr_code);
                barcode_bin_numberBitmap.SetResolution(290.0F, 290.0F);
                System.Drawing.Image bin_number_barcode = barcode_bin_numberBitmap;
                XImage bin_number_barcode_image = XImage.FromGdiPlusImage(bin_number_barcode);

                //draw barcode for the bin number
                gfx.DrawImage(bin_number_barcode_image, barcode_bin_number_horizontal_position, barcode_bin_number_vertical_position);
                //draw the box for the barcode
                gfx.DrawRectangle(pen, barcode_bin_number_box_horizontal_position, barcode_bin_number_box_vertical_position, bin_number_barcode_box_width, bin_number_barcode_box_height);

                //draw box for the part number barcode bin
                gfx.DrawRectangle(pen, part_number_barcode_box_horizontal_position, part_number_barcode_box_vertical_position, part_number_barcode_box_width, part_number_barcode_box_height);
                //draw the barcode
                gfx.DrawImage(part__number_barcode_image, part_number_barcode_horizontal_position, part_number_barcode_vertical_position);

                //draw the photo of the part number
                gfx.DrawImage(pho, kanban_image_horizontal_position, kanban_image_vertical_position);

                //Draw a slider number value box
                gfx.DrawRectangle(pen, brush, slider_number_value_box_horizontal_position, slider_number_value_box_vertical_position, slider_number_value_box_width, slider_number_value_box_height);
                points = new XPoint(slider_number_value_box_horizontal_position + (slider_number_value_box_width / 2), slider_number_value_box_vertical_position + (slider_number_value_box_height / 2));
                XRect rect = new XRect(slider_number_value_box_horizontal_position + 5, slider_number_value_box_vertical_position + 5, slider_number_value_box_width, slider_number_value_box_height);
                tf.DrawString(slider_Address, new XFont("Arial", font_size, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                //draw revision text
                XPoint revision_text_point = new XPoint(revision_text_horizontal_position - 38, revision_text_vertical_position - 10);
                gfx.DrawString(kANBAN_MASTER.REVISION, new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Red, revision_text_point, XStringFormats.Center);

                //draw revision text
                XPoint model_text_point = new XPoint(model_text_horizontal_position, model_text_vertical_position);
                gfx.DrawString(kANBAN_MASTER.MODEL, new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, model_text_point, XStringFormats.CenterLeft);

                //draw line title box
                gfx.DrawRectangle(pen, brush, line_Title_box_horizontal_position, line_title_box_vertical_position, line_title_box_width, line_title_box_height);
                points = new XPoint(line_Title_box_horizontal_position + (line_title_box_width / 2.7), line_title_box_vertical_position + (line_title_box_height / 2));
                gfx.DrawString("Line:", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);

                int number_of_lines = 0;
                string line_string = "";
                font_size = 8.0;
                if (kANBAN_MASTER.LINE == null)
                {
                    line_string = "";
                }
                else
                {
                    line_string = kANBAN_MASTER.LINE;
                    if (line_string.Length > 2)
                    {
                        font_size = 7.0;
                        string[] lineses = line_string.Split(' ');
                        line_string = "";
                        for (int j = 0; j < lineses.Length; j++)
                        {
                            if (j % 2 == 0)
                            {
                                line_string = line_string + lineses[j] + "\t";
                            }
                            else
                            {
                                line_string = line_string + lineses[j] + System.Environment.NewLine;
                            }

                        }
                    }
                    number_of_lines = line_string.Length;
                }

                //draw line title box
                gfx.DrawRectangle(pen, brush, line_value_horizontal_position, line_value_box_vertical_position, line_value_box_width, line_value_box_height);
                points = new XPoint(line_value_horizontal_position + (line_value_box_width / 2), line_value_box_vertical_position + (line_value_box_height / 2));
                rect = new XRect(line_value_horizontal_position + 2, line_value_box_vertical_position, line_value_box_width, line_value_box_height);
                tf.DrawString(line_string, new XFont("Arial", font_size, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                //Draw a slider number title box
                gfx.DrawRectangle(pen, brush, slider_number_title_box_horizontal_position, slider_number_title_box_vertical_position, slider_number_title_box_width, slider_number_title_box_height);
                points = new XPoint(slider_number_title_box_horizontal_position + (slider_number_title_box_width / 2), slider_number_title_box_vertical_position + (slider_number_title_box_height / 2));
                gfx.DrawString("Slider No:", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);

                //draw the box with P/N with the "P/N" text
                gfx.DrawRectangle(pen, brush, pn_title_box_horizontal_position, pn_title_box_vertical_position, pn_title_box_width, pn_title_box_height);
                points = new XPoint(pn_title_box_horizontal_position + 5, pn_title_box_vertical_position + (pn_title_box_height / 2));
                gfx.DrawString("P/N", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.CenterLeft);

                //draw the box with "Des" text
                gfx.DrawRectangle(pen, brush, des_text_box_horizontal_position, des_text_box_vertical_position, des_title_box_width, des_title_box_height);
                points = new XPoint(des_text_box_horizontal_position + 5, des_text_box_vertical_position + (des_title_box_height / 2));
                gfx.DrawString("Des", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.CenterLeft);

                //draw the box with "Qty" text
                gfx.DrawRectangle(pen, brush, qty_title_box_horizontal_position, qty_title_box_vertical_position, qty_title_box_width, qty_title_box_height);
                points = new XPoint(qty_title_box_horizontal_position + 5, qty_title_box_vertical_position + (qty_title_box_height / 2));
                gfx.DrawString("Qty :", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.CenterLeft);

                //draw box with quantity value
                gfx.DrawRectangle(pen, brush, qty_value_box_horizontal_position, qty_value_box_vertical_position, qty_value_box_width, qty_value_box_height);
                points = new XPoint(qty_value_box_horizontal_position + (qty_value_box_width / 2), qty_value_box_vertical_position + (qty_value_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.QTY_PER_BIN.ToString(), new XFont("Arial", 10), XBrushes.Black, points, XStringFormats.Center);

                //draw the box with part number from database
                gfx.DrawRectangle(pen, brush, pn_value_box_horizontal_position, pn_value_box_vertical_position, pn_text_box_width, pn_text_box_height);
                points = new XPoint(pn_value_box_horizontal_position + 5, pn_value_box_vertical_position + (pn_text_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.PART_NO, new XFont("Arial", 16, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.CenterLeft);

                if (kANBAN_MASTER.PART_NAME.Length > 20)
                {
                    kANBAN_MASTER.PART_NAME = kANBAN_MASTER.PART_NAME.Substring(0, 20);
                }

                //draw the box with the description
                gfx.DrawRectangle(pen, brush, des_box_horizontal_position, des_box_vertical_position, des_text_box_width, des_text_box_height);
                points = new XPoint(des_box_horizontal_position + 5, des_box_vertical_position + (des_text_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.PART_NAME, new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.CenterLeft);

                //draw box with supplier value
                gfx.DrawRectangle(pen, brush, supplier_value_horizontal_position, supplier_value_vertical_position, supplier_value_box_width, supplier_value_box_height);
                points = new XPoint(supplier_value_horizontal_position + (supplier_value_box_width / 2), supplier_value_vertical_position + (supplier_value_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.SUPPLIER.Substring(0, 10), new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);

                //draw box with supplier title
                gfx.DrawRectangle(pen, brush, supplier_text_box_horizontal_position, supplier_text_box_vertical_position, supplier_text_box_width, supplier_text_box_height);
                points = new XPoint(supplier_text_box_horizontal_position + (supplier_text_box_width / 2), supplier_text_box_vertical_position + (supplier_text_box_height / 2));
                gfx.DrawString("Supplier:", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                //draw box with bin number
                gfx.DrawRectangle(pen, bin_number_value_box_horizontal_position, bin_number_value_box_vertical_position, bin_number_value_box_width, bin_number_value_box_height);
                rect = new XRect(bin_number_value_box_horizontal_position + 10, bin_number_value_box_vertical_position + 5, bin_number_value_box_width, bin_number_value_box_height);
                tf.DrawString(bin_number, new XFont("Arial", 9.0, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                //draw box with "bin type" as text
                gfx.DrawRectangle(pen, brush, bin_type_box_horizontal_position, bin_type_box_vertical_position, bin_type_text_box_width, bin_type_text_box_height);
                points = new XPoint(bin_type_box_horizontal_position + (bin_type_text_box_width / 2), bin_type_box_vertical_position + (bin_type_text_box_height / 2));
                gfx.DrawString("Bin Type", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.Center);

                //draw box with bin type value
                gfx.DrawRectangle(pen, brush, bin_type_value_box_horizontal_position, bin_type_value_box_vertical_position, bin_type_value_box_width, bin_type_value_box_height);
                points = new XPoint(bin_type_value_box_horizontal_position + (bin_type_value_box_width / 2), bin_type_value_box_vertical_position + (bin_type_value_box_height / 2));
                gfx.DrawString(kANBAN_MASTER.BIN_TYPE, new XFont("Arial", 10), XBrushes.Black, points, XStringFormats.Center);

                //draw box with kanban title
                gfx.DrawRectangle(pen, brush, kanban_card_text_box_horizontal_position, kanban_card_text_box_vertical_position, kanban_card_title_box_width, kanban_card_title_box_height);
                points = new XPoint(kanban_card_text_box_horizontal_position + (kanban_card_title_box_width / 2), kanban_card_text_box_vertical_position + (kanban_card_title_box_height / 2));
                //gfx.DrawString("Kanban Card :", new XFont("Arial", 6), XBrushes.Black, points, XStringFormats.Center);
                rect = new XRect(kanban_card_text_box_horizontal_position + (kanban_card_title_box_width / 8), kanban_card_text_box_vertical_position + (kanban_card_title_box_height / 8), kanban_card_title_box_width, kanban_card_title_box_height);
                tf.DrawString("Kanban" + Environment.NewLine + "Card :", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

                int kanban_number = i + 1;

                //draw box with kanbancard 1
                gfx.DrawRectangle(pen, brush, kanban_card_value_box_1_horizontal_position, kanban_card_value_box_1_vertical_position, kanban_card_value_box_1_width, kanban_card_value_box_1_height);
                points = new XPoint(kanban_card_value_box_1_horizontal_position + (kanban_card_value_box_1_width / 2), kanban_card_value_box_1_vertical_position + (kanban_card_value_box_1_height / 2));
                gfx.DrawString(kanban_number.ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                //draw box with kanbancard 1
                gfx.DrawRectangle(pen, brush, kanban_card_value_box_2_horizontal_position, kanban_card_value_box_2_vertical_position, kanban_card_value_box_2_width, kanban_card_value_box_2_height);
                points = new XPoint(kanban_card_value_box_2_horizontal_position + (kanban_card_value_box_2_width / 2), kanban_card_value_box_2_vertical_position + (kanban_card_value_box_2_height / 2));
                gfx.DrawString(store_Qty.ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                col_count++;


                kanban_per_page_count++;

                main_box_horizontal_position = main_box_horizontal_position + 30 + width;
                pn_title_box_horizontal_position = pn_title_box_horizontal_position + 30 + width;
                des_text_box_horizontal_position = des_text_box_horizontal_position + 30 + width;
                qty_title_box_horizontal_position = qty_title_box_horizontal_position + 30 + width;
                pn_value_box_horizontal_position = pn_value_box_horizontal_position + 30 + width;
                des_box_horizontal_position = des_box_horizontal_position + 30 + width;
                bin_number_value_box_horizontal_position = bin_number_value_box_horizontal_position + 30 + width;
                qty_value_box_horizontal_position = qty_value_box_horizontal_position + 30 + width;
                bin_type_box_horizontal_position = bin_type_box_horizontal_position + 30 + width;
                bin_type_value_box_horizontal_position = bin_type_value_box_horizontal_position + 30 + width;
                barcode_bin_number_horizontal_position = barcode_bin_number_horizontal_position + 30 + width;
                barcode_bin_number_box_horizontal_position = barcode_bin_number_box_horizontal_position + 30 + width;
                part_number_barcode_box_horizontal_position = part_number_barcode_box_horizontal_position + 30 + width;
                slider_number_title_box_horizontal_position = slider_number_title_box_horizontal_position + 30 + width;
                slider_number_value_box_horizontal_position = slider_number_value_box_horizontal_position + 30 + width;
                part_number_barcode_horizontal_position = part_number_barcode_horizontal_position + 30 + width;
                line_Title_box_horizontal_position = line_Title_box_horizontal_position + 30 + width;
                line_value_horizontal_position = line_value_horizontal_position + 30 + width;
                supplier_text_box_horizontal_position = supplier_text_box_horizontal_position + 30 + width;
                supplier_value_horizontal_position = supplier_value_horizontal_position + 30 + width;
                kanban_card_text_box_horizontal_position = kanban_card_text_box_horizontal_position + 30 + width;
                kanban_card_value_box_1_horizontal_position = kanban_card_value_box_1_horizontal_position + 30 + width;
                kanban_card_value_box_2_horizontal_position = kanban_card_value_box_2_horizontal_position + 30 + width;
                kanban_image_horizontal_position = kanban_image_horizontal_position + 30 + width;
                revision_text_horizontal_position = revision_text_horizontal_position + 30 + width;
                model_text_horizontal_position = model_text_horizontal_position + 30 + width;

            }
            return document;
        }

        public PdfDocument create_slider_kanbanWARJ(List<KANBAN_MASTER> data , List<string> empty_p_num,string Kcolor)
        {

            //set height
            double mainbox_height = XUnit.FromMillimeter(33);
            double barcodebox_height = mainbox_height;
            double part_numberbox_height = mainbox_height / 2;
            double part_description_height = mainbox_height / 2;

            int kanban_image_height = 90;

            //set width
            double mainbox_width = XUnit.FromMillimeter(157.0);
            double barcodebox_width = XUnit.FromMillimeter(33);
            double part_numberbox_width = XUnit.FromMillimeter(80);
            double part_description_width = XUnit.FromMillimeter(80);


            int kanban_image_width = 110;

            //set horizontal Position
            double mainbox_horizontal_position = 20;
            double barcodebox_horizontal_position = mainbox_horizontal_position;
            double part_numberbox_horizontal_position = barcodebox_horizontal_position + barcodebox_width;
            double part_description_horizontal_position = barcodebox_horizontal_position + barcodebox_width;
            double kanban_image_horizontal_position = part_numberbox_horizontal_position + part_numberbox_width + 5;
            double part_number_barcode_horizontal_position = mainbox_horizontal_position + 5;
            //set vertical Position
            double mainbox_vertical_position = 20;
            double barcodebox_vertical_position = mainbox_vertical_position;
            double part_numberbox_vertical_position = mainbox_vertical_position;
            double part_description_vertical_position = mainbox_vertical_position + part_numberbox_height;
            double kanban_image_vertical_position = mainbox_vertical_position + 10;
            double part_number_barcode_vertical_position = mainbox_vertical_position + 5;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Workara rack j";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            page.Size = PdfSharp.PageSize.A4;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPen pen = new XPen(XColors.Black, 1);
            XColor kanbanColor = new XColor();
            if (Kcolor == "#F839FD")
            {
                kanbanColor = XColor.FromArgb(248, 57, 253);
            }
            else if (Kcolor == "#A6A6A6")
            {
                kanbanColor = XColor.FromArgb(166, 166, 166);
            }
            else
            {
                kanbanColor = XColor.FromArgb(102, 255, 51);
            }




            XBrush brush = new XSolidBrush(kanbanColor);
            int count = 0;
            foreach (var items in data)
            {
                //Generate barcode photo for part number
                var QCwriter = new BarcodeWriter();
                QCwriter.Format = BarcodeFormat.QR_CODE;
                QCwriter.Options.Height = 500;
                QCwriter.Options.Width = 500;
                QCwriter.Options.Margin = 0;
                var result = QCwriter.Write(items.PART_NO);
                var barcodeBitmap = new Bitmap(result);
                barcodeBitmap.SetResolution(450.0F, 450.0F);
                System.Drawing.Image part_number_barcode = barcodeBitmap;
                XImage part_number_barcode_image = XImage.FromGdiPlusImage(part_number_barcode);



                //kanban card part Number photo
                MemoryStream ms = new MemoryStream(items.PHOTO);
                System.Drawing.Image images = new Bitmap(ms);
                System.Drawing.Image photo = resizeImage(images, new Size(kanban_image_width, kanban_image_height));
                XImage pho = XImage.FromGdiPlusImage(photo);

                count++;
                if (count < 9)
                {
                    XTextFormatter tf = new XTextFormatter(gfx);
                    //draw main box
                    gfx.DrawRectangle(pen, mainbox_horizontal_position, mainbox_vertical_position, mainbox_width, mainbox_height);
                    //draw a box for barcode partnumber
                    gfx.DrawRectangle(pen, barcodebox_horizontal_position, barcodebox_vertical_position, barcodebox_width, barcodebox_height);
                    //draw a qr code
                    gfx.DrawImage(part_number_barcode_image, part_number_barcode_horizontal_position, part_number_barcode_vertical_position);
                    //draw a box for part number
                    gfx.DrawRectangle(pen, brush, part_numberbox_horizontal_position, part_numberbox_vertical_position, part_numberbox_width, part_numberbox_height);
                    //write part number 
                    XPoint points = new XPoint(part_numberbox_horizontal_position + part_numberbox_width / 2, part_numberbox_vertical_position + part_numberbox_height / 2);
                    gfx.DrawString(items.PART_NO, new XFont("Arial", 24, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.Center);
                    //draw a box for description of part number
                    gfx.DrawRectangle(pen, brush, part_description_horizontal_position , part_description_vertical_position, part_description_width, part_description_height);
                    //write description of part number
                    points = new XPoint(part_description_horizontal_position , part_description_vertical_position);
                    XRect rect = new XRect(part_description_horizontal_position + 5, part_description_vertical_position + part_description_height / 4, part_description_width, part_description_height);
                    tf.DrawString(items.PART_NAME, new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                    //draw the image of the part number
                    gfx.DrawImage(pho, kanban_image_horizontal_position, kanban_image_vertical_position);

                    mainbox_vertical_position = mainbox_vertical_position + mainbox_height + 5;
                    barcodebox_vertical_position = barcodebox_vertical_position + mainbox_height + 5;
                    part_numberbox_vertical_position = part_numberbox_vertical_position + mainbox_height + 5;
                    part_description_vertical_position = part_description_vertical_position + mainbox_height + 5;
                    kanban_image_vertical_position = kanban_image_vertical_position + mainbox_height + 5;
                    part_number_barcode_vertical_position = part_number_barcode_vertical_position + mainbox_height + 5;
                }
                else
                {
                    //reset position make new page
                    //make new page
                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Portrait;
                    page.Size = PdfSharp.PageSize.A4;
                    gfx = XGraphics.FromPdfPage(page);
                    XTextFormatter tf = new XTextFormatter(gfx);
                    //reset position
                    //set horizontal Position
                    mainbox_horizontal_position = 5;
                    barcodebox_horizontal_position = mainbox_horizontal_position;
                    part_numberbox_horizontal_position = barcodebox_horizontal_position + barcodebox_width;
                    part_description_horizontal_position = barcodebox_horizontal_position + barcodebox_width;
                    kanban_image_horizontal_position = part_numberbox_horizontal_position + part_numberbox_width + 5;
                    part_number_barcode_horizontal_position = mainbox_horizontal_position + 5;
                    //set vertical Position
                    mainbox_vertical_position = 5;
                    barcodebox_vertical_position = mainbox_vertical_position;
                    part_numberbox_vertical_position = mainbox_vertical_position;
                    part_description_vertical_position = mainbox_vertical_position + part_numberbox_height;
                    kanban_image_vertical_position = mainbox_vertical_position + 10;
                    part_number_barcode_vertical_position = mainbox_vertical_position + 5;


                    //regenerate the kanban sliders

                    //draw main box
                    gfx.DrawRectangle(pen, mainbox_horizontal_position, mainbox_vertical_position, mainbox_width, mainbox_height);
                    //draw a box for barcode partnumber
                    gfx.DrawRectangle(pen, barcodebox_horizontal_position, barcodebox_vertical_position, barcodebox_width, barcodebox_height);
                    //draw a qr code
                    gfx.DrawImage(part_number_barcode_image, part_number_barcode_horizontal_position, part_number_barcode_vertical_position);
                    //draw a box for part number
                    gfx.DrawRectangle(pen, brush, part_numberbox_horizontal_position, part_numberbox_vertical_position, part_numberbox_width, part_numberbox_height);
                    //write part number 
                    XPoint points = new XPoint(part_numberbox_horizontal_position + part_numberbox_width / 2, part_numberbox_vertical_position + part_numberbox_height / 2);
                    gfx.DrawString(items.PART_NO, new XFont("Arial", 24, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.Center);
                    //draw a box for description of part number
                    gfx.DrawRectangle(pen, brush, part_description_horizontal_position, part_description_vertical_position, part_description_width, part_description_height);
                    //write description of part number
                    points = new XPoint(part_description_horizontal_position + part_description_width / 2, part_description_vertical_position + part_description_height / 2);
                    XRect rect = new XRect(part_description_horizontal_position, part_description_vertical_position + part_description_height / 3, part_description_width, part_description_height);
                    tf.DrawString(items.PART_NAME, new XFont("Arial", 16, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                    //draw the image of the part number
                    gfx.DrawImage(pho, kanban_image_horizontal_position, kanban_image_vertical_position);

                    mainbox_vertical_position = mainbox_vertical_position + mainbox_height + 5;
                    barcodebox_vertical_position = barcodebox_vertical_position + mainbox_height + 5;
                    part_numberbox_vertical_position = part_numberbox_vertical_position + mainbox_height + 5;
                    part_description_vertical_position = part_description_vertical_position + mainbox_height + 5;
                    kanban_image_vertical_position = kanban_image_vertical_position + mainbox_height + 5;
                    part_number_barcode_vertical_position = part_number_barcode_vertical_position + mainbox_height + 5;
                    //reset counter
                    count = 0;
                }
            }



            return document;
        }

            public PdfDocument create_slider_kanban(IEnumerable<WebApplication1.Models.KANBAN_MASTER> kANBAN_MASTER, List<string> empty_p_num)
            {
            DataTable dt = new DataTable();
            dt.Columns.Add("part_number", typeof(string));
            dt.Columns.Add("slider address", typeof(string));
            dt.Columns.Add("part_desc", typeof(string));
            dt.Columns.Add("process", typeof(string));
            dt.Columns.Add("photo", typeof(MemoryStream));
            dt.Columns.Add("line", typeof(string));


            foreach (var item in kANBAN_MASTER)
            {
                if (item.SLIDER_ADDRESS != null)
                {
                    string[] slideraddresss = item.SLIDER_ADDRESS.Split(' ');
                    MemoryStream ms = new MemoryStream(item.PHOTO);
                    foreach (var addresses in slideraddresss)
                    {

                        dt.Rows.Add(item.PART_NO, addresses, item.PART_NAME, item.PROCESS, ms, item.LINE);
                        dt.Rows.Add(item.PART_NO, addresses, item.PART_NAME, item.PROCESS, ms, item.LINE);
                    }
                }



            }

            if (empty_p_num.Count != 0)
            {
                foreach (var p in empty_p_num)
                {
                    string[] prts_slider = p.Split('$');
                    dt.Rows.Add(prts_slider[0], prts_slider[1], p, "", null);
                    dt.Rows.Add(prts_slider[0], prts_slider[1], p, "", null);
                }
            }


            //set height
            double mainbox_height = XUnit.FromMillimeter(36.0);
            double barcodebox_height = mainbox_height;
            double slider_addressbox_height = mainbox_height / 2;
            double part_numberbox_height = mainbox_height / 2;
            double part_description_height = mainbox_height / 2;
            double part_number_imagebox_height = mainbox_height;
            double line_numberbox_height = mainbox_height / 2;

            int kanban_image_height = 100;

            //set width
            double mainbox_width = XUnit.FromMillimeter(172.0);
            double barcodebox_width = XUnit.FromMillimeter(31.3);
            double slider_addressbox_width = XUnit.FromMillimeter(28.5);
            double part_numberbox_width = XUnit.FromMillimeter(58.8);
            double part_description_width = slider_addressbox_width + part_numberbox_width;
            double part_number_imagebox_width = mainbox_width - (barcodebox_width + part_description_width);
            double line_numberbox_width = slider_addressbox_width;

            int kanban_image_width = 120;

            //set horizontal position
            double mainbox_horizontal_position = 30;
            double barcodebox_horizontal_position = mainbox_horizontal_position;
            double part_description_horizontal_position = barcodebox_horizontal_position + barcodebox_width;
            double part_number_imagebox_horizontal_position = part_description_horizontal_position + part_description_width;
            double slider_addressbox_horizontal_position = part_number_imagebox_horizontal_position - slider_addressbox_width;
            double part_numberbox_horizontal_position = barcodebox_horizontal_position + barcodebox_width;
            double kanban_image_horizontal = part_number_imagebox_horizontal_position + 30;
            double linebox_horizontal = part_number_imagebox_horizontal_position - line_numberbox_width;
            double linetext_horizontal_position = slider_addressbox_horizontal_position + slider_addressbox_width;


            //set vertical position
            double mainbox_vertical_position = 50;
            double barcodebox_vertical_position = mainbox_vertical_position;
            double slider_addressbox_vertical_position = mainbox_vertical_position;
            double part_numberbox_vertical_position = mainbox_vertical_position;
            double part_description_vertical_position = slider_addressbox_vertical_position + slider_addressbox_height;
            double part_number_imagebox_vertical_position = mainbox_vertical_position;
            double kanban_image_vertical_position = part_number_imagebox_vertical_position + 5;
            double linebox_vertical = part_description_vertical_position;
            double linetext_vertical_position = kanban_image_vertical_position + 76;



            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            XPoint points = new XPoint();
            XPen pen = new XPen(XColors.Black, 1);
            int i = 0;
            int j = 0;
            foreach (var line in dt.Rows)
            {
                if (dt.Rows[i][2].ToString().Contains("$") == true)
                {

                    //Create barcode for part_number
                    XColor kanbanColor = XColor.FromArgb(255, 255, 255);

                    XBrush brush = new XSolidBrush(kanbanColor);
                    if (j > 5)
                    {
                        j = 0;
                        page = document.AddPage();
                        page.Size = PdfSharp.PageSize.A4;
                        gfx = XGraphics.FromPdfPage(page);
                        tf = new XTextFormatter(gfx);

                        //reinitialize position after add page
                        mainbox_vertical_position = 50;
                        barcodebox_vertical_position = mainbox_vertical_position;
                        slider_addressbox_vertical_position = mainbox_vertical_position;
                        part_numberbox_vertical_position = mainbox_vertical_position;
                        part_description_vertical_position = slider_addressbox_vertical_position + slider_addressbox_height;
                        part_number_imagebox_vertical_position = mainbox_vertical_position;
                        kanban_image_vertical_position = part_number_imagebox_vertical_position + 6;
                        linebox_vertical = part_description_vertical_position;
                        linetext_vertical_position = kanban_image_vertical_position + 85;
                    }


                    var QCwriter = new BarcodeWriter();
                    QCwriter.Format = BarcodeFormat.QR_CODE;
                    QCwriter.Options.Height = 150;
                    QCwriter.Options.Width = 150;
                    QCwriter.Options.Margin = 0;
                    var result = QCwriter.Write("S-" + dt.Rows[i][0].ToString());
                    var barcodeBitmap = new Bitmap(result);
                    barcodeBitmap.SetResolution(150.0F, 150.0F);
                    System.Drawing.Image part_number_barcode = barcodeBitmap;
                    XImage part__number_barcode_image = XImage.FromGdiPlusImage(part_number_barcode);


                    string second_barcode = "K-" + dt.Rows[i][1].ToString();
                    var second_writer = new BarcodeWriter();
                    second_writer.Format = BarcodeFormat.QR_CODE;
                    second_writer.Options.Height = 150;
                    second_writer.Options.Width = 150;
                    second_writer.Options.Margin = 0;
                    var second_barcode_result = second_writer.Write(second_barcode);
                    var secondbitmap = new Bitmap(second_barcode_result);
                    secondbitmap.SetResolution(250.0F, 250.0F);

                    System.Drawing.Image line_barcode = secondbitmap;
                    XImage line_barcode_image = XImage.FromGdiPlusImage(line_barcode);

                    //draw main box
                    gfx.DrawRectangle(pen, mainbox_horizontal_position, mainbox_vertical_position, mainbox_width, mainbox_height);
                    //Draw Barcodebox
                    gfx.DrawRectangle(pen, barcodebox_horizontal_position, barcodebox_vertical_position, barcodebox_width, barcodebox_height);
                    //Draw barcode image onto box
                    gfx.DrawImage(part__number_barcode_image, barcodebox_horizontal_position + barcodebox_width / 10, barcodebox_vertical_position + barcodebox_height / 8);
                    //Draw slider address box
                    gfx.DrawRectangle(pen, brush, slider_addressbox_horizontal_position, slider_addressbox_vertical_position, slider_addressbox_width, slider_addressbox_height);

                    //Write Slider Address to slider address box
                    XRect rect = new XRect(slider_addressbox_horizontal_position + slider_addressbox_width / 14, slider_addressbox_vertical_position + slider_addressbox_height / 4, slider_addressbox_width, slider_addressbox_height);
                    tf.DrawString(dt.Rows[i][1].ToString(), new XFont("Arial", 24, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                    //Write part number in the part number box
                    rect = new XRect(part_numberbox_horizontal_position, part_numberbox_vertical_position + part_numberbox_height / 2, part_numberbox_width, part_numberbox_height);
                    tf.DrawString(dt.Rows[i][0].ToString(), new XFont("Arial", 48, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);


                    //Draw line image box
                    gfx.DrawRectangle(pen, linebox_horizontal, linebox_vertical, line_numberbox_width, line_numberbox_height);
                    // Draw line qr code
                    gfx.DrawImage(line_barcode_image, linebox_horizontal + line_numberbox_width / 4, linebox_vertical + 5);

                    //Draw Part number image box
                    gfx.DrawRectangle(pen, part_number_imagebox_horizontal_position, part_number_imagebox_vertical_position, part_number_imagebox_width, part_number_imagebox_height);
                    rect = new XRect(part_number_imagebox_horizontal_position + part_number_imagebox_width / 4, part_number_imagebox_vertical_position + part_numberbox_height / 2, part_numberbox_width, part_numberbox_height);
                    tf.DrawString("NA", new XFont("Arial", 48, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                }
                else
                {

                    //Create barcode for part_number
                    XColor kanbanColor = XColor.FromArgb(192, 215, 155);


                    if (dt.Rows[i][3].ToString() == "A0")
                    {
                        kanbanColor = XColor.FromArgb(154, 229, 255);
                    }
                    else if (dt.Rows[i][3].ToString() == "A0-J" || dt.Rows[i][3].ToString() == "H0-J")
                    {
                        kanbanColor = XColor.FromArgb(255, 92, 255);
                    }

                    XBrush brush = new XSolidBrush(kanbanColor);
                    if (j > 6)
                    {
                        j = 0;
                        page = document.AddPage();
                        page.Size = PdfSharp.PageSize.A4;
                        gfx = XGraphics.FromPdfPage(page);
                        tf = new XTextFormatter(gfx);

                        mainbox_vertical_position = 50;
                        barcodebox_vertical_position = mainbox_vertical_position;
                        slider_addressbox_vertical_position = mainbox_vertical_position;
                        part_numberbox_vertical_position = mainbox_vertical_position;
                        part_description_vertical_position = slider_addressbox_vertical_position + slider_addressbox_height;
                        part_number_imagebox_vertical_position = mainbox_vertical_position;
                        kanban_image_vertical_position = part_number_imagebox_vertical_position + 6;
                        linebox_vertical = part_description_vertical_position;
                        linetext_vertical_position = kanban_image_vertical_position + 75;
                    }


                    var QCwriter = new BarcodeWriter();
                    QCwriter.Format = BarcodeFormat.QR_CODE;
                    QCwriter.Options.Height = 150;
                    QCwriter.Options.Width = 150;
                    QCwriter.Options.Margin = 0;
                    var result = QCwriter.Write("S-" + dt.Rows[i][0].ToString());
                    var barcodeBitmap = new Bitmap(result);
                    barcodeBitmap.SetResolution(150.0F, 150.0F);
                    System.Drawing.Image part_number_barcode = barcodeBitmap;
                    XImage part__number_barcode_image = XImage.FromGdiPlusImage(part_number_barcode);


                    string second_barcode = "K-" + dt.Rows[i][1].ToString();
                    var second_writer = new BarcodeWriter();
                    second_writer.Format = BarcodeFormat.QR_CODE;
                    second_writer.Options.Height = 150;
                    second_writer.Options.Width = 150;
                    second_writer.Options.Margin = 0;
                    var second_barcode_result = second_writer.Write(second_barcode);
                    var secondbitmap = new Bitmap(second_barcode_result);
                    secondbitmap.SetResolution(250.0F, 250.0F);

                    System.Drawing.Image line_barcode = secondbitmap;
                    XImage line_barcode_image = XImage.FromGdiPlusImage(line_barcode);



                    MemoryStream ms = (MemoryStream)dt.Rows[i][4];

                    System.Drawing.Image images = new Bitmap(ms);
                    System.Drawing.Image photo = resizeImage(images, new Size(kanban_image_width, kanban_image_height));
                    XImage pho = XImage.FromGdiPlusImage(photo);

                    //draw main box
                    gfx.DrawRectangle(pen, mainbox_horizontal_position, mainbox_vertical_position, mainbox_width, mainbox_height);
                    //Draw Barcodebox
                    gfx.DrawRectangle(pen, barcodebox_horizontal_position, barcodebox_vertical_position, barcodebox_width, barcodebox_height);
                    //Draw barcode image onto box
                    gfx.DrawImage(part__number_barcode_image, barcodebox_horizontal_position + barcodebox_width / 10, barcodebox_vertical_position + barcodebox_height / 6);
                    //Draw slider address box
                    gfx.DrawRectangle(pen, brush, slider_addressbox_horizontal_position, slider_addressbox_vertical_position, slider_addressbox_width, slider_addressbox_height);

                    //Write Slider Address to slider address box
                    XRect rect = new XRect(slider_addressbox_horizontal_position + slider_addressbox_width / 14, slider_addressbox_vertical_position + slider_addressbox_height / 4, slider_addressbox_width, slider_addressbox_height);
                    tf.DrawString(dt.Rows[i][1].ToString(), new XFont("Arial", 24, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                    //Draw Partnumber box
                    gfx.DrawRectangle(pen, brush, part_numberbox_horizontal_position, part_numberbox_vertical_position, part_numberbox_width, part_numberbox_height);
                    //Write part number in the part number box
                    rect = new XRect(part_numberbox_horizontal_position + part_numberbox_width / 36, part_numberbox_vertical_position + part_numberbox_height / 3, part_numberbox_width, part_numberbox_height);
                    tf.DrawString(dt.Rows[i][0].ToString(), new XFont("Arial", 18, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                    if (dt.Rows[i][2].ToString().Length > 20)
                    {
                        dt.Rows[i][2] = dt.Rows[i][2].ToString().Remove(20, dt.Rows[i][2].ToString().Length - 20);
                    }
                    //Draw Part number Desc box
                    gfx.DrawRectangle(pen, brush, part_description_horizontal_position, part_description_vertical_position, part_description_width, part_description_height);
                    //write Par number desc
                    rect = new XRect(part_description_horizontal_position + 5, part_description_vertical_position + part_description_height / 3, part_description_width, part_description_height);
                    string part_num = dt.Rows[i][2].ToString();
                    if (part_num.Length > 15)
                    {
                        part_num = part_num.Substring(0, 15);
                    }
                    //Draw line image box
                    gfx.DrawRectangle(pen, linebox_horizontal, linebox_vertical, line_numberbox_width, line_numberbox_height);
                    // Draw line qr code
                    gfx.DrawImage(line_barcode_image, linebox_horizontal + line_numberbox_width / 4, linebox_vertical + 5);


                    tf.DrawString(part_num, new XFont("Arial", 16, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                    //Draw Part number image box
                    gfx.DrawRectangle(pen, part_number_imagebox_horizontal_position, part_number_imagebox_vertical_position, part_number_imagebox_width, part_number_imagebox_height);
                    //draw the photo of the part number
                    gfx.DrawImage(pho, kanban_image_horizontal, kanban_image_vertical_position);
                    string[] line_splitted = dt.Rows[i][5].ToString().Split(' ');
                    int fnt_size = 12;
                    double auto_size_linetext_vertical_position = linetext_vertical_position;
                    double linebox_width = mainbox_width - (part_description_width + barcodebox_width);

                    line_splitted = line_splitted.Where(o => o != "").ToArray();


                    if (line_splitted.Length > 6)
                    {
                        fnt_size = 9;

                    }
                    else if (line_splitted.Length <= 6)
                    {
                        fnt_size = 10;
                        auto_size_linetext_vertical_position = auto_size_linetext_vertical_position + 10;
                    }

                    rect = new XRect(linetext_horizontal_position + 2, auto_size_linetext_vertical_position, linebox_width, kanban_image_height);
                    tf.DrawString(dt.Rows[i][5].ToString(), new XFont("Arial", fnt_size, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                }

                mainbox_vertical_position = mainbox_vertical_position + mainbox_height + 5;
                barcodebox_vertical_position = barcodebox_vertical_position + mainbox_height + 5;
                slider_addressbox_vertical_position = slider_addressbox_vertical_position + mainbox_height + 5;
                part_numberbox_vertical_position = part_numberbox_vertical_position + mainbox_height + 5;
                part_description_vertical_position = part_description_vertical_position + mainbox_height + 5;
                part_number_imagebox_vertical_position = part_number_imagebox_vertical_position + mainbox_height + 5;
                kanban_image_vertical_position = kanban_image_vertical_position + mainbox_height + 5;
                linebox_vertical = linebox_vertical + mainbox_height + 5;
                linetext_vertical_position = linetext_vertical_position + mainbox_height + 5;

                i++;
                j++;
            }


            return document;
        }

        public PdfDocument create_FG_kanban(KANBAN_MASTER kANBAN_MASTER)
        {
            int store_qty = int.Parse(kANBAN_MASTER.STORE_KANBAN_QTY.ToString());
            //set width
            double mainbox_width = XUnit.FromMillimeter(61);
            double part_number_line_box_width = mainbox_width;
            double part_des_box_width = mainbox_width;
            double qty_text_box_width = mainbox_width;
            double qty_value_box_width = mainbox_width / 2;
            double qty_barcode_box_width = mainbox_width / 2;
            double stor_loc_text_box_width = mainbox_width;
            double stor_loc_barcode_box_width = mainbox_width;
            double kanban_card_one_width = mainbox_width / 2;
            double kanban_card_two_width = mainbox_width / 2;

            //set height
            double mainbox_height = XUnit.FromMillimeter(128);
            double part_number_line_box_height = XUnit.FromMillimeter(26);
            double part_des_box_height = XUnit.FromMillimeter(15.2);
            double qty_text_box_height = XUnit.FromMillimeter(10);
            double qty_value_box_height = XUnit.FromMillimeter(24);
            double qty_barcode_box_height = XUnit.FromMillimeter(24);
            double stor_loc_text_box_height = XUnit.FromMillimeter(10);
            double stor_loc_barcode_box_height = XUnit.FromMillimeter(33);
            double kanban_card_one_height = XUnit.FromMillimeter(10);
            double kanban_card_two_height = XUnit.FromMillimeter(10);

            //set horizontal position
            double mainbox_horizontal_position = 30;
            double part_number_line_box_horizontal_position = mainbox_horizontal_position;
            double part_des_box_horizontal_position = mainbox_horizontal_position;
            double qty_text_horizontal_position = mainbox_horizontal_position;
            double qty_value_harizontal_position = mainbox_horizontal_position;
            double qty_barcode_horizontal_position = qty_value_harizontal_position + qty_value_box_width;
            double stor_loc_text_box_horizontal_position = mainbox_horizontal_position;
            double stor_loc_barcode_box_horizontal_position = mainbox_horizontal_position;
            double kanban_card_one_horizontal_position = mainbox_horizontal_position;
            double kanban_card_two_horizontal_position = kanban_card_one_width + kanban_card_one_horizontal_position;


            //set vertical position
            double mainbox_vertical_position = 50;
            double part_number_line_box_vertical_position = mainbox_vertical_position;
            double part_des_vertical_position = part_number_line_box_vertical_position + part_number_line_box_height;
            double qty_text_vertical_position = part_des_box_height + part_des_vertical_position;
            double qty_value_vertical_position = qty_text_vertical_position + qty_text_box_height;
            double qty_barcode_vertical_position = qty_value_vertical_position;
            double stor_loc_text_box_vertical_position = qty_barcode_box_height + qty_barcode_vertical_position;
            double stor_loc_barcode_box_vertical_position = stor_loc_text_box_vertical_position + stor_loc_text_box_height;
            double kanban_card_one_vertical_position = stor_loc_barcode_box_height + stor_loc_barcode_box_vertical_position;
            double kanban_card_two_vertical_position = kanban_card_one_vertical_position;


            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPen main_pen = new XPen(XColors.Black, 1.5);
            XPen sub_pen = new XPen(XColors.Black, 1);

            XColor topkanbanColor = XColor.FromArgb(35, 169, 48);

            XBrush brush = new XSolidBrush(topkanbanColor);

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options.Height = 130;
            QCwriter.Options.Width = 130;
            QCwriter.Options.Margin = 0;
            var result = QCwriter.Write(kANBAN_MASTER.QTY_PER_BIN.ToString());
            var barcodeBitmap = new Bitmap(result);
            barcodeBitmap.SetResolution(155.0F, 155.0F);
            System.Drawing.Image qty_per_bin_barcode = barcodeBitmap;
            XImage qty_per_bin_barcode_image = XImage.FromGdiPlusImage(qty_per_bin_barcode);

            var line_barcode = new BarcodeWriter();
            line_barcode.Format = BarcodeFormat.CODE_128;
            line_barcode.Options.PureBarcode = true;

            line_barcode.Options.Height = 70;
            line_barcode.Options.Width = 130;
            line_barcode.Options.Margin = 0;
            var result_line = line_barcode.Write(kANBAN_MASTER.LINE);
            var linebarcodeBitmap = new Bitmap(result_line);
            //linebarcodeBitmap.SetResolution(40.0F, 155.0F);
            System.Drawing.Image line_barcode_img = linebarcodeBitmap;
            XImage line_barcode_image = XImage.FromGdiPlusImage(line_barcode_img);

            if (kANBAN_MASTER.PART_NAME.Length > 15)
            {
                kANBAN_MASTER.PART_NAME = kANBAN_MASTER.PART_NAME.Remove(15, kANBAN_MASTER.PART_NAME.Length - 15);
            }

            int col_count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (col_count == 4)
                {
                    col_count = 0;
                    page = document.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    gfx = XGraphics.FromPdfPage(page);
                    mainbox_horizontal_position = 30;
                    part_number_line_box_horizontal_position = mainbox_horizontal_position;
                    part_des_box_horizontal_position = mainbox_horizontal_position;
                    qty_text_horizontal_position = mainbox_horizontal_position;
                    qty_value_harizontal_position = mainbox_horizontal_position;
                    qty_barcode_horizontal_position = qty_value_harizontal_position + qty_value_box_width;
                    stor_loc_text_box_horizontal_position = mainbox_horizontal_position;
                    stor_loc_barcode_box_horizontal_position = mainbox_horizontal_position;
                    kanban_card_one_horizontal_position = mainbox_horizontal_position;
                    kanban_card_two_horizontal_position = kanban_card_one_width + kanban_card_one_horizontal_position;

                }
                //draw main box
                gfx.DrawRectangle(main_pen, mainbox_horizontal_position, mainbox_vertical_position, mainbox_width, mainbox_height);
                //draw part number and line box
                gfx.DrawRectangle(sub_pen, brush, part_number_line_box_horizontal_position, part_number_line_box_vertical_position, part_number_line_box_width, part_number_line_box_height);
                XPoint points = new XPoint(part_number_line_box_horizontal_position + (part_number_line_box_width / 2), part_number_line_box_vertical_position + 20);
                gfx.DrawString(kANBAN_MASTER.PART_NO, new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.Center);
                points = new XPoint(part_number_line_box_horizontal_position + (part_number_line_box_width / 2), part_number_line_box_vertical_position + 55);
                gfx.DrawString(kANBAN_MASTER.LINE, new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.Center);
                //draw part description box
                gfx.DrawRectangle(sub_pen, part_des_box_horizontal_position, part_des_vertical_position, part_des_box_width, part_des_box_height);
                points = new XPoint(part_des_box_horizontal_position + (part_des_box_width / 2), part_des_vertical_position + part_des_box_height / 2);
                gfx.DrawString(kANBAN_MASTER.PART_NAME, new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);
                //draw quantity textbox
                gfx.DrawRectangle(sub_pen, qty_text_horizontal_position, qty_text_vertical_position, qty_text_box_width, qty_text_box_height);
                points = new XPoint(qty_text_horizontal_position + (qty_text_box_width / 2), qty_text_vertical_position + qty_text_box_height / 2);
                gfx.DrawString("QUANTITY", new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);
                //draw qty value box
                gfx.DrawRectangle(sub_pen, qty_value_harizontal_position, qty_value_vertical_position, qty_value_box_width, qty_value_box_height);
                points = new XPoint(qty_value_harizontal_position + (qty_value_box_width / 2), qty_value_vertical_position + qty_value_box_height / 2);
                gfx.DrawString(kANBAN_MASTER.QTY_PER_BIN.ToString(), new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);
                //draw qty barcode box
                gfx.DrawRectangle(sub_pen, qty_barcode_horizontal_position, qty_barcode_vertical_position, qty_barcode_box_width, qty_barcode_box_height);
                gfx.DrawImage(qty_per_bin_barcode_image, qty_barcode_horizontal_position + 5, qty_barcode_vertical_position + 5);

                //draw stor loc text box
                gfx.DrawRectangle(sub_pen, stor_loc_text_box_horizontal_position, stor_loc_text_box_vertical_position, stor_loc_text_box_width, stor_loc_text_box_height);
                points = new XPoint(stor_loc_text_box_horizontal_position + (stor_loc_text_box_width / 2), stor_loc_text_box_vertical_position + stor_loc_text_box_height / 2);
                gfx.DrawString("Storage Location", new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);
                //draw strorage loc barcode box
                gfx.DrawRectangle(sub_pen, stor_loc_barcode_box_horizontal_position, stor_loc_barcode_box_vertical_position, stor_loc_barcode_box_width, stor_loc_barcode_box_height);
                points = new XPoint(stor_loc_barcode_box_horizontal_position + (stor_loc_barcode_box_width / 2), stor_loc_barcode_box_vertical_position + 75);
                gfx.DrawString(kANBAN_MASTER.LINE, new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);
                gfx.DrawImage(line_barcode_image, stor_loc_barcode_box_horizontal_position + (stor_loc_barcode_box_width / 4), stor_loc_barcode_box_vertical_position + 10);
                //draw kanban_card one
                gfx.DrawRectangle(sub_pen, kanban_card_one_horizontal_position, kanban_card_one_vertical_position, kanban_card_one_width, kanban_card_one_height);
                points = new XPoint(kanban_card_one_horizontal_position + (kanban_card_one_width / 2), kanban_card_one_vertical_position + (kanban_card_one_height / 2));
                gfx.DrawString((i + 1).ToString(), new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);
                //draw kanban card two
                gfx.DrawRectangle(sub_pen, kanban_card_two_horizontal_position, kanban_card_two_vertical_position, kanban_card_two_width, kanban_card_two_height);
                points = new XPoint(kanban_card_two_horizontal_position + (kanban_card_two_width / 2), kanban_card_two_vertical_position + (kanban_card_two_height / 2));
                gfx.DrawString("4", new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.Center);

                mainbox_horizontal_position = mainbox_horizontal_position + mainbox_width + 20;
                part_number_line_box_horizontal_position = part_number_line_box_horizontal_position + mainbox_width + 20;
                part_des_box_horizontal_position = part_des_box_horizontal_position + mainbox_width + 20;
                qty_text_horizontal_position = qty_text_horizontal_position + mainbox_width + 20;
                qty_value_harizontal_position = qty_value_harizontal_position + mainbox_width + 20;
                qty_barcode_horizontal_position = qty_barcode_horizontal_position + mainbox_width + 20;
                stor_loc_text_box_horizontal_position = stor_loc_text_box_horizontal_position + mainbox_width + 20;
                stor_loc_barcode_box_horizontal_position = stor_loc_barcode_box_horizontal_position + mainbox_width + 20;
                kanban_card_one_horizontal_position = kanban_card_one_horizontal_position + mainbox_width + 20;
                kanban_card_two_horizontal_position = kanban_card_two_horizontal_position + mainbox_width + 20;
                col_count++;

            }


            return document;
        }

        public PdfDocument createWHpickingList(string[] production_orders)
        {
            PROD_ORDER.get_part_quantity_from_binSoapClient prod_ord = new PROD_ORDER.get_part_quantity_from_binSoapClient();
            DataTable compiled_Data = new DataTable();

            string prod_orders = "";
            string orderqty = "";
            int k = 1;
            foreach (var i in production_orders)
            {
                if (i != "")
                {
                    DataTable datafrom_sap = prod_ord.GET_PRODUCTION_QUANTITY_BY_ORDER(i);
                    if (compiled_Data.Columns.Count <= 0)
                    {
                        foreach (DataColumn dc in datafrom_sap.Columns)
                        {
                            compiled_Data.Columns.Add(dc.ColumnName, dc.DataType);
                        }
                    }
                    compiled_Data.Merge(datafrom_sap);
                    if (k % 7 != 0)
                    {
                        prod_orders = prod_orders + i + " ";

                    }
                    else if (k % 7 == 0)
                    {
                        prod_orders = prod_orders + i + Environment.NewLine;
                    }

                }
                k++;
            }
            DataView dv = new DataView(compiled_Data);
            DataTable compile_Data_distinct = dv.ToTable(true, "PRODUCTION ORDER", "ORDER QUANTITY");

            for (int t = 0; t < compile_Data_distinct.Rows.Count; t++)
            {

                orderqty = orderqty + Math.Round(decimal.Parse(compile_Data_distinct.Rows[t][1].ToString())).ToString() + " ";
            }

            DataTable compile_Data_aggr = compiled_Data.AsEnumerable().
                GroupBy(r => r.Field<string>("COMPONENT")).
                Select(g =>
                {
                    var row = compiled_Data.NewRow();
                    row["COMPONENT"] = g.Key;
                    row["ORDER QUANTITY"] = g.Sum(r => r.Field<decimal>("ORDER QUANTITY"));
                    row["REQUIREMENT QUANTITY"] = g.Sum(r => r.Field<decimal>("REQUIREMENT QUANTITY"));
                    row["WITHDRAWN QUANTITY"] = g.Sum(r => r.Field<decimal>("WITHDRAWN QUANTITY"));
                    return row;
                }).CopyToDataTable();

            DataTable line_material = compiled_Data.AsEnumerable().
                GroupBy(r => new { component = r.Field<string>("COMPONENT"), line = r.Field<string>("LINE") })
                .Select(g =>
                {
                    var row = compiled_Data.NewRow();
                    row["COMPONENT"] = g.Key.component;
                    row["LINE"] = g.Key.line;
                    return row;
                }).CopyToDataTable();


            for (int i = 0; i < compile_Data_aggr.Rows.Count; i++)
            {
                string material = compile_Data_aggr.Rows[i][1].ToString();
                string line = compile_Data_aggr.Rows[i][2].ToString();

                for (int j = 0; j < line_material.Rows.Count; j++)
                {
                    string material_line_only = line_material.Rows[j][1].ToString();
                    string line_line_only = line_material.Rows[j][2].ToString();

                    if (material_line_only == material)
                    {
                        if (line == null || line == "")
                        {
                            if (!line.Contains(line_line_only))
                            {
                                compile_Data_aggr.Rows[i][2] = compile_Data_aggr.Rows[i][2].ToString() + " " + line_material.Rows[j][2].ToString();
                            }

                        }
                    }
                }

            }

            DataTable stock_location = new DataTable();
            compile_Data_aggr.Columns.Add("LOCATION", typeof(string));
            compile_Data_aggr.Columns.Add("CURRENT STOCK", typeof(decimal));
            compile_Data_aggr.Columns.Add("BALANCE", typeof(string));


            for (int i = 0; i < compile_Data_aggr.Rows.Count; i++)
            {
                STOCK_FROM_MATERIAL.get_part_quantity_from_binSoapClient STOCK_FROM_MATERIAL = new STOCK_FROM_MATERIAL.get_part_quantity_from_binSoapClient();
                string matrial = compile_Data_aggr.Rows[i][1].ToString();
                try
                {
                    stock_location = STOCK_FROM_MATERIAL.GET_STOCK_FROM_MATERIAL(matrial);

                }
                catch (Exception ex)
                {
                    string d = ex.Message.ToString();
                }


                string location_combined = "";
                if (stock_location.Rows.Count > 0)
                {

                    DataTable stock_location_aggr = stock_location.AsEnumerable().
                            GroupBy(r => r.Field<string>("material")).
                            Select(g =>
                            {
                                var row = stock_location.NewRow();
                                row["material"] = g.Key;
                                row["qty"] = g.Sum(r => r.Field<decimal>("qty"));
                                return row;
                            }).CopyToDataTable();

                    for (int j = 0; j < stock_location.Rows.Count; j++)
                    {
                        location_combined = location_combined + stock_location.Rows[j][1].ToString() + "_" + stock_location.Rows[j][2].ToString() + " ";

                    }

                    if (stock_location_aggr.Rows.Count > 0)
                    {
                        for (int j = 0; j < stock_location_aggr.Rows.Count; j++)
                        {
                            if (matrial == stock_location_aggr.Rows[0][j].ToString())
                            {
                                compile_Data_aggr.Rows[i][7] = stock_location_aggr.Rows[j][2];
                                compile_Data_aggr.Rows[i][8] = decimal.Parse(stock_location_aggr.Rows[j][2].ToString()) - decimal.Parse(compile_Data_aggr.Rows[i][4].ToString());

                            }

                        }
                    }


                }
                else
                {
                    compile_Data_aggr.Rows[i][8] = 0.0m;
                }

                compile_Data_aggr.Rows[i][6] = location_combined;
                stock_location.Clear();
            }

            DataView view = compile_Data_aggr.AsDataView();

            view.Sort = "COMPONENT ASC";

            DataTable compile_Data_aggr_SORTED = view.ToTable();


            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPen pen = new XPen(XColors.Black, 1.5);
            XSolidBrush br = new XSolidBrush(XColor.FromArgb(162, 255, 255, 1));

            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;
            var tf = new XTextFormatter(gfx);

            XFont fontParagraph = new XFont("Verdana", 8, XFontStyle.Regular);

            double row_height = XUnit.FromMillimeter(12);
            double row_width = XUnit.FromMillimeter(23);
            double line_row_width = XUnit.FromMillimeter(15);
            double req_qty_row_width = XUnit.FromMillimeter(25);
            double withdraw_qty_row_width = XUnit.FromMillimeter(56);
            double location_row_width = XUnit.FromMillimeter(90);
            double title_width = row_width * 4 + line_row_width + req_qty_row_width + withdraw_qty_row_width + location_row_width;
            double prod_order_width = row_width + line_row_width;
            double prod_order_val_width = row_width * 4 + withdraw_qty_row_width + location_row_width;


            double row_model_horizontal_position = 20;

            double row_line_horizontal_position = row_model_horizontal_position + row_width;

            double row_withdraw_qty_horizontal_position = row_line_horizontal_position + line_row_width;
            double prod_order_val_horizontal_position = row_withdraw_qty_horizontal_position;
            double row_req_qty_horizontal_position = row_withdraw_qty_horizontal_position + withdraw_qty_row_width;

            double row_order_qty_horizontal_position = row_req_qty_horizontal_position + req_qty_row_width;


            double location_horizontal_position = row_order_qty_horizontal_position + row_width;
            double cur_stock_horizontal_position = location_horizontal_position + location_row_width;
            double balance_horizontal_position = cur_stock_horizontal_position + row_width;

            double row_model_vertical_position = 180;
            double row_line_vertical_position = row_model_vertical_position;
            double row_order_qty_vertical_position = row_line_vertical_position;
            double row_req_qty_vertical_position = row_order_qty_vertical_position;
            double row_withdraw_qty_vertical_position = row_req_qty_vertical_position;
            double location_vertical_position = row_withdraw_qty_vertical_position;
            double cur_stock_vertical_position = location_vertical_position;
            double balance_vertical_position = cur_stock_vertical_position;


            XRect rect = new XRect(20, 40, title_width, row_height + 20);
            XPoint rp = new XPoint(20 + (title_width / 2), 40 + ((row_height + 20) / 2));
            gfx.DrawRectangle(pen, br, 20, 40, title_width, row_height + 20);
            gfx.DrawString("PICKLIST FOR SERVICES LOT/SLOW MOVING", new XFont("Arial", 18), XBrushes.Black, rp, XStringFormats.Center);

            tf = new XTextFormatter(gfx);
            XRect rect2 = new XRect(25, 120, prod_order_width, row_height);
            rect = new XRect(20, 110, prod_order_width, row_height);
            gfx.DrawRectangle(pen, rect);
            tf.DrawString("Production Orders", new XFont("Arial", 12), XBrushes.Black, rect2, XStringFormats.TopLeft);

            tf = new XTextFormatter(gfx);
            rect = new XRect(prod_order_val_horizontal_position, 110, prod_order_val_width, row_height);
            rect2 = new XRect(prod_order_val_horizontal_position + 10, 111, prod_order_val_width, row_height);
            gfx.DrawRectangle(pen, rect);
            tf.DrawString(prod_orders, new XFont("Arial", 12), XBrushes.Black, rect2, XStringFormats.TopLeft);

            tf = new XTextFormatter(gfx);
            rect2 = new XRect(30, 120 + row_height, prod_order_width, row_height);
            rect = new XRect(20, 110 + row_height, prod_order_width, row_height);
            gfx.DrawRectangle(pen, rect);
            tf.DrawString("Quantity", new XFont("Arial", 12), XBrushes.Black, rect2, XStringFormats.TopLeft);

            tf = new XTextFormatter(gfx);
            rect = new XRect(prod_order_val_horizontal_position, 110 + row_height, prod_order_val_width, row_height);
            rect2 = new XRect(prod_order_val_horizontal_position + 10, 120 + row_height, prod_order_val_width, row_height);
            gfx.DrawRectangle(pen, rect);
            tf.DrawString(orderqty, new XFont("Arial", 12), XBrushes.Black, rect2, XStringFormats.TopLeft);

            gfx.DrawRectangle(pen, row_model_horizontal_position, row_model_vertical_position, row_width, row_height);
            rect = new XRect(row_model_horizontal_position + 15, row_model_vertical_position + 5, row_width, row_height);
            XPoint points = new XPoint(row_model_horizontal_position + (row_width / 2), row_model_vertical_position + row_height / 2);
            //gfx.DrawString("PART NUMBER", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
            tf.DrawString("PART\nNUMBER", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

            gfx.DrawRectangle(pen, row_withdraw_qty_horizontal_position, row_withdraw_qty_vertical_position, withdraw_qty_row_width, row_height);
            points = new XPoint(row_withdraw_qty_horizontal_position + (withdraw_qty_row_width / 2), row_withdraw_qty_vertical_position + row_height / 2);
            gfx.DrawString("DESCRIPTION", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

            gfx.DrawRectangle(pen, row_line_horizontal_position, row_line_vertical_position, line_row_width, row_height);
            points = new XPoint(row_line_horizontal_position + (line_row_width / 2), row_line_vertical_position + row_height / 2);
            gfx.DrawString("LINE", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

            gfx.DrawRectangle(pen, row_order_qty_horizontal_position, row_order_qty_vertical_position, row_width, row_height);
            rect = new XRect(row_order_qty_horizontal_position + 15, row_order_qty_vertical_position + 5, row_width, row_height);
            points = new XPoint(row_order_qty_horizontal_position + (row_width / 2), row_order_qty_vertical_position + row_height / 2);
            //gfx.DrawString("ORDER QUANTITY", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
            tf.DrawString("ORDER\nQUANTITY", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

            gfx.DrawRectangle(pen, row_req_qty_horizontal_position, row_req_qty_vertical_position, req_qty_row_width, row_height);
            rect = new XRect(row_req_qty_horizontal_position + 15, row_req_qty_vertical_position + 5, req_qty_row_width, row_height);
            points = new XPoint(row_req_qty_horizontal_position + (req_qty_row_width / 2), row_req_qty_vertical_position + row_height / 2);
            //gfx.DrawString("OPEN QUANTITY", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
            tf.DrawString("OPEN\nQUANTITY", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

            gfx.DrawRectangle(pen, location_horizontal_position, location_vertical_position, location_row_width, row_height);
            points = new XPoint(location_horizontal_position + (location_row_width / 2), location_vertical_position + row_height / 2);
            gfx.DrawString("LOCATION", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

            gfx.DrawRectangle(pen, cur_stock_horizontal_position, cur_stock_vertical_position, row_width, row_height);
            rect = new XRect(cur_stock_horizontal_position + 15, cur_stock_vertical_position + 5, row_width, row_height);
            points = new XPoint(cur_stock_horizontal_position + (row_width / 2), cur_stock_vertical_position + row_height / 2);
            //gfx.DrawString("CURRENT STOCK", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
            tf.DrawString("CURRENT\nSTOCK", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

            gfx.DrawRectangle(pen, balance_horizontal_position, balance_vertical_position, row_width, row_height);
            points = new XPoint(balance_horizontal_position + (row_width / 2), balance_vertical_position + row_height / 2);
            gfx.DrawString("BALANCE", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

            Controllers.getIbusinessData GIB = new Controllers.getIbusinessData();

            int count_page = 1;
            int rows_per_page = 0;
            int count_rows_per_doc = 0;

            for (int i = 0; i < compile_Data_aggr_SORTED.Rows.Count; i++)
            {
                row_model_vertical_position = row_model_vertical_position + row_height;
                row_line_vertical_position = row_line_vertical_position + row_height;
                row_order_qty_vertical_position = row_order_qty_vertical_position + row_height;
                row_req_qty_vertical_position = row_req_qty_vertical_position + row_height;
                row_withdraw_qty_vertical_position = row_withdraw_qty_vertical_position + row_height;
                location_vertical_position = location_vertical_position + row_height;
                cur_stock_vertical_position = cur_stock_vertical_position + row_height;
                balance_vertical_position = balance_vertical_position + row_height;

                gfx.DrawRectangle(pen, row_model_horizontal_position, row_model_vertical_position, row_width, row_height);
                points = new XPoint(row_model_horizontal_position + (row_width / 2), row_model_vertical_position + row_height / 2);
                gfx.DrawString(compile_Data_aggr_SORTED.Rows[i][1].ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                gfx.DrawRectangle(pen, row_line_horizontal_position, row_line_vertical_position, line_row_width, row_height);
                points = new XPoint(row_line_horizontal_position + (line_row_width / 2), row_line_vertical_position + row_height / 2);
                gfx.DrawString(compile_Data_aggr_SORTED.Rows[i][2].ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                gfx.DrawRectangle(pen, row_order_qty_horizontal_position, row_order_qty_vertical_position, row_width, row_height);
                points = new XPoint(row_order_qty_horizontal_position + (row_width / 2), row_order_qty_vertical_position + row_height / 2);
                gfx.DrawString(compile_Data_aggr_SORTED.Rows[i][3].ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                gfx.DrawRectangle(pen, row_req_qty_horizontal_position, row_req_qty_vertical_position, req_qty_row_width, row_height);
                points = new XPoint(row_req_qty_horizontal_position + (req_qty_row_width / 2), row_req_qty_vertical_position + row_height / 2);
                gfx.DrawString(compile_Data_aggr_SORTED.Rows[i][4].ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                gfx.DrawRectangle(pen, row_withdraw_qty_horizontal_position, row_withdraw_qty_vertical_position, withdraw_qty_row_width, row_height);
                points = new XPoint(row_withdraw_qty_horizontal_position + (withdraw_qty_row_width / 2), row_withdraw_qty_vertical_position + row_height / 2);
                gfx.DrawString(GIB.get_part_description(compile_Data_aggr_SORTED.Rows[i][1].ToString()), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                string loc_bin = compile_Data_aggr_SORTED.Rows[i][6].ToString();
                string[] loc_bin_split = loc_bin.Split(' ');

                tf = new XTextFormatter(gfx);
                rect = new XRect(location_horizontal_position + 5, location_vertical_position + 2, location_row_width - 10, row_height);
                gfx.DrawRectangle(pen, location_horizontal_position, location_vertical_position, location_row_width, row_height);
                points = new XPoint(location_horizontal_position + (location_row_width / 2), location_vertical_position + row_height / 2);
                tf.DrawString(loc_bin, new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

                gfx.DrawRectangle(pen, cur_stock_horizontal_position, cur_stock_vertical_position, row_width, row_height);
                points = new XPoint(cur_stock_horizontal_position + (row_width / 2), cur_stock_vertical_position + row_height / 2);
                gfx.DrawString(compile_Data_aggr_SORTED.Rows[i][7].ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                gfx.DrawRectangle(pen, balance_horizontal_position, balance_vertical_position, row_width, row_height);
                points = new XPoint(balance_horizontal_position + (row_width / 2), balance_vertical_position + row_height / 2);
                gfx.DrawString(compile_Data_aggr_SORTED.Rows[i][8].ToString(), new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                if (count_page == 1)
                {
                    rows_per_page = 10;
                }
                else
                {

                    rows_per_page = 15;
                }


                if ((count_rows_per_doc + 1) % rows_per_page == 0)
                {

                    row_model_vertical_position = 20;
                    row_line_vertical_position = row_model_vertical_position;
                    row_order_qty_vertical_position = row_line_vertical_position;
                    row_req_qty_vertical_position = row_order_qty_vertical_position;
                    row_withdraw_qty_vertical_position = row_req_qty_vertical_position;
                    location_vertical_position = row_withdraw_qty_vertical_position;
                    cur_stock_vertical_position = location_vertical_position;
                    balance_vertical_position = cur_stock_vertical_position;

                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    page.Size = PdfSharp.PageSize.A4;
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx);

                    gfx.DrawRectangle(pen, row_model_horizontal_position, row_model_vertical_position, row_width, row_height);
                    rect = new XRect(row_model_horizontal_position + 15, row_model_vertical_position + 5, row_width, row_height);
                    points = new XPoint(row_model_horizontal_position + (row_width / 2), row_model_vertical_position + row_height / 2);
                    //gfx.DrawString("PART NUMBER", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
                    tf.DrawString("PART\nNUMBER", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

                    gfx.DrawRectangle(pen, row_withdraw_qty_horizontal_position, row_withdraw_qty_vertical_position, withdraw_qty_row_width, row_height);
                    points = new XPoint(row_withdraw_qty_horizontal_position + (withdraw_qty_row_width / 2), row_withdraw_qty_vertical_position + row_height / 2);
                    gfx.DrawString("DESCRIPTION", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                    gfx.DrawRectangle(pen, row_line_horizontal_position, row_line_vertical_position, line_row_width, row_height);
                    points = new XPoint(row_line_horizontal_position + (line_row_width / 2), row_line_vertical_position + row_height / 2);
                    gfx.DrawString("LINE", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                    gfx.DrawRectangle(pen, row_order_qty_horizontal_position, row_order_qty_vertical_position, row_width, row_height);
                    rect = new XRect(row_order_qty_horizontal_position + 15, row_order_qty_vertical_position + 5, row_width, row_height);
                    points = new XPoint(row_order_qty_horizontal_position + (row_width / 2), row_order_qty_vertical_position + row_height / 2);
                    //gfx.DrawString("ORDER QUANTITY", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
                    tf.DrawString("ORDER\nQUANTITY", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

                    gfx.DrawRectangle(pen, row_req_qty_horizontal_position, row_req_qty_vertical_position, req_qty_row_width, row_height);
                    rect = new XRect(row_req_qty_horizontal_position + 15, row_req_qty_vertical_position + 5, req_qty_row_width, row_height);
                    points = new XPoint(row_req_qty_horizontal_position + (req_qty_row_width / 2), row_req_qty_vertical_position + row_height / 2);
                    //gfx.DrawString("OPEN QUANTITY", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
                    tf.DrawString("OPEN\nQUANTITY", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

                    gfx.DrawRectangle(pen, location_horizontal_position, location_vertical_position, location_row_width, row_height);
                    points = new XPoint(location_horizontal_position + (location_row_width / 2), location_vertical_position + row_height / 2);
                    gfx.DrawString("LOCATION", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                    gfx.DrawRectangle(pen, cur_stock_horizontal_position, cur_stock_vertical_position, row_width, row_height);
                    rect = new XRect(cur_stock_horizontal_position + 15, cur_stock_vertical_position + 5, row_width, row_height);
                    points = new XPoint(cur_stock_horizontal_position + (row_width / 2), cur_stock_vertical_position + row_height / 2);
                    //gfx.DrawString("CURRENT STOCK", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);
                    tf.DrawString("CURRENT\nSTOCK", new XFont("Arial", 8), XBrushes.Black, rect, XStringFormats.TopLeft);

                    gfx.DrawRectangle(pen, balance_horizontal_position, balance_vertical_position, row_width, row_height);
                    points = new XPoint(balance_horizontal_position + (row_width / 2), balance_vertical_position + row_height / 2);
                    gfx.DrawString("BALANCE", new XFont("Arial", 8), XBrushes.Black, points, XStringFormats.Center);

                    count_page++;
                    if (count_page == 2)
                    {
                        count_rows_per_doc = 15;
                    }
                }

                count_rows_per_doc++;
            }

            return document;
        }

        public PdfDocument create_smt_bin_label(List<RACK_MATERIAL> rmp)
        {



            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPen main_pen = new XPen(XColors.Black, 3);
            XPen sub_pen = new XPen(XColors.Black, 1.5);
            var tf = new XTextFormatter(gfx);

            //set width
            double label_width = XUnit.FromMillimeter(190);
            double rack_box_width = XUnit.FromMillimeter(150);
            double pn_box_width = XUnit.FromMillimeter(150);

            //set height
            double label_height = XUnit.FromMillimeter(36);
            double rack_box_height = label_height / 2.8;
            double pn_box_height = label_height - rack_box_height;

            //set horizontal position
            double label_horizontal_position = 20;
            double rack_box_horizontal_position = label_horizontal_position;
            double pn_box_horizontal_position = rack_box_horizontal_position;

            //set vertical position
            double label_vertical_position = 20;
            double rack_box_vertical_position = label_vertical_position;
            double pn_box_vertical_position = rack_box_vertical_position + rack_box_height;
            foreach (var K in rmp)
            {
                var QCwriter = new BarcodeWriter();
                QCwriter.Format = BarcodeFormat.QR_CODE;
                QCwriter.Options.Height = 170;
                QCwriter.Options.Width = 170;
                QCwriter.Options.Margin = 0;
                var result = QCwriter.Write(K.RACK);
                var barcodeBitmap = new Bitmap(result);
                barcodeBitmap.SetResolution(140.0F, 140.0F);
                System.Drawing.Image part_number_barcode = barcodeBitmap;
                XImage part__number_barcode_image = XImage.FromGdiPlusImage(part_number_barcode);

                //draw label main box
                gfx.DrawRectangle(main_pen, label_horizontal_position, label_vertical_position, label_width, label_height);
                gfx.DrawRectangle(sub_pen, rack_box_horizontal_position, rack_box_vertical_position, rack_box_width, rack_box_height);
                XRect rect = new XRect(rack_box_horizontal_position, rack_box_vertical_position - 10, rack_box_width, rack_box_height);
                tf.Alignment = XParagraphAlignment.Center;
                tf.DrawString(K.RACK, new XFont("Calibri", 44, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                gfx.DrawRectangle(sub_pen, pn_box_horizontal_position, pn_box_vertical_position, pn_box_width, pn_box_height);
                rect = new XRect(pn_box_horizontal_position, pn_box_vertical_position + pn_box_height / 8.5, pn_box_width, pn_box_height);
                if (K.MATERIAL.Length <= 13)
                {
                    rect = new XRect(pn_box_horizontal_position, pn_box_vertical_position - 10, pn_box_width, pn_box_height);
                    tf.DrawString(K.MATERIAL, new XFont("Calibri", 62, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                }
                else
                {
                    tf.DrawString(K.MATERIAL, new XFont("Calibri", 44, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                }

                gfx.DrawImage(part__number_barcode_image, rack_box_horizontal_position + rack_box_width + 15, rack_box_vertical_position + 8);

                label_vertical_position = label_vertical_position + label_height + XUnit.FromMillimeter(2);
                rack_box_vertical_position = rack_box_vertical_position + label_height + XUnit.FromMillimeter(2);
                pn_box_vertical_position = pn_box_vertical_position + label_height + XUnit.FromMillimeter(2);


            }




            return document;
        }

        public PdfDocument createrm_work_ara_kanban(KANBAN_MASTER kANBAN_MASTER, string wa_rack, string l_quantity)
        {
            Controllers.getIbusinessData gib = new Controllers.getIbusinessData();
            string vendor = gib.get_part_vendor_name(kANBAN_MASTER.PART_NO);

            //set kanban sizes horizontal
            double kanban_width = XUnit.FromCentimeter(11);
            double pn_desc_width = XUnit.FromCentimeter(1.75);
            double pn_val_width = XUnit.FromCentimeter(7.2);
            double wa_desc_width = XUnit.FromCentimeter(2.05);
            double pn_des_desc_width = pn_desc_width;
            double pn_des_val_width = pn_val_width;
            double wa_barc_width = wa_desc_width;
            double qty_desc_width = pn_des_desc_width;
            double qty_val_width = XUnit.FromCentimeter(1.55);
            double pn_barc_width = qty_val_width + qty_desc_width;
            double bintype_desc_width = (pn_des_val_width - qty_val_width) / 2;
            double bintype_val_width = (pn_des_val_width - qty_val_width) / 2;
            double slider_no_desc_width = qty_desc_width;
            double slider_no_val_width = qty_val_width;
            double line_desc_width = slider_no_desc_width;
            double line_val_width = qty_val_width;
            double supplier_desc_width = line_desc_width;
            double supplier_val_width = line_val_width;
            double kanban_master_desc_width = supplier_desc_width;
            double kanban_master_value_width = line_val_width / 2;
            int kanban_image_width = 275;

            //set kanban sizes vertical
            double kanban_height = XUnit.FromCentimeter(9.6);
            double pn_desc_height = XUnit.FromCentimeter(0.7);
            double pn_val_height = pn_desc_height;
            double wa_desc_height = pn_val_height;
            double pn_des_desc_height = wa_desc_height;
            double qty_desc_height = pn_des_desc_height;
            double qty_val_height = pn_des_desc_height;
            double pn_des_val_height = pn_des_desc_height;
            double wa_barc_height = pn_des_desc_height + qty_desc_height;
            double pn_barc_height = pn_barc_width;
            double bintype_desc_height = qty_desc_height;
            double bintype_val_height = qty_desc_height;
            double slider_no_desc_height = pn_desc_height * 3;
            double slider_no_val_height = slider_no_desc_height;
            double line_desc_height = qty_desc_height;
            double line_val_height = qty_val_height;
            double supplier_desc_height = line_desc_height;
            double supplier_val_height = line_val_height;
            double kanban_master_desc_height = supplier_desc_height;
            double kanban_master_value_height = kanban_master_desc_height;
            int kanban_image_height = 265;

            //set kanban horizontal position
            double kanban_horizontal_position = 50;
            double pn_desc_horizontal_position = kanban_horizontal_position;
            double pn_value_horizontal_position = pn_desc_horizontal_position + pn_desc_width;
            double wa_desc_horizontal_position = kanban_horizontal_position + pn_desc_width;
            double pn_des_desc_horizontal_position = kanban_horizontal_position;
            double pn_des_val_horizontal_position = kanban_horizontal_position + pn_des_desc_width; ;
            double wa_barc_horizontal_position = kanban_horizontal_position + pn_des_val_width + pn_des_desc_width;
            double qty_desc_horizontal_position = pn_des_desc_horizontal_position;
            double qty_val_horizontal_position = qty_desc_horizontal_position + qty_desc_width;
            double pn_barc_horizontal_position = kanban_horizontal_position;
            double bintype_desc_horizontal_position = qty_val_horizontal_position + qty_val_width;
            double bintype_val_horizontal_position = bintype_desc_horizontal_position + bintype_desc_width;
            double slider_no_desc_horizontal_position = kanban_horizontal_position;
            double slider_no_val_horizontal_position = slider_no_desc_horizontal_position + slider_no_desc_width;
            double line_desc_horizontal_position = slider_no_desc_horizontal_position;
            double line_val_horizontal_position = line_desc_horizontal_position + line_desc_width;
            double supplier_desc_horizontal_position = slider_no_desc_horizontal_position;
            double supplier_val_horizontal_position = line_val_horizontal_position;
            double kanban_master_desc_horizontal_position = supplier_desc_horizontal_position;
            double kanban_master_value_horizontal_position = kanban_master_desc_horizontal_position + kanban_master_desc_width;
            double kanban_master2_value_horizontal_position = kanban_master_value_horizontal_position + kanban_master_value_width;
            double kanban_photo_horizontal_position = pn_barc_horizontal_position + pn_barc_width;

            //set kanban vertical position
            double kanban_vertical_position = 10;
            double pn_desc_vertical_position = kanban_vertical_position;
            double pn_value_vertical_position = kanban_vertical_position;
            double wa_desc_vertical_position = kanban_vertical_position;
            double pn_des_desc_vertical_position = kanban_vertical_position + pn_desc_height;
            double pn_des_val_vertical_position = kanban_vertical_position + pn_desc_height;
            double wa_barc_vertical_position = kanban_vertical_position + wa_desc_height;
            double qty_desc_vertical_position = wa_barc_vertical_position + pn_desc_height;
            double qty_val_vertical_position = pn_des_val_vertical_position + pn_des_val_height;
            double pn_barc_vertical_position = qty_desc_vertical_position + qty_desc_height;
            double bintype_desc_vertical_position = pn_des_val_vertical_position + pn_des_val_height;
            double bintype_val_vertical_position = pn_des_val_vertical_position + pn_des_val_height;
            double slider_no_desc_vertical_position = pn_barc_vertical_position + pn_barc_height;
            double slider_no_val_vertical_position = pn_barc_vertical_position + pn_barc_height;
            double line_desc_vertical_position = slider_no_desc_vertical_position + slider_no_desc_height;
            double line_val_vertical_position = line_desc_vertical_position;
            double supplier_desc_vertical_position = line_desc_vertical_position + line_desc_height;
            double supplier_val_vertical_position = line_val_vertical_position + line_val_height;
            double kanban_master_desc_vertical_position = supplier_desc_vertical_position + supplier_desc_height;
            double kanban_master_value_vertical_position = kanban_master_desc_vertical_position;
            double kanban_photo_vertical_position = bintype_desc_vertical_position + bintype_desc_height + 5;


            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPen main_pen = new XPen(XColors.Black, 1.5);
            XPen sub_pen = new XPen(XColors.Black, 1);

            XColor topkanbanColor = XColor.FromArgb(250, 191, 143);

            XBrush brush = new XSolidBrush(topkanbanColor);

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options.Height = 200;
            QCwriter.Options.Width = 200;
            QCwriter.Options.Margin = 0;
            var result = QCwriter.Write(kANBAN_MASTER.PART_NO);
            var barcodeBitmap = new Bitmap(result);
            barcodeBitmap.SetResolution(170.0F, 170.0F);
            System.Drawing.Image part_number_barcode = barcodeBitmap;
            XImage part__number_barcode_image = XImage.FromGdiPlusImage(part_number_barcode);

            Stream ms = new MemoryStream(kANBAN_MASTER.PHOTO);

            System.Drawing.Image images = new Bitmap(ms);
            System.Drawing.Image photo = resizeImage(images, new Size(kanban_image_width, kanban_image_height));
            XImage pho = XImage.FromGdiPlusImage(photo);

            QCwriter.Options.Height = 100;
            QCwriter.Options.Width = 100;
            QCwriter.Options.Margin = 0;
            var result_bin = QCwriter.Write(wa_rack);
            var barcodeBitmapbin = new Bitmap(result_bin);
            barcodeBitmapbin.SetResolution(200.0F, 200.0F);
            System.Drawing.Image bin_wa_rm_barcode = barcodeBitmapbin;
            XImage wh_bin_wa_barcode_image = XImage.FromGdiPlusImage(barcodeBitmapbin);
            int count = 0;
            for (int i = 0; i < 2; i++)
            {
                kanban_horizontal_position = 50;
                pn_desc_horizontal_position = kanban_horizontal_position;
                pn_value_horizontal_position = kanban_horizontal_position + pn_desc_width;
                wa_desc_horizontal_position = pn_value_horizontal_position + pn_val_width;
                pn_des_desc_horizontal_position = kanban_horizontal_position;
                pn_des_val_horizontal_position = kanban_horizontal_position + pn_des_desc_width;
                wa_barc_horizontal_position = kanban_horizontal_position + pn_des_val_width + pn_des_desc_width;
                qty_desc_horizontal_position = pn_des_desc_horizontal_position;
                qty_val_horizontal_position = qty_desc_horizontal_position + qty_desc_width;
                pn_barc_horizontal_position = kanban_horizontal_position;
                bintype_desc_horizontal_position = qty_val_horizontal_position + qty_val_width;
                bintype_val_horizontal_position = bintype_desc_horizontal_position + bintype_desc_width;
                slider_no_desc_horizontal_position = kanban_horizontal_position;
                slider_no_val_horizontal_position = slider_no_desc_horizontal_position + slider_no_desc_width;
                line_desc_horizontal_position = slider_no_desc_horizontal_position;
                line_val_horizontal_position = line_desc_horizontal_position + line_desc_width;
                supplier_desc_horizontal_position = slider_no_desc_horizontal_position;
                supplier_val_horizontal_position = line_val_horizontal_position;
                kanban_master_desc_horizontal_position = supplier_desc_horizontal_position;
                kanban_master_value_horizontal_position = kanban_master_desc_horizontal_position + kanban_master_desc_width;
                kanban_master2_value_horizontal_position = kanban_master_value_horizontal_position + kanban_master_value_width;
                kanban_photo_horizontal_position = pn_barc_horizontal_position + pn_barc_width;

                for (int j = 0; j < 2; j++)
                {
                    count++;
                    //draw main box
                    gfx.DrawRectangle(main_pen, kanban_horizontal_position, kanban_vertical_position, kanban_width, kanban_height);
                    //draw part number description box
                    gfx.DrawRectangle(sub_pen, brush, pn_desc_horizontal_position, pn_desc_vertical_position, pn_desc_width, pn_des_desc_height);
                    XPoint points = new XPoint(pn_desc_horizontal_position + 2, pn_desc_vertical_position + pn_des_desc_height / 2);
                    gfx.DrawString("P/N", new XFont("Arial", 14), XBrushes.Black, points, XStringFormats.CenterLeft);
                    //draw part number value box
                    gfx.DrawRectangle(sub_pen, brush, pn_value_horizontal_position, pn_value_vertical_position, pn_val_width, pn_val_height);
                    points = new XPoint(pn_value_horizontal_position + 5, pn_value_vertical_position + pn_val_height / 2);
                    gfx.DrawString(kANBAN_MASTER.PART_NO, new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.CenterLeft);
                    //draw work ara secription box
                    gfx.DrawRectangle(sub_pen, brush, wa_desc_horizontal_position, wa_desc_vertical_position, wa_desc_width, wa_desc_height);
                    points = new XPoint(wa_desc_horizontal_position + (wa_desc_width / 2), wa_desc_vertical_position + wa_desc_height / 2);
                    gfx.DrawString(wa_rack, new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black, points, XStringFormats.Center);
                    //draw description description box
                    gfx.DrawRectangle(sub_pen, brush, pn_des_desc_horizontal_position, pn_des_desc_vertical_position, pn_des_desc_width, pn_des_desc_height);
                    points = new XPoint(pn_des_desc_horizontal_position + 2, pn_des_desc_vertical_position + pn_des_desc_height / 2);
                    gfx.DrawString("Des ", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.CenterLeft);
                    //draw description value box
                    gfx.DrawRectangle(sub_pen, brush, pn_des_val_horizontal_position, pn_des_val_vertical_position, pn_des_val_width, pn_des_val_height);
                    points = new XPoint(pn_des_val_horizontal_position + 5, pn_des_val_vertical_position + pn_des_val_height / 2);
                    gfx.DrawString("Part number description ", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.CenterLeft);
                    //draw barcode box
                    gfx.DrawRectangle(sub_pen, wa_barc_horizontal_position, wa_barc_vertical_position, wa_barc_width, wa_barc_height);
                    gfx.DrawImage(wh_bin_wa_barcode_image, wa_barc_horizontal_position + 12, wa_barc_vertical_position + 2);
                    //draw quantity description
                    gfx.DrawRectangle(sub_pen, brush, qty_desc_horizontal_position, qty_desc_vertical_position, qty_desc_width, qty_desc_height);
                    points = new XPoint(qty_desc_horizontal_position + 2, qty_desc_vertical_position + qty_desc_height / 2);
                    gfx.DrawString("Qty :", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.CenterLeft);
                    //draw quantity description
                    gfx.DrawRectangle(sub_pen, brush, qty_val_horizontal_position, qty_val_vertical_position, qty_val_width, qty_val_height);
                    points = new XPoint(qty_val_horizontal_position + qty_val_width / 2, qty_val_vertical_position + qty_val_height / 2);
                    gfx.DrawString(l_quantity, new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.Center);
                    //draw part number barcode box
                    gfx.DrawRectangle(sub_pen, pn_barc_horizontal_position, pn_barc_vertical_position, pn_barc_width, pn_barc_height);
                    gfx.DrawImage(part__number_barcode_image, pn_barc_horizontal_position + 5, pn_barc_vertical_position + 5);
                    //draw part number barcode box
                    gfx.DrawRectangle(sub_pen, brush, bintype_desc_horizontal_position, bintype_desc_vertical_position, bintype_desc_width, bintype_desc_height);
                    points = new XPoint(bintype_desc_horizontal_position + bintype_desc_width / 2, bintype_desc_vertical_position + bintype_desc_height / 2);
                    gfx.DrawString("Bin type", new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.Center);
                    //draw part number barcode box
                    gfx.DrawRectangle(sub_pen, brush, bintype_val_horizontal_position, bintype_val_vertical_position, bintype_val_width, bintype_val_height);
                    points = new XPoint(bintype_val_horizontal_position + bintype_val_width / 2, bintype_val_vertical_position + bintype_val_height / 2);
                    gfx.DrawString(kANBAN_MASTER.BIN_TYPE, new XFont("Arial", 12), XBrushes.Black, points, XStringFormats.Center);
                    //draw slider number description
                    gfx.DrawRectangle(sub_pen, brush, slider_no_desc_horizontal_position, slider_no_desc_vertical_position, slider_no_desc_width, slider_no_desc_height);
                    points = new XPoint(slider_no_desc_horizontal_position + slider_no_desc_width / 2, slider_no_desc_vertical_position + slider_no_desc_height / 2);
                    gfx.DrawString("Slider No:", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);


                    //draw slider number description
                    var tf = new XTextFormatter(gfx);
                    tf.Alignment = XParagraphAlignment.Center;
                    gfx.DrawRectangle(sub_pen, brush, slider_no_val_horizontal_position, slider_no_val_vertical_position, slider_no_val_width, slider_no_val_height);
                    XRect rect = new XRect(slider_no_val_horizontal_position, slider_no_val_vertical_position, slider_no_val_width, slider_no_val_height);

                    try
                    {
                        var split_slider_address = kANBAN_MASTER.SLIDER_ADDRESS.Replace(' ', '\n');
                        tf.DrawString(split_slider_address, new XFont("Arial", 8, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                    }
                    catch
                    {
                        tf.DrawString(kANBAN_MASTER.SLIDER_ADDRESS, new XFont("Arial", 8, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);
                    }
                    //draw slider number description
                    gfx.DrawRectangle(sub_pen, brush, line_desc_horizontal_position, line_desc_vertical_position, line_desc_width, line_desc_height);
                    points = new XPoint(line_desc_horizontal_position + line_desc_width / 2, line_desc_vertical_position + line_desc_height / 2);
                    gfx.DrawString("Line:", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);



                    //draw slider number description
                    gfx.DrawRectangle(sub_pen, brush, line_val_horizontal_position, line_val_vertical_position, line_val_width, line_val_height);
                    rect = new XRect(line_val_horizontal_position, line_val_vertical_position, line_val_width, line_val_height);
                    points = new XPoint(line_val_horizontal_position + line_val_width / 2, line_val_vertical_position + line_val_height / 2);
                    try
                    {
                        var spli_line = kANBAN_MASTER.LINE.Replace(' ', '\n');
                        tf.DrawString(spli_line, new XFont("Arial", 8, XFontStyle.Bold), XBrushes.Black, rect, XStringFormats.TopLeft);

                    }
                    catch
                    {
                        gfx.DrawString(kANBAN_MASTER.LINE, new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);
                    }


                    //draw supplier description 
                    gfx.DrawRectangle(sub_pen, brush, supplier_desc_horizontal_position, supplier_desc_vertical_position, supplier_desc_width, supplier_desc_height);
                    points = new XPoint(supplier_desc_horizontal_position + supplier_desc_width / 2, supplier_desc_vertical_position + supplier_desc_height / 2);
                    gfx.DrawString("Suppplier:", new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);
                    //draw supplier value
                    gfx.DrawRectangle(sub_pen, brush, supplier_val_horizontal_position, supplier_val_vertical_position, supplier_val_width, supplier_val_height);
                    points = new XPoint(supplier_val_horizontal_position + supplier_val_width / 2, supplier_val_vertical_position + supplier_val_height / 2);
                    gfx.DrawString(vendor.Substring(0, 5), new XFont("Arial", 9), XBrushes.Black, points, XStringFormats.Center);
                    //draw supplier value
                    gfx.DrawRectangle(sub_pen, brush, kanban_master_desc_horizontal_position, kanban_master_desc_vertical_position, kanban_master_desc_width, kanban_master_desc_height);
                    points = new XPoint(kanban_master_desc_horizontal_position + kanban_master_desc_width / 2, kanban_master_desc_vertical_position + kanban_master_desc_height / 2);
                    gfx.DrawString("Kanban Card:", new XFont("Arial", 7), XBrushes.Black, points, XStringFormats.Center);
                    //draw supplier value
                    gfx.DrawRectangle(sub_pen, brush, kanban_master_value_horizontal_position, kanban_master_value_vertical_position, kanban_master_value_width, kanban_master_value_height);
                    points = new XPoint(kanban_master_value_horizontal_position + kanban_master_value_width / 2, kanban_master_value_vertical_position + kanban_master_value_height / 2);
                    gfx.DrawString("1", new XFont("Arial", 7), XBrushes.Black, points, XStringFormats.Center);
                    //draw supplier value
                    gfx.DrawRectangle(sub_pen, brush, kanban_master2_value_horizontal_position, kanban_master_value_vertical_position, kanban_master_value_width, kanban_master_value_height);
                    points = new XPoint(kanban_master2_value_horizontal_position + kanban_master_value_width / 2, kanban_master_value_vertical_position + kanban_master_value_height / 2);
                    gfx.DrawString(count.ToString(), new XFont("Arial", 7), XBrushes.Black, points, XStringFormats.Center);
                    //draw photo on kanban
                    gfx.DrawImage(pho, kanban_photo_horizontal_position + 5, kanban_photo_vertical_position);

                    //set kanban vertical position
                    kanban_horizontal_position = kanban_horizontal_position + 50 + kanban_width;
                    pn_desc_horizontal_position = pn_desc_horizontal_position + 50 + kanban_width;
                    pn_value_horizontal_position = pn_value_horizontal_position + 50 + kanban_width;
                    wa_desc_horizontal_position = wa_desc_horizontal_position + 50 + kanban_width;
                    pn_des_desc_horizontal_position = pn_des_desc_horizontal_position + 50 + kanban_width;
                    pn_des_val_horizontal_position = pn_des_val_horizontal_position + 50 + kanban_width;
                    wa_barc_horizontal_position = wa_barc_horizontal_position + 50 + kanban_width;
                    qty_desc_horizontal_position = qty_desc_horizontal_position + 50 + kanban_width;
                    qty_val_horizontal_position = qty_val_horizontal_position + 50 + kanban_width;
                    pn_barc_horizontal_position = pn_barc_horizontal_position + 50 + kanban_width;
                    bintype_desc_horizontal_position = bintype_desc_horizontal_position + 50 + kanban_width;
                    bintype_val_horizontal_position = bintype_val_horizontal_position + 50 + kanban_width;
                    slider_no_desc_horizontal_position = slider_no_desc_horizontal_position + 50 + kanban_width;
                    slider_no_val_horizontal_position = slider_no_val_horizontal_position + 50 + kanban_width;
                    line_desc_horizontal_position = line_desc_horizontal_position + 50 + kanban_width;
                    line_val_horizontal_position = line_val_horizontal_position + 50 + kanban_width;
                    supplier_desc_horizontal_position = supplier_desc_horizontal_position + 50 + kanban_width;
                    supplier_val_horizontal_position = supplier_val_horizontal_position + 50 + kanban_width;
                    kanban_master_desc_horizontal_position = kanban_master_desc_horizontal_position + 50 + kanban_width;
                    kanban_master_value_horizontal_position = kanban_master_value_horizontal_position + 50 + kanban_width;
                    kanban_master2_value_horizontal_position = kanban_master2_value_horizontal_position + 50 + kanban_width;
                    kanban_photo_horizontal_position = kanban_photo_horizontal_position + 50 + kanban_width;
                }
                kanban_vertical_position = kanban_vertical_position + kanban_height + 10;
                pn_desc_vertical_position = pn_desc_vertical_position + kanban_height + 10;
                pn_value_vertical_position = pn_value_vertical_position + kanban_height + 10;
                wa_desc_vertical_position = wa_desc_vertical_position + kanban_height + 10;
                pn_des_desc_vertical_position = pn_des_desc_vertical_position + kanban_height + 10;
                pn_des_val_vertical_position = pn_des_val_vertical_position + kanban_height + 10;
                wa_barc_vertical_position = wa_barc_vertical_position + kanban_height + 10;
                qty_desc_vertical_position = qty_desc_vertical_position + kanban_height + 10;
                qty_val_vertical_position = qty_val_vertical_position + kanban_height + 10;
                pn_barc_vertical_position = pn_barc_vertical_position + kanban_height + 10;
                bintype_desc_vertical_position = bintype_desc_vertical_position + kanban_height + 10;
                bintype_val_vertical_position = bintype_val_vertical_position + kanban_height + 10;
                slider_no_desc_vertical_position = slider_no_desc_vertical_position + kanban_height + 10;
                slider_no_val_vertical_position = slider_no_val_vertical_position + kanban_height + 10;
                line_desc_vertical_position = line_desc_vertical_position + kanban_height + 10;
                line_val_vertical_position = line_val_vertical_position + kanban_height + 10;
                supplier_desc_vertical_position = supplier_desc_vertical_position + kanban_height + 10;
                supplier_val_vertical_position = supplier_val_vertical_position + kanban_height + 10;
                kanban_master_desc_vertical_position = kanban_master_desc_vertical_position + kanban_height + 10;
                kanban_master_value_vertical_position = kanban_master_value_vertical_position + kanban_height + 10;
                kanban_photo_vertical_position = kanban_photo_vertical_position + kanban_height + 10;
            }




            return document;
        }

    }
}