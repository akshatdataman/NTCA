using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.BAL
{
    public class TigerReserveDetailsBAL
    {
         #region Design Pattern
        
        private static TigerReserveDetailsBAL instance;
        private static object syncRoot = new Object();
        private TigerReserveDetailsBAL() { }

        public static TigerReserveDetailsBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new TigerReserveDetailsBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        /// <summary>
        /// GetTigerReserveMasterDetail
        /// </summary>
        /// <param name="_tigerReserveID"></param>
        /// <returns></returns>
        public DataSet GetTigerReserveMasterDetail(int _tigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveMasterData, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { new CommanParameter { Name = DataBaseFields.TigerReserveId, Type = System.Data.DbType.String, value = _tigerReserveID }, });
        }
        public DataSet GetTigerReserveDetail(int _tigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveDetails, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { new CommanParameter { Name = DataBaseFields.TigerReserveId, Type = System.Data.DbType.String, value = _tigerReserveID }, });
        }

        /// <summary>
        /// Update the Tiger Reserve Details
        /// </summary>
        /// <param name="tigerReserveDetail"></param>
        /// <param name="_tigerReserveID"></param>
        /// <returns></returns>
        public int UpdateTigerReserveDetails(TigerReserveDetail tigerReserveDetail, int _tigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditTigerReserveDetails, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=_tigerReserveID},
                new CommanParameter{Name=DataBaseFields.LegalStatus,Type=System.Data.DbType.String,value=tigerReserveDetail.LegalStatus},
                new CommanParameter{Name=DataBaseFields.CoreAreaVillageNumber,Type=System.Data.DbType.Int32,value=tigerReserveDetail.CoreAreaVillageNumber},
                new CommanParameter{Name=DataBaseFields.SettlementStatus,Type=System.Data.DbType.String,value=tigerReserveDetail.SettlementStatus},
                new CommanParameter{Name=DataBaseFields.TigerReserveName,Type=System.Data.DbType.String,value=tigerReserveDetail.TigerReserveName},
                new CommanParameter{Name=DataBaseFields.CoreArea,Type=System.Data.DbType.String,value=tigerReserveDetail.CoreArea},
                new CommanParameter{Name=DataBaseFields.BufferArea,Type=System.Data.DbType.String,value=tigerReserveDetail.BufferArea},
                new CommanParameter{Name=DataBaseFields.TotalArea,Type=System.Data.DbType.String,value=tigerReserveDetail.TotalArea},
                //new CommanParameter{Name=DataBaseFields.DateOfRegistration,Type=System.Data.DbType.String,value=tigerReserveDetail.DateOfRegistration},
                new CommanParameter{Name=DataBaseFields.Address,Type=System.Data.DbType.String,value=tigerReserveDetail.Address},
                new CommanParameter{Name=DataBaseFields.TigerConservationPlan,Type=System.Data.DbType.String,value=tigerReserveDetail.TigerConservationPlan},
                new CommanParameter{Name=DataBaseFields.NameofPost,Type=System.Data.DbType.String,value=tigerReserveDetail.NameofPost},
                new CommanParameter{Name=DataBaseFields.SanctionedStrength,Type=System.Data.DbType.String,value=tigerReserveDetail.SanctionedStrength},
                new CommanParameter{Name=DataBaseFields.StaffInPosition,Type=System.Data.DbType.Int32,value=tigerReserveDetail.StaffInPosition},
                new CommanParameter{Name=DataBaseFields.Vacant,Type=System.Data.DbType.Int32,value=tigerReserveDetail.Vacant},
                new CommanParameter{Name=DataBaseFields.WildlifeTraining,Type=System.Data.DbType.String,value=tigerReserveDetail.WildlifeTraining},
                new CommanParameter{Name=DataBaseFields.WildlifeTrainedStaff,Type=System.Data.DbType.String,value=tigerReserveDetail.WildlifeTrainedStaff},
                new CommanParameter{Name=DataBaseFields.CasualothersStaff,Type=System.Data.DbType.String,value=tigerReserveDetail.CasualothersStaff},
                new CommanParameter{Name=DataBaseFields.TypeOfWeaponsNumber,Type=System.Data.DbType.String,value=tigerReserveDetail.TypeOfWeaponsNumber},
                new CommanParameter{Name=DataBaseFields.ShootingOfFilmsDocumentaries,Type=System.Data.DbType.String,value=tigerReserveDetail.ShootingOfFilmsDocumentaries},
                new CommanParameter{Name=DataBaseFields.VehicleType,Type=System.Data.DbType.String,value=tigerReserveDetail.VehicleType},
                new CommanParameter{Name=DataBaseFields.Wireless,Type=System.Data.DbType.String,value=tigerReserveDetail.Wireless},
                new CommanParameter{Name=DataBaseFields.BarriersDetails,Type=System.Data.DbType.String,value=tigerReserveDetail.BarriersDetails},
                new CommanParameter{Name=DataBaseFields.NumberOfBarriers,Type=System.Data.DbType.Int32,value=tigerReserveDetail.NumberOfBarriers},
                new CommanParameter{Name=DataBaseFields.DivisionArea,Type=System.Data.DbType.String,value=tigerReserveDetail.DivisionArea},
                new CommanParameter{Name=DataBaseFields.SubDivisionArea,Type=System.Data.DbType.String,value=tigerReserveDetail.SubDivisionArea},
                new CommanParameter{Name=DataBaseFields.Ranges,Type=System.Data.DbType.String,value=tigerReserveDetail.Ranges},
                new CommanParameter{Name=DataBaseFields.Beats,Type=System.Data.DbType.String,value=tigerReserveDetail.Beats},
                new CommanParameter{Name=DataBaseFields.Sections,Type=System.Data.DbType.String,value=tigerReserveDetail.Sections},
                new CommanParameter{Name=DataBaseFields.AntiPoachingCampDetails,Type=System.Data.DbType.String,value=tigerReserveDetail.AntiPoachingCampDetails},
                new CommanParameter{Name=DataBaseFields.WatchTower,Type=System.Data.DbType.String,value=tigerReserveDetail.WatchTower},
                new CommanParameter{Name=DataBaseFields.NameAndTenureOfIncumbents,Type=System.Data.DbType.String,value=tigerReserveDetail.NameAndTenureOfIncumbents},
                new CommanParameter{Name=DataBaseFields.CaptiveElephants,Type=System.Data.DbType.String,value=tigerReserveDetail.CaptiveElephants},
                new CommanParameter{Name=DataBaseFields.SpecialTigerProtectionForce,Type=System.Data.DbType.String,value=tigerReserveDetail.SpecialTigerProtectionForce},
                new CommanParameter{Name=DataBaseFields.TigerConservationFoundation,Type=System.Data.DbType.String,value=tigerReserveDetail.TigerConservationFoundation},
                new CommanParameter{Name=DataBaseFields.WildlifeOtherInformation,Type=System.Data.DbType.String,value=tigerReserveDetail.WildlifeOtherInformation},
                new CommanParameter{Name=DataBaseFields.EstimationReportForLast3Years,Type=System.Data.DbType.String,value=tigerReserveDetail.EstimationReportForLast3Years},
                new CommanParameter{Name=DataBaseFields.ImportantSpeciesAnimalsFoundInTR,Type=System.Data.DbType.String,value=tigerReserveDetail.ImportantSpeciesAnimalsFoundInTR},
                new CommanParameter{Name=DataBaseFields.WildlifePopulationEstimates,Type=System.Data.DbType.String,value=tigerReserveDetail.WildlifePopulationEstimates},
                new CommanParameter{Name=DataBaseFields.DeathOfAnimals,Type=System.Data.DbType.String,value=tigerReserveDetail.DeathOfAnimals},
                new CommanParameter{Name=DataBaseFields.Firelines,Type=System.Data.DbType.String,value=tigerReserveDetail.Firelines},
                new CommanParameter{Name=DataBaseFields.ForestType,Type=System.Data.DbType.String,value=tigerReserveDetail.ForestType},
                new CommanParameter{Name=DataBaseFields.AnyotherImportantWildlifeInformationUntilLastYear,Type=System.Data.DbType.String,value=tigerReserveDetail.AnyotherImportantWildlifeInformationUntilLastYear},
                new CommanParameter{Name=DataBaseFields.RevenueGeneratedInLast5YearsTourism,Type=System.Data.DbType.String,value=tigerReserveDetail.RevenueGeneratedInLast5YearsTourism},
                new CommanParameter{Name=DataBaseFields.RevenueGeneratedInLast5YearsOthers,Type=System.Data.DbType.String,value=tigerReserveDetail.RevenueGeneratedInLast5YearsOthers},
                new CommanParameter{Name=DataBaseFields.AnnualNoOfTourists,Type=System.Data.DbType.Int32,value=tigerReserveDetail.AnnualNoOfTourists},
                new CommanParameter{Name=DataBaseFields.FundsProvidedUnderStatePlanInLast5Years,Type=System.Data.DbType.String,value=tigerReserveDetail.FundsProvidedUnderStatePlanInLast5Years},
                new CommanParameter{Name=DataBaseFields.FundsFromCAMPAandOtherResources,Type=System.Data.DbType.String,value=tigerReserveDetail.FundsFromCAMPAandOtherResources},
                new CommanParameter{Name=DataBaseFields.FundsProvidedByCSSPT,Type=System.Data.DbType.String,value=tigerReserveDetail.FundsProvidedByCSSPT},
                new CommanParameter{Name=DataBaseFields.ExGratiaPaidInLast5Years,Type=System.Data.DbType.String,value=tigerReserveDetail.ExGratiaPaidInLast5Years},
                new CommanParameter{Name=DataBaseFields.CropDamage,Type=System.Data.DbType.String,value=tigerReserveDetail.CropDamage},
                new CommanParameter{Name=DataBaseFields.AnyOtherImportantFinancialCWWInformationUntilLastYear,Type=System.Data.DbType.String,value=tigerReserveDetail.AnyOtherImportantFinancialCWWInformationUntilLastYear},
            });

        }

        
        #endregion
    }
}
