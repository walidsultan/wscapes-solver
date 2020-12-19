using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceWrapper.Controllers
{
    [ApiController]
    [Route("solve")]
    public class WscapesController : ControllerBase
    {
        private const string _URL = "https://www.thewordfinder.com/word-unscrambler/";
        public WscapesController()
        {
        }

        [HttpGet]
        public async Task<IEnumerable<IEnumerable<string>>> Get([FromQuery] string letters)
        {
            List<List<string>> result = new List<List<string>>();
            using (var client = new HttpClient())
            {

                var formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(letters), "letters");
                formContent.Add(new StringContent("no"), "show_points");

                var response = await client.PostAsync(_URL, formContent);

                var html = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                for (int i = letters.Length; i > 2; i--)
                {
                    var wordsNode = doc.DocumentNode.Descendants("div").FirstOrDefault(x => x.Id == $"results-{i}");

                    if (wordsNode == null) continue;

                    var words = wordsNode.Descendants("p").Select(x => x.InnerText).ToList();

                    if (i == 3)
                    {
                        bool twoMs = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'M') >= 2;
                        bool oneA = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'A') >= 1;
                        if (twoMs && oneA) {
                            words.Add("mam");
                        }
                    }

                    result.Add(words);
                }

            }

            return result;
        }
    }
}
