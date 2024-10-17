using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Util
{
    public static class DBUtil
    {
        public static SqlConnection GetDBConnection()
        {
            string connString = "Data Source=DESKTOP-HFDISFV\\SQLEXPRESS;Initial Catalog=C_A_R_S;Integrated Security=True;TrustServerCertificate=True";
            return new SqlConnection(connString); // Return a new SqlConnection without opening it
        }
    }
}
