namespace CQRSandMediatR.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string EmailVerificationToken { get; set; }
        public bool IsEmailVerified { get; set; }

    }
}
