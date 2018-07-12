// File:    Client.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class Client

using System;

/// 工作站表
public class Client
{
    private string clientId;

    public string ClientId
    {
        get { return clientId; }
        set { clientId = value; }
    }
    private string clientEnable;

    public string ClientEnable
    {
        get { return clientEnable; }
        set { clientEnable = value; }
    }
   private string clientName;

   public string ClientName
   {
       get { return clientName; }
       set { clientName = value; }
   }
   private string clientMac;

   public string ClientMac
   {
       get { return clientMac; }
       set { clientMac = value; }
   }
   private string clientIp;

   public string ClientIp
   {
       get { return clientIp; }
       set { clientIp = value; }
   }
   private string clientNetmask;

   public string ClientNetmask
   {
       get { return clientNetmask; }
       set { clientNetmask = value; }
   }
   private string clientGateway;

   public string ClientGateway
   {
       get { return clientGateway; }
       set { clientGateway = value; }
   }
   private string clientDns1;

   public string ClientDns1
   {
       get { return clientDns1; }
       set { clientDns1 = value; }
   }
   private string clientDns2;

   public string ClientDns2
   {
       get { return clientDns2; }
       set { clientDns2 = value; }
   }
   public string clientWorkPath;
   public string clientResolution;
   public string clientDescription;
   public int clientDiskType;
   /// 工作站工作服务器IP
   private string clientServer;

   public string ClientServer
   {
       get { return clientServer; }
       set { clientServer = value; }
   }
   /// 客户端本地缓存模式
   public int clientMemCacheMode;
   /// 客户端本地缓存大小
   public int clientMemCacheNum;
   /// 客户端本地缓存百分比
   public int clientMemCachePercent;
   /// 是否为参照工作站
   public int clientRefertoEnable;
   /// 参照工作站ID
   public int clientRefertoId;
   /// 网卡加速使能
   public int clientNetworkAccelerateEnable;
   /// 服务器热备使能
   public int clientServerHotStandby;
   /// 超级工作站使能
   private string clientSuperEnable;

   public string ClientSuperEnable
   {
       get { return clientSuperEnable; }
       set { clientSuperEnable = value; }
   }
   /// 客户端还原使能
   public int clientRestoreEnable;
   /// 工作站有状态盘使能
   public int clientPersonalDiskEnable;
   
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
         newClientGroupList.Client = this;
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
            oldClientGroupList.Client = null;
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
            oldClientGroupList.Client = null;
         tmpClientGroupList.Clear();
      }
   }
   public ClientGroup clientGroup;
   
   /// <summary>
   /// Property for ClientGroup
   /// </summary>
   /// <pdGenerated>Default opposite class property</pdGenerated>
   public ClientGroup ClientGroup
   {
      get
      {
         return clientGroup;
      }
      set
      {
         if (this.clientGroup == null || !this.clientGroup.Equals(value))
         {
            if (this.clientGroup != null)
            {
               ClientGroup oldClientGroup = this.clientGroup;
               this.clientGroup = null;
               oldClientGroup.RemoveClient(this);
            }
            if (value != null)
            {
               this.clientGroup = value;
               this.clientGroup.AddClient(this);
            }
         }
      }
   }
   public ClientDiskList clientDiskList;
   
   /// <summary>
   /// Property for ClientDiskList
   /// </summary>
   /// <pdGenerated>Default opposite class property</pdGenerated>
   public ClientDiskList ClientDiskList
   {
      get
      {
         return clientDiskList;
      }
      set
      {
         if (this.clientDiskList == null || !this.clientDiskList.Equals(value))
         {
            if (this.clientDiskList != null)
            {
               ClientDiskList oldClientDiskList = this.clientDiskList;
               this.clientDiskList = null;
               oldClientDiskList.RemoveClient(this);
            }
            if (value != null)
            {
               this.clientDiskList = value;
               this.clientDiskList.AddClient(this);
            }
         }
      }
   }
   public ClientDiskGroupList clientDiskGroupList;
   
   /// <summary>
   /// Property for ClientDiskGroupList
   /// </summary>
   /// <pdGenerated>Default opposite class property</pdGenerated>
   public ClientDiskGroupList ClientDiskGroupList
   {
      get
      {
         return clientDiskGroupList;
      }
      set
      {
         if (this.clientDiskGroupList == null || !this.clientDiskGroupList.Equals(value))
         {
            if (this.clientDiskGroupList != null)
            {
               ClientDiskGroupList oldClientDiskGroupList = this.clientDiskGroupList;
               this.clientDiskGroupList = null;
               oldClientDiskGroupList.RemoveClient(this);
            }
            if (value != null)
            {
               this.clientDiskGroupList = value;
               this.clientDiskGroupList.AddClient(this);
            }
         }
      }
   }

}