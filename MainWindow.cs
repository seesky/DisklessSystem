using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Win2012WMIiSCSIVhdxManager;
using System.Threading;

namespace NoDiskSystem
{
    public partial class MainWindow : Form
    {
        List<ClientGroup> clientGroupList = new List<ClientGroup>();
        List<Client> clientList = new List<Client>();
        TreeNode rootNode = new TreeNode("所有分组", IconIndexes.tree, IconIndexes.tree);
        TreeNode noNode = new TreeNode("未分组", IconIndexes.tree, IconIndexes.tree);
        TFTP tftp;
        HTTP http;
        Thread tftpThread,httpThread;


        public static MainWindow pCurrentWin = null; 
        public MainWindow()
        {
            InitializeComponent();
            pCurrentWin = this;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            /**
            foreach (DataRowView row in 工作站dataGridView.Rows)
            {
                if (row["clientName"].ToString() == e.Node.Tag.ToString())
                { 
                    
                }
            }
             * */
            for (int i = 0; i < 工作站dataGridView.Rows.Count; i++)
            {
                string name = 工作站dataGridView.Rows[i].Cells["名称Column"].Value.ToString();
                if (name == e.Node.Tag.ToString())
                {
                    工作站dataGridView.CurrentRow.Selected = false;
                    工作站dataGridView.Rows[i].Selected = true;
                    工作站dataGridView.CurrentCell = 工作站dataGridView.Rows[i].Cells[0];
                }
            }
        }

        private void 磁盘管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VdiskTemplateManageWindow vdiskTemplateManageWindow = new VdiskTemplateManageWindow();
            vdiskTemplateManageWindow.ShowDialog();
        }

        private void 选项设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemSetting systemSetting = new SystemSetting();
            systemSetting.ShowDialog();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {


            

            
            updateTree();
            updateDataGrid();

            //启动tftp服务
            tftp = new TFTP();
            tftpThread = new Thread(new ThreadStart(tftp.startTftpServer));
            tftpThread.Start();
            
            //启动http服务
            http = new HTTP();
            httpThread = new Thread(new ThreadStart(http.start));
            httpThread.Start();
        }

        public void updateDataGrid()
        {
            //DataGridViewImageColumn status = new DataGridViewImageColumn();
            //status.DisplayIndex = 0;
            //status.HeaderText = "启用";
            //status.DataPropertyName = "clientEnable";
            //status.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //工作站dataGridView.Columns.Insert(0, status);

            clientList.Clear();


            
            this.工作站dataGridView.DataSource = null;

            工作站dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvTestSteps_CellFormatting);

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dt = sh.Select("select * from CLIENT;");

                    Client client;

                    foreach (DataRow row in dt.Rows)
                    {

                        client = new Client();

                        client.ClientId = row["client_id"].ToString();
                        client.ClientName = row["client_name"].ToString();
                        client.ClientIp = row["client_ip"].ToString();
                        client.ClientNetmask = row["client_netmask"].ToString();
                        client.ClientGateway = row["client_gateway"].ToString();
                        client.ClientMac = row["client_mac"].ToString();
                        client.ClientDns1 = row["client_dns1"].ToString();
                        client.ClientDns2 = row["client_dns2"].ToString();
                        client.ClientEnable = row["client_enable"].ToString();
                        client.ClientSuperEnable = row["client_super_enable"].ToString();
                        clientList.Add(client);


                    }


                    this.工作站dataGridView.AutoGenerateColumns = false;

                    if (dt.Rows.Count != 0) {
                        this.工作站dataGridView.DataSource = clientList;
                    }
                    
                   
                    //if (工作站dataGridView.Rows.Count > 0) { 工作站dataGridView.CurrentCell = 工作站dataGridView.Rows[0].Cells[0]; }

                   
                    /**
                    for (int i = 0; i < 工作站dataGridView.Rows.Count; i++)
                    {
                        if (工作站dataGridView.Rows[i].Cells["启用Column"].Value.ToString() == "1")
                        {
                            工作站dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else {
                            工作站dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Gray ;
                        }
                    }
                     * */

