using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class Role : Entity<Int64>
    {
        public string Name { get; set; }
    }
}
