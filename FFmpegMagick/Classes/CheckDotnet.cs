using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegMagick.Classes
{
    internal class CheckDotnet
    {
        // Получение установленной версии .NET
        public static string GetDotNetVersion()
        {
            string commandOutput = Utils.Cmd("dotnet --version");

            if (commandOutput != null)
            {
                return commandOutput.Trim();
            }

            return string.Empty;
        }

        // Проверка версии .NET
        public static bool IsDotNetVersionSufficient(string installedVersion, string requiredVersion)
        {
            if (string.IsNullOrWhiteSpace(installedVersion))
            {
                return false;
            }

            Version installed = Version.Parse(installedVersion);
            Version required = Version.Parse(requiredVersion);

            return installed >= required;
        }
    }
}
