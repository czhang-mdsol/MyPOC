using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRelated
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.TestNullParameter();
        }


        private void TestNullParameter()
        {
            using (var conn = GetConn())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT GETDATE() WHERE (@P IS NULL) OR (@P IS NOT NULL)";

                string arg1 = null;
                SqlParameter[] args = 
                    {
                        new SqlParameter("@P", (object)arg1??DBNull.Value)
                    };

                command.Parameters.AddRange(args);

                command.Connection = conn;
                conn.Open();
                string result = command.ExecuteScalar().ToString();
                Console.WriteLine(result);
            }
        }

        private SqlConnection GetConn(string dataSource = "localhost", string initialCatalog = "master", string userID = "sas", string password = "sas")
        {
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            connBuilder.DataSource = dataSource;
            connBuilder.InitialCatalog = initialCatalog;
            connBuilder.UserID = userID;
            connBuilder.Password = password;

            return new SqlConnection(connBuilder.ConnectionString);
        }
    }
}
