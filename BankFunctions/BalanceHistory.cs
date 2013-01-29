using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Main;
using System.Data.SqlClient;
using System.Data;

namespace BankFunctions
{
    public class BalanceHistory
    {
        //public BalanceHistory(String accNo, String starTime, String endTime)
        //{
        //    getHistoyr(accNo,starTime,endTime);
        //}

        public DataTable getHistoyr(String accNo, String starTime, String endTime)
        {
            DataTable table = new DataTable();
            MakeDataConnection dataConnection = new MakeDataConnection();
            SqlConnection conn = dataConnection.makeConnection();
            try
            {
                conn.Open();
                String command = "SELECT balance, transactionTime as time FROM Account WHERE  accNo='" + accNo + "' AND transactionTime<'" + endTime + "' AND transactionTime>'" + starTime + "'";
                using (SqlDataAdapter a = new SqlDataAdapter(command, conn))
                {
                    // 3
                    // Use DataAdapter to fill DataTable
                    //DataTable t = new DataTable();
                    a.Fill(table);

                }
                //SqlCommand getHistory = new SqlCommand("SELECT balance, transactionTime as time FROM Account WHERE  accNo='" + accNo + "' AND transactionTime<'" + endTime + "' AND transactionTime>'" + starTime + "'", conn);
                //SqlDataReader reader = null;
                //reader = getHistory.ExecuteReader();
                //Console.WriteLine("{0}\t\t{1}", "Balance", "Time");
                //while (reader.Read())
                //{
                //    //Console.WriteLine(Convert.ToString(reader["credit"]), Convert.ToString(reader["debit"]));
                    
                //    Console.WriteLine("|{0}|{1}", reader["balance"], reader["time"]);
                //}
                //reader.Close();
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return table;
        }
    }
}
