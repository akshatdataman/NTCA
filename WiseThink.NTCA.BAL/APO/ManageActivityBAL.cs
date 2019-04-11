using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DAL;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.BAL.APO
{
    /// <summary>
    /// Author: Indu
    /// Date: 02 Jan 2015
    /// Purpose: Manage activity master
    /// </summary>
    public class ManageActivityBAL
    {
        #region Design Pattern
        private static ManageActivityBAL instance;
        private static object syncRoot = new Object();
        private ManageActivityBAL() { }

        public static ManageActivityBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new ManageActivityBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public DataSet GetAllCategory(string pageType = null)
        {
            string cmd = null;
            if (string.IsNullOrEmpty(pageType))
            {
                cmd = "SELECT * FROM dbo.Category";
            }
            else if (!string.IsNullOrEmpty(pageType))
            {
                cmd = "";
            }
            return DataAccess.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// Get all activities
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public DataSet GetAllActivity(string CategoryId = null)
        {
            string cmd = null;
            if (string.IsNullOrEmpty(CategoryId))
            {
                cmd = "SELECT * FROM dbo.Activities";
            }
            else if (!string.IsNullOrEmpty(CategoryId))
            {
                cmd = "";
            }
            return DataAccess.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// Get Activity by Category
        /// </summary>
        /// <param name="CategoryIds"></param>
        /// <returns></returns>
        public DataSet GetActivity(string CategoryIds)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetActivities, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter { Name = DataBaseFields.CATEGORY_IDs, Type = DbType.String, value = CategoryIds}, });
        }
        /// <summary>
        /// Get Activity Items by Activity
        /// </summary>
        /// <param name="ActivityIDs"></param>
        /// <returns></returns>
        public DataSet GetActivityItems(string ActivityIDs)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetActivittItems, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter { Name = DataBaseFields.ACTIVITY_IDs, Type = DbType.String, value = ActivityIDs}, });
        }
        /// <summary>
        /// Get Sub-Activity Items by Activity Items
        /// </summary>
        /// <param name="ActivityItemIds"></param>
        /// <returns></returns>
        public DataSet GetSubActivityItems(string ActivityItemIds)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetSubActivittItems, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter { Name = DataBaseFields.ACTIVITY_ITEM_IDs, Type = DbType.String, value = ActivityItemIds}, });
        }
        /// <summary>
        /// Get All Activity Items
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public DataSet GetAllActivityItem(string pageType = null)
        {
            string cmd = null;
            if (string.IsNullOrEmpty(pageType))
            {
                cmd = "SELECT * FROM dbo.ActivityItems";
            }
            else if (!string.IsNullOrEmpty(pageType))
            {
                cmd = "";
            }
            return DataAccess.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// Get All Sub-Activity Items
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public DataSet GetAllSubActivityItem(string pageType = null)
        {
            string cmd = null;
            if (string.IsNullOrEmpty(pageType))
            {
                cmd = "SELECT * FROM dbo.SubActivityItems";
            }
            else if (!string.IsNullOrEmpty(pageType))
            {
                cmd = "";
            }
            return DataAccess.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// To add new category
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public int AddCategory(Activity activity)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddCategory, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.CATEGORY_NAME,Type=System.Data.DbType.String,value=activity.CategoryName},
            });
        }
        /// <summary>
        /// To add new activity
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public int AddActivity(Activity activity)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddActivity, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.CATEGORY_ID,Type=System.Data.DbType.String,value=activity.CategoryId},
             new CommanParameter{Name=DataBaseFields.ACTIVITY_NAME,Type=System.Data.DbType.String,value=activity.ActivityName},
            });
        }
        /// <summary>
        /// To add new activity items
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public int AddActivityItem(Activity activity)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddActivityItem, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.ACTIVITY_ID,Type=System.Data.DbType.String,value=activity.ActivityID},
             new CommanParameter{Name=DataBaseFields.ACTIVITY_ITEM_NAME,Type=System.Data.DbType.String,value=activity.ActivityItemName},
            });
        }
        /// <summary>
        /// To add new sub-activity items
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public int AddSubActivityItem(Activity activity)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddSubActivityItem, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.ACTIVITY_ITEM_ID,Type=System.Data.DbType.String,value=activity.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.SUB_ACTIVITY_ITEM_NAME,Type=System.Data.DbType.String,value=activity.SubActivityItemName},
            });
        }
        #endregion
    }
}
