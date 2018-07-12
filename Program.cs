using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoDiskSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Login loginWindow = new Login();
            loginWindow.StartPosition = FormStartPosition.CenterParent;
            loginWindow.ShowDialog();
            if (loginWindow.DialogResult == DialogResult.OK)
            {
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            else
            {

                return;
            }
        }
    }
}
