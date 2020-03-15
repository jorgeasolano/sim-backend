using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
namespace WebAPI.JWT
{
    public class JWTHelper
    {
        private const string mySecret = "GeorgeSun2020*BlaBla#BlaBla@2020";
        public string GenerateToken(string usuarioId, Boolean EsAdmin)
        {
            var authClaims = new[]
             {

                    new Claim(JwtRegisteredClaimNames.Sub, usuarioId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   // new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("Usuario", usuarioId),
                    new Claim("EsAdmin", EsAdmin.ToString())
                };

            var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(mySecret));


            var token = new JwtSecurityToken(
                issuer: "http://dotnetdetail.net",
                audience: "http://dotnetdetail.net",
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );


            return new JwtSecurityTokenHandler().WriteToken(token);



            // var mySecurityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(mySecret));

            // var myIssuer = "http://shipping-instructions.com";
            // var myAudience = "http://myaudience.com";

            // var tokenHandler = new JwtSecurityTokenHandler();
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(new Claim[]
            //     {
            //         new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
            //         new Claim(ClaimTypes.Role, ""),
            //         new Claim("EsAdmin", EsAdmin.ToString()),
            //         new Claim("Usuario", usuarioId)
            //     }),
            //     Expires = DateTime.UtcNow.AddDays(7),
            //     Issuer = myIssuer,
            //     Audience = myAudience,
            //     SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            // };

            // var token = tokenHandler.CreateToken(tokenDescriptor);
            // return tokenHandler.WriteToken(token);
        }



        public bool ValidateCurrentToken(string token)
        {

            var mySecurityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://mysite.com";
            var myAudience = "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

    }
}