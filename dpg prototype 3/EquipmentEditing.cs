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
    public partial class frmEqpEdit : Form
    {
        //prevent form from being dragged
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch(m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }
        //--------------------------------------

        DataBaseManager dbM;
        String empCmd;
        String eqpCmd;
        DataTable empTable;
        DataTable eqpTable;
        CurrencyManager eqpManager;

        int currentState = 1;

        public frmEqpEdit(ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.Scale(((float)Screen.PrimaryScreen.Bounds.Width / this.Width), ((float)Screen.PrimaryScreen.Bounds.Height / this.Height));
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmEqpEdit_Load(object sender, EventArgs e)
        {
            //establish commands
            empCmd = "SELECT Employee_ID FROM Employee ORDER BY Employee_Name";
            eqpCmd = "SELECT * FROM Equipment ORDER BY Employee_ID ASC";
            //establish data tables
            empTable = new DataTable();
            eqpTable = new DataTable();
            //fill tables
            dbM.execQry(empCmd, ref empTable);
            dbM.execQry(eqpCmd, ref eqpTable);
            //setup cbo boxes
            cboEmpID.DataSource = empTable;
            cboEmpID.DisplayMember = "Employee_ID";
            //set initial text field values
            txtID.DataBindings.Add("Text", eqpTable, "Equipment_ID");
            txtName.DataBindings.Add("Text", eqpTable, "Equipment_Name");
            txtDOP.DataBindings.Add("Text", eqpTable, "Date_Of_Purchase");
            txtDesc.DataBindings.Add("Text", eqpTable, "Equipment_Desc");
            txtValue.DataBindings.Add("Text", eqpTable, "Eqp_Value");
            txtSearch.Text = "";
            //setup currency managers
            eqpManager = (CurrencyManager)this.BindingContext[eqpTable];

            //set initial state
            getEmpID();
            currentState = 1;
            setState();
        }

        private void getEmpID()
        {
            for (int i = 0; i < empTable.Rows.Count; i++)
            {
                if (eqpTable.Rows[eqpManager.Position][1].ToString() == empTable.Rows[i][0].ToString())
                {
                    cboEmpID.SelectedIndex = i;
                }
            }
        }

        private void setState()
        {
            switch (currentState)
            {
                case 1:     //search state
                    txtID.Enabled = false;
                    txtName.Enabled = false;
                    txtDOP.Enabled = false;
                    txtDesc.Enabled = false;
                    txtValue.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;

                    cboEmpID.Enabled = false;

                    btnAddNew.Enabled = true;
                    txtSearch.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnReset.Enabled = true;
                    btnDone.Enabled = true;
                    btnEdit.Enabled = true;
                    btnFind.Enabled = true;
                    break;
                case 2:     //edit state
                    txtID.Enabled = false;
                    txtName.Enabled = false;
                    txtDOP.Enabled = true;
                    txtDesc.Enabled = true;
                    txtValue.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;

                    cboEmpID.Enabled = true;

                    btnAddNew.Enabled = false;
                    txtSearch.Enabled = false;
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnReset.Enabled = false;
                    btnDone.Enabled = false;
                    btnEdit.Enabled = false;
                    btnFind.Enabled = false;
                    break;
                case 3:
                    txtID.Clear();
                    txtName.Clear();
                    txtDesc.Clear();
                    txtDOP.Clear();
                    txtValue.Clear();

                    txtID.Enabled = false;
                    txtName.Enabled = true;
                    txtDOP.Enabled = false;
                    txtDesc.Enabled = true;
                    txtValue.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;

                    cboEmpID.Enabled = true;

                    btnAddNew.Enabled = false;
                    txtSearch.Enabled = false;
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnReset.Enabled = false;
                    btnDone.Enabled = false;
                    btnEdit.Enabled = false;
                    btnFind.Enabled = false;
                    break;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (eqpManager.Position > 0)
            {
                eqpManager.Position--;
                getEmpID();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (eqpManager.Position < eqpManager.Count - 1)
            {
                eqpManager.Position++;
                getEmpID();
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
            String cmdString = "exec sp_SearchByName " + txtSearch.Text + ",6";
            DataTable searchTable = new DataTable();
            dbM.execQry(cmdString, ref searchTable);
            if (searchTable.Rows.Count > 0)
            {
                for(int i=0;i<eqpManager.Count;i++)
                {
                    if (eqpTable.Rows[i][0].ToString() == searchTable.Rows[0][0].ToString())
                        eqpManager.Position = i;
                }
            }
            else
            {
                MessageBox.Show("Search using another name", "Product not found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            currentState = 1;
            setState();
            empTable.Clear();
            eqpTable.Clear();
            dbM.execQry(empCmd, ref empTable);
            dbM.execQry(eqpCmd, ref eqpTable);
            eqpManager.Position = 0;
            cboEmpID.DataSource = empTable;
            cboEmpID.DisplayMember = "Employee_ID";
            getEmpID();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            eqpManager.Position = 0;
            getEmpID();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            eqpManager.Position = eqpManager.Count - 1;
            getEmpID();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            currentState = 2;
            setState();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            currentState = 1;
            setState();
            btnReset.PerformClick();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            String qry;
            //save
            if (currentState == 2)
            {
                qry = "exec sp_updateEquipment " + txtID.Text + "," + cboEmpID.Text + ",'" + txtName.Text + "','" + txtDesc.Text + "'," + txtValue.Text;
            }
            else
            {
                qry = "exec sp_EquipInsert " + cboEmpID.Text + ",'" + txtName.Text + "','" + txtDesc.Text + "'," + txtValue.Text;
            }
            if (!dbM.execNonQry(qry))
            {
                MessageBox.Show(dbM.err, "Failed to execute query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Success", "Operation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //
            btnCancel.PerformClick();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete
            String qry = "exec sp_DeleteRecord 6," + txtID.Text;
            if (!dbM.execNonQry(qry))
            {
                MessageBox.Show(dbM.err, "Failed to execute query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Success", "Operation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //
            btnCancel.PerformClick();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            currentState = 3;
            setState();
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            empTable.Dispose();
            eqpTable.Dispose();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}











