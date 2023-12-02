using System.Reflection;
using System.Text.RegularExpressions;

namespace FFmpegMagick.Classes
{
    internal partial class Updater
    {
        [GeneratedRegex(@"<AssemblyVersion>(\d+\.\d+\.\d+\.\d+)</AssemblyVersion>")]
        private static partial Regex RegexVer();
        static readonly string baseDomain = "Stive99/FFmpegMagick";
        static readonly string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        static readonly string exename = AppDomain.CurrentDomain.FriendlyName;
        static readonly string exepath = Assembly.GetEntryAssembly().Location;
        static readonly string fileUpdate = "FFmpegMagickUpdate.exe";
        static readonly string fileOld = $"{exename}.exe.old";
        private static readonly HttpClient client = new();
        public static string UrlVersion = $"https://raw.githubusercontent.com/{baseDomain}/main/FFmpegMagick/FFmpegMagick.csproj";
        private static readonly string VersionUpdateUrl = client.GetStringAsync(UrlVersion).Result;
        public static string VersionUpdate = RegexVer().Match(VersionUpdateUrl).Groups[1].Value;
        public static string UrlDownload = $"https://github.com/{baseDomain}/releases/download/{VersionUpdate}/FFmpegMagickUpdate.exe";

        /// <summary>
        /// Скачивание обновлений
        /// </summary>
        public static async void DownloadUpdate()
        {
            // Удаление старых файлов, если они существуют
            string[] filesToDelete = [fileUpdate, fileOld];
            foreach (string file in filesToDelete)
            {
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }

            // Скачивание новой версии
            await Download.DownloadFile(UrlDownload, fileUpdate);

            // Переименовать текущий файл
            try
            {
                File.Move($"{exename}.exe", fileOld);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка переименование файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Utils.Cmd($"taskkill /f /im \"{exename}.exe\" && timeout 1 && \"{fileUpdate}\" && del \"{fileOld}\" && \"{exename}.exe\"");
        }

        /// <summary>
        /// Проверка обновлений
        /// </summary>
        public static void CheckUpdate()
        {
            if (Utils.OK())
            {
                Version currentVersion = new(version);
                Version updateVersion = new(VersionUpdate);

                if (currentVersion == updateVersion)
                {
                    // if (Tag != null)
                    // {
                    // DownloadUpdate();
                    // }
                    // else
                    // {
                    // MessageBox.Show("У вас установлена последняя версия программы!", "Обновления не найдены", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // }
                }
                else if (currentVersion < updateVersion)
                {
                    if (MessageBox.Show($"Текущая версия программы: {version}\nНовое обновление доступно: {VersionUpdate}\n\nТребуется обновление.\nОбновить программу до актуальной версии?", "Найдено новое обновление!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DownloadUpdate();
                        return;
                    }
                }
                else if (currentVersion > updateVersion)
                {
                    // MessageBox.Show("У вас установлена более новая версия программы!", "Обновления не найдены", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
