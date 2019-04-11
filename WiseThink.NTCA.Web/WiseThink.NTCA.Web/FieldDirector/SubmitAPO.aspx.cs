using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.App_Code;
using WiseThink.NTCA.Web.UserControls;
using System.Web.Services;
using Newtonsoft.Json;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class SubmitAPO1 : BasePage
    {
        Boolean IsPageRefresh;
        DataTable dt;
        public bool IsEdit = false;
        string activityItemId = string.Empty;
        APO oApo = new APO();
        int rowIndexTemp;
        public static APO objAPO;
        public static SubmitAPO1 SessionAPO = new SubmitAPO1();
        public static int TigerReserveID;

        [System.Web.Services.WebMethod]
        public static void SetButtonFlagValue(string ButtonName)
        {
            if (ButtonName == "Edit")
                SessionAPO.Session["EditButtonClicked"] = "true";
            if (ButtonName == "Delete")
                SessionAPO.Session["DeleteButtonClicked"] = "true";
        }

        [System.Web.Services.WebMethod]
        public static string SetModelPopupFlag(string strpath)
        {
            Page objp = new Page();
            objp.Session["IsModelPopOpen"] = strpath;
            return strpath;
        }

        [System.Web.Services.WebMethod]
        public static void SetValueinSession(string apo)
        {
            objAPO = JsonConvert.DeserializeObject<APO>(apo);
            if (objAPO.SubItemId.ToString() == "")
                objAPO.SubItemId = 0;
        }

        [System.Web.Services.WebMethod]
        public static void DowloadApoAttachment()
        {
            Page objp = new Page();
            if (!String.IsNullOrEmpty(objp.Session["Document"].ToString()))
            {
                objp.Session["IsModelPopOpen"] = "True";
                string filePath = "~/Upload/ApoDocuments/" + objp.Session["Document"].ToString();
                HttpContext.Current.Response.ContentType = "image/png";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + objp.Session["Document"].ToString() + "\"");
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                {
                    HttpContext.Current.Response.TransmitFile(HttpContext.Current.Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(objp, HttpContext.Current.GetType(), "File Error", "alert('File does not exists');", true);
                }
                HttpContext.Current.Response.End();
            }
        }

        [System.Web.Services.WebMethod]
        public static bool DeleteAPOEntries(string apo)
        {
            APO objApo = new APO();
            objApo = JsonConvert.DeserializeObject<APO>(apo);
            string activityItemId = objApo.ActivityItemId.ToString();//objp.Session["activityItemId"].ToString(); //gvNRCore.DataKeys[rowIndex].Values[0].ToString();
            int _activityItemId = Convert.ToInt32(activityItemId);
            APOBAL.Instance.DeleteApoEntry(TigerReserveID, _activityItemId);

            DataSet dtstatus = APOBAL.Instance.GetDeleteEntryStatus(TigerReserveID, _activityItemId);
            if (dtstatus.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [System.Web.Services.WebMethod]
        public static bool SaveAsDraftData()
        {
            SubmitAPO1 objp = new SubmitAPO1();
            try
            {
                objp.Session["IsDraftMode"] = "True";
                if (!objp.CheckApoStatusBeforeAnyAction())
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, objp.GetType());
                objp.Response.RedirectPermanent("~/ErrorPage.aspx", false);
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //ImageButton ibtn = sender as ImageButton;
                ////DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                //if (ibtn != null)
                //    rowIndexTemp = Convert.ToInt32(ibtn.Attributes["RowIndex"]);

                lblNote1.Text = ConfigurationManager.AppSettings["Note1"];
                lblNote2.Text = ConfigurationManager.AppSettings["Note2"];
                lblNote3.Text = ConfigurationManager.AppSettings["Note3"];

                Session["EditButtonClicked"] = "false";
                Session["DeleteButtonClicked"] = "false";
                Session["CallFrom"] = "SubmitAPO";
                if (Session["IsEdit"] == null)
                    IsEdit = false;
                else
                    IsEdit = true;

                string LoggedInUser = AuthoProvider.User;
                DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
                if (dsTigerReserveId != null)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                        oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    TigerReserveID = oApo.TigerReserveId;
                    oApo.LoggedInUser = LoggedInUser;
                }
                if (!IsPostBack)
                {
                    //string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                    //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                    ViewState["postids"] = Guid.NewGuid().ToString();
                    Session["postid"] = ViewState["postids"].ToString();
                    Session["IsModelPopOpen"] = null;
                    Session["AddClicked"] = null;
                    ViewState["FLAG"] = false;
                    try
                    {
                        if (Session["IsDraftMode"].ToString() == "")
                            Session["IsDraftMode"] = "";
                    }
                    catch
                    {
                        Session["IsDraftMode"] = "";
                    }
                    string strUrl = Request.UrlReferrer.AbsolutePath;
                    if (!string.IsNullOrEmpty(strUrl) && strUrl.Contains("SubmitAPO.aspx"))
                    {
                        Session["IsEdit"] = null;
                    }
                    if (Session["IsEdit"] != null)
                    {
                        IsEdit = (bool)Session["IsEdit"];
                        btnNRCoreSave.Text = "Update";
                        btnNRBufferSave.Text = "Update";
                        btnRCoreSave.Text = "Update";
                        btnRBufferSave.Text = "Update";
                    }
                    else
                    {
                        btnNRCoreSave.Text = "Save As Draft";
                        btnNRBufferSave.Text = "Save As Draft";
                        btnRCoreSave.Text = "Save As Draft";
                        btnRBufferSave.Text = "Save As Draft";
                    }
                    GetApoFormat();
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Submit APO Page", "APO");
                    if (!Request.UrlReferrer.ToString().Contains(Request.RawUrl.ToString()))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() {  $.session.remove('tableft');$.session.remove('tabmain');});</script>", false);
                    }
                    if (Session["IsDraftMode"].ToString() == "True")
                    {
                        Session["IsDraftMode"] = "False";
                        string strSuccess = ConfigurationManager.AppSettings["DraftSaveSuccessMes"].ToString();
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                    }
                    else if (Session["IsDraftMode"].ToString() == "Not")
                    {
                        Session["IsDraftMode"] = "False";
                        string strError = ConfigurationManager.AppSettings["CheckApoStatus"];
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                    }
                }
                else
                {
                    if ((bool)ViewState["FLAG"]) GetApoFormat();

                    //if ((bool)Session["EditButtonClicked"])
                    //    GetApoFormat();
                    if (ViewState["postids"].ToString() != Session["postid"].ToString())
                    {
                        IsPageRefresh = true;
                    }

                    if (Session["IsDraftMode"].ToString() == "True")
                    {
                        Session["IsDraftMode"] = "False";
                        string strSuccess = ConfigurationManager.AppSettings["DraftSaveSuccessMes"].ToString();
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                    }
                    Session["postid"] = System.Guid.NewGuid().ToString();
                    ViewState["postids"] = Session["postid"].ToString();
                    //GetApoFormat();
                }

                if (!Session["IsInsert"].IsNull())
                {
                    string strSuccess = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();//"Data has been saved successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.InfoMessage(vmSuccess.Message);
                    DisplayErrorMessage.Style.Add("display", "none");
                    DisplaySuccessMessage.Style.Add("display", "block");
                    Session["IsInsert"] = null;
                }
                else if (!Session["IsUpdate"].IsNull())
                {
                    string strSuccess = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString();//"Data has been updated successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.InfoMessage(vmSuccess.Message);
                    DisplayErrorMessage.Style.Add("display", "none");
                    DisplaySuccessMessage.Style.Add("display", "block");
                    Session["IsUpdate"] = null;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        private void SetInitialRow(GridView gv)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn(DataBaseFields.ParaNoTCP, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.SubItem, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.NumberOfItems, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.UnitPrice, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.Total, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.GPS, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.Justification, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.Document, typeof(string)));
            dt.Columns.Add(new DataColumn("Action", typeof(string)));
            dr = dt.NewRow();
            dr[DataBaseFields.ParaNoTCP] = string.Empty;
            dr[DataBaseFields.SubItem] = string.Empty;
            dr[DataBaseFields.NumberOfItems] = string.Empty;
            dr[DataBaseFields.UnitPrice] = string.Empty;
            dr[DataBaseFields.Total] = string.Empty;
            dr[DataBaseFields.GPS] = string.Empty;
            dr[DataBaseFields.Justification] = string.Empty;
            dr[DataBaseFields.Document] = string.Empty;
            dr["Action"] = string.Empty;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gv.DataSource = dt;
            gv.DataBind();
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

        protected void gvNRCore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "download")
                {
                    LinkButton lnkdownload = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                    //string fileName = gvNRCore.DataKeys[gvrow.RowIndex].Value.ToString();
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
                if (e.CommandName == "AddSubItem")
                {
                    // Retrieve the row index stored in the 
                    // CommandArgument property.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Retrieve the row that contains the button 
                    // from the Rows collection.
                    GridViewRow row = gvNRCore.Rows[index];

                    // Add code here to add the item to the shopping cart.
                }
                if (e.CommandName == "update-something")
                {
                    //int rowIndex = Convert.ToInt32(e.CommandArgument);
                    //if (rowIndex != 0 && rowIndex < 2)
                    //    rowIndex = rowIndex + 1;
                    //else
                    //    rowIndex = rowIndex * 2;
                    //gvNRCore.SelectedIndex = rowIndex;
                    //try
                    //{
                    //    lblMessage.Visible = false;
                    //    if (!IsPageRefresh)
                    //    {
                    //        Session["activityItemId"] = null;
                    //        Session["subItemId"] = null;
                    //        Session["rowIndex"] = null;
                    //        ImageButton ibtn = sender as ImageButton;
                    //        //int rowIndex = gvNRCore.SelectedIndex;
                    //        activityItemId = gvNRCore.DataKeys[rowIndex].Values[0].ToString();
                    //        Session["activityItemId"] = activityItemId;
                    //        Session["rowIndex"] = rowIndex;
                    //        foreach (GridViewRow row in gvNRCore.Rows)
                    //        {
                    //            if (row.DataItemIndex.Equals(rowIndex))
                    //            {
                    //                //Label lblSubItemId = (Label)row.FindControl("lblSubItemId");
                    //                //Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                    //                DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                    //                if (ddlSubItem.SelectedIndex == 0)
                    //                {
                    //                    Session["subItemId"] = "0";
                    //                }
                    //                else
                    //                {
                    //                    Session["subItemId"] = ddlSubItem.SelectedValue;
                    //                }
                    //            }
                    //        }
                    //        GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);
                    //        Session["IsModelPopOpen"] = "True";
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                    //    Response.RedirectPermanent("~/ErrorPage.aspx", false);
                    //}
                }
            }

            catch (FileNotFoundException ex)
            {
                string strInvalid = ex.Message;
                vmError.Message = strInvalid;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
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
                }
                dt = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#detailModal').modal('show')});</script>", false);
                GetApoFormat();
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

        protected void gvNRBuffer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
                    }
                    dt = null;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#detailModal').modal('show')});</script>", false);
                }
                if (e.CommandName == "update-something")
                {
                    //int rowIndex = Convert.ToInt32(e.CommandArgument);
                    //if (rowIndex != 0 && rowIndex < 2)
                    //    rowIndex = rowIndex + 1;
                    //else
                    //    rowIndex = rowIndex * 2;
                    //gvNRBuffer.SelectedIndex = rowIndex;
                    //try
                    //{
                    //    lblMessage.Visible = false;
                    //    if (!IsPageRefresh)
                    //    {
                    //        Session["activityItemId"] = null;
                    //        Session["subItemId"] = null;
                    //        Session["rowIndex"] = null;
                    //        ImageButton ibtn = sender as ImageButton;
                    //        //int rowIndex = gvNRBuffer.SelectedIndex;
                    //        activityItemId = gvNRBuffer.DataKeys[rowIndex].Values[0].ToString();
                    //        Session["activityItemId"] = activityItemId;
                    //        Session["rowIndex"] = rowIndex;
                    //        foreach (GridViewRow row in gvNRBuffer.Rows)
                    //        {
                    //            if (row.DataItemIndex.Equals(rowIndex))
                    //            {
                    //                //Label lblSubItemId = (Label)row.FindControl("lblSubItemId");
                    //                //Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                    //                DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                    //                if (ddlSubItem.SelectedIndex == 0)
                    //                {
                    //                    Session["subItemId"] = "0";
                    //                }
                    //                else
                    //                {
                    //                    Session["subItemId"] = ddlSubItem.SelectedValue;
                    //                }
                    //            }
                    //        }
                    //        GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);
                    //        Session["IsModelPopOpen"] = "True";
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                    //    Response.RedirectPermanent("~/ErrorPage.aspx", false);
                    //}
                }

            }
            catch (Exception)
            {

                throw;
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
                }
                dt = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#detailModal').modal('show')});</script>", false);
            }
            if (e.CommandName == "update-something")
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //if (rowIndex != 0 && rowIndex < 2)
                //    rowIndex = rowIndex + 1;
                //else
                //    rowIndex = rowIndex * 2;
                //gvRCore.SelectedIndex = rowIndex; //Convert.ToInt32(e.CommandArgument);
                //try
                //{
                //    lblMessage.Visible = false;
                //    if (!IsPageRefresh)
                //    {
                //        Session["activityItemId"] = null;
                //        Session["subItemId"] = null;
                //        Session["rowIndex"] = null;
                //        ImageButton ibtn = sender as ImageButton;
                //        //int rowIndex = gvRCore.SelectedIndex;
                //        activityItemId = gvRCore.DataKeys[rowIndex].Values[0].ToString();
                //        Session["activityItemId"] = activityItemId;
                //        Session["rowIndex"] = rowIndex;
                //        foreach (GridViewRow row in gvRCore.Rows)
                //        {
                //            if (row.DataItemIndex.Equals(rowIndex))
                //            {
                //                //Label lblSubItemId = (Label)row.FindControl("lblSubItemId");
                //                //Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                //                DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                //                if (ddlSubItem.SelectedIndex == 0)
                //                {
                //                    Session["subItemId"] = "0";
                //                }
                //                else
                //                {
                //                    Session["subItemId"] = ddlSubItem.SelectedValue;
                //                }
                //            }
                //        }
                //        GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);
                //        Session["IsModelPopOpen"] = "True";
                //    }

                //}
                //catch (Exception ex)
                //{
                //    LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                //    Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //}
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
            if (e.CommandName == "update-something")
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);
                //if (rowIndex != 0 && rowIndex < 2)
                //    rowIndex = rowIndex + 1;
                //else
                //    rowIndex = rowIndex * 2;
                //gvRBuffer.SelectedIndex = rowIndex;//Convert.ToInt32(e.CommandArgument);
                //try
                //{
                //    lblMessage.Visible = false;
                //    if (!IsPageRefresh)
                //    {
                //        Session["activityItemId"] = null;
                //        Session["subItemId"] = null;
                //        Session["rowIndex"] = null;
                //        ImageButton ibtn = sender as ImageButton;
                //        //int rowIndex = gvRBuffer.SelectedIndex;
                //        activityItemId = gvRBuffer.DataKeys[rowIndex].Values[0].ToString();
                //        Session["activityItemId"] = activityItemId;
                //        Session["rowIndex"] = rowIndex;
                //        foreach (GridViewRow row in gvRBuffer.Rows)
                //        {
                //            if (row.DataItemIndex.Equals(rowIndex))
                //            {
                //                //Label lblSubItemId = (Label)row.FindControl("lblSubItemId");
                //                //Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                //                DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                //                if (ddlSubItem.SelectedIndex == 0)
                //                {
                //                    Session["subItemId"] = "0";
                //                }
                //                else
                //                {
                //                    Session["subItemId"] = ddlSubItem.SelectedValue;
                //                }
                //            }
                //        }
                //        GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);
                //        Session["IsModelPopOpen"] = "True";
                //    }

                //}
                //catch (Exception ex)
                //{
                //    LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                //    Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //}
            }
            //if (e.CommandName.Equals("GetGPS"))
            //{
            //    //DataSet ds = ManageGuidelineBAL.Instance.GetGuidelineList();
            //    //dt = ds.Tables[0];
            //    //LinkButton lnkView = (LinkButton)e.CommandSource;
            //    //string cssPtParaNo = lnkView.Text;
            //    //string[] strArray = cssPtParaNo.Split('.');
            //    //string paraNumber = strArray[0] + "." + strArray[1];
            //    //if (dt.Rows.Count > 0)
            //    //{
            //    //    IEnumerable<DataRow> query = from i in dt.AsEnumerable()
            //    //                                 where i.Field<String>("GPS").Equals(paraNumber)
            //    //                                 select i;
            //    //    DataTable detailTable = query.CopyToDataTable<DataRow>();
            //    //    DetailsView1.DataSource = detailTable;
            //    //    DetailsView1.DataBind();
            //    //    APOBAL.Instance.GenerateGridFooter(sender, gvRBuffer);
            //    //}
            //    //dt = null;
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);

            //}
        }

        decimal totalNRCore = 0.0m;
        protected void gvNRCore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActivityId = (e.Row.Cells[0].FindControl("lblActivityId") as Label);
                Label lblActivityItemId = (e.Row.Cells[1].FindControl("lblActivityItemId") as Label);
                TextBox total = (e.Row.Cells[7].FindControl("txtTotal") as TextBox);
                if (!string.IsNullOrEmpty(total.Text))
                    totalNRCore += Convert.ToDecimal(total.Text);
                Label lblGPS = (e.Row.Cells[8].FindControl("lblGPS") as Label);
                if (Session["lblGPS"] != null)
                    lblGPS.Text += Session["lblGPS"].ToString() + "\n";

                #region for subitem

                //Label lblSubItemId = (e.Row.Cells[2].FindControl("lblSubItemId") as Label);
                //Label lblSubItem = (e.Row.Cells[2].FindControl("lblSubItem") as Label);
                DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
                //ddlSubItem.DataSource = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
                DataSet ds = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
                DataRow row = ds.Tables[0].NewRow();
                row[0] = "0";
                row[1] = "Please Select";
                ds.Tables[0].Rows.InsertAt(row, 0);
                ddlSubItem.DataSource = ds;
                ddlSubItem.DataTextField = "SubItem";
                ddlSubItem.DataValueField = "SubItemId";
                ddlSubItem.DataBind();

                int startIndex = 4;
                int count = e.Row.RowIndex;
                int initialCount = count + startIndex;
                {
                    GridView ParentGrid = (GridView)sender;
                    APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvNRCore, initialCount + count, oApo.TigerReserveId, 1, 1, Convert.ToInt32(lblActivityItemId.Text),"SubmittedAPO");
                }
                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox lblAmount = (TextBox)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalNRCore.ToString();
            }
        }

        decimal totalNRbuffer = 0.0m;
        protected void gvNRBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActivityId = (e.Row.Cells[0].FindControl("lblActivityId") as Label);
                Label lblActivityItemId = (e.Row.Cells[1].FindControl("lblActivityItemId") as Label);
                TextBox total = (e.Row.FindControl("txtTotal") as TextBox);
                if (!string.IsNullOrEmpty(total.Text))
                    totalNRbuffer += Convert.ToDecimal(total.Text);
                Label lblGPS = (e.Row.Cells[8].FindControl("lblGPS") as Label);
                if (Session["lblGPS"] != null)
                    lblGPS.Text += Session["lblGPS"].ToString() + "\n";

                #region for subitem

                //Label lblSubItemId = (e.Row.Cells[2].FindControl("lblSubItemId") as Label);
                //Label lblSubItem = (e.Row.Cells[2].FindControl("lblSubItem") as Label);
                DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
                //ddlSubItem.DataSource 
                DataSet ds = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
                DataRow row = ds.Tables[0].NewRow();
                row[0] = "0";
                row[1] = "Please Select";
                ds.Tables[0].Rows.InsertAt(row, 0);
                ddlSubItem.DataTextField = "SubItem";
                ddlSubItem.DataValueField = "SubItemId";
                ddlSubItem.DataSource = ds;
                ddlSubItem.DataBind();

                int startIndex = 4;
                int count = e.Row.RowIndex;
                int initialCount = count + startIndex;
                if (initialCount > 3)
                {
                    GridView ParentGrid = (GridView)sender;
                    APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvNRBuffer, initialCount + count, oApo.TigerReserveId, 1, 2, Convert.ToInt32(lblActivityItemId.Text), "SubmittedAPO");
                }
                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox lblAmount = (TextBox)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalNRbuffer.ToString();
            }
        }

        decimal totalRCore = 0.0m;
        protected void gvRCore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActivityId = (e.Row.Cells[0].FindControl("lblActivityId") as Label);
                Label lblActivityItemId = (e.Row.Cells[1].FindControl("lblActivityItemId") as Label);
                TextBox total = (e.Row.FindControl("txtTotal") as TextBox);
                if (!string.IsNullOrEmpty(total.Text))
                    totalRCore += Convert.ToDecimal(total.Text);
                Label lblGPS = (e.Row.Cells[8].FindControl("lblGPS") as Label);
                if (Session["lblGPS"] != null)
                    lblGPS.Text += Session["lblGPS"].ToString() + "\n";

                #region for subitem

                //Label lblSubItemId = (e.Row.Cells[2].FindControl("lblSubItemId") as Label);
                //Label lblSubItem = (e.Row.Cells[2].FindControl("lblSubItem") as Label);
                DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
                //ddlSubItem.DataSource 
                DataSet ds = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
                DataRow row = ds.Tables[0].NewRow();
                row[0] = "0";
                row[1] = "Please Select";
                ds.Tables[0].Rows.InsertAt(row, 0);
                ddlSubItem.DataTextField = "SubItem";
                ddlSubItem.DataValueField = "SubItemId";
                ddlSubItem.DataSource = ds;
                ddlSubItem.DataBind();

                int startIndex = 4;
                int count = e.Row.RowIndex;
                int initialCount = count + startIndex;
                if (initialCount > 3)
                {
                    GridView ParentGrid = (GridView)sender;
                    //APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvRCore, initialCount + count);
                    APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvRCore, initialCount + count, oApo.TigerReserveId, 2, 1, Convert.ToInt32(lblActivityItemId.Text), "SubmittedAPO");
                }
                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox lblAmount = (TextBox)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalRCore.ToString();
            }
        }

        Decimal totalRbuffer = 0.0m;
        protected void gvRBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActivityId = (e.Row.Cells[0].FindControl("lblActivityId") as Label);
                Label lblActivityItemId = (e.Row.Cells[1].FindControl("lblActivityItemId") as Label);
                TextBox total = (e.Row.FindControl("txtTotal") as TextBox);
                if (!string.IsNullOrEmpty(total.Text))
                    totalRbuffer += Convert.ToDecimal(total.Text);
                Label lblGPS = (e.Row.Cells[8].FindControl("lblGPS") as Label);
                if (Session["lblGPS"] != null)
                    lblGPS.Text += Session["lblGPS"].ToString() + "\n";

                #region for subitem

                //Label lblSubItemId = (e.Row.Cells[2].FindControl("lblSubItemId") as Label);
                //Label lblSubItem = (e.Row.Cells[2].FindControl("lblSubItem") as Label);
                DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
                //ddlSubItem.DataSource 
                DataSet ds = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
                DataRow row = ds.Tables[0].NewRow();
                row[0] = "0";
                row[1] = "Please Select";
                ds.Tables[0].Rows.InsertAt(row, 0);
                ddlSubItem.DataTextField = "SubItem";
                ddlSubItem.DataValueField = "SubItemId";
                ddlSubItem.DataSource = ds;
                ddlSubItem.DataBind();

                int startIndex = 4;
                int count = e.Row.RowIndex;
                int initialCount = count + startIndex;
                if (initialCount > 3)
                {
                    GridView ParentGrid = (GridView)sender;
                    //APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvRBuffer, initialCount + count);
                    APOBAL.Instance.GenerateBlankDataRow(ParentGrid, gvRBuffer, initialCount + count, oApo.TigerReserveId, 2, 2, Convert.ToInt32(lblActivityItemId.Text), "SubmittedAPO");
                }
                #endregion

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox lblAmount = (TextBox)e.Row.FindControl("txtSubTotal");
                lblAmount.Text = totalRbuffer.ToString();
            }
        }

        public void GetApoFormat()
        {
            DataSet dsApo = new DataSet();
            if (IsEdit == true)
                dsApo = APOBAL.Instance.GetApoForModification(Convert.ToInt32(oApo.TigerReserveId));
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
                btnNRCoreSave.Style.Add("display", "none");
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
                btnNRBufferSave.Style.Add("display", "none");
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
                btnRCoreSave.Style.Add("display", "none");
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
                btnRBufferSave.Style.Add("display", "none");
            }

        }

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
                                Label lblGpsNote = (Label)row.FindControl("lblGpsNote");
                                Label lblActivityId = (Label)row.FindControl("lblActivityId");
                                Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                                Label lblIsGpsRequired = (Label)row.FindControl("lblIsGpsRequired");
                                LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                                TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                                TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                                TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                                TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                //Label txtGPS = (Label)row.FindControl("lblGPS");
                                ImageButton addGps = (ImageButton)row.FindControl("updateSomething");
                                TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                                FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");
                                LinkButton lbDocumentName = (LinkButton)row.FindControl("clbDocumentFile");
                                //DetailsView detailsView = (DetailsView)row.FindControl("GpsDetailsView");
                                //lbDocumentName.Text = "Document";
                                GridView gpsDetails = (GridView)row.FindControl("gvGpsDetails");

                                if (lblActivityId.Text == dr["ActivityId"].ToString() && lblActivityItemId.Text == dr["ActivityItemId"].ToString())
                                {
                                    //if (lblIsGpsRequired.Text == "True" && !string.IsNullOrEmpty(lblIsGpsRequired.Text))
                                    //{
                                    //    lblGpsNote.Visible = true;
                                    //    addGps.Visible = true;
                                    //    gpsDetails.Visible = true;
                                    //}
                                    //else
                                    //{
                                    //    lblGpsNote.Visible = false;
                                    //    addGps.Visible = false;
                                    //    gpsDetails.Visible = false;
                                    //}
                                    if (IsEdit == true)
                                    {
                                        HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                                        hdnId.Value = dr["ID"].ToString();
                                        HiddenField hdnTcpParaNo = (HiddenField)row.FindControl("hdnTcpParaNo");
                                        hdnTcpParaNo.Value = dr["ParaNoTCP"].ToString();
                                        txtParaNo.Text = hdnTcpParaNo.Value;
                                    }
                                    else
                                    {
                                        txtParaNo.Text = string.Empty;// dr["ParaNoTCP"].ToString();
                                        txtNumberOfItem.Text = string.Empty;//dr["NumberOfItems"].ToString();
                                        txtSpcification.Text = string.Empty;//dr["Specification"].ToString();
                                        txtUnitPrice.Text = string.Empty;// dr["UnitPrice"].ToString();
                                        txtTotal.Text = string.Empty;//dr["Total"].ToString();

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
                                        txtJustification.Text = string.Empty;// dr["Justification"].ToString();
                                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Document"])))
                                            lbDocumentName.Text = string.Empty;//dr["Document"].ToString();
                                        else
                                            lbDocumentName.Text = string.Empty;
                                    }
                                }
                                DataSet dsGps = APOBAL.Instance.GetGPS(oApo.TigerReserveId, Convert.ToInt32(lblActivityItemId.Text), dr["SubItem"].ToString(),"");
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
                Label lblIsGpsRequired = (Label)row.FindControl("lblIsGpsRequired");
                TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                GridView gvGpsDetail = (GridView)row.FindControl("gvGpsDetails");
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
                    if (lblIsGpsRequired.Text == "True" && gvGpsDetail.Rows.Count < 1)
                        IsValid = false;
                    else
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
                int i = 0;
                foreach (GridViewRow row in gv.Rows)
                {
                    i = i + 1;
                    if (i % 2 != 0)
                    {
                        Label lblGpsNote = (Label)row.FindControl("lblGpsNote");
                        Label lblActivityId = (Label)row.FindControl("lblActivityId");
                        Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                        Label lblIsGpsRequired = (Label)row.FindControl("lblIsGpsRequired");
                        LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                        TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                        TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                        TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                        TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                        TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                        //if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
                        //{
                        //    double total = Convert.ToDouble(txtNumberOfItem.Text) * Convert.ToDouble(txtUnitPrice.Text);
                        //    txtTotal.Text = Convert.ToString(total);
                        //    txtTotal.Enabled = false;
                        //}

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
                            //oApo.Total = Convert.ToDouble(txtTotal.Text);
                            oApo.Total = oApo.NumberOfItems.Value * oApo.UnitPrice.Value;
                            oApo.IsFilled = true;
                            //if (!string.IsNullOrEmpty(txtSpcification.Text))
                            //{
                            //    string specificatin = txtSpcification.Text;
                            //    specificatin = specificatin.Replace("'", "''");
                            //    oApo.Specification = specificatin;
                            //}

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
                                Session["IsInsert"] = true;
                                //string strSuccess = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();//"Data has been saved successfully.";
                                //vmSuccess.Message = strSuccess;
                                //FlashMessage.InfoMessage(vmSuccess.Message);
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
                                Session["IsUpdate"] = true;
                                //string strSuccess = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString();//"Data has been updated successfully.";
                                //vmSuccess.Message = strSuccess;
                                //FlashMessage.InfoMessage(vmSuccess.Message);
                                //DisplayErrorMessage.Style.Add("display", "none");
                                //DisplaySuccessMessage.Style.Add("display", "block");


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
                            Session["IsUpdate"] = true;
                            //string strSuccess = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString();//"Data has been updated successfully.";
                            //vmSuccess.Message = strSuccess;
                            //FlashMessage.InfoMessage(vmSuccess.Message);
                            //DisplayErrorMessage.Style.Add("display", "none");
                            //DisplaySuccessMessage.Style.Add("display", "block");
                        }
                        else
                        {
                        }
                    }

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

        private bool CheckApoStatusBeforeAnyAction()
        {
            int apoFileId = 0;
            bool IsAuthorized = false;
            string LoggedInUser = AuthoProvider.User;
            string loggedInUserRole = string.Empty;
            int statusId = 0;
            DataSet dsUserRole = APOBAL.Instance.GetLoggedUserRole(LoggedInUser);
            if (dsUserRole != null)
            {
                DataRow dr = dsUserRole.Tables[0].Rows[0];
                loggedInUserRole = dr["RoleName"].ToString();
            }
            DataSet dsApoFileId = APOBAL.Instance.GetAPOFileId(oApo.TigerReserveId,"SubmitAPO");
            if (dsApoFileId != null && dsApoFileId.Tables[0].Rows.Count == 1)
            {
                DataRow dr = dsApoFileId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    apoFileId = Convert.ToInt32(dr[DataBaseFields.APOFileId]);
            }
            if (apoFileId > 0)
            {
                DataSet dsCurrentStatus = APOBAL.Instance.GetCurrentAPOStatus(apoFileId);
                if (dsCurrentStatus != null && dsCurrentStatus.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsCurrentStatus.Tables[0].Rows[0];
                    statusId = Convert.ToInt32(dr["StatusId"]);
                }
            }
            else
                statusId = 0;

            if (loggedInUserRole == "FIELDDIRECTOR" && statusId == 0 || loggedInUserRole == "FIELDDIRECTOR" && statusId == 1 || loggedInUserRole == "FIELDDIRECTOR" && statusId == 3)
            {
                IsAuthorized = true;
            }
            else if (loggedInUserRole == "CWLW" && statusId == 10)
            {
                IsAuthorized = true;
            }
            else if (loggedInUserRole == "REGIONALOFFICER" && (statusId == 13 || statusId == 16))
            {
                IsAuthorized = true;
            }
            else if (loggedInUserRole.Equals("NTCA"))
            {
                IsAuthorized = true;
            }
            else
            {
                Session["IsDraftMode"] = "Not";
                //string strError = ConfigurationManager.AppSettings["CheckApoStatus"];
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                IsAuthorized = false;
            }
            return IsAuthorized;
        }

        protected void btnNRCoreSave_Click(object sender, EventArgs e)
        {
            try
            {
                Session["IsModelPopOpen"] = "False";
                if (!CheckApoStatusBeforeAnyAction())
                    return;
                oApo.AreaId = 1;
                oApo.ActivityTypeId = 1;
                //InsertGridviewToDatabase(gvNRCore);
                //GetApoFormat();
                //LoadGridviewDraftData(gvNRCore);
                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
                string strSuccess = ConfigurationManager.AppSettings["DraftSaveSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
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
                Session["IsModelPopOpen"] = "False";
                if (!CheckApoStatusBeforeAnyAction())
                    return;
                oApo.AreaId = 2;
                oApo.ActivityTypeId = 1;
                //InsertGridviewToDatabase(gvNRBuffer);
                //GetApoFormat();
                //LoadGridviewDraftData(gvNRBuffer);
                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
                string strSuccess = ConfigurationManager.AppSettings["DraftSaveSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
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
                Session["IsModelPopOpen"] = "False";
                if (!CheckApoStatusBeforeAnyAction())
                    return;
                oApo.AreaId = 1;
                oApo.ActivityTypeId = 2;

                //InsertGridviewToDatabase(gvRCore);
                //GetApoFormat();
                //LoadGridviewDraftData(gvRCore);
                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
                string strSuccess = ConfigurationManager.AppSettings["DraftSaveSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
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
                Session["IsModelPopOpen"] = "False";
                if (!CheckApoStatusBeforeAnyAction())
                    return;
                oApo.AreaId = 2;
                oApo.ActivityTypeId = 2;

                //InsertGridviewToDatabase(gvRBuffer);
                //GetApoFormat();
                //LoadGridviewDraftData(gvRBuffer);
                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
                string strSuccess = ConfigurationManager.AppSettings["DraftSaveSuccessMes"].ToString();
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
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
            Response.Redirect("FieldDirectorHome.aspx", false);
        }

        protected void btnBufferCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("FieldDirectorHome.aspx", false);
        }

        protected void btnRCoreCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("FieldDirectorHome.aspx", false);
        }

        protected void btnRBufferCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("FieldDirectorHome.aspx", false);
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
                Session["IsModelPopOpen"] = "False";
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
                Session["IsModelPopOpen"] = "False";
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
                Session["IsModelPopOpen"] = "False";
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
                Session["IsModelPopOpen"] = "False";
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
                    Session["subItemId"] = null;
                    Session["rowIndex"] = null;
                    ImageButton ibtn = sender as ImageButton;
                    int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                    activityItemId = gvNRCore.DataKeys[rowIndex].Values[0].ToString();
                    Session["activityItemId"] = activityItemId;
                    Session["rowIndex"] = rowIndex;
                    foreach (GridViewRow row in gvNRCore.Rows)
                    {
                        if (row.DataItemIndex.Equals(rowIndex))
                        {
                            //Label lblSubItemId = (Label)row.FindControl("lblSubItemId");
                            //Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                            DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                            if (ddlSubItem.SelectedIndex == 0)
                            {
                                Session["subItemId"] = "0";
                            }
                            else
                            {
                                Session["subItemId"] = ddlSubItem.SelectedValue;
                            }
                        }
                    }
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
            try
            {
                int subItemId = 0;
                if (string.IsNullOrEmpty(txtGPS.Text))
                {
                    lblMessage.Text = "Please enter the Gps coordinate.";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    lblMessage.Text = "Please wait while GPS data is being Saved";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.BlueViolet;
                    int tigerReserveId = oApo.TigerReserveId;
                    //if (Session["activityItemId"] != null)
                    activityItemId = objAPO.ActivityItemId.ToString(); //Session["activityItemId"].ToString();
                    //if (Session["subItemId"] != null)
                    subItemId = Convert.ToInt32(objAPO.SubItemId);//Session["subItemId"].ToString());
                    APOBAL.Instance.AddGPSFD(tigerReserveId, Convert.ToInt32(activityItemId), subItemId, txtGPS.Text, objAPO.SubItem.ToString());
                    txtGPS.Text = string.Empty;
                    GetGPSDetails(tigerReserveId, Convert.ToInt32(activityItemId));
                    lblMessage.Text = "Gps coordinate added sucessfully, please click on add after closing this window.";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                lblMessage.Text = strError;
                return;
            }
        }

        private void GetGPSDetails(int tigerReserveId, int activityItemId)
        {
            DataSet dsGps;
            cgvGps.DataSource = null;
            cgvGps.DataBind();
            dsGps = APOBAL.Instance.GetGPS(tigerReserveId, activityItemId,objAPO.SubItem.ToString(),"AddGPS");
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

        #region To Add Multiple sub item against each item
        private bool ValidateMandatorySubItemField(GridView gv, int rowIndex)
        {
            bool IsValid = false;
            RegularExpressionValidator rev = new RegularExpressionValidator();
            RequiredFieldValidator rfv = new RequiredFieldValidator();
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.DataItemIndex.Equals(rowIndex))
                {
                    DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                    TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                    TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                    TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                    Label lblIsGpsRequired = (Label)row.FindControl("lblIsGpsRequired");
                    GridView gvGpsDetail = (GridView)row.FindControl("gvGpsDetails");
                    if (ddlSubItem.SelectedIndex != 0 || ddlSubItem.SelectedIndex != -1)
                    {
                        if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtUnitPrice.Text) && string.IsNullOrEmpty(txtJustification.Text))
                        {
                            string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            IsValid = false;
                            return IsValid;
                        }
                        else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text) && string.IsNullOrEmpty(txtJustification.Text))
                        {
                            string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            IsValid = false;
                            return IsValid;
                        }
                        else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtUnitPrice.Text) && !string.IsNullOrEmpty(txtJustification.Text))
                        {
                            string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            IsValid = false;
                            return IsValid;
                        }

                        else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text) && string.IsNullOrEmpty(txtJustification.Text))
                        {
                            string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            IsValid = false;
                            return IsValid;
                        }
                        else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text) && !string.IsNullOrEmpty(txtJustification.Text))
                        {
                            string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            IsValid = false;
                            return IsValid;
                        }
                        else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtUnitPrice.Text) && !string.IsNullOrEmpty(txtJustification.Text))
                        {
                            string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            IsValid = false;
                            return IsValid;
                        }
                        else if (string.IsNullOrEmpty(txtNumberOfItem.Text) && string.IsNullOrEmpty(txtUnitPrice.Text) && string.IsNullOrEmpty(txtJustification.Text))
                        {
                            string strError = ConfigurationManager.AppSettings["ApoRequiredField"];
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            IsValid = false;
                            IsValid = false;
                            return IsValid;
                        }
                        else if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text) && string.IsNullOrEmpty(txtJustification.Text))
                        {
                            if (lblIsGpsRequired.Text == "True" && gvGpsDetail.Rows.Count < 1)
                                IsValid = false;
                            else
                                IsValid = true;
                            return IsValid;
                        }
                    }
                    else
                    {
                        string strError = "Please Select a sub item";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        IsValid = false;
                        return IsValid;
                    }
                }
            }

            return IsValid;
        }
        [WebMethod]
        public static string test(string apo)
        {
            APO apoObj = new APO();
            apoObj = JsonConvert.DeserializeObject<APO>(apo);
            string str = "indu";
            return str;
        }
        #region Add Sub Items
        [WebMethod]
        public static bool InsertSubItem(string apo)
        {
            try
            {

                APO objApo = new APO();
                objApo = JsonConvert.DeserializeObject<APO>(apo);
                string LoggedInUser = AuthoProvider.User;
                DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
                if (dsTigerReserveId != null)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                        objApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    objApo.LoggedInUser = LoggedInUser;
                }
                objApo.IsFilled = true;
                APOBAL.Instance.AddSubItemInAPO(objApo);
                return true;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return false;
            }
        }
        private void AddSubItems(GridView gv, int rowIndex)
        {
            try
            {
                foreach (GridViewRow row in gv.Rows)
                {
                    if (row.DataItemIndex.Equals(rowIndex))
                    {
                        Label lblActivityId = (Label)row.FindControl("lblActivityId");
                        Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                        DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
                        LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                        TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                        TextBox txtSubItemName = (TextBox)row.FindControl("txtSubItemName");
                        TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                        TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                        TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                        TextBox txtTotal = (TextBox)row.FindControl("txtTotal");

                        TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                        FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");
                        LinkButton lbDocumentName = (LinkButton)row.FindControl("clbDocumentFile");
                        if (ddlSubItem.SelectedIndex != 0 && ddlSubItem.SelectedIndex != -1)// 
                        {
                            if (!string.IsNullOrEmpty(lblActivityId.Text) && !string.IsNullOrEmpty(lblActivityItemId.Text))
                            {

                                //if (!ValidateMandatorySubItemField(gv, rowIndex))
                                //    return;
                                oApo.ActivityId = Convert.ToInt32(lblActivityId.Text);
                                oApo.ActivityItemId = Convert.ToInt32(lblActivityItemId.Text);
                                oApo.SubItemId = Convert.ToInt32(ddlSubItem.SelectedValue);
                                //oApo.ParaNoCSSPTGuidelines = lbCssPT.Text;
                                oApo.ParaNoTCP = txtParaNo.Text;
                                oApo.SubItem = txtSubItemName.Text;
                                oApo.NumberOfItems = Convert.ToDouble(txtNumberOfItem.Text);
                                oApo.Specification = txtSpcification.Text;
                                oApo.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                                oApo.Total = oApo.NumberOfItems.Value * oApo.UnitPrice.Value;
                                oApo.IsFilled = true;
                                //oApo.Total = Convert.ToDouble(txtTotal.Text);
                                oApo.Justification = txtJustification.Text;
                                oApo.Document = lbDocumentName.Text;
                                APOBAL.Instance.AddSubItemInAPO(oApo);

                                string strSuccess = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                            }
                        }
                        else
                        {
                            string strError = "Please Select a sub item";
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            return;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                vmError.Message = strError;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
            }
        }
        protected void imgbtnAddSubItemNrCore_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                //DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");

                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvNRCore.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                oApo.AreaId = 1;
                oApo.ActivityTypeId = 1;
                if (rowIndex != 0 && rowIndex < 2)
                    rowIndex = rowIndex + 1;
                else
                    rowIndex = rowIndex * 2;
                AddSubItems(gvNRCore, rowIndex);
                GetApoFormat();
                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void imgbtnAddSubItemNrBuffer_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvNRBuffer.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                oApo.AreaId = 2;
                oApo.ActivityTypeId = 1;
                if (rowIndex != 0 && rowIndex < 2)
                    rowIndex = rowIndex + 1;
                else
                    rowIndex = rowIndex * 2;
                AddSubItems(gvNRBuffer, rowIndex);
                GetApoFormat();
                //string strSuccess = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();
                //vmSuccess.Message = strSuccess;
                //FlashMessage.InfoMessage(vmSuccess.Message);

                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void imgbtnAddSubItemRCore_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvRCore.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                oApo.AreaId = 1;
                oApo.ActivityTypeId = 2;
                if (rowIndex != 0 && rowIndex < 2)
                    rowIndex = rowIndex + 1;
                else
                    rowIndex = rowIndex * 2;
                AddSubItems(gvRCore, rowIndex);
                GetApoFormat();

                //string strSuccess = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();
                //vmSuccess.Message = strSuccess;
                //FlashMessage.InfoMessage(vmSuccess.Message);

                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void imgbtnAddSubItemRbuffer_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
                string activityItemId = gvRBuffer.DataKeys[rowIndex].Values[0].ToString();
                int _activityItemId = Convert.ToInt32(activityItemId);
                oApo.AreaId = 2;
                oApo.ActivityTypeId = 2;
                if (rowIndex != 0 && rowIndex < 2)
                    rowIndex = rowIndex + 1;
                else
                    rowIndex = rowIndex * 2;
                AddSubItems(gvRBuffer, rowIndex);
                GetApoFormat();

                //string strSuccess = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();
                //vmSuccess.Message = strSuccess;
                //FlashMessage.InfoMessage(vmSuccess.Message);

                //string rawUrl = Request.RawUrl.ToString();
                //Response.Redirect(rawUrl, false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        #endregion
        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(txtGPS.Text))
        //        {
        //            lblMessage.Text = "Please enter the Gps coordinate.";
        //            lblMessage.Visible = true;
        //            lblMessage.ForeColor = System.Drawing.Color.Red;
        //            return;
        //        }
        //        else
        //            lblMessage.Visible = false;
        //        int tigerReserveId = oApo.TigerReserveId;
        //        if (Session["activityItemId"] != null)
        //            activityItemId = Session["activityItemId"].ToString();
        //        APOBAL.Instance.AddGPS(tigerReserveId, Convert.ToInt32(activityItemId), txtGPS.Text);
        //        txtGPS.Text = string.Empty;
        //        GetGPSDetails(tigerReserveId, Convert.ToInt32(activityItemId));
        //        lblMessage.Text = "Gps coordinate added sucessfully, please click on save after closing this window.";
        //        lblMessage.Visible = true;
        //        lblMessage.ForeColor = System.Drawing.Color.Green;
        //    }
        //    catch (Exception ex)
        //    {
        //        string strError = ex.Message;
        //        lblMessage.Text = strError;
        //        return;
        //    }
        //}
        private void GetSubItemsDetailActivityItemWise(int tigerReserveId, int activityTypeId, int areaId, int activityItemId)
        {
            DataSet dsSubItem;
            cgvGps.DataSource = null;
            cgvGps.DataBind();
            dsSubItem = APOBAL.Instance.GetSubItemsDetailActivityItemWise(tigerReserveId, activityTypeId, areaId, activityItemId);
            if (dsSubItem.Tables[0].Rows.Count > 0)
            {
                cgvGps.DataSource = dsSubItem;
                cgvGps.DataBind();
            }
        }

        //protected void cgvGps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    cgvGps.PageIndex = e.NewPageIndex;
        //    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        //}
        //protected void cgvGps_PageIndexChanged(object sender, EventArgs e)
        //{
        //    cgvGps.EditIndex = -1;
        //    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        //}
        //protected void cgvGps_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    cgvGps.EditIndex = e.NewEditIndex;
        //    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        //}

        //protected void cgvGps_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    cgvGps.EditIndex = -1;
        //    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        //}

        //protected void cgvGps_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        Label gpsId = cgvGps.Rows[e.RowIndex].FindControl("lblGPSId") as Label;
        //        int _gpsId = Convert.ToInt32(gpsId.Text);
        //        Label activityItemid = cgvGps.Rows[e.RowIndex].FindControl("lblActivityItemId") as Label;
        //        int activityItemId = Convert.ToInt32(activityItemid.Text);
        //        TextBox gps = cgvGps.Rows[e.RowIndex].FindControl("txtGPS") as TextBox;
        //        string strgps = gps.Text;
        //        APOBAL.Instance.UpdateGPS(_gpsId, oApo.TigerReserveId, activityItemId, strgps);
        //        cgvGps.EditIndex = -1;
        //        string strSuccess = "GPS has been updated successfully.";
        //        GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
        //        Response.RedirectPermanent("~/ErrorPage.aspx", false);
        //        //string strError = ex.Message;
        //        //vmError.Message = strError;
        //        //FlashMessage.ErrorMessage(vmError.Message);
        //        //return;
        //    }
        //}
        //protected void cgvGps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        Label gpsId = cgvGps.Rows[e.RowIndex].FindControl("lblGPSId") as Label;
        //        int _gpsId = Convert.ToInt32(gpsId.Text);
        //        APOBAL.Instance.DeleteGPS(_gpsId);
        //        GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]));
        //        string strSuccess = "GPS Coordinate deleted successfully.";
        //        vmSuccess.Message = strSuccess;
        //        FlashMessage.ErrorMessage(vmSuccess.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
        //        Response.RedirectPermanent("~/ErrorPage.aspx", false);
        //        //string strError = ex.Message;
        //        //vmError.Message = strError;
        //        //FlashMessage.ErrorMessage(vmError.Message);
        //        //return;
        //    }
        //}

        protected void gvNRCore_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNRCore.PageIndex = e.NewPageIndex;
            GetApoFormat();
        }
        protected void gvNRCore_PageIndexChanged(object sender, EventArgs e)
        {
            gvNRCore.EditIndex = -1;
            GetApoFormat();
        }
        #endregion

        protected void clbDocumentFile_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Session["Document"].ToString()))
            {
                string filePath = "~/Upload/ApoDocuments/" + Session["Document"].ToString();
                Response.ContentType = "image/png";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Session["Document"].ToString() + "\"");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    Response.TransmitFile(Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                Response.End();
                Session["Document"] = null;
            }
        }
    }

}