﻿namespace Models.Auth
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}