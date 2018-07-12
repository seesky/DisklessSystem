namespace NoDiskSystem
{
    partial class ClientGroupAdd
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
            this.组名textBox = new System.Windows.Forms.TextBox();
            this.确定button = new System.Windows.Forms.Button();
            this.取消button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "组名：";
            // 
            // 组名textBox
            // 
            this.组名textBox.Location = new System.Drawing.Point(78, 50);
            this.组名textBox.Name = "组名textBox";
            this.组名textBox.Size = new System.Drawing.Size(175, 21);
            this.组名textBox.TabIndex = 1;
            // 
            // 确定button
            // 
            this.确定button.Location = new System.Drawing.Point(67, 126);
            this.确定button.Name = "确定button";
            this.确定button.Size = new System.Drawing.Size(75, 23);
            this.确定button.TabIndex = 2;
            this.确定button.Text = "确定";
            this.确定button.UseVisualStyleBackColor = true;
            this.确定button.Click += new System.EventHandler(this.确定button_Click);
            // 
            // 取消button
            // 
            this.取消button.Location = new System.Drawing.Point(148, 126);
            this.取消button.Name = "取消button";
            this.取消button.Size = new System.Drawing.Size(75, 23);
            this.取消button.TabIndex = 3;
            this.取消button.Text = "取消";
            this.取消button.UseVisualStyleBackColor = true;
            this.取消button.Click += new System.EventHandler(this.取消button_Click);
            // 
            // ClientGroupAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.取消button);
            this.Controls.Add(this.确定button);
            this.Controls.Add(this.组名textBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "ClientGroupAdd";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加组";
            this.Load += new System.EventHandler(this.ClientGroupAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox 组名textBox;
        private System.Windows.Forms.Button 确定button;
        private System.Windows.Forms.Button 取消button;
    }
}