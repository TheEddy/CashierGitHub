using IronBarCode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cashier.Model
{
    class PrintManager
    {
        private string printText;

        public PrintManager()
        {
            printText = "";
            //PrintDocument print = new PrintDocument();
        }

        public void PrintNewLabel(string printText)
        {
            this.printText = printText;
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Label №: " + printText;

            PaperSize paperSize = new PaperSize();
            paperSize.Width = 157;
            paperSize.Height = 110;
            

            //DialogResult dr = new DialogResult();
            //var result = printDialog.ShowDialog();
            //if (result ?? false)
            //{
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
                
            //}
            //PrintDocument pd = new PrintDocument();
        }

        private void PrintLabelPage(object o, PrintPageEventArgs e)
        {
            var MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode(printText, BarcodeEncoding.Code128);
            MyBarCode.ResizeTo(250, 125);

            //MyBarCode.SaveAsPng(@"C:\Users\Administrator\Documents\barcode.png");

            e.PageSettings.PaperSize.Height = MyBarCode.Height;
            e.PageSettings.PaperSize.Width = MyBarCode.Width;
            
            System.Drawing.Image barcodeImage = MyBarCode.ToImage();
            System.Drawing.Point loc = new System.Drawing.Point(0, 24);
            e.Graphics.DrawImage(barcodeImage, loc);
        }

        //public void PrintReceipt(DateTime operationDate, string OperationCode, ObservableCollection<OperationItem> operationalCollection)
        //{
        //    PrintDocument printDocument = new PrintDocument();

        //}

        public void PrintNewReceipt(string historyNo, DateTime operationDate, ObservableCollection<OperationItem> operationItems, string totalSum)
        {
            //string welcome = "Test Shop";
            string InvoiceNo = historyNo;
            string gross = totalSum;
           // decimal net = Convert.ToInt32(txtNet.Text);
            //decimal discount = gross - net;
            string InvoiceDate = operationDate.ToShortDateString() + " " + operationDate.ToShortTimeString();

            int lineHeight = 20;
            int supplementaryLines = 12;

            int bitmapLength = 330;

            Bitmap bitm = new Bitmap(bitmapLength, ((supplementaryLines * lineHeight) + ((operationItems.Count-1)*50))+ 150);
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
                    itemFont = new Font("Calibri", 11);

                    black = new SolidBrush(Color.Black);
                    white = new SolidBrush(Color.White);

                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Near;

                    StringFormat drawFormatCenter = new StringFormat();
                    drawFormatCenter.Alignment = StringAlignment.Center;

                    StringFormat drawFormatFar = new StringFormat();
                    drawFormatFar.Alignment = StringAlignment.Far;

                    float x = 0.0F;
                    float y = 150.0F;
                    float width = 100.0F;
                    int height = 50;

                    RectangleF drawRectangle = new RectangleF(x, offsetY, width, lineHeight);

                    //PointF point = new PointF(40f, 2f);


                    graphic.FillRectangle(white, 0, 0, bitm.Width, bitm.Height);

                    drawRectangle = new RectangleF(x, offsetY, bitmapLength, lineHeight);
                    graphic.DrawString("Invoice Number: " + InvoiceNo, newfont2, black, drawRectangle, drawFormatFar);

                    //graphic.DrawString("Invoice Number: " + InvoiceNo + "", newfont2, black, startX + 150, startY + offsetY);
                    offsetY = offsetY + lineHeight;

                    //PointF pointPrice = new PointF(15f, 45f);
                    drawRectangle = new RectangleF(x, offsetY, bitmapLength, lineHeight);
                    graphic.DrawString("Invoice Date: " + InvoiceDate, newfont2, black, drawRectangle, drawFormat);
                    //graphic.DrawString("Invoice Date: " + InvoiceDate + "", newfont2, black, startX, startY + offsetY);
                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;

                    //graphic.DrawString("Name" + "Amount" + "Total", newfont2, black, startX + 15, startY + offsetY);
                    //graphic.DrawString("test", newfont2, black, startX, startY);

                    drawRectangle = new RectangleF(x, offsetY, 50, lineHeight);
                    graphic.DrawString("Code", newfont2, black, drawRectangle, drawFormatCenter);

                    drawRectangle = new RectangleF(x + 50 + 10, offsetY , width, height);
                    graphic.DrawString("Name", newfont2, black, drawRectangle, drawFormatCenter);

                    drawRectangle = new RectangleF(x + 50 + width + 20, offsetY, width, lineHeight);
                    //drawFormat.Alignment = StringAlignment.Far;
                    graphic.DrawString("Amount", newfont2, black, drawRectangle, drawFormatCenter);

                    drawRectangle = new RectangleF(x + 50 + width * 2 + 30, offsetY, 50, lineHeight);
                    graphic.DrawString("Total", newfont2, black, drawRectangle, drawFormatCenter);

                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;
                    graphic.DrawString("------------------------------------------------------------------------", newfont2, black, startX, startY + offsetY);
                    PointF pointPname = new PointF(10f, 65f);
                    PointF pointBar = new PointF(10f, 65f);

                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;

                    //InvoiceNo = "Invoice Number:";

                    foreach (OperationItem operationItem in operationItems)
                    {
                        drawRectangle = new RectangleF(x, offsetY, 50, height);
                        graphic.DrawString(operationItem.ItemCode.ToString(), newfont2, black, drawRectangle, drawFormatCenter);

                        drawRectangle = new RectangleF(x+50+10, offsetY, width, height);
                        graphic.DrawString(operationItem.ItemName, newfont2, black, drawRectangle, drawFormatCenter);

                        drawRectangle = new RectangleF(x + 50 + width + 20, offsetY, width, height);
                        //drawFormat.Alignment = StringAlignment.Far;
                        graphic.DrawString(operationItem.ItemAmount.ToString(), newfont2, black, drawRectangle, drawFormatCenter);

                        drawRectangle = new RectangleF(x + 50 + width * 2 + 30, offsetY, 50, height);
                        graphic.DrawString(operationItem.ItemTotalPrice.ToString(), newfont2, black, drawRectangle, drawFormatCenter);
                        if (operationItem != operationItems.Last<OperationItem>()) offsetY = offsetY + height;
                    }
                    offsetY = offsetY + lineHeight;
                    offsetY = offsetY + lineHeight;

                    graphic.DrawString("------------------------------------------------------------------------", newfont2, black, startX, startY + offsetY);
                    offsetY = offsetY + lineHeight;
                    drawRectangle = new RectangleF(x, offsetY, width, height);
                    graphic.DrawString("Total Price: ", newfont2, black, drawRectangle, drawFormat);

                    drawRectangle = new RectangleF(x + 50 + width * 2 + 30, offsetY, 50, height);
                    graphic.DrawString(totalSum.ToString(), newfont2, black, drawRectangle, drawFormatCenter);

                    offsetY = offsetY + lineHeight;
                    graphic.DrawString("------------------------------------------------------------------------", newfont2, black, startX, startY + offsetY);
                    offsetY = offsetY + lineHeight;
                    drawRectangle = new RectangleF(x, offsetY, bitmapLength, 150);
                    graphic.DrawString("Craft House" +
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
                this.receipt = bitm;
                PrintReceipt();
            }

            

            //using (MemoryStream Mmst = new MemoryStream())
            //{
            //    bitm.Save("ms", ImageFormat.Jpeg);
            //    pictureBox1.Image = bitm;
            //    pictureBox1.Width = bitm.Width;
            //    pictureBox1.Height = bitm.Height;


            //}


        }

        private Bitmap receipt;

        public void PrintReceipt()
        {
            //this.printText = printText;
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Receipt";

            //DialogResult dr = new DialogResult();
            //var result = printDialog.ShowDialog();
            //if (result ?? false)
            //{
            try
            {
                printDocument.PrintPage += PrintBitmapPage;
                PaperSize paperSize = new PaperSize();
                paperSize.Height = 50;
                paperSize.Width = 100;
                printDocument.DefaultPageSettings.PaperSize = paperSize;
                printDocument.Print();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                System.Windows.MessageBox.Show("File is opened! Close file first!");
            }

            //}
            //PrintDocument pd = new PrintDocument();
        }

        private void PrintBitmapPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Point loc = new System.Drawing.Point(0, 24);
            e.Graphics.DrawImage(receipt, loc);
        }

    }
}
