using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity;
using System.Data;

namespace WiseThink.NTCA.BAL
{
    public class DocumentBAL
    {
        #region Design Pattern

        private static DocumentBAL instance;
        private static object syncRoot = new Object();
        private DocumentBAL() { }

        public static DocumentBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new DocumentBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
        #region Method
        public int UploadDocument(string documentFile, string loggedInUser)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddEditDocument, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.DocumentName,Type=System.Data.DbType.String,value=documentFile},
             new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=loggedInUser},
            });
        }
        public DataSet GetUploadedDocuments()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUploadedDocuments, System.Data.CommandType.StoredProcedure);
        }
        #endregion 
    }
}
