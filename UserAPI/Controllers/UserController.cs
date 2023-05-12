using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Core;
using UserAPI.Entities.Models;
using UserAPI.Entities.Resources;
using UserAPI.Persistence.repositories;

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


    [HttpGet]
    public async Task<IActionResult> GetUser([FromBody] UserCredentialsResource userResource)
    {
        var user = await userRepository.GetUser(userResource.UserName);

        if (user == null || !securityService.CheckPassword(userResource.Password, user.HashedPassword))
            return BadRequest(new
            {
                Message = "Failed to Get user.",
                Error = "UserName or Password is incorrect!"
            });

        var result = MapUserToUserResource(user, userResource);
        return Ok(result);
    }
    private UserResource MapUserToUserResource(User user, UserCredentialsResource userResource)
    {
        var result = mapper.Map<User, UserResource>(user);
        var userDecryptedData = securityService.Decrypt(user.EncryptedData, userResource.Password);
        mapper.Map<BirthdateAddressCombination, UserResource>(userDecryptedData, result);
        return result;
    }

}
