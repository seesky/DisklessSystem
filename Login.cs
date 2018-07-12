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
using System.Security.Cryptography;

namespace NoDiskSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string admin_password = null;
            //登陆验证
            using (SQLiteConnection conn = new SQLiteConnection("data source=nodisk.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    DataTable dt = sh.Select("select admin_password from SYSTEM_SETTING;");

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            admin_password = row[column].ToString();
                        }
                    }


                    conn.Close();
                }
            }

            byte[] passwordResult = Encoding.Default.GetBytes(TextBoxPassword.Text.Trim());
            
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(passwordResult);
            string inputPassword = BitConverter.ToString(output).Replace("-", "");

            if (inputPassword == admin_password.ToUpper())
            {

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("账号或密码错误！");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
