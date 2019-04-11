using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DAL
{
    public class NTCAUserDAL
    {
        #region Member Variables

        private static readonly NTCAUserDAL instance;

        #endregion Member Variables

        #region Constructors

        static NTCAUserDAL()
        {
            instance = new NTCAUserDAL();
        }

        private NTCAUserDAL() { }

        #endregion Constructors

        #region Properties

        public static NTCAUserDAL Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion Properties

        #region Methods

        #endregion
    }
}
