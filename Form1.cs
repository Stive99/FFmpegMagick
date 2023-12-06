using System.Diagnostics;
using System.Reflection;
using FFmpegMagick.Classes;
using FFmpegMagick.UserControls;

namespace FFmpegMagick
{
    public partial class Form1 : Form
    {
        private readonly UC_Images uc_images = new();
        private readonly UC_Settings uc_settings = new();

        public Form1()
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            InitializeComponent();
            ArgsProgram.Args();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Updater.CheckUpdate();
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
            Regedit.Reg();
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

            List<string> paths = [];

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
            if (Regedit.GetUACLevel() == 2)
            {
                OpenFileDialog openFileDialog = new()
                {
                    Multiselect = true,
                    Filter = "All images files (*.*)|*.*",
                    InitialDirectory = SysFolder.Desktop
                };

                // FolderBrowserDialog folderBrowserDialog = new()
                // {
                //     ShowNewFolderButton = false,
                //     RootFolder = Environment.SpecialFolder.Desktop
                // };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in openFileDialog.FileNames)
                    {
                        if (!listBox1.Items.Contains(file))
                        {
                            if (Utils.IsAllowedExtension(Path.GetExtension(file).ToLower()))
                            {
                                listBox1.Items.Add(file);
                            }
                        }
                    }
                }

                // if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                // {
                //     foreach (string file in Directory.GetFiles(folderBrowserDialog.SelectedPath))
                //     {
                //         if (!listBox1.Items.Contains(file))
                //         {
                //             if (Utils.IsAllowedExtension(Path.GetExtension(file).ToLower()))
                //             {
                //                 listBox1.Items.Add(file);
                //             }
                //         }
                //     }
                // }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            pen.DashPattern = new float[] { 2, 2 };
            e.Graphics.DrawRectangle(pen, 1, 1, panel1.Width - 2, panel1.Height - 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addUserControl(uc_images);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            addUserControl(uc_settings);
        }
    }
}
