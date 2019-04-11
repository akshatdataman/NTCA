using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace WiseThink
{
    [ToolboxData("<{0}:CustomCheckBox runat=server></{0}:CustomCheckBox>")]
    public  class CustomCheckBox : CheckBox
    {
        public CustomCheckBox()
        {
            string onchange = @"
var id = $(this).attr('id');
var span = $(this).next('label[for=' + id + ']').children('span." + checkBoxClass + @":first');
if ($(this).attr('checked')) {
    span.addClass('checked');
} else {
    span.removeClass('checked');
}";

            this.InputAttributes.Add("style", "display:none");
            this.InputAttributes.Add("onchange", onchange.Replace("\r\n", ""));
            //this.InputAttributes.Add("onchange", onchange);
            //this.InputAttributes.Add("onchange", "alert('asdf');boxChanged(this);");
            this.LabelAttributes.Add("onclick", "var input = $('#'+$(this).attr('for')); if (input.is(':disabled') == false) input.click().change();return false;");
        }
        private const string checkBoxClass = "b-checkbox";
        public override string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                string cls = checkBoxClass;
                if (this.Enabled == false) cls += " disabled";
                if (this.Checked) cls += " checked";
                if (this.Enabled == false) cls += " disabled";
                return string.Format(@"<span class=""{0}""></span>{1}", cls, s ?? string.Empty);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }
    }
}
