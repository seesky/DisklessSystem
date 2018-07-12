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

namespace NoDiskSystem
{
    public delegate void DelegateUpdateGrid(Client client);
    public partial class ClientEdit : Form
    {

        public event DelegateUpdateGrid updateGrid;

        private DataGridView parentDataGridView;

        string client_id = "";
        int client_enable = 1;
        string client_name = "";
        string client_mac = "";
        string client_group_id = "";
        string client_work_path = "";
        string client_disk_list_id = "";
        string client_name_prefix = "";
        string client_group_name = "";
        string client_description = "";

        string iscsiURL;
        string username;
        string password;

        List<VdiskTemplate> vdiskTempletList = new List<VdiskTemplate>();
        List<VdiskTemplate> tovdiskTempletList = new List<VdiskTemplate>();
        public ClientEdit(DataGridView parentDataGridView)
        {
            InitializeComponent();
            this.parentDataGridView = parentDataGridView;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClientEdit_Load(object sender, EventArgs e)
        {



            IniFile ini = new IniFile(Environment.CurrentDirectory + "\\setting.ini");
            iscsiURL = ini.ReadString("IscsiServer", "URL", "");
            username = ini.ReadString("IscsiServer", "USERNAME", "");
            password = ini.ReadString("IscsiServer", "PASSWORD", "");

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    

                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    client_id = parentDataGridView.CurrentRow.Cells["IDColumn"].Value.ToString();

                    DataTable dt = sh.Select("select * from CLIENT where client_id = '" + client_id + "';");


                    foreach (DataRow row in dt.Rows)
                    {

                        client_group_id = row["client_group_id"].ToString();
                        client_group_name = row["client_group_id"].ToString();
                        client_work_path = row["client_work_path"].ToString();
                        client_mac = row["client_mac"].ToString();
                        client_description = row["client_description"].ToString();
                        client_name = row["client_name"].ToString();
                        client_disk_list_id = row["client_disk_list_id"].ToString();


                        if (row["client_enable"].ToString() == "1")
                        {
                            启用工作站checkBox.Checked = true;
                        }
                        else {
                            启用工作站checkBox.Checked = false;
                        }

                        名称textBox.Text = client_name;
                        MACtextBox.Text = client_mac;
                        工作目录textBox.Text = client_work_path;
                        备注textBox.Text = client_description;

                    }

                    //设置combobox的值
                    DataTable dtCombobox = new DataTable();
                    dtCombobox.Columns.Add("Text", Type.GetType("System.String"));
                    dtCombobox.Columns.Add("Value", Type.GetType("System.String"));
                    dtCombobox.Rows.Add("未分组", "0");

                    分组comboBox.DataSource = dtCombobox;
                    分组comboBox.DisplayMember = "Text";
                    分组comboBox.ValueMember = "Value";

                    DataTable dtAvailableCombobox = sh.Select("select * from CLIENT_GROUP;");
                    int count = 1;
                    foreach (DataRow row in dtAvailableCombobox.Rows)
                    {
                        dtCombobox.Rows.Add(row["client_group_name"].ToString(), count++);
                    }


                    if (client_group_name == "未分组")
                    {
                        分组comboBox.SelectedIndex = 0;
                    }
                    else {
                        DataTable dtCurrentCombobox = sh.Select("select * from CLIENT_GROUP where client_group_id = '" + client_group_name + "';");
                        foreach (DataRow row in dtCurrentCombobox.Rows)
                        {
                            string temp = row["client_group_name"].ToString();
                        

                            for (int i = 0; i < 分组comboBox.Items.Count; i++)
                            {
                                if (分组comboBox.GetItemText(分组comboBox.Items[i]) == temp)
                                {
                                    分组comboBox.SelectedIndex = i;
                                }
                            }

                        }
                    }
                    


                    conn.Close();


                }
            }

            
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dtFrom = sh.Select("select * from VDISK_TEMPLET;");

                    VdiskTemplate vdiskTemp;

                    foreach (DataRow row in dtFrom.Rows)
                    {

                        vdiskTemp = new VdiskTemplate();
                        vdiskTemp.DiskId = row["disk_id"].ToString();
                        vdiskTemp.DiskName = row["disk_name"].ToString();
                        vdiskTemp.DiskPath = row["disk_path"].ToString();
                        vdiskTempletList.Add(vdiskTemp);

                    }

                    DataTable dtTo = sh.Select("select * from CLIENT_DISK_LIST where client_disk_list_id='" + client_disk_list_id + "';");
                    List<VdiskTemplate> tempVdiskTempletList = new List<VdiskTemplate>();

