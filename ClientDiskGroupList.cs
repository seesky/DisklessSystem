// File:    ClientDiskGroupList.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class ClientDiskGroupList

using System;

/// 工作站磁盘组列表
public class ClientDiskGroupList
{
   public int clientDiskGroupListId;
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
         newClient.ClientDiskGroupList = this;
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
            oldClient.ClientDiskGroupList = null;
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
            oldClient.ClientDiskGroupList = null;
         tmpClient.Clear();
      }
   }
   public VdiskGroupList vdiskGroupList;
   
   /// <summary>
   /// Property for VdiskGroupList
   /// </summary>
   /// <pdGenerated>Default opposite class property</pdGenerated>
   public VdiskGroupList VdiskGroupList
   {
      get
      {
         return vdiskGroupList;
      }
      set
      {
         if (this.vdiskGroupList == null || !this.vdiskGroupList.Equals(value))
         {
            if (this.vdiskGroupList != null)
            {
               VdiskGroupList oldVdiskGroupList = this.vdiskGroupList;
               this.vdiskGroupList = null;
               oldVdiskGroupList.RemoveClientDiskGroupList(this);
            }
            if (value != null)
            {
               this.vdiskGroupList = value;
               this.vdiskGroupList.AddClientDiskGroupList(this);
            }
         }
      }
   }

}