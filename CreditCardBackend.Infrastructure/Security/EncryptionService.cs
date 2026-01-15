using CreditCardBackend.Application.Common.Interfaces.Security;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace CreditCardBackend.Infrastructure.Security
{
    public class EncryptionService(IConfiguration configuration) : IEncryptionService
    {
        private readonly string _key = configuration["Encryption:Key"]!;
        private readonly string _iv = configuration["Encryption:IV"]!;

        public string Encrypt(string text)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(text);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string encryptedText)
        {
            var buffer = Convert.FromBase64String(encryptedText);
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
