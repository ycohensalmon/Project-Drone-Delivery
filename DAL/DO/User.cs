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
        public string UserName { get; set; }
        public int SafePassword { get; set; }
        public string Photo { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, UserName:{UserName}, SafePassword:{SafePassword} photo:{Photo}";
        }
    }
}
