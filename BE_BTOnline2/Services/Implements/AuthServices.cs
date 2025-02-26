using BE_BTOnline2.DB;
using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_BTOnline2.Services.Implements
{
    public class AuthServices : IAuthServices
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public AuthResponse Authenticate(AuthRequest request)
        {
            // Xác thực User trong Database
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user == null) return null;

            // Tạo Claim cho Token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? "NoName"),
                new Claim("RoleId", user.RoleId.ToString()) 
            };

            // Lấy thông tin từ appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo Token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            // Trả về Token
            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}
