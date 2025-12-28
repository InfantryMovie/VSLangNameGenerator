using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSLangNameGenerator
{
    public class RootObject
    {
        public string Code { get; set; }
        public List<VariantGroup> VariantGroups { get; set; }
    }

    public class VariantGroup
    {
        public string Code { get; set; }
        public List<string> States { get; set; }
    }
}
