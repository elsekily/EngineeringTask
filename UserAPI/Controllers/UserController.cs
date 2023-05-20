using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Core;
using UserAPI.Entities.Models;
using UserAPI.Entities.Resources;

namespace UserAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly ISecurityService securityService;

    public UserController(IUnitOfWork unitOfWork, IUserRepository repository, IMapper mapper, ISecurityService securityService)
    {
        this.unitOfWork = unitOfWork;
        this.userRepository = repository;
        this.mapper = mapper;
        this.securityService = securityService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> GetUser([FromBody] UserCredentialsResource userResource)
    {
        var user = await userRepository.GetUser(userResource.UserName);

        if (user == null || !securityService.CheckPassword(userResource.Password, user.HashedPassword))
            return BadRequest("Failed to Get user. UserName or Password is incorrect!");

        var result = MapUserToUserResource(user, userResource.Password);
        return Ok(result);
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("new")]
    public async Task<IActionResult> CreateUser([FromBody] UserSaveResource userResource)
    {
        var existingUser = await userRepository.GetUser(userResource.UserName);
        if (existingUser == null)
        {
            return BadRequest($"Failed to add user. User with username '{userResource.UserName}' already exists.");
        }

        var user = BuildUser(userResource);

        await userRepository.Add(user);
        await unitOfWork.CompleteAsync();

        var userFromDB = await userRepository.GetUser(userResource.UserName);
        var result = MapUserToUserResource(userFromDB, userResource.Password);

        return Created(nameof(CreateUser), result);
    }
    [HttpPut("passwordReset")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordUserResource userResource)
    {
        var user = await userRepository.GetUser(userResource.UserName);

        if (user == null || !securityService.CheckPassword(userResource.OldPassword, user.HashedPassword))
            return BadRequest("Failed to reset user password. UserName or Password is incorrect!");

        user.HashedPassword = securityService.HashPassword(userResource.NewPassword);

        ReEncryptUserData(userResource, user);

        await unitOfWork.CompleteAsync();

        var userFromDB = await userRepository.GetUser(userResource.UserName);
        var result = MapUserToUserResource(userFromDB, userResource.NewPassword);

        return Accepted(result);
    }


    private UserResource MapUserToUserResource(User user, string password)
    {
        var result = mapper.Map<User, UserResource>(user);
        var userDecryptedData = securityService.Decrypt(user.EncryptedData, password);
        mapper.Map<BirthdateAddressCombination, UserResource>(userDecryptedData, result);
        return result;
    }


    private User BuildUser(UserSaveResource userResource)
    {
        var user = mapper.Map<UserSaveResource, User>(userResource);

        user.HashedPassword = securityService.HashPassword(userResource.Password);

        var userData = mapper.Map<UserSaveResource, BirthdateAddressCombination>(userResource);

        user.EncryptedData = securityService.Encrypt(userData, userResource.Password);
        return user;
    }


    private void ReEncryptUserData(ResetPasswordUserResource userResource, User user)
    {
        var decryptedData = securityService.Decrypt(user.EncryptedData, userResource.OldPassword);
        user.EncryptedData = securityService.Encrypt(decryptedData, userResource.NewPassword);
    }

}
