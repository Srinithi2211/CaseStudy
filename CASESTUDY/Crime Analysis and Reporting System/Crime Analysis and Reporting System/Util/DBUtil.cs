using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Util
{
    public static class DBUtil
    {

        public static SqlConnection getDBConnection()
        {
            SqlConnection conn;
            string connectionstring = "Data Source=DESKTOP-HFDISFV\\SQLEXPRESS;Initial Catalog=C_A_R_S;Integrated Security=True;TrustServerCertificate=True";
            conn = new SqlConnection();
            conn.ConnectionString = connectionstring;
            return conn;
        }
    }
}
