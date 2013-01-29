using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Main;
using System.Data.SqlClient;

namespace BankFunctions
{
    public class StartAccount
    {
        public StartAccount(int ssn, String name, String address, String contactNo, String debit, String accNo, String branchCode)
        {
            startAcc(ssn, name, address, contactNo, debit, accNo,branchCode);
        }

        
        public void startAcc(int ssn, String name, String address, String contactNo, String debit, String accNo,String branchCode)
        {
            MakeDataConnection dataConnection = new MakeDataConnection();
            SqlConnection conn = dataConnection.makeConnection();
            try
            {
                conn.Open();
                //check ssn is available or not in customer
                SqlCommand getRecord = new SqlCommand("select * from customer where ssn = "+ssn+" and isDelete = 0",conn);
                SqlDataReader reader = null;
                reader = getRecord.ExecuteReader();
                if (!reader.Read())
                {
                    //add new customer in to db
                    reader.Close();
                    SqlCommand cmd = new SqlCommand("INSERT INTO customer values(" + ssn + ",'" + name + "','" + address + "', CURRENT_TIMESTAMP,0)", conn);
                    cmd.ExecuteNonQuery();
                    //add contact for new customer
                    //SqlCommand addContacts = new SqlCommand("INSERT INTO CustomerContacts values(" + ssn + ",'" + contactNo + "', CURRENT_TIMESTAMP,0)", conn);
                    //addContacts.ExecuteNonQuery();
                    //add data for accManagment
                    SqlCommand addAccManagement = new SqlCommand("INSERT INTO AccManagement (ssn,accNo,debit,transactionTime,isDelete) values(" + ssn + ",'" + accNo + "'," + debit + ", CURRENT_TIMESTAMP,0)", conn);
                    addAccManagement.ExecuteNonQuery();
                    SqlCommand addAccount = new SqlCommand("INSERT INTO Account values('" + accNo + "','" + branchCode + "'," + debit + ", CURRENT_TIMESTAMP,0)", conn);
                    addAccount.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    SqlCommand addAccManagement = new SqlCommand("INSERT INTO AccManagement (ssn,accNo,debit,transactionTime,isDelete) values(" + ssn + ",'" + accNo + "'," + debit + ", CURRENT_TIMESTAMP,0)", conn);
                    addAccManagement.ExecuteNonQuery();
                    SqlCommand addAccount = new SqlCommand("INSERT INTO Account values('" + accNo + "','" + branchCode + "'," + debit + ", CURRENT_TIMESTAMP,0)", conn);
                    addAccount.ExecuteNonQuery();
                }

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
