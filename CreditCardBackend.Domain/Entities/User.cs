using CreditCardBackend.Domain.Interfaces.IGeneric;
using Microsoft.AspNetCore.Identity;

namespace CreditCardBackend.Domain.Entities
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CreditCard> CreditCards { get; set; } = [];
    }
}
