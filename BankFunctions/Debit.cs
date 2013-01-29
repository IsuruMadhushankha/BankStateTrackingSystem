using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Main;

namespace BankFunctions
{
    public class Debit
    {
        public Debit(int ssn, String accNo, String branchCode, String money)
        {
            debitMoney(ssn, accNo, branchCode, money);
        }

        public void debitMoney(int ssn, String accNo, String branchCode, String money)
        {
            MakeDataConnection dataConnection = new MakeDataConnection();
            SqlConnection conn = dataConnection.makeConnection();
            try
            {
                conn.Open();
                SqlCommand getBalance = new SqlCommand("SELECT balance FROM account where accNo = '"+accNo+"' AND isDelete = 0",conn);
                SqlDataReader reader = null;
                reader = getBalance.ExecuteReader();
                reader.Read();
                int currentBalance = Convert.ToInt32(reader["balance"]);
                int newBalance = currentBalance + Convert.ToInt32(money);
                reader.Close();
                //add data for accManagment
                SqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "UPDATE account SET isDelete = 1 WHERE accNo = " + accNo + " AND isDelete = 0;"+
                "UPDATE AccManagement SET isDelete = 1 WHERE ssn = " + ssn + " AND isDelete = 0 AND accNo = '"+accNo+"';"+ 
                "INSERT INTO AccManagement (ssn,accNo,debit,transactionTime,isDelete) values(" + ssn + ",'" + accNo + "'," + money + ", CURRENT_TIMESTAMP,0);"+
                "INSERT INTO Account values('" + accNo + "','" + branchCode + "'," + newBalance + ", CURRENT_TIMESTAMP,0)";
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
