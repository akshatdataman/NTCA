using System;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace WiseThink.NTCA.DAL
{
    public abstract class BaseDataAccessor
    {
        protected Database _db;

        protected BaseDataAccessor()
            : this("NtcaConnectionString")
        {

        }

        protected BaseDataAccessor(String connectionStringName)
        {
            if (_db == null)
                _db = DatabaseFactory.CreateDatabase(connectionStringName);
        }
    }
}
