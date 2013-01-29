using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Main;
using System.Data.SqlClient;
using System.Data;

namespace BankFunctions
{
    public class CreditDebitHistory
    {
        //public CreditDebitHistory(int ssn, String accNo, String starTime, String endTime)
        //{
        //    getHistoyr(ssn, accNo,starTime,endTime);
        //}

        public DataTable getHistory(int ssn, String accNo, String starTime, String endTime)
        {
            DataTable table = new DataTable();
            MakeDataConnection dataConnection = new MakeDataConnection();
            SqlConnection conn = dataConnection.makeConnection();
            try
            {
                conn.Open();
                //SqlCommand getHistory = new SqlCommand("SELECT credit,debit, transactionTime as time FROM AccManagement WHERE ssn= "+ssn+" AND accNo='"+accNo+"' AND transactionTime<'"+endTime+"' AND transactionTime>'"+starTime+"'", conn);
                //SqlDataReader reader = null;
                //reader = getHistory.ExecuteReader();
                //Console.WriteLine("{0}\t\t{1}\t\t{2}", "Credit", "Debit", "Time");
                //while (reader.Read())
                //{
                //    //Console.WriteLine(Convert.ToString(reader["credit"]), Convert.ToString(reader["debit"]));
                    
                //    Console.WriteLine("|{0}|{1}|{2}", reader["credit"], reader["debit"], reader["time"]);
                //}
                //reader.Close();
                String command = "SELECT credit,debit, transactionTime as time FROM AccManagement WHERE ssn= " + ssn + " AND accNo='" + accNo + "' AND transactionTime<'" + endTime + "' AND transactionTime>'" + starTime + "'";
                using (SqlDataAdapter a = new SqlDataAdapter(command, conn))
                {
                    // 3
                    // Use DataAdapter to fill DataTable
                    //DataTable t = new DataTable();
                    a.Fill(table);
                    
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
            return table;
        
        }
    }
}
