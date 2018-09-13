using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MotoRev
{
    public partial class RoForm : Form
    {
        RO ro;
        private Boolean isNewRo = false;
        public RoForm()
        {
            init();
            Text = "RO#" + (DataManager.getCurrentRoId()+1).ToString();
            lbTakenBy.Text = "Taken by " + Core.logedIn.getName();
            lbTakenBy.Location = new Point(groupBoxCustomer.Right - lbTakenBy.Width, lbTakenBy.Location.Y);
            tbRONum.Text = (DataManager.getCurrentRoId() + 1).ToString();
            cbOut.Checked = false;
            isNewRo = true;
            ro = new RO();
        }
        public RoForm(RO roToEdit)
        {
            init();
            ro = roToEdit;
            autoFillRo(ro);
            cbPrintSigningPage.Checked = false;
        }
        private void init()
        { 
            InitializeComponent();
            addEventHandlersForTotalPrice();
            addEventHandlersForNumericalTextBoxes();
            nudHourlyRate.Value = Config.getInstance().HorlyRate;
            foreach (string make in DataManager.makeModel.Keys)
            {
                cbBikeMake.Items.Add(make);
            }
            foreach (var value in DataManager.makeModel.Values)
            {
                foreach (String model in value)
                    cbBikeModel.Items.Add(model);
            }
            dateTimePickerIn.Value = DateTime.Today;
          


        }

        private void addEventHandlersForNumericalTextBoxes()
        {
            tbPartPrice.KeyPress += tbDecimalOnlyKeyPress;
            tbGOG.KeyPress += tbDecimalOnlyKeyPress;


        }

        private void addEventHandlersForTotalPrice()
        {
            dgvPart.RowsAdded += calculateTotalPriceEvent;
            dgvPart.RowsRemoved += calculateTotalPriceEvent;
            dgvService.RowsAdded += calculateTotalPriceEvent;
            dgvService.RowsRemoved += calculateTotalPriceEvent;
            nudHourlyRate.ValueChanged += calculateTotalPriceEvent;
            nudTires.ValueChanged += calculateTotalPriceEvent;
        }
        //========================Customer
        private void autoFillCustomer(Customer customer)
        {
            tbCustomerName.Text = customer.name;
            tbCustomerAdress.Text = customer.Adress;
            tbCustomerCSZ.Text = customer.cityStateZip;
            tbCustomerEmail.Text = customer.email;
            tbCustomerPhone.Text = customer.cellPhone;
            tbCustomerSecPh.Text = customer.phone;
        }


        //=========================Bike


        private void cbBikeMake_Validated(object sender, EventArgs e)
        {
            cbBikeModel.Items.Clear();
            string make = (String)cbBikeMake.SelectedItem;
            if (make == null)
            {

                foreach (var value in DataManager.makeModel.Values)
                {
                    foreach (String model in value)
                    {
                        cbBikeModel.Items.Add(model);

                    }

                }
            }
            else if (DataManager.makeModel.ContainsKey(make))
            {

                foreach (string model in DataManager.makeModel[(String)cbBikeMake.SelectedItem])
                {
                    cbBikeModel.Items.Add(model);

                }
            }
        }

        private void tbBikeYear_Validated(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(tbBikeYear.Text);
            }
            catch (Exception exception)
            {
                tbBikeYear.Text = "";
            }
        }
        private void autoFillBike(Bike bike)
        {
            tbBikeColor.Text = bike.color;
            tbBikeEngineNo.Text = bike.engineNo;
            tbBikeFrame.Text = bike.frameNo;
            tbBikeKey.Text = bike.keyNo;
            tbBikeLic.Text = bike.licNo;
            tbBikeOdo.Text = bike.odo;
            if (bike.year != 0)
                tbBikeYear.Text = bike.year.ToString();
            cbBikeMake.Text = bike.make;
            cbBikeModel.Text = bike.model;
        }

        //==========================Part
        private void dgvPart_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar.Equals((char)Keys.Escape))
            {
                HashSet<DataGridViewRow> deletedRows = new HashSet<DataGridViewRow>();
                foreach (DataGridViewCell cell in dgvPart.SelectedCells)
                {
                    DataGridViewRow currentRow = cell.OwningRow;
                    if (!deletedRows.Contains(currentRow) && !currentRow.IsNewRow)
                    {
                        dgvPart.Rows.Remove(currentRow);
                        deletedRows.Add(currentRow);
                    }
                      
                }
               
            }
        }
        private void autoFillParts(List<PartQty> partQtys)
        {
            foreach (PartQty partqty in partQtys)
            {
                object[] data = new object[5];
                data[0] = partqty.qunatity;
                data[1] = partqty.part.partNo;
                data[2] = partqty.part.description;
                data[3] = partqty.part.price;
                data[4] = Math.Round(partqty.part.price * partqty.qunatity, 2);
                dgvPart.Rows.Add(data);
                dgvPart.Rows[dgvPart.Rows.Count - 2].Cells[0].Style.BackColor = partqty.getColorFromState();
                  
                
            }
            dgvPart.ClearSelection();

        }
        private void tbPartPrice_Validated(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(tbPartPrice.Text);
            }
            catch (Exception exception)
            {
                tbPartPrice.Text = "";
            }
        }
        private void dgvPart_Validated(object sender, EventArgs e)
        {
            dgvPart.ClearSelection();
        }
        private void btAddPart_Click(object sender, EventArgs e)
        {
            if (tbPartDesc.Text != "" && nUDPartQuantity.Value != 0 && tbPartPrice.Text != null)
            {
                try
                {
                    object[] data = new object[5];
                    data[0] = nUDPartQuantity.Value;
                    data[1] = tbPartNo.Text;
                    data[2] = tbPartDesc.Text;
                    data[3] = Math.Round(Convert.ToDouble(tbPartPrice.Text), 2);
                    data[4] = Math.Round(Convert.ToDouble(nUDPartQuantity.Value) * (double)data[3], 2);
                    dgvPart.Rows.Add(data);
                    tbPartPrice.Text = "";
                    tbPartDesc.Text = "";
                    tbPartNo.Text = "";
                    nUDPartQuantity.Value = 1;
                    tbPartDesc.Focus();

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Part price must be a number!");
                }
            }
        }
        //===========================Service
        private void dgvService_Validated(object sender, EventArgs e)
        {
            dgvService.ClearSelection();
        }
        private void dgvService_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Escape))
            {
                HashSet<DataGridViewRow> deletedRows = new HashSet<DataGridViewRow>();
                foreach (DataGridViewCell cell in dgvService.SelectedCells)
                {
                    DataGridViewRow currentRow = cell.OwningRow;
                    if (!deletedRows.Contains(currentRow) && !currentRow.IsNewRow )
                    {
                        dgvService.Rows.Remove(currentRow);
                        deletedRows.Add(currentRow);
                    }
                       
                }

            }
        }
        private void btAddService_Click(object sender, EventArgs e)
        {
            if (tbServiceDesc.Text != "" && nUDServiceHours.Value >= 0)
            {
                object[] data = new object[2];
                data[0] = tbServiceDesc.Text;
                data[1] = nUDServiceHours.Value;
                dgvService.Rows.Add(data);
                tbServiceDesc.Text = "";
                nUDServiceHours.Value = Convert.ToDecimal(0.5);
                tbServiceDesc.Focus();
            }
        }
        private void autoFillServices(List<Service> services)
        {
            foreach(Service service in services)
            {
                object[] data = new object[2];
                data[0] = service.description;
                data[1] = service.hour;
                dgvService.Rows.Add(data);
            }
            dgvService.ClearSelection();
        }

        //====================GENERATe
        private bool generateRo()
        {

            if (ro == null)
            {
                ro = new RO();
            }
            int desiredID = -1;
            try
            {
                desiredID = Convert.ToInt32(tbRONum.Text);
                if(desiredID != ro.getId())
                {
                    if (DataManager.getRoById(desiredID) != null)
                    {
                        DialogResult res = MessageBox.Show("RO #" + desiredID.ToString() + " already exists. Do you want to overwrite RO?", "Warrning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (res == DialogResult.OK)
                        {
                            ro.changeID(desiredID);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        ro.changeID(desiredID);
                    }
                }
                
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Ro id isnt a integer value!" + e.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ro.customer = getCustomer();
            ro.bike = getBike();
            ro.dateIn = dateTimePickerIn.Value;
            try
            {
                ro.deposit = Convert.ToDouble(tbDeposit.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Deposit isnt a numerical value!","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            if (cbOut.Checked)
            {
                ro.dateOut = dateTimePickerOut.Value.Date;
            } else
            {
                ro.dateOut = DateTime.MinValue;
            }
            ro.gasOilGreas = Convert.ToDouble(tbGOG.Text);
            ro.descriptionOfWork = tbDescOfWork.Text;
            ro.hourlyRate = nudHourlyRate.Value;
            ro.parts = getParts();
            ro.services = getServices();
            ro.saveOldParts = cbSaveParts.Checked;
            ro.takenBy = Core.logedIn.getName();
            ro.tires = nudTires.Value;
            ro.deposit = getDepositFromDgv();
            DataManager.addRO(ro);
            return true;

        }
        private List<PartQty> getParts()
        {
            List<PartQty> parts = new List<PartQty>();
            foreach (DataGridViewRow row in dgvPart.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                Part part = new Part();
                part.price = Convert.ToDouble(row.Cells[3].Value);
                part.description = row.Cells[2].Value.ToString();
                part.partNo = row.Cells[1].Value.ToString();
                PartQty partQty = new PartQty(part, Convert.ToInt32(row.Cells[0].Value));
                partQty.setState(row.Cells[0].Style.BackColor);
                parts.Add(partQty);

            }
            return parts;
        }
        private List<Service> getServices()
        {
            List<Service> services = new List<Service>();
            foreach (DataGridViewRow row in dgvService.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                Service service = new Service();
                service.hour = Convert.ToDouble(row.Cells[1].Value);
                service.description = row.Cells[0].Value.ToString();
                if (service.description != "")
                    services.Add(service);
            }
            return services;
        }
        private Bike getBike()
        {
            Bike bike = new Bike();
            bike.color = tbBikeColor.Text;

            bike.make = cbBikeMake.Text;
            bike.model = cbBikeModel.Text;
            bike.licNo = tbBikeLic.Text;
            bike.engineNo = tbBikeEngineNo.Text;
            bike.odo = tbBikeOdo.Text;
            bike.frameNo = tbBikeFrame.Text;
            bike.keyNo = tbBikeKey.Text;
            try
            {
                bike.year = Convert.ToInt32(tbBikeYear.Text);
            }
            catch (Exception exception)
            {
                bike.year = 0;
            }

            return bike;
        }
        private Customer getCustomer()
        {
            
            return ro.customer;
        }
        private void autoFillRo(RO ro)
        {
            this.Text = "RO#" + ro.getId().ToString();
            tbRONum.Text = ro.getId().ToString();
            tbRONum.Text = ro.getId().ToString();
            autoFillBike(ro.bike);
            autoFillCustomer(ro.customer);
            autoFillParts(ro.parts);
            autoFillServices(ro.services);
            dataGridViewTotalPrice.Rows[0].Cells[5].Value = ro.deposit;
            nudTires.Value = ro.tires;
            dateTimePickerIn.Value = ro.dateIn;
            if(ro.dateOut > DateTime.Now.AddYears(-10))
            {
                dateTimePickerOut.Value = ro.dateOut;
                dateTimePickerOut.Enabled = true;
                cbOut.Checked = true;
            }
            tbDescOfWork.Text = ro.descriptionOfWork;
            nudHourlyRate.Value = ro.hourlyRate;
            cbSaveParts.Checked = ro.saveOldParts;
            tbGOG.Text = ro.gasOilGreas.ToString();
            lbTakenBy.Text = "Taken by " + ro.takenBy;
            lbTakenBy.Location = new Point(groupBoxCustomer.Right - lbTakenBy.Width, lbTakenBy.Location.Y);
            calculateTotalPrice();

        }
        //===================Validation
        private bool validateCustomer()
        {
            if (ro.customer == null)
            {
                MessageBox.Show("Customer not selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return ro.customer != null;
        }
        private bool validateBike()
        {
            bool rets = true;
            if (cbBikeMake.Text == "")
            {
                lbBikeMake.ForeColor = Color.Red;
                rets = false;
            } else
            {
                lbBikeMake.ForeColor = Color.Black;
            }

            if (cbBikeModel.Text == "")
            {
                lbBikeModel.ForeColor = Color.Red;
                rets = false;
            } else
            {
                lbBikeModel.ForeColor = Color.Black;
            }

            if(tbBikeYear.Text == "")
            {
                lbBikeYear.ForeColor = Color.Red;
                rets = false;
            } else
            {
                lbBikeYear.ForeColor = Color.Black;
            }
            return rets;
        }
        private bool validateRO()
        {
            bool customer = validateCustomer();
            bool bike = validateBike();
            return customer && bike;
        }
        //===========================Utility

        private void btOk_Click(object sender, EventArgs e)
        {
            if (validateRO())
            {
                if (!generateRo())
                { 
                    return;
                }
                if (cbPrintSigningPage.Checked)
                {
                    SigningDocumentPrint doc = new SigningDocumentPrint(ro);
                    doc.Print();
                }
                Close();
                Dispose();
            }
        }
        private void calculateTotalPriceEvent(object sender, EventArgs e)
        {
            calculateTotalPrice();
        }
        private void calculateTotalPrice()
        {
            double labor = 0, parts = 0, tires = 0, deposit = 0, gog=0, subTotal = 0, tax = 0, total = 0;
            // Labor
            foreach (DataGridViewRow row in dgvService.Rows)
            {
                labor += Convert.ToDouble(row.Cells[1].Value);
            }
            this.dataGridViewTotalPrice.Rows[0].Cells[0].Value = labor = labor * Convert.ToDouble(nudHourlyRate.Value);
            //Parts
            foreach (DataGridViewRow row in dgvPart.Rows)
            {
                parts += Convert.ToDouble(row.Cells[4].Value);
            }
            this.dataGridViewTotalPrice.Rows[0].Cells[1].Value = parts = Math.Round(parts, 2);
            //Gas Oil Grease
            this.dataGridViewTotalPrice.Rows[0].Cells[2].Value = gog = Convert.ToDouble(tbGOG.Text);
            // Tires
            this.dataGridViewTotalPrice.Rows[0].Cells[4].Value = tires = Convert.ToDouble(nudTires.Value * 3);
            // Deposit
            deposit = getDepositFromDgv();
           
            this.dataGridViewTotalPrice.Rows[0].Cells[6].Value = subTotal = labor + parts + tires + gog;
            this.dataGridViewTotalPrice.Rows[0].Cells[7].Value = tax = Math.Round(subTotal / 10, 2);
            this.dataGridViewTotalPrice.Rows[0].Cells[8].Value = total = subTotal + tax;
            tbDue.Text = "$" + Math.Round((total - deposit),2).ToString();
        }


        private void btAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewTotalPrice.Rows[0].Cells[5].Value = Convert.ToDouble(tbDeposit.Text) + getDepositFromDgv();
            }
            catch (Exception exception)
            {
                this.dataGridViewTotalPrice.Rows[0].Cells[5].Value = 0;
            }
            calculateTotalPrice();
        }

        private void cbOut_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerOut.Enabled = cbOut.Checked;
        }

        private void tbGOG_Validated(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(tbGOG.Text);

            }
            catch (Exception exception)
            {
                tbGOG.Text = "5.99";
            }
            calculateTotalPrice();
        }
        private void tbDeposit_Validated(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(tbDeposit.Text);

            }
            catch (Exception exception)
            {
                tbDeposit.Text = "0";
            }
        }
        private double getDepositFromDgv()
        {
            try
            {
                return Convert.ToDouble(dataGridViewTotalPrice.Rows[0].Cells[5].Value);
            }
            catch (Exception exception)
            {
                return 0;
            }
        }
        private void cycleColor( DataGridViewCell cell)
        {
            Color color = cell.Style.BackColor;
            if (color == new Color())
            {
                color = Color.PaleVioletRed;
            } else if(color == Color.PaleVioletRed)
            {
                color = Color.LightGreen;
            } else
            {
                color = new Color();
            }
            cell.Style.BackColor = color;
        }


        private void dgvPart_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                cycleColor(dgvPart.Rows[e.RowIndex].Cells[0]);

                dgvPart.ClearSelection();
            }
        }

        private void dgvPart_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (e.ColumnIndex == 0)
                dgvPart.ClearSelection();
        }

        private void RoForm_Load(object sender, EventArgs e)
        {
            dgvService.ClearSelection();
            dgvPart.ClearSelection();
        }

        private void tbRONum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        private void tbDecimalOnlyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btSelectCustomer_Click(object sender, EventArgs e)
        { 
            Customer selectedCustomer = Core.selectCustomerFromCustomerDialog();
            if (selectedCustomer != null)
            {
                ro.customer = selectedCustomer;
            }
            if (ro.customer != null)
                autoFillCustomer(ro.customer);
        }

        private void nudHourlyRate_ValueChanged(object sender, EventArgs e)
        {
            if (isNewRo)
            {
                Config.getInstance().HorlyRate = nudHourlyRate.Value;
            }
          
            
        }
    }
}
