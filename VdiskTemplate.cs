// File:    VdiskTemplet.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class VdiskTemplet

using System;

/// 模板磁盘镜像文件表
public class VdiskTemplate
{
   /// 磁盘唯一ID
    private string diskId;

    public string DiskId
    {
        get { return diskId; }
        set { diskId = value; }
    }
   /// 磁盘名称
    private string diskName;

    public string DiskName
    {
        get { return diskName; }
        set { diskName = value; }
    }
   /// 磁盘大小
    private int diskSize;

    public int DiskSize
    {
        get { return diskSize; }
        set { diskSize = value; }
    }
   /// 磁盘文件
    private string diskPath;

    public string DiskPath
    {
        get { return diskPath; }
        set { diskPath = value; }
    }
   /// 磁盘文件类型
   private int diskType;

   public int DiskType
   {
       get { return diskType; }
       set { diskType = value; }
   }
   
   public System.Collections.Generic.List<VdiskGroupList> vdiskGroupList;
   
   /// <summary>
   /// Property for collection of VdiskGroupList
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<VdiskGroupList> VdiskGroupList
   {
      get
      {
         if (vdiskGroupList == null)
            vdiskGroupList = new System.Collections.Generic.List<VdiskGroupList>();
         return vdiskGroupList;
      }
      set
      {
         RemoveAllVdiskGroupList();
         if (value != null)
         {
            foreach (VdiskGroupList oVdiskGroupList in value)
               AddVdiskGroupList(oVdiskGroupList);
         }
      }
   }
   
   /// <summary>
   /// Add a new VdiskGroupList in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddVdiskGroupList(VdiskGroupList newVdiskGroupList)
   {
      if (newVdiskGroupList == null)
         return;
      if (this.vdiskGroupList == null)
         this.vdiskGroupList = new System.Collections.Generic.List<VdiskGroupList>();
      if (!this.vdiskGroupList.Contains(newVdiskGroupList))
      {
         this.vdiskGroupList.Add(newVdiskGroupList);
         newVdiskGroupList.VdiskTemplet = this;
      }
   }
   
   /// <summary>
   /// Remove an existing VdiskGroupList from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveVdiskGroupList(VdiskGroupList oldVdiskGroupList)
   {
      if (oldVdiskGroupList == null)
         return;
      if (this.vdiskGroupList != null)
         if (this.vdiskGroupList.Contains(oldVdiskGroupList))
         {
            this.vdiskGroupList.Remove(oldVdiskGroupList);
            oldVdiskGroupList.VdiskTemplet = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of VdiskGroupList from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllVdiskGroupList()
   {
      if (vdiskGroupList != null)
      {
         System.Collections.ArrayList tmpVdiskGroupList = new System.Collections.ArrayList();
         foreach (VdiskGroupList oldVdiskGroupList in vdiskGroupList)
            tmpVdiskGroupList.Add(oldVdiskGroupList);
         vdiskGroupList.Clear();
         foreach (VdiskGroupList oldVdiskGroupList in tmpVdiskGroupList)
            oldVdiskGroupList.VdiskTemplet = null;
         tmpVdiskGroupList.Clear();
      }
   }
   public System.Collections.Generic.List<ClientDiskList> clientDiskList;
   
   /// <summary>
   /// Property for collection of ClientDiskList
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<ClientDiskList> ClientDiskList
   {
      get
      {
         if (clientDiskList == null)
            clientDiskList = new System.Collections.Generic.List<ClientDiskList>();
         return clientDiskList;
      }
      set
      {
         RemoveAllClientDiskList();
         if (value != null)
         {
            foreach (ClientDiskList oClientDiskList in value)
               AddClientDiskList(oClientDiskList);
         }
      }
   }
   
   /// <summary>
   /// Add a new ClientDiskList in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddClientDiskList(ClientDiskList newClientDiskList)
   {
      if (newClientDiskList == null)
         return;
      if (this.clientDiskList == null)
         this.clientDiskList = new System.Collections.Generic.List<ClientDiskList>();
      if (!this.clientDiskList.Contains(newClientDiskList))
      {
         this.clientDiskList.Add(newClientDiskList);
         newClientDiskList.VdiskTemplet = this;
      }
   }
   
   /// <summary>
   /// Remove an existing ClientDiskList from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveClientDiskList(ClientDiskList oldClientDiskList)
   {
      if (oldClientDiskList == null)
         return;
      if (this.clientDiskList != null)
         if (this.clientDiskList.Contains(oldClientDiskList))
         {
            this.clientDiskList.Remove(oldClientDiskList);
            oldClientDiskList.VdiskTemplet = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of ClientDiskList from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllClientDiskList()
   {
      if (clientDiskList != null)
      {
         System.Collections.ArrayList tmpClientDiskList = new System.Collections.ArrayList();
         foreach (ClientDiskList oldClientDiskList in clientDiskList)
            tmpClientDiskList.Add(oldClientDiskList);
         clientDiskList.Clear();
         foreach (ClientDiskList oldClientDiskList in tmpClientDiskList)
            oldClientDiskList.VdiskTemplet = null;
         tmpClientDiskList.Clear();
      }
   }
   public System.Collections.Generic.List<VdiskRestorePoint> vdiskRestorePoint;
   
   /// <summary>
   /// Property for collection of VdiskRestorePoint
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<VdiskRestorePoint> VdiskRestorePoint
   {
      get
      {
         if (vdiskRestorePoint == null)
            vdiskRestorePoint = new System.Collections.Generic.List<VdiskRestorePoint>();
         return vdiskRestorePoint;
      }
      set
      {
         RemoveAllVdiskRestorePoint();
         if (value != null)
         {
            foreach (VdiskRestorePoint oVdiskRestorePoint in value)
               AddVdiskRestorePoint(oVdiskRestorePoint);
         }
      }
   }
   
   /// <summary>
   /// Add a new VdiskRestorePoint in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddVdiskRestorePoint(VdiskRestorePoint newVdiskRestorePoint)
   {
      if (newVdiskRestorePoint == null)
         return;
      if (this.vdiskRestorePoint == null)
         this.vdiskRestorePoint = new System.Collections.Generic.List<VdiskRestorePoint>();
      if (!this.vdiskRestorePoint.Contains(newVdiskRestorePoint))
      {
         this.vdiskRestorePoint.Add(newVdiskRestorePoint);
         newVdiskRestorePoint.VdiskTemplet = this;
      }
   }
   
   /// <summary>
   /// Remove an existing VdiskRestorePoint from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveVdiskRestorePoint(VdiskRestorePoint oldVdiskRestorePoint)
   {
      if (oldVdiskRestorePoint == null)
         return;
      if (this.vdiskRestorePoint != null)
         if (this.vdiskRestorePoint.Contains(oldVdiskRestorePoint))
         {
            this.vdiskRestorePoint.Remove(oldVdiskRestorePoint);
            oldVdiskRestorePoint.VdiskTemplet = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of VdiskRestorePoint from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllVdiskRestorePoint()
   {
      if (vdiskRestorePoint != null)
      {
         System.Collections.ArrayList tmpVdiskRestorePoint = new System.Collections.ArrayList();
         foreach (VdiskRestorePoint oldVdiskRestorePoint in vdiskRestorePoint)
            tmpVdiskRestorePoint.Add(oldVdiskRestorePoint);
         vdiskRestorePoint.Clear();
         foreach (VdiskRestorePoint oldVdiskRestorePoint in tmpVdiskRestorePoint)
            oldVdiskRestorePoint.VdiskTemplet = null;
         tmpVdiskRestorePoint.Clear();
      }
   }

}