using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRev
{
    class SigningDocumentPrint : System.Drawing.Printing.PrintDocument
    {

        private RO currentRo;
        private int printHeight;
        private int printWidth;
        private int leftMargin;
        private int topMargin;
        private RectangleF printArea, logoArea, topArea, customerArea, bikeArea;
        private Font font;
        public SigningDocumentPrint(RO roToPrint) : base()
        {
            currentRo = roToPrint;
            init();
        }
        private void initAreas()
        {
            topArea = getTopArea();
            logoArea = getLogoArea();
            customerArea = getCustomerArea();
            bikeArea = getBikeArea();
        }
        private void moveDown(ref RectangleF writeArea)
        {
            PointF point = writeArea.Location;
            point.Y += writeArea.Height;
            writeArea.Location = point;
        }
        private void drawTextToCurrentPosition(System.Drawing.Printing.PrintPageEventArgs e, string text, Font font, ref float offsetX, RectangleF area, StringFormat format)
        {

            RectangleF writeArea = new RectangleF(area.Location.X + offsetX, area.Location.Y, area.Width - offsetX, area.Height);
            if (format == null)
            {
                SizeF stringSize = e.Graphics.MeasureString(text, font);

                offsetX += stringSize.Width;
                e.Graphics.DrawString(text, font, Brushes.Black, writeArea);

            }
            else
            {
                e.Graphics.DrawString(text, font, Brushes.Black, writeArea, format);
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
            String text = currentRo.customer.name;
            SizeF sizeOfText = e.Graphics.MeasureString(text, fontNum);
            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.FitBlackBox;

            format.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(text, fontNum, Brushes.Black, topArea, format);
            format.Alignment = StringAlignment.Near;
            fontNum = new Font("Times New Roman", 14, FontStyle.Bold);
            RectangleF bikeMkeModel = new RectangleF(topArea.X, topArea.Y, topArea.Width / 3, topArea.Height);
            text = currentRo.bike.getMakeModel();
            e.Graphics.DrawString(text, fontNum, Brushes.Black, bikeMkeModel, format);

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
            float x = leftMargin + printArea.Width / 2 - width / 2;
            float y = printArea.Bottom - height;
            RectangleF logoArea = new RectangleF(x, y, width, height);
            return logoArea;
        }
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
            e.Graphics.DrawString("Customer info:", font, Brushes.Black, customerArea.X, customerArea.Y - 2 - controlFont.Height);




            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), customerArea.X, customerArea.Y - 2, customerArea.Width, customerArea.Height + 2);
            Point centarLeft = new Point(Convert.ToInt32(customerArea.X), Convert.ToInt32(customerArea.Y + customerArea.Height / 2));
            Point centarRight = new Point(Convert.ToInt32(customerArea.X + customerArea.Width), Convert.ToInt32(customerArea.Y + customerArea.Height / 2));
            e.Graphics.DrawLine(new Pen(Brushes.Black), centarLeft, centarRight);


            RectangleF infoArea = new RectangleF(customerArea.Location, new SizeF(customerArea.Width, customerArea.Height / 2));
            e.Graphics.DrawString("Name, Adress, Zip:", controlFont, Brushes.Black, infoArea);
            SizeF stringSize = e.Graphics.MeasureString("Name, Adress, Zip:", controlFont);
            infoArea.X += stringSize.Width;
            infoArea.Width -= stringSize.Width;
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
        private RectangleF getBikeArea()
        {
            return new RectangleF(customerArea.Location.X, customerArea.Y + customerArea.Height + font.Height, printArea.Width, printArea.Height / 20);
        }
        private void writeBike(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font bikeFont = new Font("Times New Roman", 14);
            Font controlFont = new Font("Times New Roman", 14, FontStyle.Bold);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), bikeArea.X, bikeArea.Y, bikeArea.Width, bikeArea.Height);
            e.Graphics.DrawString("Bike info:", font, Brushes.Black, bikeArea.X, bikeArea.Y - font.Height);
            float offset = 0;
            RectangleF upBike = new RectangleF(bikeArea.Location.X, bikeArea.Y, bikeArea.Width, bikeArea.Height / 2);
            RectangleF dwBike = new RectangleF(bikeArea.Location.X, bikeArea.Y + bikeArea.Height / 2, bikeArea.Width, bikeArea.Height / 2);
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
        private void writeSingingArea(System.Drawing.Printing.PrintPageEventArgs e)
        {

            Font textFont = new Font("Times New Roman", 14);
            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            RectangleF writeArea = new RectangleF();
            writeArea.Location = new PointF(printArea.X, bikeArea.Bottom + 30);
            writeArea.Width = printArea.Width;
            writeArea.Height = printArea.Height;
           
            string text = "I " + currentRo.customer.name + " hereby authorize repair work to be done to my " + currentRo.bike.year.ToString() + " " + currentRo.bike.getMakeModel() + " Lic. No. " + currentRo.bike.licNo + " " +
                " along with the necessary materials. You and your employees may operate the above vehicle for the purposes of testing, inspection, or delivery at my risk. " +
                "An express mechanics lien is acknowledged on above vehicle to secure the amount of repairs there too. " +
                "It is also understood that you will not be held responsible for your loss or damage to vehicle or articles left in vehicle in case of fire, theft, or any other cause beyond your control. " +
                "I also understand that a service fee of $25 per day will be charged if vehicle is not picked up within 3 days of notification of completion.";


            
            float offset = 0;
            drawTextToCurrentPosition(e, text, font, ref offset, writeArea, format);
            offset = 0;
            text = "Customer Signature ___________________________";
            float height = e.Graphics.MeasureString(text, font).Height;
            writeArea = new RectangleF(printArea.X, logoArea.Top - height * 3, printArea.Width, printArea.Height);
            format.Alignment = StringAlignment.Far;
            drawTextToCurrentPosition(e, text, font, ref offset, writeArea, format);


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
        protected override void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            //Updateing 
            initAreas();
            e.Graphics.DrawImage(DataManager.logo, logoArea);
            writeTopArea(e);
            writeCustomer(e);
            writeBike(e);
            writeSingingArea(e);



        }


    }
}
