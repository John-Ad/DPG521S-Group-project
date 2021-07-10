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
    public partial class frmLoginMng : Form
    {
        DataBaseManager dbM;
        int id;
        int state = 1;      //1=update 2=insert
        public frmLoginMng(int id, ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.id = id;
            this.Scale(((float)Screen.PrimaryScreen.Bounds.Width / this.Width), ((float)Screen.PrimaryScreen.Bounds.Height / this.Height));
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void frmLoginMng_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            String qry = "select * from loginpage where Employee_ID=" + id.ToString();
            txtID.Text = id.ToString();
            if (dbM.execQry(qry, ref table))
            {
                if (table.Rows.Count > 0)
                {
                    txtUName.Text = table.Rows[0][1].ToString();
                    txtPWord.Text = table.Rows[0][2].ToString();
                }
                else
                {
                    MessageBox.Show("This employee has no login details! Please add details", "No login details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    state = 2;
                }
            }
            else
            {
                MessageBox.Show(dbM.err, "query failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String qry;
            if (state == 1)
            {
                qry = "exec sp_updateLogin " + id.ToString() + ",'" + txtUName.Text + "','" + txtPWord.Text + "'";
            }
            else
            {
                qry = "exec sp_LogInsert " + id.ToString() + ",'" + txtUName.Text + "','" + txtPWord.Text + "'";
            }

            if(dbM.execNonQry(qry))
            {
                MessageBox.Show("Successful");
                this.Close();
            }
            else
            {
                MessageBox.Show(dbM.err, "Error saving details. Please ensure that a username is specified and the password is 8 characters long");
            }

            if(state==2)
            {
                state = 1;
            }
        }
    }
}
