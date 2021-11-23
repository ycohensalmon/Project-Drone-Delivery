using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class IdNotFoundException : Exception
        {
            public IdNotFoundException()
            {
            }

            public IdNotFoundException(string message) : base(message)
            {
            }

            public IdNotFoundException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected IdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

    }
}
