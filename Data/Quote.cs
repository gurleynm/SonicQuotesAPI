using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace SonicQuotesAPI.Data
{
    public class Quote
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Speaker { get; set; }
        public string Text { get; set; }
        public string Location { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString();
        public string Alias { get; set; }
        public string PassKey { get; set; } = "no";
        public Quote() { }
        public Quote(SqlDataReader reader)
        {
            Id = reader[0].ToString();
            Speaker = reader[1].ToString();
            Text = reader[2].ToString();
            Location = reader[3].ToString();
            Date = reader[4].ToString();
            Alias = reader[5].ToString();
        }
        public Quote(string text, string speaker, string location, string date, string alias = "")
        {
            Text = text;
            Speaker = speaker;
            Location = location;
            Date = date;
            Alias = alias;
        }
        public Quote(Quote other)
        {
            Id = other.Id;
            Text = other.Text;
            Speaker = other.Speaker;
            Location = other.Location;
            Date = other.Date;
            Alias = other.Alias;
        }
    }
}
