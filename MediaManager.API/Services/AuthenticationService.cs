using MediaManager.API.Controllers;
using MediaManager.API.Model;
using Microsoft.AspNetCore.Identity;

namespace MediaManager.API.Services
{
    public class AuthenticationService
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;

        public AuthenticationService(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> RegisterNewUser(AuthenticationRequestBody user)
        {
            var identityUser = new IdentityUser()
            {
                Email = user.Email?.Trim(),
                UserName = user.UserName?.Trim()
            };
            var result = await userManager.CreateAsync(identityUser, user.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<User?> Authenticate(AuthenticationRequestBody user)
        {
            User? userDto = null;
            var userInStore = await userManager.FindByNameAsync(user.Email);
            if (userInStore != null)
            {
                var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

                if (result.Succeeded)
                {
                    userDto = new User(userInStore.Id, userInStore.Email, userInStore.UserName);
                }
            }
            return userDto;
        }
    }
}
