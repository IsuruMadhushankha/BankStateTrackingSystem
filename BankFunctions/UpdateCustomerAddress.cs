using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Main;
using System.Data.SqlClient;

namespace BankFunctions
{
    public class UpdateCustomerAddress
    {
        public UpdateCustomerAddress(int ssn, String address)
        {
            updateAddress(ssn,address);
        }

        public void updateAddress(int ssn, String address)
        {
            MakeDataConnection dataConnection = new MakeDataConnection();
            SqlConnection conn = dataConnection.makeConnection();
            try
            {
                conn.Open();
                SqlCommand getName = new SqlCommand("SELECT name FROM Customer where ssn = " + ssn + " AND isDelete = 0", conn);
                SqlDataReader reader = null;
                reader = getName.ExecuteReader();
                reader.Read();
                String name = Convert.ToString(reader["name"]);
                reader.Close();
                SqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "UPDATE Customer SET isDelete = 1 WHERE ssn = " + ssn + " AND isDelete = 0;"+
                    "INSERT INTO Customer VALUES (" + ssn + ",'" + name + "','" + address + "', CURRENT_TIMESTAMP,0)";
                sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        
        }
    }
}
