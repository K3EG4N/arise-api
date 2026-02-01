using arise_api.dtos.Generics;
using arise_api.dtos.Request;
using arise_api.dtos.Responses;
using arise_api.services;
using Microsoft.AspNetCore.Mvc;

namespace arise_api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService service) : Controller
    {
        private readonly IUserService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserByIdResponse>>> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserByIdResponse?>> GetUserById(Guid id)
        {
            var user = await _service.FindUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            var newUser = await _service.CreateUserAsync(request);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> UpdateUser([FromRoute] Guid? id, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _service.UpdateUserAsync(id, request);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteUser([FromRoute] Guid? id)
        {
            var deletedUser = await _service.DeleteUserAsync(id);
            return Ok(deletedUser);
        }
    }
}
