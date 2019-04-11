using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DAL;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL.Reports
{
    public class CommonBAL
    {
         #region Design Pattern
        private static CommonBAL instance;
        private static object syncRoot = new Object();
        private CommonBAL() { }

        public static CommonBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new CommonBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        public DataSet GetAllState(string pageType = null)
        {
            string cmd = null;
            if (string.IsNullOrEmpty(pageType))
            {
                cmd = "SELECT * FROM State order by StateName";
            }
            else if (!string.IsNullOrEmpty(pageType) && pageType.Equals("EvaluationDue"))
            {
                cmd = "";
            }
            return DataAccess.Instance.ExecuteDataSet(cmd);
        }
        public DataSet GetAllCity(string StateId = null)
        {
            if (string.IsNullOrEmpty(StateId))
            {
                string cmd = @"select distinct City.CityId,CityName from City order by City.CityName;
                                select distinct ZooTypeId,ZooType.ZooType from ZooType order by ZooType.ZooType;
                                select distinct ZooId,ZooName from ZooDetails order by ZooName;";
                return DataAccess.Instance.ExecuteDataSet(cmd);
            }
            else
            {
                string StateCondition = "City.StateId in (" + StateId.GetEmptyOrString().GetClearString() + ")";
                //return DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetCityTypeZooForState, CommandType.StoredProcedure, new List<CommanParameter> { new CommanParameter { Name = "@StateIds", Type = DbType.String, value = StateCondition } });
                return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetDataForRenewal, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter { Name = "@StateIds", Type = DbType.String, value = StateCondition}, });
            }

        }
        public DataSet GetZooType()
        {
            string cmd = "select ZooTypeId, ZooType from ZooType order by ZooType";
            return DataAccess.Instance.ExecuteDataSet(cmd);
        }

        public DataSet GetZooType(string CityId = null)
        {
            if (string.IsNullOrEmpty(CityId))
            {
                return GetZooType();
            }
            else
            {
                string CityCondition = "ZooDetails.CityId in (" + CityId + ")";
                //return DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetTypeZooForCitys, CommandType.StoredProcedure, new List<CommanParameter> { new CommanParameter { Name = "@Citys", Type = DbType.String, value = CityCondition } });
                return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetTypeZooForCitys, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter { Name = "@Citys", Type = DbType.String, value = CityCondition }, });
            }
        }
        public DataSet GetAllZoos()
        {
            string cmd = @"select ZooId,ZooName from ZooDetails order by ZooName";
            return DataAccess.Instance.ExecuteDataSet(cmd);
        }

        public DataSet GetAllZoos(string state = null, string type = null, string city = null)
        {
            if (string.IsNullOrEmpty(state) && string.IsNullOrEmpty(type) && string.IsNullOrEmpty(city))
            {
                return GetAllZoos();
            }
            else
            {
                List<CommanParameter> parameterList = new List<CommanParameter>();
                string cmd = null;
                if (string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(city))
                {
                    cmd = @"SELECT ZooId,ZooName FROM ZooView WHERE ZooView.CityId IN(" + city.GetClearString() + ") AND ZooView.ZooTypeId IN(" + type.GetClearString() + ") ORDER BY ZooName";
                }
                else if (!string.IsNullOrEmpty(state) && string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(city))
                {
                    cmd = @"SELECT ZooId,ZooName FROM ZooView WHERE ZooView.StateId in(" + state.GetClearString() + ") AND ZooView.CityId in(" + city.GetClearString() + ") ORDER BY ZooName";
                }
                else if (!string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(city))
                {
                    cmd = @"SELECT ZooId,ZooName FROM ZooView WHERE ZooView.StateId IN(" + state.GetClearString() + ") AND ZooView.ZooTypeId in(" + type.GetClearString() + ") ORDER BY ZooName";
                }
                else if (string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(city))
                {
                    cmd = @"SELECT ZooId,ZooName FROM ZooView WHERE ZooView.ZooTypeId IN(" + type.GetClearString() + ") ORDER BY ZooName";
                }
                else if (string.IsNullOrEmpty(state) && string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(city))
                {
                    cmd = @"SELECT ZooId,ZooName FROM ZooView WHERE ZooView.CityId IN(" + city.GetClearString() + ") ORDER BY ZooName";
                }
                else if (!string.IsNullOrEmpty(state) && string.IsNullOrEmpty(type) && string.IsNullOrEmpty(city))
                {
                    cmd = @"SELECT ZooId,ZooName FROM ZooView WHERE ZooView.StateId IN(" + state.GetClearString() + ") ORDER BY ZooName";
                }
                else
                {
                    cmd = @"SELECT ZooId,ZooName FROM ZooView WHERE ZooView.StateId IN(" + state.GetClearString() + ") AND ZooView.CityId IN(" + city.GetClearString() + ") AND ZooView.ZooTypeId IN(" + type.GetClearString() + ") ORDER BY ZooName";
                }
                return DataAccess.Instance.ExecuteDataSet(cmd);
            }


        }
        #endregion
    }
}
