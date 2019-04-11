using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;

namespace WiseThink.NTCA.Web.UserControls
{
    public partial class ShowGuidelines : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGuidelines();
            }
        }
        public void BindGuidelines()
        {
            DataSet ds = ManageGuidelineBAL.Instance.GetGuidelineList();
            cgvManageGuidelines.DataSource = ds;
            cgvManageGuidelines.DataBind();
        }
        protected void cgvManageGuidelines_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvManageGuidelines.PageIndex = e.NewPageIndex;
            BindGuidelines();
        }
    }
}