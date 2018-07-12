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
    public partial class VdiskRestorePointManageWindow : Form
    {
        string iscsiURL;
        string username;
        string password;
        VdiskTemplate vDiskParent;
        List<VdiskTemplate> vdiskTempletList = new List<VdiskTemplate>();
        
        public VdiskRestorePointManageWindow()
        {
            InitializeComponent();
            
            this.还原点dataGridView.RowHeadersVisible = false;
            
        }

        private void VdiskRestorePointManageWindow_Load(object sender, EventArgs e)
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


                    DataTable dts = new DataTable();
                    dts.Columns.Add("Text", Type.GetType("System.String"));
                    dts.Columns.Add("Value", Type.GetType("System.String"));
                    dts.Rows.Add("请选择", "0");
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {

                        vdiskTemp = new VdiskTemplate();
                        vdiskTemp.DiskId = row["disk_id"].ToString();
                        vdiskTemp.DiskName = row["disk_name"].ToString();
                        vdiskTemp.DiskSize = (int)row["disk_size"];
                        vdiskTemp.DiskPath = row["disk_path"].ToString();
                        vdiskTemp.DiskType = (int)row["disk_type"];



                        dts.Rows.Add(vdiskTemp.DiskName, i++);




                        DataTable rs = sh.Select("select * from VDISK_RESTORE_POINT where disk_id = '" + vdiskTemp.DiskId + "';");
                        foreach (DataRow rows in rs.Rows)
                        { 
                            VdiskRestorePoint point = new VdiskRestorePoint();
                            point.VdiskRestorePointId = rows["vdisk_restore_point_id"].ToString();
                            point.VdiskRestorePointCreateTime = rows["vdisk_restore_point_create_time"].ToString();
                            point.VdiskResotrePointName = rows["vdisk_resotre_point_name"].ToString();
                            point.VdiskRestorePointDescription = rows["vdisk_restore_point_description"].ToString();
                            point.VdiskRestorePointPath = rows["vdisk_restore_point_path"].ToString();
                            point.VdiskRestorePointSort = float.Parse(rows["vdisk_restore_point_sort"].ToString());
                            point.VdiskTemplet = vdiskTemp;

                            vdiskTemp.AddVdiskRestorePoint(point);
                        }

                        vdiskTempletList.Add(vdiskTemp);
                    }

                    conn.Close();

                    磁盘comboBox.DataSource = dts;
                    磁盘comboBox.DisplayMember = "Text";   // Text，即显式的文本
                    磁盘comboBox.ValueMember = "Value";    // Value，即实际的值
                    磁盘comboBox.SelectedIndex = 0;
                    
                }
            }
        }

        private void 磁盘comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string diskName =  磁盘comboBox.SelectedValue.ToString().Replace("System.Windows.Controls.ComboBoxItem: ","");
            //string diskName = 磁盘comboBox.Items[磁盘comboBox.SelectedIndex].ToString();
            //string diskName = 磁盘comboBox.Items[磁盘comboBox.SelectedIndex].ToString();
            //string diskName = 磁盘comboBox.SelectedItem.ToString();

            DataRowView drv = (DataRowView)磁盘comboBox.SelectedItem;
            string diskName = Convert.ToString(drv.Row["Text"]);

            foreach (VdiskTemplate temp in vdiskTempletList)
            {
                if (temp.DiskName == diskName)
                {
                    还原点dataGridView.AutoGenerateColumns = false;
                    还原点dataGridView.DataSource = temp.vdiskRestorePoint;
                    vDiskParent = temp;
                    return;
                }
                else
                {
                    还原点dataGridView.AutoGenerateColumns = false;
                    还原点dataGridView.DataSource = new List<VdiskRestorePoint>();
                }
            }
        }

        private void 磁盘comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void 新增button_Click(object sender, EventArgs e)
        {

            DataRowView drv = (DataRowView)磁盘comboBox.SelectedItem;
            string diskName = Convert.ToString(drv.Row["Text"]);
            if (diskName == "请选择")
            {
                return;
            }


            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要创建还原点吗?", "创建还原点", messButton);
            if (dr == DialogResult.OK)
            {

                DateTime dt = DateTime.Now;
                string time = string.Format("{0:yyyyMMddHHmmssffff}", dt);

                DiskManagementData ParamObj = new DiskManagementData();
                ParamObj.VhdxType = 4;



                if (vDiskParent.vdiskRestorePoint == null || vDiskParent.vdiskRestorePoint.Count == 0)
                {
                    ParamObj.DevicePath = vDiskParent.DiskPath + vDiskParent.DiskName + "-" + time + ".vhdx";
                    ParamObj.TargetName = vDiskParent.DiskName + "-" + time;
                    ParamObj.DiskSize = ushort.Parse(vDiskParent.DiskSize.ToString());
                    ParamObj.TargetIQN = "HstecsTemplet." + vDiskParent.DiskName + "-" + time;
                    ParamObj.ParentPath = vDiskParent.DiskPath + vDiskParent.DiskName + ".vhdx";
                    ParamObj.serverURL = iscsiURL;
                    ParamObj.Username = username;
                    ParamObj.Password = password;


                    var dic = new Dictionary<string, object>();
                    dic["vdisk_restore_point_id"] = System.Guid.NewGuid().ToString("N");
                    dic["disk_id"] = vDiskParent.DiskId;
                    dic["vdisk_restore_point_create_time"] = time;
                    dic["vdisk_resotre_point_name"] = vDiskParent.DiskName + "-" + time;
                    dic["vdisk_restore_point_description"] = time;
                    dic["vdisk_restore_point_path"] = vDiskParent.DiskPath + vDiskParent.DiskName + "-" + time + ".vhdx";
                    dic["vdisk_restore_point_sort"] = time;


                    bool actual;
                    actual = DiskManager.CreateVhdxDisk(ParamObj);

                    if (actual == true)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand())
                            {
                                cmd.Connection = conn;
                                conn.Open();

                                SQLiteHelper sh = new SQLiteHelper(cmd);

                                sh.Insert("VDISK_RESTORE_POINT", dic);

                                VdiskRestorePoint tempPoint = new VdiskRestorePoint();
                                tempPoint.VdiskResotrePointName = dic["vdisk_resotre_point_name"].ToString();
                                tempPoint.VdiskRestorePointCreateTime = dic["vdisk_restore_point_create_time"].ToString();
                                tempPoint.VdiskRestorePointDescription = dic["vdisk_restore_point_create_time"].ToString();
                                tempPoint.VdiskRestorePointPath = dic["vdisk_restore_point_path"].ToString();
                                tempPoint.VdiskRestorePointId = dic["vdisk_restore_point_id"].ToString();

                                vDiskParent.AddVdiskRestorePoint(tempPoint);
                                还原点dataGridView.AutoGenerateColumns = false;
                                还原点dataGridView.DataSource = vDiskParent.vdiskRestorePoint.ToArray();

                                conn.Close();
                            }
                        }
                        MessageBox.Show("磁盘添加成功！");
                    }
                    else
                    {
                        MessageBox.Show("磁盘添加失败！");
                    }
                }
                else {
                    VdiskRestorePoint tempPoint = new VdiskRestorePoint();
                    float tempPointSort = 0;

                    foreach (VdiskRestorePoint p in vDiskParent.vdiskRestorePoint)
                    {
                        if (p.VdiskRestorePointSort > tempPointSort)
                        {
                            tempPointSort = p.VdiskRestorePointSort;
                        }
                    }

                    foreach (VdiskRestorePoint p in vDiskParent.vdiskRestorePoint)
                    {
                        if (p.VdiskRestorePointSort == tempPointSort)
                        {
                            tempPoint = p;
                        }
                    }

                    ParamObj.DevicePath = vDiskParent.DiskPath + vDiskParent.DiskName + "-" + time + ".vhdx";
                    ParamObj.TargetName = vDiskParent.DiskName + "-" + time;
                    ParamObj.DiskSize = ushort.Parse(vDiskParent.DiskSize.ToString());
                    ParamObj.TargetIQN = "HstecsTemplet." + vDiskParent.DiskName + "-" + time;
                    //ParamObj.ParentPath = vDiskParent.DiskPath + vDiskParent.DiskName + ".vhdx";
                    ParamObj.ParentPath = tempPoint.VdiskRestorePointPath;
                    ParamObj.serverURL = iscsiURL;
                    ParamObj.Username = username;
                    ParamObj.Password = password;


                    var dic = new Dictionary<string, object>();
                    dic["vdisk_restore_point_id"] = System.Guid.NewGuid().ToString("N");
                    dic["disk_id"] = vDiskParent.DiskId;
                    dic["vdisk_restore_point_create_time"] = time;
                    dic["vdisk_resotre_point_name"] = vDiskParent.DiskName + "-" + time;
                    dic["vdisk_restore_point_description"] = time;
                    dic["vdisk_restore_point_path"] = vDiskParent.DiskPath + vDiskParent.DiskName + "-" + time + ".vhdx";
                    dic["vdisk_restore_point_sort"] = time;


                    bool actual;
                    actual = DiskManager.CreateVhdxDisk(ParamObj);

                    if (actual == true)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand())
                            {
                                cmd.Connection = conn;
                                conn.Open();

                                SQLiteHelper sh = new SQLiteHelper(cmd);

                                sh.Insert("VDISK_RESTORE_POINT", dic);

                                VdiskRestorePoint tempPoints = new VdiskRestorePoint();
                                tempPoints.VdiskResotrePointName = dic["vdisk_resotre_point_name"].ToString();
                                tempPoints.VdiskRestorePointCreateTime = dic["vdisk_restore_point_create_time"].ToString();
                                tempPoints.VdiskRestorePointDescription = dic["vdisk_restore_point_create_time"].ToString();
                                tempPoints.VdiskRestorePointSort = float.Parse(dic["vdisk_restore_point_sort"].ToString());
                                tempPoints.VdiskRestorePointPath = dic["vdisk_restore_point_path"].ToString();
                                tempPoints.VdiskRestorePointId = dic["vdisk_restore_point_id"].ToString();

                                vDiskParent.AddVdiskRestorePoint(tempPoints);
                                还原点dataGridView.AutoGenerateColumns = false;
                                还原点dataGridView.DataSource = vDiskParent.vdiskRestorePoint.ToArray();

                                conn.Close();
                            }
                        }
                        MessageBox.Show("还原点创建成功！");
                    }
                    else
                    {
                        MessageBox.Show("还原点创建失败！");
                    }
                }


            }
        }

        private void 还原button_Click(object sender, EventArgs e)
        {

            DataRowView drv = (DataRowView)磁盘comboBox.SelectedItem;
            string diskName = Convert.ToString(drv.Row["Text"]);

            if (diskName == "请选择")
            {
                return;
            }

            if (vDiskParent.vdiskRestorePoint == null || vDiskParent.vdiskRestorePoint.Count == 0)
            {
                MessageBox.Show("磁盘没有还原点！");
                return;
            }


            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要退回到最近的还原点吗?", "还原", messButton);
            if (dr == DialogResult.OK)
            {
                using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);


                        VdiskRestorePoint tempPoint = new VdiskRestorePoint();
                        float tempPointSort = 0;

                        foreach (VdiskRestorePoint p in vDiskParent.vdiskRestorePoint)
                        {
                            if (p.VdiskRestorePointSort > tempPointSort)
                            {
                                tempPointSort = p.VdiskRestorePointSort;
                            }
                        }

                        foreach (VdiskRestorePoint p in vDiskParent.vdiskRestorePoint)
                        {
                            if (p.VdiskRestorePointSort == tempPointSort)
                            {
                                tempPoint = p;
                            }
                        }


                        string vdisk_restore_point_id = tempPoint.VdiskRestorePointId;
                        string vdisk_resotre_point_name = tempPoint.VdiskResotrePointName;
                        string vdisk_restore_point_path = tempPoint.VdiskRestorePointPath;


                        string deleteSql = "DELETE from VDISK_RESTORE_POINT WHERE vdisk_restore_point_id = '" + vdisk_restore_point_id + "'";

                        

                        vDiskParent.vdiskRestorePoint.Remove(tempPoint);

                        还原点dataGridView.AutoGenerateColumns = false;

                        还原点dataGridView.DataSource = vDiskParent.vdiskRestorePoint.ToArray();

                        sh.ExecuteScalar(deleteSql);
                        conn.Close();

                        DiskManagementData ParamObj = new DiskManagementData(); // TODO: 初始化为适当的值
                        ParamObj.DevicePath = tempPoint.VdiskRestorePointPath;
                        ParamObj.TargetName = tempPoint.VdiskResotrePointName;
                        ParamObj.serverURL = iscsiURL;
                        ParamObj.Username = username;
                        ParamObj.Password = password;

                        bool actual;
                        actual = DiskManager.RemoveDisk(ParamObj);

                        if (actual == true)
                        {
                            MessageBox.Show("磁盘删除成功！");
                        }
                    }
                }
            }
            
        }

        private void 退出button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
