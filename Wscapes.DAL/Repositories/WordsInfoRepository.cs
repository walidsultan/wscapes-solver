using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wscapes.DAL.Models;

namespace Wscapes.DAL.Repositories
{
    public class WordsInfoRepository
    {

        public void AddOrUpdateWords(IEnumerable<string> words)
        {
            using (var context = new WscapesDBContext())
            {
                var existingWords = context.WordsInfo.Where(x => words.Any(y => y == x.Word)).ToList();

                existingWords.ForEach(x => { x.Count++; x.UpdatedDate = DateTime.Now; });

                var newWords = words.Where(x => !existingWords.Any(y => y.Word == x)).Select(x => new WordInfo() { Id = Guid.NewGuid(), Word = x, Count = 1, CreatedDate = DateTime.Now });

                context.WordsInfo.AddRange(newWords);

                context.SaveChanges();
            }
        }

    }
}
