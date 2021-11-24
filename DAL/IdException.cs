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

            public IdNotFoundException(int id)
            {
                this.id = id;
            }

            public IdNotFoundException(int id, string message) : base(message)
            {
                this.id = id;
            }

            public override string ToString()
            {
                return $"ERROR: {Message} with id - {id} not found !";
            }
        }

        [Serializable]
        internal class IdNotValidException : Exception
        {
            private int id;
            private int digits;

            public IdNotValidException(int id, int digits)
            {
                this.digits = digits;
                this.id = id;
            }

            public IdNotValidException(int id, int digits, string message) : base(message)
            {
                this.digits = digits;
                this.id = id;
            }

            public override string ToString()
            {
                return $"ERROR: {Message} with ID - {id} is not valid, the ID must be with {digits} digits";
            }
        }

    }
}
