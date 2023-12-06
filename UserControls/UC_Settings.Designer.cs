namespace FFmpegMagick.UserControls
{
    partial class UC_Settings
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            buttonDownloadCli = new Button();
            progressBarDownload = new ProgressBar();
            labelprogressDownload = new Label();
            SuspendLayout();
            // 
            // buttonDownloadCli
            // 
            buttonDownloadCli.Location = new Point(3, 3);
            buttonDownloadCli.Name = "buttonDownloadCli";
            buttonDownloadCli.Size = new Size(75, 23);
            buttonDownloadCli.TabIndex = 0;
            buttonDownloadCli.Text = "downloand";
            buttonDownloadCli.UseVisualStyleBackColor = true;
            buttonDownloadCli.Click += buttonDownloadCli_Click;
            // 
            // progressBarDownload
            // 
            progressBarDownload.Location = new Point(122, 190);
            progressBarDownload.Name = "progressBarDownload";
            progressBarDownload.Size = new Size(533, 23);
            progressBarDownload.TabIndex = 2;
            // 
            // labelprogressDownload
            // 
            labelprogressDownload.AutoSize = true;
            labelprogressDownload.BackColor = SystemColors.Control;
            labelprogressDownload.Location = new Point(307, 194);
            labelprogressDownload.Margin = new Padding(3);
            labelprogressDownload.Name = "labelprogressDownload";
            labelprogressDownload.Size = new Size(35, 13);
            labelprogressDownload.TabIndex = 3;
            labelprogressDownload.Text = "label1";
            // 
            // UC_Settings
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelprogressDownload);
            Controls.Add(progressBarDownload);
            Controls.Add(buttonDownloadCli);
            Name = "UC_Settings";
            Size = new Size(776, 220);
            Load += UC_Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonDownloadCli;
        private ProgressBar progressBarDownload;
        private Label labelprogressDownload;
    }
}
