using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;

namespace FFmpegMagick.Classes
{
    internal class Archive
    {
        /// <summary>
        /// Скачивание и распаковка архива
        /// </summary>
        /// <param name="url">URL для скачивания архива</param>
        /// <param name="filename">Имя файла архива</param>
        /// <param name="targetDirectory">Путь, куда нужно распаковать архив</param>
        public static void DownloadAndExtractArchive(string url, string filename, string targetDirectory)
        {
            // Полный путь к архиву
            string archivePath = Path.Combine(targetDirectory, filename);

            // Скачиваем архив
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, archivePath);
            }

            // Распаковываем архив в текущую папку с заменой файлов
            ZipFile.ExtractToDirectory(archivePath, targetDirectory, true);

            // Опционально: Удаляем загруженный архив, если он больше не нужен
            File.Delete(archivePath);

            Console.WriteLine("Архив успешно скачан и распакован.");
        }

        public static void ExtractArchive()
        {
            // Имя файла архива RAR
            string archiveFileName = "FFmpegMagick.rar";

            // Путь, куда нужно распаковать архив
            string targetDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Полный путь к архиву RAR
            string archivePath = Path.Combine(targetDirectory, archiveFileName);

            try
            {
                using (var archive = RarArchive.Open(archivePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        try {
                            entry.WriteToDirectory(targetDirectory, new SharpCompress.Common.ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при распаковке файла {entry.Key}: {ex.Message}");
                        }
                    }
                }

                MessageBox.Show("Архив успешно распакован.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при распаковке архива: {ex.Message}");
            }
        }
    }
}