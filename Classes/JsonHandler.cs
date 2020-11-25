using System;
using DotNetCoreBetterConsoleApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNetCoreBetterConsoleApp.Classes
{
    public class JsonHandler : IJsonHandler
    {
        private readonly ILogger<JsonHandler> _log;
        private readonly IConfiguration _config;

        public JsonHandler(ILogger<JsonHandler> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void ProcessJsonContent(string jsonContent)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(jsonContent);
            var properties = obj.Properties();
            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name);
                Console.WriteLine(prop.Value);
            }
        }
    }
}
