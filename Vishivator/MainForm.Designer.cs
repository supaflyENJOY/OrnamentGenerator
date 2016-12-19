namespace Vishivator {
    partial class MainForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.inputWord = new System.Windows.Forms.TextBox();
            this.CreateOrnament = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // inputWord
            // 
            this.inputWord.Location = new System.Drawing.Point(12, 10);
            this.inputWord.Name = "inputWord";
            this.inputWord.Size = new System.Drawing.Size(497, 20);
            this.inputWord.TabIndex = 0;
            // 
            // CreateOrnament
            // 
            this.CreateOrnament.Location = new System.Drawing.Point(515, 10);
            this.CreateOrnament.Name = "CreateOrnament";
            this.CreateOrnament.Size = new System.Drawing.Size(128, 20);
            this.CreateOrnament.TabIndex = 1;
            this.CreateOrnament.Text = "Generate";
            this.CreateOrnament.UseVisualStyleBackColor = true;
            this.CreateOrnament.Click += new System.EventHandler(this.CreateOrnament_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 631);
            this.panel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 41);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CreateOrnament);
            this.Controls.Add(this.inputWord);
            this.Name = "MainForm";
            this.Text = "Ukrainian Ornament Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputWord;
        private System.Windows.Forms.Button CreateOrnament;
        private System.Windows.Forms.Panel panel1;
    }
}

