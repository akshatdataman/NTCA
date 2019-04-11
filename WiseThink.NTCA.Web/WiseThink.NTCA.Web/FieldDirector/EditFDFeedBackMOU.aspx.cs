using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class EditFDFeedBackMOU : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("SrNo", typeof(string));
            table.Columns.Add("Description", typeof(string));

            table.Columns.Add("Compiled", typeof(string));
            table.Columns.Add("reason", typeof(string));
            table.Columns.Add("Score", typeof(string));
            table.Columns.Add("Compliance", typeof(string));
            table.Columns.Add("Remarks", typeof(string));

            table.Rows.Add("A", "Article-ll- obligations of the Field Director");
            table.Rows.Add("1", "A Security plan would be drawn up for the reserve, considering its strength weakness opportunity and threat which would form part of the Tiger Conservation Plan, to ensure intelligence based enforcement for protection of tiger, other wild animals and the habitat.", "Compiled", "", "45", "abc", "G");

            gvFeebbackMOU.DataSource = table;
            gvFeebbackMOU.DataBind();
        }
    }
}