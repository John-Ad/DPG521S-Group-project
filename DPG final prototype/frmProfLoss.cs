using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPG_prototype_v2
{
    public partial class frmProfLoss : Form
    {
        private DataBaseManager dbM;
        int currentState;

        DataTable table;
        CurrencyManager manager;

        public frmProfLoss(ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.Scale(((float)Screen.PrimaryScreen.Bounds.Width / this.Width), ((float)Screen.PrimaryScreen.Bounds.Height / this.Height));
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmProfLoss_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            manager = (CurrencyManager)this.BindingContext[table];
            reset();
        }

        private void reset()
        {
            currentState = 1;
            string qry = "exec sp_getProfLossInfo";
            if (dbM.execQry(qry, ref table))
            {
                txtDate.DataBindings.Clear();
                txtExpenses.DataBindings.Clear();
                txtTarget.DataBindings.Clear();
                txtProfLoss.DataBindings.Clear();
                txtCompare.DataBindings.Clear();

                txtDate.DataBindings.Add("Text", table, "_Date");
                txtExpenses.DataBindings.Add("Text", table, "Expenses");
                txtTarget.DataBindings.Add("Text", table, "_Target");
                txtProfLoss.DataBindings.Add("Text", table, "Profit");
                txtCompare.DataBindings.Add("Text", table, "Compare");
                manager.Position = manager.Count - 1;
                checkCompare();
                setState();
            }
            else
                MessageBox.Show(dbM.err);
        }

        private void setState()
        {
            switch (currentState)
            {
                case 1:
                    txtDate.Enabled = false;
                    txtExpenses.Enabled = false;
                    txtTarget.Enabled = false;
                    txtProfLoss.Enabled = false;
                    txtCompare.Enabled = false;

                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnAddNew.Enabled = true;
                    btnDone.Enabled = true;
                    break;
                case 2:
                    txtDate.Enabled = false;
                    txtExpenses.Enabled = true;
                    txtTarget.Enabled = true;
                    txtProfLoss.Enabled = false;
                    txtCompare.Enabled = false;

                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDone.Enabled = false;
                    break;
                case 3:
                    txtDate.Enabled = false;
                    txtExpenses.Enabled = true;
                    txtTarget.Enabled = true;
                    txtProfLoss.Enabled = false;
                    txtCompare.Enabled = false;

                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDone.Enabled = false;

                    txtDate.Clear();
                    txtExpenses.Clear();
                    txtTarget.Clear();
                    txtProfLoss.Clear();
                    txtCompare.Clear();
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (manager.Position != manager.Count - 1)
            {
                MessageBox.Show("Only the current month can be edited!");
                return;
            }
            currentState = 2;
            setState();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            currentState = 3;
            setState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtExpenses.Text == "" || txtTarget.Text == "")
            {
                MessageBox.Show("Expenses and target must not be empty!");
                return;
            }

            string qry;
            if (currentState == 2)
            {
                qry = "exec sp_updateProfLoss " + txtExpenses.Text + "," + txtTarget.Text;
            }
            else
            {
                qry = "exec sp_ProfLossInsert " + txtExpenses.Text + "," + txtTarget.Text;
            }
            if (!dbM.execNonQry(qry))
            {
                MessageBox.Show(dbM.err);
            }
            else
            {
                MessageBox.Show("Success!");
                reset();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (manager.Position > 0)
                manager.Position--;
            checkCompare();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (manager.Position < manager.Count - 1)
                manager.Position++;
            checkCompare();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            manager.Position = 0;
            checkCompare();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            manager.Position = manager.Count - 1;
            checkCompare();
        }

        private void checkCompare()
        {
            if (table.Rows[manager.Position][4].ToString() == "-9999999.99")
            {
                txtCompare.Text = "No record for the previous year.";
            }
        }
    }
}
