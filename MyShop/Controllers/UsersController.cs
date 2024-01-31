using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;
        public UsersController(IUser user)
        {
            _user= user;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Signup([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest(
                        new { 
                            message = "Please add User Information"
                        }
                    );
            }
            userObj.Role = _user.AddUserRole(userObj.Password);

            string message = await _user.UserRegisterToDB(userObj);

            return Ok(
                new
                {
                    message = message
                }

                );
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            var user = await _user.UserValidation(loginData);
            if (user == null)
            {
                return NotFound(
                        new
                        {
                            message = "Login is invalid"
                        }
                    );
            }

            return Ok(
                    new
                    {
                        token = JWTToken.CreateJWT(user),
                        message = "Login is success"
                    }
                );
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody] UserUpdateData user)
        {
            var message = await _user.UpdateUserInDB(id, user);

            return Ok(message);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var message = await _user.DeleteUserInDB(id);
            return Ok(message);
        }
    }
}
