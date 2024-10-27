using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.User
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class UserController(UserService userService) : ControllerBase;
}