using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG278_2_
{
    class Module
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }

        public Module() { }

        public Module(string moduleCode, string moduleName, string description, string links)
        {
            ModuleCode = moduleCode;
            ModuleName = moduleName;
            Description = description;
            Links = links;
        }
    }
}
