using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using System.Data.SqlClient;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class WebForm8 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection("Data Source=HIMANSHUJAIN-PC\\SQLEXPRESS;Initial Catalog=Practice;Integrated Security=True");
            //SqlCommand cmd = new SqlCommand("Select * from dbo.ManageHead",con);
            //con.Open();
            //cmd.ExecuteReader();
            //con.Close();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //gv.DataSource = dt;
            //gv.DataBind();
        }
    }
}