using System;

namespace Tampines_CMS.Domain
{
    public class ForgotPassword : Entity<Int64>
    {
        public Guid UniqueId { get; set; }
        public UserAccount UserAccount { get; set; }
        public bool IsChanged { get; set; }
        public string Key { get; set; }
    }
}
