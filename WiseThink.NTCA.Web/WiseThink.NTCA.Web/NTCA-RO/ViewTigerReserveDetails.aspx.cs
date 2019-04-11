using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;
using System.Text;
using System.Configuration;
using System.IO;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class ViewTigerReserveDetails : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgbtnWord.Style.Add("display", "none");
            imgbtnPdf.Style.Add("display", "none");
            int tigerReserveId = 0;
            if (Session["APOFileId"] != null)
            {
                DataSet dsTrId = APOBAL.Instance.GetAPOStateAndTigerReserveId(Convert.ToInt32(Session["APOFileId"]));
                DataRow dr = dsTrId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                 tigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
            }
            if (!IsPostBack)
            {
                try
                {
                    GetTigerReserveDetails(tigerReserveId);
                    UserBAL.Instance.InsertAuditTrailDetail("Visited View Tiger Reserve Detail Page", "Tiger Reserve Detail");
                }
                catch (Exception ex)
                {
                    LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                    Response.RedirectPermanent("~/ErrorPage.aspx", false);
                }
            }
        }
        protected void imgbtnWord_click(object sender, ImageClickEventArgs e)
        {
            ExportDivContentToMsWord();
        }
        protected void imgbtnPdf_click(object sender, ImageClickEventArgs e)
        {
            ExportDivContentToPDF();
        }
        private void GetTigerReserveDetails(int tigerReserveId)
        {
            DataSet dsTrDetail = TigerReserveDetailsBAL.Instance.GetTigerReserveDetail(tigerReserveId);
            if (dsTrDetail != null)
            {
                DataRow dr = dsTrDetail.Tables[0].Rows[0];
                txtTigerReserve.Text = Convert.ToString(dr["TigerReserveName"]);
                txtLegalStatus.Text = Convert.ToString(dr["LegalStatus"]);
                txtCoreArea.Text = Convert.ToString(dr["CoreArea"]);
                txtBufferArea.Text = Convert.ToString(dr["BufferArea"]);
                txtCoreAreaVillageNumber.Text = Convert.ToString(dr["CoreAreaVillageNumber"]);

                txtSettlementStatus.Text = Convert.ToString(dr["SettlementStatus"]);
                txtTigerConservationPlan.Text = Convert.ToString(dr["TigerConservationPlan"]);
                txtNameofPost.Text = Convert.ToString(dr["NameofPost"]);
                txtSanctionedStrength.Text = Convert.ToString(dr["SanctionedStrength"]);

                txtStaffInPosition.Text = Convert.ToString(dr["StaffInPosition"]);

                txtVacant.Text = Convert.ToString(dr["Vacant"]);

                txtWildlifeTraining.Text = Convert.ToString(dr["WildlifeTraining"]);
                txtWildlifeTrainedStaff.Text = Convert.ToString(dr["WildlifeTrainedStaff"]);
                txtCasualothersStaff.Text = Convert.ToString(dr["CasualothersStaff"]);
                txtTypeOfWeaponsNumber.Text = Convert.ToString(dr["TypeOfWeaponsNumber"]);
                txtShootingOfFilmsDocumentaries.Text = Convert.ToString(dr["ShootingOfFilmsDocumentaries"]);
                txtAddress.Text = Convert.ToString(dr["Address"]);
                txtVehicleType.Text = Convert.ToString(dr["VehicleType"]);
                txtWireless.Text = Convert.ToString(dr["Wireless"]);
                txtBarriersDetails.Text = Convert.ToString(dr["BarriersDetails"]);

                txtNumberOfBarriers.Text = Convert.ToString(dr["NumberOfBarriers"]);

                //txtTotalArea.Text = Convert.ToString(dr["TotalArea"]);
                txtDivisionArea.Text = Convert.ToString(dr["DivisionArea"]);
                txtSubDivisionArea.Text = Convert.ToString(dr["SubDivisionArea"]);
                txtRange.Text = Convert.ToString(dr["Ranges"]);
                txtBeats.Text = Convert.ToString(dr["Beats"]);
                txtSections.Text = Convert.ToString(dr["Sections"]);
                txtAntiPoachingCampDetails.Text = Convert.ToString(dr["AntiPoachingCampDetails"]);
                txtWatchTower.Text = Convert.ToString(dr["WatchTower"]);
                txtNameAndTenureOfIncumbents.Text = Convert.ToString(dr["NameAndTenureOfIncumbents"]);
                txtCaptiveElephants.Text = Convert.ToString(dr["CaptiveElephants"]);
                txtSpecialTigerProtectionForce.Text = Convert.ToString(dr["SpecialTigerProtectionForce"]);
                //txtSteeringCommitteeFoundationStatus.Text = Convert.ToString(dr["SteeringCommitteeFoundationStatus"]);
                txtTigerConservationFoundation.Text = Convert.ToString(dr["TigerConservationFoundation"]);

                txtWildlifeOtherInformation.Text = Convert.ToString(dr["WildlifeOtherInformation"]);

                //Ecological Details
                txtEstimationReportForLast3Years.Text = Convert.ToString(dr["EstimationReportForLast3Years"]);
                txtImportantSpeciesAnimalsFoundInTR.Text = Convert.ToString(dr["ImportantSpeciesAnimalsFoundInTR"]);
                txtWildlifePopulationEstimates.Text = Convert.ToString(dr["WildlifePopulationEstimates"]);
                txtDeathOfAnimals.Text = Convert.ToString(dr["DeathOfAnimals"]);
                txtFirelines.Text = Convert.ToString(dr["Firelines"]);
                txtForestType.Text = Convert.ToString(dr["ForestType"]);
                txtAnyotherImportantWildlifeInformationUntilLastYear.Text = Convert.ToString(dr["AnyotherImportantWildlifeInformationUntilLastYear"]);

                //Financial Details
                txtRevenueGeneratedInLast5YearsTourism.Text = Convert.ToString(dr["RevenueGeneratedInLast5YearsTourism"]);
                txtRevenueGeneratedInLast5YearsOthers.Text = Convert.ToString(dr["RevenueGeneratedInLast5YearsOthers"]);

                txtAnnualNoOfTourists.Text = Convert.ToString(dr["AnnualNoOfTourists"]);

                txtFundsProvidedUnderStatePlanInLast5Years.Text = Convert.ToString(dr["FundsProvidedUnderStatePlanInLast5Years"]);
                txtFundsFromCAMPAandOtherResources.Text = Convert.ToString(dr["FundsFromCAMPAandOtherResources"]);
                txtFundsProvidedByCSSPT.Text = Convert.ToString(dr["FundsProvidedByCSSPT"]);
                txtExGratiaPaidInLast5Years.Text = Convert.ToString(dr["ExGratiaPaidInLast5Years"]);
                txtCropDamage.Text = Convert.ToString(dr["CropDamage"]);
                txtAnyOtherImportantFinancialCWWInformationUntilLastYear.Text = Convert.ToString(dr["AnyOtherImportantFinancialCWWInformationUntilLastYear"]);
            }
        }

        private void ExportDivContentToPDF()
        {
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            try
            {
                // create an API client instance
                string userName = ConfigurationManager.AppSettings["pdfcrowdUsername"].ToString();
                string APIKey = ConfigurationManager.AppSettings["pdfcrowdAPIKey"].ToString();
                pdfcrowd.Client client = new pdfcrowd.Client(userName, APIKey);

                // convert a web page and write the generated PDF to a memory stream
                MemoryStream Stream = new MemoryStream();
                //client.convertURI("http://www.google.com", Stream);

                // set HTTP response headers
                Response.Clear();
                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Cache-Control", "max-age=0");
                Response.AddHeader("Accept-Ranges", "none");
                Response.AddHeader("Content-Disposition", "attachment; filename=TigerReservePdfExport.pdf");
                System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
                MainContent.RenderControl(htmlWrite1);
                client.convertHtml(stringWrite1.ToString(), Stream);
                // send the generated PDF
                Stream.WriteTo(Response.OutputStream);
                Stream.Close();
                Response.Flush();
                Response.End();
            }
            catch (pdfcrowd.Error why)
            {
                Response.Write(why.ToString());
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
                //as a Word document with the name of your choice
                Response.AppendHeader("Content-Type", "application/msword");
                Response.AppendHeader("Content-disposition", "attachment; filename=TigerReserveWordExport.doc");

                Response.Write(strBody.ToString());

                Response.Flush();
                Response.End();
            }

            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
    }
}