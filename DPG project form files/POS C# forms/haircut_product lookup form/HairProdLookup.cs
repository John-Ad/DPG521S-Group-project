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

namespace DPG_Product_Lookup_form
{
    public partial class frmHairProdLookup : Form
    {
        SqlConnection connection;
        SqlCommand hairCommand;
        SqlCommand prodCommand;
        SqlCommand searchCommand;
        SqlDataAdapter hairAdapter;
        SqlDataAdapter prodAdapter;
        SqlDataAdapter searchAdapter;
        DataTable hairTable;
        DataTable prodTable;
        DataTable searchTable;
        CurrencyManager hManager;
        CurrencyManager pManager;

        public frmHairProdLookup()
        {
            InitializeComponent();
        }

        private void frmHairProdLookup_Load(object sender, EventArgs e)
        {
            //connect to db
            connection = new SqlConnection("Data Source=VIANOTE004;Initial Catalog=DPGtestDB;Integrated Security=True");
            //establish commands
            hairCommand = new SqlCommand("SELECT * FROM Haircut", connection);
            prodCommand = new SqlCommand("SELECT * FROM Product", connection);
            //establish data adapters
            searchAdapter = new SqlDataAdapter();
            hairAdapter = new SqlDataAdapter();
            prodAdapter = new SqlDataAdapter();
            hairAdapter.SelectCommand = hairCommand;
            prodAdapter.SelectCommand = prodCommand;
            //establish data tables
            hairTable = new DataTable();
            prodTable = new DataTable();
            searchTable = new DataTable();
            //setup cbo boxes
            hairAdapter.Fill(hairTable);
            prodAdapter.Fill(prodTable);
            //setup cbo boxes
            cboHairName.DataSource = hairTable;
            cboProdName.DataSource = prodTable;
            cboHairName.DisplayMember = "Haircut_Name";
            cboProdName.DisplayMember = "Product_Name";
            //set cbo starting indices to nothing
            cboHairName.SelectedIndex = -1;
            cboProdName.SelectedIndex = -1;
            //set initial text field values
            txtID.Text = "";
            txtPrice.Text = "";
            txtDesc.Text = "";
            txtQuantity.Text = "";
            txtSearch.Text = "";
            //setup currency managers
            hManager = (CurrencyManager)this.BindingContext[hairTable];
            pManager = (CurrencyManager)this.BindingContext[prodTable];
        }

        private void cboHairName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHairName.SelectedIndex != -1)
            {
                DataRow dataRow;
                if (searchTable.Rows.Count > 0)
                {
                    dataRow = searchTable.Rows[cboHairName.SelectedIndex];
                }
                else
                {
                    dataRow = hairTable.Rows[cboHairName.SelectedIndex];
                }
                txtID.Text = dataRow[0].ToString();
                txtDesc.Text = dataRow[2].ToString();
                txtPrice.Text = dataRow[3].ToString();
                txtQuantity.Text = "";
                txtQuantity.Enabled = false;
                cboProdName.SelectedIndex = -1;
            }
        }
        
        private void cboProdName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProdName.SelectedIndex != -1)
            {
                DataRow dataRow;
                if (searchTable.Rows.Count > 0)
                {
                    dataRow = searchTable.Rows[cboProdName.SelectedIndex];
                }
                else
                {
                    dataRow = prodTable.Rows[cboProdName.SelectedIndex];
                }
                txtID.Text = dataRow[0].ToString();
                txtDesc.Text = dataRow[2].ToString();
                txtPrice.Text = dataRow[3].ToString();
                txtQuantity.Text = dataRow[4].ToString();
                txtQuantity.Enabled = true;
                cboHairName.SelectedIndex = -1;
            }
            
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (cboHairName.SelectedIndex != -1 && cboHairName.SelectedIndex > 0)
            {
                cboHairName.SelectedIndex--;
            }
            else if (cboProdName.SelectedIndex != -1 && cboProdName.SelectedIndex > 0)
            {
                cboProdName.SelectedIndex--;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (cboProdName.SelectedIndex != -1 && cboProdName.SelectedIndex < pManager.Count - 1)
            {
                cboProdName.SelectedIndex++;
            }
            else if (cboHairName.SelectedIndex != -1 && cboHairName.SelectedIndex < hManager.Count - 1)
            {
                cboHairName.SelectedIndex++;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Please enter a name", "Incomplete field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Focus();
                return;
            }
            String cmdString = "exec sp_SearchByName " + txtSearch.Text;
            searchTable = new DataTable();
            if (rdoHair.Checked)
            {
                cmdString += ",1";
                searchCommand = new SqlCommand(cmdString, connection);
                searchAdapter.SelectCommand = searchCommand;
                searchAdapter.Fill(searchTable);
                if (searchTable.Rows.Count > 0)
                {
                    cboHairName.DataSource = searchTable;
                    cboHairName.DisplayMember = "Haircut_Name";
                    cboHairName.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Search using another name", "Haircut not found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rdoProd.Checked)
            {
                cmdString += ",2";
                searchCommand = new SqlCommand(cmdString, connection);
                searchAdapter.SelectCommand = searchCommand;
                searchAdapter.Fill(searchTable);
                if (searchTable.Rows.Count > 0)
                {
                    cboProdName.DataSource = searchTable;
                    cboProdName.DisplayMember = "Product_Name";
                    cboProdName.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Search using another name", "Product not found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select an option to search for", "Incomplete field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rdoHair.Focus();
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cboHairName.DataSource = hairTable;
            cboProdName.DataSource = prodTable;
            cboHairName.DisplayMember = "Haircut_Name";
            cboProdName.DisplayMember = "Product_Name";
            cboHairName.SelectedIndex = -1;
            cboProdName.SelectedIndex = -1;
            txtID.Clear();
            txtDesc.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtSearch.Clear();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (cboHairName.SelectedIndex != -1 && cboHairName.SelectedIndex > 0)
            {
                cboHairName.SelectedIndex = 0;
            }
            else if (cboProdName.SelectedIndex != -1 && cboProdName.SelectedIndex > 0)
            {
                cboProdName.SelectedIndex = 0;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (cboProdName.SelectedIndex != -1 && cboProdName.SelectedIndex < pManager.Count - 1)
            {
                cboProdName.SelectedIndex = pManager.Count - 1;
            }
            else if (cboHairName.SelectedIndex != -1 && cboHairName.SelectedIndex < hManager.Count - 1)
            {
                cboHairName.SelectedIndex = hManager.Count - 1;
            }
        }
    }
}











