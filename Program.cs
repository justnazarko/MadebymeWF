using System;
using System.Linq;
using MadebymeWF.Data;
using MadebymeWF.Models;
using System.Windows.Forms;
namespace MadebymeWF
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoginForm());
        }

    }
}
