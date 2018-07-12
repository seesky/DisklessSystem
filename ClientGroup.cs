// File:    ClientGroup.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class ClientGroup

using System;

/// 工作站分组表
public class ClientGroup
{
   /// 工作站分组唯一ID
    private string clientGroupId;

    public string ClientGroupId
    {
        get { return clientGroupId; }
        set { clientGroupId = value; }
    }
   /// 工作站分组名称
    private string clientGroupName;

    public string ClientGroupName
    {
        get { return clientGroupName; }
        set { clientGroupName = value; }
    }
   
   public System.Collections.Generic.List<ClientGroupList> clientGroupList;
   
   /// <summary>
   /// Property for collection of ClientGroupList
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<ClientGroupList> ClientGroupList
   {
      get
      {
         if (clientGroupList == null)
            clientGroupList = new System.Collections.Generic.List<ClientGroupList>();
         return clientGroupList;
      }
      set
      {
         RemoveAllClientGroupList();
         if (value != null)
         {
            foreach (ClientGroupList oClientGroupList in value)
               AddClientGroupList(oClientGroupList);
         }
      }
   }
   
   /// <summary>
   /// Add a new ClientGroupList in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddClientGroupList(ClientGroupList newClientGroupList)
   {
      if (newClientGroupList == null)
         return;
      if (this.clientGroupList == null)
         this.clientGroupList = new System.Collections.Generic.List<ClientGroupList>();
      if (!this.clientGroupList.Contains(newClientGroupList))
      {
         this.clientGroupList.Add(newClientGroupList);
         newClientGroupList.ClientGroup = this;
      }
   }
   
   /// <summary>
   /// Remove an existing ClientGroupList from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveClientGroupList(ClientGroupList oldClientGroupList)
   {
      if (oldClientGroupList == null)
         return;
      if (this.clientGroupList != null)
         if (this.clientGroupList.Contains(oldClientGroupList))
         {
            this.clientGroupList.Remove(oldClientGroupList);
            oldClientGroupList.ClientGroup = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of ClientGroupList from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllClientGroupList()
   {
      if (clientGroupList != null)
      {
         System.Collections.ArrayList tmpClientGroupList = new System.Collections.ArrayList();
         foreach (ClientGroupList oldClientGroupList in clientGroupList)
            tmpClientGroupList.Add(oldClientGroupList);
         clientGroupList.Clear();
         foreach (ClientGroupList oldClientGroupList in tmpClientGroupList)
            oldClientGroupList.ClientGroup = null;
         tmpClientGroupList.Clear();
      }
   }
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
         newClient.ClientGroup = this;
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
            oldClient.ClientGroup = null;
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
            oldClient.ClientGroup = null;
         tmpClient.Clear();
      }
   }

}