﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataProcessing
{
    public class DataProcessor
    {
        private Dictionary<string, int> ProcessData (List<string> book)
        {
            string tmp;

            Regex regex = new Regex("[^\\p{L}'-]+");
            List<string> wordsList = new List<string>();
            Dictionary<string, int> occurrencesMap = new Dictionary<string, int>();
            int numberOfOccurrences = 1;

            for (int i = 0; i < book.Count; i++)
            {
                tmp = book[i];
                List<string> words = regex.Split(tmp).ToList<string>();
                words.RemoveAll(word => word.Equals("") || word.Equals("--") || words.Equals("-"));
                wordsList.AddRange(words.ConvertAll(word => word.ToLower()));
            }

            for (int i = 0; i < wordsList.Count; i++)
            {

                if (!occurrencesMap.ContainsKey(wordsList[i]))
                {
                    occurrencesMap.Add(wordsList[i], numberOfOccurrences);
                }
                else
                {
                    occurrencesMap[wordsList[i]]++;
                }
            }

            var sortedMap = occurrencesMap.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return sortedMap;
        }
    }
}