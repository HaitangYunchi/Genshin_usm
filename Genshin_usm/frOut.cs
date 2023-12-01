using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Genshin_usm
{
    public partial class frOut : Form
    {
        public frOut()
        {
            InitializeComponent();
        }

        static StringBuilder sortOutput = null;
       // private static StringBuilder sortOutput = null;
        Process sortProcess;

        private void frOut_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            richTextBox1.Text += "-----------------------------------------" + "\n";
            BatchDemuxe();
  
            
        }
        
         public void BatchDemuxe()
        {
            if (sortProcess != null)
            {
                sortProcess.Close();
            }
            sortProcess = new Process();
            sortOutput = new StringBuilder("");
            sortProcess.StartInfo.FileName = "cmd.exe";
            sortProcess.StartInfo.UseShellExecute = false;// 必须禁用操作系统外壳程序  
            sortProcess.StartInfo.RedirectStandardOutput = true;
            sortProcess.StartInfo.RedirectStandardError = true; //重定向错误输出
            sortProcess.StartInfo.CreateNoWindow = true;  //设置不显示窗口
            sortProcess.StartInfo.RedirectStandardInput = true;
            sortProcess.StartInfo.Arguments = "/c " + GlobalVar.Command_cmd;    //设定程式执行参数
            sortProcess.Start();
            sortProcess.BeginOutputReadLine();// 异步获取命令行内容  
            sortProcess.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler); // 为异步获取订阅事件  
        }
        private void SortOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                Control.CheckForIllegalCrossThreadCalls = false;      //允许不同线程之间可以互相调用
                this.richTextBox1.Text += outLine.Data + Environment.NewLine;
            }
        }


    }
}
