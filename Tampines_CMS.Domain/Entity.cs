using System;

namespace Tampines_CMS.Domain
{
    public class Entity<TIdentity>
    {
        public TIdentity Id { get; set; }
        public Guid GUID { get; set; }
        public string EncryptedId { get; set; }
        public TIdentity CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public TIdentity ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public TIdentity DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string SystemIp { get; set; }
    }
}
