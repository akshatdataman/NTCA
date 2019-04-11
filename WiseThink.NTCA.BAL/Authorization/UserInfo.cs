using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.BAL.Authorization
{
    public class UserInfo
    {
        public UserInfo()
        {
            this.Roles = new List<Role>();
        }
        public string UserName { set; get; }
        public List<Role> Roles { get; set; }
       
    }
}
