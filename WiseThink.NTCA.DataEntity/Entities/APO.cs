using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class APO : BaseEntity
    {
        public int rowIndex { get; set; }
        public int AreaId { get; set; }
        public int ActivityTypeId { get; set; }
        public int ActivityId { get; set; }
        public int ActivityItemId { get; set; }
        public string FinancialYear { get; set; }
        public int SubItemId { get; set; }
        public string SubItem { get; set; }
        public string ParaNoCSSPTGuidelines { get; set; }
        public string ParaNoTCP { get; set; }
        public double? NumberOfItems { get; set; }
        public string Specification { get; set; }
        public double? UnitPrice { get; set; }
        public double Total { get; set; }
        public string GPS { get; set; }
        public string Justification { get; set; }
        public string Document { get; set; }
        public int StatusId { get; set; }
        public Nullable<Boolean> IsFilled { get; set; }
        public Nullable<Boolean> IsApproved { get; set; }
        public Nullable<Boolean> IsSanctioned { get; set; }
        public string PhysicalProgress { get; set; }
        public string FinancialAssessment { get; set; }
        public string Location { get; set; }
        public double Unspent { get; set; }
        public Nullable<Boolean> IsSettledUnspent { get; set; }
        public Nullable<Boolean> IsReValidate { get; set; }
        public Nullable<Boolean> IsSpillOverAdjustment { get; set; }
        public string CentralFinancialProgress { get; set; }
        public string StateFinancialProgress { get; set; }
        public string FinancialTotal { get; set; }
        public string Month { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        
    }
}
 