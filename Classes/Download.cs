namespace FFmpegMagick.Classes
{
    internal class Download
    {
        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="url">Ссылка на файл</param>
        /// <param name="path">Путь для сохранения файла</param>
        /// <param name="maxRetries">Максимальное количество попыток скачивания</param>
        /// <param name="retryDelayMilliseconds">Задержка перед повторной попыткой скачивания</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public static async Task DownloadFile(string url, string path, ProgressBar progressBar = null, Button cancelButton = null, int maxRetries = 3, int retryDelayMilliseconds = 1000, CancellationToken cancellationToken = default)
        {
            using (HttpClient httpClient = new())
            {
                for (int retry = 0; retry < maxRetries; retry++)
                {
                    try
                    {
                        using (HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                        {
                            response.EnsureSuccessStatusCode();

                            long? totalBytes = response.Content.Headers.ContentLength;
                            using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                            {
                                using (FileStream fileStream = File.Create(path))
                                {
                                    await CopyToWithProgressAsync(contentStream, fileStream, totalBytes, cancellationToken, progressBar, cancelButton);
                                    return; // Выход из цикла при успешном скачивании
                                }
                            }
                        }
                    }
                    catch (HttpRequestException) when (retry < maxRetries - 1)
                    {
                        // Ошибка HTTP запроса, подождем перед повторной попыткой
                        await Task.Delay(retryDelayMilliseconds, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        // Логирование ошибки
                        MessageBox.Show($"Ошибка скачивания файла: {ex.Message}", "Ошибка скачивания", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break; // Выход из цикла при критической ошибке
                    }
                }
            }
        }

        private static async Task CopyToWithProgressAsync(Stream source, Stream destination, long? totalBytes, CancellationToken cancellationToken, ProgressBar progressBar = null, Button cancelButton = null)
        {
            const int bufferSize = 81920; // 80 KB

            byte[] buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            int previousProgressPercentage = 0;

            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                totalBytesRead += bytesRead;

                int progressPercentage = totalBytes.HasValue
                    ? (int)(totalBytesRead * 100 / totalBytes.Value)
                    : 0;

                if (progressBar != null && progressPercentage != previousProgressPercentage)
                {
                    progressBar.Value = progressPercentage;
                    Application.DoEvents(); // Обновление UI
                    previousProgressPercentage = progressPercentage;
                }

                if (cancellationToken.IsCancellationRequested && cancelButton != null)
                {
                    cancelButton.Enabled = false; // Отключаем кнопку отмены, чтобы избежать дополнительных попыток
                    throw new OperationCanceledException("Скачивание отменено.", cancellationToken);
                }
            }

            // progressBar?.Value = 100; // Устанавливаем полный прогресс, поскольку операция завершена
            UpdateProgressBar(progressBar, 100);
        }

        private static void UpdateProgressBar(ProgressBar progressBar, int progressPercentage)
        {
            if (progressBar != null)
            {
                progressBar.Value = progressPercentage;
                Application.DoEvents(); // Обновление UI
            }
        }
    }
}
