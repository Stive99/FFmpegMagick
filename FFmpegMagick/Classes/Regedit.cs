using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegMagick.Classes
{
    internal class Regedit
    {
        public static void StartReg()
        {
            // string regFilePath = @"F:\Download\ffmpeg.reg";
            string regFilePath = "ffmpegmagick.reg";

            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "regedit.exe",
                    Arguments = $@"/s ""{regFilePath}""",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = processStartInfo })
                {
                    process.Start();
                    process.WaitForExit();

                    int exitCode = process.ExitCode;
                    if (exitCode == 0)
                    {
                        // MessageBox.Show("Реестр успешно обновлен.");
                    }
                    else
                    {
                        // MessageBox.Show($"Ошибка при обновлении реестра. Код ошибки: {exitCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Получение уровня UAC
        /// 0 - Отключен - не уведомлять (Never notify).
        /// 1 - Включен - уведомлять, когда приложение пытается внести изменения (Notify me only when apps try to make changes to my computer).
        /// 2 - Включен - уведомлять всегда (Always notify).
        /// </summary>
        /// <returns></returns>
        public static int GetUACLevel()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("EnableLUA");

                        if (value != null)
                        {
                            int enableLUA = Convert.ToInt32(value);
                            return enableLUA;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при попытке прочитать реестр: {ex.Message}");
            }

            return -1; // В случае ошибки или если значение не удалось получить
        }
    }
}
