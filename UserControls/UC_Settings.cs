using FFmpegMagick.Classes;

namespace FFmpegMagick.UserControls
{
    public partial class UC_Settings : UserControl
    {
        public UC_Settings()
        {
            InitializeComponent();
        }

        private void UC_Settings_Load(object sender, EventArgs e)
        {
            progressBarDownload.Visible = false;
            labelprogressDownload.Visible = false;
        }

        private async void buttonDownloadCli_Click(object sender, EventArgs e)
        {
            buttonDownloadCli.Enabled = false;
            progressBarDownload.Visible = true;
            labelprogressDownload.Visible = true;

            // Список URL-адресов файлов для скачивания
            List<string> urls =
            [
                "https://imagemagick.org/archive/binaries/ImageMagick-7.1.1-21-portable-Q16-x64.zip"
                // "https://www.gyan.dev/ffmpeg/builds/ffmpeg-git-full.7z"
            ];

            // Название файлов
            List<string> filenames =
            [
                "ImageMagick.zip"
                // "ffmpeg.7z"
            ];

            // Скачивание
            foreach (string url in urls)
            {
                labelprogressDownload.Text = $"Скачивание {filenames[urls.IndexOf(url)]}...";
                await Download.DownloadFile(url, $"{Environment.CurrentDirectory}\\{filenames[urls.IndexOf(url)]}", progressBarDownload, buttonDownloadCli);
                labelprogressDownload.Text = $"Скачивание {filenames[urls.IndexOf(url)]} завершено";
            }

            // Распаковка
            foreach (string filename in filenames)
            {
                Archive extractor = new();
                extractor.ProgressUpdated += (current, total) =>
                {
                    double percentage = (double)current / total * 100;
                    labelprogressDownload.Text = $"Распаковка {filename}... {Math.Round(percentage, 2)}%";
                };

                await extractor.Extract(filename, filename, SysFolder.Windows);
                labelprogressDownload.Text = $"Распаковка {filename} завершена";
            }
        }
    }
}
