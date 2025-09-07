using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AgroManager.Data.AgroDBContext agroDBContext;
        public UserController(AgroManager.Data.AgroDBContext agroDBContext)
        {
            this.agroDBContext = agroDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await agroDBContext.Users.ToListAsync();
            var userDtos = new List<Models.Domain.User>();
            // convert domain to dto
            foreach (var user in users)
            {
                userDtos.Add(new Models.Domain.User
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    IsAdmin = user.IsAdmin,
                    CreatedAt = user.CreatedAt
                });
            }
            return Ok(userDtos);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await agroDBContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new Models.Domain.User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt
            };

            return Ok(userDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(Models.DTO.AddUserDTO addUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userDomain = new Models.Domain.User
            {
                Id = Guid.NewGuid(),
                Username = addUserDTO.Username,
                Email = addUserDTO.Email,
                IsActive = addUserDTO.IsActive,
                IsAdmin = addUserDTO.IsAdmin,
                CreatedAt = DateTime.UtcNow
            };
            await agroDBContext.Users.AddAsync(userDomain);
           await  agroDBContext.SaveChangesAsync();
            var userDto = new Models.DTO.UserDTO
            {
                Id = userDomain.Id,
                Username = userDomain.Username,
                Email = userDomain.Email,
                IsActive = userDomain.IsActive,
                IsAdmin = userDomain.IsAdmin,
                CreatedAt = userDomain.CreatedAt
            };
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task< IActionResult> UpdateUser([FromRoute] Guid id, Models.DTO.UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userDomain = await agroDBContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userDomain == null)
            {
                return NotFound();
            }
            userDomain.Username = updateUserDTO.Username;
            userDomain.Email = updateUserDTO.Email;
            userDomain.IsActive = updateUserDTO.IsActive;
            userDomain.IsAdmin = updateUserDTO.IsAdmin;
            await agroDBContext.SaveChangesAsync();
            var userDto = new Models.DTO.UserDTO
            {
                Id = userDomain.Id,
                Username = userDomain.Username,
                Email = userDomain.Email,
                IsActive = userDomain.IsActive,
                IsAdmin = userDomain.IsAdmin,
                CreatedAt = userDomain.CreatedAt
            };
            return Ok(userDto);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var userDomain = await agroDBContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userDomain == null)
            {
                return NotFound();
            }
             agroDBContext.Users.Remove(userDomain);
            await agroDBContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
