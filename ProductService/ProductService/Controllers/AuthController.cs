using ProductService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly JWTConfiguration _configuration;
		private readonly IUserRepository _userRepository;

		public AuthController(JWTConfiguration configuration, IUserRepository repo)
		{
			_configuration = configuration;
			_userRepository = repo;
		}

		[HttpGet]
		[Route("test")]
		public string HealthCheck()
		{
			return "Hello world";
		}

		[HttpPost]
		[Route("token")]
		public AccessToken GenerateHash([FromBody]UserModel credentials)
		{
			string md5assword = CreateMD5(credentials.Password);

			if(!_userRepository.UserExists(credentials.Login, md5assword))
			{
				Response.StatusCode = StatusCodes.Status401Unauthorized;
				return new AccessToken
				{
					Success = false
				};
			}

			var claimes = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, credentials.Login),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};


			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.SecretKey));

			var expired = DateTime.Now.AddMinutes(_configuration.TokenExpirationTime);

			var signs = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(_configuration.Issuer, _configuration.ValidAudience, claimes, expires: expired, signingCredentials: signs);

			return new AccessToken
			{
				Success = true,
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				ExpireDate = token.ValidTo
			};
		}
		private static string CreateMD5(string input)
		{
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}
	}
}
