// File:    VdiskRestorePoint.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class VdiskRestorePoint

using System;

/// 磁盘还原点
public class VdiskRestorePoint
{
   /// 磁盘还原点唯一ID
    private string vdiskRestorePointId;

    public string VdiskRestorePointId
    {
        get { return vdiskRestorePointId; }
        set { vdiskRestorePointId = value; }
    }
   /// 磁盘还原点创建时间
    private string vdiskRestorePointCreateTime;

    public string VdiskRestorePointCreateTime
    {
        get { return vdiskRestorePointCreateTime; }
        set { vdiskRestorePointCreateTime = value; }
    }
   /// 磁盘还原点名称
    private string vdiskResotrePointName;

    public string VdiskResotrePointName
    {
        get { return vdiskResotrePointName; }
        set { vdiskResotrePointName = value; }
    }
   /// 磁盘还原点描述
    private string vdiskRestorePointDescription;

    public string VdiskRestorePointDescription
    {
        get { return vdiskRestorePointDescription; }
        set { vdiskRestorePointDescription = value; }
    }
   /// 磁盘还原点文件位置
    private string vdiskRestorePointPath;

    public string VdiskRestorePointPath
    {
        get { return vdiskRestorePointPath; }
        set { vdiskRestorePointPath = value; }
    }
   /// 磁盘还原点创建排序码
    private float vdiskRestorePointSort;

    public float VdiskRestorePointSort
    {
        get { return vdiskRestorePointSort; }
        set { vdiskRestorePointSort = value; }
    }

    private VdiskTemplate vdiskTemplet;

    public VdiskTemplate VdiskTemplet1
    {
        get { return vdiskTemplet; }
        set { vdiskTemplet = value; }
    }
   
   /// <summary>
   /// Property for VdiskTemplet
   /// </summary>
   /// <pdGenerated>Default opposite class property</pdGenerated>
   public VdiskTemplate VdiskTemplet
   {
      get
      {
         return vdiskTemplet;
      }
      set
      {
         if (this.vdiskTemplet == null || !this.vdiskTemplet.Equals(value))
         {
            if (this.vdiskTemplet != null)
            {
               VdiskTemplate oldVdiskTemplet = this.vdiskTemplet;
               this.vdiskTemplet = null;
               oldVdiskTemplet.RemoveVdiskRestorePoint(this);
            }
            if (value != null)
            {
               this.vdiskTemplet = value;
               this.vdiskTemplet.AddVdiskRestorePoint(this);
            }
         }
      }
   }

}