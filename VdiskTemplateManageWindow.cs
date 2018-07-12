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
using System.Management;


namespace NoDiskSystem
{
    public partial class VdiskTemplateManageWindow : Form
    {
        List<VdiskTemplate> vdiskTempletList = new List<VdiskTemplate>();
        string iscsiURL;
        string username;
        string password;
        public VdiskTemplateManageWindow()
        {
            InitializeComponent();
            
            this.磁盘管理dataGridView.RowHeadersVisible = false;
        }

        private void VdiskTemplateManageWindow_Load(object sender, EventArgs e)
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

                    DataTable dt = sh.Select("select * from VDISK_TEMPLET;");

                    VdiskTemplate vdiskTemp;

                    foreach (DataRow row in dt.Rows)
                    {
                        
                        vdiskTemp = new VdiskTemplate();
                        vdiskTemp.DiskId = row["disk_id"].ToString();
                        vdiskTemp.DiskName = row["disk_name"].ToString();
                        vdiskTemp.DiskSize = (int)row["disk_size"];
                        vdiskTemp.DiskPath = row["disk_path"].ToString();
                        vdiskTemp.DiskType = (int)row["disk_type"];
                        vdiskTempletList.Add(vdiskTemp);

                    }
                    this.磁盘管理dataGridView.AutoGenerateColumns = false;
                    this.磁盘管理dataGridView.DataSource = vdiskTempletList;