                    if (dtTo.Rows.Count == 0)
                    {

                    }
                    else {
                        
                        foreach (DataRow row in dtTo.Rows)
                        {
                            foreach (VdiskTemplate v in vdiskTempletList)
                            {
                                string to_disk_id = row["disk_id"].ToString();
                                if (v.DiskId == to_disk_id)
                                {
                                    tovdiskTempletList.Add(v);
                                    
                                }

                                    /**
                                else
                                {
                                    
                                    foreach (VdiskTemplate vv in tempVdiskTempletList)
                                    {
                                        if (vv.DiskId == v.DiskId)
                                        {
                                            hasItem = true;
                                        }
                                    }
                                    if (!hasItem)
                                    {
                                        tempVdiskTempletList.Add(v);
                                        hasItem = false;
                                    }  
                                }
                                     * */
                            }
                        }

                        foreach (VdiskTemplate vv in tovdiskTempletList)
                        {
                            foreach (VdiskTemplate v in vdiskTempletList)
                            {
                                if (vv.DiskId == v.DiskId)
                                {
                                    vdiskTempletList.Remove(v);
                                    break;
                                }
                            }
                        }

                        //vdiskTempletList = tempVdiskTempletList;
                    }

                    this.磁盘fromdataGridView.AutoGenerateColumns = false;
                    this.磁盘todataGridView.AutoGenerateColumns = false;
                    this.磁盘fromdataGridView.DataSource = null;
                    this.磁盘todataGridView.DataSource = null;
                    if (vdiskTempletList.Count != 0)
                    {
                        this.磁盘fromdataGridView.DataSource = vdiskTempletList;
                    }

