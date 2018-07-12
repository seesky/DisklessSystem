namespace NoDiskSystem
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.磁盘管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.分组treeView = new System.Windows.Forms.TreeView();
            this.分组imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.添加组toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.删除组toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.工作站dataGridView = new System.Windows.Forms.DataGridView();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.超级工作站Column = new System.Windows.Forms.DataGridViewImageColumn();
            this.启用Column = new System.Windows.Forms.DataGridViewImageColumn();
            this.名称Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.子网掩码Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.网关Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MACColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.警告信息Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.读写流量Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.读写速度Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.首选DNSColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备选DNSColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.启动服务器Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.分组Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.添加工作站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑工作站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除工作站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.工作站dataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.磁盘管理ToolStripMenuItem,
            this.选项设置ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 磁盘管理ToolStripMenuItem
            // 
            this.磁盘管理ToolStripMenuItem.Name = "磁盘管理ToolStripMenuItem";
            this.磁盘管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.磁盘管理ToolStripMenuItem.Text = "磁盘管理";
            this.磁盘管理ToolStripMenuItem.Click += new System.EventHandler(this.磁盘管理ToolStripMenuItem_Click);
            // 
            // 选项设置ToolStripMenuItem
            // 
            this.选项设置ToolStripMenuItem.Name = "选项设置ToolStripMenuItem";
            this.选项设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.选项设置ToolStripMenuItem.Text = "选项设置";
            this.选项设置ToolStripMenuItem.Click += new System.EventHandler(this.选项设置ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.分组treeView);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.工作站dataGridView);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(984, 536);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 1;
            // 
            // 分组treeView
            // 
            this.分组treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.分组treeView.ImageIndex = 0;
            this.分组treeView.ImageList = this.分组imageList;
            this.分组treeView.Location = new System.Drawing.Point(0, 25);
            this.分组treeView.Name = "分组treeView";
            this.分组treeView.SelectedImageIndex = 0;
            this.分组treeView.Size = new System.Drawing.Size(225, 511);
            this.分组treeView.TabIndex = 1;
            this.分组treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // 分组imageList
            // 
            this.分组imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("分组imageList.ImageStream")));
            this.分组imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.分组imageList.Images.SetKeyName(0, "tree");
            this.分组imageList.Images.SetKeyName(1, "computer");
            this.分组imageList.Images.SetKeyName(2, "enable");
            this.分组imageList.Images.SetKeyName(3, "disable");
            this.分组imageList.Images.SetKeyName(4, "super");
            this.分组imageList.Images.SetKeyName(5, "general");
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加组toolStripButton,
            this.删除组toolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(225, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // 添加组toolStripButton
            // 
            this.添加组toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("添加组toolStripButton.Image")));
            this.添加组toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.添加组toolStripButton.Name = "添加组toolStripButton";
            this.添加组toolStripButton.Size = new System.Drawing.Size(64, 22);
            this.添加组toolStripButton.Text = "添加组";
            this.添加组toolStripButton.Click += new System.EventHandler(this.添加组toolStripButton_Click);
            // 
            // 删除组toolStripButton
            // 
            this.删除组toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("删除组toolStripButton.Image")));
            this.删除组toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.删除组toolStripButton.Name = "删除组toolStripButton";
            this.删除组toolStripButton.Size = new System.Drawing.Size(64, 22);
            this.删除组toolStripButton.Text = "删除组";
            this.删除组toolStripButton.Click += new System.EventHandler(this.删除组toolStripButton_Click);
            // 
            // 工作站dataGridView
            // 
            this.工作站dataGridView.AllowUserToAddRows = false;
            this.工作站dataGridView.AllowUserToDeleteRows = false;
            this.工作站dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.工作站dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.工作站dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.超级工作站Column,
            this.启用Column,
            this.名称Column,
            this.IPColumn,
            this.子网掩码Column,
            this.网关Column,
            this.MACColumn,
            this.警告信息Column,
            this.读写流量Column,
            this.读写速度Column,
            this.首选DNSColumn,
            this.备选DNSColumn,
            this.启动服务器Column,
            this.分组Column});
            this.工作站dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.工作站dataGridView.Location = new System.Drawing.Point(0, 25);
            this.工作站dataGridView.MultiSelect = false;
            this.工作站dataGridView.Name = "工作站dataGridView";
            this.工作站dataGridView.ReadOnly = true;
            this.工作站dataGridView.RowHeadersVisible = false;
            this.工作站dataGridView.RowTemplate.Height = 23;
            this.工作站dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.工作站dataGridView.Size = new System.Drawing.Size(755, 511);
            this.工作站dataGridView.TabIndex = 1;
            this.工作站dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.工作站dataGridView_CellClick);
            this.工作站dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.工作站dataGridView_CellContentClick);
            // 
            // IDColumn
            // 
            this.IDColumn.DataPropertyName = "clientID";
            this.IDColumn.HeaderText = "工作站ID";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            // 
            // 超级工作站Column
            // 
            this.超级工作站Column.DataPropertyName = "clientSuperEnable";
            this.超级工作站Column.HeaderText = "超级工作站";
            this.超级工作站Column.Name = "超级工作站Column";
            this.超级工作站Column.ReadOnly = true;
            this.超级工作站Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.超级工作站Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 启用Column
            // 
            this.启用Column.DataPropertyName = "clientEnable";
            this.启用Column.HeaderText = "启用";
            this.启用Column.Name = "启用Column";
            this.启用Column.ReadOnly = true;
            // 
            // 名称Column
            // 
            this.名称Column.DataPropertyName = "clientName";
            this.名称Column.HeaderText = "名称";
            this.名称Column.Name = "名称Column";
            this.名称Column.ReadOnly = true;
            // 
            // IPColumn
            // 
            this.IPColumn.DataPropertyName = "clientIP";
            this.IPColumn.HeaderText = "IP地址";
            this.IPColumn.Name = "IPColumn";
            this.IPColumn.ReadOnly = true;
            this.IPColumn.Visible = false;
            // 
            // 子网掩码Column
            // 
            this.子网掩码Column.DataPropertyName = "clientNetmask";
            this.子网掩码Column.HeaderText = "子网掩码";
            this.子网掩码Column.Name = "子网掩码Column";
            this.子网掩码Column.ReadOnly = true;
            this.子网掩码Column.Visible = false;
            // 
            // 网关Column
            // 
            this.网关Column.DataPropertyName = "clientGateway";
            this.网关Column.HeaderText = "网关";
            this.网关Column.Name = "网关Column";
            this.网关Column.ReadOnly = true;
            this.网关Column.Visible = false;
            // 
            // MACColumn
            // 
            this.MACColumn.DataPropertyName = "clientMac";
            this.MACColumn.HeaderText = "MAC地址";
            this.MACColumn.Name = "MACColumn";
            this.MACColumn.ReadOnly = true;
            // 
            // 警告信息Column
            // 
            this.警告信息Column.HeaderText = "警告信息";
            this.警告信息Column.Name = "警告信息Column";
            this.警告信息Column.ReadOnly = true;
            this.警告信息Column.Visible = false;
            // 
            // 读写流量Column
            // 
            this.读写流量Column.HeaderText = "读写流量";
            this.读写流量Column.Name = "读写流量Column";
            this.读写流量Column.ReadOnly = true;
            this.读写流量Column.Visible = false;
            // 
            // 读写速度Column
            // 
            this.读写速度Column.HeaderText = "读写速度";
            this.读写速度Column.Name = "读写速度Column";
            this.读写速度Column.ReadOnly = true;
            this.读写速度Column.Visible = false;
            // 
            // 首选DNSColumn
            // 
            this.首选DNSColumn.HeaderText = "首选DNS";
            this.首选DNSColumn.Name = "首选DNSColumn";
            this.首选DNSColumn.ReadOnly = true;
            this.首选DNSColumn.Visible = false;
            // 
            // 备选DNSColumn
            // 
            this.备选DNSColumn.HeaderText = "备选DNS";
            this.备选DNSColumn.Name = "备选DNSColumn";
            this.备选DNSColumn.ReadOnly = true;
            this.备选DNSColumn.Visible = false;
            // 
            // 启动服务器Column
            // 
            this.启动服务器Column.HeaderText = "启动服务器";
            this.启动服务器Column.Name = "启动服务器Column";
            this.启动服务器Column.ReadOnly = true;
            this.启动服务器Column.Visible = false;
            // 
            // 分组Column
            // 
            this.分组Column.HeaderText = "分组";
            this.分组Column.Name = "分组Column";
            this.分组Column.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSeparator2,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripSeparator3,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(755, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加工作站ToolStripMenuItem,
            this.编辑工作站ToolStripMenuItem,
            this.删除工作站ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(97, 22);
            this.toolStripDropDownButton1.Text = "工作站管理";
            // 
            // 添加工作站ToolStripMenuItem
            // 
            this.添加工作站ToolStripMenuItem.Name = "添加工作站ToolStripMenuItem";
            this.添加工作站ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加工作站ToolStripMenuItem.Text = "添加工作站";
            this.添加工作站ToolStripMenuItem.Click += new System.EventHandler(this.添加工作站ToolStripMenuItem_Click);
            // 
            // 编辑工作站ToolStripMenuItem
            // 
            this.编辑工作站ToolStripMenuItem.Name = "编辑工作站ToolStripMenuItem";
            this.编辑工作站ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.编辑工作站ToolStripMenuItem.Text = "编辑工作站";
            this.编辑工作站ToolStripMenuItem.Click += new System.EventHandler(this.编辑工作站ToolStripMenuItem_Click);
            // 
            // 删除工作站ToolStripMenuItem
            // 
            this.删除工作站ToolStripMenuItem.Name = "删除工作站ToolStripMenuItem";
            this.删除工作站ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除工作站ToolStripMenuItem.Text = "删除工作站";
            this.删除工作站ToolStripMenuItem.Click += new System.EventHandler(this.删除工作站ToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton2.Text = "时刻表";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton3.Text = "远程开机";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton4.Text = "启用";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton5.Text = "禁用";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton6.Text = "超级";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton7.Text = "普通";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton1.Text = "刷新工作站";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HNDS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.工作站dataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 磁盘管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView 分组treeView;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton 添加组toolStripButton;
        private System.Windows.Forms.ToolStripButton 删除组toolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 添加工作站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑工作站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除工作站ToolStripMenuItem;
        private System.Windows.Forms.ImageList 分组imageList;
        private System.Windows.Forms.DataGridView 工作站dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewImageColumn 超级工作站Column;
        private System.Windows.Forms.DataGridViewImageColumn 启用Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 子网掩码Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn 网关Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn MACColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 警告信息Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn 读写流量Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn 读写速度Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn 首选DNSColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备选DNSColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 启动服务器Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn 分组Column;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

