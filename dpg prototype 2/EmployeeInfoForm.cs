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
    public partial class frmEmpInfo : Form
    {
        int id;
        DataBaseManager dbM;
        String cmd;
        DataTable table;

        public frmEmpInfo(int id, ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.id = id;
            this.dbM = dbM;
            this.Scale(((float)Screen.PrimaryScreen.Bounds.Width / this.Width), ((float)Screen.PrimaryScreen.Bounds.Height / this.Height));
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmEmpInfo_Load(object sender, EventArgs e)
        {
            //retrieve data from db
            cmd = "exec sp_GetEmpInfo " + id.ToString();
            table = new DataTable();
            if (!dbM.execQry(cmd, ref table))
            {
                MessageBox.Show(dbM.err);
                this.Close();
            }

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
            table.Dispose();
        }
    }
}
