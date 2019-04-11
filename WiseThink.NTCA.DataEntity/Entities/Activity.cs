using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class Activity
    {
        public int AreaId { get; set; }
        public int ActivityTypeId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public int ActivityItemId { get; set; }
        public string ActivityItemName { get; set; }
        public int SubActivityItemId { get; set; }
        public string SubActivityItemName { get; set; }
        public string ParaNoCSSPTGuidelines { get; set; }
        public string NumberOfItems { get; set; }
        public string Specification { get; set; }
        public string UnitPrice { get; set; }
        public string Total { get; set; }
        public string GPS { get; set; }
        public string Justification { get; set; }
        public string Document { get; set; }
        public string ActivitySubItem { get; set; }
        public string ActivitySubItemId { get; set; }
        public string ParaNo { get; set; }
        public string GpsStatus { get; set; }
        public Nullable<Boolean> IsActive { get; set; }
        
    }
}
