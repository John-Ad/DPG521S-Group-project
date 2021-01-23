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
    public partial class frmPOS : Form
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
        //-------------------------------

        private DataBaseManager dbM;
        int id;

        public frmPOS(int id,ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.id = id;
            this.Scale((float)Screen.PrimaryScreen.Bounds.Width / this.Width, (float)Screen.PrimaryScreen.Bounds.Height / this.Height);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            frmSales salesForm = new frmSales(id,ref dbM);
            salesForm.ShowDialog();
            salesForm.Dispose();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
        }

        private void btnHairProdLkp_Click(object sender, EventArgs e)
        {
            frmHairProdLookup hairProdLookup = new frmHairProdLookup(ref dbM);
            hairProdLookup.ShowDialog();
            hairProdLookup.Dispose();
        }

        private void btnCustLkp_Click(object sender, EventArgs e)
        {
            frmCustLookupEdit custLookupEdit = new frmCustLookupEdit(ref dbM);
            custLookupEdit.ShowDialog();
            custLookupEdit.Dispose();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
