using IronBarCode;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Controls;

namespace Cashier.Model
{
    class PrintManager                                                  //Used for print operations
    {
        private string printText;

        public PrintManager()
        {
            printText = "";
        }

        public void PrintNewLabel(string printText)                     //Method what prints a label with barcode on warehouse window "Print" click and on newly created warehouse item.
        {
            this.printText = printText;                                 //Save Text in variable, will be used in PrintLabelPage

            PrintDocument printDocument = new PrintDocument();          //Create Print Document object
            printDocument.DocumentName = "Label №: " + printText;       //Define name of document

            PaperSize paperSize = new PaperSize();                      //Define label size
            //paperSize.Width = 157;
            //paperSize.Height = 110;
            paperSize.RawKind = 9;
            
            try
            {
                printDocument.PrintPage += PrintLabelPage;
                printDocument.DefaultPageSettings.PaperSize = paperSize;
                printDocument.Print();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                System.Windows.MessageBox.Show("File is opened! Close file first!");
            }
        }

        private void PrintLabelPage(object o, PrintPageEventArgs e)                                         //Methot what print an image of barcode in a file. Receives text from printText string.
        {
            var MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode(printText, BarcodeEncoding.Code128);    //Create barcode from text.
            MyBarCode.ResizeTo(157, 110);                                                                   //Change size of barcode.

            //MyBarCode.SaveAsPng(@"C:\Users\Administrator\Documents\barcode.png");

            e.PageSettings.PaperSize.Height = MyBarCode.Height;
            e.PageSettings.PaperSize.Width = MyBarCode.Width;
            
            System.Drawing.Image barcodeImage = MyBarCode.ToImage();                                        //Parse barcode to image and draw it in a file.
            System.Drawing.Point loc = new System.Drawing.Point(0, 24);
            e.Graphics.DrawImage(barcodeImage, loc);
        }

