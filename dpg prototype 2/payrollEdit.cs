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
    public partial class frmPayrollEdit : Form
    {
        int currentState;

        DataBaseManager dbM;
        String cmd;
        DataTable empTable;
        DataTable pRollTable;
        CurrencyManager prManager;

        public frmPayrollEdit(ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.Scale(((float)Screen.PrimaryScreen.Bounds.Width / this.Width), ((float)Screen.PrimaryScreen.Bounds.Height / this.Height));
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void setState()
        {
            switch (currentState)
            {
                case 1:     //search state
                    txtID.Enabled = false;
                    cboEmpName.Enabled = false;
                    txtTaxNum.Enabled = false;
                    txtHrsWrkd.Enabled = false;
                    txtRenum.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;

                    btnAddNew.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnReset.Enabled = true;
                    btnDone.Enabled = true;
                    btnEdit.Enabled = true;
                    break;
                case 2:     //edit state
                    txtID.Enabled = false;
                    cboEmpName.Enabled = false;
                    txtTaxNum.Enabled = true;
                    txtHrsWrkd.Enabled = true;
                    txtRenum.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;

                    btnAddNew.Enabled = false;
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnReset.Enabled = false;
                    btnDone.Enabled = false;
                    btnEdit.Enabled = false;                    
                    break;
                case 3:
                    txtID.Clear();
                    cboEmpName.Text = "";
                    txtTaxNum.Clear();
                    txtHrsWrkd.Clear();
                    txtRenum.Clear();

                    txtID.Enabled = false;
                    cboEmpName.Enabled = true;
                    txtTaxNum.Enabled = true;
                    txtHrsWrkd.Enabled = true;
                    txtRenum.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;

                    btnAddNew.Enabled = false;
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnReset.Enabled = false;
                    btnDone.Enabled = false;
                    btnEdit.Enabled = false;                    
                    break;
            }
        }

        private void frmPayrollEdit_Load(object sender, EventArgs e)
        {
            pRollTable = new DataTable();
            empTable = new DataTable();
            cmd = "SELECT * FROM PAYROLL";
            dbM.execQry(cmd, ref pRollTable);
            txtID.DataBindings.Add("Text", pRollTable, "Employee_ID");
            txtTaxNum.DataBindings.Add("Text", pRollTable, "Tax_Number");
            txtHrsWrkd.DataBindings.Add("Text", pRollTable, "Hrs_Worked_Month");
            txtRenum.DataBindings.Add("Text", pRollTable, "Renum_Per_Hr");
            prManager = (CurrencyManager)this.BindingContext[pRollTable];
            setName();

            currentState = 1;
            setState();
        }

        private void setName()
        {
            cmd = "SELECT Employee_Name FROM Employee WHERE Employee_ID=" + txtID.Text;
            dbM.execQry(cmd, ref empTable);
            cboEmpName.Text = empTable.Rows[0][0].ToString();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (prManager.Position > 0)
            {
                prManager.Position--;
                setName();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (prManager.Position < prManager.Count - 1)
            {
                prManager.Position++;
                setName();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            prManager.Position = 0;
            setName();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            prManager.Position = prManager.Count - 1;
            setName();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }
        private void reset()
        {
            currentState = 1;
            setState();
            cmd = "SELECT * FROM PAYROLL";
            dbM.execQry(cmd, ref pRollTable);
            cboEmpName.DataSource = null;
            cboEmpName.Text = "";
            btnFirst.PerformClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            currentState = 2;
            setState();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String qry;
            if(currentState==2)
            {
                qry = "exec sp_updatePayroll " + txtID.Text + ",'" + txtTaxNum.Text + "'," + txtHrsWrkd.Text + "," + txtRenum.Text;
            }
            else
            {
                qry = "exec sp_PayrollInsert " + txtID.Text + ",'" + txtTaxNum.Text + "'," + txtHrsWrkd.Text + "," + txtRenum.Text;
            }

            if(dbM.execNonQry(qry))
            {
                MessageBox.Show("Success");
                reset();
            }
            else
            {
                MessageBox.Show(dbM.err);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            currentState = 3;
            setState();
            cmd = "SELECT Employee_ID,Employee_Name FROM Employee WHERE Employee_ID NOT IN(SELECT Employee_ID FROM Payroll)";
            dbM.execQry(cmd, ref empTable);
            if(empTable.Rows.Count<1)
            {
                MessageBox.Show("There are no employees without a payroll record!", "No new record needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset();
                return;
            }
            cboEmpName.DataSource = empTable;
            cboEmpName.DisplayMember = "Employee_Name";
            cboEmpName.SelectedIndex = -1;
        }

        private void cboEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo.SelectedIndex != -1)
            {
                txtID.Text = empTable.Rows[cbo.SelectedIndex][1].ToString();
            }
            else
                txtID.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd = "exec sp_DeleteRecord 7," + txtID.Text;
            if(dbM.execNonQry(cmd))
            {
                MessageBox.Show("Success");
                reset();
            }
            else
            {
                MessageBox.Show(dbM.err);
            }
        }
    }
}
