using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity;
using System.Data;

namespace WiseThink.NTCA.BAL
{
    public class ManageGuidelineBAL
    {
        #region Design Pattern
        private static ManageGuidelineBAL instance;
        private static object syncRoot = new Object();
        private ManageGuidelineBAL() { }

        public static ManageGuidelineBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new ManageGuidelineBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        public int AddGuideline(string cssPTParaNumber, string cssPTGuideline)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddGuideline, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.CSSPTParaNumber,Type=System.Data.DbType.String,value=cssPTParaNumber},
                new CommanParameter{Name=DataBaseFields.CSSPTGuideline,Type=System.Data.DbType.String,value=cssPTGuideline},
            });
        }
        public int UpdateGuideline(int id, string cssPTParaNumber, string cssPTGuideline)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditCSSPTGuideline, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.ID,Type=System.Data.DbType.Int32,value=id},
                new CommanParameter{Name=DataBaseFields.CSSPTParaNumber,Type=System.Data.DbType.String,value=cssPTParaNumber},
                new CommanParameter{Name=DataBaseFields.CSSPTGuideline,Type=System.Data.DbType.String,value=cssPTGuideline},
            });
        }
        public int DeleteGuideline(int id)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteGuideline, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.ID,Type=System.Data.DbType.Int32,value=id},
            });
        }
        public DataSet GetGuidelineList()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCSSPTGuidelines, System.Data.CommandType.StoredProcedure);
        }
         #endregion
    }
}
