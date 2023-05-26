using DataProcessing;

namespace DataIO
{
    internal class Program
    {
        static void Main(string[] args) //путь к текстовому файлу следует передать в аргументе командной строки
        {
            try
            {
                string line;
                Dictionary<string, int> sortedMap = new Dictionary<string, int>();                             

                List<string> book = new List<string>();

                StreamReader streamReader = new StreamReader(args[0]);
                while ((line = streamReader.ReadLine()) != null)
                {
                    book.Add(line);
                }
                streamReader.Close();

                DataProcessor processor = new DataProcessor();
                sortedMap = processor.ProcessData(book);                               

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