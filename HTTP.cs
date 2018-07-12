using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NHttp;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;

namespace NoDiskSystem
{
    class HTTP
    {
        public void start()
        {

            string server_ip = "";
            string client_disk_path = "";
            bool ipIsAvalible = false;
            string tempIp = "";
            
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
                        
                        server_ip = row["server_ip"].ToString();
                        client_disk_path = row["client_disk_path"].ToString();
                    }
                    conn.Close();


                    
                    string HostName = Dns.GetHostName();
                    IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                    for (int i = 0; i < IpEntry.AddressList.Length; i++)
                    {
                        //从IP地址列表中筛选出IPv4类型的IP地址
                        //AddressFamily.InterNetwork表示此IP为IPv4,
                        //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                        if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            if (IpEntry.AddressList[i].ToString() == server_ip)
                            {
                                ipIsAvalible = true;
                            }
                            else
                            {
                                tempIp = IpEntry.AddressList[i].ToString();
                            }

                        }
                    }


                }
            }

            using(var server = new HttpServer())
            {
                server.RequestReceived += (s, e) =>
                {
                    using(var writer = new StreamWriter(e.Response.OutputStream))
                    {
                        //char[] TrimChar = {':'};
                        string mac = e.Request.Params.Get(0);
                        mac = mac.Replace(":","").ToUpper();

                        using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand())
                            {
                                cmd.Connection = conn;
                                conn.Open();

                                SQLiteHelper sh = new SQLiteHelper(cmd);


                                

                                DataTable dt = sh.Select("select * from CLIENT where client_mac = '" + mac + "';");

                                if (dt.Rows.Count == 0)
                                {
                                    string client_id = System.Guid.NewGuid().ToString("N");
                                    string client_name = "HSTECS-" + mac;
                                    string client_mac = mac;
                                    string client_work_path = client_disk_path;
                                    string client_description = mac;
                                    string client_group_id = "未分组";
                                    string client_enable = "0";

                                    //1、磁盘第一次登录自动登记并设置为未启用
                                    string addClientString = "insert into CLIENT (client_id,client_name,client_mac,client_work_path,client_description,client_group_id,client_enable,client_super_enable) values ('" + client_id + "','" + client_name + "','" + client_mac + "','" + client_work_path + "','" + client_description + "','" + client_group_id + "','" + client_enable + "','0');";

                                    sh.ExecuteScalar(addClientString);

                                    writer.Write("#!ipxe");
                                    writer.Write(System.Environment.NewLine);
                                    writer.Write("echo Computer HSTECS-" + mac + " add success!");
                                }
                                else {

                                    
                                    //2、已注册工作站登录
                                    foreach (DataRow row in dt.Rows)
                                    {


                                        string checkSuperString = "select count(*) from CLIENT where client_super_enable;";
                                        int isSuperEnable = int.Parse(sh.ExecuteScalar(checkSuperString).ToString());

                                        if (isSuperEnable > 0 && row["client_super_enable"].ToString() != "1")
                                        {
                                            writer.Write("#!ipxe");
                                            writer.Write(System.Environment.NewLine);
                                            writer.Write("echo Super Computer have been enable!Please disable super computer in system or power on the right computer!");
                                            return;
                                        }


                                        if (row["client_enable"].ToString() == "0")
                                        {
                                            writer.Write("#!ipxe");
                                            writer.Write(System.Environment.NewLine);
                                            writer.Write("echo Computer HSTECS-" + mac + " have been disable!");
                                        }
                                        else {
                                            if (row["client_super_enable"].ToString() == "1")
                                            {
                                                //超级工作站模式

                                                
                                                writer.Write("#!ipxe");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set menu-timeout 5000");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set submenu-timeout ${menu-timeout}");


                                                //系统启动菜单
                                                string getDiskList = "select * from CLIENT_DISK_LIST where client_disk_list_id = '" + row["client_disk_list_id"] + "';";
                                                DataTable dtDiskList = sh.Select(getDiskList);
                                                Dictionary<String,String> tempDiskNameList = new Dictionary<String,String>();
                                                foreach (DataRow rowDiskList in dtDiskList.Rows)
                                                {
                                                    string disk_id = rowDiskList["disk_id"].ToString();
                                                    string getDiskName = "select * from VDISK_TEMPLET where disk_id = '" + disk_id + "';";
                                                    DataTable dtDisk = sh.Select(getDiskName);


                                                    if (dtDisk.Rows.Count > 0)
                                                    {
                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("set menu-default " + dtDisk.Rows[0]["disk_name"].ToString());
                                                    }
                                                }


                                                //writer.Write(System.Environment.NewLine);
                                                //writer.Write("isset ${menu-default} || set menu-default exit");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("###################### MAIN MENU ####################################");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":start");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("menu Hstecs PXE boot menu ");



                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item --gap --             ------------------------- Operating systems ------------------------------");


                                                //系统启动菜单
                                                getDiskList = "select * from CLIENT_DISK_LIST where client_disk_list_id = '" + row["client_disk_list_id"] + "';";
                                                dtDiskList = sh.Select(getDiskList);
                                                tempDiskNameList = new Dictionary<String, String>();
                                                foreach (DataRow rowDiskList in dtDiskList.Rows)
                                                {
                                                    string disk_id = rowDiskList["disk_id"].ToString();
                                                    string getDiskName = "select * from VDISK_TEMPLET where disk_id = '" + disk_id + "';";
                                                    DataTable dtDisk = sh.Select(getDiskName);
                                                    foreach (DataRow rowDisk in dtDisk.Rows)
                                                    {

                                                        List<VdiskRestorePoint> tempResotorePointList = new List<VdiskRestorePoint>();
                                                        DataTable rsDiskRestorePoint = sh.Select("select * from VDISK_RESTORE_POINT where disk_id = '" + disk_id + "';");
                                                        foreach (DataRow rows in rsDiskRestorePoint.Rows)
                                                        {
                                                            VdiskRestorePoint tempPoint = new VdiskRestorePoint();
                                                            tempPoint.VdiskResotrePointName = rows["vdisk_resotre_point_name"].ToString();
                                                            tempPoint.VdiskRestorePointSort = float.Parse(rows["vdisk_restore_point_sort"].ToString());

                                                            tempResotorePointList.Add(tempPoint);
                                                        }

                                                        //查询最新的还原点
                                                        VdiskRestorePoint tempPoints = new VdiskRestorePoint();
                                                        float tempPointSort = 0;

                                                        foreach (VdiskRestorePoint p in tempResotorePointList)
                                                        {
                                                            if (p.VdiskRestorePointSort > tempPointSort)
                                                            {
                                                                tempPointSort = p.VdiskRestorePointSort;
                                                            }
                                                        }

                                                        foreach (VdiskRestorePoint p in tempResotorePointList)
                                                        {
                                                            if (p.VdiskRestorePointSort == tempPointSort)
                                                            {
                                                                tempPoints = p;
                                                            }
                                                        }


                                                        string vdisk_restore_point_id = tempPoints.VdiskRestorePointId;
                                                        string vdisk_resotre_point_name = tempPoints.VdiskResotrePointName;
                                                        


                                                        //



                                                        string disk_name = rowDisk["disk_name"].ToString();

                                                        tempDiskNameList.Add(disk_name, vdisk_resotre_point_name);
                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("item  " + disk_name + "      Boot " + disk_name + " super disk from iSCSI");

                                                    }
                                                }


                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item --gap --             ------------------------- Advanced options -------------------------------");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item shell                Drop to iPXE shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item reboot               Reboot computer");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item --key x exit         Exit iPXE and continue BIOS boot");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("choose --timeout ${menu-timeout} --default ${menu-default} selected || goto cancel");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set menu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto ${selected}");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":cancel");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("echo You cancelled the menu, dropping you to a shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("echo Type 'exit' to get the back to the menu");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set menu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set submenu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto start");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":failed");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("echo Booting failed, dropping to shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":reboot");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("reboot");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":exit");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("exit");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":back");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set submenu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("clear submenu-default");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto start");


                                                Dictionary<string, string>.ValueCollection valueCol = tempDiskNameList.Values;



                                                foreach (KeyValuePair<string, string> kvp in tempDiskNameList)
                                                {
                                                    if (kvp.Value != null)
                                                    {
                                                        string tempMac = mac.ToLower();
                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write(":" + kvp.Value);

                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("echo Booting " + kvp.Value + " from iSCSI");

                                                        writer.Write(System.Environment.NewLine);

                                                        if (ipIsAvalible == true)
                                                        {

                                                            writer.Write("sanboot iscsi:" + server_ip + "::::hstecsclient." + kvp.Value + " || goto failed");
                                                        }
                                                        else
                                                        {
                                                            writer.Write("sanboot iscsi:" + tempIp + "::::hstecsclient." + kvp.Value + " || goto failed");
                                                        }


                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("goto start");
                                                    }
                                                    else {
                                                        string tempMac = mac.ToLower();
                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write(":" + kvp.Key);

                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("echo Booting " + kvp.Key + " from iSCSI");

                                                        writer.Write(System.Environment.NewLine);

                                                        if (ipIsAvalible == true)
                                                        {

                                                            writer.Write("sanboot iscsi:" + server_ip + "::::hstecsclient." + kvp.Key + " || goto failed");
                                                        }
                                                        else
                                                        {
                                                            writer.Write("sanboot iscsi:" + tempIp + "::::hstecsclient." + kvp.Key + " || goto failed");
                                                        }


                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("goto start");
                                                        
                                                    }
                                                }


                                                foreach (string name in valueCol)
                                                {

                                                    


                                                    
                                                }





                                            }
                                            else { 
                                                //普通工作站，创建普通工作站启动脚本
                                                writer.Write("#!ipxe");
                                                
                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set menu-timeout 5000");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set submenu-timeout ${menu-timeout}");


                                                //系统启动菜单
                                                string getDiskList = "select * from CLIENT_DISK_LIST where client_disk_list_id = '" + row["client_disk_list_id"] + "';";
                                                DataTable dtDiskList = sh.Select(getDiskList);
                                                List<String> tempDiskNameList = new List<String>();
                                                foreach (DataRow rowDiskList in dtDiskList.Rows)
                                                {
                                                    string disk_id = rowDiskList["disk_id"].ToString();
                                                    string getDiskName = "select * from VDISK_TEMPLET where disk_id = '" + disk_id + "';";
                                                    DataTable dtDisk = sh.Select(getDiskName);
                                                   

                                                    if (dtDisk.Rows.Count > 0)
                                                    {
                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("set menu-default " + dtDisk.Rows[0]["disk_name"].ToString());
                                                    }
                                                }


                                                //writer.Write(System.Environment.NewLine);
                                                //writer.Write("isset ${menu-default} || set menu-default exit");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("###################### MAIN MENU ####################################");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":start");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("menu Hstecs PXE boot menu ");

                                                

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item --gap --             ------------------------- Operating systems ------------------------------");


                                                //系统启动菜单
                                                getDiskList = "select * from CLIENT_DISK_LIST where client_disk_list_id = '" + row["client_disk_list_id"] + "';";
                                                dtDiskList = sh.Select(getDiskList);
                                                tempDiskNameList = new List<String>();
                                                foreach (DataRow rowDiskList in dtDiskList.Rows)
                                                {
                                                    string disk_id = rowDiskList["disk_id"].ToString();
                                                    string getDiskName = "select * from VDISK_TEMPLET where disk_id = '" + disk_id + "';";
                                                    DataTable dtDisk = sh.Select(getDiskName);
                                                    foreach (DataRow rowDisk in dtDisk.Rows)
                                                    {
                                                        string disk_name = rowDisk["disk_name"].ToString();
                                                        tempDiskNameList.Add(disk_name);
                                                        writer.Write(System.Environment.NewLine);
                                                        writer.Write("item  " + disk_name + "      Boot " + disk_name + " from iSCSI");

                                                    }
                                                }


                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item --gap --             ------------------------- Advanced options -------------------------------");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item shell                Drop to iPXE shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item reboot               Reboot computer");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("item --key x exit         Exit iPXE and continue BIOS boot");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("choose --timeout ${menu-timeout} --default ${menu-default} selected || goto cancel");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set menu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto ${selected}");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":cancel");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("echo You cancelled the menu, dropping you to a shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("echo Type 'exit' to get the back to the menu");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set menu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set submenu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto start");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":failed");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("echo Booting failed, dropping to shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto shell");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":reboot");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("reboot");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":exit");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("exit");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write(":back");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("set submenu-timeout 0");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("clear submenu-default");

                                                writer.Write(System.Environment.NewLine);
                                                writer.Write("goto start");

                                                foreach (string name in tempDiskNameList)
                                                {

                                                    string tempMac = mac.ToLower();
                                                    writer.Write(System.Environment.NewLine);
                                                    writer.Write(":" + name);

                                                    writer.Write(System.Environment.NewLine);
                                                    writer.Write("echo Booting "+ name +" from iSCSI");

                                                    writer.Write(System.Environment.NewLine);
                                                    if (ipIsAvalible == true)
                                                    {
                                                        writer.Write("sanboot iscsi:" + server_ip + "::::hstecsclient." + name + "-" + tempMac + " || goto failed");
                                                    }
                                                    else {
                                                        writer.Write("sanboot iscsi:" + tempIp + "::::hstecsclient." + name + "-" + tempMac + " || goto failed");
                                                    }
                                                    

                                                    writer.Write(System.Environment.NewLine);
                                                    writer.Write("goto start");
                                                }
                                            }
                                        }
                                    }


                                    /**
                                    //3、如果工作站中有机器被设置为超级工作站，则将超级工作站磁盘连接到对应的工作站上进行软件安装

                                    writer.Write("#!ipxe");
                                    writer.Write(System.Environment.NewLine);
                                    writer.Write(":loop");
                                    writer.Write(System.Environment.NewLine);
                                    writer.Write("echo Hello World");
                                    writer.Write(System.Environment.NewLine);
                                    writer.Write("goto loop");
                                     * */
                                }

                                
                                conn.Close();

                            }
                        }


                        





                        
                    }
                };



                if (ipIsAvalible == true)
                {
                    server.EndPoint = new IPEndPoint(IPAddress.Parse(server_ip), 80);
                }
                else {
                    server.EndPoint = new IPEndPoint(IPAddress.Parse(tempIp), 80);
                }
                
                server.Start();
                //Process.Start(String.Format("http://{0}/", server.EndPoint));
                while (true)
                {
                    Thread.Sleep(10000);
                }
            }

            
        }
    }
}
