using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class CentralStateShare : BaseEntity
    {
        public double PFYSanctionAmount { get; set; }
        public double SpentAmount { get; set; }
        public double CFYSanctionAmount { get; set; }
        public double Quantity { get; set; }
        public double UnspentAmount { get; set; }
        public double UnspentAdjustedAmount { get; set; }
        public double NonRecurringTotal { get; set; }
        public double RecurringTotal { get; set; }
        public double TotalAmount { get; set; }
        public double CentralShare { get; set; }
        public double StateShare { get; set; }
        public double FirstCentralRelease { get; set; }
        public double SecondCentralRelease { get; set; }
        public double FirstStateRelease { get; set; }
        public double SecondStateRelease { get; set; }
        public string CurrentFinancialYear { get; set; }
        public string IFDDiaryNumber { get; set; }
        public Nullable<DateTime> IFDDate { get; set; }
    }
}
