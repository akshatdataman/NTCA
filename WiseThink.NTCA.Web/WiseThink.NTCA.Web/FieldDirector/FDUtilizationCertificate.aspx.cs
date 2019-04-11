using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using System.IO;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.App_Code;
using System.Configuration;
using System.Net;
using System.Linq;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class FDUtilizationCertificate : BasePage
    {
        UtilizationCertificate uCertificate = new UtilizationCertificate();
        CommonClass cc = new CommonClass();
        string currentFinancialYear = string.Empty;
        string previousFinancialYear = string.Empty;
        int currentApoFileId;
        int previousApoFileId;
        DataSet dsApoCounts;
        int _apoCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["NonRecurring"] = null;
            Session["Recurring"] = null;
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow drTrId = dsTigerReserveId.Tables[0].Rows[0];
                if (drTrId[0] != DBNull.Value)
                    uCertificate.TigerReserveId = Convert.ToInt32(drTrId["TigerReserveId"]);
                else
                    uCertificate.TigerReserveId = 0;

                if (uCertificate.TigerReserveId != 0)
                {
                    DataSet dsCFY = APOBAL.Instance.GetCurrentYearAPOFileId(uCertificate.TigerReserveId);
                    if (dsCFY.Tables[0].Rows.Count == 1)
                    {
                        DataRow drCFY = dsCFY.Tables[0].Rows[0];
                        uCertificate.FinancialYear = Convert.ToString(drCFY["FinancialYear"]);
                        currentApoFileId = Convert.ToInt32(drCFY["APOFileId"]);
                        currentFinancialYear = uCertificate.FinancialYear;
                    }
                    if (!string.IsNullOrEmpty(currentFinancialYear))
                    {
                        //CommonClass cc = new CommonClass();
                        previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
                        DataSet dsPFY = APOBAL.Instance.GetPreviousYearAPOFileId(uCertificate.TigerReserveId, previousFinancialYear);
                        if (dsPFY.Tables[0].Rows.Count == 1)
                        {
                            DataRow drPFY = dsPFY.Tables[0].Rows[0];
                            previousApoFileId = Convert.ToInt32(drPFY["APOFileId"]);
                            previousFinancialYear = Convert.ToString(drPFY["FinancialYear"]);
                            Session["APOFileId"] = previousApoFileId;
                        }
                    }
                }
            }
            if (!IsPostBack)
            {
                SettledUcDiv.Attributes.Add("style", "display:none");
                UserBAL.Instance.InsertAuditTrailDetail("Visited Generate Utilization Certificate Page", "UtilizationCertificate");
            }
            //DataTable table = new DataTable();
            //table.Columns.Add("SrNo", typeof(int));
            //table.Columns.Add("SanctionAmount", typeof(string));

            //table.Columns.Add("SanctionOn", typeof(string));
            //table.Columns.Add("UCStatus", typeof(string));
            //table.Columns.Add("UCAmount", typeof(string));

            //table.Rows.Add(1, "500002", "20/05/2015", "Submitted", "460000");
            //table.Rows.Add(2, "600002", "25/07/2015", "Pending", "NIL");

            //gvUtilizationCertificate.DataSource = table;
            //gvUtilizationCertificate.DataBind();
        }
        private void GetUcDetails(int tigerReserveId, string previousYFinancialYear)
        {
            DataSet dsUc = UtilizationCertificateBAL.Instance.GetUtilizationCertificateDetails(tigerReserveId, previousYFinancialYear);
            if (dsUc.Tables[0].Rows.Count > 0)
            {
                gvUtilizationCertificate.DataSource = dsUc;
                gvUtilizationCertificate.DataBind();
                DataRow dr = dsUc.Tables[0].Rows[0];
                uCertificate.SanctionAmount = Convert.ToDouble(dr["SanctionAmount"]);
                if (!string.IsNullOrEmpty(dr["SpentAmount"].ToString()))
                    uCertificate.UCAmount = Convert.ToDouble(dr["SpentAmount"]);
                else
                    uCertificate.UCAmount = 0.0;
                if (!string.IsNullOrEmpty(dr["UnspentAmount"].ToString()))
                    uCertificate.UnSpendAmount = Convert.ToDouble(dr["UnspentAmount"]);
                else
                    uCertificate.UnSpendAmount = 0.0;
                //if (uCertificate.SanctionAmount > uCertificate.UCAmount)
                //{
                //    SettledUcDiv.Attributes.Add("Style", "display:block");
                //}
                if (!string.IsNullOrEmpty(dr["FirstCentralRelease"].ToString()))
                    uCertificate.FreshRelease = Convert.ToDouble(dr["FirstCentralRelease"]);
                else
                    uCertificate.FreshRelease = 0.0;
                if (!string.IsNullOrEmpty(dr["SecondCentralRelease"].ToString()))
                    uCertificate.SecondRelease = Convert.ToDouble(dr["SecondCentralRelease"]);
                else
                    uCertificate.SecondRelease = 0.0;
                if (uCertificate.FreshRelease + uCertificate.SecondRelease > uCertificate.UCAmount)
                {
                    SettledUcDiv.Attributes.Add("Style", "display:block");
                }
            }
            else
            {
                string strError = "Oops!! there's no data to generate utilization certificate.";
                vmError.Message = strError;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
            }
        }
        private bool IsUnspentAmountSettled(int tigerReserveId)
        {
            bool IsUnspent = false;

            if (tigerReserveId != 0)
            {
                dsApoCounts = CheckListBAL.Instance.GetApoCounts(tigerReserveId);
                if (dsApoCounts.Tables[0].Rows.Count == 1)
                {
                    DataRow drApoCount = dsApoCounts.Tables[0].Rows[0];
                    _apoCount = Convert.ToInt32(drApoCount["ApoCounts"]);
                }
                if (_apoCount == 1)
                    IsUnspent = true;
                else if (_apoCount > 1)
                {
                    DataSet dsCFY = APOBAL.Instance.GetCurrentYearAPOFileId(tigerReserveId);
                    if (dsCFY.Tables[0].Rows.Count == 1)
                    {
                        DataRow drCFY = dsCFY.Tables[0].Rows[0];
                        currentFinancialYear = Convert.ToString(drCFY["FinancialYear"]);
                    }
                    if (!string.IsNullOrEmpty(currentFinancialYear))
                    {
                        CommonClass cc = new CommonClass();
                        previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
                        DataSet dsPFY = APOBAL.Instance.GetPreviousYearAPOFileId(tigerReserveId, previousFinancialYear);
                        if (dsPFY.Tables[0].Rows.Count == 1)
                        {
                            DataRow drPFY = dsPFY.Tables[0].Rows[0];
                            previousApoFileId = Convert.ToInt32(drPFY["APOFileId"]);
                            previousFinancialYear = Convert.ToString(drPFY["FinancialYear"]);
                        }
                        DataSet dsUnspent = UtilizationCertificateBAL.Instance.GetUtilizationCertificateDetails(tigerReserveId, previousFinancialYear);
                        if (dsUnspent.Tables[0].Rows.Count == 1)
                        {
                            DataRow dr = dsUnspent.Tables[0].Rows[0];
                            uCertificate.SanctionAmount = Convert.ToDouble(dr["SanctionAmount"]);
                            if (!string.IsNullOrEmpty(dr["SpentAmount"].ToString()))
                                uCertificate.UCAmount = Convert.ToDouble(dr["SpentAmount"]);
                            else
                                uCertificate.UCAmount = 0.0;
                            if (!string.IsNullOrEmpty(dr["UnspentAmount"].ToString()))
                                uCertificate.UnSpendAmount = Convert.ToDouble(dr["UnspentAmount"]);
                            else
                                uCertificate.UnSpendAmount = 0.0;
                        }
                        if (uCertificate.SanctionAmount == uCertificate.UCAmount + uCertificate.UnSpendAmount)
                            IsUnspent = true;
                        else
                            IsUnspent = false;
                    }

                }
            }
            return IsUnspent;
        }
        protected void btnGenerateUC_Click(object sender, EventArgs e)
        {
            if (!IsUnspentAmountSettled(uCertificate.TigerReserveId))
            {
                SettledUcDiv.Attributes.Add("style", "display:block");
                string strError = ConfigurationManager.AppSettings["UnspentAmountNotSettled"];
                vmError.Message = strError;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
            }
            GetUcDetails(uCertificate.TigerReserveId, previousFinancialYear);
            UserBAL.Instance.InsertAuditTrailDetail("Generated Utilization Certificate", "UtilizationCertificate");
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvUtilizationCertificate.Rows)
                {
                    FileUpload fuUpload = (FileUpload)row.FindControl("fuUpload");

                    if (fuUpload.HasFile)
                    {
                        string fileName = Path.GetFileName(fuUpload.PostedFile.FileName);
                        fuUpload.PostedFile.SaveAs(Server.MapPath("~/Upload/ApoDocuments/") + fileName);
                        string uploadedFile = fuUpload.FileName;
                        string strError = ConfigurationManager.AppSettings["FinalUc"];
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void btnSettledUc_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FieldDirector/SettledUc.aspx", false);
        }

        string[] FileExtensionList = {"aif","cda","mid","midi","mp3","mpa","ogg","wav","wma","wpl","7z","arj","deb","pkg","rar",
                                         "rpm","targz","z","zip","bin","dmg","iso","toast","vcd","csv","dat","db","dbf","log","mdb",
                                         "sav","sql","tar","xml","apk","bat","bin","cgi","pl","com","exe","gadget","jar","py","wsf",
                                         "fnt","fon","otf","ttf","ai","bmp","gif","ico","jpeg","jpg","png","ps","psd","svg","tif","tiff",
                                         "asp","aspx","cer","cfm","cgi","pl","css","htm","html","js","jsp","part","php","py","rss","xhtml",
                                         "key","odp","pps","ppt","pptx","c","class","cpp","cs","h","java","sh","swift","vb","ods","xlr","xls","xlsx",
                                         "bak","cab","cfg","cpl","cur","dll","dmp","drv","icns","ico","ini","lnk","msi","sys","tmp","3g2","3gp","avi",
                                         "flv","h264","m4v","mkv","mov","mp4","mpg","mpeg","rm","swf","vob","wmv","doc","docx","odt","pdf","rtf","tex",
                                         "txt","wks","wps","wpd" };
        protected void btnUploadFinalUc_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuUploadFinalUC.HasFile)
                {
                    int extensionCount = 0;
                    string fileName = Path.GetFileName(fuUploadFinalUC.PostedFile.FileName);
                    string[] splitFile = fileName.Split('.');
                    //foreach (string ext in splitFile)
                    //{
                    //    //if (ext.ToLower() == "jpg" || ext.ToLower() == "png" || ext.ToLower() == "gif" || ext.ToLower() == "sql" || ext.ToLower() == "psd" || 
                    //    //    ext.ToLower() == "pdf" || ext.ToLower() == "txt" || ext.ToLower() == "rtf" || ext.ToLower() == "doc" || ext.ToLower() == "docx" ||
                    //    //    ext.ToLower() == "xls" || ext.ToLower() == "xlsx" || ext.ToLower() == "html" || ext.ToLower() == "htm" || ext.ToLower() == "jpeg" || 
                    //    //    ext.ToLower() == "exe")
                    //    {
                    //        extensionCount++;
                    //    }
                    //}
                    foreach (string ext in splitFile)
                    {
                        if (FileExtensionList.Contains(ext.ToLower()))
                        {
                            extensionCount++;
                        }
                    }
                    if (extensionCount <= 1)
                    {
                        fuUploadFinalUC.PostedFile.SaveAs(Server.MapPath("~/Upload/UC/Final/") + fileName);
                        uCertificate.FinalnalUC = fuUploadFinalUC.FileName;
                        Stream fs = fuUploadFinalUC.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        if (bytes[0] == 37 && bytes[1] == 80)
                        {
                            fs.Close();
                            br.Close();
                            fs.Dispose();
                            br.Dispose();
                            fuUploadFinalUC.PostedFile.InputStream.Close();
                            bytes = null;
                           
                            if (Session["APOFileId"] != null && !string.IsNullOrEmpty(uCertificate.FinalnalUC))
                            {
                                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                                UtilizationCertificateBAL.Instance.UploadFinalUC(apoFileId, uCertificate.FinalnalUC);
                                string strSuccess = ConfigurationManager.AppSettings["FinalUc"];
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                                lbtnFileName.Text = uCertificate.FinalnalUC;
                            }
                            else
                            {
                                string strError = ConfigurationManager.AppSettings["UcNotGenerated"];
                                vmError.Message = strError;
                                FlashMessage.ErrorMessage(vmError.Message);
                                return;
                            }
                        }
                        else
                        {
                            string strError = "Only PDF Files are allowed";
                            uCertificate.FinalnalUC = string.Empty;
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            File.Delete(Server.MapPath("~/Upload/UC/Final/") + fileName);
                            return;
                        }
                       
                    }
                    else
                    {
                        string strError = "Invalid File";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return;
                    }
                }
                else if (!string.IsNullOrEmpty(lblFileName.Text))
                    uCertificate.FinalnalUC = lblFileName.Text;
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void lbtnFileName_Click(object sender, EventArgs e)
        {
            string filePath = "~/Upload/UC/Final/" + fuUploadFinalUC.FileName.ToString();
            if (lbtnFileName.Text != string.Empty)
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                filePath = filePath + lbtnFileName.Text;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename='" + lbtnFileName.Text + "'");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    byte[] data = req.DownloadData(Server.MapPath(filePath));
                    response.BinaryWrite(data);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                response.End();
            }
        }

        protected void lbtnNonRecurring_Click(object sender, EventArgs e)
        {
            Session["NonRecurring"] = 1;
            Response.Redirect("SubmitQuarterlyReport.aspx");

        }

        protected void lbtnRecurring_Click(object sender, EventArgs e)
        {
            Session["Recurring"] = 1;
            Response.Redirect("SubmitQuarterlyReport.aspx");

        }
    }
}