using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class FDFlexiAmont : BasePage
    {
        CommonClass cc = new CommonClass();
        string currentFinancialYear = string.Empty;
        string previousFinancialYear = string.Empty;
        int previousApoFileId;
        CentralStateShare centralStateShare = new CentralStateShare();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string LoggedInUser = AuthoProvider.User;
                DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
                //if (dsTigerReserveId != null)
                //{
                //    DataRow drTrId = dsTigerReserveId.Tables[0].Rows[0];
                //    if (drTrId[0] != DBNull.Value)
                //        uCertificate.TigerReserveId = Convert.ToInt32(drTrId["TigerReserveId"]);
                //    uCertificate.FinancialYear = drTrId["FinancialYear"].ToString();
                //    uCertificate.PreviousFinancialYear = cc.GetPreviousFinancialYear(uCertificate.FinancialYear);

                //}
                if (dsTigerReserveId != null)
                {
                    DataRow drTrId = dsTigerReserveId.Tables[0].Rows[0];
                    if (drTrId[0] != DBNull.Value)
                        centralStateShare.TigerReserveId = Convert.ToInt32(drTrId["TigerReserveId"]);
                    else
                        centralStateShare.TigerReserveId = 0;

                    if (centralStateShare.TigerReserveId != 0)
                    {
                        DataSet dsCFY = APOBAL.Instance.GetCurrentYearAPOFileId(centralStateShare.TigerReserveId);
                        if (dsCFY.Tables[0].Rows.Count == 1)
                        {
                            DataRow drCFY = dsCFY.Tables[0].Rows[0];
                            centralStateShare.CurrentFinancialYear = Convert.ToString(drCFY["FinancialYear"]);
                            currentFinancialYear = centralStateShare.CurrentFinancialYear;
                            centralStateShare.APOId = Convert.ToInt32(drCFY["APOFileId"]);
                            centralStateShare.PreviousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
                        }
                        if (!string.IsNullOrEmpty(currentFinancialYear))
                        {
                            //CommonClass cc = new CommonClass();
                            previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
                            DataSet dsPFY = APOBAL.Instance.GetPreviousYearAPOFileId(centralStateShare.TigerReserveId, previousFinancialYear);
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
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Flexi Amount Page", "Flexi Amount");
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }

        }

        protected void btnCalculateFlexi_Click(object sender, EventArgs e)
        {

            try
            { 
                DataSet ds = CentralStateShareBAL.Instance.GetCentralStateShare(centralStateShare.TigerReserveId, centralStateShare.PreviousFinancialYear);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    //centralStateShare.FirstCentralRelease = Convert.ToDouble(dr["CentralFirstInstallmentToBeRelease"]);
                    //centralStateShare.SecondCentralRelease = Convert.ToDouble(dr["CentralSecondInstallmentToBeRelease"]);
                    //centralStateShare.FirstStateRelease = Convert.ToDouble(dr["StateFirstInstallmentToBeRelease"]);
                    //centralStateShare.SecondStateRelease = Convert.ToDouble(dr["StateSecondInstallmentToBeRelease"]);

                    centralStateShare.CentralShare = Convert.ToDouble(dr["CentralShare"]);
                    centralStateShare.StateShare = Convert.ToDouble(dr["StateShare"]);
                }
                txtCentralFlexiAmount.Text = Convert.ToString((centralStateShare.CentralShare * 10) / 100);
                txtStateFlexiAmount.Text = Convert.ToString((centralStateShare.StateShare * 10) / 100);

                UserBAL.Instance.InsertAuditTrailDetail("Calculated Flexi Amount", "Flexi Amount");
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

    }
}