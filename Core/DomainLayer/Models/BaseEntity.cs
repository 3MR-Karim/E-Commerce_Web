using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BaseEntity<Tkey>
    {
        public Tkey id { get; set; } 
        // after work can come id will be integer 
        // if can base enitty set constirains 
    }
}
