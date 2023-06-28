using MediaManager.API.Model;
using MediaManager.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediaManager.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public partial class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IConfiguration configuration;
        private readonly AuthenticationService authenticationService;

        public AuthenticationController(IConfiguration configuration, AuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticationRequestBody user)
        {
            try
            {
                var response = await authenticationService.RegisterNewUser(user);
                if (!String.IsNullOrEmpty(response))
                {
                    return BadRequest(response);
                }
                return Ok($"User {user.Email} is registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("authentocate")]
        public async Task<ActionResult<string>> Authentocate(AuthenticationRequestBody authRequest)
        {
            try
            {
                if (ModelState.IsValid && !String.IsNullOrWhiteSpace(authRequest.UserName) && !String.IsNullOrWhiteSpace(authRequest.Password) &&
                    !String.IsNullOrWhiteSpace(authRequest.Email))
                {
                    var loggedInUser = await authenticationService.Authenticate(authRequest);
                    if (loggedInUser is null)
                    {
                        return Unauthorized();
                    }

                    var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GetOption("SECRETFORKEY", "Authentication")));

                    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claimsForToken = new List<Claim>();
                    claimsForToken.Add(new Claim("sub", loggedInUser.Id));
                    claimsForToken.Add(new Claim("user_name", loggedInUser.UserName));
                    claimsForToken.Add(new Claim("email", loggedInUser.Email));

                    var jwtSecurityToken = new JwtSecurityToken(
                        GetOption("ISSUER", "Authentication"),
                        GetOption("AUDIENCE", "Authentication"),
                        claimsForToken,
                        DateTime.UtcNow,
                        DateTime.UtcNow.AddHours(1),
                        signingCredentials
                        );
                    var token = new JwtSecurityTokenHandler()
                        .WriteToken(jwtSecurityToken);
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Authentication Failed: {ex}");
            }
            return Unauthorized();
        }

        private string GetOption(string setting, string? section = null)
        {
            var option = Environment.GetEnvironmentVariable($"{setting}");
            if(String.IsNullOrWhiteSpace(option))
            {
                option = String.IsNullOrWhiteSpace(section) ? configuration[setting] : configuration[$"{section}:{setting}"];
            }

            return option;
        }
    }
}
