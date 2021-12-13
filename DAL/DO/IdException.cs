using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class IdAlreadyExistException : Exception
    {
        public IdAlreadyExistException(int id, string message) : base($"ERROR: {message} with id - {id} already exsist !") { }
    }

    [Serializable]
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(int id, string message) : base($"ERROR: {message} with id - {id} not found !") { }
    }
}
