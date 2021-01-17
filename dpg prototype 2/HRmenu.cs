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
    public partial class frmHRMenu : Form
    {
        private DataBaseManager dbM;
        public frmHRMenu(ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.Scale(((float)Screen.PrimaryScreen.Bounds.Width / this.Width), ((float)Screen.PrimaryScreen.Bounds.Height / this.Height));
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmMngMenu_Load(object sender, EventArgs e)
        {
        }

        private void btnEmpEdit_Click(object sender, EventArgs e)
        {
            frmEmpEdit empEdit = new frmEmpEdit(ref dbM);
            empEdit.ShowDialog();
            empEdit.Dispose();
        }

        private void btnPayRollEdit_Click(object sender, EventArgs e)
        {
            frmPayrollEdit payrollEdit = new frmPayrollEdit(ref dbM);
            payrollEdit.ShowDialog();
            payrollEdit.Dispose();
        }

        private void btnEqpEdit_Click(object sender, EventArgs e)
        {
            frmEqpEdit eqpEdit = new frmEqpEdit(ref dbM);
            eqpEdit.ShowDialog();
            eqpEdit.Dispose();
        }
    }
}
