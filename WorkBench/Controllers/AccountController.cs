using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WorkBench.Models;

namespace WorkBench.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationSettings _appSettings;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }
        /*
        {
          "Email":"vasimsharif@gmail.com",
          "Password":"Vasim@123",
          "ConfirmPassword":"Vasim@123",
          "City":"Chennai"
        }
        */
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(Account model)
        {
            try
            {
                model.Role = "Customer";
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, model.Role);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Created("Registered Successfully", result);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return BadRequest(error);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(model);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(Account model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName) ?? await _userManager.FindByEmailAsync(model.UserName);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {


                    var role = await _userManager.GetRolesAsync(user);
                    IdentityOptions identityOptions = new IdentityOptions();


                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(identityOptions.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())

                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
            }
            
            catch(Exception ex)
            {
                throw ex;
               
            }
            return BadRequest(new { message = "Username or password is incorrect." });
        }
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}