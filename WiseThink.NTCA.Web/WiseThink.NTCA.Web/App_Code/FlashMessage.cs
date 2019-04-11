using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiseThink.NTCA.App_Code
{
    public static class FlashMessage
    {
        public static void InfoMessage(string message)
        {
            HttpContext.Current.Session[Constants.INFO_MESSAGE] = message;
        }

        public static void ErrorMessage(string message)
        {
            HttpContext.Current.Session[Constants.ERROR_MESSAGE] = message;
        }

        public static void WarningMessage(string message)
        {
            HttpContext.Current.Session[Constants.WARNING_MESSAGE] = message;
        }

        /// <summary>
        /// Clear the message
        /// </summary>
        public static void Clear()
        {
            if (HttpContext.Current.Session[Constants.INFO_MESSAGE] != null)
            {
                HttpContext.Current.Session.Remove(Constants.INFO_MESSAGE);
            }

            if (HttpContext.Current.Session[Constants.WARNING_MESSAGE] != null)
            {
                HttpContext.Current.Session.Remove(Constants.WARNING_MESSAGE);
            }

            if (HttpContext.Current.Session[Constants.ERROR_MESSAGE] != null)
            {
                HttpContext.Current.Session.Remove(Constants.ERROR_MESSAGE);
            }
        }
    }
}