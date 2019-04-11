using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class Feedback:BaseEntity
    {
        public int FeedbackId { get; set; }
        public int ObligationId { get; set; }
        public string CompiledOrNot { get; set; }
        public string ReasonIfNotCompiled { get; set; }
        public int Score { get; set; }
        public string ComplianceProcess { get; set; }
        public string Remarks { get; set; }
    }
}
