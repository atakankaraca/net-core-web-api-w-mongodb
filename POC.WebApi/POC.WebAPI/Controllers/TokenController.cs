using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POC.WebAPI.Models;
using POC.WebAPI.Repository;

namespace POC.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly IRepository<Account> _accountRepository;

        public TokenController(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("generate")]
        public IActionResult Get([FromBody] Account account)
        {
            if (IsLoginCredentialsValid(account))
            {
                return new ObjectResult(GenerateToken(account));
            }

            return Unauthorized();
        }

        private static string GenerateToken(Account account)
        {
            var someClaims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.UniqueName,account.Username),
                new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var token = new JwtSecurityToken(
                issuer: "self",
                audience: "http://localhost",
                claims: someClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsLoginCredentialsValid(Account account)
        {
            var result = _accountRepository.Get(account);

            return result.Result;
        }
    }
}
