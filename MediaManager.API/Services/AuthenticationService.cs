using MediaManager.API.Controllers;
using MediaManager.API.Model;
using Microsoft.AspNetCore.Identity;

namespace MediaManager.API.Services
{
    public class AuthenticationService
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AuthenticationService> logger;

        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AuthenticationService> logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> RegisterNewUser(AuthenticationRequestBody authRequest)
        {
            var message = String.Empty;

            var registerEnabled = Environment.GetEnvironmentVariable("REGISTER_ENABLED");
            if (String.IsNullOrWhiteSpace(registerEnabled) || registerEnabled.ToLower() != "true")
            {
                return "Register not supported";
            }
            var userInStore = await userManager.FindByNameAsync(authRequest.Email);
            if (userInStore != null)
            {
                return "already registered";
            }
            var identityUser = new IdentityUser()
            {
                Email = authRequest.Email?.Trim(),
                UserName = authRequest.UserName?.Trim()
            };
            var result = await userManager.CreateAsync(identityUser, authRequest.Password);
            if (!result.Succeeded)
            {
                return "Register failed";
            }
            return message;
        }

        public async Task<User?> Authenticate(AuthenticationRequestBody authRequest)
        {
            User? userDto = null;
            var userInStore = await userManager.FindByNameAsync(authRequest.Email);

            if (userInStore != null)
            {
                var result = await signInManager.PasswordSignInAsync(authRequest.Email, authRequest.Password, false, false);

                if (result.Succeeded)
                {
                    userDto = new User(userInStore.Id, userInStore.Email, userInStore.UserName);
                }
            }
            else
            {
                if (authRequest.RegisterIfNotFound)
                {
                    var responseRegister = await RegisterNewUser(authRequest);
                    if (!String.IsNullOrWhiteSpace(responseRegister))
                    {
                        logger.LogInformation(responseRegister);
                    }
                }
            }
            return userDto;
        }

        public async Task<string?> UpdateUser(AuthenticationRequestBody authRequest)
        {
            string message = string.Empty;
            var userInStore = await userManager.FindByNameAsync(authRequest.Email);

            if (userInStore != null)
            {
                var result = await signInManager.PasswordSignInAsync(authRequest.Email, authRequest.Password, false, false);

                if (result.Succeeded)
                {
                    if (authRequest.UpdateRoles && authRequest.Roles != null && authRequest.Roles.Any())
                    {
                        var resultUpdateRoles = await UpdateRoles(userInStore, authRequest.Roles);

                    }
                }
            }
            else
            {
                if (authRequest.RegisterIfNotFound)
                {
                    var responseRegister = await RegisterNewUser(authRequest);
                }
            }
            return message;
        }

        private async Task<bool> UpdateRoles(IdentityUser user, ICollection<string> roles)
        {
            var result = await userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        //public async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        //{
        //    ApplicationUser user = await _userManager.FindByEmailAsync(email);
        //    var result = await _userManager.AddToRolesAsync(user, roles);

        //    return result;
        //}
    }
}
