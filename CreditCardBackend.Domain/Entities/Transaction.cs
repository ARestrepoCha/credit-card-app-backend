using CreditCardBackend.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardBackend.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string ProductDescription { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
        public string Status { get; set; } = "Aprobado";

        public Guid CreditCardId { get; set; }
        public virtual CreditCard CreditCard { get; set; } = null!;
    }
}
