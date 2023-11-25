using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegMagick.Classes
{
    internal class Utils
    {
        public static string Cmd(string promt)
        {
            StringBuilder outputBuilder = new StringBuilder();

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

                    // Добавить пустую строку
                    outputBuilder.AppendLine();


                    // if (process.ExitCode == 1)
                    // {
                    //     //
                    // }

                    // Проверяем ExitCode
                    // if (process.ExitCode != 0)
                    // {
                    //     // System.Windows.Forms.MessageBox.Show($"Command execution failed with exit code: {process.ExitCode}");
                    //     return $"Command execution failed with exit code: {process.ExitCode}";
                    // }
                }

                return outputBuilder.ToString();
            }
            catch (Exception ex)
            {
                return $"Error executing command: {ex.Message}";
                // return null;
            }
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

        public static string GetFFmpegPath()
        {
            // string ffmpegPath = Path.Combine(
            // 	Path.GetDirectoryName(
            // 		System.Reflection.Assembly.GetExecutingAssembly().Location),
            // 	"ffmpeg.exe");
            // return ffmpegPath;

            string ffmpegFileName = "ffmpeg.exe";
            string systemRootPath = SysFolder.Windows;

            if (!string.IsNullOrEmpty(systemRootPath))
            {
                // string ffmpegPathInSystemRoot = Path.Combine(systemRootPath, "System32", ffmpegFileName);

                if (File.Exists(systemRootPath))
                {
                    return systemRootPath;
                }
            }

            // return null;
            return "null";
        }

        // internal static void RunProcess(string v1, string v2)
        // {
        // 	throw new NotImplementedException();
        // }

        static async Task OpenWebsiteAsync(string url)
        {
            Process websiteProcess = Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            // Дожидаемся завершения процесса открытия сайта
            await Task.Run(() => websiteProcess.WaitForExit());
        }

        public static void OpenWebsite(string url)
        {
            Process websiteProcess = Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            // Дожидаемся завершения процесса открытия сайта
            Task.Run(() => websiteProcess.WaitForExit()).Wait();
        }
    }
}