                        conn.Close();

                    
                }
            }
        }


        void dgvTestSteps_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (工作站dataGridView.Columns[e.ColumnIndex].HeaderText.Equals("启用"))
            {
                if (e.Value == null)
                {
                    return;
                }
                string type = e.Value.ToString();

                switch (type)
                {
                    case "1":
                        e.Value = 分组imageList.Images[IconIndexes.enable];
                        break;
                    case "0":
                        e.Value = 分组imageList.Images[IconIndexes.disable];
                        break;
                }
            }


            if (工作站dataGridView.Columns[e.ColumnIndex].HeaderText.Equals("超级工作站"))
            {
                if (e.Value == null)
                {
                    return;
                }
                string type = e.Value.ToString();

                switch (type)
                {
                    case "1":
                        e.Value = 分组imageList.Images[IconIndexes.super];
                        break;
                    case "0":
                        e.Value = 分组imageList.Images[IconIndexes.general];
                        break;
                }
            }
        }


        public void updateTree()
        {

            分组treeView.Nodes.Clear();
            rootNode.Nodes.Clear();
            

            //root tree
            //TreeNode rootNode = new TreeNode("所有分组",IconIndexes.tree, IconIndexes.tree);
            rootNode.Tag = "所有分组";
            rootNode.Text = "所有分组";
            分组treeView.Nodes.Add(rootNode);

            TreeNode noNode = new TreeNode("未分组", IconIndexes.tree, IconIndexes.tree);
            noNode.Tag = "未分组";
            noNode.Text = "未分组";
            分组treeView.Nodes.Add(noNode);

            //group tree
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dt = sh.Select("select * from CLIENT_GROUP;");
                    DataTable dts = sh.Select("select * from CLIENT;");

                    ClientGroup tempClientGroup = new ClientGroup();

                    foreach (DataRow ro in dts.Rows)
                    {
                        if (ro["client_group_id"].ToString() == "未分组")
                        {
                            TreeNode clientNode = new TreeNode(ro["client_name"].ToString(), IconIndexes.computer, IconIndexes.computer);
                            clientNode.Tag = ro["client_name"].ToString();
                            clientNode.Text = ro["client_name"].ToString();
                            noNode.Nodes.Add(clientNode);
                        }
                    }


                    foreach (DataRow row in dt.Rows)
                    {

                        tempClientGroup = new ClientGroup();
                        tempClientGroup.ClientGroupId = row["client_group_id"].ToString();
                        tempClientGroup.ClientGroupName = row["client_group_name"].ToString();

                        TreeNode tempNode = new TreeNode(tempClientGroup.ClientGroupName, IconIndexes.computer, IconIndexes.computer);
                        tempNode.Tag = tempClientGroup.ClientGroupName;
                        tempNode.Text = tempClientGroup.ClientGroupName;



                        //将工作站放入对应的组
                        foreach (DataRow rows in dts.Rows)
                        {
                            string tempGroupId = rows["client_group_id"].ToString();

                            if (tempGroupId == tempClientGroup.ClientGroupId)
                            {
                                TreeNode clientNode = new TreeNode(rows["client_name"].ToString(), IconIndexes.computer, IconIndexes.computer);
                                clientNode.Tag = rows["client_name"].ToString();
                                clientNode.Text = rows["client_name"].ToString();
                                tempNode.Nodes.Add(clientNode);
                            }
                        }
                        //
                        rootNode.Nodes.Add(tempNode);
                        clientGroupList.Add(tempClientGroup);

                    }

                    conn.Close();


                }
            }

            分组treeView.ExpandAll();
        }

        private void 添加组toolStripButton_Click(object sender, EventArgs e)
        {
            ClientGroupAdd clientGroupAdd = new ClientGroupAdd(pCurrentWin);
            clientGroupAdd.updateTree += new DelegateUpdateTree(UpdateTree);
            clientGroupAdd.ShowDialog();
            
        }

        public void UpdateTree(TreeNode node)
        {
            this.rootNode.Nodes.Add(node);
        }

        public void UpdateTreeClient(TreeNode node, string tag, Client client)
        {

            //工作站dataGridView.AutoGenerateColumns = false;
            工作站dataGridView.DataSource = null;
            clientList.Add(client);
            工作站dataGridView.DataSource = clientList;


            if (tag == "未分组")
            {
                foreach (TreeNode n in 分组treeView.Nodes)
                {
                    if (n.Text == "未分组")
                    {
                        n.Nodes.Add(node);
                    }

                }

                
            }
            else {
                foreach (TreeNode n in rootNode.Nodes)
                {
                    if (n.Text == tag)
                    {
                        n.Nodes.Add(node);
                    }

                }
            }

        }

        private void 删除组toolStripButton_Click(object sender, EventArgs e)
        {
            string client_group_name = 分组treeView.SelectedNode.Tag.ToString();;
            TreeNode tempNode = 分组treeView.SelectedNode;

            if (tempNode.Parent == null) {
                return;
            }

            if (tempNode.Parent.Tag.ToString() != "所有分组") {
                return;
            }

            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除选中的分组吗?", "删除分组", messButton);
            if (dr == DialogResult.OK)
            {
                
                //将节点下的所有的机器分组设置为无分组


                foreach (TreeNode node in tempNode.Nodes) {
                    noNode.Nodes.Add(node);
                }

                //
                
                tempNode.Remove();

                //group tree
                using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        string sqlstring = "delete from CLIENT_GROUP where client_group_name = '" + client_group_name + "';";

                        sh.ExecuteScalar(sqlstring);

                        conn.Close();


                    }
                }
            }
        }

        private void 添加工作站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientAdd clientAdd = new ClientAdd();
            clientAdd.updateTree += new DelegateUpdateTreeClient(UpdateTreeClient);
            clientAdd.ShowDialog();
        }

        private void 工作站dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void 工作站dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            string name = 工作站dataGridView.Rows[e.RowIndex].Cells["名称Column"].Value.ToString();
            SelectTreeView2(分组treeView, null, name);
            
            /**
            foreach (TreeNode n in 分组treeView.Nodes)
            {
                if (n.Text == name)
                {
                    
                }

            }
             * */
        }

        private void SelectTreeView(TreeView treeView, string selectStr)
        {
            treeView.Focus();
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
                {
                    if (treeView.Nodes[i].Nodes[j].Text == selectStr)
                    {
                        treeView.SelectedNode = treeView.Nodes[i].Nodes[j];//选中
                        //treeView.Nodes[i].Nodes[j].Checked = true;
                        treeView.Nodes[i].Expand();//展开父级
                        return;
                    }
                }
            }
        }

        private void SelectTreeView2(TreeView treeView, TreeNode treeNode, string selectStr)
        {
            if(treeView != null && treeNode == null)
            {
                treeView.Focus();
                for (int i = 0; i < treeView.Nodes.Count; i++)
                {
                    if (treeView.Nodes[i].Tag.ToString() == selectStr)
                    {
                        treeView.SelectedNode = treeView.Nodes[i];
                    }
                    else {
                        if (treeView.Nodes[i].Nodes.Count > 0)
                        {
                            SelectTreeView2(treeView, treeView.Nodes[i], selectStr);
                        }
                    }
                }
            }
            else if (treeView != null && treeNode != null) {
                if (treeNode.Tag.ToString() == selectStr)
                {
                    treeView.SelectedNode = treeNode;
                }else if(treeNode.Nodes.Count > 0){
                    for (int j = 0; j < treeNode.Nodes.Count; j++)
                    {
                        SelectTreeView2(treeView, treeNode.Nodes[j], selectStr);
                    }
                        
                }
            }
            
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            /**
            DataView dv = 工作站dataGridView.DataSource as DataView;
            dv.RowFilter = "名称Column like '%" + 过滤toolStripTextBox.Text.Trim() + "%'";
            工作站dataGridView.DataSource = dv;
             * */
        }

        private void 编辑工作站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (工作站dataGridView.Rows.Count == 0 || 工作站dataGridView.SelectedRows == null)
            {
               
                return;
            }
            ClientEdit clientEdit = new ClientEdit(工作站dataGridView);
            clientEdit.updateGrid += new DelegateUpdateGrid(UpdateGrid);
            clientEdit.ShowDialog();
        }

        public void UpdateGrid(Client client)
        { 
            
        }

        private void 删除工作站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string client_disk_list_id = "";
            string client_word_path = "";
            string client_mac = "";
            string iscsiURL;
            string username;
            string password;

            IniFile ini = new IniFile(Environment.CurrentDirectory + "\\setting.ini");
            iscsiURL = ini.ReadString("IscsiServer", "URL", "");
            username = ini.ReadString("IscsiServer", "USERNAME", "");
            password = ini.ReadString("IscsiServer", "PASSWORD", "");

            if (工作站dataGridView.Rows.Count != 0)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除选中的工作站?", "删除工作站", messButton);
                if (dr == DialogResult.OK)
                {
                    using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand())
                        {
                            cmd.Connection = conn;
                            conn.Open();

                            SQLiteHelper sh = new SQLiteHelper(cmd);

                            string selectDiskListId = "select * from CLIENT where client_id = '" + 工作站dataGridView.CurrentRow.Cells["IDColumn"].Value + "';";
                            string deleteClient = "delete from CLIENT where client_id = '" + 工作站dataGridView.CurrentRow.Cells["IDColumn"].Value + "';";
                            DataTable dtGroup = sh.Select(selectDiskListId);

                            foreach (DataRow row in dtGroup.Rows)
                            {
                                client_disk_list_id = row["client_disk_list_id"].ToString();
                                client_word_path = row["client_work_path"].ToString();
                                client_mac = row["client_mac"].ToString();

                            }

                            string deleteDiskList = "delete from CLIENT_DISK_LIST where client_disk_list_id = '" + client_disk_list_id + "';";

                            string selectDiskList = "select * from CLIENT_DISK_LIST where client_disk_list_id = '" + client_disk_list_id + "';";

                            DataTable dtDiskList = sh.Select(selectDiskList);
                            //删除磁盘文件
                            foreach (DataRow row in dtDiskList.Rows)
                            {
                                string selectDisk = "select * from VDISK_TEMPLET where disk_id = '" + row["disk_id"].ToString() + "';";
                                DataTable dtDisk = sh.Select(selectDisk);
                                foreach (DataRow rowDisk in dtDisk.Rows)
                                {
                                    DiskManagementData ParamObj = new DiskManagementData(); // TODO: 初始化为适当的值
                                    ParamObj.DevicePath = client_word_path + "\\" + rowDisk["disk_name"].ToString() + "-" + client_mac + ".vhdx"; ;
                                    ParamObj.TargetName = rowDisk["disk_name"].ToString() + "-" + client_mac;
                                    ParamObj.serverURL = iscsiURL;
                                    ParamObj.Username = username;
                                    ParamObj.Password = password;

                                    bool actual;
                                    actual = DiskManager.RemoveDisk(ParamObj);

                                    if (actual == true)
                                    {
                                        //MessageBox.Show("磁盘删除成功！");
                                    }
                                }
                            }

                            sh.ExecuteScalar(deleteDiskList);
                            sh.ExecuteScalar(deleteClient);

                            for (int i = 0; i < clientList.Count; i++)
                            {
                                if (clientList[i].ClientMac == client_mac)
                                {
                                    clientList.Remove(clientList[i]);
                                }
                            }
                            this.工作站dataGridView.AutoGenerateColumns = false;
                            工作站dataGridView.DataSource = null;
                            工作站dataGridView.DataSource = clientList;

                            TreeNode node = 分组treeView.SelectedNode;
                            node.Remove();

                            conn.Close();

                            MessageBox.Show("工作站删除完成！");
                        }
                    }
                }
            }
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    string enableString = "update CLIENT set client_enable = '1' where client_id = '" + 工作站dataGridView.CurrentRow.Cells["IDColumn"].Value.ToString() + "';";

                    sh.ExecuteScalar(enableString);

                    for (int i = 0; i < clientList.Count; i++)
                    {
                        if (clientList[i].ClientMac == 工作站dataGridView.CurrentRow.Cells["MACColumn"].Value)
                        {
                            clientList[i].ClientEnable = "1";
                            //工作站dataGridView.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                    }
                    this.工作站dataGridView.AutoGenerateColumns = false;
                    工作站dataGridView.DataSource = null;
                    工作站dataGridView.DataSource = clientList;

                    conn.Close();
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    string enableString = "update CLIENT set client_enable = '0' where client_id = '" + 工作站dataGridView.CurrentRow.Cells["IDColumn"].Value.ToString() + "';";

                    sh.ExecuteScalar(enableString);

                    
                    for (int i = 0; i < clientList.Count; i++)
                    {
                        if (clientList[i].ClientMac == 工作站dataGridView.CurrentRow.Cells["MACColumn"].Value)
                        {
                            clientList[i].ClientEnable = "0";
                            //工作站dataGridView.CurrentRow.DefaultCellStyle.BackColor = Color.Gray;
                        }
                    }
                    this.工作站dataGridView.AutoGenerateColumns = false;
                    工作站dataGridView.DataSource = null;
                    工作站dataGridView.DataSource = clientList;

                    conn.Close();
                }
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    string enableString = "update CLIENT set client_super_enable = '1' where client_id = '" + 工作站dataGridView.CurrentRow.Cells["IDColumn"].Value.ToString() + "';";
                    string disableString = "update CLIENT set client_super_enable = '0' where client_id != '" + 工作站dataGridView.CurrentRow.Cells["IDColumn"].Value.ToString() + "';";
                    sh.ExecuteScalar(enableString);
                    sh.ExecuteScalar(disableString);

                    for (int i = 0; i < clientList.Count; i++)
                    {
                        if (clientList[i].ClientMac == 工作站dataGridView.CurrentRow.Cells["MACColumn"].Value)
                        {
                            clientList[i].ClientSuperEnable = "1";
                            //工作站dataGridView.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else {
                            clientList[i].ClientSuperEnable = "0";
                        }
                    }
                    this.工作站dataGridView.AutoGenerateColumns = false;
                    工作站dataGridView.DataSource = null;
                    工作站dataGridView.DataSource = clientList;

                    conn.Close();
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    
                    string disableString = "update CLIENT set client_super_enable = '0';";
                    
                    sh.ExecuteScalar(disableString);

                    for (int i = 0; i < clientList.Count; i++)
                    {

                        clientList[i].ClientSuperEnable = "0";
                       
                    }
                    this.工作站dataGridView.AutoGenerateColumns = false;
                    工作站dataGridView.DataSource = null;
                    工作站dataGridView.DataSource = clientList;

                    conn.Close();
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string mac = 工作站dataGridView.CurrentRow.Cells["MACColumn"].Value.ToString();

            byte[] _mac = new byte[mac.Length / 2];
            for (int i = 0; i < _mac.Length; i++)
            {
                _mac[i] = Convert.ToByte(mac.Substring(i * 2, 2), 16);
            }

            ClientWakeUp.WakeUp(_mac);

            MessageBox.Show("开机请求已发送！");
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            tftpThread.Abort();
            httpThread.Abort();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            updateDataGrid();
            updateTree();
        }
    }
}
