using System;
using System.Collections.Generic;
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

            IDictionary<string, JToken> JsonData = JObject.Parse(jsonContent);

            foreach (KeyValuePair<string, JToken> element in JsonData)
            {
                if (element.Value is JArray)
                {
                    // Process JArray
                }
                else if (element.Value is JObject)
                {
                    currentDirectory = element.Key;

                    string newDirectory = baseDirectory + "/" + currentDirectory;

                    Directory.CreateDirectory(newDirectory);

                    RecursiveCreateNestedDirectories(element.Value as JObject, newDirectory);
                }

                // /home/leonardo/projects/DotNetCoreBetterConsoleApp/tests/test.json
                // C:\ASV\DotNetCoreDirectoryStructureBasedOnJsonFile\tests\test.json
            }
        }

        private void RecursiveCreateNestedDirectories(JObject jObject, string baseDirectory)
        {
            string currentDirectory = "";

            foreach (var child in jObject)
            {
                currentDirectory = child.Key;

                string newDirectory = baseDirectory + "/" + currentDirectory;

                Directory.CreateDirectory(newDirectory);

                RecursiveCreateNestedDirectories(child.Value as JObject, newDirectory);
            }
        }
    }
}
