﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiseThink.NTCA.UserControls
{
    public partial class ValidationMessage : System.Web.UI.UserControl
    {
        public string Message
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}