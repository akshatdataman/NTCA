using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.BAL;
using System.Net;
using System.Configuration;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class ViewUtilizatinCertificate : BasePage
    {
        UtilizationCertificate uCertificate = new UtilizationCertificate();
        CommonClass cc = new CommonClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            string currentFinancialYear = string.Empty;
            string previousFinancialYear = string.Empty;
            int previousApoFileId;
            string LoggedInUser = AuthoProvider.User;
            //DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (Session["APOFileId"] != null)
            {
                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                DataSet dsTigerReserveId = APOBAL.Instance.GetAPOStateAndTigerReserveId(apoFileId);
                if (dsTigerReserveId != null)
                //{
                //    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                //    uCertificate.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                //    uCertificate.FinancialYear = dr["FinancialYear"].ToString();
                //    uCertificate.PreviousFinancialYear = cc.GetPreviousFinancialYear(uCertificate.FinancialYear);
                //}
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
                            currentFinancialYear = uCertificate.FinancialYear;
                        }
                        if (!string.IsNullOrEmpty(currentFinancialYear))
                        {
                            //CommonClass cc = new CommonClass();
                            ViewState["PFY"] = cc.GetPreviousFinancialYear(currentFinancialYear);

                            DataSet dsPFY = APOBAL.Instance.GetPreviousYearAPOFileId(uCertificate.TigerReserveId, previousFinancialYear);
                            if (dsPFY.Tables[0].Rows.Count == 1)
                            {
                                DataRow drPFY = dsPFY.Tables[0].Rows[0];
                                previousApoFileId = Convert.ToInt32(drPFY["APOFileId"]);
                                //previousFinancialYear = Convert.ToString(drPFY["FinancialYear"]);
                                Session["APOFileId"] = previousApoFileId;
                            }
                        }
                    }
                }
                //Session["APOFileId"] = null;
            }

            //DataSet ds = UtilizationCertificateBAL.Instance.GetUtilizationCertificateDetails(uCertificate.TigerReserveId,uCertificate.PreviousFinancialYear);
            DataSet ds;
            if (ViewState["PFY"] != null)
                ds = UtilizationCertificateBAL.Instance.GetUtilizationCertificateDetails(uCertificate.TigerReserveId, ViewState["PFY"].ToString());
            else
                ds = UtilizationCertificateBAL.Instance.GetUtilizationCertificateDetails(uCertificate.TigerReserveId, previousFinancialYear);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    DataRow drNorthEast = ds.Tables[1].Rows[0];

                    uCertificate.APOFileNo = dr["ApoFileNumber"].ToString();
                    uCertificate.APOTitle = dr["APOTitle"].ToString();
                    if (!string.IsNullOrEmpty(dr["SanctionAmount"].ToString()))
                        uCertificate.SanctionAmount = Convert.ToDouble(dr["SanctionAmount"].ToString());
                    else
                        uCertificate.SanctionAmount = 0.0;
                    if (!string.IsNullOrEmpty(dr["SpentAmount"].ToString()))
                        uCertificate.UCAmount = Convert.ToDouble(dr["SpentAmount"].ToString());
                    else
                        uCertificate.UCAmount = 0.0;
                    if (!string.IsNullOrEmpty(dr["UnspentAmount"].ToString()))
                        uCertificate.UnSpendAmount = Convert.ToDouble(dr["UnspentAmount"].ToString());
                    else
                        uCertificate.UnSpendAmount = 0.0;
                    if (!string.IsNullOrEmpty(dr["NRTotal"].ToString()))
                        uCertificate.NRTotal = Convert.ToDouble(dr["NRTotal"].ToString());
                    else
                        uCertificate.NRTotal = 0.0;
                    if (!string.IsNullOrEmpty(dr["RTotal"].ToString()))
                        uCertificate.RTotal = Convert.ToDouble(dr["RTotal"].ToString());
                    else
                        uCertificate.RTotal = 0.0;
                    //if (!string.IsNullOrEmpty(dr["EcoTotal"].ToString()))
                    //    uCertificate.EcoTotal = Convert.ToDouble(dr["EcoTotal"].ToString());
                    //else
                    //    uCertificate.EcoTotal = 0.0;

                    uCertificate.FinancialYear = dr["FinancialYear"].ToString();
                    uCertificate.IsNorthEastState = Convert.ToBoolean(drNorthEast["IsNorthEastState"]);
                    //if (uCertificate.RTotal != 0.0)
                    //{
                    //    if (uCertificate.IsNorthEastState)
                    //    {
                    //        uCertificate.StateShare = (uCertificate.RTotal * 10) / 100;
                    //        uCertificate.CentralStateRatio = "90:10";
                    //    }
                    //    else
                    //    {
                    //        uCertificate.StateShare = (uCertificate.RTotal * 50) / 100;
                    //        uCertificate.CentralStateRatio = "50:50";
                    //    }
                    //}
                    //else
                    //    uCertificate.StateShare = 0;

                    //uCertificate.CentralShare = uCertificate.SanctionAmount - uCertificate.StateShare;
                    //uCertificate.FreshRelease = (uCertificate.CentralShare * 80) / 100;
                    //uCertificate.SecondInstalment = (uCertificate.CentralShare * 20) / 100;

                    if (!string.IsNullOrEmpty(dr["CentralShare"].ToString()))
                        uCertificate.CentralShare = Convert.ToDouble(dr["CentralShare"].ToString());
                    else
                        uCertificate.CentralShare = 0;


                    uCertificate.StateShare = uCertificate.SanctionAmount - uCertificate.CentralShare;
                    uCertificate.CentralStateRatio = "'" + uCertificate.CentralShare * 100 / uCertificate.SanctionAmount + "':'" + uCertificate.StateShare * 100 / uCertificate.SanctionAmount + "'";

                    if (!string.IsNullOrEmpty(dr["FirstCentralRelease"].ToString()))
                        uCertificate.FreshRelease = Convert.ToDouble(dr["FirstCentralRelease"].ToString());
                    else
                        uCertificate.FreshRelease = 0;
                    if (!string.IsNullOrEmpty(dr["SecondCentralRelease"].ToString()))
                        uCertificate.SecondInstalment = Convert.ToDouble(dr["SecondCentralRelease"].ToString());
                    else
                        uCertificate.SecondInstalment = 0;
                }
            }

            DataTable table = new DataTable();

            table.Columns.Add("letter1", typeof(string));
            table.Columns.Add("letter2", typeof(string));
            table.Columns.Add("letter3", typeof(string));
            table.Columns.Add("letter4", typeof(string));
            table.Columns.Add("letter5", typeof(string));
            table.Columns.Add("letter6", typeof(string));
            table.Columns.Add("letter7", typeof(string));
            table.Columns.Add("letter8", typeof(string));

            table.Columns.Add("Amount1", typeof(string));
            table.Columns.Add("Amount2", typeof(string));
            table.Columns.Add("Amount3", typeof(string));
            table.Columns.Add("Amount4", typeof(string));
            table.Columns.Add("Amount5", typeof(string));
            table.Columns.Add("Amount6", typeof(string));
            table.Columns.Add("Amount7", typeof(string));
            table.Columns.Add("Amount8", typeof(string));
            table.Columns.Add("Amount9", typeof(string));


            table.Columns.Add("Description", typeof(string));



            //table.Columns.Add("Patient", typeof(string));
            //table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(uCertificate.APOFileNo, DateTime.Now.Date, "", DateTime.Now.Date, uCertificate.UnSpendAmount,
                uCertificate.StateShare, uCertificate.APOFileNo, DateTime.Now.Date, "", "", uCertificate.FreshRelease, uCertificate.UnSpendAmount,
                uCertificate.StateShare, "", "", uCertificate.SecondInstalment, uCertificate.SanctionAmount,
                "Certified that out of <b>Rs.<span>" + uCertificate.FreshRelease + "</span></b> Lakhs grant-in-aid Sanctioned and released as First Instalment Under CSS 'Project Tiger ' during the year <b><span>"
                + uCertificate.FinancialYear + "</span></b> in favour of <b><span>" + uCertificate.APOTitle + "</span></b> under <b><span>"
                + uCertificate.APOFileNo + "</span></b> letter No. given the margin received a sum of <b>Rs. <span>"
                + uCertificate.UnSpendAmount + "</span></b> lakhs adjustment with the unsent amount of <b>Rs. <span>"
                + uCertificate.UnSpendAmount + "</span></b> lakhs of the previous year(lying with DC chirang), <b>Rs. <span>"
                + uCertificate.StateShare + "</span></b> Lakhs being state share(shared Central:State - " + uCertificate.CentralStateRatio + ") and released and received as second Instalment of <b>Rs. <span>"
                + uCertificate.SecondInstalment + "</span></b> Video letter indicated in the margin, a total amount of <b>Rs. <span>"
                + uCertificate.SanctionAmount + "</span></b> has been Utilized for implementation of schemes for which it was sanctioned under css 'Project Tiger' <b><span>"
                + uCertificate.APOTitle + "</span></b> during <b><span>" + uCertificate.FinancialYear + "</span></b>");

            gvView.DataSource = table;
            gvView.DataBind();
            if (!IsPostBack)
            {
                UserBAL.Instance.InsertAuditTrailDetail("Visited View Utilization Certificate Page", "UtilizationCertificate");
            }
        }

        protected void imgbtnPdf_click(object sender, ImageClickEventArgs e)
        {
            ExportUCToPDF();
        }
        private void ExportUCToPDF()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.pdf", "ucPDFExport"));
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //For First DataTable
            System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
            //DataGrid myDataGrid1 = new DataGrid();
            //myDataGrid1.DataSource = dt1;
            //myDataGrid1.DataBind();
            ucDiv.RenderControl(htmlWrite1);
            //For Second DataTable 
            //System.IO.StringWriter stringWrite2 = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);

            //gvRecuring.RenderControl(htmlWrite2);
            //You can add more DataTable
            StringReader sr = new StringReader(stringWrite1.ToString());
            Document pdfDoc = new Document(new Rectangle(288f, 144f), 10f, 10f, 10f, 0f);
            pdfDoc.SetPageSize(PageSize.A4.Rotate());
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        { }

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

                    DataSet ds = UtilizationCertificateBAL.Instance.GetUploadedFileName(Convert.ToInt32(Session["APOFileId"]));
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        //string ExistFile = ds.ToString();
                        //int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                        string fileName = Path.GetFileName(fuUploadFinalUC.PostedFile.FileName);
                        int extensionCount = 0;

                        string[] splitFile = fileName.Split('.');
                        //foreach (string ext in splitFile)
                        //{
                        //    if (ext.ToLower() == "jpg" || ext.ToLower() == "png" || ext.ToLower() == "gif" || ext.ToLower() == "sql" || ext.ToLower() == "psd" ||
                        //    ext.ToLower() == "pdf" || ext.ToLower() == "txt" || ext.ToLower() == "rtf" || ext.ToLower() == "doc" || ext.ToLower() == "docx" ||
                        //    ext.ToLower() == "xls" || ext.ToLower() == "xlsx" || ext.ToLower() == "html" || ext.ToLower() == "htm" || ext.ToLower() == ".jpeg" ||
                        //    ext.ToLower() == "exe")
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
                            if (dr[0].ToString() == fileName)
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
                                if (dr[0].ToString().Length > 0)
                                {
                                    var path = Path.Combine(Server.MapPath("~/Upload/UC/Final/") + dr[0]);
                                    System.IO.File.Delete(path);
                                }
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
                        }
                        else
                        {
                            string strerror = "Invalid File";
                            vmError.Message = strerror;
                            FlashMessage.InfoMessage(vmError.Message);
                        }
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
    }
}