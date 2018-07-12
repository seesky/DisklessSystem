namespace NoDiskSystem
{
    partial class SystemSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.名称前缀textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.加入方式comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.使用IPcheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.登录密码textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.超时时间numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.确定button = new System.Windows.Forms.Button();
            this.取消button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.默认目录textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.超时时间numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(331, 123);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.默认目录textBox);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.名称前缀textBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.加入方式comboBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(323, 97);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "加入方式";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // 名称前缀textBox
            // 
            this.名称前缀textBox.Location = new System.Drawing.Point(119, 29);
            this.名称前缀textBox.Name = "名称前缀textBox";
            this.名称前缀textBox.Size = new System.Drawing.Size(177, 21);
            this.名称前缀textBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称前缀：";
            // 
            // 加入方式comboBox
            // 
            this.加入方式comboBox.FormattingEnabled = true;
            this.加入方式comboBox.Location = new System.Drawing.Point(119, 6);
            this.加入方式comboBox.Name = "加入方式comboBox";
            this.加入方式comboBox.Size = new System.Drawing.Size(177, 20);
            this.加入方式comboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "加入方式：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.使用IPcheckedListBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(323, 97);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "IP设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // 使用IPcheckedListBox
            // 
            this.使用IPcheckedListBox.FormattingEnabled = true;
            this.使用IPcheckedListBox.Location = new System.Drawing.Point(8, 25);
            this.使用IPcheckedListBox.Name = "使用IPcheckedListBox";
            this.使用IPcheckedListBox.Size = new System.Drawing.Size(308, 68);
            this.使用IPcheckedListBox.TabIndex = 1;
            this.使用IPcheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.使用IPcheckedListBox_ItemCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "使用IP：";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.登录密码textBox);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.超时时间numericUpDown);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(323, 97);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其他";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(268, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "设置";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // 登录密码textBox
            // 
            this.登录密码textBox.Location = new System.Drawing.Point(120, 42);
            this.登录密码textBox.Name = "登录密码textBox";
            this.登录密码textBox.PasswordChar = '*';
            this.登录密码textBox.Size = new System.Drawing.Size(142, 21);
            this.登录密码textBox.TabIndex = 3;
            this.登录密码textBox.Enter += new System.EventHandler(this.登录密码textBox_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "登录密码：";
            // 
            // 超时时间numericUpDown
            // 
            this.超时时间numericUpDown.Location = new System.Drawing.Point(120, 12);
            this.超时时间numericUpDown.Name = "超时时间numericUpDown";
            this.超时时间numericUpDown.Size = new System.Drawing.Size(142, 21);
            this.超时时间numericUpDown.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "启动菜单超时时间：";
            // 
            // 确定button
            // 
            this.确定button.Location = new System.Drawing.Point(90, 132);
            this.确定button.Name = "确定button";
            this.确定button.Size = new System.Drawing.Size(75, 23);
            this.确定button.TabIndex = 1;
            this.确定button.Text = "确定";
            this.确定button.UseVisualStyleBackColor = true;
            this.确定button.Click += new System.EventHandler(this.确定button_Click);
            // 
            // 取消button
            // 
            this.取消button.Location = new System.Drawing.Point(171, 132);
            this.取消button.Name = "取消button";
            this.取消button.Size = new System.Drawing.Size(75, 23);
            this.取消button.TabIndex = 2;
            this.取消button.Text = "取消";
            this.取消button.UseVisualStyleBackColor = true;
            this.取消button.Click += new System.EventHandler(this.取消button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "工作站默认目录：";
            // 
            // 默认目录textBox
            // 
            this.默认目录textBox.Location = new System.Drawing.Point(119, 55);
            this.默认目录textBox.Name = "默认目录textBox";
            this.默认目录textBox.Size = new System.Drawing.Size(138, 21);
            this.默认目录textBox.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.取消button);
            this.Controls.Add(this.确定button);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "SystemSetting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选项设置";
            this.Load += new System.EventHandler(this.SystemSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.超时时间numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button 确定button;
        private System.Windows.Forms.Button 取消button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox 加入方式comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox 名称前缀textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox 使用IPcheckedListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown 超时时间numericUpDown;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox 登录密码textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox 默认目录textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
    }
}