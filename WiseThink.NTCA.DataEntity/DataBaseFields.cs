using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace WiseThink
{
    public class DataBaseFields
    {
        // Login table
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string Role = "Role";
        public const string IsActive = "IsActive";
        //public const string Pwd = "Password";


        // Login Error Info
        public const string Error_Count="Error_Count";
        public const string Lock_Time = "Lock_Time";
        public const string Create_Time = "Create_Time";
        public const string Update_Time = "Update_Time";

        // Audit trail details
        public const string IPAddress = "IPAddress";
        public const string LoginDateTime = "LoginDateTime";
        public const string LogoutDateTime = "LogoutDateTime";
        public const string BrowserType = "BrowserType";
        public const string AuditPageVisited = "AuditPageVisited";
        public const string AuditPageReferer = "AuditPageReferer";
        public const string ActionPerformed = "ActionPerformed";
        public const string ActionDate = "ActionDate";
        public const string ModuleName = "ModuleName";
        public const string LoginStatus = "LoginStatus";

        //User Module field
        public const string Password = "Password";
        public const string Title = "Title";
        public const string FirstName = "FirstName";
        public const string MiddleName = "MiddleName";
        public const string LastName = "LastName";
        public const string DesignationId = "DesignationId";
        public const string Designation = "Designation";
        //public const string DateOfBirth = "DateOfBirth";
        public const string Gender = "Gender";
        public const string RoleID = "Role";
        public const string Address = "Address";
        public const string PinCode = "PinCode";
        public const string City = "City";
        public const string District = "District";
        public const string State = "State";
        public const string Country = "Country";
        public const string PhoneNumber = "PhoneNumber";
        public const string MobileNumber = "MobileNumber";
        public const string FaxNumber = "FaxNumber";
        public const string Email = "Email";
        public const string Question = "Question";
        public const string Answer = "Answer";


        //Declared By Zahir for Login module
        public const string RoleName = "RoleName";
       // public const string Password = "Password";
        public const string IsLocked = "isLocked";
        public const string lock_time = "lock_time";
        public const string isFirstLogin = "isFirstLogin";

        //Manage Activity Fields
        public const string AREA_ID = "AreaId";
        public const string ACTIVITY_TYPE_ID = "ActivityTypeId";
        public const string CATEGORY_ID = "CategoryId";
        public const string CATEGORY_NAME = "CategoryName";
        
        public const string CATEGORY_IDs = "CategoryIds";
        public const string ACTIVITY_ID = "ActivityId";
        public const string ACTIVITY_NAME = "ActivityName";
        
        public const string ACTIVITY_IDs = "ActivityIDs";

        public const string ACTIVITY_ITEM_ID = "ActivityItemId";
        public const string ACTIVITY_ITEM_NAME = "ActivityItem";
        public const string PARA_NO_CSSPT_GUIDELINES = "ParaNoCSSPTGuidelines";
        
        public const string ACTIVITY_ITEM_IDs = "ActivityItemIds";

        public const string SUB_ACTIVITY_ITEM_NAME = "SubActivityItemName";
        public const string SUB_ACTIVITY_ITEM_ID = "SubActivityItemId";
        public const string IS_ACTIVE = "IsActive";
        public const string ActivitySubItemId = "SubItemId";
        public const string ParaNo = "ParaNo";
        public const string SubItemId = "SubItemId";
        public const string SubItem = "SubItem";
        public const string GpsStatus = "GpsStatus";
        public const string NoOfItem = "NoOfItem";       

        #region Report database field
        public const string EVALUATOR_ID = "EvaluatorID";
        public const string EVALUATOR_NAME = "EvaluatorName";
        public const string APPLICATION_ID = "AppId";
        public const string REGION_NAME = "RegionName";
        //Zoo Fields
        public const string ZOO_ID = "ZooId";
        public const string ZOO_NAME = "ZooName";
        public const string ZOO_ADDRESS = "ZooAddress";
        public const string ZOO_CONTAUTH_TEL = "ContAuthTel";
        public const string ZOO_CONTAUTH_MAIL = "ContAuthEmail";
        //Status Fields
        public const string STATUS_ID = "StatusId";
        public const string STATUS_TYPE = "StatusType";


        //City Fields
        public const string CITY_ID = "CityID";
        public const string CITY_NAME = "CityName";


        //State Fields
        public const string STATE_ID = "StateID";
        public const string STATE_NAME = "StateName";
        public const string IsNorthEastState = "IsNorthEastState";
        //Common Fields
        public const string FILTER_VALUE = "filterValue";
        public const string EXTRAFILTER_VALUE = "extraFilterValue";
        public const string FILTER_VALUE_Rec = "filterValueRec";
        public const string TODAY_DATE = "todayDate";
        public const string CURRENTFINANCIAL_ENDDATE = "currentFinancialEndDate";
        public const string CURRENTFINANCIAL_STARTDATE = "currentFinancialStartDate";
        //Tiger Reserve Field
        public const string TigerReserveId = "TigerReserveId";
        public const string TigerReserveName = "TigerReserveName";
        public const string CoreArea = "CoreArea";
        public const string BufferArea = "BufferArea";
        public const string TotalArea = "TotalArea";
        public const string DateOfRegistration = "DateOfRegistration";
        public const string MonthId = "MonthId";
       
        public const string FieldDirector = "FieldDirector";
        public const string AlternatePhoneNumber = "AlternatePhoneNumber";

        //Alerts 
        public const string UserIds = "UserIds";
        public const string LoggedInUser = "LoggedInUser";
        public const string EmailIds = "EmailIds";
        public const string Subject = "Subject";
        public const string Body = "Body";
        public const string APOId = "APOId";
        public const string SentTo = "SentTo";
       // public const string UserId = "UserId";
        //Documents
        public const string DocumentName = "DocumentName";

        //APO database fields
        public const string APOID = "APOID";
        public const string APOFileNo = "APOFileNo";
        public const string APOTitle = "APOTitle";
        public const string StatusId = "StatusId";
        public const string DateOfSubmission = "DateOfSubmission";
        public const string IsFilled = "IsFilled";
        public const string IsApproved = "IsApproved";
        public const string StateId = "StateId";
        public const string IsSanctioned = "IsSanctioned";
        public const string AreaId = "AreaId";
        public const string ActivityTypeId = "ActivityTypeId";
        public const string ActivityId = "ActivityId";
        public const string ActivityItemId = "ActivityItemId";
        public const string Id = "Id";
        public const string FinancialYear = "FinancialYear";
        public const string ParaNoCSSPTGuidelines = "ParaNoCSSPTGuidelines";
        public const string ParaNoTCP = "ParaNoTCP";
        public const string NumberOfItems = "NumberOfItems";
        public const string Specification = "Specification";
        public const string UnitPrice = "UnitPrice";
        public const string Total = "Total";
        public const string GPS = "GPS";
        public const string Justification = "Justification";
        public const string Document = "Document";
        public const string DateOfFilling = "DateOfFilling";
        public const string DateOfModification = "DateOfModification";
        public const string ModifiedBy = "ModifiedBy";
        public const string inputJSON = "inputJSON";
      
        //Obligations database fields
        public const string APOFileId = "APOFileId";
        public const string ObligationId = "ObligationId";
        public const string Descriptions = "Descriptions";
        //public const string TigerReserveId = "TigerReserveId";
        public const string CompledOrNotOrNotApplicable = "CompledOrNotOrNotApplicable";
        public const string LevelOfCompliance = "LevelOfCompliance";
        public const string ReasonIfNotCompiled = "ReasonIfNotCompiled";
        public const string GPSId = "GPSId";
        
        #endregion
        #region Quarterly report
        public const string QuarterId = "QuarterId";
        public const string PhysicalProgress = "PhysicalProgress";
        public const string CentralFinancialProgress = "CentralFinancialProgress";
        public const string StateFinancialProgress = "StateFinancialProgress";
        public const string FinancialTotal = "FinancialTotal";
        public const string Month = "MonthId";
        public const string FromDate = "FromDate";
        public const string ToDate = "ToDate";
        #endregion
        #region CheckList
        public const string CheckedOrNotApplicable = "CheckedOrNotApplicable";
        #endregion
        #region Feedback
        public const string Score = "Score";
        public const string ComplianceprocessUnderway = "ComplianceprocessUnderway";
        public const string Remarks = "Remarks";
        #endregion

        #region Update Tiger Reserve Details
        public const string LegalStatus = "LegalStatus";
        public const string CoreAreaVillageNumber = "CoreAreaVillageNumber";
        public const string SettlementStatus = "SettlementStatus";
        public const string TigerConservationPlan = "TigerConservationPlan";
        public const string NameofPost = "NameofPost";
        public const string SanctionedStrength = "SanctionedStrength";
        public const string StaffInPosition = "StaffInPosition";
        public const string Vacant = "Vacant";
        public const string WildlifeTraining = "WildlifeTraining";
        public const string WildlifeTrainedStaff = "WildlifeTrainedStaff";
        public const string CasualothersStaff = "CasualothersStaff";
        public const string TypeOfWeaponsNumber = "TypeOfWeaponsNumber";
        public const string ShootingOfFilmsDocumentaries = "ShootingOfFilmsDocumentaries";
        public const string VehicleType = "VehicleType";
        public const string Wireless = "Wireless";
        public const string BarriersDetails = "BarriersDetails";
        public const string NumberOfBarriers = "NumberOfBarriers";
        public const string DivisionArea = "DivisionArea";
        public const string SubDivisionArea = "SubDivisionArea";
        public const string Ranges = "Ranges";
        public const string Beats = "Beats";
        public const string Sections = "Sections";
        public const string AntiPoachingCampDetails = "AntiPoachingCampDetails";
        public const string WatchTower = "WatchTower";
        public const string NameAndTenureOfIncumbents = "NameAndTenureOfIncumbents";
        public const string CaptiveElephants = "CaptiveElephants";
        public const string SpecialTigerProtectionForce = "SpecialTigerProtectionForce";
        public const string TigerConservationFoundation = "TigerConservationFoundation";
        public const string WildlifeOtherInformation = "WildlifeOtherInformation";
        public const string EstimationReportForLast3Years = "EstimationReportForLast3Years";
        public const string ImportantSpeciesAnimalsFoundInTR = "ImportantSpeciesAnimalsFoundInTR";
        public const string WildlifePopulationEstimates = "WildlifePopulationEstimates";
        public const string DeathOfAnimals = "DeathOfAnimals";
        public const string Firelines = "Firelines";
        public const string ForestType = "ForestType";
        public const string AnyotherImportantWildlifeInformationUntilLastYear = "AnyotherImportantWildlifeInformationUntilLastYear";
        public const string RevenueGeneratedInLast5YearsTourism = "RevenueGeneratedInLast5YearsTourism";
        public const string RevenueGeneratedInLast5YearsOthers = "RevenueGeneratedInLast5YearsOthers";
        public const string AnnualNoOfTourists = "AnnualNoOfTourists";
        public const string FundsProvidedUnderStatePlanInLast5Years = "FundsProvidedUnderStatePlanInLast5Years";
        public const string FundsFromCAMPAandOtherResources = "FundsFromCAMPAandOtherResources";
        public const string FundsProvidedByCSSPT = "FundsProvidedByCSSPT";
        public const string ExGratiaPaidInLast5Years = "ExGratiaPaidInLast5Years";
        public const string CropDamage = "CropDamage";
        public const string AnyOtherImportantFinancialCWWInformationUntilLastYear = "AnyOtherImportantFinancialCWWInformationUntilLastYear";
        #endregion
        #region Manage Installments
        public const string InstallmentId = "InstallmentId";
        public const string FirstInstallment = "FirstInstallment";
        public const string SecondInstallment = "SecondInstallment";
        #endregion

        #region Manage Non-Recurring and Recurring
        public const string CentralNonRecurringRatio = "CentralNonRecurringRatio";
        public const string CentralRecurringRatio = "CentralRecurringRatio";
        public const string StateNonRecurringRatio = "StateNonRecurringRatio";
        public const string StateRecurringRatio = "StateRecurringRatio";
        #endregion

        #region Settled unspent amount
        public const string Unspent = "Unspent";
        public const string IsSettledUnspent = "IsSettledUnspent";
        public const string IsReValidationOrAdjustment = "IsReValidate";
        public const string IsAdjustmentOrSpillOverAdjustment = "IsSpillOverAdjustment";
        #endregion

        #region Monitoring central and state share
        public const string PFYSanctionAmount = "PFYSanctionAmount";
        public const string SanctionedAmount = "SanctionedAmount";
        public const string SpentAmount = "SpentAmount";
        public const string CFYSanctionAmount = "SanctionAmount";
        public const string UnspentAmount = "UnspentAmount";
        public const string UnspentAdjustedAmount = "UnspentAdjustedAmount";
        public const string NonRecurringTotal = "NonRecurringTotal";
        public const string RecurringTotal = "RecurringTotal";
        public const string TotalAmount = "Total";
        public const string CentralShare = "CentralShare";
        public const string Quantity = "Quantity";
        public const string StateShare = "StateShare";
        public const string FirstCentralRelease = "FirstCentralRelease";
        public const string SecondCentralRelease = "SecondCentralRelease";
        public const string FirstStateRelease = "FirstStateRelease";
        public const string SecondStateRelease = "SecondStateRelease";
        public const string PreviousFinancialYear = "PreviousFinancialYear";
        public const string IFDDiaryNumber = "IFDDiaryNumber";
        public const string IFDDate = "IFDDate";
        #endregion
        #region CSSPT Guidelines
        public const string ID = "ID";
        public const string CSSPTParaNumber = "CSSPTParaNumber";
        public const string CSSPTGuideline = "CSSPTGuideline";
        #endregion
    }
}
