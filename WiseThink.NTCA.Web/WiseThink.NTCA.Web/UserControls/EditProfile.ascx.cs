using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WiseThink.NTCA.Web.UserControls
{
    public partial class EditProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Attributes.Add("readonly", "readonly");
            txtDOB.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            SqlConnection con = new SqlConnection("Data Source=HIMANSHUJAIN-PC\\SQLEXPRESS;Initial Catalog=Practice;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from dbo.UserRegistration where UserId=1 ", con);
            con.Open();
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtUsername.Text = dr["UserName"].ToString();
                ddlTitle.SelectedItem.Text = dr["Title"].ToString();
                txtFirstName.Text = dr["FirstName"].ToString();
                txtMiddleName.Text = dr["MiddleName"].ToString();
                txtLastName.Text =dr["LastName"].ToString();
                string date = DateTime.Parse(dr["DateOfBirth"].ToString()).ToShortDateString();
                txtDOB.Text = date;
                string GenderValue = dr["Gender"].ToString().Trim();
                if (GenderValue.Equals("Male"))
                {

                    radiobtn.Items[0].Selected = true;
                }
                else
                {
                    radiobtn.Items[1].Selected = true;
                }
                ddlRole.SelectedItem.Text = dr["Role"].ToString();
                txtAddress.Text = dr["Address"].ToString();
                ddlCountry.SelectedItem.Text = dr["Country"].ToString();
                ddlState.SelectedItem.Text = dr["State"].ToString();
                txtDistrict.Text = dr["District"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtPinCode.Text = dr["Pincode"].ToString();
                string s = dr["PhoneNumber"].ToString();
                txtPhoneNumber.Text = s;
                txtMobileNumber.Text = dr["MobileNumber"].ToString();
                txtFaxNumber.Text = dr["FaxNumber"].ToString();
                txtEmail.Text = dr["Email"].ToString();
            }
            con.Close();

        }
    }
}