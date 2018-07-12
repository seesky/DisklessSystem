using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace Win2012WMIiSCSIVhdxManager
{
    public class DiskManagementData
    {
        
        
        public string Username { get; set; }      //服务器用户名

        public string Password { get; set; }      //服务器密码
        public string serverURL { get; set; }      //服务器地址
        public ushort? VhdxType { get; set; }       //虚拟磁盘的类型
        public string DevicePath { get; set; }      //虚拟磁盘路径
        public string ParentPath { get; set; }      //母盘（黄金镜像）路径
        public ushort? DiskSize { get; set; }         //磁盘大小
        public string TargetName { get; set; }      //Target名字  
        public string TargetIQN { get; set; }       //Target的IQN



        //public ushort? PartitionStyle { get; set; } //卷类型'1':'MBR';'2':'GPT';
        //public ulong? PartitionSize { get; set; }   //卷大小
        //public bool? PartitionUseMaxSize { get; set; } //是否使用最大空间设置卷大小
        //public string PartitionDriveLetter { get; set; } //盘符
        //public bool? PartitionAssignDriveLetter { get; set; } //是否设置盘符
        //public ushort? PartitionMbrType { get; set; }//文件系统格式类型
        //public bool? PartitionIsActive { get; set; } //是否为活动状态
    }

    /// <summary>
    /// 方法执行结果（包括错误类型）
    /// </summary>
    public enum ReturnCode
    {
        [Description("方法执行正确！")]
        RC_Completed,
        [Description("参数错误！")]
        RC_ParamIllegal,
        [Description("创建虚拟磁盘错误！")]
        RC_NewVirtualDisk,
        [Description("创建Target错误！")]
        RC_NewTarget,
        [Description("设置Target IQN错误！")]
        RC_SetTargetIQN,
        [Description("创建发起器错误！")]
        RC_NewInitiatorIds,
        [Description("添加磁盘与Target的关联关系错误！")]
        RC_AddDiskTargetMapping,
        [Description("装载虚拟磁盘错误！")]
        RC_AttachVirtualDisk,
        [Description("获得磁盘序号错误！")]
        RC_GetDiskImageNumber,
        [Description("获得磁盘错误！")]
        RC_GetDisk,
        [Description("初始化磁盘错误！")]
        RC_InitializeDisk,
        [Description("创建磁盘分区错误！")]
        RC_NewPartition,
        [Description("格式化磁盘分区错误！")]
        RC_FormatVolume,
        [Description("分离虚拟磁盘错误！")]
        RC_DetachDisk,
        [Description("获得Target错误！")]
        RC_GetTarget,
        [Description("获得虚拟磁盘错误！")]
        RC_GetVirtualDisk,
        [Description("删除磁盘与Target的关联关系错误！")]
        RC_DeleteDiskTargetMapping,
        [Description("删除磁盘发起器错误！")]
        RC_DeleteInitiatorIds,
        [Description("删除Target错误！")]
        RC_DeleteTarget,
        [Description("删除虚拟磁盘错误！")]
        RC_DeleteVirtualDisk,
        [Description("删除虚拟磁盘文件错误！")]
        RC_DeleteVirtualDiskFile,
        [Description("虚拟磁盘文件已经存在！")]
        RC_VirtualDiskFileIsExist,
        [Description("虚拟磁盘文件不存在！")]
        RC_VirtualDiskFileNoExist,
        [Description("虚拟磁盘已经存在！")]
        RC_DiskIsExist,
        [Description("Target已经存在！")]
        RC_TargetIsExist,
        [Description("母盘文件不存在！")]
        RC_ParentVhdxNoExist,
        [Description("导入虚拟磁盘失败！")]
        RC_ImportVhdxFaild
    }

    public static class Extension
    {
        /// <summary>
        /// get enum description by ReturnCode
        /// </summary>
        /// <returns></returns>
        public static string GetEnumDescription(ReturnCode value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }

    public class DiskManager
    {

        public static bool isLocalhost = false; //是否是在服务器上直接运行


        

        /// <summary>
        /// CreateDisk 创建虚拟磁盘
        /// </summary>
        /// <returns></returns>
        public static bool CreateVhdxDisk(DiskManagementData ParamObj)
        {
            uint WTD = 0;
            ManagementObject TargetObj = null;

            //System.Console.WriteLine(ParamObj.VhdxType + "\n" + ParamObj.DevicePath + "\n" + ParamObj.DiskSize + "\n" + ParamObj.ParentPath + "\n" + ParamObj.TargetName + "\n" + ParamObj.TargetIQN);

            if (ParamObj == null
                || ParamObj.VhdxType == null || ParamObj.VhdxType < 2 || ParamObj.VhdxType > 4
                || string.IsNullOrEmpty(ParamObj.DevicePath)
                || ParamObj.DiskSize == null
                || string.IsNullOrEmpty(ParamObj.ParentPath)
                || string.IsNullOrEmpty(ParamObj.TargetName)
                || string.IsNullOrEmpty(ParamObj.TargetIQN)
                )
            {
                System.Console.WriteLine("1:" +ParamObj.VhdxType + "\n" + ParamObj.DevicePath + "\n" + ParamObj.DiskSize + "\n" + ParamObj.ParentPath + "\n" + ParamObj.TargetName + "\n" + ParamObj.TargetIQN);
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ParamIllegal));
            }

            try
            {

                if (!NewVirtualDisk((ushort)ParamObj.VhdxType, @ParamObj.DevicePath, @ParamObj.ParentPath, (ushort)ParamObj.DiskSize, ref WTD, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewVirtualDisk));
                }

                if (!NewTarget(@ParamObj.TargetName, ref TargetObj, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewTarget));
                }

                if (!SetTargetIQN(@ParamObj.TargetIQN, ref TargetObj))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_SetTargetIQN));
                }

                if (!NewInitiatorIds(@ParamObj.Username, @ParamObj.Password, @ParamObj.TargetName, @ParamObj.serverURL))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewInitiatorIds));
                }

                if (!AddDiskTargetMapping(WTD, ref TargetObj))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_AddDiskTargetMapping));
                }

                if (TargetObj != null)
                {
                    TargetObj.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }


        /// <summary>
        /// ImportDisk 导入虚拟磁盘
        /// </summary>
        /// <returns></returns>

        public static bool ImportVhdxDisk(DiskManagementData ParamObj)
        {
            uint WTD = 0;
            ManagementObject TargetObj = null;

            if (ParamObj == null
                || string.IsNullOrEmpty(ParamObj.DevicePath)
                )
            {
                System.Console.WriteLine("2:" + ParamObj.VhdxType + "\n" + ParamObj.DevicePath + "\n" + ParamObj.DiskSize + "\n" + ParamObj.ParentPath + "\n" + ParamObj.TargetName + "\n" + ParamObj.TargetIQN);
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ParamIllegal));
            }

            try
            {
                /**
                if (!NewVirtualDisk((ushort)ParamObj.VhdxType, @ParamObj.DevicePath, @ParamObj.ParentPath, (ushort)ParamObj.DiskSize, ref WTD))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewVirtualDisk));
                }
                */
                if (!ImportVirtualDisk(@ParamObj.DevicePath, ref WTD, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ImportVhdxFaild));
                }

                if (!NewTarget(@ParamObj.TargetName, ref TargetObj, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewTarget));
                }

               
                if (!SetTargetIQN(@ParamObj.TargetIQN, ref TargetObj))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_SetTargetIQN));
                }
                

                if (!NewInitiatorIds(@ParamObj.Username, @ParamObj.Password, @ParamObj.TargetName, @ParamObj.serverURL))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewInitiatorIds));
                }

                if (!AddDiskTargetMapping(WTD, ref TargetObj))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_AddDiskTargetMapping));
                }

                if (TargetObj != null)
                {
                    TargetObj.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        /// <summary>
        /// FormatDisk 格式化磁盘
        /// </summary>
        /// <returns></returns>
        public static bool FormatDisk(DiskManagementData ParamObj)
        {
            uint DiskNumber = 0;
            ManagementObject DiskObj = null;
            string OutDriveLetter = null;

            if (ParamObj == null
                || string.IsNullOrEmpty(ParamObj.DevicePath)
                //|| ParamObj.PartitionStyle == null || ParamObj.PartitionStyle < 1 || ParamObj.PartitionStyle > 2
                //|| ParamObj.PartitionSize == null
                //|| ParamObj.PartitionUseMaxSize == null
                //|| string.IsNullOrEmpty(ParamObj.PartitionDriveLetter)
                //|| ParamObj.PartitionAssignDriveLetter == null
                //|| ParamObj.PartitionMbrType == null || ParamObj.PartitionMbrType < 1
                //|| ParamObj.PartitionIsActive == null
                )
            {
                System.Console.WriteLine("3:" + ParamObj.VhdxType + "\n" + ParamObj.DevicePath + "\n" + ParamObj.DiskSize + "\n" + ParamObj.ParentPath + "\n" + ParamObj.TargetName + "\n" + ParamObj.TargetIQN);
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ParamIllegal));
            }
            try
            {
                if (!IsFileExist(@ParamObj.DevicePath))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_VirtualDiskFileNoExist) + ParamObj.DevicePath);
                }

                if (!AttachVirtualDisk(@ParamObj.DevicePath, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_AttachVirtualDisk));
                }

                if (!GetDiskImageNumber(@ParamObj.DevicePath, ref DiskNumber, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_GetDiskImageNumber));
                }

                if (!GetDisk(DiskNumber, ref DiskObj, @ParamObj.serverURL))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_GetDisk));
                }

                if (!InitializeDisk(ref DiskObj))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_InitializeDisk));
                }

                if (!NewPartition(ref DiskObj, ref OutDriveLetter))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewPartition));
                }

                if (!FormatVolume(@ParamObj.serverURL,OutDriveLetter))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_FormatVolume));
                }

                if (!DetachDisk(@ParamObj.DevicePath, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DetachDisk));
                }

                if (DiskObj != null)
                {
                    DiskObj.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        /// <summary>
        /// RemoveDisk 移除虚拟磁盘
        /// </summary>
        /// <returns></returns>
        public static bool RemoveDisk(DiskManagementData ParamObj)
        {
            ManagementObject TargetObj = null;
            ManagementObject DiskObj = null;

            if (ParamObj == null
                || string.IsNullOrEmpty(ParamObj.DevicePath)
                || string.IsNullOrEmpty(ParamObj.TargetName)
                )
            {
                System.Console.WriteLine("4:" + ParamObj.VhdxType + "\n" + ParamObj.DevicePath + "\n" + ParamObj.DiskSize + "\n" + ParamObj.ParentPath + "\n" + ParamObj.TargetName + "\n" + ParamObj.TargetIQN);
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ParamIllegal));
            }
            try
            {
                if (!GetTarget(ParamObj.TargetName, ref TargetObj, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_GetTarget));
                }
                else
                {

                    if (!GetVirtualDisk(@ParamObj.DevicePath, ref DiskObj, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                    {
                        throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_GetDisk));
                    }
                    else
                    {
                        if (!DeleteDiskTargetMapping(ref TargetObj, ref DiskObj))
                        {
                            throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DeleteDiskTargetMapping));
                        }
                    }





                    if (!DeleteAllInitiatorIds(@ParamObj.TargetName, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                    {
                        throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_DeleteInitiatorIds));
                    }

                    if (!DeleteTarget(ref TargetObj))
                    {
                        throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_DeleteTarget));
                    }
                }

                if (!GetVirtualDisk(@ParamObj.DevicePath, ref DiskObj, @ParamObj.serverURL, @ParamObj.Username, @ParamObj.Password))
                {
                    throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_GetDisk));
                }
                else
                {
                    if (!DeleteVirtualDisk(ref DiskObj))
                    {
                        throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_DeleteVirtualDisk));
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////////
                /**
                if (!DeleteVirtualDiskFile(@ParamObj.DevicePath))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DeleteVirtualDiskFile));
                }
                */
                /////////////////////////////////////////////////////////////////////////////////////
                if (TargetObj != null)
                {
                    TargetObj.Dispose();
                }

                if (DiskObj != null)
                {
                    DiskObj.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }


        /// <summary>
        /// 导入虚拟磁盘
        /// </summary>
        /// <param name="DevicePath">虚拟磁盘路径</param>
        /// <param name="WTD">[Out]iSCSI Disk Index</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// https://msdn.microsoft.com/en-us/library/dn469763(v=vs.85).aspx
        private static bool ImportVirtualDisk(string DevicePath,  ref uint WTD, string ServerURL, string Username, string Password)
        {
            try
            {
                string NamespacePath =  ServerURL + "\\ROOT\\WMI"; 
                string ClassName = "WT_Disk";

                ManagementScope scope = null;
                ConnectionOptions options = null;
                options = new ConnectionOptions();
                if (!isLocalhost)
                {
                    options.Username = Username;
                    options.Password = Password;
                }
                
                scope = new ManagementScope(ServerURL + "\\ROOT\\WMI", options);

                string strWtd = "";

                ManagementClass DiskClassObj = new ManagementClass(NamespacePath, ClassName, null);
                DiskClassObj.Scope = scope;
                ManagementBaseObject MethodParamsObj = DiskClassObj.GetMethodParameters("ImportWTDisk");
                MethodParamsObj["DevicePath"] = DevicePath;
                MethodParamsObj["Description"] = System.IO.Path.GetFileNameWithoutExtension(DevicePath); ;
                MethodParamsObj["ResourceGroup"] = @"";

                ManagementBaseObject OutParamsObj = DiskClassObj.InvokeMethod("ImportWTDisk", MethodParamsObj, null);
                ManagementBaseObject ResultObj = (OutParamsObj["returnValue"] as ManagementBaseObject);
                strWtd = ResultObj["WTD"].ToString();
                WTD = uint.Parse(strWtd);


                if (OutParamsObj != null)
                {
                    OutParamsObj.Dispose();
                }

                if (MethodParamsObj != null)
                {
                    OutParamsObj.Dispose();
                }

                if (DiskClassObj != null)
                {
                    DiskClassObj.Dispose();
                }
            }
            catch (System.Exception ex) {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ImportVhdxFaild) + ex.Message);
            }
            return true;
        }


        /// <summary>
        /// 创建虚拟磁盘
        /// </summary>
        /// <param name="VhdxType">虚拟磁盘类型 2:Fixed';3:'Dynamic';4:'Differencing</param>
        /// <param name="DevicePath">虚拟磁盘路径</param>
        /// <param name="ParentPath">母盘（黄金镜像）虚拟磁盘</param>
        /// <param name="DiskSize">虚拟磁盘大小</param>
        /// <param name="WTD">[Out]iSCSI Disk Index</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_disk/#createvhdwtdisk_methods
        private static bool NewVirtualDisk(ushort VhdxType, string DevicePath, string ParentPath, ushort DiskSize, ref uint WTD, string ServerURL,string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\WMI";
            string ClassName = "WT_Disk";

            ManagementScope scope = null;

            ConnectionOptions options = null;
            options = new ConnectionOptions();
            if (!isLocalhost)
            {
                options.Username = Username;
                options.Password = Password;
            }
            scope = new ManagementScope(ServerURL + "\\ROOT\\WMI", options);

            string strWtd = "";

            //uint maxSize = (uint)DiskSize * 1024 * 1024 * 1024;
            UInt64 maxSize = (UInt64)DiskSize * 1024 * 1024 * 1024;
            try
            {
                if (!IsCreateVhdxDiskArgumentsValid(VhdxType, DevicePath, ParentPath,ServerURL, Username, Password))
                {
                    throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DiskIsExist));
                }

                ManagementClass DiskClassObj = new ManagementClass(NamespacePath, ClassName, null);
                DiskClassObj.Scope = scope;
                
                
                ManagementBaseObject MethodParamsObj = DiskClassObj.GetMethodParameters("CreateVhdWTDisk");
                switch (VhdxType)
                {
                    case 2: //固定盘
                        MethodParamsObj["VhdType"] = 2;
                        MethodParamsObj["MaxInternalSize"] = maxSize;//1 G = 1073741824 B
                        MethodParamsObj["ParentPath"] = @"";
                        MethodParamsObj["LogicalSectorSize"] = 512;
                        MethodParamsObj["PhysicalSectorSize"] = 512;
                        break;
                    case 3: //动态盘
                        MethodParamsObj["VhdType"] = 3;
                        MethodParamsObj["MaxInternalSize"] = maxSize;//1 G = 1073741824 B
                        MethodParamsObj["ParentPath"] = @"";
                        MethodParamsObj["LogicalSectorSize"] = 512;
                        MethodParamsObj["PhysicalSectorSize"] = 512;
                        break;
                    case 4: //差异盘,快照 【测试通过，且大小为4M/127G】
                        MethodParamsObj["VhdType"] = 4;
                        MethodParamsObj["ParentPath"] = ParentPath;
                        break;
                }
                MethodParamsObj["DevicePath"] = DevicePath;
                MethodParamsObj["ClearData"] = true;
                ManagementBaseObject OutParamsObj = DiskClassObj.InvokeMethod("CreateVhdWTDisk", MethodParamsObj, null);
                ManagementBaseObject ResultObj = (OutParamsObj["returnValue"] as ManagementBaseObject);
                strWtd = ResultObj["WTD"].ToString();
                WTD = uint.Parse(strWtd);

                if (OutParamsObj != null)
                {
                    OutParamsObj.Dispose();
                }

                if (MethodParamsObj != null)
                {
                    OutParamsObj.Dispose();
                }

                if (DiskClassObj != null)
                {
                    DiskClassObj.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("5:" + ServerURL + "\n");
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ParamIllegal) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 创建iSCSI Target
        /// </summary>
        /// <param name="TargetName">Target friendly name</param>
        /// <param name="TargetObj">[out]Target Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_host/#newhost_methods
        private static bool NewTarget(string TargetName, ref ManagementObject TargetObj, string ServerURL, string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\WMI";
            string ClassName = "WT_Host";
            string ObjectName = ".HostName='";

            ManagementScope scope = null;

            ConnectionOptions options = null;
            options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Packet;
            options.Timeout = new TimeSpan(0, 0, 30);
            options.EnablePrivileges = true;
            if (!isLocalhost)
            {
                options.Username = Username;
                options.Password = Password;
            }
            //options.Authority = "ntlmdomain:DOMAIN";
            scope = new ManagementScope(ServerURL + "\\ROOT\\WMI", options);


            try
            {
                if (GetTarget(TargetName, ref TargetObj, ServerURL, Username, Password))
                {
                    //throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_TargetIsExist));
                    System.Console.WriteLine("iscsi已存在\n");
                    return true;
                }

                ManagementClass WTHostClass = new ManagementClass(NamespacePath, ClassName, null);
                WTHostClass.Scope = scope;
                ManagementBaseObject NewHostParams = WTHostClass.GetMethodParameters("NewHost");
                NewHostParams["HostName"] = TargetName;
                WTHostClass.InvokeMethod("NewHost", NewHostParams, null);

                if (NewHostParams != null)
                {
                    NewHostParams.Dispose();
                }

                TargetObj = new ManagementObject(NamespacePath + ":" + ClassName + ObjectName + TargetName + "'");
                System.Console.WriteLine("创建TargetObj成功\n");


                if (WTHostClass != null)
                {
                    WTHostClass.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewTarget) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 设置Target IQN
        /// </summary>
        /// <param name="TargetIQN">Target IQN</param>
        /// <param name="TargetObj">[out]Target Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_host/#targetiqn_properties
        private static bool SetTargetIQN(string TargetIQN, ref ManagementObject TargetObj)
        {
            try
            {
                if (TargetObj == null)
                {
                    return false;
                }
                System.Console.WriteLine("TargetIQN:" + TargetIQN + "\n");
                TargetObj.SetPropertyValue("TargetIQN", TargetIQN);
                System.Console.WriteLine("TargetIQN设置成功，但还没有更新" + "\n");
                TargetObj.Put();
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_SetTargetIQN) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// function that associates a target with an initiator
        /// </summary>
        /// <param name="TargetName">Target Host Name</param>
        /// <param name="InitiatorMethod">Identification Method</param>
        /// <param name="InitiatorMethodValue">Identification Value</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_idmethod
        private static bool NewInitiatorIds(string Username, string Password, string TargetName, string ServerURL, ushort Method = 4, string Value = "*")
        {
            string NamespacePath = ServerURL + "\\ROOT\\WMI";
            string ClassName = "WT_IDMethod";
            ManagementScope scope = null;

            ConnectionOptions options = null;
            options = new ConnectionOptions();
            if (!isLocalhost)
            {
                options.Username = Username;
                options.Password = Password;
            }
            scope = new ManagementScope(ServerURL + "\\ROOT\\WMI", options);


            try
            {
                ManagementClass WTIDMethodClass = new ManagementClass(NamespacePath, ClassName, null);
                WTIDMethodClass.Scope = scope;
                ManagementObject IDMethodObj = WTIDMethodClass.CreateInstance();
                IDMethodObj["HostName"] = TargetName;
                IDMethodObj["Method"] = Method;
                IDMethodObj["Value"] = Value;
                IDMethodObj.Put();

                if (IDMethodObj != null)
                {
                    IDMethodObj.Dispose();
                }
                if (WTIDMethodClass != null)
                {
                    WTIDMethodClass.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewInitiatorIds) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// Assign iSCSI Disk To Target
        /// </summary>
        /// <param name="WTD">'iSCSI Disk Index</param>
        /// <param name="TargetObj">[out]Target Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_host/#addwtdisk_methods
        private static bool AddDiskTargetMapping(uint WTD, ref ManagementObject TargetObj)
        {
            try
            {
                if (TargetObj == null)
                {
                    return false;
                }

                ManagementBaseObject AddWTDiskParams = TargetObj.GetMethodParameters("AddWTDisk");
                AddWTDiskParams["WTD"] = WTD;
                TargetObj.InvokeMethod("AddWTDisk", AddWTDiskParams, null);

                if (AddWTDiskParams != null)
                {
                    AddWTDiskParams.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_AddDiskTargetMapping) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// Attaches a virtual disk image file in loopback mode
        /// </summary>
        /// <param name="DevicePath">Full path to VHD file</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/virtualization/v2/msvm_imagemanagementservice/#attachvirtualharddisk_methods
        private static bool AttachVirtualDisk(string DevicePath, string ServerURL, string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\virtualization\\v2";
            string ClassName = "Msvm_ImageManagementService";
            ManagementScope scope = null;

            ConnectionOptions options = null;
            options = new ConnectionOptions();
            if (!isLocalhost)
            {
                options.Username = Username;
                options.Password = Password;
            }
            scope = new ManagementScope(ServerURL + "\\ROOT\\virtualization\\v2", options);


            try
            {
                //http://www.msdn.microsoft.com/en-us/library/Hb850023
                ManagementClass serviceClass = new ManagementClass(NamespacePath + ":" + ClassName);
                serviceClass.Scope = scope;
                ManagementObjectCollection services = serviceClass.GetInstances();
                ManagementObject imageService = null;
                foreach (ManagementObject serviceObject in services)
                {
                    imageService = serviceObject;
                }
                if (imageService == null)
                {
                    return false;
                }
                ManagementBaseObject AttachParams = imageService.GetMethodParameters("AttachVirtualHardDisk");
                AttachParams["Path"] = DevicePath;
                AttachParams["AssignDriveLetter"] = true;
                AttachParams["ReadOnly"] = false;
                ManagementBaseObject ReturnParams = imageService.InvokeMethod("AttachVirtualHardDisk", AttachParams, null);
                //ReturnCode.Started = 4096来自网页 http://www.msdn.microsoft.com/en-us/library/cc723869(v=vs.85).aspx 
                if ((uint)ReturnParams["ReturnValue"] != 4096)
                {
                    return false;
                }
                ReturnParams.Dispose();
                AttachParams.Dispose();
                imageService.Dispose();
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_AttachVirtualDisk) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 获得虚拟磁盘的Number
        /// </summary>
        /// <param name="DevicePath">Full path to VHD file</param>
        /// <param name="DiskNumber">[out]system's number for the disk</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/microsoft/windows/storage/msft_diskimage
        private static bool GetDiskImageNumber(string DevicePath, ref uint DiskNumber, string ServerURL, string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\Microsoft\\Windows\\Storage";
            string ClassName = "MSFT_DiskImage";
            ManagementScope scope = null;

            ConnectionOptions options = null;
            options = new ConnectionOptions();
            if (!isLocalhost)
            {
                options.Username = Username;
                options.Password = Password;
            }
            scope = new ManagementScope(ServerURL + "\\ROOT\\Microsoft\\Windows\\Storage", options);

            try
            {
                //http://www.msdn.microsoft.com/en-us/library/Hb850023
                ManagementClass DiskImageClass = new ManagementClass(NamespacePath + ":" + ClassName);
                ManagementObject DiskImageObj = DiskImageClass.CreateInstance();
                DiskImageObj.SetPropertyValue("ImagePath", DevicePath);
                DiskImageObj.SetPropertyValue("StorageType", 3);
                DiskImageObj.Get();
                string StrDiskNumber = DiskImageObj.GetPropertyValue("Number").ToString();
                if (string.IsNullOrEmpty(StrDiskNumber))
                {
                    return false;
                }
                DiskNumber = uint.Parse(StrDiskNumber);
                if (DiskImageObj != null)
                {
                    DiskImageObj.Dispose();
                }
                if (DiskImageClass != null)
                {
                    DiskImageClass.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_GetDiskImageNumber) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 获取磁盘
        /// </summary>
        /// <param name="DiskNumber">The operating system's number for the disk</param>
        /// <param name="DiskObj">[out]Disk Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/microsoft/windows/storage/msft_disk
        public static bool GetDisk(uint DiskNumber, ref ManagementObject DiskObj, string ServerURL)
        {
            string NamespacePath = ServerURL + "\\ROOT\\Microsoft\\Windows\\Storage";
            string ClassName = "MSFT_Disk";
            string ObjectName = "Number";



            try
            {
                string t = string.Format("SELECT * FROM {0} where {1} = '{2}'", ClassName, ObjectName, DiskNumber);

                ManagementScope scope = new ManagementScope(NamespacePath);
                ObjectQuery query = new ObjectQuery(t);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    DiskObj = m;
                }

                if (DiskObj == null)
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_GetDisk) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// This method initializes a RAW disk with a particular partition style
        /// </summary>
        /// <param name="PartitionStyle">初始化磁盘 1:'MBR';'2':'GPT';</param>
        /// <param name="DiskObj">Disk Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/microsoft/windows/storage/msft_disk/#initialize_methods
        public static bool InitializeDisk(ref ManagementObject DiskObj, ushort PartitionStyle = 1)
        {
            try
            {
                if (DiskObj == null)
                {
                    return false;
                }

                //得到DiskInitialize方法的参数集
                ManagementBaseObject DiskInitializeParams = DiskObj.GetMethodParameters("Initialize");
                //设置DiskInitialize方法的参数集
                DiskInitializeParams["PartitionStyle"] = PartitionStyle;
                //反射执行DiskInitialize方法
                DiskObj.InvokeMethod("Initialize", DiskInitializeParams, null);

                //释放DiskInitialize方法的参数集
                if (DiskInitializeParams != null)
                {
                    DiskInitializeParams.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_InitializeDisk) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 创建分区
        /// </summary>
        /// <param name="MbrType">Specifies the MBR partition type</param>
        /// <param name="IsActive">设置活动分区</param>
        /// <param name="DriveLetter">[out]盘符</param>
        /// <param name="DiskObj">[out] Disk Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/microsoft/windows/storage/msft_disk/#createpartition_methods
        public static bool NewPartition(
            ref ManagementObject DiskObj,
            ref string OutDriveLetter,
            ulong Size = 1024,
            bool UseMaxSize = true,
            string DriveLetter = "",
            bool AssignDriveLetter = true,
            ushort MbrType = 12,
            bool IsActive = true
            )
        {
            try
            {
                if (DiskObj == null)
                {
                    return false;
                }

                //得到CreatePartition方法的参数集
                ManagementBaseObject CreatePartitionParams = DiskObj.GetMethodParameters("CreatePartition");
                //设置CreatePartition方法的参数集
                CreatePartitionParams["UseMaximumSize"] = UseMaxSize;
                CreatePartitionParams["AssignDriveLetter"] = AssignDriveLetter;
                CreatePartitionParams["MbrType"] = MbrType;
                CreatePartitionParams["IsActive"] = IsActive;
                //反射执行CreatePartition方法
                ManagementBaseObject OutParamsObj = DiskObj.InvokeMethod("CreatePartition", CreatePartitionParams, null);
                ManagementBaseObject PartitionObj = (OutParamsObj["CreatedPartition"] as ManagementBaseObject);
                OutDriveLetter = PartitionObj["DriveLetter"].ToString() + ":";
                //释放CreatePartition方法的参数集
                if (CreatePartitionParams != null)
                {
                    CreatePartitionParams.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_NewPartition) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 格式化卷
        /// </summary>
        /// <param name="DriveLetter">盘符</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/cimv2/win32_volume/#format_methods
        public static bool FormatVolume(
            string ServerURL,
            string DriveLetter,
            string fileSystem = "NTFS",
            bool quickFormat = true,
            int clusterSize = 8192,
            string label = "",
            bool enableCompression = false
            
            )
        {
            string NamespacePath = ServerURL + "\\ROOT\\Cimv2";
            string ClassName = "Win32_Volume";
            string ObjectName = "DriveLetter";
            try
            {
                if (DriveLetter.Length != 2 || DriveLetter[1] != ':' || !char.IsLetter(DriveLetter[0]))
                {
                    return false;
                }
                string DataValuesTemp = DriveLetter.Replace(@"\", @"\\");
                string t = string.Format("SELECT * FROM {0} where {1} = '{2}'", ClassName, ObjectName, DataValuesTemp);

                ManagementScope scope = new ManagementScope(NamespacePath);
                ObjectQuery query = new ObjectQuery(t);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();
                foreach (ManagementObject vi in searcher.Get())
                {
                    vi.InvokeMethod("Format", new object[] { fileSystem, quickFormat, clusterSize, label, enableCompression });
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_FormatVolume) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 分离磁盘
        /// </summary>
        /// <param name="DevicePath">Full path to VHD file</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/virtualization/v2/msvm_mountedstorageimage/#detachvirtualharddisk_methods
        private static bool DetachDisk(string DevicePath, string ServerURL, string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\virtualization\\v2";
            string ClassName = "Msvm_MountedStorageImage";


            ManagementScope scope = null;

            ConnectionOptions options = null;
            options = new ConnectionOptions();
            if (!isLocalhost)
            {
                options.Username = Username;
                options.Password = Password;
            }
            scope = new ManagementScope(ServerURL + "\\ROOT\\virtualization\\v2", options);

            try
            {
                //http://www.msdn.microsoft.com/en-us/library/Hb850046
                ManagementClass serviceClass = new ManagementClass(NamespacePath + ":" + ClassName);
                serviceClass.Scope = scope;
                using (ManagementObjectCollection services = serviceClass.GetInstances())
                {
                    ManagementObject image = null;
                    foreach (ManagementObject serviceObject in services)
                    {
                        image = serviceObject;
                    }
                    if (image == null)
                    {
                        return false;
                    }
                    using (image)
                    {
                        string name = image.GetPropertyValue("Name").ToString();
                        if (string.Equals(name, DevicePath, StringComparison.OrdinalIgnoreCase))
                        {
                            ManagementBaseObject ReturnParams = image.InvokeMethod("DetachVirtualHardDisk", null, null);
                            if ((uint)ReturnParams["ReturnValue"] != 0)
                            {
                                return false;
                            }
                            ReturnParams.Dispose();
                        }
                        image.Dispose();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DetachDisk) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 获取iISCSI Target
        /// </summary>
        /// <param name="TargetName">Target friendly name</param>
        /// <param name="TargetObj">Target Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_host
        private static bool GetTarget(string TargetName, ref ManagementObject TargetObj, string ServerURL, string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\WMI";
            string ClassName = "WT_Host";
            string ObjectName = ".HostName='";


            ManagementScope scope = null;

            ConnectionOptions options = null;
            options = new ConnectionOptions();
            if (!isLocalhost)
            {
                options.Username = Username;
                options.Password = Password;
            }
            
            scope = new ManagementScope(ServerURL +  "\\ROOT\\WMI", options);

            try
            {
                //创建WT_Host查询对象
                TargetObj = new ManagementObject(NamespacePath + ":" + ClassName + ObjectName + TargetName + "'");
                TargetObj.Scope = scope;
                //如果对象未创建这里会出现异常，用来判断HostName是否存在
                TargetObj.GetPropertyValue("HostName");

            }
            catch (System.Exception ex)
            {
                //throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_GetTarget) + ex.Message);
                string msg = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取虚拟磁盘
        /// </summary>
        /// <param name="DevicePath">Full path to VHD file</param>
        /// <param name="DiskObj">Disk Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_disk
        public static bool GetVirtualDisk(string DevicePath, ref ManagementObject DiskObj, string ServerURL, string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\WMI";
            string ClassName = "WT_Disk";
            string ObjectName = "DevicePath";

            try
            {
                string DataValuesTemp = DevicePath.Replace(@"\", @"\\");
                string t = string.Format("SELECT * FROM {0} where {1} = '{2}'", ClassName, ObjectName, DataValuesTemp);


                ConnectionOptions options = null;
                options = new ConnectionOptions();
                if (!isLocalhost)
                {
                    options.Username = Username;
                    options.Password = Password;
                }

                ManagementScope scope = new ManagementScope(NamespacePath, options);
                scope.Connect();
                ObjectQuery query = new ObjectQuery(t);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    DiskObj = m;
                }
                if (DiskObj == null)
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                //throw new Exception(Extension.GetEnumDescription( ReturnCode.RC_GetDiskImageNumber) + ex.Message);
                string msg = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 删除虚拟磁盘与Target的关联关系
        /// </summary>
        /// <param name="TargetObj">Target Object</param>
        /// <param name="DiskObj">Disk Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_host/#removewtdisk_methods
        private static bool DeleteDiskTargetMapping(ref ManagementObject TargetObj, ref ManagementObject DiskObj)
        {
            if (DiskObj == null || TargetObj == null)
            {
                return false;
            }

            try
            {
                //得到AddWTDisk方法的参数集
                ManagementBaseObject RemoveWTDiskParams = TargetObj.GetMethodParameters("RemoveWTDisk");
                //设置AddWTDisk方法d参数集
                RemoveWTDiskParams["WTD"] = uint.Parse(DiskObj.GetPropertyValue("WTD").ToString()); ;
                //反射执行AddWTDisk方法
                TargetObj.InvokeMethod("RemoveWTDisk", RemoveWTDiskParams, null);

                //释放AddWTDisk方法的参数集
                if (RemoveWTDiskParams != null)
                {
                    RemoveWTDiskParams.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DeleteDiskTargetMapping) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 删除磁盘发起器
        /// </summary>
        /// <param name="TargetName">Target friendly name</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_idmethod
        private static bool DeleteAllInitiatorIds(string TargetName, string ServerURL, string Username, string Password)
        {
            string NamespacePath = ServerURL + "\\ROOT\\WMI";
            string ClassName = "WT_IDMethod";
            string ObjectName = "HostName";

            if (TargetName.Length <= 0)
            {
                return false;
            }

            try
            {
                ConnectionOptions options = null;
                options = new ConnectionOptions();
                if (!isLocalhost)
                {
                    options.Username = Username;
                    options.Password = Password;
                }



                string t = string.Format("SELECT * FROM {0} where {1} = '{2}'", ClassName, ObjectName, TargetName);
                bool IsSearch = false;
                ManagementScope scope = new ManagementScope(NamespacePath, options);
                scope.Connect();
                ObjectQuery query = new ObjectQuery(t);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    m.Delete();
                    IsSearch = true;
                }
                if (IsSearch)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DeleteInitiatorIds) + ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 删除iISCSI Target
        /// </summary>
        /// <param name="TargetObj">Target Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_host
        private static bool DeleteTarget(ref ManagementObject TargetObj)
        {
            try
            {
                if (TargetObj == null)
                {
                    return false;
                }
                TargetObj.Delete();
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DeleteTarget) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 删除虚拟磁盘
        /// </summary>
        /// <param name="DiskObj">Disk Object</param>
        /// <returns>执行成功返回true，否则false</returns>
        /// http://wutils.com/wmi/root/wmi/wt_disk
        private static bool DeleteVirtualDisk(ref ManagementObject DiskObj)
        {
            try
            {
                if (DiskObj == null)
                {
                    return false;
                }
                DiskObj.Delete();
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DeleteVirtualDisk) + ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 删除虚拟磁盘文件
        /// </summary>
        /// <param name="DevicePath">Full path to VHD file</param>
        /// <returns>执行成功返回true，否则false</returns>
        private static bool DeleteVirtualDiskFile(string DevicePath)
        {
            try
            {
                if (IsFileExist(DevicePath))
                {
                    File.Delete(DevicePath);
                }
                else
                {
                    //return false;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DeleteVirtualDiskFile) + ex.Message);
            }
            return true;
        }

        private static bool IsCreateVhdxDiskArgumentsValid(ushort VhdxType, string DevicePath, string ParentPath, string ServerURL, string Username, string Password)
        {
            ManagementObject DiskObj = null;

            if (IsFileExist(DevicePath))
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_VirtualDiskFileIsExist));
            }

            if (GetVirtualDisk(DevicePath, ref DiskObj, ServerURL, Username, Password))
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_DiskIsExist));
            }

            if (DiskObj != null)
            {
                DiskObj.Dispose();
            }

            if (VhdxType == 4 && !IsFileExist(ParentPath))
            {
                throw new Exception(Extension.GetEnumDescription(ReturnCode.RC_ParentVhdxNoExist));
            }
            return true;
        }

        private static bool IsFileExist(string DevicePath)
        {
            return File.Exists(DevicePath);
        }
    }
}
