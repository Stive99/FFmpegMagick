using System.Diagnostics;
using FFmpegMagick.Classes;
using FFmpegMagick.UserControls;

namespace FFmpegMagick
{
    public partial class Form1 : Form
    {
        private UC_Images uc_images;

        public Form1()
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            InitializeComponent();
            label1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Проверка обновлений
            Download.CheckUpdate();

            // string ffmpegPath = Utils.GetFFmpegPath();
            // if (File.Exists(ffmpegPath))
            // {
            // 	label1.Text = ffmpegPath;
            // }
            // else
            // {
            // 	label1.Text = "ffmpeg.exe not found";
            // }

            // label1.Text = Utils.Cmd("ffmpeg -version");
            // label1.Text += Utils.Cmd("echo.");
            // label1.Text += Utils.Cmd("magick -version");

            // label1.Text = Utils.Cmd("ffmpeg -version & echo. & magick -version");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Удаление старого файла, если существует
            string oldFilePath = "FFmpegMagickOldVersion.exe";
            if (File.Exists(oldFilePath))
            {
                try
                {
                    File.Delete(oldFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления старого файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // if (e.KeyCode == Keys.F1)
            // {
            //     string help = "F1 - help" +
            //         "\nF7 - debug";

            //     MessageBox.Show(help);
            // }
            // if (e.KeyCode == Keys.F7)
            // {
            //     label1.Visible = !label1.Visible;
            // }
            // if (e.KeyCode == Keys.F8)
            // {
            //     // MessageBox.Show($"{Utils.GetSystemRootPath()}\\ffmpeg.exe");
            //     Download.DownloadFFmpeg();
            //     // Utils.ExtractArchive($"{Utils.GetSystemRootPath()}\\ffmpeg-git-full.7z");
            //     // Download.File("https://www.gyan.dev/ffmpeg/builds/ffmpeg-git-full.7z", $"{Utils.GetSystemRootPath()}\\ffmpeg-git-full.7z");
            // }
        }

        private void UC_Images_ClearListBoxRequested(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelUserControl.Controls.Clear();
            panelUserControl.Controls.Add(userControl);
            panelUserControl.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Process.Start("regidit /s F:\\Download\\ffmpeg.reg");
            // Utils.Cmd("regidit /s @F:\\Download\\ffmpeg.reg");
            Regedit.StartReg();
            // MessageBox.Show(Utils.Cmd("regidit /s \"F:\\Download\\ffmpeg.reg\""));
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                labelDragAndDrop.Text = "Отпустите мышь";
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            labelDragAndDrop.Text = "Перетащите файлы сюда";
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            labelDragAndDrop.Text = "Перетащите файлы сюда";

            List<string> paths = new List<string>();

            foreach (string obj in (string[])e.Data.GetData(DataFormats.FileDrop))
                if (Directory.Exists(obj))
                {
                    paths.AddRange(Directory.GetFiles(obj, "*.*", SearchOption.AllDirectories));
                }
                else
                {
                    // Проверка расширения файла
                    string extension = Path.GetExtension(obj).ToLower();
                    if (Utils.IsAllowedExtension(extension))
                    {
                        paths.Add(obj);
                    }
                }

            foreach (string path in paths)
            {
                string fileName = Path.GetFileName(path);
                if (Utils.IsAllowedExtension(Path.GetExtension(fileName).ToLower()))
                {
                    if (!listBox1.Items.Contains(path))
                    {
                    	listBox1.Items.Add(path);
                    }
                    // if (!listBox1.Items.Contains(fileName))
                    // {
                    //     listBox1.Items.Add(fileName);
                    // }
                }
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            // OpenFileDialog openFileDialog = new OpenFileDialog();
            // openFileDialog.Multiselect = true;
            // openFileDialog.Filter = "All files (*.*)|*.*";
            // openFileDialog.InitialDirectory = SysFolder.Desktop;

            // if (openFileDialog.ShowDialog() == DialogResult.OK)
            // {
            // 	foreach (string file in openFileDialog.FileNames)
            // 	{
            // 		if (!listBox1.Items.Contains(file))
            // 		{
            // 			listBox1.Items.Add(file);
            // 		}
            // 	}
            // 	// label2.Text = openFileDialog.FileName;
            // 	// listBox1.Items.Add(openFileDialog.FileName);
            //	// label2.Text = string.Join("\r\n", paths);
            //	// listBox1.Items.AddRange(paths.ToArray());
            // }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            pen.DashPattern = new float[] { 2, 2 };
            e.Graphics.DrawRectangle(pen, 1, 1, panel1.Width - 2, panel1.Height - 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Если listBox пустой
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Список файлов пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                UC_Images uc_Images = new UC_Images();
                addUserControl(uc_Images);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
