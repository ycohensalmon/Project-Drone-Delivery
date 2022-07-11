using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        public string SafePassword { get; set; }
        public string Photo { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Customer Customer { get; set; }
        public override string ToString()
        {
            return $"SafePassword:{SafePassword} photo:{Photo}, customer{Customer}";
        }
    }
}
