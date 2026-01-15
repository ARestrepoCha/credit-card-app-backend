namespace CreditCardBackend.Application.Common.Interfaces.Security
{
    public interface IEncryptionService
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
    }
}
