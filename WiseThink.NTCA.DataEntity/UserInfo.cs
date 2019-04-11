using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity
{
    public class UserInfo
    {
        public UserInfo()
        {
            this.Roles = new List<Role>();
        }
        public double UserId { get; set; }
        public string UserName { set; get; }
        public List<Role> Roles { get; set; }
        public Role CurrentRole { get; set; }        
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool isFirstLogin { set; get; }
        //public string password { set; get; }

        //added below entities to store IP add in db - 
        public double AuditTrailId { get; set; }
        public string IPAddress { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime? LogOutDateTime { get; set; }
        public string BrowserType { get; set; }
        public string AuditPageVisited { get; set; }
        public string AuditPageReferer { get; set; }
        public string LoginStatus { get; set; }
        public string ActionPerformed { get; set; }
        public string ModuleName { get; set; }
        public DateTime ActionDate { get; set; }
       
    }
}
