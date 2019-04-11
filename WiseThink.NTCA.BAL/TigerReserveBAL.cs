using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;
using System.Data;

namespace WiseThink.NTCA.BAL
{
    public class TigerReserveBAL
    {
        #region Design Pattern
        
        private static TigerReserveBAL instance;
        private static object syncRoot = new Object();
        private TigerReserveBAL() { }

        public static TigerReserveBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new TigerReserveBAL();
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
        /// To create new TigerReserve
        /// </summary>
        /// <param name="tigerReserve"></param>
        /// <returns>TigerReserve object</returns>
        public int AddTigerReserve(TigerReserve tigerReserve)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddTigerReserve, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.STATE_ID,Type=System.Data.DbType.String,value=tigerReserve.State},
                new CommanParameter{Name=DataBaseFields.District,Type=System.Data.DbType.String,value=tigerReserve.District},
                new CommanParameter{Name=DataBaseFields.City,Type=System.Data.DbType.String,value=tigerReserve.City},
                new CommanParameter{Name=DataBaseFields.TigerReserveName,Type=System.Data.DbType.String,value=tigerReserve.TigerReserveName},
                new CommanParameter{Name=DataBaseFields.CoreArea,Type=System.Data.DbType.String,value=tigerReserve.CoreArea},
                new CommanParameter{Name=DataBaseFields.BufferArea,Type=System.Data.DbType.String,value=tigerReserve.BufferArea},
                new CommanParameter{Name=DataBaseFields.TotalArea,Type=System.Data.DbType.String,value=tigerReserve.TotalArea},
                new CommanParameter{Name=DataBaseFields.DateOfRegistration,Type=System.Data.DbType.String,value=tigerReserve.DateOfRegistration},
                new CommanParameter{Name=DataBaseFields.Address,Type=System.Data.DbType.String,value=tigerReserve.Address},
                new CommanParameter{Name=DataBaseFields.PinCode,Type=System.Data.DbType.String,value=tigerReserve.PinCode},
                new CommanParameter{Name=DataBaseFields.FieldDirector,Type=System.Data.DbType.String,value=tigerReserve.FeildDirector},
                new CommanParameter{Name=DataBaseFields.PhoneNumber,Type=System.Data.DbType.String,value=tigerReserve.PhoneNumber},
                new CommanParameter{Name=DataBaseFields.AlternatePhoneNumber,Type=System.Data.DbType.String,value=tigerReserve.AlternatePhoneNumber},
                new CommanParameter{Name=DataBaseFields.MobileNumber,Type=System.Data.DbType.String,value=tigerReserve.MobileNumber},
                new CommanParameter{Name=DataBaseFields.Email,Type=System.Data.DbType.String,value=tigerReserve.Email},
            });

        }

        /// <summary>
        /// Update the TigerReserve
        /// </summary>
        /// <param name="tigerReserve"></param>
        /// <param name="_tigerReserveID"></param>
        /// <returns></returns>
        public int UpdateTigerReserve(TigerReserve tigerReserve, int _tigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditTigerReserve, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=_tigerReserveID},
                new CommanParameter{Name=DataBaseFields.STATE_ID,Type=System.Data.DbType.String,value=tigerReserve.State},
                new CommanParameter{Name=DataBaseFields.District,Type=System.Data.DbType.String,value=tigerReserve.District},
                new CommanParameter{Name=DataBaseFields.City,Type=System.Data.DbType.String,value=tigerReserve.City},
                new CommanParameter{Name=DataBaseFields.TigerReserveName,Type=System.Data.DbType.String,value=tigerReserve.TigerReserveName},
                new CommanParameter{Name=DataBaseFields.CoreArea,Type=System.Data.DbType.String,value=tigerReserve.CoreArea},
                new CommanParameter{Name=DataBaseFields.BufferArea,Type=System.Data.DbType.String,value=tigerReserve.BufferArea},
                new CommanParameter{Name=DataBaseFields.TotalArea,Type=System.Data.DbType.String,value=tigerReserve.TotalArea},
                new CommanParameter{Name=DataBaseFields.DateOfRegistration,Type=System.Data.DbType.String,value=tigerReserve.DateOfRegistration},
                new CommanParameter{Name=DataBaseFields.Address,Type=System.Data.DbType.String,value=tigerReserve.Address},
                new CommanParameter{Name=DataBaseFields.PinCode,Type=System.Data.DbType.String,value=tigerReserve.PinCode},
                new CommanParameter{Name=DataBaseFields.FieldDirector,Type=System.Data.DbType.String,value=tigerReserve.FeildDirector},
                new CommanParameter{Name=DataBaseFields.PhoneNumber,Type=System.Data.DbType.String,value=tigerReserve.PhoneNumber},
                new CommanParameter{Name=DataBaseFields.AlternatePhoneNumber,Type=System.Data.DbType.String,value=tigerReserve.AlternatePhoneNumber},
                new CommanParameter{Name=DataBaseFields.MobileNumber,Type=System.Data.DbType.String,value=tigerReserve.MobileNumber},
                new CommanParameter{Name=DataBaseFields.Email,Type=System.Data.DbType.String,value=tigerReserve.Email},
            });

        }

        /// <summary>
        /// Get the list of TigerReserve
        /// </summary>
        /// <returns></returns>
        public DataSet GetTigerReserveList()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveList, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetTigerReserve(int _tigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserve, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            { new CommanParameter { Name = DataBaseFields.TigerReserveId, Type = System.Data.DbType.String, value = _tigerReserveID }, });
        }
        public DataSet GetStatesAndFeildDirector()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStatesAndFeildDirectors, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetTigerReserveName()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveName, System.Data.CommandType.StoredProcedure);
        }

        public DataSet GetTigerReserveBasedOnStates(string stateId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveBasedOnState, System.Data.CommandType.StoredProcedure,new List<ICommanParameter> 
            {new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.String,value = stateId},
            });
        }
        #endregion
    }
}
