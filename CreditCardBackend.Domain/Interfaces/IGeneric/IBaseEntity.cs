using System.ComponentModel.DataAnnotations;

namespace CreditCardBackend.Domain.Interfaces.IGeneric
{
    public interface IBaseEntity
    {
        [Key]
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? LastModifiedOn { get; set; }
        Guid? CreatedBy { get; set; }
        Guid? LastModifiedBy { get; set; }
        bool IsActive { get; set; }
    }
}
