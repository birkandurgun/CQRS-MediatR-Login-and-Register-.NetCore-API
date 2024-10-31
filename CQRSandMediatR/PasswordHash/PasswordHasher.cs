using System.Security.Cryptography;
using System.Text;

namespace CQRSandMediatR.PasswordHash
{
    public static class PasswordHasher
    {
        public static string Hash(string password,string salt, string pepper, int iteration)
        {
            if (iteration <= 0) return password;

            using (SHA256 sha256 = SHA256.Create())
            {
                string pepperPasswordSalt = $"{pepper}{password}{salt}";
                
                byte[] byteValue = Encoding.UTF8.GetBytes(pepperPasswordSalt);
                
                byte[] hashValue = sha256.ComputeHash(byteValue);
                
                string hash = Convert.ToBase64String(hashValue);
                return Hash(hash,salt,pepper, iteration-1);
            }
        }

        public static string CreateSalt()
        {
            var saltBytes = new byte[32];

            using (var random = RandomNumberGenerator.Create())
                random.GetBytes(saltBytes);

            string salt = Convert.ToBase64String(saltBytes);

            salt += DateTime.Now.Ticks.ToString();

            return salt;
        }
    }
}
