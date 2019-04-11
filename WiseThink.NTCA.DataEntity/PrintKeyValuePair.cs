using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity
{
    [Serializable]
    public class PrintKeyValuePair
    {
        public string TableColumn { get; set; }
        public string DisplayColumn { get; set; }
    }
}
