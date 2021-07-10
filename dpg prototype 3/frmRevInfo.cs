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
    public partial class frmRevInfo : Form
    {
        DataBaseManager dbM;
        String cmd;
        DataTable top3TableH;
        DataTable top3TableP;
        DataTable top3CustTable;
        DataTable top3EmpTable;
        DataTable singleTableH;
        DataTable singleTableP;
        DataTable searchTableH;
        DataTable searchTableP;
        DataTable revGenTable;

        String[] searchType = { "Single item search", "Top 3 Products/Haircuts", "Total Revenue", "Top 3 Customers", "Top 3 Employees" };

        public frmRevInfo(ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.Scale((float)Screen.PrimaryScreen.Bounds.Width / this.Width, (float)Screen.PrimaryScreen.Bounds.Height / this.Height);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmRevInfo_Load(object sender, EventArgs e)
        {
            //setup tables
            top3TableH = new DataTable();
            top3TableP = new DataTable();
            top3CustTable = new DataTable();
            top3EmpTable = new DataTable();
            singleTableH = new DataTable();
            singleTableP = new DataTable();
            searchTableH = new DataTable();
            searchTableP = new DataTable();
            revGenTable = new DataTable();
            //setup search type
            cboSearchType.DataSource = searchType;
            cboSearchType.SelectedIndex = 1;
            //set initial t3 values
            btnCurrM.PerformClick();
        }

        private void setTop3(ref DataTable table, bool setHair)
        {
            if (setHair)
            {
                if (table.Rows.Count > 0)
                {
                    txtHID1.Text = table.Rows[0][0].ToString();
                    txtHName1.Text = table.Rows[0][1].ToString();
                    txtHQty1.Text = table.Rows[0][2].ToString();
                    txtHTtl1.Text = table.Rows[0][3].ToString();
                }
                if (table.Rows.Count > 1)
                {
                    txtHID2.Text = table.Rows[1][0].ToString();
                    txtHName2.Text = table.Rows[1][1].ToString();
                    txtHQty2.Text = table.Rows[1][2].ToString();
                    txtHTtl2.Text = table.Rows[1][3].ToString();
                }
                if (table.Rows.Count > 2)
                {
                    txtHID3.Text = table.Rows[2][0].ToString();
                    txtHName3.Text = table.Rows[2][1].ToString();
                    txtHQty3.Text = table.Rows[2][2].ToString();
                    txtHTtl3.Text = table.Rows[2][3].ToString();
                }
                if (table.Rows.Count == 0)
                {
                    txtHID1.Text = "";
                    txtHName1.Text = "";
                    txtHQty1.Text = "";
                    txtHTtl1.Text = "";
                    txtHID2.Text = "";
                    txtHName2.Text = "";
                    txtHQty2.Text = "";
                    txtHTtl2.Text = "";
                    txtHID3.Text = "";
                    txtHName3.Text = "";
                    txtHQty3.Text = "";
                    txtHTtl3.Text = "";
                }
            }
            else
            {
                if (table.Rows.Count > 0)
                {
                    txtPID1.Text = table.Rows[0][0].ToString();
                    txtPName1.Text = table.Rows[0][1].ToString();
                    txtPQty1.Text = table.Rows[0][2].ToString();
                    txtPTtl1.Text = table.Rows[0][3].ToString();
                }
                if (table.Rows.Count > 1)
                {
                    txtPID2.Text = table.Rows[1][0].ToString();
                    txtPName2.Text = table.Rows[1][1].ToString();
                    txtPQty2.Text = table.Rows[1][2].ToString();
                    txtPTtl2.Text = table.Rows[1][3].ToString();
                }
                if (table.Rows.Count > 2)
                {
                    txtPID3.Text = table.Rows[2][0].ToString();
                    txtPName3.Text = table.Rows[2][1].ToString();
                    txtPQty3.Text = table.Rows[2][2].ToString();
                    txtPTtl3.Text = table.Rows[2][3].ToString();
                }
                if (table.Rows.Count == 0)
                {
                    txtPID1.Text = "";
                    txtPName1.Text = "";
                    txtPQty1.Text = "";
                    txtPTtl1.Text = "";
                    txtPID2.Text = "";
                    txtPName2.Text = "";
                    txtPQty2.Text = "";
                    txtPTtl2.Text = "";
                    txtPID3.Text = "";
                    txtPName3.Text = "";
                    txtPQty3.Text = "";
                    txtPTtl3.Text = "";
                }
            }
        }
        private void setTop3Cust(ref DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                txtCID1.Text = table.Rows[0][0].ToString();
                txtCName1.Text = table.Rows[0][1].ToString();
                txtCTot1.Text = table.Rows[0][2].ToString();
            }
            if (table.Rows.Count > 1)
            {
                txtCID2.Text = table.Rows[1][0].ToString();
                txtCName2.Text = table.Rows[1][1].ToString();
                txtCTot2.Text = table.Rows[1][2].ToString();
            }
            if (table.Rows.Count > 2)
            {
                txtCID3.Text = table.Rows[2][0].ToString();
                txtCName3.Text = table.Rows[2][1].ToString();
                txtCTot3.Text = table.Rows[2][2].ToString();
            }
        }
        private void setTop3Emp(ref DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                txtEID1.Text = table.Rows[0][0].ToString();
                txtEName1.Text = table.Rows[0][1].ToString();
                txtETot1.Text = table.Rows[0][2].ToString();
            }
            if (table.Rows.Count > 1)
            {
                txtEID2.Text = table.Rows[1][0].ToString();
                txtEName2.Text = table.Rows[1][1].ToString();
                txtETot2.Text = table.Rows[1][2].ToString();
            }
            if (table.Rows.Count > 2)
            {
                txtEID3.Text = table.Rows[2][0].ToString();
                txtEName3.Text = table.Rows[2][1].ToString();
                txtETot3.Text = table.Rows[2][2].ToString();
            }
        }
        private void setSingleItem(ref DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                txtSID.Text = table.Rows[0][0].ToString();
                txtSName.Text = table.Rows[0][1].ToString();
                txtSQuantity.Text = table.Rows[0][2].ToString();
                txtSTotal.Text = table.Rows[0][3].ToString();
            }
            else
            {
                MessageBox.Show("This product/Haircut has not been sold within the given time period!");
            }
        }

        private void setRevGen(ref DataTable table)
        {
            txtRevHair.Text = table.Rows[0][0].ToString();
            txtRevProd.Text = table.Rows[1][0].ToString();
            txtRevTotal.Text = ((Decimal)table.Rows[0][0] + (Decimal)table.Rows[1][0]).ToString();
        }

        private void btnCurrD_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 4)
            {
                if (dbM.execQry("exec sp_getTopEmployees 1", ref top3EmpTable))
                    setTop3Emp(ref top3EmpTable);
            }
            if (cboSearchType.SelectedIndex == 3)
            {
                if (dbM.execQry("exec sp_BestCustomers 1", ref top3CustTable))
                    setTop3Cust(ref top3CustTable);
            }
            if (cboSearchType.SelectedIndex == 2)
            {
                if (dbM.execQry("exec sp_GetTotalSales 1", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (dbM.execQry("exec sp_GetBestSelling 1,0", ref top3TableH))
                    setTop3(ref top3TableH, true);
                //get/set prod
                if (dbM.execQry("exec sp_GetBestSelling 1,1", ref top3TableP))
                    setTop3(ref top3TableP, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref searchTableH))
                    {
                        if (searchTableH.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 1,0,null,null," + searchTableH.Rows[0][0].ToString(), ref singleTableH))
                            {
                                setSingleItem(ref singleTableH);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref searchTableP))
                    {
                        if (searchTableP.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 1,1,null,null," + searchTableP.Rows[0][0].ToString(), ref singleTableP))
                            {
                                setSingleItem(ref singleTableP);
                            }
                        }
                    }
                }
            }
        }

        private void btnCurrM_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 4)
            {
                if (dbM.execQry("exec sp_getTopEmployees 2", ref top3EmpTable))
                    setTop3Emp(ref top3EmpTable);
            }
            if (cboSearchType.SelectedIndex == 3)
            {
                if (dbM.execQry("exec sp_BestCustomers 2", ref top3CustTable))
                    setTop3Cust(ref top3CustTable);
            }
            if (cboSearchType.SelectedIndex == 2)
            {
                if (dbM.execQry("exec sp_GetTotalSales 2", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (dbM.execQry("exec sp_GetBestSelling 2,0", ref top3TableH))
                    setTop3(ref top3TableH, true);
                //get/set prod
                if (dbM.execQry("exec sp_GetBestSelling 2,1", ref top3TableP))
                    setTop3(ref top3TableP, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref searchTableH))
                    {
                        if (searchTableH.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 2,0,null,null," + searchTableH.Rows[0][0].ToString(), ref singleTableH))
                            {
                                setSingleItem(ref singleTableH);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref searchTableP))
                    {
                        if (searchTableP.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 2,1,null,null," + searchTableP.Rows[0][0].ToString(), ref singleTableP))
                            {
                                setSingleItem(ref singleTableP);
                            }
                        }
                    }
                }
            }
        }

        private void btnCurrY_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 4)
            {
                if (dbM.execQry("exec sp_getTopEmployees 3", ref top3EmpTable))
                    setTop3Emp(ref top3EmpTable);
            }
            if (cboSearchType.SelectedIndex == 3)
            {
                if (dbM.execQry("exec sp_BestCustomers 3", ref top3CustTable))
                    setTop3Cust(ref top3CustTable);
            }
            if (cboSearchType.SelectedIndex == 2)
            {
                if (dbM.execQry("exec sp_GetTotalSales 3", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (dbM.execQry("exec sp_GetBestSelling 3,0", ref top3TableH))
                    setTop3(ref top3TableH, true);
                //get/set prod
                if (dbM.execQry("exec sp_GetBestSelling 3,1", ref top3TableP))
                    setTop3(ref top3TableP, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref searchTableH))
                    {
                        if (searchTableH.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 3,0,null,null," + searchTableH.Rows[0][0].ToString(), ref singleTableH))
                            {
                                setSingleItem(ref singleTableH);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref searchTableP))
                    {
                        if (searchTableP.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 3,1,null,null," + searchTableP.Rows[0][0].ToString(), ref singleTableP))
                            {
                                setSingleItem(ref singleTableP);
                            }
                        }
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 4)
            {
                if (dbM.execQry("exec sp_getTopEmployees 4,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref top3EmpTable))
                    setTop3Emp(ref top3EmpTable);
            }
            if (cboSearchType.SelectedIndex == 3)
            {
                if (dbM.execQry("exec sp_BestCustomers 4,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref top3CustTable))
                    setTop3Cust(ref top3CustTable);
            }
            if (cboSearchType.SelectedIndex == 2)
            {
                if (dbM.execQry("exec sp_GetTotalSales 4,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (dbM.execQry("exec sp_GetBestSelling 4,0,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref top3TableH))
                    setTop3(ref top3TableH, true);
                //get/set prod
                if (dbM.execQry("exec sp_GetBestSelling 4,1,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref top3TableP))
                    setTop3(ref top3TableP, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref searchTableH))
                    {
                        if (searchTableH.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 4,0,'" + txtStartD.Text + "','" + txtEndD.Text + "'," + searchTableH.Rows[0][0].ToString(), ref singleTableH))
                            {
                                setSingleItem(ref singleTableH);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (dbM.execQry("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref searchTableP))
                    {
                        if (searchTableP.Rows.Count > 0)
                        {
                            if (dbM.execQry("exec sp_GetBestSelling 4,1,'" + txtStartD.Text + "','" + txtEndD.Text + "'," + searchTableP.Rows[0][0].ToString(), ref singleTableP)) {
                                setSingleItem(ref singleTableP);
                            }
                        }
                    }
                }
            }
        }
    }
}
