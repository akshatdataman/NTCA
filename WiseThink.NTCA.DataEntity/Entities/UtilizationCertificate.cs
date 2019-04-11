using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class UtilizationCertificate : BaseEntity
    {
        public double SanctionAmount { get; set; }
        public double UCAmount { get; set; }
        public double UnSpendAmount { get; set; }
        public double StateShare { get; set; }
        public double CentralShare { get; set; }
        public double FreshRelease { get; set; }
        public double SecondRelease { get; set; }
        public double NRTotal { get; set; }
        public double RTotal { get; set; }
        public double EcoTotal { get; set; }
        public double SecondInstalment { get; set; }
        public string FinancialYear { get; set; }
        public string CentralStateRatio { get; set; }
        public string ProvisionalUC { get; set; }
        public string FinalnalUC { get; set; }

    }
}
