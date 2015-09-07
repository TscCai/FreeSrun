using FreeSrun.Forms;
using System;
using System.Threading;
using System.Windows.Forms;



namespace FreeSrun
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1 && (args[0] == "-help" || args[0] == "-h" || args[0] == "-?"))
            {
                MessageBox.Show(Configuration.HelpInfo);
            }
            else if (args.Length > 0 && !Configuration.CheckParam(args))
            {
                MessageBox.Show("Incorrect parameters\n" + Configuration.HelpInfo);
            }
#if !DEBUG
            bool newMutex;
            Mutex m = new System.Threading.Mutex(true, @"Global\" + Application.ProductName, out newMutex);
            {
                if (newMutex)
                {
#endif
                    Configuration config= null;
                    Form frmMain = null;
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    if (args.Length > 0 && Configuration.CheckParam(args))
                    {
                        config = Configuration.Configure(args);
                        frmMain = new Frm_Main(config);
                    }
                    else
                    {
                        frmMain = new Frm_Main();

                    }
                    
                    Application.Run(frmMain);
#if !DEBUG
                }
                else
                {
                    MessageBox.Show("程序已运行。");
                }
            }
#endif
        }

    }

}