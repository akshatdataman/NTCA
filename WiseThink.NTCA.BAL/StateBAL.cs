using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.DataEntity;
using System.Data;

namespace WiseThink.NTCA.BAL
{
    public class StateBAL
    {
         #region Design Pattern
        
        private static StateBAL instance;
        private static object syncRoot = new Object();
        private StateBAL() { }

        public static StateBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new StateBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods

        /// <summary>
        /// Genarate randam number for captcha image
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        }

        /// <summary>
        /// To create new state
        /// </summary>
        /// <param name="state"></param>
        /// <returns>state object</returns>
        public int AddState(State state)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddState, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.STATE_NAME,Type=System.Data.DbType.String,value=state.StateName},
             new CommanParameter{Name=DataBaseFields.IsNorthEastState,Type=System.Data.DbType.Boolean,value=state.IsNorthEastState},
            });

        }

        /// <summary>
        /// Update the state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public int UpdateState(State state, int _stateID)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditState, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.STATE_ID,Type=System.Data.DbType.String,value=_stateID},
             new CommanParameter{Name=DataBaseFields.STATE_NAME,Type=System.Data.DbType.String,value=state.StateName},
             new CommanParameter{Name=DataBaseFields.IsNorthEastState,Type=System.Data.DbType.Boolean,value=state.IsNorthEastState},
            });

        }

        /// <summary>
        /// Get the list of states
        /// </summary>
        /// <returns></returns>
        public DataSet GetStateList()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateList, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetState(int _stateID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetState, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { new CommanParameter { Name = DataBaseFields.STATE_ID, Type = System.Data.DbType.String, value = _stateID }, });
        }
        #endregion
    }
}
