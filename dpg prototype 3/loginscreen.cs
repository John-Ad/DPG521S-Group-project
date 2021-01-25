using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPG_prototype_v2
{
    public partial class loginscreen : Form
    {
        private DataBaseManager dbM;
        public loginscreen(ref DataBaseManager dbM)
        {
            InitializeComponent();
            this.dbM = dbM;
            this.Scale(((float)Screen.PrimaryScreen.Bounds.Width / this.Width), ((float)Screen.PrimaryScreen.Bounds.Height / this.Height));
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void reset()
        {
            username_textbox.Clear();
            password_textbox.Clear();
        }
        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            if(isValid())
            {
                string query = "SELECT * FROM loginpage WHERE login_username = '" + username_textbox.Text.Trim() + "'";
                DataTable loginTable = new DataTable();
                if (dbM.execQry(query, ref loginTable))
                {
                    if (loginTable.Rows.Count > 0)
                    {
                        if (loginTable.Rows[0][2].ToString() == password_textbox.Text)
                        {
                            dbM.execNonQry("exec sp_LoginLogInsert " + loginTable.Rows[0][0].ToString());
                            frmMainMenu frmMainMenu = new frmMainMenu(int.Parse(loginTable.Rows[0][0].ToString()), ref dbM);
                            frmMainMenu.ShowDialog();
                            frmMainMenu.Dispose();
                            reset();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password.", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No login details found. Incorrect user name or user does not have login details. Please have HR setup the login details.", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(dbM.err, "Query error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private bool isValid()
        {//checks whether the username and textboxes are empty or not and return an error if empty
            if (username_textbox.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Enter valid username please ", "Error");
                return false;
            }else if (password_textbox.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Enter valid password please ", "Error");
                return false;
            }
            return true;
        }
    }
}
