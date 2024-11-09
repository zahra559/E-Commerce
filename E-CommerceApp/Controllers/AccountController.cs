using E_CommerceApp.Constants;
using E_CommerceApp.Dtos.Requests.Account;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;

        public AccountController(UserManager<AppUser> userManager , ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(
                new CreateUserDto
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user , userRoles)
                }
            );
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var appUser = new AppUser { 
                    UserName = registerDto.UserName,
                    Email = registerDto.Email };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var createdRole = await _userManager.AddToRoleAsync(appUser, RoleTypes.USER_ROLE);

                    var userRoles = await _userManager.GetRolesAsync(appUser);

                    if (createdRole.Succeeded) return Ok(new CreateUserDto
                    {
                        Name = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser , userRoles)

                    });
                  
                    else return StatusCode(500, createdRole.Errors);

                }
                else  return StatusCode(500, createdUser.Errors);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
