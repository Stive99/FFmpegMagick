using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFmpegMagick.Classes
{
    internal class Download
    {
        static readonly string baseDomain = "Stive99/FFmpegMagick";
        static readonly string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        static readonly string exename = AppDomain.CurrentDomain.FriendlyName;
        static readonly string exepath = Assembly.GetEntryAssembly().Location;
        private static readonly WebClient wc = new WebClient();
        public static string UrlVersion = $"https://raw.githubusercontent.com/{baseDomain}/main/FFmpegMagick/FFmpegMagick.csproj";
        private static readonly string VersionUpdateUrl = wc.DownloadString(UrlVersion);
        public static string VersionUpdate = Regex.Match(VersionUpdateUrl, @"\[assembly\: AssemblyVersion\(""(\d+\.\d+\.\d+\.\d+)""\)\]").Groups[1].Value;
        public static string UrlDownload = $"https://github.com/{baseDomain}/releases/download/{VersionUpdate}/FFmpegMagick.exe";

        static void DownloadFile(string url, string path)
        {
            using (WebClient wc = new WebClient())
            {
                bool success = false;
                int retriesLeft = 3;
                while (!success && retriesLeft > 0)
                {
                    try
                    {
                        wc.DownloadFile(new Uri(url), path);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        retriesLeft--;
                        if (retriesLeft == 0)
                        {
                            MessageBox.Show($"Ошибка скачивания файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверка обновлений
        /// </summary>
        public static void CheckUpdate()
        {
            if (Utils.OK())
            {
                // if (Convert.ToDouble(version, CultureInfo.InvariantCulture) == Convert.ToDouble(VersionUpdate, CultureInfo.InvariantCulture))
                if (version == VersionUpdate)
                {
                    // MessageBox.Show("У вас установлена последняя версия программы!", "Обновления не найдены", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Текущая версия программы " + version + "\nНовое обновление доступно " + VersionUpdate + "\n\nТребуется обновление.\nОбновить программу до актуальной версии?", "Найдено новое обновление!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DownloadUpdate();
                    }
                }
            }
        }

        /// <summary>
        /// Скачивание обновлений
        /// </summary>
        public static void DownloadUpdate()
        {
            string currentFilePath = Path.GetFileName(Application.ExecutablePath);
            string oldFilePath = "FFmpegMagickOldVersion.exe";
            string newFilePath = Path.Combine(Environment.CurrentDirectory, "FFmpegMagickNewVersion.exe");
            string hashFilePath = Path.Combine(Environment.CurrentDirectory, "FFmpegMagickNewVersion.hash");

            // Удаление старого файла, если существует
            if (File.Exists(oldFilePath))
            {
                try
                {
                    File.Delete(oldFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления старого файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Скачивание новой версии
            using (WebClient wc = new WebClient())
            {
                bool success = false;
                int retriesLeft = 3;
                while (!success && retriesLeft > 0)
                {
                    try
                    {
                        wc.DownloadFile(new Uri(UrlDownload), newFilePath);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        retriesLeft--;
                        if (retriesLeft == 0)
                        {
                            MessageBox.Show("Ошибка скачивания файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            // Проверка целостности файла
            if (File.Exists(hashFilePath))
            {
                string expectedHash = File.ReadAllText(hashFilePath);
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(newFilePath))
                    {
                        string actualHash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                        if (expectedHash != actualHash)
                        {
                            MessageBox.Show("Ошибка проверки целостности файла!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            // Перемещение текущего файла
            try
            {
                File.Move(currentFilePath, oldFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка перемещения файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Замена старой версии новой
            try
            {
                File.Move(newFilePath, currentFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка замены старой версии новой: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Запуск новой версии
            try
            {
                Process.Start(currentFilePath);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запуска файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async Task DownloadAsync(string url, string path)
        {
            using (WebClient wc = new WebClient())
            {
                bool success = false;
                int retriesLeft = 3;

                while (!success && retriesLeft > 0)
                {
                    try
                    {
                        await wc.DownloadFileTaskAsync(new Uri(url), path);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        retriesLeft--;
                        if (retriesLeft == 0)
                        {
                            MessageBox.Show($"Ошибка скачивания файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // Добавьте задержку перед повторной попыткой
                            await Task.Delay(1000);
                        }
                    }
                }
            }
        }

        public static void DownloadFFmpeg()
        {
            // string ffmpegPath = GetFFmpegPath();
            // if (!File.Exists(ffmpegPath))
            // {
            DownloadAsync("https://www.gyan.dev/ffmpeg/builds/ffmpeg-git-full.7z", $"{SysFolder.Temp}\\ffmpeg-git-full.7z");
            // ZipFile.ExtractToDirectory("ffmpeg.zip", ".");
            // File.Delete("ffmpeg.zip");
            // Directory.Move("ffmpeg-4.3.1-win64-static", "ffmpeg");
            // }
        }

        public static void DownloadMagick()
        {
            // string magickPath = GetMagickPath();
            // if (!File.Exists(magickPath))
            // {
            DownloadAsync("https://www.imagemagick.org/download/binaries/ImageMagick-7.0.10-60-Q16-HDRI-x64-dll.exe", $"{SysFolder.Temp}\\ImageMagick-7.0.10-60-Q16-HDRI-x64-dll.exe");
            // ZipFile.ExtractToDirectory("ffmpeg.zip", ".");
            // File.Delete("ffmpeg.zip");
            // Directory.Move("ffmpeg-4.3.1-win64-static", "ffmpeg");
            // }
        }

        public static void DownloadPandoc()
        {
            // string pandocPath = GetPandocPath();
            // if (!File.Exists(pandocPath))
            // {
            // DownloadAsync("");
        }
    }
}
