using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace DataProcessing
{
    public class DataProcessor
    {        
        public Dictionary<string, int> ProcessData(List<string> book)
        {           
            string tmp;
            Regex regex = new Regex("[^\\p{L}'-]+");
            List<string> wordsList = new List<string>();
            ConcurrentBag<string> wordsListConcurrent = new ConcurrentBag<string>();
            Dictionary<string, int> occurrencesMap = new Dictionary<string, int>();
            ConcurrentDictionary<string, int> occurancesMapConcurrent = new ConcurrentDictionary<string, int>();
            int numberOfOccurrences = 1;
            
            Parallel.For(0, book.Count, i =>
            {
                tmp = book[i];
                List<string> words = regex.Split(tmp).ToList<string>();
                words.RemoveAll(word => word.Equals("") || word.Equals("--") || words.Equals("-"));
                foreach(string word in words.ConvertAll(word => word.ToLower()))
                {
                    wordsListConcurrent.Add(word);
                }                            
            });
            wordsList = wordsListConcurrent.ToList();
            
            Parallel.For(0, wordsList.Count, i =>
            {
                if (!occurancesMapConcurrent.ContainsKey(wordsList[i]))
                {
                    occurancesMapConcurrent.TryAdd(wordsList[i], numberOfOccurrences);
                }
                else
                {
                    occurancesMapConcurrent[wordsList[i]]++;
                }
            });           

            var sortedMap = occurancesMapConcurrent.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return sortedMap;
        }
    }
}
