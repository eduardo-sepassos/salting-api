using Microsoft.AspNetCore.Mvc;
using SaltingApi.Models;
using SaltingApi.Repository;
using SaltingHandler.Entities;
using SaltingHandler.Handlers.Passwords;

namespace SaltingApi.Controllers;
[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private const string ErrorMessage = "Incorrect User or Password";
    private readonly IPasswordHandler _passwordHandler;
    private readonly IUserCredentialsRepository _userCredentialsRepository;

    public UserController(IPasswordHandler passwordHandler, IUserCredentialsRepository userCredentialsRepository)
    {
        _passwordHandler = passwordHandler;
        _userCredentialsRepository = userCredentialsRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePassword([FromBody] UserCredentials request)
    {
        try
        {
            var result = _passwordHandler.CreateCredentials(request.Password);

            var storedCredentials = new StoredCredentials
            {
                User = request.User,
                Password = result.PasswordHash,
                Salt = result.Salt
            };

            await _userCredentialsRepository.AddAsync(storedCredentials);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("log-in")]
    public async Task<IActionResult> LogIn([FromBody] UserCredentials request)
    {
        var storedCredentials = await _userCredentialsRepository.GetAsync(request.User);

        if (storedCredentials is null)
            return BadRequest(ErrorMessage);

        var isValid = _passwordHandler.VerifyPassword(request.Password, new HashedCredentials
        {
            PasswordHash = storedCredentials.Password,
            Salt = storedCredentials.Salt
        });

        if(isValid)
            return Ok("Correct");

        return BadRequest(ErrorMessage);

    }
}
