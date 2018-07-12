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
using System.Management;
using Win2012WMIiSCSIVhdxManager;

namespace NoDiskSystem
{
    public partial class VdiskTemplateAdd : Form
    {

        public List<VdiskTemplate> vdiskTempletList;
        private DataGridView parentDataGridView;
        string iscsiURL;
        string username;
        string password;
        public DataGridView ParentDataGridView
        {
            get { return parentDataGridView; }
            set { parentDataGridView = value; }
        }
        public List<VdiskTemplate> VdiskTemplet
        {
            get { return vdiskTempletList; }
            set { vdiskTempletList = value; }
        }
        public VdiskTemplateAdd()
        {
            InitializeComponent();
        }

        private void 取消button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 确定button_Click(object sender, EventArgs e)
        {
            if (this.磁盘名称textBox.Text.Replace(" ", "") == "" || this.磁盘容量textBox.Text.Replace(" ", "") == "" || this.映像文件textBox.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("请填写完整的信息！");
                return;
            }

            if (!TextInputStringValidator.IsEnglishWithNum(this.磁盘名称textBox.Text.Replace(" ", "")))
            {
                MessageBox.Show("磁盘名需以英文字母、数字、_组成！");
                return;
            }

            if (!TextInputStringValidator.IsNumber(this.磁盘容量textBox.Text.Replace(" ", "")))
            {
                MessageBox.Show("磁盘大小只能为数字！");
                return;
            }


            string diskName = this.磁盘名称textBox.Text.Replace(" ", "");
            int diskSize = int.Parse(this.磁盘容量textBox.Text.Replace(" ", ""));
            string diskPath = this.映像文件textBox.Text.Replace(" ", "");
            int diskType;
            if (this.稀疏文件checkBox.Checked)
            {
                diskType = 3;
            }
            else
            {
                diskType = 2;
            }

            if (diskSize < 1 || diskSize > 2048)
            {
                MessageBox.Show("磁盘大小应介于1-2048G之间！");
                return;
            }

            for (int i = 0; i < this.vdiskTempletList.Count; i++)
            {
                if (vdiskTempletList[i].DiskName == diskName)
                {
                    MessageBox.Show("磁盘名称已存在！");
                    return;
                }
            }

            var dic = new Dictionary<string, object>();
            dic["disk_name"] = diskName;
            dic["disk_size"] = diskSize;
            dic["disk_path"] = diskPath + "\\";
            dic["disk_type"] = diskType;
            dic["disk_id"] = System.Guid.NewGuid().ToString("N");


            //插入磁盘数据到数据库
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    sh.Insert("VDISK_TEMPLET", dic);

                    VdiskTemplate vdiskTemplet = new VdiskTemplate();
                    vdiskTemplet.DiskId = dic["disk_id"].ToString();
                    vdiskTemplet.DiskName = dic["disk_name"].ToString();
                    vdiskTemplet.DiskSize = int.Parse(dic["disk_size"].ToString());
                    vdiskTemplet.DiskPath = dic["disk_path"].ToString();
                    vdiskTemplet.DiskType = int.Parse(dic["disk_type"].ToString());

                    vdiskTempletList.Add(vdiskTemplet);
                    parentDataGridView.AutoGenerateColumns = false;
                    parentDataGridView.DataSource = vdiskTempletList.ToArray();

                    //创建磁盘文件
                    DiskManagementData ParamObj = new DiskManagementData(); // TODO: 初始化为适当的值

                    if (this.稀疏文件checkBox.Checked)
                    {
                        ParamObj.VhdxType = 3; //动态盘
                    }
                    else
                    {
                        ParamObj.VhdxType = 2; //固定大小
                    }

                    ParamObj.DevicePath = dic["disk_path"].ToString() + dic["disk_name"].ToString() + ".vhdx";
                    ParamObj.TargetName = dic["disk_name"].ToString();
                    ParamObj.DiskSize = ushort.Parse(dic["disk_size"].ToString());
                    ParamObj.TargetIQN = "HstecsTemplet." + dic["disk_name"].ToString();
                    ParamObj.ParentPath = dic["disk_path"].ToString() + dic["disk_name"].ToString() + ".vhdx";
                    ParamObj.serverURL = iscsiURL;
                    ParamObj.Username = username;
                    ParamObj.Password = password;


                    
                    bool actual;
                    actual = DiskManager.CreateVhdxDisk(ParamObj);

                    if (actual == true)
                    {
                        MessageBox.Show("磁盘添加成功！");
                    }
                    else
                    {
                        MessageBox.Show("磁盘添加失败！");
                    }

                    conn.Close();

                    this.Close();
                }
            }
        }

        private void 映像文件button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.映像文件textBox.Text = path.SelectedPath;
        }

        private void VdiskTemplateAdd_Load(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(Environment.CurrentDirectory + "\\setting.ini");
            iscsiURL = ini.ReadString("IscsiServer", "URL", "");
            username = ini.ReadString("IscsiServer", "USERNAME", "");
            password = ini.ReadString("IscsiServer", "PASSWORD", "");
        }
    }
}
