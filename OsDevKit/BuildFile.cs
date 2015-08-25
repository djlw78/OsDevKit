using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsDevKit
{
    public class BuildFile
    {
        public string Name { get; set; }
        public List<BuildStep> Steps { get; set; } = new List<BuildStep>();
        public string Description { get; set; }
        public string LinkerScriptPath { get; set; }

    }

    public class BuildStep
    {
        //Meta
        public string Name { get; set; }
        public string Description { get; set; }

        //Needed Data
        public string Exe { get; set; }
        public bool OnceOfExecute { get; set; }
        public bool Waitfor { get; set; } = true;
        public string OPT { get; set; }
        public string OutPutRegex { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }
        public List<string> Args { get; set; }
    }
}
