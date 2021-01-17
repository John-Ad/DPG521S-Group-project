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
    public partial class frmMngMenu : Form
    {
        private DataBaseManager dbM;
        public frmMngMenu(ref DataBaseManager dbM)
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

        private void btnRevInfo_Click(object sender, EventArgs e)
        {
            frmRevInfo revInfo = new frmRevInfo(ref dbM);
            revInfo.ShowDialog();
            revInfo.Dispose();
        }

        private void btnCustEdit_Click(object sender, EventArgs e)
        {
            frmCustLookupEdit custLookupEdit = new frmCustLookupEdit(ref dbM);
            custLookupEdit.ShowDialog();
            custLookupEdit.Dispose();
        }

        private void btnHairProdEdit_Click(object sender, EventArgs e)
        {
            frmHairProdEdit hairProdEdit = new frmHairProdEdit(ref dbM);
            hairProdEdit.ShowDialog();
            hairProdEdit.Dispose();
        }
    }
}
