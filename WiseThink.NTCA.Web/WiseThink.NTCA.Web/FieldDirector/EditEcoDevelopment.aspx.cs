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


namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class EditEcoDevelopment : BasePage
    {
        public bool IsEdit = false;
        APO oApo = new APO();
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                oApo.LoggedInUser = LoggedInUser;
            }
            if (!IsPostBack)
            {
                if (Session["IsEdit"] != null)
                {
                    IsEdit = (bool)Session["IsEdit"];
                    btnEcoDevCoreSave.Text = "Update";
                    btnEcoDevBufferSave.Text = "Udate";
                }
                GetApoFormat();
            }

        }

        private void GetApoFormat()
        {
            DataSet dsApo = new DataSet();
            if (IsEdit == true)
                dsApo = APOBAL.Instance.GetApoForModification(Convert.ToInt32(oApo.TigerReserveId));
            else
                dsApo = APOBAL.Instance.GetApoFormat();
            if (dsApo.Tables[4].Rows.Count > 0)
            {
                gvEcoDevCore.DataSource = dsApo.Tables[4];
                gvEcoDevCore.DataBind();
                LoadGridviewDraftData(gvEcoDevCore);
            }
            if (dsApo.Tables[5].Rows.Count > 0)
            {
                gvEcoDevBuffer.DataSource = dsApo.Tables[5];
                gvEcoDevBuffer.DataBind();
                LoadGridviewDraftData(gvEcoDevBuffer);
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
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
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
                                Label lblActivityId = (Label)row.FindControl("lblActivityId");
                                Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                                LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                                TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                                TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                                TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                                TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                TextBox txtGPS = (TextBox)row.FindControl("txtGPS");
                                TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                                FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");

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
                                    txtGPS.Text = dr["GPS"].ToString();
                                    txtJustification.Text = dr["Justification"].ToString();
                                    //fuUploadDocument.FileName = dr["NumberOfItems"].ToString();
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
                //return;
            }
        }
        private void InsertGridviewToDatabase(GridView gv)
        {
            try
            {
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
                        if (Session["IsEdit"] == null)
                        {
                            APOBAL.Instance.SubmitAPO(oApo);
                            string strSuccess = "Data has been saved successfully.";
                            vmSuccess.Message = strSuccess;
                            FlashMessage.InfoMessage(vmSuccess.Message);
                        }
                        else
                        {
                            //Label Id = (Label)row.FindControl("lblId");
                            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                            //oApo.APOId = Convert.ToInt32(Id.Text);
                            oApo.APOId = Convert.ToInt32(hdnId.Value);
                            oApo.LoggedInUser = AuthoProvider.User;
                            APOBAL.Instance.ModifyAPO(oApo);
                            string strSuccess = "Data has been updated successfully.";
                            vmSuccess.Message = strSuccess;
                            FlashMessage.InfoMessage(vmSuccess.Message);
                            //Session["IsEdit"] = null;
                        }
                    }
                    //else 
                    //{
                    //    oApo.NumberOfItems = 0.0;
                    //    oApo.UnitPrice = 0.0;
                    //    oApo.Total = 0.0;
                    //    oApo.IsFilled = false;
                    //}

                }
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
        protected void btnEcoDevCoreSave_Click(object sender, EventArgs e)
        {
            try
            {
                oApo.AreaId = 1;
                oApo.ActivityTypeId = 5;
                //ModifyApo(gvNRCore);
                InsertGridviewToDatabase(gvEcoDevCore);
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

        protected void btnEcoDevBufferSave_Click(object sender, EventArgs e)
        {
            try
            {
                oApo.AreaId = 2;
                oApo.ActivityTypeId = 6;
                InsertGridviewToDatabase(gvEcoDevBuffer);
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
    
    }
}