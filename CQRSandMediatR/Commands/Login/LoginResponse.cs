namespace CQRSandMediatR.Commands.Login
{
    public class LoginResponse
    {
        public bool AuthenticationResult { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpireDate { get; set; }
    }
}
