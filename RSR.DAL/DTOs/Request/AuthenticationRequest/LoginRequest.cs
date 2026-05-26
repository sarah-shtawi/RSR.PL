namespace RSR.DAL.DTOs.Request.Authentication
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        // ROLE CHOSEN DURING LOGIN
        public string LoginAs { get; set; }
    }
}
