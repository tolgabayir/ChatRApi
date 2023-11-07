using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SignalR101.Models;
using SignalR101.Repository;
using SignalR101.Repository.Concrete.EfCore;

namespace SignalR101.Controller
{

    [ApiController]
	[Route("Auth")]
	public class AuthController:ControllerBase
	{
		private readonly UserManager<ApplicationUser> userManager;

		
		public AuthController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
			
		}



        [HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody]LoginModel model)
		{
			var user = await userManager.FindByNameAsync(model.Username);
			if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
			{
				var claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.Id),
					new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Iss,"secret_issuer"),
					new Claim(JwtRegisteredClaimNames.Aud,"secret_audience")
         
				};

				var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));
				
				var token = new JwtSecurityToken(
					expires:DateTime.UtcNow.AddHours(1),
					claims:claims,
					signingCredentials: new SigningCredentials(signinKey,SecurityAlgorithms.HmacSha256)

				);
				
				return Ok(new
				{
					token=new JwtSecurityTokenHandler().WriteToken(token),
					expiration=token.ValidTo
				}); 
			}
			else return Unauthorized("Yanlış bilgi");
		}




		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			var userExist = await userManager.FindByNameAsync(model.Username);
			if (userExist!=null)
				return StatusCode(StatusCodes.Status500InternalServerError,"User already exist!");
            ApplicationUser user = new() { Email = model.Email, SecurityStamp = Guid.NewGuid().ToString(), UserName = model.Username };
            var result = await userManager.CreateAsync(user, model.Password);
            var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iss,"secret_issuer"),
                    new Claim(JwtRegisteredClaimNames.Aud,"secret_audience")
                };

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)

            );
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,"User creation failed!");

			
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });

        }




        [HttpGet]
        [Route("users")]
        public List<ApplicationUser> GetUsersAsync()
		{
            using (var context = new ApplicationDbContext())
            {
				var result = userManager.Users.ToListAsync().Result;

				return result;
            }

        }


}
}

