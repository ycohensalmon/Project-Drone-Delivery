using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        [Serializable]
        internal class IdAlreadyExistException : Exception
        {
            private int id;

            public IdAlreadyExistException(int id, string message) : base(message)
            {
                this.id = id;
            }

            public override string ToString()
            {
                return $"ERROR: {Message} with id - {id} already exsist !";
            }
        }

        [Serializable]
        internal class IdNotFoundException : Exception
        {
            private int id;
            public IdNotFoundException()
            {
            }

            public IdNotFoundException(int id, string message) : base(message)
            {
            }

            public IdNotFoundException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected IdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                return $"ERROR: {Message} with id - {id} not found !";
            }
        }
    }
}
