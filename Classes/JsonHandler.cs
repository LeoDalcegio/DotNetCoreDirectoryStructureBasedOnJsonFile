using System;
using System.IO;
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

        public void CreateDirectoriesBasedOnJsonContent(string jsonContent, string jsonPath)
        {
            string currentDirectory = "";

            string baseDirectory = Path.GetDirectoryName(jsonPath);

            JObject obj = JsonConvert.DeserializeObject<JObject>(jsonContent);

            var properties = obj.Properties();

            // /home/leonardo/projects/DotNetCoreBetterConsoleApp/tests/test.json

            foreach (JProperty prop in properties)
            {
                currentDirectory = prop.Name;

                // Check if value is another json, go inside it (currentDirectory = ),
                // create directory with that name

                Directory.CreateDirectory(baseDirectory + "/" + currentDirectory);

                // var childrenTokens = prop.Children();

                // foreach (var child in childrenTokens)
                // {
                //     currentDirectory = currentDirectory + "/" + child.Path;

                //     Directory.CreateDirectory(baseDirectory + "/" + currentDirectory);
                // }
            }
        }
    }
}
