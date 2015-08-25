using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsDevKit
{
    public static class Compiler
    {
        public static string Log = "";
        /*
           {name} = only name;
           {buildedname} = special name for build folder;
           {filepath} = current file
           {build} = build folder path with \
           {img} = builded image file
           {include} = include folder

           {opt} = opt
           {dls} = defuly linker sript,
           {buildfilels} = custom linker script
           {nasm} = nasm loc
           {gcc} = gcc loc
           {g++} = g++ loc
           {fbc} = fbc loc
           {ld} = ld loc
           {git} = GrubImageTool loc
           {qemu} = qemu loc
       */

        private static string BuildFolder = "./Factory\\Build";

        private static string DoReplacements(string val, BuildStep stp, string fl)
        {
           
            //val = val.Replace("","");
            if (!string.IsNullOrEmpty(fl))
            {
                var f1 = new FileInfo(fl);

                val = val.Replace("{name}",(f1.Name.Split('.')[0]));
                val = val.Replace("{buildedname}", f1.Name.Split('.')[0] + DateTime.Now.ToFileTime() + ".o");
                val = val.Replace("{filepath}", Path.GetFullPath(Path.Combine(Global.CurrentProjectFilePath, "files", fl)));
                val = val.Replace("{build}", Path.GetFullPath(BuildFolder));
                
                var incl = Path.GetFullPath(Path.Combine(Global.CurrentProjectFilePath, "files", "include"));
                val = val.Replace("{include}", incl);

            }
            var locimg = Path.Combine(Global.CurrentProjectFilePath, "Bin", "Boot.img");
            val = val.Replace("{img}", Path.GetFullPath(locimg));
            val = val.Replace("{opt}", stp.OPT);
            val = val.Replace("{build}", BuildFolder + "\\");
            val = val.Replace("{dls}", "./Factory\\linker.ld");
            val = val.Replace("{buildfilels}", Global.CurrentBuildFile.LinkerScriptPath);
            val = val.Replace("{nasm}", "./Factory\\nasm.exe");
            val = val.Replace("{git}", "./Factory\\GrubImgTool.exe");
            val = val.Replace("{gcc}", "./Tools\\bin\\gcc.exe");
            val = val.Replace("{g++}", "./Tools\\bin\\g++.exe");
            val = val.Replace("{ld}", "./Tools\\bin\\ld.exe");
            val = val.Replace("{fcb}", "./freebasic\\fbc.exe");
            val = val.Replace("{qemu}", "./Factory\\qemu\\qemu.exe");

            return val;
        }

        public static void Compile()
        {
            if (!Directory.Exists("Factory\\Build"))
            {
                Directory.CreateDirectory("Factory\\Build");
            }
            foreach (var i in Directory.GetFiles("Factory\\Build"))
            {
                File.Delete(i);
            }

            if (!Directory.Exists(Path.Combine(Global.CurrentProjectFilePath, "Bin")))
            {
                Directory.CreateDirectory(Path.Combine(Global.CurrentProjectFilePath, "Bin"));
            }
            foreach (var i in Directory.GetFiles(Path.Combine(Global.CurrentProjectFilePath, "Bin")))
            {
                File.Delete(i);
            }
            bool buidlfilefound = true;
            if (Global.CurrentBuildFile == null)
            {
                Global.CurrentBuildFile = JsonConvert.DeserializeObject<BuildFile>(File.ReadAllText("basic.buildfile"));
                buidlfilefound = false;
            }

            foreach (var i in Global.CurrentBuildFile.Steps)
            {

                string args = "";
                foreach (var y in i.Args)
                {
                    args += DoReplacements(y, i, "") + " ";
                }

                if (i.OnceOfExecute)
                {
                    StartProcces(DoReplacements(i.Exe, i, ""), DoReplacements(args, i, ""), "Factory",i,"", i.Waitfor);

                }
                else
                {
                    foreach (var z in Global.CurrentProjectFile.Files)
                    {
                        if (Regex.IsMatch("." + z.Split('.').Last(), i.FileType))
                        {
                            StartProcces(DoReplacements(i.Exe, i, z), DoReplacements(args, i, z), "Factory",i, z, i.Waitfor);
                        }
                    }
                }
                var locimg = Path.Combine(Global.CurrentProjectFilePath, "Bin", "Boot.img");
                if (File.Exists("Boot.img"))
                {
                    File.Delete(locimg);
                    File.Copy("Boot.img", locimg);
                }

            }
            if (!buidlfilefound)
            {
                Global.CurrentBuildFile = null;
            }

        }

        public static void StartProcces(string name, string args, string path, BuildStep st,string fl, bool waitfor = true )
        {


            System.Diagnostics.Process p = new System.Diagnostics.Process();
            // p.StartInfo.WorkingDirectory = new DirectoryInfo( path).FullName ;




             p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            p.StartInfo.FileName = name;
            p.StartInfo.Arguments = args;
           // p.StartInfo.WorkingDirectory = ".";

            p.Start();
            if (waitfor)
            {
                p.WaitForExit();
            }
            else
            {
                return;
            }

            var x =  p.StandardOutput.ReadToEnd() + "\n" + p.StandardError.ReadToEnd();
            var ret = x;
            
           
            Global.OutPut += "----------------------------------------\n" + DoReplacements( st.OutPutRegex,st, fl) + "\n" + ret + "\n\n";
        }
    }
}
