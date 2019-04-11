using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Net;

namespace WiseThink
{
    public class CustomLabel : Label
    {
        public override string Text
        {
            get
            {
                var finalText = WebUtility.HtmlEncode(base.Text.ToString().RemoveScriptAndHtml());
                return finalText;
            }
            set
            {
               // var finalText = WebUtility.HtmlDecode(value.ToString().RemoveScriptAndHtml());
                if (value != null)
                    base.Text = value.ToString().FormatEmailIdWithInString().RemoveScriptAndHtml();
                else
                    base.Text = value;
            }
        }
    }

}