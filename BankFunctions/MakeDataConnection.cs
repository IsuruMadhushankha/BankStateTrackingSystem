using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Main
{
    public class MakeDataConnection
    {
        public SqlConnection makeConnection()
        {
            SqlConnection thiscon = new SqlConnection();
            thiscon.ConnectionString = ("Data Source=ruwan-pc\\sqlexpressr2;Initial Catalog=stateTrackingTest;Integrated Security=True");
            return thiscon;
        }
    }
}
