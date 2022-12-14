using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using ZXing;


namespace WebApplication1.Controllers
{
    public class GeneratePullListPDF
    {
        public PdfDocument generatePullList(DataTable pullData)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "SMT Pull List";
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            XRect rect = new XRect(XUnit.FromCentimeter(7), XUnit.FromCentimeter(0.5), XUnit.FromCentimeter(18), XUnit.FromCentimeter(1));
            tf.DrawString("SMTMaterialPullList " + DateTime.Now.ToString("yyyy"), new XFont("Times New Roman", 20, XFontStyle.Regular), XBrushes.Black, rect, XStringFormats.TopLeft);

            XRect dateRect = new XRect(XUnit.FromCentimeter(14.5), XUnit.FromCentimeter(1.3), XUnit.FromCentimeter(7), XUnit.FromCentimeter(0.5));
            tf.DrawString("Date \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + DateTime.Now.ToString("dd/MM/yyyy"), new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, dateRect, XStringFormats.TopLeft);

            XRect line3Rect = new XRect(XUnit.FromCentimeter(10.4), XUnit.FromCentimeter(1.7), XUnit.FromCentimeter(18), XUnit.FromCentimeter(0.5));
            tf.DrawString("Shift B \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t NO PULL LIST \t\t\t\t\t\t\t\t\t 00000001", new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, line3Rect, XStringFormats.TopLeft);

            XRect line4Rect = new XRect(XUnit.FromCentimeter(10.4), XUnit.FromCentimeter(2.2), XUnit.FromCentimeter(18), XUnit.FromCentimeter(0.5));
            tf.DrawString("SMT 6", new XFont("Calibri", 14, XFontStyle.Regular), XBrushes.Black, line4Rect, XStringFormats.TopLeft);

            XRect line4Col1Rect = new XRect(XUnit.FromCentimeter(13.5), XUnit.FromCentimeter(2.2), XUnit.FromCentimeter(12), XUnit.FromCentimeter(0.5));
            tf.DrawString("TIME REQUEST \t\t\t\t\t\t\t\t\t" + DateTime.Now.ToString("hh:mm:ss"), new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, line4Col1Rect, XStringFormats.TopLeft);

            XRect line6Col2Rect = new XRect(XUnit.FromCentimeter(13.5), XUnit.FromCentimeter(2.7), XUnit.FromCentimeter(12), XUnit.FromCentimeter(0.5));
            tf.DrawString("TIME RECEIVE", new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, line6Col2Rect, XStringFormats.TopLeft);

            double noColHeight = XUnit.FromMillimeter(5);
            double PartNumberColHeight = noColHeight;
            double StgeQtyColHeight = PartNumberColHeight;
            double RecvQtyColHeight = StgeQtyColHeight;
            double RefLocColHeight = RecvQtyColHeight;
            double RefNumReelColHeight = RefLocColHeight;
            double BcodeColHeight = RefNumReelColHeight;

            double noColWidth = XUnit.FromMillimeter(9.5);
            double PartNumberColWidth = XUnit.FromMillimeter(35);
            double ShtgeQtyColWidth = XUnit.FromMillimeter(20);
            double RecvQtyColWidth = XUnit.FromMillimeter(19.5);
            double RefLocColWidth = XUnit.FromMillimeter(20.1);
            double RefNumReelColWidth = XUnit.FromMillimeter(24);
            double BcodeColWidth = XUnit.FromMillimeter(70);

            double HeaderXnoPos = 10;
            double HeaderXPartNumberPos = HeaderXnoPos + noColWidth;
            double HeaderXSthgeQtyPos = HeaderXPartNumberPos + PartNumberColWidth;
            double HeaderXRecvQtyPos = HeaderXSthgeQtyPos + ShtgeQtyColWidth;
            double HeaderXRefLocPos = HeaderXRecvQtyPos + RecvQtyColWidth;
            double HeaderXRefNumReelPos = HeaderXRefLocPos + RefLocColWidth;
            double HeaderXBcodePos = HeaderXRefNumReelPos + RefNumReelColWidth;

            double HeaderYnoColPos = XUnit.FromCentimeter(3.3);
            double HeaderYPartNumberPos = HeaderYnoColPos;
            double HeaderYShtgeQtyPos = HeaderYPartNumberPos;
            double HeaderYRecvQtyPos = HeaderYShtgeQtyPos;
            double HeaderYRefLocPos = HeaderYRecvQtyPos;
            double HeaderYRefNumReelPos = HeaderYRefLocPos;
            double HeaderYBcodePos = HeaderYRefNumReelPos;
            XColor HeadernotoRecColor = XColor.FromArgb(31, 73, 125);

            XBrush HeadernotoRecbrush = new XSolidBrush(HeadernotoRecColor);

            XColor HeaderREFloctoBcodeColor = XColor.FromArgb(204, 51, 0);

            XBrush HeaderREFloctoBcodebrush = new XSolidBrush(HeaderREFloctoBcodeColor);

            //No Header Column
            gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, HeaderXnoPos, HeaderYnoColPos, noColWidth, noColHeight);
            rect = new XRect(HeaderXnoPos + XUnit.FromMillimeter(1), HeaderYnoColPos + XUnit.FromMillimeter(0.5), noColWidth, noColHeight);
            tf.DrawString(" NO ", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rect, XStringFormats.TopLeft);

