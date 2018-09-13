using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRev
{
    class RoPrint : System.Drawing.Printing.PrintDocument
    {
        bool partsOnSecondPage = false;
        private RO currentRo;
        private int printHeight;
        private int printWidth;
        private int leftMargin;
        private int topMargin;
        private RectangleF printArea, customerArea, logoArea, dateArea, bikeArea, serviceArea, partsArea, bottomArea,topArea, descriptionArea;
        private Font font;

        public RoPrint(RO roToPrint) : base()
        {
            currentRo = roToPrint;
            init();
        }
        private void init()
        {
            base.DefaultPageSettings.Margins.Bottom = 50;
            base.DefaultPageSettings.Margins.Top = 50;
            base.DefaultPageSettings.Margins.Left = 50;
            base.DefaultPageSettings.Margins.Right = 50;

            printHeight = base.DefaultPageSettings.PaperSize.Height - base.DefaultPageSettings.Margins.Top - base.DefaultPageSettings.Margins.Bottom;
            printWidth = base.DefaultPageSettings.PaperSize.Width - base.DefaultPageSettings.Margins.Left - base.DefaultPageSettings.Margins.Right;
            leftMargin = base.DefaultPageSettings.Margins.Left;  //X
            topMargin = base.DefaultPageSettings.Margins.Top;  //Y
            printArea = new RectangleF(leftMargin, topMargin, printWidth, printHeight);
            font = new Font("Times New Roman", 12);
           
            
        }
        private void initAreas()
        {
            topArea = getTopArea();
            logoArea = getLogoArea();
            customerArea = getCustomerArea();
            dateArea = getDateArea();
            bikeArea = getBikeArea();
            serviceArea = getServiceArea();

            bottomArea = getBottomArea();
            
            
        }
        private void drawTextToCurrentPosition(System.Drawing.Printing.PrintPageEventArgs e, string text, Font font, ref float offsetX, RectangleF area,  StringFormat format)
        {

            RectangleF writeArea = new RectangleF(area.Location.X + offsetX, area.Location.Y, area.Width-offsetX, area.Height);
            if (format == null)
            {
                SizeF stringSize = e.Graphics.MeasureString(text, font);

                offsetX += stringSize.Width;
                e.Graphics.DrawString(text, font, Brushes.Black, writeArea);

            } else
            {
                e.Graphics.DrawString(text, font, Brushes.Black, writeArea,format);
            }
           
            
        }
        private RectangleF getTopArea()
        {
            Font fontText = new Font("Times New Roman", 16, FontStyle.Bold);
            return new RectangleF(printArea.X, printArea.Y - 10, printArea.Width, 4 * fontText.Height);
        }
        private void writeTopArea(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font fontNum = new Font("Times New Roman", 14, FontStyle.Bold);
            String text = "R.O. #" + currentRo.getId();
            SizeF sizeOfText = e.Graphics.MeasureString(text, fontNum);
            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            
            format.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(text, fontNum, Brushes.Black, topArea,format);
            format.Alignment = StringAlignment.Near;
            fontNum = new Font("Times New Roman", 14);
            text = "Taken By \n" + currentRo.takenBy;
            e.Graphics.DrawString(text, fontNum, Brushes.Black, topArea, format);

            format.Alignment = StringAlignment.Center;
            fontNum = new Font("Times New Roman", 16, FontStyle.Bold);
            text = "Revolution Motorsports";
            e.Graphics.DrawString(text, fontNum, Brushes.Black, topArea, format);
            text = "10778 Myers Way S\nSeattle, WA 98168\n(206)327 - 9891";

            RectangleF writeArea = new RectangleF(topArea.X, topArea.Y + fontNum.Height, topArea.Width, topArea.Height - fontNum.Height);
            fontNum = new Font("Times New Roman", 14);
            e.Graphics.DrawString(text, fontNum, Brushes.Black, writeArea, format);

        }
        private RectangleF getLogoArea()
        {
            int width = printWidth / 2;
            int height = Convert.ToInt32(Math.Round((double)DataManager.logo.Height * ((double)width / DataManager.logo.Width)));
            float x = leftMargin + printArea.Width/2 - width/2;
            float y = printArea.Bottom - height;
            RectangleF logoArea = new RectangleF(x,y, width, height);
            return logoArea;
        }
       
        private void moveDown(ref RectangleF writeArea)
        {
            PointF point = writeArea.Location;
            point.Y += writeArea.Height;
            writeArea.Location = point;
        }
        #region Customer


        private RectangleF getCustomerArea()
        {
              Font fontNum = new Font("Times New Roman", 16);
            return new RectangleF(leftMargin, topArea.Bottom, printArea.Width, printArea.Height / 20);
        }

        private void writeCustomer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Customer customer = currentRo.customer;
            Font customerFont = new Font("Times New Roman", 14);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            e.Graphics.DrawString("Customer info:", font, Brushes.Black, customerArea.X,customerArea.Y-2-controlFont.Height);
            

            

            e.Graphics.DrawRectangle(new Pen(Brushes.Black,2), customerArea.X, customerArea.Y-2, customerArea.Width, customerArea.Height+2);
            Point centarLeft = new Point(Convert.ToInt32(customerArea.X), Convert.ToInt32(customerArea.Y + customerArea.Height / 2));
            Point centarRight = new Point(Convert.ToInt32(customerArea.X + customerArea.Width), Convert.ToInt32(customerArea.Y + customerArea.Height / 2));
            e.Graphics.DrawLine(new Pen(Brushes.Black), centarLeft, centarRight);
          

            RectangleF infoArea = new RectangleF(customerArea.Location, new SizeF(customerArea.Width, customerArea.Height / 2));
            e.Graphics.DrawString("Name, Adress, Zip:", controlFont, Brushes.Black, infoArea);
            SizeF stringSize = e.Graphics.MeasureString("Name, Adress, Zip:", controlFont);
            infoArea.X += stringSize.Width;
            infoArea.Width-= stringSize.Width;
            StringFormat customerFormat = new StringFormat();
            customerFormat.Alignment = StringAlignment.Center;
            customerFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            e.Graphics.DrawString(customer.getInfo(), customerFont, Brushes.Black, infoArea, customerFormat);

            
            RectangleF contactArea = new RectangleF(centarLeft, new SizeF(customerArea.Width, customerArea.Height / 2));
            e.Graphics.DrawString("Cell, Other, Email:", controlFont, Brushes.Black, contactArea);
            stringSize = e.Graphics.MeasureString("Cell, Other, Email:", controlFont);
            contactArea.X += stringSize.Width;
            contactArea.Width -= stringSize.Width;
            e.Graphics.DrawString(customer.getContact(), customerFont, Brushes.Black, contactArea, customerFormat);

        }
        #endregion
        #region Date
        private RectangleF getDateArea()
        {
            return new RectangleF(getCustomerArea().Location.X, getCustomerArea().Y + getCustomerArea().Height, printArea.Width, printArea.Height / 40);
        }
        private void writeDate(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font dateFont = new Font("Times New Roman", 14);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            RectangleF dateInArea = new RectangleF(dateArea.Location.X, dateArea.Location.Y, dateArea.Width / 2, dateArea.Height);
            RectangleF dateOutArea = new RectangleF(dateArea.Location.X + dateInArea.Width, dateArea.Location.Y, dateArea.Width / 2, dateArea.Height);
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), dateArea.X + dateInArea.Width, dateArea.Y, dateArea.X + dateInArea.Width, dateArea.Y + dateArea.Height);


            e.Graphics.DrawString("Date in:", controlFont, Brushes.Black, dateInArea);
            SizeF stringSize = e.Graphics.MeasureString("Date in:", controlFont);
            dateInArea.X += stringSize.Width;
            dateInArea.Width -= stringSize.Width;
            e.Graphics.DrawString(currentRo.dateIn.ToLongDateString(), dateFont, Brushes.Black, dateInArea, format);
            if (currentRo.isCLosed())
            {
                stringSize = e.Graphics.MeasureString("Date out:", controlFont);
                e.Graphics.DrawString("Date out:", controlFont, Brushes.Black, dateOutArea);
                dateOutArea.X += stringSize.Width;
                dateOutArea.Width -= stringSize.Width;
                e.Graphics.DrawString(currentRo.dateOut.ToLongDateString(), dateFont, Brushes.Black, dateOutArea, format);
            }


        }
        #endregion
        #region Bike
        private RectangleF getBikeArea()
        {
            return new RectangleF(getDateArea().Location.X, getDateArea().Y + getDateArea().Height + font.Height, printArea.Width, printArea.Height / 20);
        }
        private void writeBike(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font bikeFont = new Font("Times New Roman", 14);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), bikeArea.X, bikeArea.Y , bikeArea.Width, bikeArea.Height);
            e.Graphics.DrawString("Bike info:", font, Brushes.Black, bikeArea.X, bikeArea.Y - font.Height);
            float offset = 0;
            RectangleF upBike = new RectangleF(bikeArea.Location.X, bikeArea.Y, bikeArea.Width, bikeArea.Height / 2);
            RectangleF dwBike = new RectangleF(bikeArea.Location.X, bikeArea.Y+ bikeArea.Height/2, bikeArea.Width, bikeArea.Height / 2);
            e.Graphics.DrawLine(new Pen(Brushes.Black), dwBike.Location.X, dwBike.Location.Y, dwBike.Right, dwBike.Top);
            drawTextToCurrentPosition(e, "Make/Model:", controlFont, ref offset, bikeArea, null);
            drawTextToCurrentPosition(e, currentRo.bike.getMakeModel(), bikeFont, ref offset, upBike, null);
            drawTextToCurrentPosition(e, "Year:", controlFont, ref offset, upBike, null);
            drawTextToCurrentPosition(e, currentRo.bike.year.ToString(), bikeFont, ref offset, upBike, null);
            drawTextToCurrentPosition(e, "Color:", controlFont, ref offset, upBike, null);
            drawTextToCurrentPosition(e, currentRo.bike.color, bikeFont, ref offset, upBike, null);
            drawTextToCurrentPosition(e, "Odo:", controlFont, ref offset, upBike, null);
            drawTextToCurrentPosition(e, currentRo.bike.odo, bikeFont, ref offset, upBike, null);
            offset = 0;
            drawTextToCurrentPosition(e, "Lic.No.:", controlFont, ref offset, dwBike, null);
            drawTextToCurrentPosition(e, currentRo.bike.licNo, bikeFont, ref offset, dwBike, null);
            drawTextToCurrentPosition(e, "Eng. No.:", controlFont, ref offset, dwBike, null);
            drawTextToCurrentPosition(e, currentRo.bike.engineNo, bikeFont, ref offset, dwBike, null);
            drawTextToCurrentPosition(e, "Frm. No.:", controlFont, ref offset, dwBike, null);
            drawTextToCurrentPosition(e, currentRo.bike.engineNo, bikeFont, ref offset, dwBike, null);
            drawTextToCurrentPosition(e, "Key. No.:", controlFont, ref offset, dwBike, null);
            drawTextToCurrentPosition(e, currentRo.bike.keyNo, bikeFont, ref offset, dwBike, null);

        }
        #endregion
        #region Service
        private RectangleF getServiceArea()
        {

            Font textFont = new Font("Times New Roman", 14);
            return new RectangleF(getBikeArea().Location.X, getBikeArea().Y + getBikeArea().Height + font.Height, printArea.Width, (currentRo.services.Count +1)* textFont.Height);
        }
        private void writeServices(System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat format = new StringFormat();
            RectangleF writeArea = new RectangleF(serviceArea.X, serviceArea.Y - font.Height, printArea.Width, font.Height);
            format.Alignment = StringAlignment.Far;
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            float offset = 0;
            drawTextToCurrentPosition(e, "Services Requested/Problems:", font, ref offset, writeArea, null);
            drawTextToCurrentPosition(e, "Hours:", font, ref offset, writeArea, format);


            e.Graphics.DrawLine(new Pen(Brushes.Black, 3), serviceArea.X, serviceArea.Y, serviceArea.Right, serviceArea.Y);
            Font serviceFont = new Font("Times New Roman", 14);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
           
            
            offset = 0;
            writeArea = new RectangleF(serviceArea.Location.X, serviceArea.Location.Y, serviceArea.Width, serviceFont.Height);
            double totalHours = 0;
            foreach (Service service in currentRo.services)
            {
                drawTextToCurrentPosition(e, service.description, serviceFont, ref offset, writeArea, null);
                drawTextToCurrentPosition(e, service.hour.ToString(), serviceFont, ref offset, writeArea, format);
                totalHours += service.hour;
                offset = 0;
                PointF point = writeArea.Location;
                point.Y += writeArea.Height;
                writeArea.Location = point;
               
            }
            e.Graphics.DrawLine(new Pen(Brushes.Black, 3), writeArea.X, writeArea.Y, writeArea.Right, writeArea.Y);
            drawTextToCurrentPosition(e, "Total hours: "+ totalHours.ToString()+"h", controlFont, ref offset, writeArea, format);
            offset = 0;
            format.Alignment = StringAlignment.Near;
            drawTextToCurrentPosition(e, "Hourly Rate: $" + currentRo.hourlyRate.ToString() + "/h", controlFont, ref offset, writeArea, format);
            offset = 0;
            format.Alignment = StringAlignment.Center;
            drawTextToCurrentPosition(e, "Total Labor: $" + ((double)currentRo.hourlyRate* totalHours).ToString(), controlFont, ref offset, writeArea, format);

            descriptionArea = getDescriptionArea(e);
            partsArea = getPartsArea();
            if (partsArea.Bottom > bottomArea.Top)
            {
                partsOnSecondPage = true;
            }
        }


        #endregion
        #region Parts
        private RectangleF getPartsArea()
        {
            Font textFont = new Font("Times New Roman", 14);
            return new RectangleF(descriptionArea.Location.X, descriptionArea.Y + descriptionArea.Height + font.Height+10, descriptionArea.Width, (currentRo.parts.Count ) * textFont.Height);
        }
        private void writeParts(System.Drawing.Printing.PrintPageEventArgs e)
        {

            StringFormat format = new StringFormat();
            Font partsFont = new Font("Times New Roman", 14);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            RectangleF writeArea = new RectangleF(partsArea.X, partsArea.Y - font.Height, printArea.Width, font.Height);
            format.Alignment = StringAlignment.Near;
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            float offset = 0;
            RectangleF qtyArea = new RectangleF(writeArea.X, writeArea.Y, e.Graphics.MeasureString("Qty:", font).Width * 1.5f, partsFont.Height);
            drawTextToCurrentPosition(e, "Qty:", font, ref offset, qtyArea, null);
            offset = 0;
            RectangleF partnoArea =new  RectangleF(qtyArea.Right, qtyArea.Y, e.Graphics.MeasureString("Part. No.:", font).Width * 1.7f, partsFont.Height);
            drawTextToCurrentPosition(e, "Part. No.:", font,ref offset, partnoArea, format);
            offset = 0;
            RectangleF descArea = new RectangleF(partnoArea.Right, partnoArea.Y, e.Graphics.MeasureString("Description:", font).Width * 4.7f, partsFont.Height);
            drawTextToCurrentPosition(e, "Description", font, ref offset, descArea, format);
            offset = 0;
            RectangleF sPriceArea = new RectangleF(descArea.Right, descArea.Y, descArea.Width/9, partsFont.Height);
            drawTextToCurrentPosition(e, "@", font, ref offset, sPriceArea, format);
            offset = 0;
            RectangleF totalArea = new RectangleF(sPriceArea.Right, sPriceArea.Y, printArea.Right - sPriceArea.Right, partsFont.Height);
            format.Alignment = StringAlignment.Far;
            drawTextToCurrentPosition(e, "Amount", font, ref offset, totalArea, format);
            offset = 0;
            format.Alignment = StringAlignment.Near;

            writeArea = new RectangleF(partsArea.X, partsArea.Y, printArea.Width, font.Height);
            e.Graphics.DrawLine(new Pen(Brushes.Black, 3), writeArea.X, writeArea.Y, writeArea.Right, writeArea.Y);

             offset = 0;
             double totalParts = 0;
             foreach (PartQty partQty in currentRo.parts)
             {
                moveDown(ref qtyArea);
                drawTextToCurrentPosition(e, partQty.qunatity.ToString(), partsFont, ref offset, qtyArea, format);
                offset = 0;
                moveDown(ref partnoArea);
                drawTextToCurrentPosition(e, partQty.part.partNo, partsFont, ref offset, partnoArea, format);
                offset = 0;
                moveDown(ref descArea);
                drawTextToCurrentPosition(e, partQty.part.description, partsFont, ref offset, descArea, format);
                offset = 0;
                moveDown(ref sPriceArea);
                drawTextToCurrentPosition(e, partQty.part.price.ToString(), partsFont, ref offset, sPriceArea, format);
                offset = 0;
                moveDown(ref totalArea);
                format.Alignment = StringAlignment.Far;
                drawTextToCurrentPosition(e, (partQty.part.price * partQty.qunatity).ToString(), partsFont, ref offset, totalArea, format);
                offset = 0;
                format.Alignment = StringAlignment.Near;
                totalParts += partQty.part.price * partQty.qunatity;

            }
            totalParts = Math.Round(totalParts, 2);
            format.Alignment = StringAlignment.Far;
            writeArea = new RectangleF(partsArea.X, partsArea.Bottom + 2, printArea.Width, controlFont.Height);
            e.Graphics.DrawLine(new Pen(Brushes.Black, 3), writeArea.X, writeArea.Y, writeArea.Right, writeArea.Y);
            
            drawTextToCurrentPosition(e, "Total Parts: $" + totalParts.ToString(), controlFont , ref offset, writeArea, format);
            offset = 0;


        }

        #endregion
        #region Description

        private RectangleF getDescriptionArea(PrintPageEventArgs e)
        {
            Font textFont = new Font("Times New Roman", 10);
            SizeF field = e.Graphics.MeasureString(currentRo.descriptionOfWork, textFont, (int)printArea.Width);
            return new RectangleF(serviceArea.Location.X, serviceArea.Y + serviceArea.Height + font.Height + 10, printArea.Width, field.Height);
        }
        private void writeDescriptionArea(System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat format = new StringFormat();
            Font descriptionFont = new Font("Times New Roman", 10);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            RectangleF writeArea = new RectangleF(descriptionArea.X, descriptionArea.Y - font.Height , printArea.Width, font.Height);
            format.Alignment = StringAlignment.Near;
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            float offset = 0;
            drawTextToCurrentPosition(e, "Description of work preformed:", font, ref offset, writeArea, null);
            offset = 0;
            writeArea = new RectangleF(descriptionArea.X, descriptionArea.Y, printArea.Width, font.Height);
            e.Graphics.DrawLine(new Pen(Brushes.Black, 3), writeArea.X, writeArea.Y, writeArea.Right, writeArea.Y);
            writeArea = new RectangleF(descriptionArea.X, descriptionArea.Y, printArea.Width, descriptionArea.Height);
            drawTextToCurrentPosition(e, currentRo.descriptionOfWork, descriptionFont, ref offset, writeArea, format);



        }

        #endregion
        private RectangleF getBottomArea()
        {
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            return new RectangleF(partsArea.X, printArea.Bottom - 4 - 9 * controlFont.Height, printArea.Width, printArea.Bottom - (printArea.Bottom + 4 - 9 * controlFont.Height));
        }
        private void writeBottomArea(System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat format = new StringFormat();
            Font textFont = new Font("Times New Roman", 14);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            format.Alignment = StringAlignment.Near;
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            float offset = 0;

            
            textFont = new Font("Times New Roman", 12);

            format.Alignment = StringAlignment.Far;
            SizeF field = e.Graphics.MeasureString("Gas/Oil/Greas", textFont);
            RectangleF valueArea = new RectangleF(bottomArea.Right - field.Width, bottomArea.Top, field.Width, field.Height);
            RectangleF infoArea  = new RectangleF(valueArea.Left - field.Width, bottomArea.Top, field.Width, field.Height);
            drawTextToCurrentPosition(e, "Total Labor:", textFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.getTotalLabor().ToString(), textFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "Total Parts:", textFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.getTotalPartsPrice().ToString() , textFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "Gas/Oil/Grease:", textFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.gasOilGreas.ToString() , textFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "EPA/Waste:", textFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.getTotalWaste().ToString() , textFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "Deposit:", textFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.deposit.ToString() , textFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "SubTotal:", textFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.getSubTotal().ToString() , textFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "Tax:", textFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.getTax().ToString(), textFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "Total:", controlFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.getTotal().ToString(), controlFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);
            drawTextToCurrentPosition(e, "Due:", controlFont, ref offset, infoArea, format);
            offset = 0;
            drawTextToCurrentPosition(e, "$" + currentRo.getDept().ToString() , controlFont, ref offset, valueArea, format);
            offset = 0;
            moveDown(ref valueArea);
            moveDown(ref infoArea);


        }


        protected override void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            //Updateing 
             if (partsOnSecondPage)
            {
                partsArea = new RectangleF(printArea.Location,partsArea.Size);
                e.Graphics.DrawImage(DataManager.logo, logoArea);
                writeParts(e);
                writeBottomArea(e);
                partsOnSecondPage = false;
            } else
            {
                initAreas();
                e.Graphics.DrawImage(DataManager.logo, logoArea);
                writeTopArea(e);
                writeCustomer(e);
                writeDate(e);
                writeBike(e);
                writeServices(e);
                writeDescriptionArea(e);
                if (!partsOnSecondPage)
                    writeParts(e);
                writeBottomArea(e);
            }
           

            if  (partsOnSecondPage)
            {
                e.HasMorePages = true;
  
            }
            else
            {
                e.HasMorePages = false;
            }

        }



    }
}
