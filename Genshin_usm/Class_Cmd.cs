﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Genshin_usm
{
    /// <summary>
    /// 通过调用CMD执行命令
    /// </summary>
    public class Class_Cmd
    {
        /// 
        /// 执行单条命令
        /// 
        ///命令文本
        /// 命令输出文本
        public static string ExeCommand(string commandText)
        {
            return ExeCommand(new string[] { commandText });
        }
        /// 
        /// 执行多条命令
        /// 
        ///命令文本数组
        /// 命令输出文本
        public static string ExeCommand(string[] commandTexts)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;//标准输入
            p.StartInfo.RedirectStandardOutput = true;//标准输出
            p.StartInfo.RedirectStandardError = true;//错误
            p.StartInfo.CreateNoWindow = true;//隐藏运行
            
            try
            {
                p.Start();
                foreach (string item in commandTexts)
                {
                    p.StandardInput.WriteLine(item);
                }
                p.StandardInput.WriteLine("exit");
                GlobalVar.strOutput = p.StandardOutput.ReadToEnd();

               //GlobalVar.strOutput = Encoding.UTF8.GetString(Encoding.Default.GetBytes(GlobalVar.strOutput));
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                GlobalVar.strOutput = e.Message;
            }
            return GlobalVar.strOutput;
        }

        /// 
        /// 启动外部Windows应用程序，隐藏程序界面
        ///
        ///应用程序路径名称
        /// true表示成功，false表示失败
        public static bool StartApp(string appName)
        {
            return StartApp(appName, ProcessWindowStyle.Hidden);
        }
        /// 
        /// 启动外部Windows应用程序，进程窗口模式
        ///
        ///应用程序路径名称
        /// true表示成功，false表示失败
        public static bool StartApp(string appName, ProcessWindowStyle style)
        {
            return StartApp(appName, null, style);
        }
        ///
        /// 启动外部应用程序，隐藏程序界面
        ///
        ///应用程序路径名称
        ///启动参数
        /// true表示成功，false表示失败
        public static bool StartApp(string appName, string arguments)
        {
            return StartApp(appName, arguments, ProcessWindowStyle.Hidden);
        }
        ///
        /// 启动外部应用程序
        ///
        ///应用程序路径名称
        ///启动参数
        ///进程窗口模式
        /// true表示成功，false表示失败
        public static bool StartApp(string appName, string arguments, ProcessWindowStyle style)
        {
            bool blnRst = false;
            Process p = new Process();
            p.StartInfo.FileName = appName;//exe,bat and so on
            p.StartInfo.WindowStyle = style;
            p.StartInfo.Arguments = arguments;
            try
            {
                p.Start();
                p.WaitForExit();
                p.Close();
                blnRst = true;
            }
            catch
            {
            }
            return blnRst;
        }

        internal static void StartApp()
        {
            throw new NotImplementedException();
        }
    }
}
