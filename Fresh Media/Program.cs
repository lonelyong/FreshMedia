using System.Threading;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
namespace FreshMedia
{
    #region enum
    enum StartMode : int { Command = 1, Usual = 2 }
    #endregion

    static class Program
    {
        #region public fileds
        /// <summary>
        /// 本程序的全局唯一标识符
        /// </summary>
        public static string AppGuid { get; } = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();

        /// <summary>
        /// 官方网站
        /// </summary>
        public static string WEBSITE { get; } = $"http://freshmedia.netgrace.cn/";

        /// <summary>
        /// 可以在存储在XML文件中的程序名，通常是去除XML非法字符（空格等）
        /// </summary>
        public static string XmlAppName { get; } = NgNet.Applications.Current.AssemblyProduct.Replace(" ", null);

        /// <summary>
        /// 命令行参数中的音乐数
        /// </summary>
        public static List<string> Audios { get; private set; }
        /// <summary>
        /// 是否有命令行参数
        /// </summary>
        public static bool HasCommandArgs
        {
            get
            {
                return Audios.Count > 0;
            }
        }

        public static Controller.MainController Controller { get; private set; }
        #endregion

        #region main
        [STAThread]
        private static void Main(string[] args)
        {
            RunArgs(args);
            var mutexName = string.Format("Global\\{0}", AppGuid);
            var createdNew = false;
            using (var mutex = new Mutex(true, mutexName, out createdNew))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (createdNew)
                {
                    //Process.GetCurrentProcess().PriorityBoostEnabled = true;
                    //Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
                    Application.ApplicationExit += new EventHandler((Object sender, EventArgs e) => { });
                    Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
                    Controller = new Controller.MainController();
                    Controller.Init();
                    Application.Run(Controller.MainForm);     
                }
                else
                {
                    var process = RunningInstance();
                    if (process == null)
                        MessageBox.Show(null, "程序已经运行", Application.ProductName);
                    else
                        HandleRunningInstance(process);
                    return;
                }
            }
        }
        #endregion

        #region constructor destructor
        static Program()
        {
            Audios = new List<string>();
        }
        #endregion

        #region private methods
        private static void RunArgs(string[] args)
        {
            if (args == null)
                return;
            foreach (string item in args)
            {
                if (NgNet.IO.PathHelper.IsPath(item))
                    if (Player.Types.SupportedTypes.Contains(System.IO.Path.GetExtension(item)))
                    {
                        Audios.Add(item);
                    }
            }
        }
        #endregion

        #region Running Instance
        /// <summary>
        /// 获取正在运行的进程实例
        /// </summary>
        /// <returns></returns>
        private static Process RunningInstance()
        {
            var current = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(current.ProcessName);
            //循环所有进程   
            foreach (var process in processes)
            {
                //忽略当前进程
                if (process.Id != current.Id)
                {
                    //确保当前进程是从本Exe启动的进程  
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //返回其他进程实例
                        return process;
                    }
                }
            }
            //如果没有其他同名进程存在，则返回NULL 
            return null;
        }

        /// <summary>
        /// 如果有另一个同名进程启动，则调用之前的进程实例
        /// </summary>
        /// <param name="instance"></param>
        private static void HandleRunningInstance(Process instance)
        {
            //确保窗体不是最小化或者最大化   
            NgNet.Windows.Apis.User32.ShowWindowAsync(instance.MainWindowHandle, 1);
            //将真实的实例弄到前台窗口
            NgNet.Windows.Apis.User32.SetForegroundWindow(instance.MainWindowHandle);
        }
        #endregion  
    }
}
