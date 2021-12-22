using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        public Int32 Id { get; set; }
        public string UserName { get; set; }
        public string SafePassword { get; set; }
        public string Photo { get; set; }
        public Customer customer { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, UserName:{UserName}, SafePassword:{SafePassword} photo:{Photo}, customer{customer}";
        }
    }
}
