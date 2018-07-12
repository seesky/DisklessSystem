namespace NoDiskSystem
{
    partial class VdiskTemplateManageWindow
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
            this.新增button = new System.Windows.Forms.Button();
            this.删除button = new System.Windows.Forms.Button();
            this.导入button = new System.Windows.Forms.Button();
            this.还原点button = new System.Windows.Forms.Button();
            this.退出button = new System.Windows.Forms.Button();
            this.磁盘管理dataGridView = new System.Windows.Forms.DataGridView();
            this.diskId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diskSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diskPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.磁盘管理dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // 新增button
            // 
            this.新增button.Location = new System.Drawing.Point(401, 5);
            this.新增button.Name = "新增button";
            this.新增button.Size = new System.Drawing.Size(75, 23);
            this.新增button.TabIndex = 1;
            this.新增button.Text = "新增(&A)";
            this.新增button.UseVisualStyleBackColor = true;
            this.新增button.Click += new System.EventHandler(this.新增button_Click);
            // 
            // 删除button
            // 
            this.删除button.Location = new System.Drawing.Point(401, 34);
            this.删除button.Name = "删除button";
            this.删除button.Size = new System.Drawing.Size(75, 23);
            this.删除button.TabIndex = 2;
            this.删除button.Text = "删除(&D)";
            this.删除button.UseVisualStyleBackColor = true;
            this.删除button.Click += new System.EventHandler(this.删除button_Click);
            // 
            // 导入button
            // 
            this.导入button.Location = new System.Drawing.Point(401, 63);
            this.导入button.Name = "导入button";
            this.导入button.Size = new System.Drawing.Size(75, 23);
            this.导入button.TabIndex = 3;
            this.导入button.Text = "导入(&I)";
            this.导入button.UseVisualStyleBackColor = true;
            this.导入button.Click += new System.EventHandler(this.导入button_Click);
            // 
            // 还原点button
            // 
            this.还原点button.Location = new System.Drawing.Point(401, 92);
            this.还原点button.Name = "还原点button";
            this.还原点button.Size = new System.Drawing.Size(75, 23);
            this.还原点button.TabIndex = 4;
            this.还原点button.Text = "还原点(&R)";
            this.还原点button.UseVisualStyleBackColor = true;
            this.还原点button.Click += new System.EventHandler(this.还原点button_Click);
            // 
            // 退出button
            // 
            this.退出button.Location = new System.Drawing.Point(401, 235);
            this.退出button.Name = "退出button";
            this.退出button.Size = new System.Drawing.Size(75, 23);
            this.退出button.TabIndex = 5;
            this.退出button.Text = "退出(&E)";
            this.退出button.UseVisualStyleBackColor = true;
            this.退出button.Click += new System.EventHandler(this.退出button_Click);
            // 
            // 磁盘管理dataGridView
            // 
            this.磁盘管理dataGridView.AllowUserToAddRows = false;
            this.磁盘管理dataGridView.AllowUserToDeleteRows = false;
            this.磁盘管理dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.磁盘管理dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.磁盘管理dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.diskId,
            this.diskName,
            this.diskSize,
            this.diskPath});
            this.磁盘管理dataGridView.Location = new System.Drawing.Point(3, 5);
            this.磁盘管理dataGridView.MultiSelect = false;
            this.磁盘管理dataGridView.Name = "磁盘管理dataGridView";
            this.磁盘管理dataGridView.ReadOnly = true;
            this.磁盘管理dataGridView.RowTemplate.Height = 23;
            this.磁盘管理dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.磁盘管理dataGridView.Size = new System.Drawing.Size(392, 253);
            this.磁盘管理dataGridView.TabIndex = 6;
            // 
            // diskId
            // 
            this.diskId.DataPropertyName = "diskId";
            this.diskId.HeaderText = "磁盘ID";
            this.diskId.Name = "diskId";
            this.diskId.ReadOnly = true;
            // 
            // diskName
            // 
            this.diskName.DataPropertyName = "diskName";
            this.diskName.HeaderText = "磁盘名称";
            this.diskName.Name = "diskName";
            this.diskName.ReadOnly = true;
            // 
            // diskSize
            // 
            this.diskSize.DataPropertyName = "diskSize";
            this.diskSize.HeaderText = "磁盘大小";
            this.diskSize.Name = "diskSize";
            this.diskSize.ReadOnly = true;
            // 
            // diskPath
            // 
            this.diskPath.DataPropertyName = "diskPath";
            this.diskPath.HeaderText = "磁盘文件";
            this.diskPath.Name = "diskPath";
            this.diskPath.ReadOnly = true;
            // 
            // VdiskTemplateManageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.磁盘管理dataGridView);
            this.Controls.Add(this.退出button);
            this.Controls.Add(this.还原点button);
            this.Controls.Add(this.导入button);
            this.Controls.Add(this.删除button);
            this.Controls.Add(this.新增button);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "VdiskTemplateManageWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "磁盘管理";
            this.Load += new System.EventHandler(this.VdiskTemplateManageWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.磁盘管理dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button 新增button;
        private System.Windows.Forms.Button 删除button;
        private System.Windows.Forms.Button 导入button;
        private System.Windows.Forms.Button 还原点button;
        private System.Windows.Forms.Button 退出button;
        private System.Windows.Forms.DataGridView 磁盘管理dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn diskId;
        private System.Windows.Forms.DataGridViewTextBoxColumn diskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn diskSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn diskPath;
    }
}