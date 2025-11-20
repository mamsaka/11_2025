namespace SimplerForm
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.textEnter = new System.Windows.Forms.TextBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.viewList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(318, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 30);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textEnter
            // 
            this.textEnter.Location = new System.Drawing.Point(12, 12);
            this.textEnter.Name = "textEnter";
            this.textEnter.Size = new System.Drawing.Size(300, 22);
            this.textEnter.TabIndex = 1;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(318, 408);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 30);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // viewList
            // 
            this.viewList.HideSelection = false;
            this.viewList.Location = new System.Drawing.Point(424, 12);
            this.viewList.Name = "viewList";
            this.viewList.Size = new System.Drawing.Size(364, 426);
            this.viewList.TabIndex = 3;
            this.viewList.UseCompatibleStateImageBehavior = false;
            this.viewList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.viewList_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.viewList);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textEnter);
            this.Controls.Add(this.buttonSave);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textEnter;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ListView viewList;
    }
}

