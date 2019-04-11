using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL.Reports;

namespace WiseThink.NTCA.UserControls
{
    public partial class FilterReport : System.Web.UI.UserControl
    {
        //ZooDetailBAL zooBAL = new ZooDetailBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (trYear.Visible == true)
                {
                    BindYearOfEstablisment();
                }
                BindState();
                BindCity();
                //BindZooType();
                //BindZooName();

            }
        }
        public bool HideRecognitionDate { set { trRecognition.Visible = value; } }
        public bool HideAssignDate { set { trAssign.Visible = value; } }
        public bool HideEvaluation { set { trEvaluation.Visible = value; } }
        public bool HideValidUpto { set { trValidUpto.Visible = value; } }
        public bool HideYear { set { trYear.Visible = value; } }
        public bool HideApply { set { trApply.Visible = value; } }
        public bool HideFieldSetDiv { set { fieldSet_div.Visible = value; } }


        protected void BindYearOfEstablisment()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Insert(0, "All Year");
            string CurrentYear = DateTime.Now.Year.ToString();
            for (int i = 1950; i <= Convert.ToInt32(CurrentYear); i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        protected void BindState()
        {
            //DataSet dsState = zooBAL.GetAllState();
            DataSet dsState = CommonBAL.Instance.GetAllState();
            if (dsState.Tables[0].Rows.Count > 0)
            {
                mChkState.DataSource = dsState.Tables[0];
                mChkState.DataTextField = dsState.Tables[0].Columns[DataBaseFields.STATE_NAME].ToString();
                mChkState.DataValueField = dsState.Tables[0].Columns[DataBaseFields.STATE_ID].ToString();
                mChkState.DataBind();
            }
            else
            {
                mChkState.DataSource = null;
                mChkState.DataBind();
            }

        }

        protected void BindCity(string StateId = null)
        {
            //DataSet dsCity = zooBAL.GetAllCity(StateId);
            DataSet dsCity = CommonBAL.Instance.GetAllCity(StateId);
            if (!string.IsNullOrEmpty(StateId))
            {
                //City

                BindCity(dsCity.Tables[0]);
                BindCategory(dsCity.Tables[1]);
                BindZooData(dsCity.Tables[2]);
            }
            else
            {
                BindCity(dsCity.Tables[0]);
                BindCategory(dsCity.Tables[1]);
                BindZooData(dsCity.Tables[2]);
                // BindZooName();
            }

        }

        protected void BindZooType(string StateId = null, string CityId = null)
        {
            //DataSet dsRegion = string.IsNullOrEmpty(CityId) ? zooBAL.GetAllCity(StateId) : zooBAL.GetZooType(CityId);
            DataSet dsRegion = string.IsNullOrEmpty(CityId) ? CommonBAL.Instance.GetAllCity(StateId) : CommonBAL.Instance.GetZooType(CityId);
            if (!string.IsNullOrEmpty(CityId))
            {
                BindCategory(dsRegion.Tables[0]);
                BindZooData(dsRegion.Tables[1]);
            }
            else
            {
                BindCity(dsRegion.Tables[0]);
                BindCategory(dsRegion.Tables[1]);
                BindZooData(dsRegion.Tables[2]);
            }
        }


        protected void BindZooName(string state = null, string Type = null, string city = null)
        {
            if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(Type) && string.IsNullOrEmpty(state))
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos();
                BindZooData(ds.Tables[0]);
            }
            else if (string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(Type))
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos(state: state, type: Type);
                BindZooData(ds.Tables[0]);
            }
            else if (!string.IsNullOrEmpty(city) && string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(Type))
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos(type: Type, city: city);
                BindZooData(ds.Tables[0]);
            }
            else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state) && string.IsNullOrEmpty(Type))
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos(state: state, city: city);
                BindZooData(ds.Tables[0]);
            }
            else if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(Type))
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos(type: Type);
                BindZooData(ds.Tables[0]);
            }
            else if (string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state) && string.IsNullOrEmpty(Type))
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos(state: state);
                BindZooData(ds.Tables[0]);
            }
            else if (!string.IsNullOrEmpty(city) && string.IsNullOrEmpty(state) && string.IsNullOrEmpty(Type))
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos(city: city);
                BindZooData(ds.Tables[0]);
            }
            else
            {
                DataSet ds = CommonBAL.Instance.GetAllZoos(state, Type, city);
                BindZooData(ds.Tables[0]);
            }
        }

        public void BindCity(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                mChkCity.DataSource = dt;
                mChkCity.DataTextField = dt.Columns[DataBaseFields.CITY_NAME].ToString();
                mChkCity.DataValueField = dt.Columns[DataBaseFields.CITY_ID].ToString();
                mChkCity.DataBind();
            }
            else
            {
                mChkCity.DataSource = null;
                mChkCity.DataBind();
            }
        }

        public void BindCategory(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                mChkCategory.DataSource = dt;
                mChkCategory.DataTextField = dt.Columns["ZooType"].ToString();
                mChkCategory.DataValueField = dt.Columns["ZooTypeId"].ToString();
                mChkCategory.DataBind();
            }
            else
            {
                mChkCategory.DataSource = null;
                mChkCategory.DataBind();
            }
        }


        public void BindZooData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                mChkZoo.DataSource = dt;
                mChkZoo.DataTextField = dt.Columns["ZooName"].ToString();
                mChkZoo.DataValueField = dt.Columns["ZooId"].ToString();
                mChkZoo.DataBind();
            }
            else
            {
                mChkZoo.DataSource = null;
                mChkZoo.DataBind();
            }
        }

        public string GetFilterEntity()
        {
            string strQuery = "";
            string isAnd = "";
            if (trRecognition.Visible == true)
            {
                if ((txtRecognitionFrom.Text.Length > 8 && txtRecognitionTo.Text.Length > 8))
                {

                    strQuery += "ExtensionDate BETWEEN '" + txtRecognitionFrom.Text.Trim() + "' AND '" + txtRecognitionTo.Text.Trim() + "'";
                    isAnd = " AND ";
                }
            }
            if (trAssign.Visible == true)
            {
                if ((txtAssignFrom.Text.Length > 8 && txtAssignTo.Text.Length > 8))
                {
                    strQuery += isAnd + "AE.AssignDate BETWEEN '" + txtAssignFrom.Text.Trim() + "' AND '" + txtAssignTo.Text.Trim() + "'";
                    isAnd = " AND ";
                }
            }

            if (trEvaluation.Visible == true)
            {
                if ((txtEvaluationFrom.Text.Length > 8 && txtEvaluationTo.Text.Length > 8))
                {
                    strQuery += "AE.DateOfEvaluation BETWEEN '" + txtEvaluationFrom.Text.Trim() + "' AND '" + txtEvaluationTo.Text.Trim() + "'";
                    isAnd = " AND ";
                }
            }

            if (trValidUpto.Visible == true)
            {
                if ((txtValidUpDateStart.Text.Length > 8 && txtValidUpDateEnd.Text.Length > 8))
                {
                    strQuery += isAnd + "ValidUpTo BETWEEN '" + txtValidUpDateStart.Text + "' AND '" + txtValidUpDateEnd.Text + "'";
                    isAnd = " AND ";
                }
            }

            if (trApply.Visible == true)
            {
                if ((txtApplyDateFrom.Text.Length > 8 && txtApplyDateTo.Text.Length > 8))
                {

                    strQuery += isAnd + "CreationDate BETWEEN '" + txtApplyDateFrom.Text.Trim() + "' AND '" + txtApplyDateTo.Text.Trim() + "'";
                    isAnd = " AND ";
                }
            }

            if (trYear.Visible == true)
            {
                if (ddlYear.SelectedIndex > 0)
                {

                    strQuery += isAnd + "Year_of_Establisment= '" + ddlYear.SelectedItem.Text + "'";
                    isAnd = " AND ";
                }
            }

            if (mChkState.SelectedValue != "")
            {
                strQuery += isAnd + "StateId in (" + mChkState.SelectedValueList + ")";
                isAnd = " AND ";
            }


            if (mChkCity.SelectedValue != "")
            {
                strQuery += isAnd + "CityId in (" + mChkCity.SelectedValueList + ")";
                isAnd = " AND ";
            }

            if (mChkCategory.SelectedValue != "")
            {
                strQuery += isAnd + "ZooType in (" + mChkCategory.SelectedTextListForDB + ")";
                isAnd = " AND ";
            }

            if (mChkZoo.SelectedValue != "")
            {
                strQuery += isAnd + "ZooId in (" + mChkZoo.SelectedValueList + ")";
            }

            return strQuery;

        }
        public string GetFilterReport
        {
            get
            {
                return GetFilterEntity();
            }
        }

        protected void mChkState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity(mChkState.SelectedValueList);
        }
        protected void mChkCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindZooType(mChkState.SelectedValueList, mChkCity.SelectedValueList);
        }
        protected void mChkCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindZooName(mChkState.SelectedValueList, mChkCategory.SelectedValueList, mChkCity.SelectedValueList);
        }
    }
}