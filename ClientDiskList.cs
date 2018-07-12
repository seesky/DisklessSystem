// File:    ClientDiskList.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class ClientDiskList

using System;

/// 工作站磁盘列表
public class ClientDiskList
{
   public int clientDiskListId;
   /// 磁盘排序码
   public int diskSort;
   
   public System.Collections.Generic.List<Client> client;
   
   /// <summary>
   /// Property for collection of Client
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Client> Client
   {
      get
      {
         if (client == null)
            client = new System.Collections.Generic.List<Client>();
         return client;
      }
      set
      {
         RemoveAllClient();
         if (value != null)
         {
            foreach (Client oClient in value)
               AddClient(oClient);
         }
      }
   }
   
   /// <summary>
   /// Add a new Client in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddClient(Client newClient)
   {
      if (newClient == null)
         return;
      if (this.client == null)
         this.client = new System.Collections.Generic.List<Client>();
      if (!this.client.Contains(newClient))
      {
         this.client.Add(newClient);
         newClient.ClientDiskList = this;
      }
   }
   
   /// <summary>
   /// Remove an existing Client from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveClient(Client oldClient)
   {
      if (oldClient == null)
         return;
      if (this.client != null)
         if (this.client.Contains(oldClient))
         {
            this.client.Remove(oldClient);
            oldClient.ClientDiskList = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of Client from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllClient()
   {
      if (client != null)
      {
         System.Collections.ArrayList tmpClient = new System.Collections.ArrayList();
         foreach (Client oldClient in client)
            tmpClient.Add(oldClient);
         client.Clear();
         foreach (Client oldClient in tmpClient)
            oldClient.ClientDiskList = null;
         tmpClient.Clear();
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
               oldVdiskTemplet.RemoveClientDiskList(this);
            }
            if (value != null)
            {
               this.vdiskTemplet = value;
               this.vdiskTemplet.AddClientDiskList(this);
            }
         }
      }
   }

}