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

using System.IO;//
using System.Diagnostics;

namespace Genshin_usm
{
    public partial class FrMain : Form
    {
        public FrMain()
        {
            InitializeComponent();
            ///this.skinEngine1.SkinFile = "Skins\\default.ssk";
        }
        private void FrMain_Load(object sender, EventArgs e)
        {
            textBox1.Text = GlobalVar.StrPath + @"\GICutscenes.exe";
            textBox2.Text = GlobalVar.StrPath + @"\ffmpeg.exe";
            Class_Ini.writeString("Config", "Cutscenes", textBox1.Text, GlobalVar.IniName);
            Class_Ini.writeString("Config", "Ffmpegr", textBox2.Text, GlobalVar.IniName);
            this.toolStripStatusLabel4.Text = GlobalVar.VersionNo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //打开GI_Cutscenes文件


            string SaveFilesName = GlobalVar.StrPath + "\\GICutscenes.exe";
            string Appsettings = GlobalVar.StrPath + "\\appsettings.json";
            string CuVersion = GlobalVar.StrPath + "\\versions.json";
            //string Str = "";
            // Str= System.Windows.Forms.Application.StartupPath;
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "选择 GI_Cutscenes";
            file.Filter = "程序文件（*.exe）|*.exe";
            file.InitialDirectory = GlobalVar.StrPath;
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                this.textBox1.Text = "";
            else
            {
                string path = file.FileName;
                string pAppName = System.IO.Path.GetDirectoryName(file.FileName) + "\\appsettings.json";
                string pVerName = System.IO.Path.GetDirectoryName(file.FileName) + "\\versions.json";

                if (System.IO.File.Exists(path))//必须判断要复制的文件是否存在
                {
                    System.IO.File.Copy(path, SaveFilesName, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                }
                if (System.IO.File.Exists(pAppName))
                {
                    System.IO.File.Copy(pAppName, Appsettings, true);
                }
                if (System.IO.File.Exists(pVerName))
                {
                    System.IO.File.Copy(pVerName, CuVersion, true);
                }
                this.textBox1.Text = SaveFilesName;//会返回 GI_Cutscenes 程序全路径，包含了文件名和后缀
                GlobalVar.GICutscents_path = SaveFilesName;
                Class_Ini.writeString("Config", "Cutscenes", GlobalVar.GICutscents_path, GlobalVar.IniName);//写入 Config.ini 配置文件
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //打开Ffmpe文件

            string SaveFilesName = GlobalVar.StrPath + "\\ffmpeg.exe";
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "选择 Ffmpe";
            file.Filter = "程序文件（*.exe）|*.exe";
            file.InitialDirectory = GlobalVar.StrPath;
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                this.textBox2.Text = "";
            else
            {
                string path = file.FileName;
                if (System.IO.File.Exists(path))//同上
                {
                    System.IO.File.Copy(path, SaveFilesName, true);//同上
                }
                this.textBox2.Text = SaveFilesName;//会返回 Ffmpe 程序全路径，同上
                GlobalVar.Ffmpeg_path = SaveFilesName;
                Class_Ini.writeString("Config", "Ffmpegr", GlobalVar.Ffmpeg_path, GlobalVar.IniName);//同上
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ///输出目录
            ///

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                GlobalVar.Output_path = f.SelectedPath;
                this.textBox3.Text = GlobalVar.Output_path;//返回文件夹路径
                Class_Ini.writeString("Config", "Output_path", GlobalVar.Output_path, GlobalVar.IniName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            ///游戏目录
            ///

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择 YuanShen.exe";
            openFileDialog.Filter = "程序文件|YuanShen.exe";
            openFileDialog.InitialDirectory = @"D:\";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                GlobalVar.SelectedFolder = System.IO.Path.GetDirectoryName(selectedFile);
                this.textBox4.Text = GlobalVar.SelectedFolder;
                GlobalVar.Games_path = GlobalVar.SelectedFolder;
                Class_Ini.writeString("Config", "Games_path", GlobalVar.Games_path, GlobalVar.IniName);
                // MessageBox.Show("选择的文件夹为：" +  GlobalVar.SelectedFolder);
            }
            else
            {
                textBox4.Text = "";
                GlobalVar.Games_path = textBox4.Text;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

            ///USM 文件
            ///

            if (textBox4.Text == "")
            {
                MessageBox.Show("游戏目录不能为！", GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Title = "选择 GI_Cutscenes";
                file.Filter = "USM文件（*.usm）|*.usm";
                file.InitialDirectory = GlobalVar.Games_path + @"\YuanShen_Data\StreamingAssets\VideoAssets\StandaloneWindows64";
                file.Multiselect = false;
                if (file.ShowDialog() == DialogResult.Cancel)
                {
                    this.textBox5.Text = "";
                }
                else
                {
                    GlobalVar.USM_Files = file.FileName;//会返回 USM 文件全路径，包含了文件名和后缀
                    string _temp = Path.GetFileName(GlobalVar.USM_Files);
                    this.textBox5.Text = _temp;//返回 USM 文件名和后缀
                    GlobalVar.Video_Name = Path.GetFileNameWithoutExtension(_temp);
                    //MessageBox.Show(GlobalVar.Video_Name);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            ///导出
            ///

            if (textBox1.Text == "")
            {
                MessageBox.Show("GICutscenes 程序不能为空！", GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Ffmpeg 程序不能为空！", GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("输出目录不能为空！", GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("游戏目录不能为空！", GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("请选择要转换的文件！", GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    GlobalVar.Out_Language = "0";
                }
                else if (radioButton2.Checked == true)
                {
                    GlobalVar.Out_Language = "1";
                }
                else if (radioButton3.Checked == true)
                {
                    GlobalVar.Out_Language = "2";
                }
                else if (radioButton4.Checked == true)
                {
                    GlobalVar.Out_Language = "3";
                }
                else
                {
                    GlobalVar.Out_Language = "0";
                }

                Class_Ini.writeString("Config", "Language", GlobalVar.Out_Language, GlobalVar.IniName);
                // MessageBox.Show("选择的语言：" + GlobalVar.Out_Language);
                GlobalVar.Audio_Name = GlobalVar.Video_Name + "_" + GlobalVar.Out_Language + ".wav";
                GlobalVar.Command_cmd = "GICutscenes demuxUsm " + "\"" + GlobalVar.USM_Files + "\"" + " -o " + GlobalVar.Output_path + "\\" + GlobalVar.Video_Name;
                GlobalVar.Command_mpeg = "ffmpeg -i " + GlobalVar.Output_path + "\\" + GlobalVar.Video_Name + "\\" + GlobalVar.Video_Name + ".ivf" + " -i " + GlobalVar.Output_path + "\\" + GlobalVar.Video_Name + "\\" + GlobalVar.Audio_Name + " -c:v copy -c:a copy " + GlobalVar.Output_path + "\\" + GlobalVar.Video_Name + "\\" + GlobalVar.Video_Name + "_" + GlobalVar.Out_Language + ".mp4 &&exit";

                //MessageBox.Show(GlobalVar.Command_cmd);

                Class_Cmd.ExeCommand(GlobalVar.Command_cmd);
                MessageBox.Show(GlobalVar.strOutput, GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Class_Cmd.ExeCommand(GlobalVar.Command_mpeg);

            }



        }



        private void button7_Click(object sender, EventArgs e)
        {
            //GlobalVar.Games_path = "";
            GlobalVar.GICutscents_path = GlobalVar.StrPath + @"\GICutscenes.exe";
            GlobalVar.Ffmpeg_path = GlobalVar.StrPath + @"\ffmpeg.exe";
            GlobalVar.Output_path = Class_Ini.getString("Config", "Output_path", "", GlobalVar.IniName);
            GlobalVar.Games_path = Class_Ini.getString("Config", "Games_path", "", GlobalVar.IniName);
            textBox1.Text = GlobalVar.GICutscents_path;
            textBox2.Text = GlobalVar.Ffmpeg_path;
            textBox3.Text = GlobalVar.Output_path;
            textBox4.Text = GlobalVar.Games_path;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button6.Text = "国语配音";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button6.Text = "英语配音";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button6.Text = "日语配音";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button6.Text = "韩语配音";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Class_Cmd.ExeCommand("GICutscenes update &&exit");
            MessageBox.Show(GlobalVar.strOutput, GlobalVar.AuthorName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frAbout f = new frAbout();
            f.ShowDialog();
        }
    }
}
