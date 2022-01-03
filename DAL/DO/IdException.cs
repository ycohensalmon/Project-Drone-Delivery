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

    [Serializable]
    public class EmptyListException : Exception
    {
        public EmptyListException(string message) : base($"ERROR: the list of {message} is empty") { }
    }

    [Serializable]
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base($"ERROR: the {message} was not found") { }
    }
}
