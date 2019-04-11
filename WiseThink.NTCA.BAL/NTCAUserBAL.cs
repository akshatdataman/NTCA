using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;
using System.Data;
using System.Data.SqlClient;

namespace WiseThink.NTCA.BAL
{
    public class NTCAUserBAL
    {
        
        #region Member Variables

        private static readonly NTCAUserBAL instance;

        #endregion Member Variables

        #region Constructors

        static NTCAUserBAL()
        {
            instance = new NTCAUserBAL();
        }

        private NTCAUserBAL() { }

        #endregion Constructors

        #region Properties

        public static NTCAUserBAL Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion Properties
        readonly Random random = new Random();

        #region Methods
        public string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        }
        
        /*Registration Page Metho*/
        public int Registration(User user)
        {
            
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spRegistration, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.USERID,Type=System.Data.DbType.String,value=user.UserId},
             new CommanParameter{Name=DataBaseFields.PASSWORD,Type=System.Data.DbType.String,value=user.Password},
             new CommanParameter{Name=DataBaseFields.TITLE,Type=System.Data.DbType.String,value=user.Title},
             new CommanParameter{Name=DataBaseFields.FIRSTNAME,Type=System.Data.DbType.String,value=user.FirstName},
             new CommanParameter{Name=DataBaseFields.MIDDLENAME,Type=System.Data.DbType.String,value=user.MiddleName},
             new CommanParameter{Name=DataBaseFields.LASTNAME,Type=System.Data.DbType.String,value=user.LastName},
             new CommanParameter{Name="@DateofBirth",Type=System.Data.DbType.DateTime,value=user.DateOfBirth},
             new CommanParameter{Name=DataBaseFields.GENDER,Type=System.Data.DbType.String,value=user.Gender},
             new CommanParameter{Name=DataBaseFields.ROLE,Type=System.Data.DbType.String,value=user.Role},
             new CommanParameter{Name=DataBaseFields.ADDRESS,Type=System.Data.DbType.String,value=user.Address},
             new CommanParameter{Name=DataBaseFields.PINCODE,Type=System.Data.DbType.String,value=user.PinCode},
             new CommanParameter{Name=DataBaseFields.CITY,Type=System.Data.DbType.String,value=user.City},
             new CommanParameter{Name=DataBaseFields.DISTRICT,Type=System.Data.DbType.String,value=user.District},
             new CommanParameter{Name=DataBaseFields.STATE,Type=System.Data.DbType.String,value=user.State},
             new CommanParameter{Name=DataBaseFields.COUNTRY,Type=System.Data.DbType.String,value=user.country},
             new CommanParameter{Name=DataBaseFields.PHONENO,Type=System.Data.DbType.String,value=user.PhoneNo},
             new CommanParameter{Name=DataBaseFields.MOBILENO,Type=System.Data.DbType.String,value=user.MobileNo},
             new CommanParameter{Name=DataBaseFields.EMAIL,Type=System.Data.DbType.String,value=user.Email},
             new CommanParameter{Name=DataBaseFields.FAXNO,Type=System.Data.DbType.String,value=user.FaxNo},
             new CommanParameter{Name=DataBaseFields.SECURITYQUESTION,Type=System.Data.DbType.String,value=user.question},
             new CommanParameter{Name=DataBaseFields.ANSWER,Type=System.Data.DbType.String,value=user.Answer},
            });

        }
        /*Get Record to show Manage User Record*/
        public DataSet GetUserRecord()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetManageUSer, System.Data.CommandType.StoredProcedure);
            
        }
        #endregion 
    }
}
