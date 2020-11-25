using System;
using System.IO;
using System.Threading.Tasks;
using DotNetCoreBetterConsoleApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCoreBetterConsoleApp.Classes
{
    public class FilesHandler : IFilesHandler
    {
        private readonly ILogger<JsonHandler> _log;
        private readonly IConfiguration _config;

        public FilesHandler(ILogger<JsonHandler> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public string GetFilePath(string message = "")
        {
            if (Util.IsNullOrBlank(message))
            {
                message = "Enter your JSON file path, directories will be created in the same folder where the JSON file is: ";
            }

            string filePath;

            do
            {
                Console.WriteLine(message);

                filePath = Console.ReadLine();
            } while (Util.IsNullOrBlank(filePath));

            return filePath;
        }

        public async Task<string> GetFileContentByPath(string filePath)
        {
            return await File.ReadAllTextAsync($@"{filePath}");
        }
    }
}
