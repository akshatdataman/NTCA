using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.DAL;
using System.Data;

namespace WiseThink.NTCA.BAL
{
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
        public int AddActivity(string activityName)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddActivity, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             //new CommanParameter{Name=DataBaseFields.AREA_ID,Type=System.Data.DbType.String,value=activity.AreaId},
             //new CommanParameter{Name=DataBaseFields.ACTIVITY_TYPE_ID,Type=System.Data.DbType.String,value=activity.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ACTIVITY_NAME,Type=System.Data.DbType.String,value=activityName},
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
             new CommanParameter{Name=DataBaseFields.AREA_ID,Type=System.Data.DbType.String,value=activity.AreaId},
             new CommanParameter{Name=DataBaseFields.ACTIVITY_TYPE_ID,Type=System.Data.DbType.String,value=activity.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ACTIVITY_ID,Type=System.Data.DbType.String,value=activity.ActivityId},
             new CommanParameter{Name=DataBaseFields.PARA_NO_CSSPT_GUIDELINES,Type=System.Data.DbType.String,value=activity.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ACTIVITY_ITEM_NAME,Type=System.Data.DbType.String,value=activity.ActivityItemName},
             new CommanParameter{Name=DataBaseFields.GpsStatus,Type=System.Data.DbType.String,value=activity.GpsStatus},
            });
        }

        //Function for adding the Subitem
        public int AddActivitySubItem(Activity activity)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddActivitySubItems, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activity.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.ParaNo,Type=System.Data.DbType.String,value=activity.ParaNo},
             new CommanParameter{Name=DataBaseFields.SubItem,Type=System.Data.DbType.String,value=activity.ActivitySubItem},
             
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
        public DataSet GetManageActivityMasterData()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetActivityMasterData, System.Data.CommandType.StoredProcedure);
        }

        public DataSet GetActivitySubItemList()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetActivitySubItemList, System.Data.CommandType.StoredProcedure);
        }

        public DataSet GetAreaActivityAndType()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAreaActivityData, System.Data.CommandType.StoredProcedure);
        }

        public DataSet GetActivityItem(string activityId, string areaId,string activityTypeId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetActivityItems, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> 
            {   new CommanParameter { Name = "@ActivityId", Type = System.Data.DbType.String, value = activityId },
                new CommanParameter { Name = "@AreaId", Type = System.Data.DbType.String, value = areaId },
                new CommanParameter { Name = "@ActivityTypeId", Type = System.Data.DbType.String, value = activityTypeId },

            });
        }
        public DataSet GetActivityItem(string activityId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetActivityItems, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> 
            {
                new CommanParameter { Name = "@ActivityId", Type = System.Data.DbType.String, value = activityId },
            });
        }

        public DataSet GetActivityItemForAddNewSubItem(string activityId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetActivityItemListForAddNewSubItem, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> 
            {   new CommanParameter { Name = "@ActivityId", Type = System.Data.DbType.String, value = activityId },
               
            });
        }


        public int UpdateActivity(int activityId, string activity)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateActivity, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.ACTIVITY_ID,Type=System.Data.DbType.Int32,value=activityId},
             new CommanParameter{Name=DataBaseFields.ACTIVITY_NAME,Type=System.Data.DbType.String,value=activity},
            });
        }
        public int UpdateActivityItem(int areaId, int activityTypeId, int activityId, string strCSSPTParaNo, int activityItemId,string activityItem,string gps)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateActivityItem, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=areaId},
                new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=activityTypeId},
                new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=activityId},
                new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=strCSSPTParaNo},
                new CommanParameter{Name=DataBaseFields.ACTIVITY_ITEM_ID,Type=System.Data.DbType.Int32,value=activityItemId},
                new CommanParameter{Name=DataBaseFields.ACTIVITY_ITEM_NAME,Type=System.Data.DbType.String,value=activityItem},
                new CommanParameter{Name=DataBaseFields.GpsStatus,Type=System.Data.DbType.String,value=gps},
            });
        }

        public int UpdateActivitySubItem(int ActivityItemId, string ParaNo, string SubItem, int ActivitySubItemId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateActivitySubItems, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
               
                new CommanParameter{Name=DataBaseFields.ACTIVITY_ITEM_ID,Type=System.Data.DbType.Int32,value=ActivityItemId},
                new CommanParameter{Name=DataBaseFields.ParaNo,Type=System.Data.DbType.String,value=ParaNo},
                new CommanParameter{Name=DataBaseFields.SubItem,Type=System.Data.DbType.String,value=SubItem},
                new CommanParameter{Name=DataBaseFields.ActivitySubItemId,Type=System.Data.DbType.Int64,value=ActivitySubItemId},
            });
        }


        #endregion
    }
}
