using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class ViewQuarterlyReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindQuarters();
            }
        }
        private void BindQuarters()
        {
            DataSet dsQuarter = QuarterlyReportBAL.Instance.GetQuarterListt();
            gvNonRecurring.DataSource = dsQuarter.Tables[0];           
            
            gvNonRecurring.DataBind();
            //ddlNRQuarter.Items.Insert(0, "Select");

            gvRecurring.DataSource = dsQuarter.Tables[0];           
            
            gvRecurring.DataBind();
            //ddlRQuarter.Items.Insert(0, "Select");
        }
    }
}