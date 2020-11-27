namespace DotNetCoreBetterConsoleApp.Interfaces
{
    public interface IJsonHandler
    {
        void CreateDirectoriesBasedOnJsonContent(string jsonContent, string jsonPath);
    }
}
