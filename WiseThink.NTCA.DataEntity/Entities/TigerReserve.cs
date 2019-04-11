using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity.Entities
{
    public class TigerReserve
    {
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string TigerReserveName { get; set; }
        public string CoreArea { get; set; }
        public string BufferArea { get; set; }
        public string TotalArea { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string FeildDirector { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string ImageCapcha { get; set; }
    }
}
