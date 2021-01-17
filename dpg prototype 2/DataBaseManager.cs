using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DPG_prototype_v2
{
    public class DataBaseManager
    {
        private SqlConnection connection;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        public String err;

        public DataBaseManager()
        {
            connection = new SqlConnection("Data Source=VIANOTE004;Initial Catalog=DPGtestDBv2;Integrated Security=True");
            connection.Open();
            adapter = new SqlDataAdapter();
        }
        public bool execNonQry(String qry)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd = new SqlCommand(qry, connection);
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch(SqlException ex)
            {
                err = ex.Message;
                return false;
            }
        }
        public bool execQry(String qry, ref DataTable table)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd = new SqlCommand(qry, connection);
            adapter.SelectCommand = cmd;
            try
            {
                table.Clear();
                adapter.Fill(table);
                return true;
            }
            catch(SqlException ex)
            {
                err = ex.Message;
                return false;
            }
        }
    }
}
