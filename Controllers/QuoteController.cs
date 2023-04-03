using Microsoft.AspNetCore.Mvc;
using SonicQuotesAPI.Data;
using SonicQuotesAPI.Repo;
using System.Text.Json;

namespace SonicQuotesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(ILogger<QuoteController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetQuotes")]
        public string Get()
        {
            return JsonSerializer.Serialize<List<Quote>>(QuoteRepo.GetQuotes());
        }

        [HttpPost(Name = "PostQuotes")]
        public string Post([FromBody] Quote quote)
        {
            if (quote.Alias.ToUpper() == "NONE")
                quote.Alias = "";

            var t = QuoteRepo.AddOrUpdate(quote);

            return JsonSerializer.Serialize<List<Quote>>(t.ToList());
        }
        [HttpDelete(Name = "DeleteQuotes")]
        public string Delete([FromBody] Quote quote)
        {
            QuoteRepo.Remove(quote);

            return JsonSerializer.Serialize<List<Quote>>(QuoteRepo.Quotes);
        }
    }
}