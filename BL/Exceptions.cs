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
        [Serializable]
        public class NegetiveValueException : Exception
        {
            public NegetiveValueException(string item)
                : base($"ERROR: {item} must be positive.") { }
            public NegetiveValueException(string item, int digits)
                : base($"ERROR: {item} must be positive, The {item} must be content {digits} digits.") { }
            public NegetiveValueException(string item, int digits, double v)
                : base($"ERROR: {item} must be positive, The {item} must be content at least {digits} digits (example:{v})") { }
        }

        [Serializable]
        public class OnlyDigitsException : Exception
        {
            public OnlyDigitsException(string message) : base($"ERROR: {message} must be only with digits.") { }
        }

        [Serializable]
        public class DalException : Exception
        {
            public DalException(Exception item) { }
        }

        public class ItemNotFoundException : Exception
        {
            public ItemNotFoundException(int item, string message) : base($"ERROR: {message} with id - {item} not exsist") { }
        }

    }
}
