using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegMagick.Classes
{
    internal class Archive
    {
        public static void ExtractArchive(string archiveFilePath)
        {
            try
            {
                // Замените "7z" на полный путь к исполняемому файлу 7-Zip, если он не в PATH
                string sevenZipPath = "7z";

                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = sevenZipPath,
                        Arguments = $"x \"{archiveFilePath}\" -o\"{Environment.CurrentDirectory}\" -y",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    process.StartInfo = startInfo;
                    process.Start();

                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show($"Архив успешно распакован в текущую папку.");
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка распаковки архива. Код завершения: {process.ExitCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении операции: {ex.Message}");
            }
        }
    }
}
