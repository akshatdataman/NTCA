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
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class TigerReserveDetails : BasePage
    {
        TigerReserveDetail trDetail = new TigerReserveDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string LoggedInUser = AuthoProvider.User;
                DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
                if (dsTigerReserveId != null)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                        trDetail.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                }
                if (!IsPostBack)
                {
                    BindControls();
                    GetTigerReserveDetails();
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Update Tiger Reserve Detail Page", "Tiger Reserve Detail");
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
        private void BindControls()
        {
            DataSet dsTrDetails = TigerReserveDetailsBAL.Instance.GetTigerReserveMasterDetail(trDetail.TigerReserveId);
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            { 
                DataRow dr = dsTrDetails.Tables[0].Rows[0];
                txtTigerReserve.Text = Convert.ToString(dr["TigerReserveName"]);
                txtCoreArea.Text = Convert.ToString(dr["CoreArea"]);
                txtBufferArea.Text = Convert.ToString(dr["BufferArea"]);
                txtTotalArea.Text = Convert.ToString(dr["TotalArea"]);
                txtAddress.Text = Convert.ToString(dr["Address"]);
                //txtdateOfCreation.Text = Convert.ToString(dr["DateOfRegistration"]);
            }
            //ddlVehicleType.DataSource = dsTrDetails.Tables[1];
            //ddlVehicleType.DataValueField = "VehicleTypeId";
            //ddlVehicleType.DataTextField = "VehicleType";
            //ddlVehicleType.DataBind();
            //ddlVehicleType.Items.Insert(0, "Select");

            //ddlWireless.DataSource = dsTrDetails.Tables[2];
            //ddlWireless.DataValueField = "WirelessId";
            //ddlWireless.DataTextField = "WirelessName";
            //ddlWireless.DataBind();
            //ddlWireless.Items.Insert(0, "Select");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateTigerReserveDetails();
                UserBAL.Instance.InsertAuditTrailDetail("Updated Tiger Reserve Details", "Tiger Reserve Detail");
                string strSuccess = "Data has been updated successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
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
        private void GetTigerReserveDetails()
        {
            DataSet dsTrDetail = TigerReserveDetailsBAL.Instance.GetTigerReserveDetail(trDetail.TigerReserveId);
            if (dsTrDetail != null && dsTrDetail.Tables[0].Rows.Count >0)
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
        private void UpdateTigerReserveDetails()
        {
            //Administrative Details
            trDetail.TigerReserveName = txtTigerReserve.Text;
            trDetail.LegalStatus = txtLegalStatus.Text;
            if (!string.IsNullOrEmpty(txtCoreArea.Text))
                trDetail.CoreArea = txtCoreArea.Text;
            else
                trDetail.CoreArea = "0.0";
            if (!string.IsNullOrEmpty(txtBufferArea.Text))
                trDetail.BufferArea = txtBufferArea.Text;
            else
                trDetail.BufferArea = "0.0";
            txtTotalArea.Text = Convert.ToString(Convert.ToDouble(trDetail.CoreArea) + Convert.ToDouble(trDetail.BufferArea));
            if (!string.IsNullOrEmpty(txtCoreAreaVillageNumber.Text))
                trDetail.CoreAreaVillageNumber = Convert.ToInt32(txtCoreAreaVillageNumber.Text);
            else
                trDetail.CoreAreaVillageNumber = 0;
            trDetail.SettlementStatus = txtSettlementStatus.Text;
            trDetail.TigerConservationPlan = txtTigerConservationPlan.Text;
            trDetail.NameofPost = txtNameofPost.Text;
            trDetail.SanctionedStrength = txtSanctionedStrength.Text;
            if (!string.IsNullOrEmpty(txtStaffInPosition.Text))
                trDetail.StaffInPosition = Convert.ToInt32(txtStaffInPosition.Text);
            else
                trDetail.StaffInPosition = 0;
            if (!string.IsNullOrEmpty(txtVacant.Text))
                trDetail.Vacant = Convert.ToInt32(txtVacant.Text);
            else
                trDetail.Vacant = 0;
            trDetail.WildlifeTraining = txtWildlifeTraining.Text;
            trDetail.WildlifeTrainedStaff = txtWildlifeTrainedStaff.Text;
            trDetail.CasualothersStaff = txtCasualothersStaff.Text;
            trDetail.TypeOfWeaponsNumber = txtTypeOfWeaponsNumber.Text;
            trDetail.ShootingOfFilmsDocumentaries = txtShootingOfFilmsDocumentaries.Text;
            trDetail.Address = txtAddress.Text;
            trDetail.VehicleType = txtVehicleType.Text;
            trDetail.Wireless = txtWireless.Text;
            trDetail.BarriersDetails = txtBarriersDetails.Text;
            if (!string.IsNullOrEmpty(txtNumberOfBarriers.Text))
                trDetail.NumberOfBarriers = Convert.ToInt32(txtNumberOfBarriers.Text);
            else
                trDetail.NumberOfBarriers = 0;
            trDetail.TotalArea = txtTotalArea.Text;
            trDetail.DivisionArea = txtDivisionArea.Text;
            trDetail.SubDivisionArea = txtSubDivisionArea.Text;
            trDetail.Ranges = txtRange.Text;
            trDetail.Beats = txtBeats.Text;
            trDetail.Sections = txtSections.Text;
            trDetail.AntiPoachingCampDetails = txtAntiPoachingCampDetails.Text;
            trDetail.WatchTower = txtWatchTower.Text;
            trDetail.NameAndTenureOfIncumbents = txtNameAndTenureOfIncumbents.Text;
            trDetail.CaptiveElephants = txtCaptiveElephants.Text;
            trDetail.SpecialTigerProtectionForce = txtSpecialTigerProtectionForce.Text;
            trDetail.SteeringCommitteeFoundationStatus = txtSteeringCommitteeFoundationStatus.Text;
            trDetail.TigerConservationFoundation = txtTigerConservationFoundation.Text;
            
            trDetail.WildlifeOtherInformation = txtWildlifeOtherInformation.Text;

            //Ecological Details
            trDetail.EstimationReportForLast3Years = txtEstimationReportForLast3Years.Text;
            trDetail.ImportantSpeciesAnimalsFoundInTR = txtImportantSpeciesAnimalsFoundInTR.Text;
            trDetail.WildlifePopulationEstimates = txtWildlifePopulationEstimates.Text;
            trDetail.DeathOfAnimals = txtDeathOfAnimals.Text;
            trDetail.Firelines = txtFirelines.Text;
            trDetail.ForestType = txtForestType.Text;
            trDetail.AnyotherImportantWildlifeInformationUntilLastYear = txtAnyotherImportantWildlifeInformationUntilLastYear.Text;

            //Financial Details
            trDetail.RevenueGeneratedInLast5YearsTourism = txtRevenueGeneratedInLast5YearsTourism.Text;
            trDetail.RevenueGeneratedInLast5YearsOthers = txtRevenueGeneratedInLast5YearsOthers.Text;
            if (!string.IsNullOrEmpty(txtAnnualNoOfTourists.Text))
                trDetail.AnnualNoOfTourists = Convert.ToInt32(txtAnnualNoOfTourists.Text);
            else
                trDetail.AnnualNoOfTourists = 0;
            trDetail.FundsProvidedUnderStatePlanInLast5Years = txtFundsProvidedUnderStatePlanInLast5Years.Text;
            trDetail.FundsFromCAMPAandOtherResources = txtFundsFromCAMPAandOtherResources.Text;
            trDetail.FundsProvidedByCSSPT = txtFundsProvidedByCSSPT.Text;
            trDetail.ExGratiaPaidInLast5Years = txtExGratiaPaidInLast5Years.Text;
            trDetail.CropDamage = txtCropDamage.Text;
            trDetail.AnyOtherImportantFinancialCWWInformationUntilLastYear = txtAnyOtherImportantFinancialCWWInformationUntilLastYear.Text;

            TigerReserveDetailsBAL.Instance.UpdateTigerReserveDetails(trDetail, trDetail.TigerReserveId);

        }
    }
}