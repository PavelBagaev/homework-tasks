using DataProcessing;
using Microsoft.AspNetCore.Mvc;

namespace TextProcessorWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextProcessorController : ControllerBase
    {
        private readonly ILogger<TextProcessorController> _logger;

        public TextProcessorController(ILogger<TextProcessorController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "ProcessText")]
        public Dictionary<string, int> ProcessText([FromBody] List<string> book)
        {
            DataProcessor dataProcessor = new DataProcessor();
            return dataProcessor.ProcessData(book);
        }
    }
}