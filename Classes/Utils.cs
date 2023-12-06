using System.Diagnostics;
using System.Net;
using System.Text;

namespace FFmpegMagick.Classes
{
    internal class Utils
    {
        public static string CmdString(string promt)
        {
            StringBuilder outputBuilder = new();

            try
            {
                using (var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    // Arguments = $"/c chcp 65001 & {promt}",
                    Arguments = $"/c {promt}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }))
                {
                    process.BeginOutputReadLine();
                    process.OutputDataReceived += (s, a) =>
                    {
                        outputBuilder.AppendLine(a.Data);
                    };

                    process.WaitForExit();

                    // Проверяем ExitCode
                    if (process.ExitCode != 0)
                    {
                        return $"Выполнение команды завершилось неудачей с кодом выхода: {process.ExitCode}";
                    }
                }

                return outputBuilder.ToString();
            }
            catch (Exception ex)
            {
                return $"Ошибка при выполнении команды: {ex.Message}";
                // return null;
            }
        }

        public static void Cmd(string line)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {line}",
                    WindowStyle = ProcessWindowStyle.Hidden
                });
            }
            catch { }
        }

        /// <summary>
        /// Проверка расширения файла
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static bool IsAllowedExtension(string extension)
        {
            // Список разрешенных расширений
            string[] allowedExtensions = [".png", ".jpg", ".gif", ".webp"];
            return allowedExtensions.Contains(extension);
        }

        /// <summary>
        /// Проверка доступности интернета
        /// </summary>
        public static bool OK()
        {
            try
            {
                Dns.GetHostEntry("github.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
