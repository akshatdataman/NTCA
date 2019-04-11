using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.DAL
{
    public class DataAccess : BaseDataAccessor
    {
        private static DataAccess instance;
        private static object syncRoot = new Object();
        private DataAccess() { }

        public static DataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new DataAccess();
                    }
                }
                return instance;
            }
        }
        #region ExecuteDataSet
        
        public DataSet ExecuteDataSet(string commandText, CommandType type= CommandType.Text, List<ICommanParameter> parameters=null)
        {
            DbCommand cmd = null;
            cmd = _db.GetSqlStringCommand(commandText);
            cmd.CommandType = type;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    //if (p.PType == PType.Input)
                    //{
                    _db.AddInParameter(cmd, p.Name, p.Type, p.value);
                    //}
                    //else
                    //{
                    //    _db.AddOutParameter(cmd, p.Name, p.Type, 50);
                    //}
                }
            }
            return _db.ExecuteDataSet(cmd);
        }
       
        #endregion ExecuteDataSet

        #region ExecuteNonQuery

        public int ExecuteNonQuery(string commandText, CommandType type = CommandType.Text, List<ICommanParameter> parameters = null)
        {
            DbCommand cmd = null;
            cmd = _db.GetSqlStringCommand(commandText);
            cmd.CommandType = type;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    _db.AddInParameter(cmd, p.Name, p.Type, p.value);
                }
                return _db.ExecuteNonQuery(cmd);
            }
            else
            {
                return _db.ExecuteNonQuery(cmd);
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteScalar

        public object ExecuteScalar(string commandText, CommandType type= CommandType.Text, List<ICommanParameter> parameters=null)
        {
            DbCommand cmd = null;
            cmd = _db.GetSqlStringCommand(commandText);
            cmd.CommandType = type;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    _db.AddInParameter(cmd, p.Name, p.Type, p.value);
                }
            }
            return _db.ExecuteScalar(cmd);
        }
       
        #endregion ExecuteScalar
    }

}
