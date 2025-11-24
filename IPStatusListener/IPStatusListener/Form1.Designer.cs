namespace IPStatusListener
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
            this.IPList = new System.Windows.Forms.ListBox();
            this.enterIP = new System.Windows.Forms.TextBox();
            this.saveIP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IPList
            // 
            this.IPList.FormattingEnabled = true;
            this.IPList.ItemHeight = 16;
            this.IPList.Location = new System.Drawing.Point(424, 12);
            this.IPList.Name = "IPList";
            this.IPList.Size = new System.Drawing.Size(364, 420);
            this.IPList.TabIndex = 0;
            this.IPList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IPList_MouseDoubleClick);
            // 
            // enterIP
            // 
            this.enterIP.Location = new System.Drawing.Point(12, 12);
            this.enterIP.Name = "enterIP";
            this.enterIP.Size = new System.Drawing.Size(300, 22);
            this.enterIP.TabIndex = 3;
            // 
            // saveIP
            // 
            this.saveIP.Location = new System.Drawing.Point(318, 12);
            this.saveIP.Name = "saveIP";
            this.saveIP.Size = new System.Drawing.Size(100, 30);
            this.saveIP.TabIndex = 2;
            this.saveIP.Text = "Save";
            this.saveIP.UseVisualStyleBackColor = true;
            this.saveIP.Click += new System.EventHandler(this.saveIP_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.enterIP);
            this.Controls.Add(this.saveIP);
            this.Controls.Add(this.IPList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox IPList;
        private System.Windows.Forms.TextBox enterIP;
        private System.Windows.Forms.Button saveIP;
    }
}

