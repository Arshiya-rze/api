using api.Dtos;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppUserController : ControllerBase
{
    //injection repository
    private readonly IAppUserAccountRepository _appUserAccountRepository;

    public AppUserController(IAppUserAccountRepository appUserAccountRepository)
    {
        _appUserAccountRepository = appUserAccountRepository;
    }

    #region sign-up
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto userInput)
    {
        //first Check Password
        if (userInput.Password != userInput.ConfirmPassword)
        {
            return BadRequest("Wrong Passwords!");
        }

        UserDto? userDto = await _appUserAccountRepository.Create(userInput);

        if (userDto is null)
        {
            return BadRequest("user already exist!");
        }
        return userDto;
    }
    #endregion
    // now create login with post
    #region login
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto userInput)
    {
        UserDto? userDto = await _appUserAccountRepository.Login(userInput);

        if (userDto is null)
        {
            return Unauthorized("hhh");
        }
        return userDto;
    }
    #endregion
}

