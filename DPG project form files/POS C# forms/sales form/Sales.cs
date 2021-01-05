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

namespace DPG_sales_form
{
    public partial class frmSales : Form
    {
        SqlConnection connection;
        SqlCommand hairCommand;
        SqlCommand prodCommand;
        SqlCommand empCommand;
        SqlCommand custCommand;
        SqlCommand barberCommand;
        SqlDataAdapter hairAdapter;
        SqlDataAdapter prodAdapter;
        SqlDataAdapter empAdapter;
        SqlDataAdapter custAdapter;
        SqlDataAdapter barberAdapter;
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
        String dbInsertQuery;

        public frmSales()
        {
            InitializeComponent();
        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            totalAmount = 0;
            dbInsertQuery = "";
            try
            {
                //connect to db
                connection = new SqlConnection("Data Source=VIANOTE004;Initial Catalog=DPGtestDB;Integrated Security=True");
                connection.Open();
                //establish command objects
                hairCommand = new SqlCommand("SELECT * FROM Haircut", connection);
                prodCommand = new SqlCommand("SELECT * FROM Product", connection);
                empCommand = new SqlCommand("SELECT * FROM Employee WHERE Employee_ID=1", connection);
                custCommand = new SqlCommand("SELECT * FROM Customer", connection);
                barberCommand = new SqlCommand("SELECT * FROM Employee WHERE Position_ID=3", connection);
                //establish data adapters and tables
                hairAdapter = new SqlDataAdapter();
                prodAdapter = new SqlDataAdapter();
                empAdapter = new SqlDataAdapter();
                custAdapter = new SqlDataAdapter();
                barberAdapter = new SqlDataAdapter();
                hairTable = new DataTable();
                prodTable = new DataTable();
                empTable = new DataTable();
                custTable = new DataTable();
                barberTable = new DataTable();
                hairAdapter.SelectCommand = hairCommand;
                prodAdapter.SelectCommand = prodCommand;
                empAdapter.SelectCommand = empCommand;
                custAdapter.SelectCommand = custCommand;
                barberAdapter.SelectCommand = barberCommand;
                hairAdapter.Fill(hairTable);
                prodAdapter.Fill(prodTable);
                empAdapter.Fill(empTable);
                custAdapter.Fill(custTable);
                barberAdapter.Fill(barberTable);
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
            txtItemList.AppendText(itemText + "\r\n");

            totalAmount += total;
            txtTotal.Text = totalAmount.ToString();

            if(cboHairName.SelectedIndex != -1)     //if cboHairName is in use
            {
                dbInsertQuery += "exec sp_ServRendInsert " + barberTable.Rows[cboBarberName.SelectedIndex][1].ToString() + "," + txtCustID.Text + "," + txtHairProdID.Text + "," + "'" + cboCustName.Text + "'" + "," + txtTotal.Text + "\r\n";
            }
            else
            {
                dbInsertQuery += "exec sp_ProdSoldInsert " + txtEmpID.Text + "," + txtCustID.Text + "," + txtHairProdID.Text + "," + "'" + cboCustName.Text + "'" + "," + txtTotal.Text + "\r\n";
            }

            //txtItemList.AppendText(dbInsertQuery + "\r\n");
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (dbInsertQuery == "")
            {
                MessageBox.Show("No items have been selected!", "Incomplete transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //setup an SqlCommand to execute the insertion procedures
            SqlCommand cmd = new SqlCommand(dbInsertQuery, connection);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transaction successfully completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Insert failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            totalAmount = 0;
            dbInsertQuery = "";
            txtTotal.Text = "";
            txtHairProdID.Text = "";
            cboHairName.SelectedIndex = -1;
            cboProdName.SelectedIndex = -1;
            cboBarberName.SelectedIndex = -1;
            cboBarberName.Enabled = false;
            txtPrice.Text = "";
            txtQuantity.Text = "";
        }

        
    }
}
