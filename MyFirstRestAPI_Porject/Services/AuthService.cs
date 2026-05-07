using Microsoft.IdentityModel.Tokens;
using StudentApi.Data;
using StudentApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentApi.Services
{
    public class AuthService : IAuthService
    {
        // Dependency Injection: Injecting the Database Context
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public string Login(LoginRequest request)
        {
            // Step 1: Find the user by email from the REAL database (_context.Users)
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            // If user not found, return null (Controller will handle the 401 response)
            if (user == null)
                return null;

            // Step 2: Verify the provided password against the stored BCrypt hash
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValidPassword)
                return null;

            // Step 3: Create Claims based on the Database User
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role) // Reading the role from DB
            };


            var secretKey = _configuration["Jwt:Key"];

            // Step 4: JWT Signature & Configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Console.WriteLine("key JWT for user:" + key.KeyId);
            //Console.WriteLine("creds JWT for user:" + creds.ToString());

            var token = new JwtSecurityToken(
                issuer: "LmsApi",
                audience: "LmsUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            // Step 5: Return the generated token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}