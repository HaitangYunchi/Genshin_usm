using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Genshin_usm
{
   public static class GlobalVar
    {
        ///代码设计海棠云螭（B站）  小海棠（抖音）
        ///2023-11-26 更新
        ///
        ///
        /// 
        /// <summary>
        /// <param name="StrPath">项目运行目录</param>
        /// </summary>

        public static string StrPath = System.Windows.Forms.Application.StartupPath;
        public static string VersionNo= Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string AuthorName = "海棠云螭";
        public static string IniName = StrPath + "\\Config.ini";
        public static string GICutscents_path = "";     //GICutscents 路径
        public static string Ffmpeg_path = "";          //Ffmpeg 路径
        public static string Output_path = "";          //输出目录
        public static string Games_path = "";           //游戏目录
        public static string USM_Files = "";            //usm 名称
        public static string SelectedFolder;            //原神 YuanShen.exe 所在目录
        /// <summary>
        /// <param name="Out_Language">输出语言  中文=0 ， 英文=1 ， 日语=2 ， 韩文=3</param>
        /// </summary>

        public static string Out_Language = "0";        //输出语言

        public static string Video_Name = "";           //输出视频名称
        public static string Command_cmd = "";          //命令
        public static string Command_mpeg = "";          //命令
        public static string Audio_Name = "";           //音频名称
        public static string strOutput = "";
        public static string _tempVideo_Name = "";      //视频临时名字，可能用不到，先放这里吧，懒得删了


    }
}
