using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MotoRev
{
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }
        bool triger = false;
        private List<RO> roQuery = new List<RO>();
        private void HistoryForm_Load(object sender, EventArgs e)
        {
           
            dtpLast.Value = DateTime.Now;
            dtpFirst.Value  = DateTime.Now.AddMonths(-1);
            dtpFirst.MaxDate = DateTime.Now;
            dtpLast.MaxDate = DateTime.Now;
            tbMakeModel.TextChanged += dataChanged;
            tbCustomer.TextChanged += dataChanged;
            dtpFirst.ValueChanged += dataChanged;
            dtpLast.ValueChanged += dataChanged;
            tbRo.Validated += dataChanged;
            cbBikesIn.CheckedChanged += dataChanged;
            tbCustomer.KeyPress += keyPressInData;
            tbMakeModel.KeyPress += keyPressInData;
            tbRo.KeyPress += keyPressInData;
            triger = true;

            fillRos();
            
        }
        private void fillRos()
        {
            dgvRo.Rows.Clear();
            roQuery.Clear();
            int id=0;
            try
            {
                id = Convert.ToInt32(tbRo.Text);
            }
            catch (Exception)
            {
                id = 0;
                tbRo.Text = "0";
            }
            double total = 0;
            double subTotal = 0;
            foreach (RO ro in DataManager.getRosList())
            {
                
                if (ro.filter(id, dtpFirst.Value, dtpLast.Value, true, tbMakeModel.Text, tbCustomer.Text))
                {
                    if (!ro.isCLosed())
                    {
                        if(!cbBikesIn.Checked)
                            continue;

                    }
                    else
                    {
                        total += ro.getTotal();
                        subTotal += ro.getSubTotal();
                    }

                    addRoToDgv(ro);
                    roQuery.Add(ro);
                }
                   
            }
            tbTotal.Text = total.ToString() +"$";
            tbSubTotal.Text = subTotal.ToString() + "$";
        }

        private void clearFileds()
        {
            dtpLast.Value = DateTime.Now.AddYears(-1);
            dtpFirst.Value = DateTime.Now.AddMonths(-1);
            triger = false;
            tbCustomer.Text = "";
            tbMakeModel.Text = "";
            tbRo.Text = "0";
            triger = true;
            fillRos();
        }

        private void addRoToDgv(RO ro)
        {
            object[] data = new object[7];
            data[0] = ro.getId();
            data[1] = ro.customer.name;
            data[2] = ro.bike.getMakeModel();
            data[3] = ro.dateIn.ToShortDateString();
            if(!ro.isCLosed())
            {
                data[4] = "n/a";
            } else
            {
                data[4] = ro.dateOut.ToShortDateString();
            }
            data[5] = ro.getSubTotal();
            data[6] = ro.getTotal();
            dgvRo.Rows.Add(data);
        }
       
        private void keyPressInData(object sender,KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Escape))
            {
                clearFileds();
            }
        }
        private void dataChanged(object sender, EventArgs e)
        {
            if(triger)
            fillRos();
        }


        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btPrintSelected_Click(object sender, EventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            if (dgvRo.SelectedCells.Count > 0)
            {
                if (!dgvRo.SelectedCells[0].OwningRow.IsNewRow)
                {
                    if (printDlg.ShowDialog() == DialogResult.OK)
                    {
                        int id = Convert.ToInt32(dgvRo.SelectedCells[0].OwningRow.Cells[0].Value);
                        RO ro = DataManager.getRoById(id);
                        printRo(printDlg.PrinterSettings, ro);
                    }
                       
                }
              
            }
            
           
        }
        private void printRo(PrinterSettings settings, RO ro)
        {
            PrintDocument printDoc;
            printDoc = new RoPrint(ro);
            printDoc.PrinterSettings = settings;
            printDoc.DocumentName = "RO #" + ro.getId();
            printDoc.Print();
            if (cbSignPage.Checked)
            {
                printDoc = new SigningDocumentPrint(ro);
                printDoc.PrinterSettings = settings;
                printDoc.DocumentName = "Sign RO #" + ro.getId();
                printDoc.Print();
            }
            
        }

        private void btPrintAll_Click(object sender, EventArgs e)
        {

            PrintDialog printDlg = new PrintDialog();
            //Call ShowDialog 
            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                foreach(RO currentRo in roQuery)
                {
                    printRo(printDlg.PrinterSettings, currentRo);
                }
            }

        }

        private void dgvRo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dgvRo.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
            int roId = Convert.ToInt32(dgvRo.Rows[e.RowIndex].Cells[0].Value);
            if (roId < 1)
                return;
            RO editRO = DataManager.getRoById(roId);
            new RoForm(editRO).ShowDialog();
            fillRos();
        }

        private void dgvRo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Escape))
            {
                HashSet<DataGridViewRow> deletedRows = new HashSet<DataGridViewRow>();
                foreach (DataGridViewCell cell in dgvRo.SelectedCells)
                {
                    DataGridViewRow currentRow = cell.OwningRow;
                    if (!deletedRows.Contains(currentRow) && !currentRow.IsNewRow)
                    {
                        int roId = Convert.ToInt32(currentRow.Cells[0].Value);
                        if (roId < 1)
                            return;
                        RO editRO = DataManager.getRoById(roId);
                       
                        DialogResult res = MessageBox.Show("Are you sure you want to delete RO #" + roId.ToString() + " " + editRO.customer.name+"?", "Warrning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (res == DialogResult.OK)
                        {
                            dgvRo.Rows.Remove(currentRow);
                            deletedRows.Add(currentRow);
                            editRO.delete();
                        }
                       
                    }

                }
                fillRos();

            }
        }
    }
}
