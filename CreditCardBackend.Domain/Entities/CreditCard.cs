using CreditCardBackend.Domain.Entities.Base;

namespace CreditCardBackend.Domain.Entities
{
    public class CreditCard : BaseEntity
    {
        public string CardHolderName { get; set; } = null!;
        public string EncryptedCardNumber { get; set; } = null!;
        public string LastFourDigits { get; set; } = null!;
        public string ExpirationMonth { get; set; } = null!;
        public string ExpirationYear { get; set; } = null!;
        public string CardType { get; set; } = null!;

        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
