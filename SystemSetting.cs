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
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace NoDiskSystem
{
    public partial class SystemSetting : Form
    {
        public SystemSetting()
        {
            InitializeComponent();
        }

        private void 取消button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SystemSetting_Load(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(Environment.CurrentDirectory + "\\setting.ini");
            string iscsiURL = ini.ReadString("IscsiServer", "URL", "");
            string username = ini.ReadString("IscsiServer", "USERNAME", "");
            string password = ini.ReadString("IscsiServer", "PASSWORD", "");

            string client_add_mode = "";
            string client_name_prefix = "";
            string client_name_num;
            string server_ip = "";
            string client_boot_delay = "";
            string admin_password = "";
            string client_disk_path = "";
            

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dt = sh.Select("select * from SYSTEM_SETTING;");

                    

                    foreach (DataRow row in dt.Rows)
                    {
                        client_add_mode = row["client_add_mode"].ToString();
                        client_name_prefix = row["client_name_prefix"].ToString();
                        client_name_num = row["client_name_num"].ToString();
                        server_ip = row["server_ip"].ToString();
                        client_boot_delay = row["client_boot_delay"].ToString();
                        admin_password = row["admin_password"].ToString();
                        client_disk_path = row["client_disk_path"].ToString();
                    }
                    conn.Close();
                    DataTable dts = new DataTable();
                    
                    dts.Columns.Add("Text", Type.GetType("System.String"));
                    dts.Columns.Add("Value", Type.GetType("System.String"));
                    dts.Rows.Add("手动加入", "0");
                    dts.Rows.Add("全自动加入", "1");

                    加入方式comboBox.DataSource = dts;
                    加入方式comboBox.DisplayMember = "Text";
                    加入方式comboBox.ValueMember = "Value";



                    switch (client_add_mode)
                    { 
                        case "手动加入":
                            加入方式comboBox.SelectedIndex = 0;
                            break;

                        case "全自动加入":
                            加入方式comboBox.SelectedIndex = 1;
                            break;
                    }

                    名称前缀textBox.Text = client_name_prefix;
                    默认目录textBox.Text = client_disk_path;


                    DataTable dtss = new DataTable();

                    dtss.Columns.Add("Text", Type.GetType("System.String"));
                    dtss.Columns.Add("Value", Type.GetType("System.String"));

                    /**
                    string hostname = Dns.GetHostName();
                    IPAddress[] ipadrlist = Dns.GetHostAddresses(hostname);
                    int i = 0;
                    foreach (IPAddress ipa in ipadrlist)
                    {
                        dtss.Rows.Add(ipa.ToString(), i++);
                    }
                     * */
                    string HostName = Dns.GetHostName();
                    IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                    for (int i = 0; i < IpEntry.AddressList.Length; i++)
                    {
                        //从IP地址列表中筛选出IPv4类型的IP地址
                        //AddressFamily.InterNetwork表示此IP为IPv4,
                        //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                        if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            dtss.Rows.Add(IpEntry.AddressList[i].ToString(), i);
                        }
                    }

                    使用IPcheckedListBox.DataSource = dtss;
                    使用IPcheckedListBox.ValueMember = "Value";
                    使用IPcheckedListBox.DisplayMember = "Text";

                    int item = 0;
                    for(int i = 0; i < dtss.Rows.Count; i++){
                        if (dtss.Rows[i]["Text"].ToString() == server_ip) {
                            item = int.Parse(dtss.Rows[i]["Value"].ToString());
                        }
                    }
                    使用IPcheckedListBox.SetItemChecked(item-1, true);

                    超时时间numericUpDown.Value = decimal.Parse(client_boot_delay);

                    登录密码textBox.Text = admin_password;
                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] passwordResult = Encoding.Default.GetBytes(登录密码textBox.Text.Trim());

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(passwordResult);

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    //string deleteSql = "DELETE from VDISK_RESTORE_POINT WHERE vdisk_restore_point_id = '" + vdisk_restore_point_id + "'";
                    string updateSql = "UPDATE SYSTEM_SETTING SET admin_password = '" + BitConverter.ToString(output).Replace("-", "").ToLower() + "'";
                    sh.ExecuteScalar(updateSql);
                    MessageBox.Show("密码更新成功！");
                }
            }
        }

        private void 登录密码textBox_Enter(object sender, EventArgs e)
        {
            登录密码textBox.Text = "";
        }

        private void 确定button_Click(object sender, EventArgs e)
        {
            string jiarufangshi = 加入方式comboBox.Text;
            string mingchengqianzhui = 名称前缀textBox.Text;
            string shiyongip = ((DataRowView)使用IPcheckedListBox.SelectedItem).Row["Text"].ToString();
            string qidongchaoshishijian = 超时时间numericUpDown.Value.ToString();
            string client_disk_path = 默认目录textBox.Text;

            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);


                    string updateSql = "UPDATE SYSTEM_SETTING SET client_add_mode = '" + jiarufangshi + "',client_name_prefix='" + mingchengqianzhui + "',server_ip='" + shiyongip + "',client_boot_delay='" + qidongchaoshishijian + "',client_disk_path='" + client_disk_path + "';";
                    sh.ExecuteScalar(updateSql);
                    this.Close();
                }
            }
        }

        private void 使用IPcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (使用IPcheckedListBox.CheckedItems.Count > 0)
            {
                for (int i = 0; i < 使用IPcheckedListBox.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        使用IPcheckedListBox.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            默认目录textBox.Text =  path.SelectedPath;
        }
    }
}
