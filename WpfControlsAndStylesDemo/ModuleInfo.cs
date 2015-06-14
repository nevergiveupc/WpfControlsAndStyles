using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlsAndStylesDemo
{
    public class ModuleInfo
    {
        public ModuleInfo()
        {
        }

        public string MenuName
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            set;
        }

        private List<ModuleInfo> _moduleChildren;
        public List<ModuleInfo> ModuleChildren
        {
            get
            {
                return _moduleChildren ?? (_moduleChildren = new List<ModuleInfo>());
            }
            set
            {
                _moduleChildren = value;
            }
        }
    }
}