                    if (tovdiskTempletList.Count != 0)
                    {
                        this.磁盘todataGridView.DataSource = tovdiskTempletList;
                    }

                    
                    conn.Close();
                }
            }
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            工作目录textBox.Text = path.SelectedPath;
        }

        private void 复原button_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dt = sh.Select("select * from VDISK_TEMPLET;");

                    vdiskTempletList.Clear();

                    VdiskTemplate vdiskTemp;

                    foreach (DataRow row in dt.Rows)
                    {

                        vdiskTemp = new VdiskTemplate();
                        vdiskTemp.DiskId = row["disk_id"].ToString();
                        vdiskTemp.DiskName = row["disk_name"].ToString();
                        vdiskTemp.DiskPath = row["disk_path"].ToString();
                        vdiskTempletList.Add(vdiskTemp);

                    }

                    this.磁盘fromdataGridView.AutoGenerateColumns = false;
                    this.磁盘todataGridView.AutoGenerateColumns = false;

                    this.磁盘fromdataGridView.DataSource = null;
                    this.磁盘fromdataGridView.DataSource = vdiskTempletList;

                    this.磁盘todataGridView.DataSource = null;
                    tovdiskTempletList.Clear();

                    conn.Close();
                }
            }

            磁盘todataGridView.DataSource = new List<VdiskTemplate>();
        }

        private void button添加_Click(object sender, EventArgs e)
        {
            string disk_id = "";
            string disk_name = "";
            string disk_path = "";

            if (磁盘fromdataGridView.Rows.Count != 0)
            {
                int a = 磁盘fromdataGridView.CurrentRow.Index;
                disk_id = 磁盘fromdataGridView.Rows[a].Cells["diskId"].Value.ToString();
                disk_name = 磁盘fromdataGridView.Rows[a].Cells["diskName"].Value.ToString();
                disk_path = 磁盘fromdataGridView.Rows[a].Cells["diskPath"].Value.ToString();

                foreach (VdiskTemplate vdisk in vdiskTempletList)
                {
                    if (vdisk.DiskId == disk_id)
                    {
                        vdiskTempletList.Remove(vdisk);
                        break;
                    }
                }
                磁盘fromdataGridView.DataSource = null;

                if (vdiskTempletList.Count != 0)
                {
                    磁盘fromdataGridView.DataSource = vdiskTempletList;
                }
                

                //int addindex = this.磁盘todataGridView.Rows.Add();
                //this.磁盘todataGridView.Rows[addindex].Cells["toDiskId"].Value = disk_id;
                //this.磁盘todataGridView.Rows[addindex].Cells["toDiskName"].Value = disk_name;

                
                this.磁盘todataGridView.AutoGenerateColumns = false;
                磁盘todataGridView.DataSource = null;
                VdiskTemplate tovdisk = new VdiskTemplate();
                tovdisk.DiskId = disk_id;
                tovdisk.DiskName = disk_name;
                tovdisk.DiskPath = disk_path;
                tovdiskTempletList.Add(tovdisk);

                if (tovdiskTempletList.Count != 0)
                {
                    磁盘todataGridView.DataSource = tovdiskTempletList;
                }

                磁盘todataGridView.Refresh();
                //磁盘todataGridView.CurrentCell = 磁盘todataGridView.Rows[0].Cells[0];
                
            }

            
        }

        private void button删除_Click(object sender, EventArgs e)
        {
            string disk_id = "";
            string disk_name = "";
            string disk_path = "";
            this.磁盘fromdataGridView.AutoGenerateColumns = false;
            this.磁盘todataGridView.AutoGenerateColumns = false;

            if (磁盘todataGridView.Rows.Count != 0) 
            {
                int a = 磁盘todataGridView.CurrentRow.Index;
                disk_id = 磁盘todataGridView.Rows[a].Cells["toDiskId"].Value.ToString();
                disk_name = 磁盘todataGridView.Rows[a].Cells["toDiskName"].Value.ToString();
                disk_path = 磁盘todataGridView.Rows[a].Cells["toDiskPath"].Value.ToString();

                foreach (VdiskTemplate vdisk in tovdiskTempletList)
                {
                    if (vdisk.DiskId == disk_id)
                    {
                        tovdiskTempletList.Remove(vdisk);
                        break;
                    }
                }
                磁盘todataGridView.DataSource = null;

                if (tovdiskTempletList.Count != 0)
                {
                    磁盘todataGridView.DataSource = tovdiskTempletList;
                }
                

                //int addindex = this.磁盘todataGridView.Rows.Add();
                //this.磁盘todataGridView.Rows[addindex].Cells["toDiskId"].Value = disk_id;
                //this.磁盘todataGridView.Rows[addindex].Cells["toDiskName"].Value = disk_name;
                磁盘fromdataGridView.DataSource = null;
                VdiskTemplate fromvdisk = new VdiskTemplate();
                fromvdisk.DiskId = disk_id;
                fromvdisk.DiskName = disk_name;
                fromvdisk.DiskPath = disk_path;
                vdiskTempletList.Add(fromvdisk);

                if (vdiskTempletList.Count != 0)
                {
                    磁盘fromdataGridView.DataSource = vdiskTempletList;
                }
                
            }

            
        }

        private void 排序向上button_Click(object sender, EventArgs e)
        {
            string disk_id = "";
            string disk_name = "";
            string disk_path = "";

            this.磁盘fromdataGridView.AutoGenerateColumns = false;
            this.磁盘todataGridView.AutoGenerateColumns = false;

            if (磁盘todataGridView.Rows.Count != 0)
            {
                int rowIndex = 磁盘todataGridView.CurrentRow.Index;
                disk_id = 磁盘todataGridView.Rows[rowIndex].Cells["toDiskId"].Value.ToString();
                disk_name = 磁盘todataGridView.Rows[rowIndex].Cells["toDiskName"].Value.ToString();
                disk_path = 磁盘todataGridView.Rows[rowIndex].Cells["toDiskPath"].Value.ToString();

                if (磁盘todataGridView.Rows.Count == 1)
                {
                    return;
                }
                else {
                    if (rowIndex == 0)
                    {
                        return;
                    }
                    else {
                        for (int i = 0; i < tovdiskTempletList.Count; i++)
                        {
                            if (((VdiskTemplate)tovdiskTempletList[i]).DiskId == disk_id)
                            {

                                ((VdiskTemplate)tovdiskTempletList[i]).DiskId = ((VdiskTemplate)tovdiskTempletList[i - 1]).DiskId;
                                ((VdiskTemplate)tovdiskTempletList[i]).DiskName = ((VdiskTemplate)tovdiskTempletList[i - 1]).DiskName;

                                ((VdiskTemplate)tovdiskTempletList[i - 1]).DiskId = disk_id;
                                ((VdiskTemplate)tovdiskTempletList[i - 1]).DiskName = disk_name;
                                ((VdiskTemplate)tovdiskTempletList[i - 1]).DiskPath = disk_path;

                                磁盘todataGridView.DataSource = null;

                                if (tovdiskTempletList.Count != 0)
                                {
                                    磁盘todataGridView.DataSource = tovdiskTempletList;
                                }
                                

                                磁盘todataGridView.Rows[rowIndex - 1].Selected = true;
                                磁盘todataGridView.Rows[rowIndex].Selected = false;

                                磁盘todataGridView.CurrentCell = 磁盘todataGridView.Rows[rowIndex - 1].Cells[0];

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void 排序向下button_Click(object sender, EventArgs e)
        {
            string disk_id = "";
            string disk_name = "";
            string disk_path = "";

            this.磁盘fromdataGridView.AutoGenerateColumns = false;
            this.磁盘todataGridView.AutoGenerateColumns = false;

            if (磁盘todataGridView.Rows.Count != 0)
            {
                int rowIndex = 磁盘todataGridView.CurrentRow.Index;
                disk_id = 磁盘todataGridView.Rows[rowIndex].Cells["toDiskId"].Value.ToString();
                disk_name = 磁盘todataGridView.Rows[rowIndex].Cells["toDiskName"].Value.ToString();
                disk_path = 磁盘todataGridView.Rows[rowIndex].Cells["toDiskPath"].Value.ToString();

                if (磁盘todataGridView.Rows.Count == 1)
                {
                    return;
                }
                else
                {
                    if (rowIndex == (磁盘todataGridView.Rows.Count - 1))
                    {
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < tovdiskTempletList.Count; i++)
                        {
                            if (((VdiskTemplate)tovdiskTempletList[i]).DiskId == disk_id)
                            {

                                ((VdiskTemplate)tovdiskTempletList[i]).DiskId = ((VdiskTemplate)tovdiskTempletList[i + 1]).DiskId;
                                ((VdiskTemplate)tovdiskTempletList[i]).DiskName = ((VdiskTemplate)tovdiskTempletList[i + 1]).DiskName;

                                ((VdiskTemplate)tovdiskTempletList[i + 1]).DiskId = disk_id;
                                ((VdiskTemplate)tovdiskTempletList[i + 1]).DiskName = disk_name;
                                ((VdiskTemplate)tovdiskTempletList[i + 1]).DiskPath = disk_path;

                                磁盘todataGridView.DataSource = null;

                                if (tovdiskTempletList.Count != 0)
                                {
                                    磁盘todataGridView.DataSource = tovdiskTempletList;
                                }
                                

                                磁盘todataGridView.Rows[rowIndex + 1].Selected = true;
                                磁盘todataGridView.Rows[rowIndex].Selected = false;

                                磁盘todataGridView.CurrentCell = 磁盘todataGridView.Rows[rowIndex + 1].Cells[0];

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void 应用button_Click(object sender, EventArgs e)
        {
            if((名称textBox.Text.Trim() == "") || (MACtextBox.Text.Trim() == "") || (工作目录textBox.Text.Trim() == ""))
            {
                MessageBox.Show("请填写完整的信息！");
                return;
            }
            
            string client_name = 名称textBox.Text.Trim();
            string client_mac = MACtextBox.Text.Trim();
            string client_work_path = 工作目录textBox.Text.Trim();
            string client_description = 备注textBox.Text.Trim();
            string client_group_id = "";
            string client_group_tag = "";

            if (client_disk_list_id == "")
            {
                client_disk_list_id = System.Guid.NewGuid().ToString("N");
            }
            
            
            if (启用工作站checkBox.Checked == true)
            {
                client_enable = 1;
            }
            else {
                client_enable = 0;
            }

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    string tempSelectedText = 分组comboBox.Text;

                    DataTable dt = sh.Select("select * from CLIENT_GROUP where client_group_name = '" + tempSelectedText + "';");

                    if (dt.Rows.Count == 0)
                    {
                        client_group_id = "未分组";
                        client_group_tag = "未分组";
                    }else{
                        foreach (DataRow row in dt.Rows)
                        {
                            client_group_id = row["client_group_id"].ToString();
                            client_group_tag = row["client_group_name"].ToString();
                        }
                    }
                    conn.Close();
                }
            }


            //判断是否有重复的MAC地址，如果有则不允许添加
            bool tempMacOnly = false;

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    string tempMacString = "select count(*) from CLIENT where client_mac = '" + client_mac + "' and client_id != '" + client_id + "';";
                    int count = int.Parse(sh.ExecuteScalar(tempMacString).ToString());

                    if (count == 0)
                    {
                        tempMacOnly = true;
                    }
                    else
                    {
                        tempMacOnly = false;
                    }
                    conn.Close();
                }
            }


            if (tempMacOnly == true)
            {
                using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        //更新主页datagrid的source
                        Client client = new Client();
                        client.ClientId = client_id;
                        client.ClientName = client_name;
                        client.ClientMac = client_mac;
                        client.clientWorkPath = client_work_path;
                        client.clientDescription = client_description;
                        
                        

                        //string addClientSql = "udpate CLIENT (client_id,client_name,client_mac,client_work_path,client_description,client_group_id,client_disk_list_id,client_enable) values ('" + client_id + "','" + client_name + "','" + client_mac + "','" + client_work_path + "','" + client_description + "','" + client_group_id + "','" + client_disk_list_id + "','"  + client_enable + "');";
                        string updateClientSql = "update CLIENT set client_name='" + client_name + "',client_mac='" + client_mac + "',client_work_path='" + client_work_path + "',client_description='" + client_description + "',client_group_id='" + client_group_id + "',client_disk_list_id='" + client_disk_list_id + "',client_enable='" + client_enable + "' where client_id='" + client_id + "';";

                        sh.ExecuteScalar(updateClientSql);
                        //删除原先已经创建的磁盘以及数据库中的内容
                        string deleteClientDiskList = "delete from CLIENT_DISK_LIST where client_disk_list_id='" + client_disk_list_id + "';";

                        DataTable dtClintDiskList = sh.Select("select * from CLIENT_DISK_LIST where client_disk_list_id = '" + client_disk_list_id + "';");
                        foreach (DataRow row in dtClintDiskList.Rows)
                        {
                            string sqlClientDiskName = "select * from VDISK_TEMPLET where disk_id='" + row["disk_id"] + "';";
                            DataTable dtClientDisk = sh.Select(sqlClientDiskName);
                            foreach (DataRow rows in dtClientDisk.Rows)
                            {
                                DiskManagementData ParamObj = new DiskManagementData(); // TODO: 初始化为适当的值
                                ParamObj.DevicePath = client_work_path + "\\" + rows["disk_name"].ToString() + "-" + client_mac + ".vhdx"; ;
                                ParamObj.TargetName = rows["disk_name"].ToString() + "-" + client_mac;
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

                        sh.ExecuteScalar(deleteClientDiskList);



                        int diskSort = 0;
                        string addClientDiskList = "";
                        foreach (VdiskTemplate v in tovdiskTempletList)
                        {
                           addClientDiskList = "insert into CLIENT_DISK_LIST (client_disk_list_id,disk_id,disk_sort) values ('" + client_disk_list_id + "','" + v.DiskId + "','" + diskSort++ + "');";

                           sh.ExecuteScalar(addClientDiskList);
                        }
                        

                        conn.Close();
                        this.Close();


                        //创建客户端启动脚本


                        //创建客户端启动盘
                        foreach (VdiskTemplate v in tovdiskTempletList)
                        {


                            //获取最新的还原点的磁盘路径

                            string parentPointPath = "";
                            string sort = "0";
                            DataTable rs = sh.Select("select * from VDISK_RESTORE_POINT where disk_id = '" + v.DiskId + "';");
                            int count = rs.Rows.Count;
                            if (count != 0)
                            {

                                foreach (DataRow rows in rs.Rows)
                                {
                                    if (float.Parse(sort) < float.Parse(rows["vdisk_restore_point_sort"].ToString()))
                                    {
                                        sort = rows["vdisk_restore_point_sort"].ToString();
                                        parentPointPath = rows["vdisk_restore_point_path"].ToString();
                                    }

                                }
                            }
                            else
                            {
                                parentPointPath = v.DiskPath + v.DiskName + ".vhdx";
                            }



                            //创建磁盘文件
                            DiskManagementData ParamObj = new DiskManagementData(); // TODO: 初始化为适当的值

                            
                            ParamObj.VhdxType = 4; //动态盘
                            ParamObj.DevicePath = client_work_path + "\\" + v.DiskName + "-" + client_mac + ".vhdx";
                            ParamObj.TargetName = v.DiskName + "-" + client_mac;
                            ParamObj.DiskSize = ushort.Parse("10");
                            ParamObj.TargetIQN = "HstecsClient." + v.DiskName + "-" + client_mac;
                            ParamObj.ParentPath = parentPointPath;
                            ParamObj.serverURL = iscsiURL;
                            ParamObj.Username = username;
                            ParamObj.Password = password;

                            DiskManager.CreateVhdxDisk(ParamObj);

                        }

                        //更新tree及主页datagrid
                        TreeNode node = new TreeNode(client_name, IconIndexes.computer, IconIndexes.computer);
                        node.Tag = client_name;
                        node.Text = client_name;



                        UpdateGrid(client);

                        MessageBox.Show("工作站编辑成功！");
                        
                    }
                }
            }
            else {
                MessageBox.Show("已存在指定MAC地址的客户端！");
            }


        }

        private void UpdateGrid(Client client)
        {
            updateGrid(client);
        }
    }
}
