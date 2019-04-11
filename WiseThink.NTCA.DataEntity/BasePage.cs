using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace WiseThink
{
    public class BasePage : Page
    {
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

            /* Verifies that the control is rendered */

        }
    }
}
