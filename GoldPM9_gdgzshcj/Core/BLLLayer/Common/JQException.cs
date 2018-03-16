using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class JQException : Exception
    {
        public JQException()
        {

        }

        public string ErrorCode
        {
            get;
            set;
        }

        public JQException(string message)
            : this(null, message)
        {

        }

        public JQException(string errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public JQException(string message, Exception innerException)
            : this(null, message, innerException)
        {

        }

        public JQException(string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }
    }
}
