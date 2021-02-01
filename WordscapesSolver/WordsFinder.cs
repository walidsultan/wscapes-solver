using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WS.Wscapes
{
    public class WordsFinder
    {
        private const string _URL = "https://www.thewordfinder.com/word-unscrambler/";

        public static async Task<IEnumerable<IEnumerable<string>>> Get(string letters)
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

                    var words = new List<string>();
                    if (wordsNode != null)
                    {
                        words = wordsNode.Descendants("p").Select(x => x.InnerText).ToList();
                    }

                    words.AddRange(GetMissingWords(letters, i));

                    if (words.Count() == 0) continue;


                    result.Add(words);
                }

            }

            return result;
        }

        private static List<string> GetMissingWords(string letters, int wordLength)
        {
            List<string> missingWords = new List<string>();

            if (wordLength == 3)
            {
                bool twoMs = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'M') >= 2;
                bool oneA = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'A') >= 1;
                if (twoMs && oneA)
                {
                    missingWords.Add("mam");
                }
            }

            if (wordLength == 5)
            {
                bool oneM = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'M') >= 1;
                bool oneU = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'U') >= 1;
                bool oneL = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'L') >= 1;
                bool oneT = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'T') >= 1;
                bool oneI = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'I') >= 1;
                if (oneM && oneU && oneL && oneT && oneI)
                {
                    missingWords.Add("multi");
                }
            }


            if (wordLength == 4)
            {
                bool oneA = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'A') >= 1;
                bool oneF = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'F') >= 1;
                bool oneR = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'R') >= 1;
                bool oneO = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'O') >= 1;
                if (oneA && oneF && oneR && oneO)
                {
                    missingWords.Add("afro");
                }
            }


            if (wordLength == 6)
            {

                bool twoGs = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'G') >= 2;
                bool twoOs = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'O') >= 2;
                bool oneL = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'L') >= 1;
                bool oneE = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'E') >= 1;
                if (twoGs && twoOs && oneL && oneE)
                {
                    missingWords.Add("google");
                }

            }


            if (wordLength == 5)
            {

                bool oneB = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'B') >= 1;
                bool oneI = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'I') >= 1;
                bool oneR = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'R') >= 1;
                bool oneD = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'D') >= 1;
                bool oneY = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'Y') >= 1;
                if (oneB && oneI && oneR && oneD && oneY)
                {
                    missingWords.Add("birdy");
                }

                bool oneL = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'L') >= 1;
                bool oneN = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'N') >= 1;
                bool oneG = letters.ToUpper().ToCharArray().ToList().Count(x => x == 'G') >= 1;
                if (oneB && oneL && oneI && oneN && oneG)
                {
                    missingWords.Add("bling");
                }
            }

            return missingWords;
        }
    }
}
