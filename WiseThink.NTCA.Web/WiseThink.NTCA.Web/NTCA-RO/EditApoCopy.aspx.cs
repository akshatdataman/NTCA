using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.App_Code;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class EditApoCopy : BasePage
    {
        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;
        int gvEditIndex = -1;
        Boolean IsPageRefresh;
        DataTable dt;
        DataSet dsApo = new DataSet();
        public bool IsEdit = false;
        string activityItemId = string.Empty;
        APO oApo = new APO();
        int rowIndexTemp;
        public static APO objAPO;
        public static int TigerReserveID;
        decimal NRCoreTotal = 0;
        decimal NRBufferTotal = 0;
        decimal RCoreTotal = 0;
        decimal RBufferTotal = 0;
        Decimal totalNRbuffer = 0.0m;
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
        public static void DowloadApo(string fileName)
        {
            Page objp = new Page();
            string filePath = "../Upload/ApoDocuments/" + fileName;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
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
            EditApoCopy objp = new EditApoCopy();
            try
            {
                objp.Session["IsDraftMode"] = "True";
                //if (!objp.CheckApoStatusBeforeAnyAction())
                //    return false;
                return true;
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, objp.GetType());
                //objp.//Response.RedirectPermanent("~/ErrorPage.aspx", false);
                return false;
            }
        }

        [WebMethod]
        public static bool InsertSubItem(string apo)
        {
            try
            {
                APO objApo = new APO();
                objApo = JsonConvert.DeserializeObject<APO>(apo);
                string LoggedInUser = AuthoProvider.User;
                objApo.TigerReserveId = TigerReserveID;
                objApo.LoggedInUser = LoggedInUser;
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


        public bool CheckApoStatusBeforeAnyAction()
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
            DataSet dsApoFileId = APOBAL.Instance.GetAPOFileId(oApo.TigerReserveId, "SubmitAPO");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            gvNRBuffer.Visible = true;
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
                    TigerReserveID = oApo.TigerReserveId;
                    Session["TigerReserveId"] = oApo.TigerReserveId;
                    oApo.LoggedInUser = LoggedInUser;
                    //Session["APOFileId"] = null;
                }
            }
            if (!IsPostBack)
            {
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
                if (Session["IsEdit"] != null)
                {
                    IsEdit = (bool)Session["IsEdit"];
                    //btnNRCoreSave.Text = "Update";
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

        private void GetApoFormat()
        {

            if (IsEdit == true)
            {
                dsApo = APOBAL.Instance.GetApoForModification(Convert.ToInt32(oApo.TigerReserveId));
            }
            else
            {
                dsApo = APOBAL.Instance.GetApoFormat();
            }

            if (dsApo.Tables[4].Rows.Count > 0)
            {
                // gvNRCore.FooterRow.FindControl("txtNRCoreTotal").v
                if (dsApo.Tables[4].Rows[0][0] != DBNull.Value)
                    NRCoreTotal = Convert.ToDecimal(dsApo.Tables[4].Rows[0][0]);
                if (dsApo.Tables[4].Rows[0][1] != DBNull.Value)
                    NRBufferTotal = Convert.ToDecimal(dsApo.Tables[4].Rows[0][1]);
                if (dsApo.Tables[4].Rows[0][2] != DBNull.Value)
                    RCoreTotal = Convert.ToDecimal(dsApo.Tables[4].Rows[0][2]);
                if (dsApo.Tables[4].Rows[0][3] != DBNull.Value)
                    RBufferTotal = Convert.ToDecimal(dsApo.Tables[4].Rows[0][3]);
            }

            if (dsApo.Tables[0].Rows.Count > 0)
            {
                gvNRCore.DataSource = dsApo.Tables[0];
                gvNRCore.DataBind();
              //  LoadGridviewDraftData(gvNRCore);
            }
            else
            {
                gvNRCore.DataSource = dsApo.Tables[0];
                gvNRCore.DataBind();
               // LoadGridviewDraftData(gvNRCore);
                //btnNRCoreSave.Style.Add("display", "none");
            }
            if (dsApo.Tables[1].Rows.Count > 0)
            {
                gvNRBuffer.DataSource = dsApo.Tables[1];
                gvNRBuffer.DataBind();
               // LoadGridviewDraftData(gvNRBuffer);
            }
            else
            {
                gvNRBuffer.DataSource = dsApo.Tables[1];
                gvNRBuffer.DataBind();
                //LoadGridviewDraftData(gvNRBuffer);
                //btnNRBufferSave.Style.Add("display", "none");
            }
            if (dsApo.Tables[2].Rows.Count > 0)
            {
                gvRCore.DataSource = dsApo.Tables[2];
                gvRCore.DataBind();
                //LoadGridviewDraftData(gvRCore);
            }
            else
            {
                gvRCore.DataSource = dsApo.Tables[2];
                gvRCore.DataBind();
                //LoadGridviewDraftData(gvRCore);
                //btnRCoreSave.Style.Add("display", "none");
            }
            if (dsApo.Tables[3].Rows.Count > 0)
            {
                gvRBuffer.DataSource = dsApo.Tables[3];
                gvRBuffer.DataBind();
               // LoadGridviewDraftData(gvRBuffer);
            }
            else
            {
                gvRBuffer.DataSource = dsApo.Tables[3];
                gvRBuffer.DataBind();
               // LoadGridviewDraftData(gvRBuffer);
                //btnRBufferSave.Style.Add("display", "none");
            }
            try
            {
                TextBox NRCoreTotalText = (gvNRCore.FooterRow.FindControl("txtNRCoreTotal") as TextBox);
                NRCoreTotalText.Text = NRCoreTotal.ToString();
            }
            catch (Exception ex) { }
            try
            {
                TextBox NRBufferTotalText = (gvNRBuffer.FooterRow.FindControl("txtNRBufferTotal") as TextBox);
                NRBufferTotalText.Text = NRBufferTotal.ToString();
            }
            catch (Exception ex) { }
            try
            {
                TextBox RCoreTotalText = (gvRCore.FooterRow.FindControl("txtRCoreTotal") as TextBox);
                RCoreTotalText.Text = RCoreTotal.ToString();
            }
            catch (Exception ex) { }
            try
            {
                TextBox RBufferTotalText = (gvRBuffer.FooterRow.FindControl("txtRBufferTotal") as TextBox);
                RBufferTotalText.Text = RBufferTotal.ToString();
            }
            catch (Exception ex) { }


            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "SetTotalValues('" + NRCoreTotal + "','" + NRBufferTotal + "','" + RCoreTotal + "','" + RBufferTotal + "');", true);

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
                    FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");

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
                return;
                //lblMessage.Text = "error" + ex.ToString();
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

                                        txtJustification.Text = string.Empty;// dr["Justification"].ToString();
                                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Document"])))
                                            lbDocumentName.Text = string.Empty;//dr["Document"].ToString();
                                        else
                                            lbDocumentName.Text = string.Empty;
                                    }
                                }
                                string subItems = string.Empty;

                                if (oApo.SubItem !=null)
                                {
                                    subItems = oApo.SubItem.ToString();
                                }

                                DataSet dsGps = APOBAL.Instance.GetGPS(oApo.TigerReserveId, Convert.ToInt32(lblActivityItemId.Text),subItems , "");
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
                ////Response.Redirect(rawUrl, false);
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
                ////Response.Redirect(rawUrl, false);
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
                ////Response.Redirect(rawUrl, false);
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
        #region ParentRecord

        protected void gvNRCore_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateGridHeader(ProductGrid, gvNRCore);
            }
        }

        protected void gvNRCore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            if (row.DataItem == null)
                return;

            DataSet ds = new DataSet();
            GridView gv = new GridView();
            Label lb = new Label();
            lb = (Label)row.FindControl("lblActivityItemId");
            DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
            //ddlSubItem.DataSource = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
            DataSet dsgps = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lb.Text));
            DataRow rowgps = dsgps.Tables[0].NewRow();
            rowgps[0] = "0";
            rowgps[1] = "Please Select";
            dsgps.Tables[0].Rows.InsertAt(rowgps, 0);
            ddlSubItem.DataSource = dsgps;
            ddlSubItem.DataTextField = "SubItem";
            ddlSubItem.DataValueField = "SubItemId";
            ddlSubItem.DataBind();


            gv = (GridView)row.FindControl("ChildRecordsRecurring");
            string ActiviItemID = lb.Text.ToString();
            ds = GetChildGridRecords(((DataRowView)e.Row.DataItem)["ActivityItemId"].ToString());
            gv.DataSource = ds;
            gv.DataBind();

            //TextBox txtTotal = ((TextBox)gvNRCore.FooterRow.FindControl("txtNRCoreTotal"));
            //txtTotal.Text = NRCoreTotal.ToString();

        }

        protected DataSet GetChildGridRecords(string ActivityItemID)
        {
            DataSet tempDS = new DataSet();

            tempDS = APOBAL.Instance.GetChildRecordDetails(Convert.ToInt32(ActivityItemID), Convert.ToInt32(oApo.TigerReserveId));
            return tempDS;
        }

        protected void gvNRCore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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

            if (e.CommandName == "Edit")
            {

            }

            if (e.CommandName == "Update")
            {

            }

            if (e.CommandName == "Delete") { }
        }

        #endregion

        #region ChildRecord

        protected void ChildRecordsRecurring_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            //Get the values stored in the text boxes
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string strFreight = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtParaNoTCPChild")).Text;
            string strShipperName = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtSubItemChild")).Text;
            string strShipAdress = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtNumberOfItemsChild")).Text;
            string strShipAdress1 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtUnitPriceChild")).Text;
            string strShipAdress2 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtJustificationChild")).Text;
            string strShipAddress3 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("lblTotalChild")).Text;
            UpdateChildRecordInDb(Id, strFreight, strShipperName, strShipAdress, strShipAdress1, strShipAdress2, strShipAddress3);
            gvTemp.EditIndex = -1;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();

            UpdateFooterTotal();
        }

        private void UpdateFooterTotal()
        {
            DataSet ds1 = new DataSet();
            ds1 = APOBAL.Instance.GetApoTotal(Convert.ToInt32(oApo.TigerReserveId));

            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0][0] != DBNull.Value)
                    NRCoreTotal = Convert.ToDecimal(ds1.Tables[0].Rows[0][0]);
                if (ds1.Tables[0].Rows[0][1] != DBNull.Value)
                    NRBufferTotal = Convert.ToDecimal(ds1.Tables[0].Rows[0][1]);
                if (ds1.Tables[0].Rows[0][2] != DBNull.Value)
                    RCoreTotal = Convert.ToDecimal(ds1.Tables[0].Rows[0][2]);
                if (ds1.Tables[0].Rows[0][3] != DBNull.Value)
                    RBufferTotal = Convert.ToDecimal(ds1.Tables[0].Rows[0][3]);

                try
                {
                    TextBox NRCoreTotalText = (gvNRCore.FooterRow.FindControl("txtNRCoreTotal") as TextBox);
                    NRCoreTotalText.Text = NRCoreTotal.ToString();
                }
                catch (Exception ex)
                { }
                try
                {
                    TextBox NRBufferTotalText = (gvNRBuffer.FooterRow.FindControl("txtNRBufferTotal") as TextBox);
                    NRBufferTotalText.Text = NRBufferTotal.ToString();
                }
                catch (Exception ex)
                { }

                try
                {
                    TextBox RCoreTotalText = (gvRCore.FooterRow.FindControl("txtRCoreTotal") as TextBox);
                    RCoreTotalText.Text = RCoreTotal.ToString();
                }
                catch (Exception ex) { }
                try
                {
                    TextBox RBufferTotalText = (gvRBuffer.FooterRow.FindControl("txtRBufferTotal") as TextBox);
                    RBufferTotalText.Text = RBufferTotal.ToString();
                }
                catch (Exception ex) { }
            }
        }

        private void UpdateChildRecordInDb(string id, string strFreight, string strShipperName, string strShipAdress, string strShipAdress1, string strShipAdress2, string strShipAddress3)
        {
            APOBAL.Instance.UpdateChildRecordInDb(id, strFreight, strShipperName, strShipAdress, strShipAdress1, strShipAdress2, strShipAddress3);
        }

        protected void ChildRecordsRecurring_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ChildRecordsRecurring_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView gvTemp = (GridView)sender;
            rowIndexTemp = e.NewEditIndex;
            string strOrderID = ((Label)gvTemp.Rows[e.NewEditIndex].FindControl("lblActivityItemId")).Text;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            gvTemp.EditIndex = gvEditIndex;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();

        }
        protected void ChildRecordsNonRecurringBuffer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            //Get the values stored in the text boxes
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string strFreight = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtParaNoTCPChild")).Text;
            string strShipperName = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtSubItemChild")).Text;
            string strShipAdress = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtNumberOfItemsChildNRBuffer")).Text;
            string strShipAdress1 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtUnitPriceChildNRBuffer")).Text;
            string strShipAdress2 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtJustificationChild")).Text;
            string strShipAdderss3 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("lblTotalChild")).Text;
            UpdateChildRecordInDb(Id, strFreight, strShipperName, strShipAdress, strShipAdress1, strShipAdress2, strShipAdderss3);
            gvTemp.EditIndex = -1;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();
            UpdateFooterTotal();
        }
        protected void ChildRecordsNonRecurringBuffer_RowCommand(object sender, GridViewCommandEventArgs e) { }
        protected void ChildRecordsNonRecurringBuffer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string strOrderID = ((Label)gvTemp.Rows[e.NewEditIndex].FindControl("lblActivityItemId")).Text;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            gvTemp.EditIndex = gvEditIndex;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();
        }
        protected void ChildRecordsRecurring_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }
        protected void ChildRecordsNonRecurringBuffer_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }
        protected void ChildRecordsNonRecurringBuffer_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IsEdit = true;
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            GetApoFormat();
        }
        protected void ChildRecordsNonRecurringBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ChildRecordsNonRecurringBuffer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            DeleteChildRecordFromDb(Id);
            gvTemp.DataSource = GetChildGridRecords(strOrderID);
            gvTemp.DataBind();
            UpdateFooterTotal();
        }
        protected void ChildRecordsNonRecurringBuffer_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }
        protected void ChildRecordsRecurringNormal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            //Get the values stored in the text boxes
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string strFreight = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtParaNoTCPChild")).Text;
            string strShipperName = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtSubItemChild")).Text;
            string strShipAdress = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtNumberOfItemsChildRCore")).Text;
            string strShipAdress1 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtUnitPriceChildRCore")).Text;
            string strShipAdress2 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtJustificationChild")).Text;
            string strShipAdderss3 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("lblTotalChild")).Text;
            UpdateChildRecordInDb(Id, strFreight, strShipperName, strShipAdress, strShipAdress1, strShipAdress2, strShipAdderss3);
            gvTemp.EditIndex = -1;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();
            UpdateFooterTotal();
        }
        protected void ChildRecordsRecurringNormal_RowCommand(object sender, GridViewCommandEventArgs e) { }
        protected void ChildRecordsRecurringNormal_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string strOrderID = ((Label)gvTemp.Rows[e.NewEditIndex].FindControl("lblActivityItemId")).Text;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            gvTemp.EditIndex = gvEditIndex;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();
        }
        protected void ChildRecordsRecurringNormal_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }
        protected void ChildRecordsRecurringNormal_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IsEdit = true;
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            GetApoFormat();
        }
        protected void ChildRecordsRecurringNormal_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ChildRecordsRecurringNormal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            DeleteChildRecordFromDb(Id);
            gvTemp.DataSource = GetChildGridRecords(strOrderID);
            gvTemp.DataBind();
            UpdateFooterTotal();
        }
        protected void ChildRecordsRecurringNormal_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }
        protected void ChildRecordsRecurringBuffer_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }
        protected void ChildRecordsRecurringBuffer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            DeleteChildRecordFromDb(Id);
            gvTemp.DataSource = GetChildGridRecords(strOrderID);
            gvTemp.DataBind();
            UpdateFooterTotal();
        }
        protected void ChildRecordsRecurringBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ChildRecordsRecurringBuffer_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IsEdit = true;
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            GetApoFormat();
        }
        protected void ChildRecordsRecurringBuffer_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }
        protected void ChildRecordsRecurringBuffer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string strOrderID = ((Label)gvTemp.Rows[e.NewEditIndex].FindControl("lblActivityItemId")).Text;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            gvTemp.EditIndex = gvEditIndex;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();
        }
        protected void ChildRecordsRecurringBuffer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            //Get the values stored in the text boxes
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string strFreight = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtParaNoTCPChild")).Text;
            string strShipperName = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtSubItemChild")).Text;
            string strShipAdress = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtNumberOfItemsChildRBuffer")).Text;
            string strShipAdress1 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtUnitPriceChildRBuffer")).Text;
            string strShipAdress2 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtJustificationChild")).Text;
            string strShipAdderss3 = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("lblTotalChild")).Text;
            UpdateChildRecordInDb(Id, strFreight, strShipperName, strShipAdress, strShipAdress1, strShipAdress2, strShipAdderss3);
            gvTemp.EditIndex = -1;
            DataSet ds = GetChildGridRecords(strOrderID);
            gvTemp.DataSource = ds;
            gvTemp.DataBind();
            UpdateFooterTotal();
        }
        protected void ChildRecordsRecurringBuffer_RowCommand(object sender, GridViewCommandEventArgs e) { }
        protected void ChildRecordsRecurring_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IsEdit = true;
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            GetApoFormat();
        }

        protected void ChildRecordsRecurring_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ChildRecordsRecurring_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblActivityItemId")).Text;
            string Id = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblId")).Text;
            DeleteChildRecordFromDb(Id);
            gvTemp.DataSource = GetChildGridRecords(strOrderID);
            gvTemp.DataBind();
            UpdateFooterTotal();

        }

        private void DeleteChildRecordFromDb(string id)
        {
            APOBAL.Instance.DeleteChildRecord(id);
        }

        protected void ChildRecordsRecurring_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        #endregion

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
                    Session["activityItemId"] = activityItemId;
                    //if (Session["subItemId"] != null)
                    subItemId = Convert.ToInt32(objAPO.SubItemId);//Session["subItemId"].ToString());
                    APOBAL.Instance.AddGPSFD(tigerReserveId, Convert.ToInt32(activityItemId), subItemId, txtGPS.Text, objAPO.SubItem.ToString());
                    txtGPS.Text = string.Empty;
                    GetGPSDetails(tigerReserveId, Convert.ToInt32(activityItemId), objAPO.SubItem.ToString());
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

        private void GetGPSDetails(int tigerReserveId, int activityItemId, string SubitemName)
        {
            DataSet dsGps;
            cgvGps.DataSource = null;
            cgvGps.DataBind();
            dsGps = APOBAL.Instance.GetGPS(tigerReserveId, activityItemId, SubitemName, "AddGPS");
            if (dsGps.Tables[0].Rows.Count > 0)
            {
                cgvGps.DataSource = dsGps;
                cgvGps.DataBind();
            }
        }

        #region GPS GRID

        protected void cgvGps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvGps.PageIndex = e.NewPageIndex;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]), oApo.SubItem.ToString());
        }

        protected void cgvGps_PageIndexChanged(object sender, EventArgs e)
        {
            cgvGps.EditIndex = -1;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]), oApo.SubItem.ToString());
        }

        protected void cgvGps_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvGps.EditIndex = e.NewEditIndex;
            Label SubItemName = cgvGps.Rows[e.NewEditIndex].FindControl("lblSubItemName") as Label;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(Session["activityItemId"]), SubItemName.Text.ToString().Trim());
        }

        protected void cgvGps_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Label activityItemId = cgvGps.Rows[e.RowIndex].FindControl("lblActivityItemId") as Label;
            Label SubItemName = cgvGps.Rows[e.RowIndex].FindControl("lblSubItemName") as Label;
            cgvGps.EditIndex = -1;
            GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId.Text.Trim().ToString()), SubItemName.Text.Trim().ToString());
        }

        protected void cgvGps_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label gpsId = cgvGps.Rows[e.RowIndex].FindControl("lblGPSId") as Label;
                int _gpsId = Convert.ToInt32(gpsId.Text);
                Label activityItemid = cgvGps.Rows[e.RowIndex].FindControl("lblActivityItemId") as Label;
                Label SubItemName = cgvGps.Rows[e.RowIndex].FindControl("lblSubItemName") as Label;
                int activityItemId = Convert.ToInt32(activityItemid.Text);
                TextBox gps = cgvGps.Rows[e.RowIndex].FindControl("txtGPS") as TextBox;
                string strgps = gps.Text;
                APOBAL.Instance.UpdateGPS(_gpsId, oApo.TigerReserveId, activityItemId, strgps);
                cgvGps.EditIndex = -1;
                string strSuccess = "GPS has been updated successfully.";
                GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId), SubItemName.Text.Trim().ToString());
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
                Label activityItemId = cgvGps.Rows[e.RowIndex].FindControl("lblActivityItemId") as Label;
                Label SubItemName = cgvGps.Rows[e.RowIndex].FindControl("lblSubItemName") as Label;
                int _gpsId = Convert.ToInt32(gpsId.Text);
                APOBAL.Instance.DeleteGPS(_gpsId);
                GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId.Text.Trim()), SubItemName.Text.ToString());
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

        #endregion

        protected void btnCoreCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("Home.aspx", false);
        }
        protected void downloadBtn_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string fileName = lb.Text;
            string file = Server.MapPath("../Upload/ApoDocuments/" + fileName);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
            try
            {
                if (fileInfo.Exists)
                {
                    Response.Clear();
                    string value = "attachment;filename=" + fileInfo.Name;
                    Response.AppendHeader("Content-Disposition", value);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.TransmitFile(fileInfo.FullName);
                    Response.End();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "blah", "alert('file download successfully');", true);
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(GetType(), "CallMyFunction", "alert('file not found on server');", true);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //  context.Response.End();
            }

        }
        protected void downloadBtn2_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string fileName = lb.Text;
            string file = Server.MapPath("../Upload/ApoDocuments/" + fileName);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
            try
            {
                if (fileInfo.Exists)
                {
                    Response.Clear();
                    string value = "attachment;filename=" + fileInfo.Name;
                    Response.AppendHeader("Content-Disposition", value);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.TransmitFile(fileInfo.FullName);
                    Response.End();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "blah", "alert('file download successfully');", true);
                }
                else
                {
                    // Page.ClientScript.RegisterStartupScript(GetType(), "CallMyFunction", "alert('file not found on server');", true);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //  context.Response.End();
            }

        }
        protected void downloadBtn3_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string fileName = lb.Text;
            string file = Server.MapPath("../Upload/ApoDocuments/" + fileName);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
            try
            {
                if (fileInfo.Exists)
                {
                    Response.Clear();
                    string value = "attachment;filename=" + fileInfo.Name;
                    Response.AppendHeader("Content-Disposition", value);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.TransmitFile(fileInfo.FullName);
                    Response.End();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "blah", "alert('file download successfully');", true);
                }
                else
                {
                    // Page.ClientScript.RegisterStartupScript(GetType(), "CallMyFunction", "alert('file not found on server');", true);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //  context.Response.End();
            }

        }
        protected void downloadBtn4_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string fileName = lb.Text;
            string file = Server.MapPath("../Upload/ApoDocuments/" + fileName);
            FileInfo fileInfo = new FileInfo(file);
            try
            {
                if (fileInfo.Exists)
                {
                    Response.Clear();
                    string value = "attachment;filename=" + fileInfo.Name;
                    Response.AppendHeader("Content-Disposition", value);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.TransmitFile(fileInfo.FullName);
                    Response.End();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "blah", "alert('file download successfully');", true);
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(GetType(), "CallMyFunction", "alert('file not found on server');", true);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //  context.Response.End();
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
        protected void gvNRBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            if (row.DataItem == null)
                return;

            DataSet ds = new DataSet();
            GridView gv = new GridView();
            Label lb = new Label();
            lb = (Label)row.FindControl("lblActivityItemId");
            DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
            //ddlSubItem.DataSource = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
            DataSet dsgps = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lb.Text));
            DataRow rowgps = dsgps.Tables[0].NewRow();
            rowgps[0] = "0";
            rowgps[1] = "Please Select";
            dsgps.Tables[0].Rows.InsertAt(rowgps, 0);
            ddlSubItem.DataSource = dsgps;
            ddlSubItem.DataTextField = "SubItem";
            ddlSubItem.DataValueField = "SubItemId";
            ddlSubItem.DataBind();


            gv = (GridView)row.FindControl("ChildRecordsNonRecurringBuffer");
            string ActiviItemID = lb.Text.ToString();
            ds = GetChildGridRecords(((DataRowView)e.Row.DataItem)["ActivityItemId"].ToString());
            gv.DataSource = ds;
            gv.DataBind();

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtTotal = (TextBox)e.Row.FindControl("txtNRBufferTotal");
                txtTotal.Text = NRBufferTotal.ToString(); //total.ToString();
            }
        }
        protected void btnBufferCancel_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = null;
            Response.Redirect("Home.aspx", false);
        }
        Decimal totalRCore = 0.0m;
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
        protected void gvRCore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            if (row.DataItem == null)
                return;

            DataSet ds = new DataSet();
            GridView gv = new GridView();
            Label lb = new Label();
            lb = (Label)row.FindControl("lblActivityItemId");
            DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
            //ddlSubItem.DataSource = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
            DataSet dsgps = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lb.Text));
            DataRow rowgps = dsgps.Tables[0].NewRow();
            rowgps[0] = "0";
            rowgps[1] = "Please Select";
            dsgps.Tables[0].Rows.InsertAt(rowgps, 0);
            ddlSubItem.DataSource = dsgps;
            ddlSubItem.DataTextField = "SubItem";
            ddlSubItem.DataValueField = "SubItemId";
            ddlSubItem.DataBind();


            gv = (GridView)row.FindControl("ChildRecordsRecurringNormal");
            string ActiviItemID = lb.Text.ToString();
            ds = GetChildGridRecords(((DataRowView)e.Row.DataItem)["ActivityItemId"].ToString());
            gv.DataSource = ds;
            gv.DataBind();

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtTotal = (TextBox)e.Row.FindControl("txtRCoreTotal");
                txtTotal.Text = RCoreTotal.ToString(); //total.ToString();
            }
        }
        Decimal totalRbuffer = 0.0m;
        protected void gvRBuffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            if (row.DataItem == null)
                return;

            DataSet ds = new DataSet();
            GridView gv = new GridView();
            Label lb = new Label();
            lb = (Label)row.FindControl("lblActivityItemId");
            DropDownList ddlSubItem = (e.Row.Cells[2].FindControl("ddlSubItem") as DropDownList);
            //ddlSubItem.DataSource = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lblActivityItemId.Text));
            DataSet dsgps = APOBAL.Instance.GetSubItemsActivityItemWise(Convert.ToInt32(lb.Text));
            DataRow rowgps = dsgps.Tables[0].NewRow();
            rowgps[0] = "0";
            rowgps[1] = "Please Select";
            dsgps.Tables[0].Rows.InsertAt(rowgps, 0);
            ddlSubItem.DataSource = dsgps;
            ddlSubItem.DataTextField = "SubItem";
            ddlSubItem.DataValueField = "SubItemId";
            ddlSubItem.DataBind();


            gv = (GridView)row.FindControl("ChildRecordsRecurringBuffer");
            string ActiviItemID = lb.Text.ToString();
            ds = GetChildGridRecords(((DataRowView)e.Row.DataItem)["ActivityItemId"].ToString());
            gv.DataSource = ds;
            gv.DataBind();

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtTotal = (TextBox)e.Row.FindControl("txtRBufferTotal");
                txtTotal.Text = RBufferTotal.ToString(); //total.ToString();
            }
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
                //Response.Redirect(rawUrl, false);
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
                //Response.Redirect(rawUrl, false);
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
                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId), oApo.SubItem.ToString());
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
                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId), oApo.SubItem.ToString());
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

                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId), oApo.SubItem.ToString());
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
                    GetGPSDetails(oApo.TigerReserveId, Convert.ToInt32(activityItemId), oApo.SubItem.ToString());
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>$( document ).ready(function() { $('#gpsModal').modal('show')});</script>", false);
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
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

        protected void txtNumberOfItemsChild_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChild");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChild");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);

        }

        protected void txtUnitPriceChild_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChild");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChild");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);
        }

        protected void txtNumberOfItemsChildNRBuffer_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChildNRBuffer");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChildNRBuffer");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);
        }

        protected void txtUnitPriceChildNRBuffer_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChildNRBuffer");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChildNRBuffer");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);
        }

        protected void txtNumberOfItemsChildRCore_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChildRCore");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChildRCore");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);
        }

        protected void txtUnitPriceChildRCore_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChildRCore");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChildRCore");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);
        }

        protected void txtNumberOfItemsChildRBuffer_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChildRBuffer");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChildRBuffer");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);
        }

        protected void txtUnitPriceChildRBuffer_TextChanged(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            var currentDetailsGridRow = ((GridView)((TextBox)sender).NamingContainer.NamingContainer).Rows[rowIndex];
            var currentDetailsGridView = (GridView)(currentDetailsGridRow.NamingContainer);

            TextBox txt = (TextBox)currentDetailsGridRow.FindControl("txtNumberOfItemsChildRBuffer");
            TextBox txt1 = (TextBox)currentDetailsGridRow.FindControl("txtUnitPriceChildRBuffer");
            TextBox txt2 = (TextBox)currentDetailsGridRow.FindControl("lblTotalChild");

            Int32 noofitems = 0;
            Decimal unitPrice = 0;
            if (txt.Text.Length > 0)
            {
                try
                {
                    noofitems = Convert.ToInt32(txt.Text);
                }
                catch (Exception ex) { }
            }

            if (txt1.Text.Length > 0)
            {
                try
                {
                    unitPrice = Convert.ToDecimal(txt1.Text);
                }
                catch (Exception ex) { }
            }

            txt2.Text = Convert.ToString(noofitems * unitPrice);
        }

    }
}