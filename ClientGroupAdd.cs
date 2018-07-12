using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace NoDiskSystem
{
    public delegate void DelegateUpdateTree(TreeNode node);
    public partial class ClientGroupAdd : Form
    {
        public event DelegateUpdateTree updateTree;
        MainWindow parentWindows;
        public ClientGroupAdd(MainWindow parent)
        {
            this.parentWindows = parent;
            InitializeComponent();
        }

        private void 确定button_Click(object sender, EventArgs e)
        {
            if(组名textBox.Text == ""){
                MessageBox.Show("组名不能为空！");
            }else{
                string client_group_name = 组名textBox.Text.Trim();

                using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        string sql = "select count(*) from CLIENT_GROUP where client_group_name = '" + client_group_name + "';";
                        
                        int count = int.Parse(sh.ExecuteScalar(sql).ToString());

                        if (count > 0)
                        {
                            MessageBox.Show("已存在 " + client_group_name + " 分组");
                            conn.Close();
                            return;
                        }
                        else { 

                            var dic = new Dictionary<string, object>();
                            dic["client_group_name"] = client_group_name;
                            dic["client_group_id"] = System.Guid.NewGuid().ToString("N");;
                            sh.Insert("CLIENT_GROUP", dic);

                            TreeNode tempNode = new TreeNode(client_group_name, IconIndexes.computer, IconIndexes.computer);
                            tempNode.Tag = client_group_name;
                            tempNode.Text = client_group_name;

                            //TreeView parentTreeView = (TreeView)parentWindows.Controls.Find("分组treeView", false)[0];
                            //parentTreeView.Nodes.Add(tempNode);
                            UpdateTree(tempNode);
                            this.Close();
                        }

                        conn.Close();
                    }
                }

            }
            
        }

        private void UpdateTree(TreeNode node)
        {
            updateTree(node);
        }

        private void 取消button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClientGroupAdd_Load(object sender, EventArgs e)
        {

        }
    }

   
}
