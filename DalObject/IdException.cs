using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    [Serializable]
    internal class IdAlreadyExistException : Exception
    {
        public IdAlreadyExistException(int id, string message) : base($"ERROR: {message} with id - {id} already exsist !") { }
    }

    [Serializable]
    internal class IdNotFoundException : Exception
    {
        public IdNotFoundException(int id, string message) : base($"ERROR: {message} with id - {id} not found !") { }
    }
}
