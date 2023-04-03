using Microsoft.VisualBasic;
using SonicQuotesAPI.Data;
using SonicQuotesAPI.SQLHelper;

namespace SonicQuotesAPI.Repo
{
    public class QuoteRepo
    {
        public static List<Quote> Quotes { get; set; } = new List<Quote>();
        public static List<Quote> GetQuotes()
        {
            SQLRunner sqlr;
            sqlr = new SQLRunner();
            Quotes = sqlr.GetQuotes();
            return Quotes;
        }
        public static List<Quote> AddOrUpdate(Quote quote)
        {
            SQLRunner sqlr = new SQLRunner();
            Quote quoteExists = Quotes.FirstOrDefault(t => t.Id == quote.Id);

            if (quote.PassKey == Constants.SHA256(quote.Text + Constants.PassKey))
                return Quotes;

            if (quoteExists != null)
            {
                Remove(quoteExists);
                return AddOrUpdate(quote);
            }

            Quotes = sqlr.AddOrUpdateQuote(quote);

            return Quotes;
        }
        public static bool Remove(Quote quote)
        {
            if (quote.PassKey != Constants.SHA256(quote.Text + Constants.PassKey))
                return false;

            SQLRunner sqlr;
            sqlr = new SQLRunner();

            Quotes = sqlr.RemoveQuote(quote.Id);

            return true;
        }
    }
}
