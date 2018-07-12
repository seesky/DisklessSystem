// File:    VdiskGroup.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class VdiskGroup

using System;

/// 磁盘组表
public class VdiskGroup
{
   /// 磁盘组唯一ID
    private string diskGroupId;

    public string DiskGroupId
    {
        get { return diskGroupId; }
        set { diskGroupId = value; }
    }
   /// 磁盘组名称
    private string diskGroupName;

    public string DiskGroupName
    {
        get { return diskGroupName; }
        set { diskGroupName = value; }
    }
   /// 磁盘组描述
    private string diskGroupDescription;

    public string DiskGroupDescription
    {
        get { return diskGroupDescription; }
        set { diskGroupDescription = value; }
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
         newVdiskGroupList.VdiskGroup = this;
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
            oldVdiskGroupList.VdiskGroup = null;
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
            oldVdiskGroupList.VdiskGroup = null;
         tmpVdiskGroupList.Clear();
      }
   }

}