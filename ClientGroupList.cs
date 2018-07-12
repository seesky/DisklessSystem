// File:    ClientGroupList.cs
// Author:  Administrator
// Created: 2018年5月31日 10:52:47
// Purpose: Definition of Class ClientGroupList

using System;

/// 工作站分组列表
public class ClientGroupList
{
   /// 工作站分组列表ID
   public int clientGroupListId;
   
   public Client client;
   
   /// <summary>
   /// Property for Client
   /// </summary>
   /// <pdGenerated>Default opposite class property</pdGenerated>
   public Client Client
   {
      get
      {
         return client;
      }
      set
      {
         if (this.client == null || !this.client.Equals(value))
         {
            if (this.client != null)
            {
               Client oldClient = this.client;
               this.client = null;
               oldClient.RemoveClientGroupList(this);
            }
            if (value != null)
            {
               this.client = value;
               this.client.AddClientGroupList(this);
            }
         }
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
               oldClientGroup.RemoveClientGroupList(this);
            }
            if (value != null)
            {
               this.clientGroup = value;
               this.clientGroup.AddClientGroupList(this);
            }
         }
      }
   }

}