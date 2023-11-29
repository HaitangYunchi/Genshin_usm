using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Genshin_usm
{
    public partial class frAbout : Form
    {
        public frAbout()
        {
            InitializeComponent();
        }

        private void Out_put_Man_Load(object sender, EventArgs e)
        {
            this.label6.Text = "当前版本："+GlobalVar.VersionNo;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://space.bilibili.com/3493128132626725");//链接到海棠云螭的B站
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://haitang.x3322.net:88/client_app/Genshin_usm/download/versions.json");
        }
    }
}
