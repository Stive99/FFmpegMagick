using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFmpegMagick.Classes;

namespace FFmpegMagick.UserControls
{
    public partial class UC_Images : UserControl
    {
        public UC_Images()
        {
            InitializeComponent();
        }

        private void button512x_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            // Если listBox пустой
            if (form1.listBox1.Items.Count == 0)
            {
                MessageBox.Show("Список файлов пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                foreach (string file in form1.listBox1.Items)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string fileExt = Path.GetExtension(file);
                    string fileDir = Path.GetDirectoryName(file);
                    string fileNew = fileDir + "\\" + fileName + "_512x" + fileExt;
                    string command = $"magick convert \"{file}\" -resize 512x \"{fileNew}\"";
                    Utils.Cmd(command);
                }
            }
        }
    }
}
