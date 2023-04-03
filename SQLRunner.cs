using Microsoft.Data.SqlClient;
using SonicQuotesAPI.Data;
using System.Data;

namespace SonicQuotesAPI.SQLHelper
{
    public class SQLRunner
    {
        SqlConnection connection;
        public SQLRunner()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "gurleyguy.mynetgear.com,1433";
                builder.UserID = "Sonic";
                builder.Password = "GroguB0mb";
                builder.InitialCatalog = "QuotesDB";
                builder.TrustServerCertificate = true;

                connection = new SqlConnection(builder.ConnectionString);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public List<Quote> GetQuotes()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            List<Quote> quotes = new List<Quote>();
            string sql = "SELECT * FROM Quotes";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        quotes.Add(new Quote(reader));
                    }
                }

            }
            connection.Close();
            return quotes;
        }
        public List<Quote> AddOrUpdateQuote(Quote q)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();


            string cmd = "AddOrUpdateQuote";

            int rowCount;
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", q.Id));
                command.Parameters.Add(new SqlParameter("@Text", q.Text));
                command.Parameters.Add(new SqlParameter("@Speaker", q.Speaker));
                command.Parameters.Add(new SqlParameter("@Location", q.Location));
                command.Parameters.Add(new SqlParameter("@Date", q.Date));
                command.Parameters.Add(new SqlParameter("@Alias", q.Alias));
                

                rowCount = command.ExecuteNonQuery();
            }

            return GetQuotes();
        }
        public List<Quote> RemoveQuote(string id)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            string cmd = "RemoveQuote";

            int rowCount;
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", id));

                rowCount = command.ExecuteNonQuery();
            }

            return GetQuotes();
        }
    }
}
