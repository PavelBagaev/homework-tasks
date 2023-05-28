using DataProcessing;
using System.Net.Http.Json;
using System;
using System.Text;
using System.Text.Json;

namespace DataIO
{
    internal class Program
    {
        static void Main(string[] args) //путь к текстовому файлу следует передать в аргументе командной строки
        {
            try
            {
                string line;
                Dictionary<string, string> sortedMap = new Dictionary<string, string>();

                List<string> book = new List<string>();

                StreamReader streamReader = new StreamReader(args[0]);
                while ((line = streamReader.ReadLine()) != null)
                {
                    book.Add(line);
                }
                streamReader.Close();

                HttpClient client = new HttpClient();
                var uri = "https://localhost:7048/TextProcessor";
                string jsonBook = JsonSerializer.Serialize(book);
                var content = new StringContent(jsonBook, Encoding.UTF8, "application/json");
                var response = client.PostAsync(uri, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result.Split(",");
                    sortedMap = responseContent.Select(item => item.Split(':')).ToDictionary(s => s[0], s => s[1]);                  
                }

                StreamWriter streamWriter = new StreamWriter("result.txt");

                foreach (var entry in sortedMap)
                {
                    streamWriter.WriteLine(String.Format("{0,-30}{1}", entry.Key, entry.Value));
                }
                streamWriter.Close();

            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}