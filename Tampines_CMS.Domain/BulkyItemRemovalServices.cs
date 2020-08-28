using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class BulkyItemRemovalServices : Entity<Int64>
    {
        public string Content { get; set; }

        public Guid UserId { get; set; }

        public ResidentServicesBanner ResidentServicesBanner { get; set; }
    }
}
