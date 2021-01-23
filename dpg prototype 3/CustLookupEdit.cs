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
    public partial class frmCustLookupEdit : Form
    {
        DataBaseManager dbM;
        String hairCommand;
        String custCommand;
        String searchCommand;
        DataTable hairTable;
        DataTable custTable;
        DataTable searchTable;
        CurrencyManager cManager;

        int currentState;       // 1=search 2=edit 3=add new

        public frmCustLookupEdit(ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.Scale((float)Screen.PrimaryScreen.Bounds.Width / this.Width, (float)Screen.PrimaryScreen.Bounds.Height / this.Height);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmHairProdLookup_Load(object sender, EventArgs e)
        {
            currentState = 1;
            //connect to db
            //establish commands
            custCommand = "SELECT * FROM Customer";
            //establish data tables
            custTable = new DataTable();
            searchTable = new DataTable();
            //setup cbo boxes
            dbM.execQry(custCommand, ref custTable);
            //setup currency managers
            cManager = (CurrencyManager)this.BindingContext[custTable];
            //setup cbo boxes
            cboCustName.DataSource = custTable;
            cboCustName.DisplayMember = "Customer_Name";
            //set initial text field values
            txtID.DataBindings.Add("Text", custTable, "Customer_ID");
            txtCellNum.DataBindings.Add("Text", custTable, "Cell_number");
            txtNumOfVis.DataBindings.Add("Text", custTable, "Num_of_Visits");
            txtSearch.Text = "";

            setState();
        }

        private void setState()
        {
            switch(currentState)
            {
                case 1:    //search state
                    txtID.ReadOnly = true;
                    cboFavHair.Enabled = false;
                    txtCellNum.ReadOnly = true;
                    txtNumOfVis.ReadOnly = true;
                    txtSearch.Enabled = true;

                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDone.Enabled = true;
                    btnFind.Enabled = true;
                    btnReset.Enabled = true;
                    btnAddNew.Enabled = true;

                    btnDelete.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                    break;
                case 2:     //edit state
                    txtID.ReadOnly = true;
                    cboFavHair.Enabled = true;
                    txtCellNum.ReadOnly = false;
                    txtNumOfVis.ReadOnly = false;
                    txtSearch.Enabled = false;

                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDone.Enabled = false;
                    btnFind.Enabled = false;
                    btnReset.Enabled = false;
                    btnAddNew.Enabled = false;

                    btnDelete.Enabled = true;
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    break;
                case 3:     //addNew state
                    txtID.ReadOnly = true;
                    cboFavHair.Enabled = true;
                    txtCellNum.ReadOnly = false;
                    txtNumOfVis.ReadOnly = false;
                    txtSearch.Enabled = false;
                    
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDone.Enabled = false;
                    btnFind.Enabled = false;
                    btnReset.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;

                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    break;
            }
        }

        private void getHairName(int index)
        {
            hairTable = new DataTable();
            String hCommandString = "SELECT * FROM Haircut WHERE Haircut_ID=" + ((searchTable.Rows.Count == 0) ? custTable.Rows[index][1].ToString() : searchTable.Rows[index][1].ToString());
            dbM.execQry(hCommandString, ref hairTable);
            if (hairTable.Rows.Count > 0)
            {
                cboFavHair.Text = hairTable.Rows[0][1].ToString();
            }
            else
            {
                cboFavHair.Text = "";
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (cboCustName.SelectedIndex != -1 && cboCustName.SelectedIndex > 0)
            {
                cboCustName.SelectedIndex--;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (searchTable.Rows.Count == 0 && cboCustName.SelectedIndex != -1 && cboCustName.SelectedIndex < cManager.Count - 1)
            {
                cboCustName.SelectedIndex++;
            }
            else if (cboCustName.SelectedIndex != -1 && cboCustName.SelectedIndex < searchTable.Rows.Count - 1)
            {
                cboCustName.SelectedIndex++;
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
            String cmdString = "exec sp_SearchByName " + txtSearch.Text + ",3";
            searchTable = new DataTable();
            dbM.execQry(cmdString, ref searchTable);
            if (searchTable.Rows.Count > 0)
            {
                cboCustName.DataSource = searchTable;
                cboCustName.DisplayMember = "Haircut_Name";
                cboCustName.SelectedIndex = 0;
                txtID.DataBindings.Clear();
                txtCellNum.DataBindings.Clear();
                txtNumOfVis.DataBindings.Clear();
                txtID.DataBindings.Add("Text", searchTable, "Customer_ID");
                txtCellNum.DataBindings.Add("Text", searchTable, "Cell_number");
                txtNumOfVis.DataBindings.Add("Text", searchTable, "Num_of_Visits");
                txtSearch.Text = "";
            }
            else
            {
                MessageBox.Show("Search using another name", "No matches found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            searchTable.Clear();
            dbM.execQry(custCommand, ref custTable);

            cboCustName.DataSource = custTable;
            cboCustName.DisplayMember = "Customer_Name";
            txtID.DataBindings.Clear();
            txtCellNum.DataBindings.Clear();
            txtNumOfVis.DataBindings.Clear();
            txtID.DataBindings.Add("Text", custTable, "Customer_ID");
            txtCellNum.DataBindings.Add("Text", custTable, "Cell_number");
            txtNumOfVis.DataBindings.Add("Text", custTable, "Num_of_Visits");
            txtSearch.Text = "";
            cboCustName.SelectedIndex = 0;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (cboCustName.SelectedIndex != -1 && cboCustName.SelectedIndex > 0)
            {
                cboCustName.SelectedIndex = 0;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (searchTable.Rows.Count == 0 && cboCustName.SelectedIndex != -1 && cboCustName.SelectedIndex < cManager.Count - 1)
            {
                cboCustName.SelectedIndex = cManager.Count - 1;
            }
            else if (cboCustName.SelectedIndex != -1 && cboCustName.SelectedIndex < searchTable.Rows.Count - 1)
            {
                cboCustName.SelectedIndex = searchTable.Rows.Count - 1;
            }
        }

        private void cboCustName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo.SelectedIndex >= 0 && custTable.Rows[cbo.SelectedIndex][1].ToString()!="")
            {
                getHairName(cbo.SelectedIndex);
            }
            else
            {
                cboFavHair.Text = "";
            }
        }

        private void cboFavHair_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            currentState = 2;
            setState();

            //allow user to choose new haircut
            hairTable = new DataTable();
            hairCommand = "SELECT Haircut_Name FROM Haircut";
            dbM.execQry(hairCommand, ref hairTable);
            cboFavHair.DataSource = hairTable;
            cboFavHair.DisplayMember = "Haircut_Name";

            //prevent user from selecting another customers name
            String name = cboCustName.Text;
            cboCustName.DataSource = null;
            cboCustName.Text = name;

            //clear remaining data bindings and keep current info
            String[] info = new string[2];
            info[0] = txtCellNum.Text;
            info[1] = txtNumOfVis.Text;
            txtCellNum.DataBindings.Clear();
            txtNumOfVis.DataBindings.Clear();
            txtCellNum.Text = info[0];
            txtNumOfVis.Text = info[1];
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            currentState = 3;
            setState();

            hairCommand = "SELECT Haircut_Name FROM Haircut";
            dbM.execQry(hairCommand, ref hairTable);
            cboFavHair.DataSource = hairTable;
            cboFavHair.DisplayMember = "Haircut_Name";
            cboFavHair.SelectedIndex = -1;

            cboCustName.DataSource = null;
            txtID.DataBindings.Clear();
            txtCellNum.DataBindings.Clear();
            txtNumOfVis.DataBindings.Clear();
            txtID.Clear();
            txtCellNum.Clear();
            txtNumOfVis.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            currentState = 1;
            setState();
            btnReset.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //get the haircut id based on text in txtFavHair
            String cmd;
            DataTable table = new DataTable();
            SqlDataAdapter adapter;
            if(cboFavHair.Text!="")
            {
                cmd = "exec sp_SearchByName '" + cboFavHair.Text + "', 1";
                dbM.execQry(cmd, ref table);
            }


            if (currentState == 2)
            {
                String updateStatement;
                if (cboFavHair.Text != "")
                {
                    updateStatement = "exec sp_updateCustomer " + txtID.Text + "," + table.Rows[0][0].ToString() + ",'" + cboCustName.Text + "','" + txtCellNum.Text + "'," + txtNumOfVis.Text;
                }
                else
                {
                    updateStatement = "exec sp_updateCustomer " + txtID.Text + ",null" + ",'" + cboCustName.Text + "','" + txtCellNum.Text + "'," + txtNumOfVis.Text;
                }
                if (dbM.execNonQry(updateStatement))
                    MessageBox.Show("Successfully saved!", "update successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show(dbM.err, "Failed to update record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (currentState == 3)
            {
                String updateStatement;
                if (cboFavHair.Text != "")
                {
                    updateStatement = "exec sp_CustInsert " + table.Rows[0][0].ToString() + ",'" + cboCustName.Text + "','" + txtCellNum.Text + "'," + txtNumOfVis.Text;
                }
                else
                {
                    updateStatement = "exec sp_CustInsert null" + ",'" + cboCustName.Text + "','" + txtCellNum.Text + "'," + txtNumOfVis.Text;
                }
                if (dbM.execNonQry(updateStatement))
                    MessageBox.Show("Successfully added!", "addition successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show(dbM.err, "Failed to add new record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            currentState = 1;
            setState();
            btnReset.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String deleteStatement = "exec sp_DeleteRecord 3," + txtID.Text;

            if (!dbM.execNonQry(deleteStatement))
            {
                MessageBox.Show("the record specified could not be deleted", "Failed to delete record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Record successfully deleted!", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentState = 1;
                setState();
                btnReset.PerformClick();
            }
        }

        private void frmCustLookupEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //dispose of other objects
            hairTable.Dispose();
            custTable.Dispose();
            searchTable.Dispose();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCellNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))      //allow only digits and control key presses
            {
                e.Handled = true;
            }
        }
    }
}











