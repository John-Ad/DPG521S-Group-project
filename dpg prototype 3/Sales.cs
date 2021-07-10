using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DPG_prototype_v2
{
    public partial class frmSales : Form
    {
        DataBaseManager dbM;
        String hairCommand;
        String prodCommand;
        String empCommand;
        String custCommand;
        String barberCommand;
        DataTable hairTable;
        DataTable prodTable;
        DataTable empTable;
        DataTable custTable;
        DataTable barberTable;
        CurrencyManager hairManager;
        CurrencyManager prodManager;
        CurrencyManager empManager;
        CurrencyManager custManager;

        float totalAmount;
        int id;
        List<List<string>> dbInsertQuery;

        public frmSales(int id,ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.id = id;
            this.Scale((float)Screen.PrimaryScreen.Bounds.Width / this.Width, (float)Screen.PrimaryScreen.Bounds.Height / this.Height);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            totalAmount = 0;
            dbInsertQuery = new List<List<string>>();
            try
            {
                //establish command objects
                hairCommand = "SELECT * FROM Haircut";
                prodCommand = "SELECT * FROM Product";
                empCommand = "SELECT * FROM Employee WHERE Employee_ID="+id.ToString();
                custCommand = "SELECT * FROM Customer";
                barberCommand = "SELECT * FROM Employee WHERE Position_ID=4";
                //establish data adapters and tables
                hairTable = new DataTable();
                prodTable = new DataTable();
                empTable = new DataTable();
                custTable = new DataTable();
                barberTable = new DataTable();
                dbM.execQry(hairCommand, ref hairTable);
                dbM.execQry(prodCommand, ref prodTable);
                dbM.execQry(empCommand, ref empTable);
                dbM.execQry(custCommand, ref custTable);
                dbM.execQry(barberCommand, ref barberTable);
                //bind controls to data tables
                txtEmpID.DataBindings.Add("Text", empTable, "Employee_ID");
                //establish currency managers
                hairManager = (CurrencyManager)this.BindingContext[hairTable];
                prodManager = (CurrencyManager)this.BindingContext[prodTable];
                empManager = (CurrencyManager)this.BindingContext[empTable];
                custManager = (CurrencyManager)this.BindingContext[custTable];txtItemList.Text = "";
                //establish combo boxes
                cboCustName.DataSource = custTable;
                cboHairName.DataSource = hairTable;
                cboProdName.DataSource = prodTable;
                cboBarberName.DataSource = barberTable;
                cboCustName.DisplayMember = "Customer_Name";
                cboHairName.DisplayMember = "Haircut_Name";
                cboProdName.DisplayMember = "Product_Name";
                cboBarberName.DisplayMember = "Employee_Name";
                //set starting selection to nothing
                cboCustName.SelectedIndex = -1;
                cboHairName.SelectedIndex = -1;
                cboProdName.SelectedIndex = -1;
                cboBarberName.SelectedIndex = -1;
                //set initial text field values
                txtCustID.Text = "";
                txtTotal.Text = "";
                txtHairProdID.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error establishing data tables", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboCustName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if(cbo.SelectedIndex!=-1)
            {
                DataRow dataRow = custTable.Rows[cbo.SelectedIndex];
                txtCustID.Text = dataRow[0].ToString();
            }
            else
            {
                txtCustID.Text = "";
            }
        }

        private void cboHairName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if(cbo.SelectedIndex!=-1)
            {
                DataRow dataRow = hairTable.Rows[cbo.SelectedIndex];
                txtHairProdID.Text = dataRow[0].ToString();
                txtPrice.Text = dataRow[3].ToString();
                txtQuantity.Text = "";
                cboProdName.SelectedIndex = -1;
                cboBarberName.Enabled = true;
            }
        }

        private void cboProdName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if(cbo.SelectedIndex!=-1)
            {
                DataRow dataRow = prodTable.Rows[cbo.SelectedIndex];
                txtHairProdID.Text = dataRow[0].ToString();
                txtPrice.Text = dataRow[3].ToString();
                txtQuantity.Text = "";
                cboHairName.SelectedIndex = -1;
                cboBarberName.SelectedIndex = -1;
                cboBarberName.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtHairProdID.Text == "")
            {
                MessageBox.Show("Please select a Haircut or Product!", "Incomplete field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(txtQuantity.Text == "")
            {
                MessageBox.Show("Please enter a quantity!", "Incomplete field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(txtCustID.Text == "")
            {
                MessageBox.Show("Please choose a customer!", "Incomplete field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cboBarberName.Text == "" && cboHairName.SelectedIndex != -1)
            {
                MessageBox.Show("Please choose a barber!", "Incomplete field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String name = (cboHairName.SelectedIndex == -1) ? cboProdName.Text : cboHairName.Text;      //gets name based on which cbo is in use
            float total = float.Parse(txtPrice.Text) * float.Parse(txtQuantity.Text);
            String itemText = "ID: " + txtHairProdID.Text + "     NAME: " + name + "     QUANTITY: " + txtQuantity.Text + "      TOTAL: " + total.ToString();

            if(cboHairName.SelectedIndex != -1)     //if cboHairName is in use
            {
                dbInsertQuery.Add(new List<string>());
                dbInsertQuery[dbInsertQuery.Count - 1].Add("exec sp_ServRendInsert ");
                dbInsertQuery[dbInsertQuery.Count - 1].Add(barberTable.Rows[cboBarberName.SelectedIndex][0].ToString());
                dbInsertQuery[dbInsertQuery.Count - 1].Add(txtHairProdID.Text);
                dbInsertQuery[dbInsertQuery.Count - 1].Add(txtQuantity.Text);
                dbInsertQuery[dbInsertQuery.Count - 1].Add((float.Parse(txtQuantity.Text) * float.Parse(txtPrice.Text)).ToString());
                //dbInsertQuery += "exec sp_ServRendInsert " + barberTable.Rows[cboBarberName.SelectedIndex][0].ToString() + "," + txtCustID.Text + "," + txtHairProdID.Text + "," + "'" + cboCustName.Text + "'" + "," + txtQuantity.Text + "," + (float.Parse(txtQuantity.Text) * float.Parse(txtPrice.Text)).ToString() + "\r\n";
            }
            else
            {
                if (int.Parse(prodTable.Rows[cboProdName.SelectedIndex][4].ToString()) - int.Parse(txtQuantity.Text) >= 0)
                {
                    dbInsertQuery.Add(new List<string>());
                    dbInsertQuery[dbInsertQuery.Count - 1].Add("exec sp_ProdSoldInsert ");
                    dbInsertQuery[dbInsertQuery.Count - 1].Add(txtHairProdID.Text);
                    dbInsertQuery[dbInsertQuery.Count - 1].Add(txtQuantity.Text);
                    dbInsertQuery[dbInsertQuery.Count - 1].Add((float.Parse(txtQuantity.Text) * float.Parse(txtPrice.Text)).ToString());

                    //dbInsertQuery += "exec sp_ProdSoldInsert " + txtEmpID.Text + "," + txtCustID.Text + "," + txtHairProdID.Text + "," + "'" + cboCustName.Text + "'" + "," + txtQuantity.Text + "," + (float.Parse(txtQuantity.Text) * float.Parse(txtPrice.Text)).ToString() + "\r\n";
                    prodTable.Rows[cboProdName.SelectedIndex][4] = ((int)prodTable.Rows[cboProdName.SelectedIndex][4] - int.Parse(txtQuantity.Text));
                    //MessageBox.Show(prodTable.Rows[cboProdName.SelectedIndex][4].ToString());     //for debugging
                }
                else
                {
                    MessageBox.Show("There is not enough stock available for this item!", "Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            totalAmount += total;
            txtTotal.Text = totalAmount.ToString();
            txtItemList.AppendText(itemText + "\r\n");
            //txtItemList.AppendText(dbInsertQuery + "\r\n");       //for debugging
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (dbInsertQuery.Count == 0)
            {
                MessageBox.Show("No items have been selected!", "Incomplete transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable t = new DataTable();
            if (!dbM.execQry("exec sp_tranPerformed " + txtEmpID.Text + "," + txtCustID.Text + ",'" + cboCustName.Text + "'," + txtTotal.Text, ref t))
            {
                MessageBox.Show(dbM.err);
                return;
            }

            int tID = int.Parse(t.Rows[0][0].ToString());
            string qry = "";
            for (int i = 0; i < dbInsertQuery.Count; i++)
            {
                qry += dbInsertQuery[i][0];
                qry += tID.ToString();
                for (int f = 1; f < dbInsertQuery[i].Count; f++)
                {
                    qry += "," + dbInsertQuery[i][f];
                }
                qry += "\r\n";
            }
            //setup an String to execute the insertion procedures
            if (dbM.execNonQry(qry))
            {
                MessageBox.Show("Transaction successfully completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnReset.PerformClick();
            }
            else
                MessageBox.Show(dbM.err, "Insert failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            totalAmount = 0;
            dbInsertQuery.Clear();
            txtTotal.Text = "";
            txtHairProdID.Text = "";
            dbM.execQry(prodCommand, ref prodTable);
            cboHairName.SelectedIndex = -1;
            cboProdName.SelectedIndex = -1;
            cboBarberName.SelectedIndex = -1;
            cboBarberName.Enabled = false;
            txtPrice.Text = "";
            txtQuantity.Text = "";
            txtItemList.Text = "";
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