            //PartNumber Header Column
            gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, HeaderXPartNumberPos, HeaderYPartNumberPos, PartNumberColWidth, PartNumberColHeight);
            rect = new XRect(HeaderXPartNumberPos + XUnit.FromMillimeter(1), HeaderYPartNumberPos + XUnit.FromMillimeter(0.5), PartNumberColWidth, PartNumberColHeight);
            tf.DrawString(" Part Number", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rect, XStringFormats.TopLeft);

            //SHortage Quantity Header Column
            gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, HeaderXSthgeQtyPos, HeaderYShtgeQtyPos, ShtgeQtyColWidth, StgeQtyColHeight);
            rect = new XRect(HeaderXSthgeQtyPos, HeaderYShtgeQtyPos, ShtgeQtyColWidth, StgeQtyColHeight);
            tf.DrawString(" ShortageQty", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rect, XStringFormats.TopLeft);

            //Recieved Quantity Header Column
            gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, HeaderXRecvQtyPos, HeaderYRecvQtyPos, RecvQtyColWidth, RecvQtyColHeight);
            rect = new XRect(HeaderXRecvQtyPos, HeaderYRecvQtyPos, RecvQtyColWidth, RecvQtyColHeight);
            tf.DrawString(" ReceivedQty", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rect, XStringFormats.TopLeft);

            //Reference Loc Header Column
            gfx.DrawRectangle(XPens.Black, HeaderREFloctoBcodebrush, HeaderXRefLocPos, HeaderYRefLocPos, RefLocColWidth, RefLocColHeight);
            rect = new XRect(HeaderXRefLocPos, HeaderYRefLocPos, RefLocColWidth, RefLocColHeight);
            tf.DrawString(" Ref.Location", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rect, XStringFormats.TopLeft);

            //Reference Num Reel Header Column
            gfx.DrawRectangle(XPens.Black, HeaderREFloctoBcodebrush, HeaderXRefNumReelPos, HeaderYRefNumReelPos, RefNumReelColWidth, RefNumReelColHeight);
            rect = new XRect(HeaderXRefNumReelPos, HeaderYRefNumReelPos, RefNumReelColWidth, RefNumReelColHeight);
            tf.DrawString(" Ref.NumofReel", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rect, XStringFormats.TopLeft);

            //Barcode Header Column
            gfx.DrawRectangle(XPens.Black, HeaderREFloctoBcodebrush, HeaderXBcodePos, HeaderYBcodePos, BcodeColWidth, BcodeColHeight);
            rect = new XRect(HeaderXBcodePos + XUnit.FromMillimeter(15), HeaderYBcodePos, BcodeColWidth, BcodeColHeight);
            tf.DrawString(" BARCODE", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rect, XStringFormats.TopLeft);

            double rownoColHeight = XUnit.FromMillimeter(12);
            double rowPartNumHeight = rownoColHeight;
            double rowShortageQtyHeight = rowPartNumHeight;
            double rowRecvQtyHeight = rowShortageQtyHeight;
            double rowRefLocHeight = rowRecvQtyHeight;
            double rowRefNumOfReelHeight = rowRefLocHeight;
            double rowPlnrCodeHeight = rowRefNumOfReelHeight;
            double rowBcodeHeight = rowPlnrCodeHeight;

            double rownoColWidth = noColWidth;
            double rowPartNumberWidth = PartNumberColWidth;
            double rowShortageQtyWidth = ShtgeQtyColWidth;
            double rowRecvQtyWidth = RecvQtyColWidth;
            double rowRefLocWidth = RefLocColWidth;
            double rowRefNumOfReelWidth = RefNumReelColWidth;
            double rowBcodeWidth = BcodeColWidth;

            double RowXnoPos = HeaderXnoPos;
            double RowXPNPos = HeaderXPartNumberPos;
            double RowXShtgeQtyPos = HeaderXSthgeQtyPos;
            double RowXRecvQtyPos = HeaderXRecvQtyPos;
            double RowXRefLocpOS = HeaderXRefLocPos;
            double RowXrowRefNumOfReelPos = HeaderXRefNumReelPos;
            double RowXBcodePos = HeaderXBcodePos;

            double RowYnoColPos = HeaderYnoColPos + noColHeight;
            double RowYPNColPos = HeaderYPartNumberPos + PartNumberColHeight;
            double RowYShtgeQtyPos = HeaderYShtgeQtyPos + StgeQtyColHeight;
            double RowYRecvQtyPos = HeaderYRecvQtyPos + RecvQtyColHeight;
            double RowYRefLocPos = HeaderYRefLocPos + RefLocColHeight;
            double RowYrowRefNumOfReelPos = HeaderYRefNumReelPos + RefNumReelColHeight;
            double RowYBcodePos = HeaderYBcodePos + BcodeColHeight;

            int rowCount = 0;
            int pageCount = 0;

            double getrowLastXPpos = 0;
            double getrowLastYPpos = 0;



            for (int i = 0; i < pullData.Rows.Count; i++)
            {
                XTextFormatter picTF = new XTextFormatter(gfx);



                rowCount++;
                if (rowCount > 20)
                {
                    //Generate New Page 
                    pageCount++;
                    //Generate new age Footer
                    double picRequestHeight = XUnit.FromMillimeter(16);
                    double picIssueHeight = picRequestHeight;
                    double confBinHeight = picIssueHeight;

                    double picRequestWidth = XUnit.FromMillimeter(60);
                    double picIssueWidth = picRequestWidth;
                    double confBinWidth = picIssueWidth;

                    double picRequestX = RowXnoPos + XUnit.FromMillimeter(15);
                    double picIssueX = picRequestX + picRequestWidth;
                    double confBoxX = picIssueX + picIssueWidth;

                    double picRequestY = RowYnoColPos + XUnit.FromMillimeter(2.5);
                    double picIssueY = picRequestY;
                    double confBoxY = picRequestY;

                    gfx.DrawRectangle(XPens.Black, picRequestX - XUnit.FromMillimeter(5), picRequestY - XUnit.FromMillimeter(1.3), picRequestWidth, picRequestHeight);

                    gfx.DrawRectangle(XPens.Black, picIssueX - XUnit.FromMillimeter(5), picIssueY - XUnit.FromMillimeter(1.3), picIssueWidth, picIssueHeight);

                    gfx.DrawRectangle(XPens.Black, confBoxX - XUnit.FromMillimeter(5), confBoxY - XUnit.FromMillimeter(1.3), confBinWidth, confBinHeight);

                    page = document.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    //Generate signature box
                    //gfx.DrawRoundedRectangle(XPens.Black, signBoxX, signBoxY, signBoxWidth, signBoxHeight, 20, 20);
                    //generate Graphics of new page
                    gfx = XGraphics.FromPdfPage(page);

                    //regenerate Header
                    XTextFormatter Titletf = new XTextFormatter(gfx);
                    XTextFormatter npDatetf = new XTextFormatter(gfx);
                    XTextFormatter npPlNotf = new XTextFormatter(gfx);
                    XTextFormatter npSMTtf = new XTextFormatter(gfx);
                    XTextFormatter npTimeRequestRecttf = new XTextFormatter(gfx);
                    XTextFormatter npGnjiltf = new XTextFormatter(gfx);
                    XTextFormatter TimeRecvtf = new XTextFormatter(gfx);

                    XRect Titlerect = new XRect(XUnit.FromCentimeter(7), XUnit.FromCentimeter(0.5), XUnit.FromCentimeter(18), XUnit.FromCentimeter(1));
                    Titletf.DrawString("SMTMaterialPullList " + DateTime.Now.ToString("yyyy"), new XFont("Times New Roman", 20, XFontStyle.Regular), XBrushes.Black, Titlerect, XStringFormats.TopLeft);

                    XRect npdateRect = new XRect(XUnit.FromCentimeter(14.5), XUnit.FromCentimeter(1.3), XUnit.FromCentimeter(7), XUnit.FromCentimeter(0.5));
                    npDatetf.DrawString("Date \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + DateTime.Now.ToString("dd/MM/yyyy"), new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, dateRect, XStringFormats.TopLeft);

                    XRect PlNoRect = new XRect(XUnit.FromCentimeter(10.4), XUnit.FromCentimeter(1.7), XUnit.FromCentimeter(18), XUnit.FromCentimeter(0.5));
                    npPlNotf.DrawString("Shift B \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t NO PULL LIST \t\t\t\t\t\t\t\t\t 00000001", new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, PlNoRect, XStringFormats.TopLeft);

                    XRect SMTRect = new XRect(XUnit.FromCentimeter(10.4), XUnit.FromCentimeter(2.2), XUnit.FromCentimeter(18), XUnit.FromCentimeter(0.5));
                    npSMTtf.DrawString("SMT 6", new XFont("Calibri", 14, XFontStyle.Regular), XBrushes.Black, SMTRect, XStringFormats.TopLeft);

                    XRect TimeRequestRect = new XRect(XUnit.FromCentimeter(13.5), XUnit.FromCentimeter(2.2), XUnit.FromCentimeter(12), XUnit.FromCentimeter(0.5));
                    npTimeRequestRecttf.DrawString("TIME REQUEST \t\t\t\t\t\t\t\t\t" + DateTime.Now.ToString("hh:mm:ss"), new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, TimeRequestRect, XStringFormats.TopLeft);

                    XRect TimeRecvRect = new XRect(XUnit.FromCentimeter(13.5), XUnit.FromCentimeter(2.7), XUnit.FromCentimeter(12), XUnit.FromCentimeter(0.5));
                    TimeRecvtf.DrawString("TIME RECEIVE", new XFont("Times New Roman", 12, XFontStyle.Regular), XBrushes.Black, TimeRecvRect, XStringFormats.TopLeft);

                   
                    XRect picRequestRect = new XRect(picRequestX, picRequestY, picRequestWidth, picRequestHeight);
                    picTF.DrawString("PIC REQUEST : \n\n _________________________", new XFont("Arial", 10, XFontStyle.Regular), XBrushes.Black, picRequestRect, XStringFormats.TopLeft);

                    XRect picIssueRect = new XRect(picIssueX, picIssueY, picIssueWidth, picIssueHeight);
                    picTF.DrawString("PIC ISSUE   : \n\n _________________________", new XFont("Arial", 10, XFontStyle.Regular), XBrushes.Black, picIssueRect, XStringFormats.TopLeft);

                    XRect confByRect = new XRect(confBoxX, confBoxY, confBinWidth, confBinHeight);
                    picTF.DrawString("CONFIRM BY PIC RECEIVED :\n\n _________________________", new XFont("Arial", 10, XFontStyle.Regular), XBrushes.Black, confByRect, XStringFormats.TopLeft);

                    //Reset Position of X and Y to generate new rows in new page
                    RowXnoPos = HeaderXnoPos;
                    RowXPNPos = HeaderXPartNumberPos;

                    int distanceFromTop = 0;
                    RowYnoColPos = HeaderYnoColPos + noColHeight - XUnit.FromMillimeter(distanceFromTop);
                    RowYPNColPos = HeaderYPartNumberPos + PartNumberColHeight - XUnit.FromMillimeter(distanceFromTop);
                    RowYShtgeQtyPos = HeaderYShtgeQtyPos + StgeQtyColHeight - XUnit.FromMillimeter(distanceFromTop);
                    RowYRecvQtyPos = HeaderYRecvQtyPos + RecvQtyColHeight - XUnit.FromMillimeter(distanceFromTop);
                    RowYRefLocPos = HeaderYRefLocPos + RefLocColHeight - XUnit.FromMillimeter(distanceFromTop);
                    RowYrowRefNumOfReelPos = HeaderYRefNumReelPos + RefNumReelColHeight - XUnit.FromMillimeter(distanceFromTop);
                    RowYBcodePos = HeaderYBcodePos + BcodeColHeight - XUnit.FromMillimeter(distanceFromTop);

                    //generate new page header
                    double npHeaderXnoPos = RowXnoPos;
                    double npHeaderXPartNumberPos = npHeaderXnoPos + noColWidth;
                    double npHeaderXShortageQty = npHeaderXPartNumberPos + PartNumberColWidth;
                    double npHeaderXRecvQty = npHeaderXShortageQty + ShtgeQtyColWidth;
                    double npHeaderXRefLoc = npHeaderXRecvQty + RecvQtyColWidth;
                    double npHeaderXRefNumReel = npHeaderXRefLoc + RefLocColWidth;
                    double npHeaderXBcode = npHeaderXRefNumReel + RefNumReelColWidth;

                    double npHeaderYnoColPos = RowYnoColPos - noColHeight;
                    double npHeaderYPartNumberPos = npHeaderYnoColPos;
                    double npHeaderYShortageQty = npHeaderYPartNumberPos;
                    double npHeaderYRecvQty = npHeaderYShortageQty;
                    double npHeaderYRefLoc = npHeaderYRecvQty;
                    double npHeaderYRefNumReel = npHeaderYRefLoc;
                    double npHeaderYBcode = npHeaderYRefNumReel;

                    gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, npHeaderXnoPos, npHeaderYnoColPos, noColWidth ,noColHeight);
                    XRect rectnpcolno = new XRect(npHeaderXnoPos + XUnit.FromMillimeter(1), npHeaderYnoColPos + XUnit.FromMillimeter(0.5), noColWidth, noColHeight);
                    XTextFormatter npcolnoTF = new XTextFormatter(gfx);
                    npcolnoTF.DrawString(" NO ", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rectnpcolno, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, npHeaderXPartNumberPos, npHeaderYPartNumberPos, PartNumberColWidth, PartNumberColHeight);
                    XRect rectnpPartno = new XRect(npHeaderXPartNumberPos + XUnit.FromMillimeter(1), npHeaderYPartNumberPos + XUnit.FromMillimeter(0.5), PartNumberColWidth, PartNumberColHeight);
                    XTextFormatter npcolPartNumTF = new XTextFormatter(gfx);
                    npcolPartNumTF.DrawString(" Part Number", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rectnpPartno, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, npHeaderXShortageQty, npHeaderYShortageQty, ShtgeQtyColWidth, StgeQtyColHeight);
                    XRect rectnpShtgeQty = new XRect(npHeaderXShortageQty, npHeaderYShortageQty, ShtgeQtyColWidth, StgeQtyColHeight);
                    XTextFormatter npcolShtgeQtyTF = new XTextFormatter(gfx);
                    npcolShtgeQtyTF.DrawString(" ShortageQty", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rectnpShtgeQty, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, HeadernotoRecbrush, npHeaderXRecvQty, npHeaderYRecvQty, RecvQtyColWidth, RecvQtyColHeight);
                    XRect rectnpRecvQty = new XRect(npHeaderXRecvQty, npHeaderYRecvQty, RecvQtyColWidth, RecvQtyColHeight);
                    XTextFormatter npcolRecvQtyTF = new XTextFormatter(gfx);
                    npcolRecvQtyTF.DrawString(" RecievedQty", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rectnpRecvQty, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, HeaderREFloctoBcodebrush, npHeaderXRefLoc, npHeaderYRefLoc, RefLocColWidth, RefLocColHeight);
                    XRect rectnpRefLoc = new XRect(npHeaderXRefLoc, npHeaderYRefLoc, RefLocColWidth, RefLocColHeight);
                    XTextFormatter npcolRefLocTF = new XTextFormatter(gfx);
                    npcolRefLocTF.DrawString(" Ref.Location", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rectnpRefLoc, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, HeaderREFloctoBcodebrush, npHeaderXRefNumReel, npHeaderYRefNumReel, RefNumReelColWidth, RefNumReelColHeight);
                    XRect rectnpRefNumReel = new XRect(npHeaderXRefNumReel, npHeaderYRefNumReel, RefNumReelColWidth, RefNumReelColHeight);
                    XTextFormatter npcolRefNumReelTF = new XTextFormatter(gfx);
                    npcolRefNumReelTF.DrawString(" Ref.NumofReel", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rectnpRefNumReel, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, HeaderREFloctoBcodebrush, npHeaderXBcode, npHeaderYBcode, BcodeColWidth, BcodeColHeight);
                    XRect rectnpBCode = new XRect(npHeaderXBcode + XUnit.FromMillimeter(15), npHeaderYBcode, BcodeColWidth, BcodeColHeight);
                    XTextFormatter npcolBCodeTF = new XTextFormatter(gfx);
                    npcolBCodeTF.DrawString(" BARCODE", new XFont("Times New Roman", 10, XFontStyle.Regular), XBrushes.White, rectnpBCode, XStringFormats.TopLeft);


                    rowCount = 0;
                }

                var QCwriter = new BarcodeWriter();
                QCwriter.Format = BarcodeFormat.CODE_128;
                QCwriter.Options.Height = 39;
                QCwriter.Options.Width = 25;
                QCwriter.Options.Margin = 5;
                QCwriter.Options.PureBarcode = false;
                var result = QCwriter.Write(pullData.Rows[i][1].ToString());
                var barcodeBitmap = new Bitmap(result);


                //No Row col
                gfx.DrawRectangle(XPens.Black, RowXnoPos, RowYnoColPos, rownoColWidth, rownoColHeight);
                XPoint p = new XPoint(RowXnoPos + rownoColWidth / 2, RowYnoColPos + noColHeight);
                gfx.DrawString((i + 1).ToString(), new XFont("Arial", 10), XBrushes.Black, p, XStringFormats.Center);

                //Part Number col
                gfx.DrawRectangle(XPens.Black, RowXPNPos, RowYPNColPos, rowPartNumberWidth, rowPartNumHeight);
                XPoint PNp = new XPoint(RowXPNPos + rowPartNumberWidth / 2, RowYPNColPos + rowPartNumHeight/2);
                gfx.DrawString(pullData.Rows[i][1].ToString(), new XFont("Arial", 10), XBrushes.Black, PNp, XStringFormats.Center);

                //Shortage Quantity col
                gfx.DrawRectangle(XPens.Black, RowXShtgeQtyPos, RowYShtgeQtyPos, rowShortageQtyWidth, rowShortageQtyHeight);
                XPoint ShrtgeQtyp = new XPoint(RowXShtgeQtyPos + rowShortageQtyWidth / 2, RowYShtgeQtyPos + rowShortageQtyHeight / 2);
                gfx.DrawString(pullData.Rows[i][2].ToString(), new XFont("Arial", 10), XBrushes.Black, ShrtgeQtyp, XStringFormats.Center);

                //Received Quantity Col
                gfx.DrawRectangle(XPens.Black, RowXRecvQtyPos, RowYRecvQtyPos, rowRecvQtyWidth, rowRecvQtyHeight);
                XPoint RecvQtyp = new XPoint(RowXRecvQtyPos + rowRecvQtyWidth / 2, RowYRecvQtyPos + rowRecvQtyHeight / 2);
                gfx.DrawString("", new XFont("Arial", 10), XBrushes.Black, RecvQtyp, XStringFormats.Center);

                //Ref location Col
                gfx.DrawRectangle(XPens.Black, RowXRefLocpOS, RowYRefLocPos, rowRefLocWidth, rowRefLocHeight);
                XPoint RefLocp = new XPoint(RowXRefLocpOS + rowRefLocWidth / 2, RowYRefLocPos + rowRefLocHeight / 2);
                gfx.DrawString(pullData.Rows[i][4].ToString(), new XFont("Arial", 10), XBrushes.Black, RefLocp, XStringFormats.Center);

                //Ref location Col
                gfx.DrawRectangle(XPens.Black, RowXrowRefNumOfReelPos, RowYrowRefNumOfReelPos, rowRefNumOfReelWidth, rowRefNumOfReelHeight);
                XPoint RefReelNump = new XPoint(RowXrowRefNumOfReelPos + rowRefNumOfReelWidth / 2, RowYrowRefNumOfReelPos + rowRefNumOfReelHeight / 2);
                gfx.DrawString(pullData.Rows[i][6].ToString(), new XFont("Arial", 10), XBrushes.Black, RefReelNump, XStringFormats.Center);

                //Barcode Col
                gfx.DrawRectangle(XPens.Black, RowXBcodePos, RowYBcodePos, rowBcodeWidth, rowBcodeHeight);
                gfx.DrawImage(barcodeBitmap, RowXBcodePos + XUnit.FromMillimeter(1), RowYBcodePos + XUnit.FromMillimeter(1.3));





                RowYnoColPos = RowYnoColPos + rownoColHeight;
                RowYPNColPos = RowYPNColPos + rowPartNumHeight;
                RowYShtgeQtyPos = RowYShtgeQtyPos + rowShortageQtyHeight;
                RowYRecvQtyPos = RowYRecvQtyPos + rowRecvQtyHeight;
                RowYRefLocPos = RowYRefLocPos + rowRefLocHeight;
                RowYrowRefNumOfReelPos = RowYrowRefNumOfReelPos + rowRefNumOfReelHeight;
                RowYBcodePos = RowYBcodePos + rowBcodeHeight;

               

                if (i == pullData.Rows.Count - 1)
                {
                    


                    XTextFormatter picTFLast = new XTextFormatter(gfx);
                    double picRequestHeightLast = XUnit.FromMillimeter(16);
                    double picIssueHeightLast = picRequestHeightLast;
                    double confBinHeightLast = picIssueHeightLast;

                    double picRequestWidthLast = XUnit.FromMillimeter(60);
                    double picIssueWidthLast = picRequestWidthLast;
                    double confBinWidthLast = picIssueWidthLast;

                    double picRequestXLast = RowXnoPos + XUnit.FromMillimeter(15);
                    double picIssueXLast = picRequestXLast + picRequestWidthLast;
                    double confBoxXLast = picIssueXLast + picIssueWidthLast;

                    double picRequestYLast = RowYnoColPos + XUnit.FromMillimeter(2.5);
                    double picIssueYLast = picRequestYLast;
                    double confBoxYLast = picRequestYLast;

                    gfx.DrawRectangle(XPens.Black, picRequestXLast - XUnit.FromMillimeter(5), picRequestYLast - XUnit.FromMillimeter(1.3), picRequestWidthLast, picRequestHeightLast);
                    XRect picRequestRectLast = new XRect(picRequestXLast, picRequestYLast, picRequestWidthLast, picRequestHeightLast);
                    picTFLast.DrawString("PIC REQUEST :\n\n _________________________", new XFont("Arial", 10, XFontStyle.Regular), XBrushes.Black, picRequestRectLast, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, picIssueXLast - XUnit.FromMillimeter(5), picIssueYLast - XUnit.FromMillimeter(1.3), picIssueWidthLast, picIssueHeightLast);
                    XRect picIssueRectLast = new XRect(picIssueXLast, picIssueYLast, picIssueWidthLast, picIssueHeightLast);
                    picTFLast.DrawString("PIC ISSUE   :\n\n _________________________", new XFont("Arial", 10, XFontStyle.Regular), XBrushes.Black, picIssueRectLast, XStringFormats.TopLeft);

                    gfx.DrawRectangle(XPens.Black, confBoxXLast - XUnit.FromMillimeter(5), confBoxYLast - XUnit.FromMillimeter(1.3), confBinWidthLast, confBinHeightLast);
                    XRect confByRect = new XRect(confBoxXLast, confBoxYLast, confBinWidthLast, confBinHeightLast);
                    picTF.DrawString("CONFIRM BY PIC RECEIVED :\n\n _________________________", new XFont("Arial", 10, XFontStyle.Regular), XBrushes.Black, confByRect, XStringFormats.TopLeft);


                    
                }


            }
            UpdateTable(pullData);
            return document;
        }

        private void UpdateTable(DataTable dt)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApplication1Context"].ConnectionString);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                string matTemp = dt.Rows[i][1].ToString();
                string sql = "UPDATE [dbo].[SMT_PULLLIST] SET [PRINTED] = @flag WHERE [PART_NUMBER] = @mat";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                SqlParameter p_mat = new SqlParameter();
                p_mat.ParameterName = "@mat";
                p_mat.Value = matTemp;
                p_mat.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p_flag = new SqlParameter();
                p_flag.ParameterName = "@flag";
                p_flag.Value = "1";
                p_flag.SqlDbType= SqlDbType.NVarChar;
                cmd.Parameters.Add(p_flag);
                cmd.Parameters.Add(p_mat);

                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {

                }
                finally
                {
                    cnn.Close();
                }


            }
        }

    }
}