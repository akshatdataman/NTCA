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
    public partial class Obligation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = AlertBAL.Instance.GetAlerts("FieldDirector");
            gvobligation.DataSource = ds.Tables[1];
            gvobligation.DataBind();
            //gvAPoSubmitted.DataSource = ds.Tables[1];
            //gvAPoSubmitted.DataBind();
            //gvQuarterlyReport.DataSource = ds.Tables[1];
            //gvQuarterlyReport.DataBind();
            //gvApproved.DataSource = ds.Tables[1];
            //gvApproved.DataBind();
        }

        protected void gvobligation_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell=new TableCell();
                HeaderCell.Text = "Sr.No";
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Description of Obligations of the Field Director";
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Compiled / Not Compiled / Not Applicable";
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Level of Complaince";
                HeaderRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "If not, reason there for";
                HeaderRow.Cells.Add(HeaderCell);

                HeaderRow.Attributes.Add("class", "th_Header");

                gvobligation.Controls[0].Controls.AddAt(0,HeaderRow);



                GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "A";
                HeaderRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Article-ll- obligations of the Field Director";
                HeaderCell1.ColumnSpan = 4;
                HeaderRow1.Cells.Add(HeaderCell1);



                 HeaderRow1.Attributes.Add("class", "td_Bold");

                gvobligation.Controls[0].Controls.AddAt(1, HeaderRow1);






























            }
        }
    }
}