using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class XmlFileCreateException : Exception
    {
        public XmlFileCreateException(string xmlPath) : base($"ERROR: fail to create xml file: {xmlPath}") { }
    }

    public class XmlFileLoadException : Exception
    {
        public XmlFileLoadException(string xmlPath) : base($"ERROR: fail to load xml file: {xmlPath}") { }
    }
}
