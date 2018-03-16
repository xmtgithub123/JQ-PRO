using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Extenstions
{
    public class FilterChilde
    {
        
        private string _ljf = "and";
        /// <summary>
        /// 链接符
        /// </summary>
        public string ljf
        {
            get { return _ljf; }
            set { _ljf = value; }
        }



        private string _filedType = "String";
        /// <summary>
        /// 字段类型
        /// </summary>
        public string filedType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }

        /// <summary>
        /// 过滤的关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 过滤的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 过滤的约束 比如：'<' '<=' '>' '>=' 'like'等  
        /// </summary>
        public string Contract { get; set; }
    }

    public class Filter
    {

        private string _ljf = "and";
        public string ljf
        {
            get { return _ljf; }
            set { _ljf = value; }
        }

        
        public bool isGroup { get; set; }

        public List<FilterChilde> list { get; set; }
    }
}
