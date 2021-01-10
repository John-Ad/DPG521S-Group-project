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

namespace dpg_employee_edit_form
{
    public partial class frmEmpEdit : Form
    {
        SqlConnection connection;
        SqlCommand empCmd;
        SqlCommand posCmd;
        SqlDataAdapter adapter;
        SqlDataAdapter searchAdapter;
        DataTable empTable;
        DataTable posTable;
        DataTable searchTable;
        CurrencyManager empManager;

        int currentState = 1;

        public frmEmpEdit()
        {
            InitializeComponent();
        }

        private void frmEmpEdit_Load(object sender, EventArgs e)
        {
            //connect to db
            connection = new SqlConnection("Data Source=VIANOTE004;Initial Catalog=DPGtestDB;Integrated Security=True");
            //establish commands
            empCmd = new SqlCommand("SELECT * FROM Employee ORDER BY Employee_Name", connection);
            posCmd = new SqlCommand("SELECT Position_ID,Position_Name FROM Position", connection);
            //establish data adapters
            searchAdapter = new SqlDataAdapter();
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = empCmd;
            //establish data tables
            empTable = new DataTable();
            posTable = new DataTable();
            searchTable = new DataTable();
            //setup cbo boxes
            adapter.Fill(empTable);
            adapter.SelectCommand = posCmd;
            adapter.Fill(posTable);
            //setup cbo boxes
            cboPosName.DataSource = posTable;
            cboPosName.DisplayMember = "Position_Name";
            //set initial text field values
            txtID.DataBindings.Add("Text", empTable, "Employee_ID");
            txtPosID.DataBindings.Add("Text", empTable, "Position_ID");
            txtAge.DataBindings.Add("Text", empTable, "Age");
            txtName.DataBindings.Add("Text", empTable, "Employee_Name");
            txtCellNum.DataBindings.Add("Text", empTable, "Cell_Number");
            txtSearch.Text = "";
            //setup currency managers
            empManager = (CurrencyManager)this.BindingContext[empTable];

            //set initial state
            getPosName();
            currentState = 1;
            setState();
        }

        private void getPosName()
        {
            for (int i = 0; i < posTable.Rows.Count; i++)
            {
                if (txtPosID.Text == posTable.Rows[i][0].ToString())
                {
                    cboPosName.SelectedIndex = i;
                }
            }
        }

        private void setState()
        {
            switch (currentState)
            {
                case 1:     //search state
                    txtID.Enabled = false;
                    txtPosID.Enabled = false;
                    txtAge.Enabled = false;
                    txtName.Enabled = false;
                    txtCellNum.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;

                    cboPosName.Enabled = false;

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
                    txtPosID.Enabled = false;
                    txtAge.Enabled = true;
                    txtName.Enabled = true;
                    txtCellNum.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;

                    cboPosName.Enabled = true;

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
                    txtPosID.Clear();
                    txtName.Clear();
                    txtAge.Clear();
                    txtCellNum.Clear();

                    txtID.Enabled = false;
                    txtPosID.Enabled = false;
                    txtAge.Enabled = true;
                    txtName.Enabled = true;
                    txtCellNum.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;

                    cboPosName.Enabled = true;

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


        private void cboPosName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentState > 1)
                txtPosID.Text = posTable.Rows[cboPosName.SelectedIndex][0].ToString();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (empManager.Position > 0)
            {
                empManager.Position--;
                getPosName();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (empManager.Position < empManager.Count - 1)
            {
                empManager.Position++;
                getPosName();
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
            String cmdString = "exec sp_SearchByName " + txtSearch.Text + ",5";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            searchTable = new DataTable();
            searchAdapter.SelectCommand = cmd;
            searchAdapter.Fill(searchTable);
            if (searchTable.Rows.Count > 0)
            {
                for(int i=0;i<empManager.Count;i++)
                {
                    if (empTable.Rows[i][0].ToString() == searchTable.Rows[0][0].ToString())
                        empManager.Position = i;
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
            posTable.Clear();
            adapter.SelectCommand = empCmd;
            adapter.Fill(empTable);
            empManager.Position = 0;
            adapter.SelectCommand = posCmd;
            adapter.Fill(posTable);
            cboPosName.DataSource = posTable;
            cboPosName.DisplayMember = "Position_Name";
            getPosName();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            empManager.Position = 0;
            getPosName();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            empManager.Position = empManager.Count - 1;
            getPosName();
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
                qry = "exec sp_UpdateEmployee " + txtID.Text + "," + txtPosID.Text + ",'" + txtName.Text + "'," + txtAge.Text + ",'" + txtCellNum.Text + "'";
            }
            else
            {
                qry = "exec sp_EmpInsert " + txtPosID.Text + ",'" + txtName.Text + "'," + txtAge.Text + ",'" + txtCellNum.Text + "'";
            }
            if (!executeQuery(qry))
            {
                return;
            }
            //
            btnCancel.PerformClick();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete
                String qry = "exec sp_DeleteRecord 5," + txtID.Text;
                if (!executeQuery(qry))
                    return;
            //
            btnCancel.PerformClick();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            currentState = 3;
            setState();
        }

        private bool executeQuery(String query)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success", "Operation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Failed to execute query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
            connection.Dispose();
            adapter.Dispose();
            searchAdapter.Dispose();
            empTable.Dispose();
            searchTable.Dispose();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}











