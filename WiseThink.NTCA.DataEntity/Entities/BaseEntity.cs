using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    [Serializable]
    public class BaseEntity
    {
        public int APOId { get; set; }
        public string APOFileNo { get; set; }
        public string APOTitle { get; set; }
        public int StateId { get; set; }
        public int TigerReserveId { get; set; }
        public int QuarterId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string EmailId { get; set; }
        public string LoggedInUser { get; set; }
        public bool IsNorthEastState { get; set; }
        public string PreviousFinancialYear { get; set; }
        public string convertQuotes(string str)
        {
            return str.Replace("'", "''");
        }
    }
}
