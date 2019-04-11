using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WiseThink.NTCA.Web;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.Web.App_Code;


namespace WiseThink.NTCA.Web.UserControls
{
    public partial class Message : System.Web.UI.UserControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string infoMessage = (string)Session[Constants.INFO_MESSAGE];
            if (!string.IsNullOrEmpty(infoMessage))
            {
                successContainer.Visible = true;
                ltrInfoMessage.Text = infoMessage;
                Session.Remove(Constants.INFO_MESSAGE);
            }

            string errorMessage = (string)Session[Constants.ERROR_MESSAGE];
            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorContainer.Visible = true;
                ltrErrorMessage.Text = errorMessage;
                Session.Remove(Constants.ERROR_MESSAGE);
            }

            string warningMessage = (string)Session[Constants.WARNING_MESSAGE];
            if (!string.IsNullOrEmpty(warningMessage))
            {
                warningContainer.Visible = true;
                ltrWarningMessage.Text = warningMessage;
                Session.Remove(Constants.WARNING_MESSAGE);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}