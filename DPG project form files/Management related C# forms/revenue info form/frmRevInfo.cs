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

namespace DPG_revenue_info_form
{
    public partial class frmRevInfo : Form
    {
        SqlConnection connection;
        SqlCommand cmd = null;
        SqlDataAdapter adapter = null;
        DataTable top3Table;
        DataTable singleTable;
        DataTable revGenTable;

        String[] searchType = { "Single", "Top 3", "Total Revenue" };

        public frmRevInfo()
        {
            InitializeComponent();
        }

        private void frmRevInfo_Load(object sender, EventArgs e)
        {
            //establish connection
            connection = new SqlConnection("Data Source=VIANOTE004;Initial Catalog=DPGtestDB;Integrated Security=True");
            connection.Open();
            //setup tables
            top3Table = new DataTable();
            singleTable = new DataTable();
            revGenTable = new DataTable();
            //setup search type
            cboSearchType.DataSource = searchType;
            cboSearchType.SelectedIndex = 1;
            //set initial t3 values
            btnCurrM.PerformClick();
        }

        private bool executeQuery(String query, ref DataTable table)
        {
            if (cboSearchType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search type!", "Failed to execute query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmd != null)
            {
                cmd.Dispose();
            }
            if (adapter != null)
            {
                adapter.Dispose();
            }
            if(table.Rows.Count>0)
            {
                table.Dispose();
                table = new DataTable();
            }

            try
            {
                cmd = new SqlCommand(query, connection);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(table);
                return true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Failed to execute query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
            if (table.Rows.Count > 0)
            {
                txtRevHair.Text = table.Rows[0][0].ToString();
                txtRevProd.Text = table.Rows[1][0].ToString();
                txtRevTotal.Text = ((Decimal)table.Rows[0][0] + (Decimal)table.Rows[1][0]).ToString();
            }
            else
            {
                MessageBox.Show("There have been no sales during this time period!");
            }
        }

        private void btnCurrD_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 2)
            {
                if (executeQuery("exec sp_GetTotalSales 1", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (executeQuery("exec sp_GetBestSelling 1,0", ref top3Table))
                    setTop3(ref top3Table, true);
                //get/set prod
                if (executeQuery("exec sp_GetBestSelling 1,1", ref top3Table))
                    setTop3(ref top3Table, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 1,0,null,null," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 1,1,null,null," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
            }
        }

        private void btnCurrM_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 2)
            {
                if (executeQuery("exec sp_GetTotalSales 2", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (executeQuery("exec sp_GetBestSelling 2,0", ref top3Table))
                    setTop3(ref top3Table, true);
                //get/set prod
                if (executeQuery("exec sp_GetBestSelling 2,1", ref top3Table))
                    setTop3(ref top3Table, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 2,0,null,null," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 2,1,null,null," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
            }
        }

        private void btnCurrY_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 2)
            {
                if (executeQuery("exec sp_GetTotalSales 3", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (executeQuery("exec sp_GetBestSelling 3,0", ref top3Table))
                    setTop3(ref top3Table, true);
                //get/set prod
                if (executeQuery("exec sp_GetBestSelling 3,1", ref top3Table))
                    setTop3(ref top3Table, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 3,0,null,null," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 3,1,null,null," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedIndex == 2)
            {
                if (executeQuery("exec sp_GetTotalSales 4,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref revGenTable))
                    setRevGen(ref revGenTable);
            }
            if (cboSearchType.SelectedIndex == 1)
            {
                //get/set hair
                if (executeQuery("exec sp_GetBestSelling 4,0,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref top3Table))
                    setTop3(ref top3Table, true);
                //get/set prod
                if (executeQuery("exec sp_GetBestSelling 4,1,'" + txtStartD.Text + "','" + txtEndD.Text + "'", ref top3Table))
                    setTop3(ref top3Table, false);
            }
            if (cboSearchType.SelectedIndex == 0)
            {
                if (rdoH.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',1", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 4,0,'" + txtStartD.Text + "','" + txtEndD.Text + "'," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
                if (rdoP.Checked)
                {
                    if (executeQuery("exec sp_SearchByName '" + txtNameSearch.Text + "',2", ref singleTable))
                    {
                        if (singleTable.Rows.Count > 0)
                        {
                            if (executeQuery("exec sp_GetBestSelling 4,1,'" + txtStartD.Text + "','" + txtEndD.Text + "'," + singleTable.Rows[0][0].ToString(), ref singleTable))
                            {
                                setSingleItem(ref singleTable);
                            }
                        }
                    }
                }
            }
        }

        private void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdoP_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rdoH_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
