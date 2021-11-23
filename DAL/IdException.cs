using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IdException : Exception
    {
        public int Id;

        public IdException()
        {
        }

        public IdException(string message) : base(message)
        {
        }

        public IdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
