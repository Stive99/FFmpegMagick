namespace FFmpegMagick.UserControls
{
    partial class UC_Images
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
            button512x = new Button();
            SuspendLayout();
            // 
            // button512x
            // 
            button512x.Location = new Point(3, 3);
            button512x.Name = "button512x";
            button512x.Size = new Size(75, 23);
            button512x.TabIndex = 0;
            button512x.Text = "512x";
            button512x.UseVisualStyleBackColor = true;
            button512x.Click += button512x_Click;
            // 
            // UC_Images
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button512x);
            Name = "UC_Images";
            Size = new Size(776, 220);
            ResumeLayout(false);
        }

        #endregion

        private Button button512x;
    }
}
