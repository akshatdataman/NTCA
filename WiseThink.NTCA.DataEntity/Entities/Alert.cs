using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class Alert :BaseEntity
    {
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SentTo { get; set; }
    }
}
