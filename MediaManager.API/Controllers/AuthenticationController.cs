using MediaManager.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediaManager.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApiUser> signInManager;
        private readonly UserManager<ApiUser> userManager;

        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        //public AuthenticationController(IConfiguration configuration, SignInManager<ApiUser> signInManager, UserManager<ApiUser> userManager, ILogger<AuthenticationController> logger)
        //{
        //    this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        //    this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        //    this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        //    this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody authRequest)
        {
            var user = new ApiUser
            {
                UserName = "inye@mailinator.com",
                FirstName = "ira",
                LastName = "nye"
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));

            var jwtSecurityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                signingCredentials
                );
            var token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(token);
            //try
            //{
            //    if (ModelState.IsValid && !String.IsNullOrWhiteSpace(authRequest.UserName) && !String.IsNullOrWhiteSpace(authRequest.Password))
            //    {
            //        var user = await userManager.FindByNameAsync(authRequest.UserName);
            //        if (user == null)
            //        {
            //            return Unauthorized();
            //        }

            //        var signInResult = await signInManager.CheckPasswordSignInAsync(user, authRequest.Password, false);
            //        if (!signInResult.Succeeded)
            //        {
            //            return Unauthorized();
            //        }

            //        var secretForKey = configuration["Authentication:SecretForKey"];
            //        if (String.IsNullOrWhiteSpace(secretForKey))
            //        {
            //            logger.LogError($"Config Setting Not Found");
            //            return Unauthorized();
            //        }
            //        var securityKey = new SymmetricSecurityKey(
            //            Encoding.ASCII.GetBytes(secretForKey));

            //        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //        var claimsForToken = new List<Claim>();
            //        claimsForToken.Add(new Claim("sub", authRequest.UserName.ToString()));
            //        claimsForToken.Add(new Claim("given_name", user.FirstName));
            //        claimsForToken.Add(new Claim("family_name", user.LastName));

            //        var jwtSecurityToken = new JwtSecurityToken(
            //            configuration["Authentication:Issuer"],
            //            configuration["Authentication:Audience"],
            //            claimsForToken,
            //            DateTime.UtcNow,
            //            DateTime.UtcNow.AddHours(1),
            //            signingCredentials
            //            );
            //        var token = new JwtSecurityTokenHandler()
            //            .WriteToken(jwtSecurityToken);

            //        return Ok(token);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError($"Authentication Failed: {ex}");
            //}
            //return Unauthorized();
        }

        public class AuthenticationRequestBody
        {
            [Required]
            [MinLength(4)]
            [MaxLength(50)]
            public string? UserName { get; set; } = String.Empty;

            [Required]
            [MinLength(4)]
            [MaxLength(50)]
            public string? Password { get; set; } = String.Empty;
        }
    }
}
