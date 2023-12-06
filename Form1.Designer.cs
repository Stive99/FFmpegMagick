namespace FFmpegMagick
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            labelDragAndDrop = new Label();
            buttonContexMenu = new Button();
            listBox1 = new ListBox();
            panel2 = new Panel();
            buttonSettings = new Button();
            buttonImages = new Button();
            panelUserControl = new Panel();
            buttonCleanListBox = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AllowDrop = true;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(labelDragAndDrop);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(266, 135);
            panel1.TabIndex = 1;
            panel1.Click += panel1_Click;
            panel1.DragDrop += panel1_DragDrop;
            panel1.DragEnter += panel1_DragEnter;
            panel1.DragLeave += panel1_DragLeave;
            panel1.Paint += panel1_Paint;
            // 
            // labelDragAndDrop
            // 
            labelDragAndDrop.Location = new Point(3, 55);
            labelDragAndDrop.Name = "labelDragAndDrop";
            labelDragAndDrop.Size = new Size(258, 23);
            labelDragAndDrop.TabIndex = 0;
            labelDragAndDrop.Text = "Перетащите файлы сюда";
            labelDragAndDrop.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonContexMenu
            // 
            buttonContexMenu.Location = new Point(12, 415);
            buttonContexMenu.Name = "buttonContexMenu";
            buttonContexMenu.Size = new Size(129, 23);
            buttonContexMenu.TabIndex = 2;
            buttonContexMenu.Text = "Добавить в контекст.";
            buttonContexMenu.UseVisualStyleBackColor = true;
            buttonContexMenu.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 13;
            listBox1.Location = new Point(284, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(504, 134);
            listBox1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.AppWorkspace;
            panel2.Controls.Add(buttonSettings);
            panel2.Controls.Add(buttonImages);
            panel2.Location = new Point(12, 153);
            panel2.Name = "panel2";
            panel2.Size = new Size(776, 29);
            panel2.TabIndex = 5;
            // 
            // buttonSettings
            // 
            buttonSettings.Location = new Point(701, 3);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(72, 23);
            buttonSettings.TabIndex = 1;
            buttonSettings.Text = "Настройки";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // buttonImages
            // 
            buttonImages.Location = new Point(3, 4);
            buttonImages.Name = "buttonImages";
            buttonImages.Size = new Size(127, 23);
            buttonImages.TabIndex = 0;
            buttonImages.Text = "Работа с картинками";
            buttonImages.UseVisualStyleBackColor = true;
            buttonImages.Click += button2_Click;
            // 
            // panelUserControl
            // 
            panelUserControl.BackColor = SystemColors.ActiveBorder;
            panelUserControl.Location = new Point(12, 186);
            panelUserControl.Name = "panelUserControl";
            panelUserControl.Size = new Size(776, 220);
            panelUserControl.TabIndex = 6;
            // 
            // buttonCleanListBox
            // 
            buttonCleanListBox.Location = new Point(713, 415);
            buttonCleanListBox.Name = "buttonCleanListBox";
            buttonCleanListBox.Size = new Size(75, 23);
            buttonCleanListBox.TabIndex = 7;
            buttonCleanListBox.Text = "Очистить";
            buttonCleanListBox.UseVisualStyleBackColor = true;
            buttonCleanListBox.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonCleanListBox);
            Controls.Add(panelUserControl);
            Controls.Add(panel2);
            Controls.Add(listBox1);
            Controls.Add(buttonContexMenu);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(816, 489);
            MinimumSize = new Size(816, 489);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Tag = "beta";
            Text = "FFmpegMagick";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Button buttonContexMenu;
        private Label labelDragAndDrop;
        private Panel panel2;
        private Panel panelUserControl;
        private Button buttonImages;
        private Button buttonCleanListBox;
        public ListBox listBox1;
        private Button buttonSettings;
    }
}
