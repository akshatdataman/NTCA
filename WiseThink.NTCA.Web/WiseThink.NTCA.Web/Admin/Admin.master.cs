using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class AdminMaster : BaseMaster
    {
        protected void Page_InIt(object sender, EventArgs e)
        {
            if (!AuthoProvider.LoggedInRoles.Contains(Role.ADMIN))
            {
                // Redirect to Login page
                AuthoProvider.LogOut();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}