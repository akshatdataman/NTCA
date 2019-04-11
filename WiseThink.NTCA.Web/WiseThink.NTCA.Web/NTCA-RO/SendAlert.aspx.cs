using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class SendAlert : BasePage
    {
        int _userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindControls();
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Send Alert Page", "Alerts");
                }
            }
            catch (Exception ex)
            { 
            }
        }
        public void BindControls()
        {
            //ddlName.DataSource = AlertBAL.Instance.GetUserNameList();
            //ddlName.DataValueField = "UserId";
            //ddlName.DataTextField = "FullName";
            //ddlName.DataBind();
            //ddlName.Items.Insert(0, "Select Name");

            mChkName.DataSource = AlertBAL.Instance.GetUserNameList();
            mChkName.DataValueField = "UserId";
            mChkName.DataTextField = "FullName";
            mChkName.DataBind();
            //mChkName.Items.Insert(0, "Select Name");
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendAlerts();
                string strSuccess = "Alert has been sent successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
                Clear();
                UserBAL.Instance.InsertAuditTrailDetail("Sent an Alert", "Alerts");
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Home.aspx", false);
        //}
        private void SendAlerts()
        {
            try
            {
                //int userId = Convert.ToInt32(ddlName.SelectedValue);
                int n = 0;
                string EmailAddress = string.Empty;
                string userName = string.Empty;                
                Alert alert = new Alert();
                string userIds = mChkName.SelectedValueList;
                string userNames = mChkName.SelectedTextListForDB.Replace("'", "");
                DataSet dsEmail = AlertBAL.Instance.GetMultipleEmailAddress(userIds);
                List<string> Emailid = new List<string>();
                for (int i = 0; i < dsEmail.Tables[0].Rows.Count; i++)
                {
                    string[] userId = userIds.Split(',');
                    EmailAddress = Convert.ToString(dsEmail.Tables[0].Rows[i]["Email"]);
                    Emailid.Add(EmailAddress);
                    string[] recipiantName = userNames.Split(',');
                    //userName = Convert.ToString(dsEmail.Tables[0].Rows[i]["UserName"]);

                    alert.UserId = Convert.ToInt32(userId[i]);
                    alert.SentTo = recipiantName[i].ToString();
                    alert.Subject = txtSubject.Text;
                    alert.Body = txtComments.Text;
                    //Remove bellow hard coded APO title once we have APO in the database
                    alert.APOTitle = "APO 1";
                    alert.LoggedInUser = AuthoProvider.User;
                    n = AlertBAL.Instance.SendAlerts(alert);
                }
                if (n != 0)
                {
                    //List<string> Emailid = new List<string>();
                    //Emailid.Add(EmailAddress);
                    string subject = alert.Subject;
                    string body = alert.Body;
                    SendEmailService sendEmail = new SendEmailService();
                    sendEmail.SendEmails(Emailid, subject, body);
                }
            }
            catch(Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private void Clear()
        {
            mChkName.SelectedIndex = -1;
            txtSubject.Text = "";
            txtComments.Text = "";
        }

        protected void mChkName_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}