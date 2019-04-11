using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WiseThink
{
    public static class ConfigManager
    {
        static ConfigManager()
        {
            DateTimeFormat = (ConfigurationManager.AppSettings["datetimeFormat"]);
        }
        public static string DateTimeFormat { get; private set; }      
    }
}