                    conn.Close();
                }
            }
        }

        private void 新增button_Click(object sender, EventArgs e)
        {
            VdiskTemplateAdd vdiskTempletAdd = new VdiskTemplateAdd();
            vdiskTempletAdd.vdiskTempletList = this.vdiskTempletList;
            vdiskTempletAdd.ParentDataGridView = this.磁盘管理dataGridView;
            vdiskTempletAdd.ShowDialog();
        }

        private void 删除button_Click(object sender, EventArgs e)
        {
            bool hasClientUseTheDisk = false;
            bool hasPointOfTheDisk = false;

            string diskIds = this.磁盘管理dataGridView.SelectedRows[0].Cells["diskId"].Value.ToString();

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dtClientUseDisk = sh.Select("select * from CLIENT_DISK_LIST where disk_id='" + diskIds + "';");
                    DataTable dtPointOfTheDisk = sh.Select("select * from VDISK_RESTORE_POINT where disk_id='" + diskIds + "';");

                    if (dtClientUseDisk.Rows.Count > 0)
                    {
                        hasClientUseTheDisk = false;
                    }
                    else {
                        hasClientUseTheDisk = true;
                    }

                    if (dtPointOfTheDisk.Rows.Count > 0)
                    {
                        hasPointOfTheDisk = false;
                    }
                    else
                    {
                        hasPointOfTheDisk = true;
                    }

                    conn.Close();
                }
            }

            if (!hasClientUseTheDisk)
            {
                MessageBox.Show("存在使用本磁盘的工作站，请先将工作站磁盘设置为空或设置为其他工作站！");
                return;
            }

            if (!hasPointOfTheDisk)
            {
                MessageBox.Show("本磁盘存在还原点，请先将所有还原点删除后再删除本磁盘！");
                return;
            }



            if (vdiskTempletList.Count != 0)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除选中的磁盘吗?", "删除磁盘", messButton);
                if (dr == DialogResult.OK)
                {
                    //删除选中的磁盘
                    using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand())
                        {
                            cmd.Connection = conn;
                            conn.Open();

                            SQLiteHelper sh = new SQLiteHelper(cmd);



                            //int at = this.skinDataGridViewVdiskAdd.CurrentRow.Index;
                            string diskId = this.磁盘管理dataGridView.SelectedRows[0].Cells["diskId"].Value.ToString();
                            string diskName = this.磁盘管理dataGridView.SelectedRows[0].Cells["diskName"].Value.ToString();
                            string diskPath = this.磁盘管理dataGridView.SelectedRows[0].Cells["diskPath"].Value.ToString();

                            string deleteSql = "DELETE from VDISK_TEMPLET WHERE disk_id = '" + diskId + "'";

                            for (int i = 0; i < vdiskTempletList.Count; i++)
                            {
                                if (vdiskTempletList[i].DiskId == diskId)
                                {
                                    vdiskTempletList.RemoveAt(i);
                                }
                            }
                            this.磁盘管理dataGridView.AutoGenerateColumns = false;
                            this.磁盘管理dataGridView.DataSource = vdiskTempletList.ToArray();

                            sh.ExecuteScalar(deleteSql);



                            DiskManagementData ParamObj = new DiskManagementData(); // TODO: 初始化为适当的值
                            ParamObj.DevicePath = diskPath + diskName + ".vhdx";
                            ParamObj.TargetName = diskName;
                            ParamObj.serverURL = iscsiURL;
                            ParamObj.Username = username;
                            ParamObj.Password = password;

                            bool actual;
                            actual = DiskManager.RemoveDisk(ParamObj);

                            if (actual == true)
                            {
                                MessageBox.Show("磁盘删除成功！");
                            }

                            conn.Close();
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("系统中没有磁盘！");
            }
        }

        private void 导入button_Click(object sender, EventArgs e)
        {
            string devicePath = "";
            string fileName = "";
            string fileNameWithOutEx = "";
            string fileDirectoryName = "";

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = System.IO.Path.GetFileName(dialog.FileName);
                fileNameWithOutEx = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                devicePath = System.IO.Path.GetFullPath(dialog.FileName);
                fileDirectoryName = System.IO.Path.GetDirectoryName(dialog.FileName) + "\\";


                for (int i = 0; i < this.vdiskTempletList.Count; i++)
                {
                    if (vdiskTempletList[i].DiskName == fileNameWithOutEx)
                    {
                        MessageBox.Show("磁盘名称已存在！");
                        return;
                    }
                }

                DiskManagementData ParamObj = new DiskManagementData(); // TODO: 初始化为适当的值
                ParamObj.DevicePath = devicePath;
                ParamObj.TargetName = fileNameWithOutEx;
                ParamObj.TargetIQN = "HstecsTemplet." + fileNameWithOutEx;
                ParamObj.serverURL = iscsiURL;
                ParamObj.Username = username;
                ParamObj.Password = password;
                bool actual;
                actual = DiskManager.ImportVhdxDisk(ParamObj);

                if (actual == true)
                {
                    //获取虚拟磁盘的类型和大小
                    ManagementObject DiskObj = null;
                    if (!DiskManager.GetVirtualDisk(@devicePath, ref DiskObj, iscsiURL, username, password))
                    {
                        throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_GetDisk));

                    }
                    else
                    {
                        var dic = new Dictionary<string, object>();
                        dic["disk_name"] = fileNameWithOutEx;
                        dic["disk_size"] = uint.Parse(DiskObj.GetPropertyValue("Size").ToString()) / 1024;
                        dic["disk_path"] = fileDirectoryName;
                        dic["disk_type"] = uint.Parse(DiskObj.GetPropertyValue("Type").ToString());
                        dic["disk_id"] = System.Guid.NewGuid().ToString("N");


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
                                this.磁盘管理dataGridView.AutoGenerateColumns = false;
                                this.磁盘管理dataGridView.DataSource = vdiskTempletList.ToArray();


                                conn.Close();


                            }
                        }
                        MessageBox.Show("磁盘添加成功！");
                    }

                }
                else
                {
                    MessageBox.Show("磁盘添加失败！");
                }
            }
        }

        private void 退出button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 还原点button_Click(object sender, EventArgs e)
        {
            VdiskRestorePointManageWindow vdiskRestorePointManageWindow = new VdiskRestorePointManageWindow();
            vdiskRestorePointManageWindow.ShowDialog();
        }
    }
}
