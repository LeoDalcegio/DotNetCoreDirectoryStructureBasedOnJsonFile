using System.Threading.Tasks;

namespace DotNetCoreBetterConsoleApp.Interfaces
{
    public interface IFilesHandler
    {
        string GetFilePath(string message = "");
        Task<string> GetFileContentByPath(string filePath);
    }
}
