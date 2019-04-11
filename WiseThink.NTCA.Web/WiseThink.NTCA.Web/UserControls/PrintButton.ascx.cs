using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiseThink.NTCA.UserControls
{
    public partial class PrintButton : System.Web.UI.UserControl
    {
        public string printDivID = null;
        public string printRowPerPage = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string divId
        {
            set
            {
                printDivID = value;
            }
        }
        public string printRowNumber
        {
            set
            {
                printRowPerPage = value;
            }
        }  
    }
}