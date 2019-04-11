using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL.Authorization
{
    public class SessionHijacking
    {
         static string SessionName = "encryptedSession";
        public static void ValidateSession()
        {
            string _sessionIPAddress = string.Empty;
            string _encryptedString = Convert.ToString(HttpContext.Current.Session[SessionName]);
            byte[] _encodedAsBytes = System.Convert.FromBase64String(_encryptedString);
            string _decryptedString = System.Text.ASCIIEncoding.ASCII.GetString(_encodedAsBytes);
            string browserInfo = string.Empty;
            char[] _separator = new char[] { '^' };
            if (_decryptedString != string.Empty && _decryptedString != "" && _decryptedString != null)
            {
                string[] _splitStrings = _decryptedString.Split(_separator);
                if (_splitStrings.Count() > 0)
                {

                    if (_splitStrings[2].Count() > 0)
                    {
                        string[] _userBrowserInfo = _splitStrings[2].Split('~');
                        browserInfo = _splitStrings[2];
                        if (_userBrowserInfo.Count() > 0)
                        {
                            _sessionIPAddress = _userBrowserInfo[1];
                        }
                    }
                }
            }
            string VisitorsIPAddr = Utility.GetUser_IP();

            System.Net.IPAddress result;
            if (!System.Net.IPAddress.TryParse(VisitorsIPAddr, out result))
            {
                result = System.Net.IPAddress.None;
            }

            if (_sessionIPAddress != "" && _sessionIPAddress != string.Empty)
            {
                //Same way we can validate browser info also...
                string _currentBrowserInfo = GenerateString();
                if (_currentBrowserInfo != browserInfo)
                {
                    AuthoProvider.LogOut();
                }

            }
        }
        private static string GenerateString()
        {
            return HttpContext.Current.Request.Browser.Browser
                       + HttpContext.Current.Request.Browser.Version
                       + HttpContext.Current.Request.UserAgent + "~"
                       + Utility.GetUser_IP();
        }
        public static void ClearSession()
        {
            HttpContext.Current.Session[SessionName] = null;    
        }
        public static void SetSessionHijachingSession()
        {
            string _browserInfo = GenerateString();

            string _sessionValue = AuthoProvider.User + "^"
                                    + DateTime.Now.Ticks + "^"
                                    + _browserInfo + "^"
                                    + System.Guid.NewGuid();

            byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
            string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
            HttpContext.Current.Session[SessionName] = _encryptedString;
        }
        public static string GetEncryptedString()
        {
            return HttpContext.Current.Session[SessionName].GetEmptyOrString();
        }
        public static void RegenrateSessionId()
        {
            Authorization.AuthoCookie.RegenerateAuthoCookie();
            SessionIDManager manager = new SessionIDManager();
            string oldId = manager.GetSessionID(HttpContext.Current);
            string newId = manager.CreateSessionID(HttpContext.Current);
            bool isAdd = false, isRedir = false;
            manager.SaveSessionID(HttpContext.Current, newId, out isRedir, out isAdd);
            HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
            HttpModuleCollection mods = ctx.Modules;
            System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
            System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            SessionStateStoreProviderBase store = null;
            System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
                if (field.Name.Equals("_rqId")) rqIdField = field;
                if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
                if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
            }
            object lockId = rqLockIdField.GetValue(ssm);
            if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(HttpContext.Current, oldId, lockId);
            rqStateNotFoundField.SetValue(ssm, true);
            rqIdField.SetValue(ssm, newId);
        }
    }
}
