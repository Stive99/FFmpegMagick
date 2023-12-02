using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;

namespace FFmpegMagick.Classes
{
    internal class Archive
    {
        // Делегат для обновления прогресса
        public delegate void ProgressUpdateHandler(int current, int total);

        // Событие для уведомления об обновлении прогресса
        public event ProgressUpdateHandler ProgressUpdated = null!;

        /// <summary>
        /// Асинхронное скачивание и распаковка архива
        /// </summary>
        /// <param name="filename">Имя файла архива</param>
        /// <param name="type">Тип архива</param>
        /// <param name="targetDirectory">Путь, куда нужно распаковать архив</param>
        public async Task Extract(string filename, string type, string targetDirectory)
        {
            string fileExtension = Path.GetExtension(filename);
            type = fileExtension.EndsWith(".7z") ? "7z" : "zip";

            await Task.Run(() =>
            {
                IArchive archive;

                switch (type)
                {
                    case "7z":
                        archive = SevenZipArchive.Open(filename);
                        break;
                    case "zip":
                        archive = ZipArchive.Open(filename);
                        break;
                    default:
                        // Неизвестный формат архива
                        return;
                }

                int totalEntries = archive.Entries.Count();
                int processedEntries = 0;

                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(targetDirectory, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });

                    processedEntries++;
                    OnProgressUpdated(processedEntries, totalEntries);
                }
            });
        }

        // Метод для вызова асинхронного события обновления прогресса
        protected virtual void OnProgressUpdated(int current, int total)
        {
            // Проверяем, есть ли подписчики на событие
            ProgressUpdated?.Invoke(current, total);
        }
    }
}