using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.BAL
{
    class customAttribueClass
    {

    }
    [AttributeUsage(AttributeTargets.All)]
    internal class WebSysDescriptionAttribute : DescriptionAttribute
    {
        public override string Description { get;  }
        public override object TypeId { get;  }
        public WebSysDescriptionAttribute(string text)
        {
            Description = text;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class WebCategoryAttribute : CategoryAttribute
    {
        public override object TypeId { get;  }

        // protected override string GetLocalizedString(string value);
    }
}
