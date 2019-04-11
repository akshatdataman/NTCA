using System;
using System.Collections.Generic;
using System.Linq;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.Web;

namespace WiseThink.NTCA
{
    public partial class Login : BasePage
    {
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            //if (Session[AntiXsrfToken.CSRFToken] != null)
            //{
            //    var requestCookie = Request.Cookies[AntiXsrfToken.AntiXsrfTokenKey];
            //    Guid requestCookieGuidValue;
            //    if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            //    {
            //        if (requestCookie.Value == Session[AntiXsrfToken.CSRFToken].ToString())
            //        {
            //            //Stop Responce Here.
            //            Response.Redirect("~/nocookie.htm");
            //        }
            //    }
            //}
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        private void RemovedLoggedUser()
        {
            string userLoggedIn = Session["UserLoggedIn"] == null ? Application["currentUser"] == null ? string.Empty : (string)Application["currentUser"] : (string)Session["UserLoggedIn"];
            if (userLoggedIn.Length > 0)
            {
                System.Collections.Generic.List<string> d = Application["UsersLoggedIn"]
                    as System.Collections.Generic.List<string>;
                if (d != null)
                {
                    lock (d)
                    {
                        d.Remove(userLoggedIn);
                    }
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Application["LoggedInUsers"] = null;
            try
            {
                if (!IsLogedIn(txtUserName.Text.Trim()))
                {
                    return;
                }
                MD5HASH.Encryptdata(txtPassword.Text);

                if (Session["CaptchaImageText"] != null)
                {
                    string captchaString = Session["CaptchaImageText"].ToString();
                    string hiddenPassword=hdnPassword.Value;
                    string mixedPassword = AESEncrytDecry.DecryptStringAES(hiddenPassword);
                    string[] mixedValue = mixedPassword.Split('#');

                    string codeNumber = mixedValue[1];
                    hdnPassword.Value = mixedValue[0];
                    if (!captchaString.Equals(codeNumber))
                    {
                        RemovedLoggedUser();
                        txtCaptcha.Text = "";
                        lblMessageDisplay.Text = "Code entered does not match, please try again !";
                        return;
                    }
                    else
                    { 
                        DataSet ds = AuthoProvider.IsUserLocked(txtUserName.Text); // We are checking whether User is Locked from login or Not. (Zahir)
                        if (ds.IsValid()) // If any unsuccessfull login entry is found then dataset will contain rows for that particular user. (Zahir)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            int idLocked = Convert.ToInt16(dr[DataBaseFields.IsLocked]);
                            double totalSeconds = 0;
                            double seconds = 900;
                            if (idLocked > 0) // If IdLocked is 1 then User is not allowed to Login. (Zahir)
                            {
                                totalSeconds = (DateTime.Now - Convert.ToDateTime(dr[DataBaseFields.lock_time])).TotalSeconds;
                                if (totalSeconds <= seconds)
                                {
                                    RemovedLoggedUser();
                                    lblMessageDisplay.Text = "Your Id is being Locked, Please try after " + Math.Round((seconds - totalSeconds) / 60) + " Mins...!";
                                    return;
                                }
                                else
                                {
                                    // If the lock Time is passed then the entries is deleted for that particular user. (Zahir)

                                    int n = AuthoProvider.DeleteLoginErrorInfo(txtUserName.Text);

                                    checkAuthentication(); // call the function for redirecting user after checking username and password. (Zahir) 
                                }
                            }
                            else
                            {
                                checkAuthentication();
                            }
                        }
                        else
                        {
                            checkAuthentication();
                        }
                    }
                }
                else
                {
                    lblMessageDisplay.Text = "Session has been expired Please refresh page";
                    RemovedLoggedUser();
                }
            }
            catch (Exception ex)
            {
                RemovedLoggedUser();
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private bool IsValidCredential(UserInfo uinfo)
        {
            bool IsValid = false;
            if (uinfo.Password == hdnPassword.Value)
            {
                string clientMD5 = txtPassword.Text.Trim();

                string serverMD5 = MD5HASH.GetMD5HashCode(uinfo.UserName + "#" + uinfo.Password + "#" + Session["CaptchaImageText"].ToString().ToLower());
                if (!string.IsNullOrEmpty(serverMD5))
                {
                    IsValid = serverMD5.Equals(clientMD5);
                    IsValid = true;
                }
                else
                {
                    RemovedLoggedUser();
                    lblMessageDisplay.Text = "Invalid User name and password! Please Try Again";
                }
            }
            if (IsValid == true)
                return true;
            else
                return false;
        }
        protected void checkAuthentication()
        {
            AuthoProvider Autho_prov = new AuthoProvider();
            UserInfo uinfo = new UserInfo();
            try
            {
                
                string InvalidMessage = "Invalid User name and password! Please Try Again";
                //Check whether user is active or not.(Indu)
                DataSet dsActive = AuthoProvider.IsUserInactive(txtUserName.Text);
                if (dsActive.Tables[0].Rows.Count >=0)
                {
                    //uinfo = Autho_prov.AuthenticateUser(txtUserName.Text, MD5HASH.GetMD5HashCode(txtPassword.Text.Trim()), false);
                    uinfo = Autho_prov.AuthenticateUser(txtUserName.Text, hdnPassword.Value, false);
                }
                else
                {
                    RemovedLoggedUser();
                    lblMessageDisplay.Text = "Your account is not active! Please contact to NTCA for activation.";
                    return;
                }
                if (uinfo == null)
                {
                    txtCaptcha.Text = "";
                    txtPassword.Text = "";
                    txtUserName.Text = "";
                    lblMessageDisplay.Text = InvalidMessage;
                }
                    
                /* Here we are checking whether User is loggin in with temporary password and actual password. 
                If isFirstLogin is true then user is using temporary password. 
                So we are redirecting user to change password page. (Zahir) */
                if (uinfo != null)
                {

                    if (!IsValidCredential(uinfo))
                    {
                        return;
                    }

                    else
                    {
                        Application["currentUser"] = (string)Session["UserLoggedIn"];
                        if (uinfo.Roles.Contains(Role.ADMIN) && uinfo.isFirstLogin == false)
                        {
                            UserBAL.Instance.InsertAuditTrailDetail("Login", "Login Module");
                            Session["LoginDateTime"] = DateTime.Now;
                            Response.Redirect("~/Admin/Home.aspx", false);
                        }
                        else if (uinfo.Roles.Contains(Role.NTCA) && uinfo.isFirstLogin == false)
                        {
                            UserBAL.Instance.InsertAuditTrailDetail("Login", "Login Module");
                            Session["LoginDateTime"] = DateTime.Now;
                            Response.Redirect("~/NTCA-RO/Home.aspx", false);
                        }
                        else if (uinfo.Roles.Contains(Role.REGIONALOFFICER) && uinfo.isFirstLogin == false)
                        {
                            UserBAL.Instance.InsertAuditTrailDetail("Login", "Login Module");
                            Session["LoginDateTime"] = DateTime.Now;
                            Response.Redirect("~/NTCA-RO/Home.aspx", false);
                        }
                        else if (uinfo.Roles.Contains(Role.CWLW) && uinfo.isFirstLogin == false)
                        {
                            UserBAL.Instance.InsertAuditTrailDetail("Login", "Login Module");
                            Session["LoginDateTime"] = DateTime.Now;
                            Response.Redirect("~/CWW-Secretary/Home.aspx", false);
                        }
                        else if (uinfo.Roles.Contains(Role.SECRETARY) && uinfo.isFirstLogin == false)
                        {
                            UserBAL.Instance.InsertAuditTrailDetail("Login", "Login Module");
                            Session["LoginDateTime"] = DateTime.Now;
                            Response.Redirect("~/CWW-Secretary/Home.aspx", false);
                        }
                        else if (uinfo.Roles.Contains(Role.FIELDDIRECTOR) && uinfo.isFirstLogin == false)
                        {
                            UserBAL.Instance.InsertAuditTrailDetail("Login", "Login Module");
                            Session["LoginDateTime"] = DateTime.Now;
                            Response.Redirect("FieldDirector/FieldDirectorHome.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("CreatePassword.aspx", false);
                        }
                    }
                    //}
                }
                else
                {
                    RemovedLoggedUser();
                    lblMessageDisplay.Text = "Invalid User name and password! Please Try Again";
                    return;
                }
            }
            catch (Exception ex)
            {
                RemovedLoggedUser();
                Autho_prov.InsertLoginTimeInfo("Login", false, uinfo);
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private bool IsLoggedInOtherBrowserOrSystem(UserInfo uinfo, bool IsFromAlreadyLoggedIn)
        {
            Dictionary<string, DateTime> LoggedInUsers = Application["LoggedInUsers"] as Dictionary<string, DateTime>;
            if (LoggedInUsers != null)
            {
                if (LoggedInUsers.Keys.Contains(uinfo.UserName))
                {
                    //if (LoggedInUsers[uinfo.UserName] < DateTime.Now.AddMinutes(-2))
                    if (LoggedInUsers[uinfo.UserName] < DateTime.Now.AddMilliseconds(-2))
                    {
                        LoggedInUsers.Remove(uinfo.UserName);
                        LoggedInUsers.Add(uinfo.UserName, DateTime.Now);
                    }
                    else
                    {

                        if (!IsFromAlreadyLoggedIn)
                        {
                                //clearText();
                            AuthoProvider.LogOut();
                            lblMessageDisplay.Text = "Same user logged in on other browser or system.";
                            LoggedInUsers.Remove(uinfo.UserName);
                            return true;
                            
                        }
                    }
                }
                else
                {
                    LoggedInUsers.Add(uinfo.UserName, DateTime.Now);
                }
                Application["LoggedInUsers"] = LoggedInUsers;
            }
            else
            {
                LoggedInUsers = new Dictionary<string, DateTime>();
                LoggedInUsers.Add(uinfo.UserName, DateTime.Now);
                Application["LoggedInUsers"] = LoggedInUsers;
            }
            return false;
        }

        protected bool IsLogedIn(string userName)
        {
            System.Collections.Generic.List<string> d = Application["UsersLoggedIn"]
                as System.Collections.Generic.List<string>;
            if (d != null)
            {
                lock (d)
                {
                    if (d.Contains(userName))
                    {
                        // User is already logged in!!!
                        lblMessageDisplay.Text = "Same user is already logged in another browser or system!!!";
                        return false;
                    }
                    d.Add(userName);
                }
            }
            Session["UserLoggedIn"] = userName;
            return true;
        }

    }
}
