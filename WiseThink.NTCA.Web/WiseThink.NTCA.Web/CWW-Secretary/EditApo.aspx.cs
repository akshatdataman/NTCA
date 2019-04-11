using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL.Authorization;
using System.Data;
using WiseThink.NTCA.BAL;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class EditApo : BasePage
    {
        Boolean IsPageRefresh;
        DataTable dt;
        public bool IsEdit = false;
        string activityItemId = string.Empty;
        APO oApo = new APO();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNote1.Text = ConfigurationManager.AppSettings["Note1"];
            lblNote2.Text = ConfigurationManager.AppSettings["Note2"];
            lblNote3.Text = ConfigurationManager.AppSettings["Note3"];
            string LoggedInUser = AuthoProvider.User;

            if (Session["APOFileId"] != null)
            {
                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                DataSet dsTigerReserveId = APOBAL.Instance.GetAPOStateAndTigerReserveId(apoFileId);

                if (dsTigerReserveId != null)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                        oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    Session["TigerReserveId"] = oApo.TigerReserveId;
                    oApo.LoggedInUser = LoggedInUser;
                    //Session["APOFileId"] = null;
                }
            }

            //DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            //if (dsTigerReserveId != null)
            //{
            //    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
            //    if (dr[0] != DBNull.Value)
            //        oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
            //    oApo.LoggedInUser = LoggedInUser;
            //}
            if (!IsPostBack)
            {
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
                if (Session["IsEdit"] != null)
                {
                    IsEdit = (bool)Session["IsEdit"];
                    //btnNRCoreSave.Text = "Update";
                    //btnNRBufferSave.Text = "Update";
                    //btnRCoreSave.Text = "Update";
                    //btnRBufferSave.Text = "Update";
                }
                GetApoFormat();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Edit APO Page", "APO");
                if (!Request.UrlReferrer.ToString().Contains(Request.RawUrl.ToString()))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() {  $.session.remove('tableft');$.session.remove('tabmain');});</script>", false);
                }
            }
            else
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString())
                {
                    IsPageRefresh = true;
                    //GetApoFormat();
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"].ToString();
                //GetApoFormat();
            }

        }
       
        protected void gvNRCore_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateGridHeader(ProductGrid, gvNRCore);
            }
        }

        protected void gvNRBuffer_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateGridHeader(ProductGrid, gvNRBuffer);
            }
        }

        protected void gvRCore_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateGridHeader(ProductGrid, gvRCore);
            }
        }
        protected void gvRBuffer_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateGridHeader(ProductGrid, gvRBuffer);
            }
        }

        Decimal totalNRCore = 0.0m;
        protected void gvNRCore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalNRCore += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalNRCore.ToString();
            }
        }
        Decimal totalNRbuffer = 0.0m;
        protected void gvNRBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalNRbuffer += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalNRbuffer.ToString();
            }
        }

        Decimal totalRCore = 0.0m;
        protected void gvRCore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalRCore += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalRCore.ToString();
            }
        }
        Decimal totalRbuffer = 0.0m;
        protected void gvRBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalRbuffer += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalRbuffer.ToString();
            }
        }
        protected void gvNRCore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                LinkButton lnkdownload = (LinkButton)e.CommandSource;
                GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                string fileName = gvNRCore.DataKeys[gvrow.RowIndex].Value.ToString();
                string filePath = "~/Upload/ApoDocuments/" + fileName;
                Response.ContentType = "doc/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    Response.TransmitFile(Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                Response.End();

            }
            if (e.CommandName.Equals("detail"))
            {
                DataSet ds = ManageGuidelineBAL.Instance.GetGuidelineList();
                dt = ds.Tables[0];
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string cssPtParaNo = lnkView.Text;
                string[] strArray = cssPtParaNo.Split('.');
                string paraNumber = strArray[0] + "." + strArray[1];
                if (dt.Rows.Count > 0)
                {
                    IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                                 where i.Field<String>("CSSPTParaNumber").Equals(paraNumber)
                                                 select i;
                    DataTable detailTable = query.CopyToDataTable<DataRow>();
                    DetailsView1.DataSource = detailTable;
                    DetailsView1.DataBind();
                    APOBAL.Instance.GenerateGridFooter(sender, gvRBuffer);
                }
                dt = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#detailModal').modal('show')});</script>", false);

            }
            
        }
        protected void gvNRBuffer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                LinkButton lnkdownload = (LinkButton)e.CommandSource;
                GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                string filePath = "~/Upload/ApoDocuments/" + lnkdownload.Text;
                Response.ContentType = "doc/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + lnkdownload.Text + "\"");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    Response.TransmitFile(Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                Response.End();

            }
            if (e.CommandName.Equals("detail"))
            {
                DataSet ds = ManageGuidelineBAL.Instance.GetGuidelineList();
                dt = ds.Tables[0];
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string cssPtParaNo = lnkView.Text;
                string[] strArray = cssPtParaNo.Split('.');
                string paraNumber = strArray[0] + "." + strArray[1];
                if (dt.Rows.Count > 0)
                {
                    IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                                 where i.Field<String>("CSSPTParaNumber").Equals(paraNumber)
                                                 select i;
                    DataTable detailTable = query.CopyToDataTable<DataRow>();
                    DetailsView1.DataSource = detailTable;
                    DetailsView1.DataBind();
                    APOBAL.Instance.GenerateGridFooter(sender, gvRBuffer);
                }
                dt = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#detailModal').modal('show')});</script>", false);

            }

        }
        protected void gvRCore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                LinkButton lnkdownload = (LinkButton)e.CommandSource;
                GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                string filePath = "~/Upload/ApoDocuments/" + lnkdownload.Text;
                Response.ContentType = "doc/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + lnkdownload.Text + "\"");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    Response.TransmitFile(Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                Response.End();

            }
            if (e.CommandName.Equals("detail"))
            {
                DataSet ds = ManageGuidelineBAL.Instance.GetGuidelineList();
                dt = ds.Tables[0];
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string cssPtParaNo = lnkView.Text;
                string[] strArray = cssPtParaNo.Split('.');
                string paraNumber = strArray[0] + "." + strArray[1];
                if (dt.Rows.Count > 0)
                {
                    IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                                 where i.Field<String>("CSSPTParaNumber").Equals(paraNumber)
                                                 select i;
                    DataTable detailTable = query.CopyToDataTable<DataRow>();
                    DetailsView1.DataSource = detailTable;
                    DetailsView1.DataBind();
                    APOBAL.Instance.GenerateGridFooter(sender, gvRBuffer);
                }
                dt = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#detailModal').modal('show')});</script>", false);

            }

        }
        protected void gvRBuffer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                LinkButton lnkdownload = (LinkButton)e.CommandSource;
                GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                string filePath = "~/Upload/ApoDocuments/" + lnkdownload.Text;
                Response.ContentType = "doc/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + lnkdownload.Text + "\"");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    Response.TransmitFile(Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                Response.End();

            }
            if (e.CommandName.Equals("detail"))
            {
                DataSet ds = ManageGuidelineBAL.Instance.GetGuidelineList();
                dt = ds.Tables[0];
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string cssPtParaNo = lnkView.Text;
                string[] strArray = cssPtParaNo.Split('.');
                string paraNumber = strArray[0] + "." + strArray[1];
                if (dt.Rows.Count > 0)
                {
                    IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                                 where i.Field<String>("CSSPTParaNumber").Equals(paraNumber)
                                                 select i;
                    DataTable detailTable = query.CopyToDataTable<DataRow>();
                    DetailsView1.DataSource = detailTable;
                    DetailsView1.DataBind();
                    APOBAL.Instance.GenerateGridFooter(sender, gvRBuffer);
                }
                dt = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#detailModal').modal('show')});</script>", false);

            }

        }

        private void GetApoFormat()
        {
            DataSet dsApo = new DataSet();
            if (IsEdit == true)
                dsApo = APOBAL.Instance.GetApoForModification(Convert.ToInt32(Convert.ToInt32(Session["TigerReserveId"])));
            else
                dsApo = APOBAL.Instance.GetApoFormat();
            if (dsApo.Tables[0].Rows.Count > 0)
            {
                gvNRCore.DataSource = dsApo.Tables[0];
                gvNRCore.DataBind();
                LoadGridviewDraftData(gvNRCore);
            }
            else
            {
                gvNRCore.DataSource = dsApo.Tables[0];
                gvNRCore.DataBind();
                LoadGridviewDraftData(gvNRCore);
                //btnNRCoreSave.Style.Add("display", "none");
            }
            if (dsApo.Tables[1].Rows.Count > 0)
            {
                gvNRBuffer.DataSource = dsApo.Tables[1];
                gvNRBuffer.DataBind();
                LoadGridviewDraftData(gvNRBuffer);
            }
            else
            {
                gvNRBuffer.DataSource = dsApo.Tables[1];
                gvNRBuffer.DataBind();
                LoadGridviewDraftData(gvNRBuffer);
                //btnNRBufferSave.Style.Add("display", "none");
            }
            if (dsApo.Tables[2].Rows.Count > 0)
            {
                gvRCore.DataSource = dsApo.Tables[2];
                gvRCore.DataBind();
                LoadGridviewDraftData(gvRCore);
            }
            else
            {
                gvRCore.DataSource = dsApo.Tables[2];
                gvRCore.DataBind();
                LoadGridviewDraftData(gvRCore);
                //btnRCoreSave.Style.Add("display", "none");
            }
            if (dsApo.Tables[3].Rows.Count > 0)
            {
                gvRBuffer.DataSource = dsApo.Tables[3];
                gvRBuffer.DataBind();
                LoadGridviewDraftData(gvRBuffer);
            }
            else
            {
                gvRBuffer.DataSource = dsApo.Tables[3];
                gvRBuffer.DataBind();
                LoadGridviewDraftData(gvRBuffer);
                //btnRBufferSave.Style.Add("display", "none");
            }

        }
        private void ModifyApo(GridView gv)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (GridViewRow row in gv.Rows)
                {
                    Label lblActivityId = (Label)row.FindControl("lblActivityId");
                    Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                    LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                    TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                    TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                    TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                    TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                    if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
                    {
                        double total = Convert.ToDouble(txtNumberOfItem.Text) * Convert.ToDouble(txtUnitPrice.Text);
                        txtTotal.Text = Convert.ToString(total);
                        txtTotal.Enabled = false;
                    }
                    TextBox txtGPS = (TextBox)row.FindControl("txtGPS");
                    TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                    FileUpload fuUploadDocument =(FileUpload) row.FindControl("fuUploadDocument");

                    oApo.ActivityId = Convert.ToInt32(lblActivityId.Text);
                    oApo.ActivityItemId = Convert.ToInt32(lblActivityItemId.Text);
                    oApo.ParaNoTCP = txtParaNo.Text;
                    if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
                    {
                        oApo.NumberOfItems = Convert.ToDouble(txtNumberOfItem.Text);
                        oApo.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                        oApo.Total = Convert.ToDouble(txtTotal.Text);
                        oApo.IsFilled = true;
                    }
                    else
                    {
                        oApo.NumberOfItems = 0.0;
                        oApo.UnitPrice = 0.0;
                        oApo.Total = 0.0;
                        oApo.IsFilled = false;
                    }
                    oApo.Specification = txtSpcification.Text;
                    if (!string.IsNullOrEmpty(txtNumberOfItem.Text))
                        oApo.GPS = txtGPS.Text;
                    else
                        oApo.GPS = string.Empty;
                    oApo.Justification = txtJustification.Text;
                    if (fuUploadDocument.HasFile)
                    {
                        string fileName = Path.GetFileName(fuUploadDocument.PostedFile.FileName);
                        fuUploadDocument.PostedFile.SaveAs(Server.MapPath("~/Upload/ApoDocuments/") + fileName);
                        oApo.Document = fuUploadDocument.FileName;
                    }

                    sb.Append("{");
                    sb.AppendFormat("\"ID\":\"{0}\",\"ParaNoTCP\":\"{1}\",\"NumberofItems\":\"{2}\",\"Specification\":\"{3}\",\"UnitPrice\":\"{4}\",\"Total\":\"{5}\",\"GPS\":\"{6}\",\"Justification\":\"{7}\",\"Document\":\"{8}\"",
                                    oApo.APOId, oApo.ParaNoTCP, oApo.NumberOfItems, oApo.Specification, oApo.UnitPrice, oApo.Total, oApo.GPS, oApo.Justification, oApo.Document);
                    sb.Append("},");

                }

                if (sb.ToString().Length > 1)
                {
                    sb.Append("]");

                    string inputData = sb.ToString();

                    //APOBAL.Instance.UpdateAPO(inputData);
                    string consString = ConfigurationManager.ConnectionStrings["NtcaConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(consString))
                    {
                        SqlCommand cmd = new SqlCommand("spUpdateAPO", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inputJSON", inputData);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    //btnUpdate.Visible = false;
                    //lblMessage.Text = "Data updated successfully!";
                    //LoadData();
                }
                else
                {
                    //lblMessage.Text = "No value selected for update!";
                }
            }
            catch (SqlException ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("./ErrorPage.aspx", true);
                //lblMessage.Text = "error" + ex.ToString();
            }
        }
        #region old code
        //private void LoadGridviewDraftData(GridView gv)
        //{
        //    try
        //    {
        //        int trId = Convert.ToInt32(oApo.TigerReserveId);
        //        DataSet dsDraft = APOBAL.Instance.GetApoDraftData(trId);
        //        for (int i = 0; i < dsDraft.Tables.Count; i++)
        //        {
        //            if (dsDraft.Tables[i].Rows.Count > 0)
        //            {
        //                for (int j = 0; j < dsDraft.Tables[i].Rows.Count; j++)
        //                {
        //                    DataRow dr = dsDraft.Tables[i].Rows[j];
        //                    foreach (GridViewRow row in gv.Rows)
        //                    {
        //                        Label lblActivityId = (Label)row.FindControl("lblActivityId");
        //                        Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
        //                        LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
        //                        TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
        //                        TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
        //                        TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
        //                        TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
        //                        TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
        //                        TextBox txtGPS = (TextBox)row.FindControl("txtGPS");
        //                        TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
        //                        FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");
        //                        LinkButton lbDocumentName = (LinkButton)row.FindControl("clbDocumentFile");

        //                        if (lblActivityId.Text == dr["ActivityId"].ToString() && lblActivityItemId.Text == dr["ActivityItemId"].ToString())
        //                        {
        //                            //lbCssPT.Text = dr["ParaNoCSSPTGuidelines"].ToString();
        //                            if (IsEdit == true)
        //                            {
        //                                HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
        //                                hdnId.Value = dr["ID"].ToString();
        //                                HiddenField hdnTcpParaNo = (HiddenField)row.FindControl("hdnTcpParaNo");
        //                                hdnTcpParaNo.Value = dr["ParaNoTCP"].ToString();
        //                                txtParaNo.Text = hdnTcpParaNo.Value;
        //                            }
        //                            else
        //                                txtParaNo.Text = dr["ParaNoTCP"].ToString();
        //                            txtNumberOfItem.Text = dr["NumberOfItems"].ToString();
        //                            txtSpcification.Text = dr["Specification"].ToString();
        //                            txtUnitPrice.Text = dr["UnitPrice"].ToString();
        //                            txtTotal.Text = dr["Total"].ToString();
        //                            txtGPS.Text = dr["GPS"].ToString();
        //                            txtJustification.Text = dr["Justification"].ToString();
        //                            if (!string.IsNullOrEmpty(Convert.ToString(dr["Document"])))
        //                                lbDocumentName.Text = dr["Document"].ToString();
        //                            else
        //                                lbDocumentName.Text = string.Empty;
                                    
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
        //        Response.RedirectPermanent("~/ErrorPage.aspx", false);
        //    }
        //}
        //private void InsertGridviewToDatabase(GridView gv)
        //{
        //    try
        //    {
        //        foreach (GridViewRow row in gv.Rows)
        //        {

        //            Label lblActivityId = (Label)row.FindControl("lblActivityId");
        //            Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
        //            LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
        //            TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
        //            TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
        //            TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
        //            TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
        //            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
        //            if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
        //            {
        //                double total = Convert.ToDouble(txtNumberOfItem.Text) * Convert.ToDouble(txtUnitPrice.Text);
        //                txtTotal.Text = Convert.ToString(total);
        //                txtTotal.Enabled = false;
        //            }
        //            TextBox txtGPS = (TextBox)row.FindControl("txtGPS");
        //            TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
        //            FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");
        //            LinkButton lbDocumentName = (LinkButton)row.FindControl("clbDocumentFile");
        //            oApo.ActivityId = Convert.ToInt32(lblActivityId.Text);
        //            oApo.ActivityItemId = Convert.ToInt32(lblActivityItemId.Text);
        //            oApo.ParaNoTCP = txtParaNo.Text;
        //            if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text) && !string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                oApo.NumberOfItems = Convert.ToDouble(txtNumberOfItem.Text);
        //                oApo.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
        //                oApo.Total = Convert.ToDouble(txtTotal.Text);
        //                oApo.IsFilled = true;
        //                oApo.Specification = txtSpcification.Text;
        //                if (!string.IsNullOrEmpty(txtNumberOfItem.Text))
        //                    oApo.GPS = txtGPS.Text;
        //                else
        //                    oApo.GPS = string.Empty;
        //                oApo.Justification = txtJustification.Text;
        //                if (fuUploadDocument.HasFile)
        //                {
        //                    string fileName = Path.GetFileName(fuUploadDocument.PostedFile.FileName);
        //                    fuUploadDocument.PostedFile.SaveAs(Server.MapPath("~/Upload/ApoDocuments/") + fileName);
        //                    oApo.Document = fuUploadDocument.FileName;
        //                }
        //                else if (!string.IsNullOrEmpty(lbDocumentName.Text))
        //                    oApo.Document = lbDocumentName.Text;
        //                else
        //                    oApo.Document = string.Empty;
        //                if (Session["IsEdit"] == null)
        //                    APOBAL.Instance.SubmitAPO(oApo);
        //                else
        //                {
        //                    //Label Id = (Label)row.FindControl("lblId");
        //                    HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
        //                    //oApo.APOId = Convert.ToInt32(Id.Text);
        //                    oApo.APOId = Convert.ToInt32(hdnId.Value);
        //                    oApo.LoggedInUser = AuthoProvider.User;
        //                    APOBAL.Instance.ModifyAPO(oApo);
        //                    //Session["IsEdit"] = null;
        //                }
        //            }
        //            else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
        //                vmError.Message = strError;
        //                FlashMessage.ErrorMessage(vmError.Message);
        //                return;
        //            }
        //            else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
        //                vmError.Message = strError;
        //                FlashMessage.ErrorMessage(vmError.Message);
        //                return;
        //            }
        //            else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
        //                vmError.Message = strError;
        //                FlashMessage.ErrorMessage(vmError.Message);
        //                return;
        //            }

        //            else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
        //                vmError.Message = strError;
        //                FlashMessage.ErrorMessage(vmError.Message);
        //                return;
        //            }
        //            else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
        //                vmError.Message = strError;
        //                FlashMessage.ErrorMessage(vmError.Message);
        //                return;
        //            }
        //            else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
        //                vmError.Message = strError;
        //                FlashMessage.ErrorMessage(vmError.Message);
        //                return;
        //            }
        //            else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
        //                vmError.Message = strError;
        //                FlashMessage.ErrorMessage(vmError.Message);
        //                return;
        //            }
        //            if (Session["IsEdit"] != null && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
        //            {
        //                //Label Id = (Label)row.FindControl("lblId");
        //                HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
        //                //oApo.APOId = Convert.ToInt32(Id.Text);
        //                oApo.APOId = Convert.ToInt32(hdnId.Value);
        //                oApo.LoggedInUser = AuthoProvider.User;
        //                APOBAL.Instance.ModifyAPO(oApo);
        //                string strSuccess = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString();//"Data has been updated successfully.";
        //                vmSuccess.Message = strSuccess;
        //                FlashMessage.InfoMessage(vmSuccess.Message);
        //                DisplayErrorMessage.Style.Add("display", "none");
        //                DisplaySuccessMessage.Style.Add("display", "block");
        //            }
        //            else
        //            {
        //            }

        //        }
        //        UserBAL.Instance.InsertAuditTrailDetail("Modified the APO", "APO");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
        //        Response.RedirectPermanent("~/ErrorPage.aspx", false);
        //    }
        //}
        #endregion

        private void LoadGridviewDraftData(GridView gv)
        {
            try
            {
                int trId = Convert.ToInt32(oApo.TigerReserveId);
                DataSet dsDraft = APOBAL.Instance.GetApoDraftData(trId);
                for (int i = 0; i < dsDraft.Tables.Count; i++)
                {
                    if (dsDraft.Tables[i].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsDraft.Tables[i].Rows.Count; j++)
                        {
                            DataRow dr = dsDraft.Tables[i].Rows[j];
                            foreach (GridViewRow row in gv.Rows)
                            {
                                Label lblActivityId = (Label)row.FindControl("lblActivityId");
                                Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                                LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                                TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                                TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                                TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                                TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                //Label txtGPS = (Label)row.FindControl("lblGPS");
                                TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                                FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");
                                LinkButton lbDocumentName = (LinkButton)row.FindControl("clbDocumentFile");
                                //DetailsView detailsView = (DetailsView)row.FindControl("GpsDetailsView");
                                //lbDocumentName.Text = "Document";
                                GridView gpsDetails = (GridView)row.FindControl("gvGpsDetails");

                                if (lblActivityId.Text == dr["ActivityId"].ToString() && lblActivityItemId.Text == dr["ActivityItemId"].ToString())
                                {
                                    //lbCssPT.Text = dr["ParaNoCSSPTGuidelines"].ToString();
                                    if (IsEdit == true)
                                    {
                                        HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                                        hdnId.Value = dr["ID"].ToString();
                                        HiddenField hdnTcpParaNo = (HiddenField)row.FindControl("hdnTcpParaNo");
                                        hdnTcpParaNo.Value = dr["ParaNoTCP"].ToString();
                                        txtParaNo.Text = hdnTcpParaNo.Value;
                                    }
                                    else
                                        txtParaNo.Text = dr["ParaNoTCP"].ToString();
                                    txtNumberOfItem.Text = dr["NumberOfItems"].ToString();
                                    txtSpcification.Text = dr["Specification"].ToString();
                                    txtUnitPrice.Text = dr["UnitPrice"].ToString();
                                    txtTotal.Text = dr["Total"].ToString();

                                    //GetGPSDetails(gv, oApo.TigerReserveId, Convert.ToInt32(lblActivityItemId.Text));
                                    //DataSet dsGps = APOBAL.Instance.GetGPS(oApo.TigerReserveId, Convert.ToInt32(lblActivityItemId.Text));
                                    //if (dsGps.Tables[0].Rows.Count > 0)
                                    //{
                                    //    detailsView.DataSource = dsGps.Tables[0];
                                    //    detailsView.DataBind();
                                    //}

                                    //if (string.IsNullOrEmpty(dr["GPS"].ToString()) || dr["GPS"].ToString() == "0.00000")
                                    //    txtGPS.Text = string.Empty;
                                    //else
                                    //    txtGPS.Text = dr["GPS"].ToString();
                                    txtJustification.Text = dr["Justification"].ToString();
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Document"])))
                                        lbDocumentName.Text = dr["Document"].ToString();
                                    else
                                        lbDocumentName.Text = string.Empty;
                                }
                                DataSet dsGps = APOBAL.Instance.GetGPS(oApo.TigerReserveId, Convert.ToInt32(lblActivityItemId.Text),oApo.SubItem.ToString(),"");
                                if (dsGps.Tables[0].Rows.Count > 0)
                                {
                                    gpsDetails.DataSource = dsGps.Tables[0];
                                    gpsDetails.DataBind();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
            }
        }
        private bool ValidateMandatoryApoField(GridView gv)
        {
            bool IsValid = true;
            RegularExpressionValidator rev = new RegularExpressionValidator();
            RequiredFieldValidator rfv = new RequiredFieldValidator();
            foreach (GridViewRow row in gv.Rows)
            {
                TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
                {
                    string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    IsValid = false;
                    return IsValid;
                }
                else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
                {
                    string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    IsValid = false;
                    return IsValid;
                }
                else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtJustification.Text))
                {
                    string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    IsValid = false;
                    return IsValid;
                }

                else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
                {
                    string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    IsValid = false;
                    return IsValid;
                }
                else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtJustification.Text))
                {
                    string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    IsValid = false;
                    return IsValid;
                }
                else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtJustification.Text))
                {
                    string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    IsValid = false;
                    return IsValid;
                }
                else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
                {
                    IsValid = true;
                    return IsValid;
                }
            }

            return IsValid;
        }
        private void InsertGridviewToDatabase(GridView gv)
        {
            try
            {
                RegularExpressionValidator rev = new RegularExpressionValidator();
                RequiredFieldValidator rfv = new RequiredFieldValidator();
                foreach (GridViewRow row in gv.Rows)
                {

                    Label lblActivityId = (Label)row.FindControl("lblActivityId");
                    Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                    LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                    TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                    TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                    TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                    TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                    if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
                    {
                        double total = Convert.ToDouble(txtNumberOfItem.Text) * Convert.ToDouble(txtUnitPrice.Text);
                        txtTotal.Text = Convert.ToString(total);
                        txtTotal.Enabled = false;
                    }

                    //RequiredFieldValidator rfvNumberOfItem = (RequiredFieldValidator)row.FindControl("rfvNumberOfItem");
                    //RequiredFieldValidator rfvUnitPrice = (RequiredFieldValidator)row.FindControl("rfvUnitPrice");
                    //RequiredFieldValidator rfvJustification = (RequiredFieldValidator)row.FindControl("rfvJustification");

                    //Label txtGPS = (Label)row.FindControl("lblGPS");
                    TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                    FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");
                    LinkButton lbDocumentName = (LinkButton)row.FindControl("clbDocumentFile");


                    oApo.ActivityId = Convert.ToInt32(lblActivityId.Text);
                    oApo.ActivityItemId = Convert.ToInt32(lblActivityItemId.Text);
                    oApo.ParaNoTCP = txtParaNo.Text;
                    //if (!string.IsNullOrEmpty(txtNumberOfItem.Text) || !string.IsNullOrEmpty(txtNumberOfItem.Text) || !string.IsNullOrEmpty(txtJustification.Text))
                    //{
                    //    rfvNumberOfItem.Enabled = true;
                    //    rfvUnitPrice.Enabled = true;
                    //    rfvJustification.Enabled = true;
                    //    return;
                    //}
                    if (!ValidateMandatoryApoField(gv))
                        return;
                    if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text) && !string.IsNullOrEmpty(txtJustification.Text))
                    {
                        oApo.NumberOfItems = Convert.ToDouble(txtNumberOfItem.Text);
                        oApo.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                        oApo.Total = Convert.ToDouble(txtTotal.Text);
                        oApo.IsFilled = true;
                        oApo.Specification = txtSpcification.Text;

                        //if (!string.IsNullOrEmpty(txtGPS.Text))
                        //{
                        //    oApo.GPS = txtGPS.Text;
                        //    //rev.Enabled = true;
                        //}
                        //else
                        //{
                        //    oApo.GPS = string.Empty;
                        //    //rev.Enabled = false;
                        //}
                        oApo.Justification = txtJustification.Text;
                        if (fuUploadDocument.HasFile)
                        {
                            string fileName = Path.GetFileName(fuUploadDocument.PostedFile.FileName);
                            fuUploadDocument.PostedFile.SaveAs(Server.MapPath("~/Upload/ApoDocuments/") + fileName);
                            oApo.Document = fuUploadDocument.FileName;
                        }
                        else if (!string.IsNullOrEmpty(lbDocumentName.Text))
                            oApo.Document = lbDocumentName.Text;
                        else
                            oApo.Document = string.Empty;

                        if (Session["IsEdit"] == null)
                        {
                            APOBAL.Instance.SubmitAPO(oApo);
                            string strSuccess = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();//"Data has been saved successfully.";
                            vmSuccess.Message = strSuccess;
                            FlashMessage.InfoMessage(vmSuccess.Message);
                            //DisplayErrorMessage.Style.Add("display", "none");
                            //DisplaySuccessMessage.Style.Add("display", "block");
                        }
                        else
                        {
                            //Label Id = (Label)row.FindControl("lblId");
                            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                            //oApo.APOId = Convert.ToInt32(Id.Text);
                            oApo.APOId = Convert.ToInt32(hdnId.Value);
                            oApo.LoggedInUser = AuthoProvider.User;
                            APOBAL.Instance.ModifyAPO(oApo);
                            string strSuccess = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString();//"Data has been updated successfully.";
                            vmSuccess.Message = strSuccess;
                            FlashMessage.InfoMessage(vmSuccess.Message);
                            DisplayErrorMessage.Style.Add("display", "none");
                            DisplaySuccessMessage.Style.Add("display", "block");

                            //Session["IsEdit"] = null;
                        }
                    }
                    
                    if (Session["IsEdit"] != null && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtJustification.Text))
                    {
                        //Label Id = (Label)row.FindControl("lblId");
                        HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                        //oApo.APOId = Convert.ToInt32(Id.Text);
                        oApo.APOId = Convert.ToInt32(hdnId.Value);
                        oApo.ParaNoTCP = null;
                        oApo.NumberOfItems = 0;
                        oApo.Specification = null;
                        oApo.UnitPrice = 0;
                        oApo.GPS = null;
                        oApo.Justification = null;
                        oApo.Document = null;
                        oApo.LoggedInUser = AuthoProvider.User;
                        APOBAL.Instance.ModifyAPO(oApo);
                        string strSuccess = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString();//"Data has been updated successfully.";
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                        DisplayErrorMessage.Style.Add("display", "none");
                        DisplaySuccessMessage.Style.Add("display", "block");
                    }
                    else
                    {
                    }
                    LoadGridviewDraftData(gv);
                }
                UserBAL.Instance.InsertAuditTrailDetail("Saved in Draft / Modified APO", "APO");

            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }

        protected void btnNRCoreSave_Click(object sender, EventArgs e)
        {
            try
            {
                oApo.AreaId = 1;
                oApo.ActivityTypeId = 1;
                //ModifyApo(gvNRCore);
                InsertGridviewToDatabase(gvNRCore);
                //LoadGridviewDraftData(gvNRCore);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
            finally
            {
                if (oApo != null)
                {
                    oApo = null;
                }
            }
        }

        protected void btnNRBufferSave_Click(object sender, EventArgs e)
        {
            try
            {
                oApo.AreaId = 2;
                oApo.ActivityTypeId = 1;
                InsertGridviewToDatabase(gvNRBuffer);
                //LoadGridviewDraftData(gvNRBuffer);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
            finally
            {
                if (oApo != null)
                {
                    oApo = null;
                }
            }
        }


        protected void btnRCoreSave_Click(object sender, EventArgs e)
        {
            try
            {
                oApo.AreaId = 1;
                oApo.ActivityTypeId = 2;
                InsertGridviewToDatabase(gvRCore);
                //LoadGridviewDraftData(gvRCore);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
            finally
            {
                if (oApo != null)
                {
                    oApo = null;
                }
            }
        }

        protected void btnRBufferSave_Click(object sender, EventArgs e)
        {
            try
            {
                oApo.AreaId = 2;
                oApo.ActivityTypeId = 2;
                InsertGridviewToDatabase(gvRBuffer);
                //LoadGridviewDraftData(gvRBuffer);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
            finally
            {
                if (oApo != null)
                {
                    oApo = null;
                }
            }
        }

        protected void btnCoreCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("Home.aspx", false);
        }

        protected void btnBufferCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("FieldDirectorHome.aspx", false);
        }

        protected void btnRCoreCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("Home.aspx", false);
        }

        protected void btnRBufferCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("Home.aspx", false);
        }
        #region Delete Apo Entries
        protected void imgbtnDeleteNrCore_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvNRCore.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                APOBAL.Instance.DeleteApoEntry(oApo.TigerReserveId, _activityItemId);
                //LoadGridviewDraftData(gvNRCore);
                //GetApoFormat();
                string strSuccess = ConfigurationManager.AppSettings["DeleteSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void imgbtnDeleteNrBuffer_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvNRBuffer.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                APOBAL.Instance.DeleteApoEntry(oApo.TigerReserveId, _activityItemId);
                //LoadGridviewDraftData(gvNRBuffer);
                string strSuccess = ConfigurationManager.AppSettings["DeleteSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void imgbtnDeleteRcore_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvRCore.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                APOBAL.Instance.DeleteApoEntry(oApo.TigerReserveId, _activityItemId);
                //LoadGridviewDraftData(gvRCore);
                string strSuccess = ConfigurationManager.AppSettings["DeleteSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void imgbtnDeleteRbuffer_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvRBuffer.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                APOBAL.Instance.DeleteApoEntry(oApo.TigerReserveId, _activityItemId);
                //LoadGridviewDraftData(gvRBuffer);
                string strSuccess = ConfigurationManager.AppSettings["DeleteSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
                string rawUrl = Request.RawUrl.ToString();
                Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        #endregion

        #region To Add Multiple GPS Coordinates
        protected void imgbtnAddNrCoreGPS_click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Visible = false;
                if (!IsPageRefresh)
                {
                    Session["activityItemId"] = null;
                    Session["rowIndex"] = null;
                    ImageButton ibtn = sender as ImageButton;
                    int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                    activityItemId = gvNRCore.DataKeys[rowIndex].Values[0].ToString();
                    Session["activityItemId"] = activityItemId;
                    Session["rowIndex"] = rowIndex;
                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void imgbtnAddNrBufferGPS_click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Visible = false;
                if (!IsPageRefresh)
                {
                    Session["activityItemId"] = null;
                    Session["rowIndex"] = null;
                    ImageButton ibtn = sender as ImageButton;
                    int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                    activityItemId = gvNRBuffer.DataKeys[rowIndex].Values[0].ToString();
                    Session["activityItemId"] = activityItemId;
                    Session["rowIndex"] = rowIndex;
                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void imgbtnAddRCoreGPS_click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Visible = false;
                if (!IsPageRefresh)
                {
                    Session["activityItemId"] = null;
                    Session["rowIndex"] = null;
                    ImageButton ibtn = sender as ImageButton;
                    int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                    activityItemId = gvRCore.DataKeys[rowIndex].Values[0].ToString();
                    Session["activityItemId"] = activityItemId;
                    Session["rowIndex"] = rowIndex;

                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void imgbtnAddRBufferGPS_click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Visible = false;
                if (!IsPageRefresh)
                {
                    Session["activityItemId"] = null;
                    Session["rowIndex"] = null;
                    ImageButton ibtn = sender as ImageButton;
                    int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                    activityItemId = gvRBuffer.DataKeys[rowIndex].Values[0].ToString();
                    Session["activityItemId"] = activityItemId;
                    Session["rowIndex"] = rowIndex;
                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int tigerReserveId = 0;
            int subItemId = 0;
            if (string.IsNullOrEmpty(txtGPS.Text))
            {
                lblMessage.Text = "Please enter the Gps coordinate.";
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
                lblMessage.Visible = false;
            if(Session["TigerReserveId"] !=null)
            tigerReserveId = Convert.ToInt32(Session["TigerReserveId"]);// oApo.TigerReserveId;
            if (Session["activityItemId"] != null)
                activityItemId = Session["activityItemId"].ToString();
            if (Session["subItemId"] != null)
                subItemId = Convert.ToInt32(Session["subItemId"].ToString());
            APOBAL.Instance.AddGPS(tigerReserveId, Convert.ToInt32(activityItemId),subItemId, txtGPS.Text,"");
            txtGPS.Text = string.Empty;
            GetGPSDetails(tigerReserveId, Convert.ToInt32(activityItemId));
            LoadGridviewDraftData(gvNRCore);
            lblMessage.Text = "Gps coordinate added sucessfully, please click on update after closing this window.";
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        private void GetGPSDetails(int tigerReserveId, int activityItemId)
        {
            DataSet dsGps;
            cgvGps.DataSource = null;
            cgvGps.DataBind();
            dsGps = APOBAL.Instance.GetGPS(tigerReserveId, activityItemId, "","AddGPS");
            if (dsGps.Tables[0].Rows.Count > 0)
            {
                cgvGps.DataSource = dsGps;
                cgvGps.DataBind();
            }
        }

        protected void cgvGps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvGps.PageIndex = e.NewPageIndex;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        }
        protected void cgvGps_PageIndexChanged(object sender, EventArgs e)
        {
            cgvGps.EditIndex = -1;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        }
        protected void cgvGps_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvGps.EditIndex = e.NewEditIndex;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        }

        protected void cgvGps_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cgvGps.EditIndex = -1;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        }

        protected void cgvGps_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label gpsId = cgvGps.Rows[e.RowIndex].FindControl("lblGPSId") as Label;
                int _gpsId = Convert.ToInt32(gpsId.Text);
                Label activityItemid = cgvGps.Rows[e.RowIndex].FindControl("lblActivityItemId") as Label;
                int activityItemId = Convert.ToInt32(activityItemid.Text);
                TextBox gps = cgvGps.Rows[e.RowIndex].FindControl("txtGPS") as TextBox;
                string strgps = gps.Text;
                APOBAL.Instance.UpdateGPS(_gpsId, oApo.TigerReserveId, activityItemId, strgps);
                cgvGps.EditIndex = -1;
                string strSuccess = "GPS has been updated successfully.";
                GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }
        protected void cgvGps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                
                Label gpsId = cgvGps.Rows[e.RowIndex].FindControl("lblGPSId") as Label;
                int _gpsId = Convert.ToInt32(gpsId.Text);
                APOBAL.Instance.DeleteGPS(_gpsId);
                GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
                string strSuccess = "GPS Coordinate deleted successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }


        #endregion
    }
}