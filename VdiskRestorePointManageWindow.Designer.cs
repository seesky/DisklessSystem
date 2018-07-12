namespace NoDiskSystem
{
    partial class VdiskRestorePointManageWindow
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
            this.磁盘comboBox = new System.Windows.Forms.ComboBox();
            this.还原点dataGridView = new System.Windows.Forms.DataGridView();
            this.vdiskResotrePointName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vdiskRestorePointCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VdiskRestorePointDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.新增button = new System.Windows.Forms.Button();
            this.还原button = new System.Windows.Forms.Button();
            this.退出button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.还原点dataGridView)).BeginInit();
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
            // 磁盘comboBox
            // 
            this.磁盘comboBox.FormattingEnabled = true;
            this.磁盘comboBox.Location = new System.Drawing.Point(83, 6);
            this.磁盘comboBox.Name = "磁盘comboBox";
            this.磁盘comboBox.Size = new System.Drawing.Size(121, 20);
            this.磁盘comboBox.TabIndex = 1;
            this.磁盘comboBox.SelectedIndexChanged += new System.EventHandler(this.磁盘comboBox_SelectedIndexChanged);
            this.磁盘comboBox.SelectionChangeCommitted += new System.EventHandler(this.磁盘comboBox_SelectionChangeCommitted);
            // 
            // 还原点dataGridView
            // 
            this.还原点dataGridView.AllowUserToAddRows = false;
            this.还原点dataGridView.AllowUserToDeleteRows = false;
            this.还原点dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.还原点dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.还原点dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vdiskResotrePointName,
            this.vdiskRestorePointCreateTime,
            this.VdiskRestorePointDescription});
            this.还原点dataGridView.Location = new System.Drawing.Point(2, 32);
            this.还原点dataGridView.Name = "还原点dataGridView";
            this.还原点dataGridView.ReadOnly = true;
            this.还原点dataGridView.RowTemplate.Height = 23;
            this.还原点dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.还原点dataGridView.Size = new System.Drawing.Size(392, 226);
            this.还原点dataGridView.TabIndex = 2;
            // 
            // vdiskResotrePointName
            // 
            this.vdiskResotrePointName.DataPropertyName = "vdiskResotrePointName";
            this.vdiskResotrePointName.HeaderText = "还原点名称";
            this.vdiskResotrePointName.Name = "vdiskResotrePointName";
            this.vdiskResotrePointName.ReadOnly = true;
            // 
            // vdiskRestorePointCreateTime
            // 
            this.vdiskRestorePointCreateTime.DataPropertyName = "vdiskRestorePointCreateTime";
            this.vdiskRestorePointCreateTime.HeaderText = "日期时间";
            this.vdiskRestorePointCreateTime.Name = "vdiskRestorePointCreateTime";
            this.vdiskRestorePointCreateTime.ReadOnly = true;
            // 
            // VdiskRestorePointDescription
            // 
            this.VdiskRestorePointDescription.DataPropertyName = "VdiskRestorePointDescription";
            this.VdiskRestorePointDescription.HeaderText = "说明";
            this.VdiskRestorePointDescription.Name = "VdiskRestorePointDescription";
            this.VdiskRestorePointDescription.ReadOnly = true;
            // 
            // 新增button
            // 
            this.新增button.Location = new System.Drawing.Point(400, 32);
            this.新增button.Name = "新增button";
            this.新增button.Size = new System.Drawing.Size(75, 23);
            this.新增button.TabIndex = 3;
            this.新增button.Text = "新增(&A)";
            this.新增button.UseVisualStyleBackColor = true;
            this.新增button.Click += new System.EventHandler(this.新增button_Click);
            // 
            // 还原button
            // 
            this.还原button.Location = new System.Drawing.Point(400, 61);
            this.还原button.Name = "还原button";
            this.还原button.Size = new System.Drawing.Size(75, 23);
            this.还原button.TabIndex = 5;
            this.还原button.Text = "还原(&R)";
            this.还原button.UseVisualStyleBackColor = true;
            this.还原button.Click += new System.EventHandler(this.还原button_Click);
            // 
            // 退出button
            // 
            this.退出button.Location = new System.Drawing.Point(400, 235);
            this.退出button.Name = "退出button";
            this.退出button.Size = new System.Drawing.Size(75, 23);
            this.退出button.TabIndex = 6;
            this.退出button.Text = "退出(&E)";
            this.退出button.UseVisualStyleBackColor = true;
            this.退出button.Click += new System.EventHandler(this.退出button_Click);
            // 
            // VdiskRestorePointManageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.退出button);
            this.Controls.Add(this.还原button);
            this.Controls.Add(this.新增button);
            this.Controls.Add(this.还原点dataGridView);
            this.Controls.Add(this.磁盘comboBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "VdiskRestorePointManageWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "还原点管理";
            this.Load += new System.EventHandler(this.VdiskRestorePointManageWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.还原点dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox 磁盘comboBox;
        private System.Windows.Forms.DataGridView 还原点dataGridView;
        private System.Windows.Forms.Button 新增button;
        private System.Windows.Forms.Button 还原button;
        private System.Windows.Forms.Button 退出button;
        private System.Windows.Forms.DataGridViewTextBoxColumn vdiskResotrePointName;
        private System.Windows.Forms.DataGridViewTextBoxColumn vdiskRestorePointCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn VdiskRestorePointDescription;

    }
}