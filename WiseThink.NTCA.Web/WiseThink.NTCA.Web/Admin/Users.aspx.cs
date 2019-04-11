using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Admin
{
    public partial class Users : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindGrid();
                    if (Session["UserId"] != null && Convert.ToString(Session["Updatebtn"]) == "1")
                    {
                         string strSuccess = "User has been updated successfully.";
                         vmSuccess.Message = strSuccess;
                         FlashMessage.ErrorMessage(vmSuccess.Message);
                         Session.Remove("Updatebtn");
                    }
                    UserBAL.Instance.InsertAuditTrailDetail("Visited User List Page", "Manage User");
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
        }
        private void BindGrid()
        {
            cgvUsers.DataSource = UserBAL.Instance.GetUserList();
            cgvUsers.DataBind();
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Session["Updatebtn"] = null;
            Response.Redirect("~/Admin/add-User.aspx",false);
        }
        protected void imgbtnAction_click(object sender, EventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
            string userId = cgvUsers.DataKeys[rowIndex].Values[0].ToString();
            int _userId = Convert.ToInt32(userId);
            Session["UserId"] = userId;
            //Response.Redirect("~/Admin/add-User.aspx?UserId="+userId+"");
            Response.Redirect("~/Admin/add-User.aspx");

           
        }
        protected void cgvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvUsers.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void cgvUsers_DataBinding(object sender, EventArgs e)
        {
            
        }
    }
}