using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.BAL;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class ViewAPO : BasePage
    {
        int tigerReserveId, stateId;
        string currentFinancialYear = string.Empty;
        string previousFinancialYear = string.Empty;
        string subItemDetails = string.Empty;
        string subItemTotal = string.Empty;
        string strJustification = string.Empty;
        Decimal totalNR = 0.0m;
        Decimal totalR = 0.0m;
        Decimal activityTotal = 0.0m;

        Decimal RCentral = 0.0m;
        Decimal RState = 0.0m;
        Decimal NRCentral = 0.0m;
        Decimal NRState = 0.0m;
        Decimal GTotal = 0.0m;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsIds;
            if (Request.QueryString["Id"] != null)
            {
                dsIds = APOBAL.Instance.GetStateIdTigerReserveIdAndFinancialYear(Convert.ToInt32(Request.QueryString["Id"]));
                if (dsIds != null)
                {
                    DataRow dr = dsIds.Tables[0].Rows[0];
                    tigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    stateId = Convert.ToInt32(dr["StateId"]);
                    currentFinancialYear = dr["FinancialYear"].ToString();
                }
            }
            if (!IsPostBack)
            {
                GetApoForView("");
                UserBAL.Instance.InsertAuditTrailDetail("Visited View APO Page", "APO");
            }
            DisplayGrandTotal();
        }
        private void DisplayGrandTotal()
        {
            DataSet ds = new DataSet();
            ds = APOBAL.Instance.GetAPOAmount(tigerReserveId);

            gvApoAmountBreakup.DataSource = ds.Tables[0];
            gvApoAmountBreakup.DataBind();

            //decimal nrTotal = 0.0m;
            //decimal rTotal = 0.0m;
            //if (Session["totalNR"] != null)

            //    nrTotal = (decimal)Session["totalNR"];
            //if (Session["totalR"] != null)
            //    rTotal = (decimal)Session["totalR"];
            //decimal Total = nrTotal + rTotal;
           // lblRorNRAmount.Text = "(" + totalNR.ToString("#.###") + "+" + totalR.ToString("#.###") + ")   =   ";
            lblGrandTotal.Text = GTotal.ToString("#.###") + " (In lakhs)";
        }
        protected void gvNonRecurring_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.ViewGenerateGridHeader(ProductGrid, gvNonRecurring);
            }
        }

        protected void gvRecuring_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.ViewGenerateGridHeader(ProductGrid, gvRecuring);
            }
        }

        protected void gvApoAmountBreakup_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateAPOAmountBreakUpGridHeader(ProductGrid, gvApoAmountBreakup, tigerReserveId.ToString());
            }
        }

        protected void gvNonRecurring_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActivityId = (e.Row.Cells[0].FindControl("lblActivityId") as Label);
                Label lblActivityItemId = (e.Row.Cells[1].FindControl("lblActivityItemId") as Label);
                Label lblNumberOfItems = (e.Row.Cells[0].FindControl("lblNumberOfItems") as Label);
                Label lblUnitPrice = (e.Row.Cells[1].FindControl("lblUnitPrice") as Label);
                Label lblActivityAmountInLakhs = (Label)e.Row.FindControl("lblTotal");
                if (!string.IsNullOrEmpty(lblActivityAmountInLakhs.Text))
                {
                    activityTotal = Convert.ToDecimal(lblActivityAmountInLakhs.Text);
                    activityTotal = activityTotal / 100000;
                    lblActivityAmountInLakhs.Text = activityTotal.ToString() + " lakhs";
                }
                //totalNR += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
                totalNR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NRTotal"));
                //ViewGpsCoordinate(gvNonRecurring);

                #region Gps Details
                HtmlGenericControl GpsDetailsdiv = (HtmlGenericControl)e.Row.FindControl("GpsDetailsdiv");
                HtmlGenericControl GpsDetailsUl = (HtmlGenericControl)e.Row.FindControl("GpsDetailsUl");
                //
                DataSet dsGps = APOBAL.Instance.GetGPS(tigerReserveId, Convert.ToInt32(lblActivityItemId.Text), "ViewAPO", "");
                if (dsGps.Tables[0].Rows.Count > 0)
                {
                    if (dsGps != null)
                    {
                        GpsDetailsdiv.Style.Add("display", "block");
                        for (int i = 0; i < dsGps.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dsGps.Tables[0].Rows[i];
                            subItemDetails = Convert.ToString(dr["GPS"]);
                            System.Web.UI.HtmlControls.HtmlGenericControl createLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            createLi.ID = "createLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            createLi.InnerHtml = subItemDetails;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            createLi.Attributes.Add("class", "text-left");
                            GpsDetailsUl.Controls.Add(createLi);
                        }
                    }
                }
                else
                    GpsDetailsdiv.Style.Add("display", "none");
                #endregion

                #region View Sub Items
                //ViewSubItemDetails(gvNonRecurring);

                //int startIndex = 4;
                //int count = e.Row.RowIndex;
                //int initialCount = count + startIndex;
                //{
                //    GridView ParentGrid = (GridView)sender;
                //    APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvNonRecurring, initialCount + count, tigerReserveId, 1, Convert.ToInt32(lblActivityItemId.Text));
                //}
                HtmlGenericControl SubItemDetailsdiv = (HtmlGenericControl)e.Row.FindControl("SubItemDetailsdiv");
                HtmlGenericControl SubItemDetailsUl = (HtmlGenericControl)e.Row.FindControl("SubItemDetailsUl");

                HtmlGenericControl UnitPricediv = (HtmlGenericControl)e.Row.FindControl("UnitPricediv");
                HtmlGenericControl UnitPriceUl = (HtmlGenericControl)e.Row.FindControl("UnitPriceUl");

                HtmlGenericControl Justificationdiv = (HtmlGenericControl)e.Row.FindControl("JustificationDetailsDiv");
                HtmlGenericControl JustificationUl = (HtmlGenericControl)e.Row.FindControl("JustificationDetailsUl");
                //
                DataSet ds = APOBAL.Instance.GetSubItemsDetailsForView(tigerReserveId, 1, Convert.ToInt32(lblActivityItemId.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        string TotalNumberOfItem = string.Empty;
                        string UnitPrice = string.Empty;
                        SubItemDetailsdiv.Style.Add("display", "block");
                        UnitPricediv.Style.Add("display", "block");
                        Justificationdiv.Style.Add("display", "block");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            TotalNumberOfItem = Convert.ToString(dr["NumberOfItemTotal"]);
                            subItemDetails = Convert.ToString(dr["SubItemDetails"]);
                            UnitPrice = Convert.ToString(dr["UnitPrice"]);
                            subItemTotal = Convert.ToString(dr["SubTotal"]);
                            strJustification = Convert.ToString(dr["Justification"]);
                            System.Web.UI.HtmlControls.HtmlGenericControl subItemLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            subItemLi.ID = "createLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            subItemLi.InnerHtml = subItemDetails;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            subItemLi.Attributes.Add("class", "text-left");
                            SubItemDetailsUl.Controls.Add(subItemLi);

                            System.Web.UI.HtmlControls.HtmlGenericControl unitpriceLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            unitpriceLi.ID = "unitpriceLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            unitpriceLi.InnerHtml = UnitPrice;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            unitpriceLi.Attributes.Add("class", "text-left");
                            UnitPriceUl.Controls.Add(unitpriceLi);

                            System.Web.UI.HtmlControls.HtmlGenericControl justificationLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            justificationLi.ID = "justificationLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            justificationLi.InnerHtml = strJustification;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            justificationLi.Attributes.Add("class", "text-left");
                            JustificationUl.Controls.Add(justificationLi);
                        }
                        lblActivityAmountInLakhs.Text = subItemTotal.ToString() + " lakhs";
                        lblNumberOfItems.Text = TotalNumberOfItem;
                        lblUnitPrice.Text = lblActivityAmountInLakhs.Text;
                    }
                }
                else
                {
                    lblUnitPrice.Visible = true;
                    SubItemDetailsdiv.Style.Add("display", "none");
                    UnitPricediv.Style.Add("display", "none");
                }
                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("txtSubTotal");
                totalNR = totalNR / 100000;
                lblAmount.Text = totalNR.ToString("#.###") + " (In lakhs)";
                Session["totalNR"] = totalNR;
            }
        }

        protected void gvApoAmountBreakup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSRNo = (e.Row.Cells[0].FindControl("lblSRNo") as Label);
                Label lblCentralShareCore = (e.Row.Cells[1].FindControl("lblCentralShareCore") as Label);
                Label lblStateShareCore = (e.Row.Cells[0].FindControl("lblStateShareCore") as Label);
                Label lblCentralShareBuffer = (e.Row.Cells[1].FindControl("lblCentralShareBuffer") as Label);
                Label lblStateShareBuffer = (Label)e.Row.FindControl("lblStateShareBuffer");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                if (!string.IsNullOrEmpty(lblTotal.Text))
                {
                    RCentral = RCentral + Convert.ToDecimal(lblCentralShareCore.Text);
                    RState = RState + Convert.ToDecimal(lblStateShareCore.Text);
                    NRCentral = NRCentral + Convert.ToDecimal(lblCentralShareBuffer.Text);
                    NRState = NRState + Convert.ToDecimal(lblStateShareBuffer.Text);
                    GTotal = RCentral + RState + NRCentral + NRState;
                }

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblSRNo = (e.Row.Cells[0].FindControl("lblSr") as Label);
                Label lblCentralShareCore = (e.Row.Cells[1].FindControl("lblRCentral") as Label);
                Label lblStateShareCore = (e.Row.Cells[0].FindControl("lblRState") as Label);
                Label lblCentralShareBuffer = (e.Row.Cells[1].FindControl("lblNRCentral") as Label);
                Label lblStateShareBuffer = (Label)e.Row.FindControl("lblNRState");
                //Label lblTotal = (Label)e.Row.FindControl("lblTotal");

                Label lblGrandTotal1 = (Label)e.Row.FindControl("lblGrandTotal1");
                lblSRNo.Text = "Total";
                lblCentralShareCore.Text = RCentral.ToString();
                lblStateShareCore.Text = RState.ToString();
                lblCentralShareBuffer.Text = NRCentral.ToString();
                lblStateShareBuffer.Text = NRState.ToString();
                lblGrandTotal1.Text = GTotal.ToString("#.###");
                lblRorNRAmount.Text = "( " + (NRCentral + NRState) + " + " + (RCentral + RState) + " ) = ";
            }
        }

        protected void gvRecuring_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActivityId = (e.Row.Cells[0].FindControl("lblActivityId") as Label);
                Label lblActivityItemId = (e.Row.Cells[1].FindControl("lblActivityItemId") as Label);
                Label lblNumberOfItems = (e.Row.Cells[0].FindControl("lblNumberOfItems") as Label);
                Label lblUnitPrice = (e.Row.Cells[1].FindControl("lblUnitPrice") as Label);
                Label lblActivityAmountInLakhs = (Label)e.Row.FindControl("lblTotal");
                if (!string.IsNullOrEmpty(lblActivityAmountInLakhs.Text))
                {
                    activityTotal = Convert.ToDecimal(lblActivityAmountInLakhs.Text);
                    activityTotal = activityTotal / 100000;
                    lblActivityAmountInLakhs.Text = activityTotal.ToString() + " lakhs";
                }
                //totalR += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
                if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "RTotal").ToString()))
                {
                    totalR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RTotal"));
                }
                //ViewGpsCoordinate(gvRecuring);

                #region Gps Details
                HtmlGenericControl GpsDetailsdiv = (HtmlGenericControl)e.Row.FindControl("GpsDetailsdiv");
                HtmlGenericControl GpsDetailsUl = (HtmlGenericControl)e.Row.FindControl("GpsDetailsUl");
                //
                DataSet dsGps = APOBAL.Instance.GetGPS(tigerReserveId, Convert.ToInt32(lblActivityItemId.Text), "ViewAPO", "");
                if (dsGps.Tables[0].Rows.Count > 0)
                {
                    if (dsGps != null)
                    {
                        GpsDetailsdiv.Style.Add("display", "block");
                        for (int i = 0; i < dsGps.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dsGps.Tables[0].Rows[i];
                            subItemDetails = Convert.ToString(dr["GPS"]);
                            System.Web.UI.HtmlControls.HtmlGenericControl createLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            createLi.ID = "createLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            createLi.InnerHtml = subItemDetails;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            createLi.Attributes.Add("class", "text-left");
                            GpsDetailsUl.Controls.Add(createLi);
                        }
                    }
                }
                else
                    GpsDetailsdiv.Style.Add("display", "none");
                #endregion

                #region View Sub Items
                //ViewSubItemDetails(gvNonRecurring);

                //int startIndex = 4;
                //int count = e.Row.RowIndex;
                //int initialCount = count + startIndex;
                //{
                //    GridView ParentGrid = (GridView)sender;
                //    APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvNonRecurring, initialCount + count, tigerReserveId, 1, Convert.ToInt32(lblActivityItemId.Text));
                //}
                HtmlGenericControl SubItemDetailsdiv = (HtmlGenericControl)e.Row.FindControl("SubItemDetailsdiv");
                HtmlGenericControl SubItemDetailsUl = (HtmlGenericControl)e.Row.FindControl("SubItemDetailsUl");

                HtmlGenericControl UnitPricediv = (HtmlGenericControl)e.Row.FindControl("UnitPricediv");
                HtmlGenericControl UnitPriceUl = (HtmlGenericControl)e.Row.FindControl("UnitPriceUl");

                HtmlGenericControl Justificationdiv = (HtmlGenericControl)e.Row.FindControl("JustificationDetailsDiv");
                HtmlGenericControl JustificationUl = (HtmlGenericControl)e.Row.FindControl("JustificationDetailsUl");


                //
                DataSet ds = APOBAL.Instance.GetSubItemsDetailsForView(tigerReserveId, 2, Convert.ToInt32(lblActivityItemId.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        string TotalNumberOfItem = string.Empty;
                        string UnitPrice = string.Empty;
                        SubItemDetailsdiv.Style.Add("display", "block");
                        UnitPricediv.Style.Add("display", "block");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            TotalNumberOfItem = Convert.ToString(dr["NumberOfItemTotal"]);
                            subItemDetails = Convert.ToString(dr["SubItemDetails"]);
                            UnitPrice = Convert.ToString(dr["UnitPrice"]);
                            subItemTotal = Convert.ToString(dr["SubTotal"]);
                            strJustification = Convert.ToString(dr["Justification"]);
                            System.Web.UI.HtmlControls.HtmlGenericControl subItemLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            subItemLi.ID = "createLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            subItemLi.InnerHtml = subItemDetails;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            subItemLi.Attributes.Add("class", "text-left");
                            SubItemDetailsUl.Controls.Add(subItemLi);

                            System.Web.UI.HtmlControls.HtmlGenericControl unitpriceLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            unitpriceLi.ID = "unitpriceLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            unitpriceLi.InnerHtml = UnitPrice;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            unitpriceLi.Attributes.Add("class", "text-left");
                            UnitPriceUl.Controls.Add(unitpriceLi);

                            System.Web.UI.HtmlControls.HtmlGenericControl justificationLi =
                            new System.Web.UI.HtmlControls.HtmlGenericControl("Li");
                            justificationLi.ID = "justificationLi" + (i + 1);
                            //createLi.InnerHtml = "<b>(" + (i + 1).ToString() + ") </b>" + subItemDetails;
                            justificationLi.InnerHtml = strJustification;
                            //createLi.Attributes.Add("class", "table table-bordered table-hover");
                            justificationLi.Attributes.Add("class", "text-left");
                            JustificationUl.Controls.Add(justificationLi);
                        }
                        lblActivityAmountInLakhs.Text = subItemTotal.ToString() + " lakhs";
                        lblNumberOfItems.Text = TotalNumberOfItem;
                        lblUnitPrice.Text = lblActivityAmountInLakhs.Text;
                    }
                }
                else
                {
                    lblUnitPrice.Visible = true;
                    SubItemDetailsdiv.Style.Add("display", "none");
                    UnitPricediv.Style.Add("display", "none");
                }
                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblActivityId = (e.Row.Cells[0].FindControl("lblActivityId") as Label);
                Label lblActivityItemId = (e.Row.Cells[1].FindControl("lblActivityItemId") as Label);
                Label lblAmount = (Label)e.Row.FindControl("txtSubTotal");
                totalR = totalR / 100000;
                lblAmount.Text = totalR.ToString("#.###") + " (In lakhs)";
                Session["totalR"] = totalR;
            }
        }
        //private void GetApoForView()
        //{
        //    DataSet dsViewApo = new DataSet();
        //    string LoggedInUser = AuthoProvider.User;
        //    string loggedInUserRole = string.Empty;
        //    DataSet dsUserRole = APOBAL.Instance.GetLoggedUserRole(LoggedInUser);
        //    if (dsUserRole != null)
        //    {
        //        DataRow dr = dsUserRole.Tables[0].Rows[0];
        //        loggedInUserRole = dr["RoleName"].ToString();
        //    }
        //    CommonClass cc = new CommonClass();
        //    previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
        //    if (loggedInUserRole == "FIELDDIRECTOR")
        //        dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId, previousFinancialYear);
        //    else if (loggedInUserRole == "CWLW" || loggedInUserRole == "SECRETARY")
        //        dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId, previousFinancialYear);
        //    else if (loggedInUserRole == "NTCA" || loggedInUserRole == "REGIONALOFFICER")
        //        dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId, previousFinancialYear);
        //    if ((bool)Session["IsApprove"] == true)
        //    {
        //        if (dsViewApo.Tables[2].Rows.Count > 0)
        //        {
        //            DataTable dt = dsViewApo.Tables[2];
        //            dt = dt.AsEnumerable().GroupBy(r => new { ID = r.Field<Int64>("ID"), ActivityItemID = r.Field<Int32>("ActivityItemID") }).Select(g => g.First()).CopyToDataTable();
        //            DataView view = new DataView(dsViewApo.Tables[2]);

        //            DataRow dr = dsViewApo.Tables[2].Rows[0];
        //            ApoTitle.InnerText = Convert.ToString(dr["APOTitle"]);
        //            gvNonRecurring.DataSource = dt; // dsViewApo.Tables[2];
        //            gvNonRecurring.DataBind();
        //            //ViewGpsCoordinate(gvNonRecurring);
        //        }
        //        else
        //        {
        //            gvNonRecurring.DataSource = null;
        //            gvNonRecurring.DataBind();
        //        }
        //        if (dsViewApo.Tables[3].Rows.Count > 0)
        //        {
        //            //gvRecuring.DataSource = dsViewApo.Tables[3];
        //            //gvRecuring.DataBind();

        //            DataTable dt = dsViewApo.Tables[3];
        //            dt = dt.AsEnumerable().GroupBy(r => new { ID = r.Field<Int64>("ID"), ActivityItemID = r.Field<Int32>("ActivityItemID") }).Select(g => g.First()).CopyToDataTable();
        //            gvRecuring.DataSource = dt;
        //            gvRecuring.DataBind();
        //            //ViewGpsCoordinate(gvRecuring);
        //        }
        //        else
        //        {
        //            gvRecuring.DataSource = null;
        //            gvRecuring.DataBind();
        //        }
        //        Session["IsApprove"] = false;
        //    }
        //    else
        //    {
        //        if (dsViewApo.Tables[0].Rows.Count > 0)
        //        {
        //            //DataRow dr = dsViewApo.Tables[0].Rows[0];
        //            //ApoTitle.InnerText = Convert.ToString(dr["APOTitle"]);
        //            //gvNonRecurring.DataSource = dsViewApo.Tables[0];
        //            //gvNonRecurring.DataBind();
        //            //ViewGpsCoordinate(gvNonRecurring);

        //            DataTable dt = dsViewApo.Tables[0];
        //            dt = dt.AsEnumerable().GroupBy(r => new { ID = r.Field<Int64>("ID"), ActivityItemID = r.Field<Int32>("ActivityItemID") }).Select(g => g.First()).CopyToDataTable();
        //            DataRow dr = dsViewApo.Tables[2].Rows[0];
        //            ApoTitle.InnerText = Convert.ToString(dr["APOTitle"]);
        //            gvNonRecurring.DataSource = dt;
        //            gvNonRecurring.DataBind();
        //        }
        //        else
        //        {
        //            gvNonRecurring.DataSource = null;
        //            gvNonRecurring.DataBind();
        //        }
        //        if (dsViewApo.Tables[1].Rows.Count > 0)
        //        {
        //            //gvRecuring.DataSource = dsViewApo.Tables[1];
        //            //gvRecuring.DataBind();

        //            DataTable dt = dsViewApo.Tables[1];
        //            dt = dt.AsEnumerable().GroupBy(r => new { ID = r.Field<Int64>("ID"), ActivityItemID = r.Field<Int32>("ActivityItemID") }).Select(g => g.First()).CopyToDataTable();
        //            gvRecuring.DataSource = dt;
        //            gvRecuring.DataBind();
        //        }
        //        else
        //        {
        //            gvRecuring.DataSource = null;
        //            gvRecuring.DataBind();
        //        }
        //    }
        //}
        private void GetApoForView(string ButtonName)
        {

            DataSet dsViewApo = new DataSet();
            string LoggedInUser = AuthoProvider.User;
            string loggedInUserRole = string.Empty;
            DataSet dsUserRole = APOBAL.Instance.GetLoggedUserRole(LoggedInUser);
            if (dsUserRole != null)
            {
                DataRow dr = dsUserRole.Tables[0].Rows[0];
                loggedInUserRole = dr["RoleName"].ToString();
            }
            CommonClass cc = new CommonClass();
            previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
            if (loggedInUserRole == "FIELDDIRECTOR")
                dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId, previousFinancialYear);
            else if (loggedInUserRole == "CWLW" || loggedInUserRole == "SECRETARY")
                dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId, previousFinancialYear);
            else if (loggedInUserRole == "NTCA" || loggedInUserRole == "REGIONALOFFICER")
                dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId, previousFinancialYear);
            if ((bool)Session["IsApprove"] == true)
            {
                if (dsViewApo.Tables[2].Rows.Count > 0)
                {
                    //DataTable dt = dsViewApo.Tables[2];
                    //dt = dt.AsEnumerable().GroupBy(r => new { ID = r.Field<Int64>("ID"), ActivityItemID = r.Field<Int32>("ActivityItemID") }).Select(g => g.First()).CopyToDataTable();
                    //DataView view = new DataView(dsViewApo.Tables[2]);

                    DataRow dr = dsViewApo.Tables[2].Rows[0];
                    ApoTitle.InnerText = Convert.ToString(dr["APOTitle"]);

                    DataTable dtTempNR = dsViewApo.Tables[2].Clone();
                    List<int> ActivityItemId = new List<int>();

                    foreach (DataRow dr1 in dsViewApo.Tables[2].Rows)
                    {
                        if (!ActivityItemId.Contains(Convert.ToInt16(dr1["ActivityItemId"])))
                        {
                            ActivityItemId.Add(Convert.ToInt16(dr1["ActivityItemId"]));
                            dtTempNR.ImportRow(dr1);
                        }
                    }
                    gvNonRecurring.DataSource = dtTempNR; // dsViewApo.Tables[2];
                    gvNonRecurring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "9px");
                    }

                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                    //ViewGpsCoordinate(gvNonRecurring);
                }
                else
                {
                    gvNonRecurring.DataSource = null;
                    gvNonRecurring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                    }
                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                }
                if (dsViewApo.Tables[3].Rows.Count > 0)
                {
                    //gvRecuring.DataSource = dsViewApo.Tables[3];
                    //gvRecuring.DataBind();

                    DataTable dtTempR = dsViewApo.Tables[3].Clone();
                    List<int> ActivityItemId = new List<int>();

                    foreach (DataRow dr1 in dsViewApo.Tables[3].Rows)
                    {
                        if (!ActivityItemId.Contains(Convert.ToInt16(dr1["ActivityItemId"])))
                        {
                            ActivityItemId.Add(Convert.ToInt16(dr1["ActivityItemId"]));
                            dtTempR.ImportRow(dr1);
                        }
                    }

                    gvRecuring.DataSource = dtTempR;
                    gvRecuring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "9px");
                    }
                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                    //ViewGpsCoordinate(gvRecuring);
                }
                else
                {
                    gvRecuring.DataSource = null;
                    gvRecuring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "9px");
                    }
                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                }
                Session["IsApprove"] = false;
            }
            else
            {
                if (dsViewApo.Tables[0].Rows.Count > 0)
                {

                    DataTable dtTempNR = dsViewApo.Tables[0].Clone();
                    List<int> ActivityItemId = new List<int>();

                    foreach (DataRow dr1 in dsViewApo.Tables[0].Rows)
                    {
                        if (!ActivityItemId.Contains(Convert.ToInt16(dr1["ActivityItemId"])))
                        {
                            ActivityItemId.Add(Convert.ToInt16(dr1["ActivityItemId"]));
                            dtTempNR.ImportRow(dr1);
                        }
                    }

                    DataRow dr = dsViewApo.Tables[0].Rows[0];
                    ApoTitle.InnerText = Convert.ToString(dr["APOTitle"]);

                    gvNonRecurring.DataSource = dtTempNR;// dsViewApo.Tables[0];
                    gvNonRecurring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "9px");
                    }
                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                }
                else
                {
                    gvNonRecurring.DataSource = null;
                    gvNonRecurring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "9px");
                    }
                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvNonRecurring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvNonRecurring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                }
                if (dsViewApo.Tables[1].Rows.Count > 0)
                {
                    //gvRecuring.DataSource = dsViewApo.Tables[1];
                    //gvRecuring.DataBind();

                    DataTable dtTempR = dsViewApo.Tables[1].Clone();
                    List<int> ActivityItemId = new List<int>();

                    foreach (DataRow dr1 in dsViewApo.Tables[1].Rows)
                    {
                        if (!ActivityItemId.Contains(Convert.ToInt16(dr1["ActivityItemId"])))
                        {
                            ActivityItemId.Add(Convert.ToInt16(dr1["ActivityItemId"]));
                            dtTempR.ImportRow(dr1);
                        }
                    }
                    gvRecuring.DataSource = dtTempR;
                    gvRecuring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "9px");
                    }
                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                }
                else
                {
                    gvRecuring.DataSource = null;
                    gvRecuring.DataBind();
                    if (ButtonName.ToUpper() == "PDF" || ButtonName.ToUpper() == "EXCEL")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "7px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "9px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "9px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "9px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "9px");
                    }
                    if (ButtonName.ToUpper() == "WORD")
                    {
                        gvRecuring.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        gvRecuring.Style.Add("font-size", "11px");
                        ApoTitle.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        ApoTitle.Style.Add("font-size", "11px");
                        Group1Title.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Group1Title.Style.Add("font-size", "11px");
                        Grouptitle2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        Grouptitle2.Style.Add("font-size", "11px");
                        GrandTotal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        GrandTotal.Style.Add("font-size", "11px");
                    }
                }
            }
        }
        private void ViewGpsCoordinate(GridView gv)
        {

            foreach (GridViewRow row in gv.Rows)
            {
                DataSet dsGps = new DataSet();
                Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                GridView gpsDetails = (GridView)row.FindControl("gvGpsDetails");
                if (!string.IsNullOrEmpty(lblActivityItemId.Text))
                {
                    dsGps = APOBAL.Instance.GetGPS(tigerReserveId, Convert.ToInt32(lblActivityItemId.Text), "ViewAPO", "");
                    if (dsGps.Tables[0].Rows.Count > 0)
                    {
                        gpsDetails.DataSource = dsGps.Tables[0];
                        gpsDetails.DataBind();
                    }
                    else
                    {
                        dsGps = null;
                        gpsDetails.DataSource = dsGps;
                        gpsDetails.DataBind();
                    }
                }
            }
        }
        private void ViewSubItemDetails(GridView gv, int activityTypeId)
        {

            foreach (GridViewRow row in gv.Rows)
            {
                DataSet dsSubItems = new DataSet();
                Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                DetailsView SubItemDetailsView = (DetailsView)row.FindControl("SubItemDetailsView");
                if (!string.IsNullOrEmpty(lblActivityItemId.Text))
                {
                    dsSubItems = APOBAL.Instance.GetSubItemsDetailsForView(tigerReserveId, activityTypeId, Convert.ToInt32(lblActivityItemId.Text));
                    if (dsSubItems.Tables[0].Rows.Count > 0)
                    {
                        SubItemDetailsView.DataSource = dsSubItems.Tables[0];
                        SubItemDetailsView.DataBind();
                    }
                    else
                    {
                        //dsSubItems = null;
                        //SubItemDetailsView.DataSource = dsSubItems;
                        //SubItemDetailsView.DataBind();
                    }
                }
            }
        }
        protected void imgbtnWord_click(object sender, ImageClickEventArgs e)
        {
            GetApoForView("WORD");
            ExportDivContentToMsWord();
        }
        protected void imgbtnExcel_click(object sender, ImageClickEventArgs e)
        {
            GetApoForView("EXCEL");
            ExportDivContentToMsExcel();
        }
        protected void imgbtnPdf_click(object sender, ImageClickEventArgs e)
        {
            GetApoForView("PDF");
            ExportDivContentToPDF();
        }
        /// <summary>
        /// This is the third party paid Api trial period over and it stop working
        /// </summary>
        //private void ExportDivContentToPDFOld()
        //{
        //    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
        //    try
        //    {
        //        // create an API client instance
        //        string userName = ConfigurationManager.AppSettings["pdfcrowdUsername"].ToString();
        //        string APIKey = ConfigurationManager.AppSettings["pdfcrowdAPIKey"].ToString();
        //        pdfcrowd.Client client = new pdfcrowd.Client(userName, APIKey);
        //        string fileName = ApoTitle.InnerText + ".pdf";
        //        // convert a web page and write the generated PDF to a memory stream
        //        MemoryStream Stream = new MemoryStream();
        //        //client.convertURI("http://www.google.com", Stream);

        //        // set HTTP response headers
        //        Response.Clear();
        //        Response.AddHeader("Content-Type", "application/pdf");
        //        Response.AddHeader("Cache-Control", "max-age=0");
        //        Response.AddHeader("Accept-Ranges", "none");
        //        Response.AddHeader("Content-Disposition", "attachment; filename='" + fileName + "'");
        //        System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
        //        System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
        //        MainContent.RenderControl(htmlWrite1);
        //        client.convertHtml(stringWrite1.ToString(), Stream);
        //        // send the generated PDF
        //        Stream.WriteTo(Response.OutputStream);
        //        Stream.Close();
        //        Response.Flush();
        //    }
        //    catch (pdfcrowd.Error why)
        //    {
        //        LogHandler.LogFatal((why.InnerException != null ? why.InnerException.Message : why.Message), why, this.GetType());
        //    }
        //}
        private void ExportDivContentToPDF()
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                string fileName = ApoTitle.InnerText + ".pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename='" + fileName + "'"));
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //For First DataTable
                System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
                //DataGrid myDataGrid1 = new DataGrid();
                //myDataGrid1.DataSource = dt1;
                //myDataGrid1.DataBind();
                MainContent.RenderControl(htmlWrite1);
                //For Second DataTable 
                //System.IO.StringWriter stringWrite2 = new System.IO.StringWriter();
                //System.Web.UI.HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);

                //gvRecuring.RenderControl(htmlWrite2);
                //You can add more DataTable
                StringReader sr = new StringReader(stringWrite1.ToString());
                Document pdfDoc = new Document(PageSize.LETTER.Rotate(), 7f, 7f, 7f, 0f);//new Rectangle(288f, 144f), 7f, 7f, 7f, 0f);
                pdfDoc.SetPageSize(PageSize.A4.Rotate());
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.End();
                GetApoForView("");
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void ExportDivContentToMsWord()
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                MainContent.RenderControl(htmlWrite);

                //build the content for the dynamic Word document
                //in HTML alongwith some Office specific style properties. 
                var strBody = new StringBuilder();

                strBody.Append("<html " +
                 "xmlns:o='urn:schemas-microsoft-com:office:office' " +
                 "xmlns:w='urn:schemas-microsoft-com:office:word'" +
                  "xmlns='http://www.w3.org/TR/REC-html40'>" +
                  "<head><title>Time</title>");

                //The setting specifies document's view after it is downloaded as Print
                //instead of the default Web Layout
                strBody.Append("<!--[if gte mso 9]>" +
                 "<xml>" +
                 "<w:WordDocument>" +
                 "<w:View>Print</w:View>" +
                 "<w:Zoom>100</w:Zoom>" +
                 "<w:DoNotOptimizeForBrowser/>" +
                 "</w:WordDocument>" +
                 "</xml>" +
                 "<![endif]-->");

                strBody.Append("<style>" +
                 "<!-- /* Style Definitions */" +
                 "@page Section1" +
                 "   {size:9.5in 10.0in; " +
                 "   margin:0.5in 1.0in 0.5in 1.0in ; " +
                 "   mso-header-margin:.5in; " +
                 "   mso-footer-margin:.5in; mso-paper-source:0;}" +
                 " div.Section1" +
                 "   {page:Section1;}" +
                 "-->" +
                 "@page Section1 {size:841.7pt 595.45pt;mso-page-orientation:landscape;margin:0.25in 0.25in 0.25in 0.25in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;} " +
                 "@page body1 {font-family:arial;font-size:10.5;}" +
                 "</style></head>");

                strBody.Append("<body class=body1 lang=EN-US style='tab-interval:.5in'>" +
                 "<div class=Section1>");
                strBody.Append(stringWrite);
                strBody.Append("</div></body></html>");

                //Force this content to be downloaded 
                //as a Word document with the name of your choice
                string fileName = ApoTitle.InnerText + ".doc";
                Response.AppendHeader("Content-Type", "application/msword");
                Response.AppendHeader("Content-disposition", "attachment; filename='" + fileName + "'");

                Response.Write(strBody.ToString());
                Response.Flush();
                GetApoForView("");
            }

            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }
        }
        private void ExportDivContentToMsExcel()
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                MainContent.RenderControl(htmlWrite);

                //build the content for the dynamic Word document
                //in HTML alongwith some Office specific style properties. 
                var strBody = new StringBuilder();

                strBody.Append("<html " +
                 "xmlns:o='urn:schemas-microsoft-com:office:office' " +
                 "xmlns:w='urn:schemas-microsoft-com:office:excel'" +
                  "xmlns='http://www.w3.org/TR/REC-html40'>" +
                  "<head><title>Time</title>");

                //The setting specifies document's view after it is downloaded as Print
                //instead of the default Web Layout
                strBody.Append("<!--[if gte mso 9]>" +
                 "<xml>" +
                 "<w:WordDocument>" +
                 "<w:View>Print</w:View>" +
                 "<w:Zoom>100</w:Zoom>" +
                 "<w:DoNotOptimizeForBrowser/>" +
                 "</w:WordDocument>" +
                 "</xml>" +
                 "<![endif]-->");

                strBody.Append("<style>" +
                 "<!-- /* Style Definitions */" +
                 "@page Section1" +
                 "   {size:9.5in 10.0in; " +
                 "   margin:1.0in 1.25in 1.0in 1.25in ; " +
                 "   mso-header-margin:.5in; " +
                 "   mso-footer-margin:.5in; mso-paper-source:0;}" +
                 " div.Section1" +
                 "   {page:Section1;}" +
                 "-->" +
                 "</style></head>");

                strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" +
                 "<div class=Section1>");
                strBody.Append(stringWrite);
                strBody.Append("</div></body></html>");

                //Force this content to be downloaded 
                //as a excel file with the name of your choice
                string fileName = ApoTitle.InnerText + ".xls";
                Response.AppendHeader("Content-Type", "application/excel");
                Response.AppendHeader("Content-disposition", "attachment; filename='" + fileName + "'");

                Response.Write(strBody.ToString());
                Response.Flush();
                GetApoForView("");
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }

        }
        private void PrintDiv()
        {
            Process printjob = new Process();
            printjob.StartInfo.FileName = MainContent.ToString();
            printjob.StartInfo.Verb = "Print";
            printjob.StartInfo.CreateNoWindow = true;
            printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            PrinterSettings setting = new PrinterSettings();
            setting.DefaultPageSettings.Landscape = true;
            printjob.Start();
        }
        public override void VerifyRenderingInServerForm(Control control)
        { }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }
    }
}