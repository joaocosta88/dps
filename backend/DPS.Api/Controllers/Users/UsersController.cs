using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class UsersController(UserService userService) : ControllerBase;
}