using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct User
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string SafePassword { get; set; }
        public Int32 Phone { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name:{Name}, Phone:{Phone}";
        }
    }
}
