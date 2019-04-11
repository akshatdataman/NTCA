using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;

namespace WiseThink.NTCA.DataEntity
{
    public interface ICommanParameter
    {
        string Name { get; set; }
        DbType Type { get; set; }
        object value { get; set; }
    }
    public class CommanParameter : ICommanParameter
    {
        object _value;
            public string Name { get; set; }
            public DbType Type { get; set; }
            public object value { 
                get {
                    return _value;
            } 
                set {
                    if (value is string)
                    {
                        _value = WebUtility.HtmlEncode(value.ToString().RemoveScriptAndHtml());
                    }
                    else
                        _value = value;
                } 
            }
    }
    public class CommanParameterDecoded : ICommanParameter
    {
        object _value;
        public string Name { get; set; }
        public DbType Type { get; set; }
        public object value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value is string)
                {
                    _value = value.ToString().RemoveScript();
                }
                else
                    _value = value;
            }
        }

        //    public PType PType { get; set; }


        //public enum PType
        //{
        //    Input,
        //    Out
        //}

    }
}
