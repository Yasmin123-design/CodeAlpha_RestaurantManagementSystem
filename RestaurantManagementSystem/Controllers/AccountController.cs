using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagementSystem.Dto;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        public AccountController(UserManager<ApplicationUser> _userManager, IConfiguration config , IEmailService _emailService)
        {
            userManager = _userManager;
            configuration = config;
            emailService = _emailService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerDto.UserName;
                applicationUser.PasswordHash = registerDto.Password;
                applicationUser.Email = registerDto.Email;
                applicationUser.Address = registerDto.Address;


                IdentityResult result = await userManager.CreateAsync(applicationUser, registerDto.Password);
                if (result.Succeeded)
                {
                    return Ok(applicationUser);
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(loginDto.UserName);
                
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, loginDto.Password);
                    if (found)
                    {
                        // create token
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                        SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"],
                            audience: configuration["JWT:ValidAudiance"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1), // token هيبقى valid لحد امتى
                            signingCredentials: signing

                            );


                        return Ok(new
                        {
                            mytoken = new JwtSecurityTokenHandler().WriteToken(token), // create token
                            expiration = token.ValidTo  // هتبقى valid لحد امتا
                        });
                    }
                }
                return Unauthorized();

            }
            return BadRequest(ModelState);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPassword)
        {
             if (ModelState.IsValid)
                {

                
                    var user = await userManager.FindByEmailAsync(forgetPassword.Email.Trim().ToLower());
                    
                    if (user.Email == null)
                    {
                        return NotFound("not found");
                    }

                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var callbackUrl = Url.Action("ResetPassword", "Account", new { token = HttpUtility.UrlEncode(token), email = user.Email }, Request.Scheme);

                    await emailService.SendEmailAsyn(user.Email, "Reset Password", callbackUrl);
                    return Ok("Reset password link has been sent to your email.");

                }
             return BadRequest(ModelState);
            
            
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser application = await userManager.FindByEmailAsync(resetPassword.Email);
                
                if (application == null)
                {
                    return NotFound("not found");
                }
                // اعاده تكوين كلمه المرور
                var result = await userManager.ResetPasswordAsync(application, resetPassword.Token, resetPassword.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("reset password done successfully");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
