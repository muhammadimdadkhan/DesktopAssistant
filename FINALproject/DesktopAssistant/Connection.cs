using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAssistant
{
    class Connection
    {

        
        string connect = @"Data Source=PHEECA\SQLEXPRESS;Initial Catalog=desktopAssistant;Integrated Security=True";
        SqlConnection conn = null;

        public Connection()
        {
            conn = new SqlConnection(connect);

        }
        public SqlConnection Connect()
        {
            if (conn != null)
            {
                conn.Open();
            }
            return conn;
        }
        //here we close connection, when ever you open connection it's necessary to close after communication 
        public void Close()
        {
            conn.Close();
        }

    }
}
