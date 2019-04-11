using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class Obligations:BaseEntity
    {
        public int ObligationId { get; set; }
        public string CompiledOrNot { get; set; }
        public int ComplianceLevel { get; set; }
        public string Reason { get; set; }
    }
}