        //Method what print an receipt, building it by line by line;
        public void PrintNewReceipt(string historyNo, DateTime operationDate, ObservableCollection<OperationItem> operationItems, string totalSum)
        {
            string InvoiceNo = historyNo;
            string gross = totalSum;                     
            // decimal net = Convert.ToInt32(txtNet.Text);       //Reserved. Could be used in a future
            //decimal discount = gross - net;                   //Reserved. Could be used in a future
            string InvoiceDate = operationDate.ToShortDateString() + " " + operationDate.ToShortTimeString();       //Date of invoice

            int lineHeight = 20;                                 //Line height in px
            int supplementaryLines = 12;                         //Amount of lines. Used for calculation of receipt height

            int bitmapLength = 285;                              //Witdh of receipt

            Bitmap bitm = new Bitmap(bitmapLength, ((supplementaryLines * lineHeight) + ((operationItems.Count-1)*50))+ 150);       //Create a bitmap image. Calculating width of receipt.
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            using (Graphics graphic = Graphics.FromImage(bitm))
            {
                int startX = 0;
                int startY = 0;
                int offsetY = 0;
                Font newfont2 = null;
                Font itemFont = null;
                SolidBrush black = null;
                SolidBrush white = null;

                try
                {
                    //Font newfont = new Font("Arial Black", 8);
                    newfont2 = new Font("Calibri", 11);
                    itemFont = new Font("Calibri", 9);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.Alignment = StringAlignment.Center;

                    black = new SolidBrush(Color.Black);
                    white = new SolidBrush(Color.White);

                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Near;
                    drawFormat.LineAlignment = StringAlignment.Center;

                    StringFormat drawFormatCenter = new StringFormat();
                    drawFormatCenter.Alignment = StringAlignment.Center;
                    drawFormatCenter.LineAlignment = StringAlignment.Center;

                    StringFormat drawFormatFar = new StringFormat();
                    drawFormatFar.Alignment = StringAlignment.Far;
                    drawFormatFar.LineAlignment = StringAlignment.Center;

                    float x = 0.0F;
                    float y = 150.0F;
                    float width = 100.0F;
                    int height = 50;

                    RectangleF drawRectangle = new RectangleF(x, offsetY, width, lineHeight);

                    //PointF point = new PointF(40f, 2f);


                    graphic.FillRectangle(white, 0, 0, bitm.Width, bitm.Height);

                    drawRectangle = new RectangleF(x, offsetY, bitmapLength, lineHeight);                                           //Draw a rectangle what will be filled with text.
                    graphic.DrawString("Invoice Number: " + InvoiceNo, newfont2, black, drawRectangle, drawFormatFar);              //Fill created rectangle with text. (Write a line of text)

                    //graphic.DrawString("Invoice Number: " + InvoiceNo + "", newfont2, black, startX + 150, startY + offsetY);
                    offsetY = offsetY + lineHeight;                                                                                 //Calculating next Y position of rectangle. (Go to next line)

                    //PointF pointPrice = new PointF(15f, 45f);
                    drawRectangle = new RectangleF(x, offsetY, bitmapLength, lineHeight);                                           //Repeat.
                    graphic.DrawString("Invoice Date: " + InvoiceDate, newfont2, black, drawRectangle, drawFormat);
                    //graphic.DrawString("Invoice Date: " + InvoiceDate + "", newfont2, black, startX, startY + offsetY);
                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;

                    //graphic.DrawString("Name" + "Amount" + "Total", newfont2, black, startX + 15, startY + offsetY);
                    //graphic.DrawString("test", newfont2, black, startX, startY);

                    drawRectangle = new RectangleF(x, offsetY, 40, lineHeight);
                    graphic.DrawString("Code", newfont2, black, drawRectangle, drawFormat);

                    drawRectangle = new RectangleF(x + 25, offsetY , width, lineHeight);
                    graphic.DrawString("Name", newfont2, black, drawRectangle, drawFormatCenter);

                    drawRectangle = new RectangleF(x + 25 + width + 5, offsetY, 40, lineHeight);
                    //drawFormat.Alignment = StringAlignment.Far;
                    graphic.DrawString("Price", newfont2, black, drawRectangle, drawFormatCenter);

                    drawRectangle = new RectangleF(x + 25 + width + 45, offsetY, 30, lineHeight);
                    //drawFormat.Alignment = StringAlignment.Far;
                    graphic.DrawString("Qty", newfont2, black, drawRectangle, drawFormatCenter);

                    drawRectangle = new RectangleF(x + 25 + width + 75, offsetY, 30, lineHeight);
                    //drawFormat.Alignment = StringAlignment.Far;
                    graphic.DrawString("Disc(%)", newfont2, black, drawRectangle, drawFormatCenter);

                    drawRectangle = new RectangleF(x + 25 + width + 80, offsetY, 80, lineHeight);
                    graphic.DrawString("Total(€)", newfont2, black, drawRectangle, drawFormatFar);

                    offsetY = offsetY + lineHeight;
                    //offsetY = offsetY + lineHeight;
                    graphic.DrawString("------------------------------------------------------------------------", newfont2, black, startX, startY + offsetY);  //Draw a section line
                    PointF pointPname = new PointF(10f, 65f);
                    PointF pointBar = new PointF(10f, 65f);

                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;
                    //offsetY = offsetY + lineHeight;

                    //InvoiceNo = "Invoice Number:";

                    foreach (OperationItem operationItem in operationItems)
                    {
                        drawRectangle = new RectangleF(x, offsetY, 35, height);
                        graphic.DrawString(operationItem.ItemCode.ToString(), itemFont, black, drawRectangle, drawFormat);

                        drawRectangle = new RectangleF(x + 25, offsetY, width, height);
                        graphic.DrawString(operationItem.ItemName, itemFont, black, drawRectangle, drawFormatCenter);

                        drawRectangle = new RectangleF(x + 25 + width + 5, offsetY, 40, height);
                        //drawFormat.Alignment = StringAlignment.Far;
                        graphic.DrawString(operationItem.ItemPrice.ToString(), itemFont, black, drawRectangle, drawFormatCenter);

                        drawRectangle = new RectangleF(x + 25 + width + 45, offsetY, 30, height);
                        //drawFormat.Alignment = StringAlignment.Far;
                        graphic.DrawString(operationItem.ItemAmount.ToString(), itemFont, black, drawRectangle, drawFormatCenter);

                        drawRectangle = new RectangleF(x + 25 + width + 75, offsetY, 30, height);
                        //drawFormat.Alignment = StringAlignment.Far;
                        graphic.DrawString(operationItem.ItemDiscount.ToString(), itemFont, black, drawRectangle, drawFormatCenter);

                        drawRectangle = new RectangleF(x + 25 + width + 80, offsetY, 80, height);
                        graphic.DrawString(operationItem.ItemTotalPrice.ToString(), itemFont, black, drawRectangle, drawFormatFar);
                        if (operationItem != operationItems.Last<OperationItem>()) offsetY = offsetY + height;
                    }
                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;

                    graphic.DrawString("------------------------------------------------------------------------", newfont2, black, startX, startY + offsetY);
                    //offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;
                    drawRectangle = new RectangleF(x, offsetY, width, lineHeight);
                    graphic.DrawString("Total Price: ", newfont2, black, drawRectangle, drawFormat);

                    drawRectangle = new RectangleF(x + 25 + width + 80, offsetY, 80, lineHeight);
                    graphic.DrawString(totalSum.ToString() + " €", newfont2, black, drawRectangle, drawFormatFar);

                    //offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;
                    graphic.DrawString("------------------------------------------------------------------------", newfont2, black, startX, startY + offsetY);
                    offsetY = offsetY + lineHeight;
                    drawRectangle = new RectangleF(x, offsetY, bitmapLength, 150);
                    graphic.DrawString("Craft House" +                                                                     //Draw tail of invoice
                                      "\nDunkri 5, Tallinn, Estonia" +
                                      "\n" +
                                      "\nJEVGENI LANG FIE" +
                                      "\nReg. Kood: 11882291" +
                                      "\n" +
                                      "\nVILLA STYLE OÜ" +
                                      "\nReg. Kood: 12542232", newfont2, black, drawRectangle, drawFormatCenter);
                }
                finally
                {
                    black.Dispose();
                    white.Dispose();
                    itemFont.Dispose();
                    newfont2.Dispose();
                }
                this.receipt = bitm;                        // Set created receipt to variable
                PrintReceipt();                             // Call a method what will print out built receipt
            }
        }

        private Bitmap receipt;

        public void PrintReceipt()
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Receipt";

            try
            {
                printDocument.PrintPage += PrintBitmapPage;
                PaperSize paperSize = new PaperSize();
                paperSize.Height = 775;
                paperSize.Width = 275;
                paperSize.PaperName = "Receipt";
                //paperSize.RawKind = 9;
                printDocument.DefaultPageSettings.PaperSize = paperSize;
                printDocument.Print();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                System.Windows.MessageBox.Show("File is opened! Close file first!");
            }
        }

        private void PrintBitmapPage(object o, PrintPageEventArgs e)        //Print out bitmap image to printer driver
        {
            System.Drawing.Point loc = new System.Drawing.Point(0, 24);
            e.Graphics.DrawImage(receipt, loc);
        }

    }
}
