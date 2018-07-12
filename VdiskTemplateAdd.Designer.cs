namespace NoDiskSystem
{
    partial class VdiskTemplateAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.磁盘名称textBox = new System.Windows.Forms.TextBox();
            this.磁盘容量textBox = new System.Windows.Forms.TextBox();
            this.映像文件textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.映像文件button = new System.Windows.Forms.Button();
            this.稀疏文件checkBox = new System.Windows.Forms.CheckBox();
            this.确定button = new System.Windows.Forms.Button();
            this.取消button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "磁盘名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "磁盘容量：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "映像文件目录：";
            // 
            // 磁盘名称textBox
            // 
            this.磁盘名称textBox.Location = new System.Drawing.Point(102, 9);
            this.磁盘名称textBox.Name = "磁盘名称textBox";
            this.磁盘名称textBox.Size = new System.Drawing.Size(100, 21);
            this.磁盘名称textBox.TabIndex = 3;
            // 
            // 磁盘容量textBox
            // 
            this.磁盘容量textBox.Location = new System.Drawing.Point(102, 41);
            this.磁盘容量textBox.Name = "磁盘容量textBox";
            this.磁盘容量textBox.Size = new System.Drawing.Size(100, 21);
            this.磁盘容量textBox.TabIndex = 4;
            // 
            // 映像文件textBox
            // 
            this.映像文件textBox.Location = new System.Drawing.Point(102, 72);
            this.映像文件textBox.Name = "映像文件textBox";
            this.映像文件textBox.Size = new System.Drawing.Size(100, 21);
            this.映像文件textBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "G（1-2048）";
            // 
            // 映像文件button
            // 
            this.映像文件button.Location = new System.Drawing.Point(210, 72);
            this.映像文件button.Name = "映像文件button";
            this.映像文件button.Size = new System.Drawing.Size(75, 23);
            this.映像文件button.TabIndex = 7;
            this.映像文件button.Text = "浏览(&B)";
            this.映像文件button.UseVisualStyleBackColor = true;
            this.映像文件button.Click += new System.EventHandler(this.映像文件button_Click);
            // 
            // 稀疏文件checkBox
            // 
            this.稀疏文件checkBox.AutoSize = true;
            this.稀疏文件checkBox.Checked = true;
            this.稀疏文件checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.稀疏文件checkBox.Enabled = false;
            this.稀疏文件checkBox.Location = new System.Drawing.Point(12, 107);
            this.稀疏文件checkBox.Name = "稀疏文件checkBox";
            this.稀疏文件checkBox.Size = new System.Drawing.Size(72, 16);
            this.稀疏文件checkBox.TabIndex = 8;
            this.稀疏文件checkBox.Text = "稀疏文件";
            this.稀疏文件checkBox.UseVisualStyleBackColor = true;
            // 
            // 确定button
            // 
            this.确定button.Location = new System.Drawing.Point(90, 126);
            this.确定button.Name = "确定button";
            this.确定button.Size = new System.Drawing.Size(75, 23);
            this.确定button.TabIndex = 9;
            this.确定button.Text = "确定";
            this.确定button.UseVisualStyleBackColor = true;
            this.确定button.Click += new System.EventHandler(this.确定button_Click);
            // 
            // 取消button
            // 
            this.取消button.Location = new System.Drawing.Point(171, 126);
            this.取消button.Name = "取消button";
            this.取消button.Size = new System.Drawing.Size(75, 23);
            this.取消button.TabIndex = 10;
            this.取消button.Text = "取消";
            this.取消button.UseVisualStyleBackColor = true;
            this.取消button.Click += new System.EventHandler(this.取消button_Click);
            // 
            // VdiskTemplateAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.取消button);
            this.Controls.Add(this.确定button);
            this.Controls.Add(this.稀疏文件checkBox);
            this.Controls.Add(this.映像文件button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.映像文件textBox);
            this.Controls.Add(this.磁盘容量textBox);
            this.Controls.Add(this.磁盘名称textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "VdiskTemplateAdd";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加磁盘";
            this.Load += new System.EventHandler(this.VdiskTemplateAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox 磁盘名称textBox;
        private System.Windows.Forms.TextBox 磁盘容量textBox;
        private System.Windows.Forms.TextBox 映像文件textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button 映像文件button;
        private System.Windows.Forms.CheckBox 稀疏文件checkBox;
        private System.Windows.Forms.Button 确定button;
        private System.Windows.Forms.Button 取消button;
    }
}