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

namespace DPG_employee_view_form
{
    public partial class frmEmpInfo : Form
    {
        int id;

        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable table;

        public frmEmpInfo(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void frmEmpInfo_Load(object sender, EventArgs e)
        {
            //establish connection to db
            connection = new SqlConnection("Data Source=VIANOTE004;Initial Catalog=DPGtestDB;Integrated Security=True");
            connection.Open();

            //retrieve data from db
            cmd = new SqlCommand("exec sp_GetEmpInfo " + id.ToString(), connection);
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            table = new DataTable();
            adapter.Fill(table);

            //set text          //there is no user interaction so databindings are unnecessary
            txtID.Text = table.Rows[0][0].ToString();
            txtName.Text = table.Rows[0][1].ToString();
            txtAge.Text = table.Rows[0][2].ToString();
            txtCellNum.Text = table.Rows[0][3].ToString();
            txtPosName.Text = table.Rows[0][4].ToString();
            txtPosDesc.Text = table.Rows[0][5].ToString();
            txtTaxNum.Text = table.Rows[0][6].ToString();
            txtHrsWrked.Text = table.Rows[0][7].ToString();
            txtRenumPerHr.Text = table.Rows[0][8].ToString();
            txtTransComplt.Text = table.Rows[0][9].ToString();
            txtRevGen.Text = table.Rows[0][10].ToString();
            txtMostHelpedCust.Text = table.Rows[0][11].ToString();
            txtNumOfItems.Text = table.Rows[0][12].ToString();
            txtTotalVal.Text = table.Rows[0][13].ToString();

            //set txt to read only
            txtID.ReadOnly = true;
            txtName.ReadOnly = true;
            txtAge.ReadOnly = true;
            txtCellNum.ReadOnly = true;
            txtPosName.ReadOnly = true;
            txtPosDesc.ReadOnly = true;
            txtTaxNum.ReadOnly = true;
            txtHrsWrked.ReadOnly = true;
            txtRenumPerHr.ReadOnly = true;
            txtTransComplt.ReadOnly = true;
            txtRevGen.ReadOnly = true;
            txtMostHelpedCust.ReadOnly = true;
            txtNumOfItems.ReadOnly = true;
            txtTotalVal.ReadOnly = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
            connection.Dispose();
            cmd.Dispose();
            table.Dispose();
            adapter.Dispose();
        }
    }
}
