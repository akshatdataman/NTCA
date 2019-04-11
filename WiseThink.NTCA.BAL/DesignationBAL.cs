using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL
{
    public class DesignationBAL
    {
        #region Design Pattern
        private static DesignationBAL instance;
        private static object syncRoot = new Object();
        private DesignationBAL() { }

        public static DesignationBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new DesignationBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        public int AddDesignation(string designation)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddDesignation, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.Designation,Type=System.Data.DbType.String,value=designation},
            });
        }
        public int UpdateDesignation(int designationid, string designation)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditDesignation, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.DesignationId,Type=System.Data.DbType.String,value=designationid},
                new CommanParameter{Name=DataBaseFields.Designation,Type=System.Data.DbType.String,value=designation},
            });
        }
        public int DeleteDesignation(int designationid)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteDesignation, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.DesignationId,Type=System.Data.DbType.String,value=designationid},
            });
        }
        public DataSet GetDesignation()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDesignation, System.Data.CommandType.StoredProcedure);
        }
         #endregion
    }
}
