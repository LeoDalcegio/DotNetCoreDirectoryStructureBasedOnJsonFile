using System;
using System.IO;
using DotNetCoreBetterConsoleApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCoreBetterConsoleApp.Classes
{
    public static class Util
    {
        public static bool IsNullOrBlank(string value)
        {
            if (value == null || value == "")
            {
                return true;
            }

            return false;
        }
    }
}
