using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsDevKit.UI.Dialogs
{
    public partial class NewBuildFileDialog : Form
    {

        /*
            {name} = only name;
            {buildedname} = special name for build folder;
            {filepath} = current file
            {opt} = opt
            {build} = build folder path with \
            {include} = include folder
            {dls} = defuly linker sript,
            {buildfilels} = custom linker script
            {img} = builded image file
            {nasm} = nasm loc
            {gcc} = gcc loc
            {g++} = g++ loc
            {fbc} = fbc loc
            {ld} = ld loc
            {git} = GrubImageTool loc
            {qemu} = qemu loc
        */

        public NewBuildFileDialog()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            BuildFile bf = new BuildFile();

            bf.Name = "Standert Build File";
            bf.Description = "This is the build file used to compile the templates";

            bf.Steps.Add(new BuildStep()
            {
                Description = "Asm - Compile all asm files",
                Name = "Assembly",
                OPT = "",
                Args = new List<string>() { "-f elf", "-o {build}{buildedname}", "{filepath}" },
                Exe = "{nasm}",
                FileType = ".asm",
                Path = ".",
                OutPutRegex = ""
            });
            bf.Steps.Add(new BuildStep() {
                Description = "Gcc - Compile all C files" ,
                Name = "GCC", OPT = "-w -m32 -Wall -O -fstrength-reduce  -finline-functions -fomit-frame-pointer -nostdinc -fno-builtin -I {include} -c -fno-strict-aliasing -fno-common -fno-stack-protector",
                Args = new List<string>() {"{opt}", "-o {build}{buildedname}", "{filepath}" } ,
                Exe = "{gcc}" ,
                FileType = ".c" ,
                Path = ".",
                OutPutRegex = ""});
            bf.Steps.Add(new BuildStep()
            {
                Description = "G++ - Compile all C++ files",
                Name = "G++",
                OPT = "-ffreestanding -O2 -Wall -Wextra -fno-exceptions -fno-rtti -I {include}",
                Args = new List<string>() { "{opt}", "-o {build}{buildedname}", "{filepath}" },
                Exe = "{g++}",
                FileType = ".cpp|.c\\+\\+",
                Path = ".",
                OutPutRegex = ""
            });
            bf.Steps.Add(new BuildStep()
            {
                Description = "Freebasic - Compile all bas files",
                Name = "FreeBasic",
                OPT = "",
                Args = new List<string>() { "-c {filepath}", "-o {build}{buildedname}" },
                Exe = "{fbc}",
                FileType = ".bas",
                Path = ".",
                OutPutRegex = ""
            });
            bf.Steps.Add(new BuildStep()
            {
                Description = "Linker - Link all files",
                Name = "Linker",
                OnceOfExecute = true,
                OPT = "",
                Args = new List<string>() { "-melf_i386", "-T {dls}", "{build}*.o" },
                Exe = "{ld}",
                FileType = ".o",
                Path = ".",
                OutPutRegex = ""
            });
            bf.Steps.Add(new BuildStep()
            {
                Description = "Build Image File - Builds Image File",
                Name = "BuildImageFile",
                OnceOfExecute = true,
                OPT = "",
                Args = new List<string>() { "-i Factory\\CD" , "-o {img}" },
                Exe = "{git}",
                FileType = ".*+",
                Path = ".",
                OutPutRegex = ""
            });
            bf.Steps.Add(new BuildStep()
            {
                Description = "Qemu - Start the emulator",
                Name = "Qemu",
                OnceOfExecute = true,
                Waitfor = false,
                OPT = "",
                Args = new List<string>() { "-L ." , "-fda {img}", "-serial tcp:127.0.0.1:8080,server,nowait" },
                Exe = "{qemu}",
                FileType = ".*+",
                Path = ".",
                OutPutRegex = ""
            });
            Global.CurrentBuildFile = bf;
            Global.Save();
            this.Close();
        }
    }
}
