using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace WiseThink.NTCA.DataEntity
{
    public class AntiXsrfToken
    {
        
        public static string AntiXsrfTokenKey = "__AntiXsrfToken";
        public static string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        //private static string _antiXsrfTokenValue;
        public static string CSRFToken = "__CSRFToken";
      
    }
}
