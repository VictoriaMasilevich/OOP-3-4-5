namespace PaintF
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.DrawAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DrawAllButton
            // 
            this.DrawAllButton.BackColor = System.Drawing.Color.MediumPurple;
            this.DrawAllButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DrawAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DrawAllButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DrawAllButton.Location = new System.Drawing.Point(77, 34);
            this.DrawAllButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DrawAllButton.Name = "DrawAllButton";
            this.DrawAllButton.Size = new System.Drawing.Size(210, 66);
            this.DrawAllButton.TabIndex = 0;
            this.DrawAllButton.Text = "Serialize";
            this.DrawAllButton.UseVisualStyleBackColor = false;
            this.DrawAllButton.Click += new System.EventHandler(this.Serialize_Click);
            // 
            // Paint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(683, 388);
            this.Controls.Add(this.DrawAllButton);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimizeBox = false;
            this.Name = "Paint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paint";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button DrawAllButton;
    }
}

