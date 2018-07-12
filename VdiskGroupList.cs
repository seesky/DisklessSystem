// File:    VdiskGroupList.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class VdiskGroupList

using System;

/// 磁盘组磁盘列表
public class VdiskGroupList
{
   public int diskListId;
   /// 磁盘排序码
   public int diskSort;
   
   public System.Collections.Generic.List<ClientDiskGroupList> clientDiskGroupList;
   
   /// <summary>
   /// Property for collection of ClientDiskGroupList
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<ClientDiskGroupList> ClientDiskGroupList
   {
      get
      {
         if (clientDiskGroupList == null)
            clientDiskGroupList = new System.Collections.Generic.List<ClientDiskGroupList>();
         return clientDiskGroupList;
      }
      set
      {
         RemoveAllClientDiskGroupList();
         if (value != null)
         {
            foreach (ClientDiskGroupList oClientDiskGroupList in value)
               AddClientDiskGroupList(oClientDiskGroupList);
         }
      }
   }
   
   /// <summary>
   /// Add a new ClientDiskGroupList in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddClientDiskGroupList(ClientDiskGroupList newClientDiskGroupList)
   {
      if (newClientDiskGroupList == null)
         return;
      if (this.clientDiskGroupList == null)
         this.clientDiskGroupList = new System.Collections.Generic.List<ClientDiskGroupList>();
      if (!this.clientDiskGroupList.Contains(newClientDiskGroupList))
      {
         this.clientDiskGroupList.Add(newClientDiskGroupList);
         newClientDiskGroupList.VdiskGroupList = this;
      }
   }
   
   /// <summary>
   /// Remove an existing ClientDiskGroupList from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveClientDiskGroupList(ClientDiskGroupList oldClientDiskGroupList)
   {
      if (oldClientDiskGroupList == null)
         return;
      if (this.clientDiskGroupList != null)
         if (this.clientDiskGroupList.Contains(oldClientDiskGroupList))
         {
            this.clientDiskGroupList.Remove(oldClientDiskGroupList);
            oldClientDiskGroupList.VdiskGroupList = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of ClientDiskGroupList from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllClientDiskGroupList()
   {
      if (clientDiskGroupList != null)
      {
         System.Collections.ArrayList tmpClientDiskGroupList = new System.Collections.ArrayList();
         foreach (ClientDiskGroupList oldClientDiskGroupList in clientDiskGroupList)
            tmpClientDiskGroupList.Add(oldClientDiskGroupList);
         clientDiskGroupList.Clear();
         foreach (ClientDiskGroupList oldClientDiskGroupList in tmpClientDiskGroupList)
            oldClientDiskGroupList.VdiskGroupList = null;
         tmpClientDiskGroupList.Clear();
      }
   }
   public VdiskTemplate vdiskTemplet;
   
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
               oldVdiskTemplet.RemoveVdiskGroupList(this);
            }
            if (value != null)
            {
               this.vdiskTemplet = value;
               this.vdiskTemplet.AddVdiskGroupList(this);
            }
         }
      }
   }
   public VdiskGroup vdiskGroup;
   
   /// <summary>
   /// Property for VdiskGroup
   /// </summary>
   /// <pdGenerated>Default opposite class property</pdGenerated>
   public VdiskGroup VdiskGroup
   {
      get
      {
         return vdiskGroup;
      }
      set
      {
         if (this.vdiskGroup == null || !this.vdiskGroup.Equals(value))
         {
            if (this.vdiskGroup != null)
            {
               VdiskGroup oldVdiskGroup = this.vdiskGroup;
               this.vdiskGroup = null;
               oldVdiskGroup.RemoveVdiskGroupList(this);
            }
            if (value != null)
            {
               this.vdiskGroup = value;
               this.vdiskGroup.AddVdiskGroupList(this);
            }
         }
      }
   }

}