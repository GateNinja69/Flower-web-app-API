using FlowersWebAPI.Repository;
using FlowersWebAPI.ViewModels;
using FlowrAAppAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace FlowersWebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;

        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, ITokenRepository tokenRepository, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }

                var user = new User                       // first thing we do is we create the user and async User and RegisterviewModel
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email
                };

                var createdUser = await userManager.CreateAsync(user, registerViewModel.password);    // here we created the user and async the password to it

                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "User");      // here we assign the role to user like Admin or User
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserViewModel
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = tokenRepository.CreateToken(user)
                            }
                            );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }

                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var user = await userManager.Users.FirstOrDefaultAsync(i => i.UserName == loginViewModel.UserName);

            if(user == null)
            {
                return Unauthorized("Invalid UserName");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid UserName or Password");
            }

            return Ok(
                new NewUserViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = tokenRepository.CreateToken(user)
                }
                );
        }
    }
}
