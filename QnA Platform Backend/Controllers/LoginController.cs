using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QnAPlatformBackend.Data.Repositories;
using QnAPlatformBackend.ViewModels;
using System.Security.Claims;

namespace QnAPlatformBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public string Login()
        {
            return "You Must Login First";
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .FirstOrDefault().Value);

            var newUser = await userRepository.GetByUsernameAndPasswordAsync(loginModel.UserName, loginModel.Password);

            if (newUser == null)
                return Unauthorized();

            if (User.Identity.IsAuthenticated && userId == newUser.Id)
                return Ok("User is already Authenticated");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties { IsPersistent = true });

            return Ok("Login successfully");
        }
    }
}
