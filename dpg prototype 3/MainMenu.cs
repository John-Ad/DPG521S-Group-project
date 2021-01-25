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
    public partial class frmMainMenu : Form
    {
        private DataBaseManager dbM;
        private int id;

        public frmMainMenu(int id,ref DataBaseManager dbM)
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

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            DataTable posTable = new DataTable();
            dbM.execQry("SELECT Position_ID FROM Employee WHERE Employee_ID=" + id, ref posTable);
            int posID = int.Parse(posTable.Rows[0][0].ToString());

            if (posID == 1)    //manager id
            {
                btnHR.Enabled = false;
            }
            if (posID == 2)    //cashier id
            {
                btnMng.Enabled = false;
                btnHR.Enabled = false;
            }
            if (posID == 3)     //hr id
            {
                btnPOS.Enabled = false;
                btnMng.Enabled = false;
            }
            if (posID > 3003)   //other positions that dont use the system
            {
                btnPOS.Enabled = false;
                btnMng.Enabled = false;
                btnHR.Enabled = false;
            }
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPOS pos = new frmPOS(id,ref dbM);
            pos.ShowDialog();
            pos.Dispose();
        }

        private void btnMng_Click(object sender, EventArgs e)
        {
            frmMngMenu mngMenu = new frmMngMenu(ref dbM);
            mngMenu.ShowDialog();
            mngMenu.Dispose();
        }

        private void btnHR_Click(object sender, EventArgs e)
        {
            frmHRMenu hrMenu = new frmHRMenu(ref dbM);
            hrMenu.ShowDialog();
            hrMenu.Dispose();
        }

        private void btnEmpInfo_Click(object sender, EventArgs e)
        {
            frmEmpInfo empInfo = new frmEmpInfo(id, ref dbM);
            empInfo.ShowDialog();
            empInfo.Dispose();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
