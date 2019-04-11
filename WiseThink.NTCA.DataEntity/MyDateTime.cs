using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink
{
    /// <summary>
    /// Summary description for MyDateTime
    /// </summary>
    public class MyDateTime
    {
        private MyDateTime()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DateTime Now
        {
            get
            {
                TimeZoneInfo Time_Zone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, Time_Zone);
            }
        }
    }
}
