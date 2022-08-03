using Microsoft.IdentityModel.Tokens;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OnlineStore_PL.Helpers
{
    public class JwtHelper
    {
        public static string GenerateJwt(User user, IEnumerable<string> roles, JwtSettings jwtSettings)
        {
            var claims = GetIdentity(user, roles);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.Lifetime));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Issuer,
                Subject = claims,
                Expires = expires,
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GetIdentity(User user, IEnumerable<string> roles)
        {
            if (user == null)
                throw new NullReferenceException("Cannot generate jwt for null user");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
