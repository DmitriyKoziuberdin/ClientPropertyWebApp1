using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientPropertyWebApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _appDbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            var singleUser = await _appDbContext.Users.FirstOrDefaultAsync(userId => userId.Id == id);
            if(singleUser == null)
            {
                return NotFound("User with this id not found!");
            }
            return Ok(singleUser);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateProperties(User user)
        {
            //user.User = null;
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return Ok(await GetDbUsers());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateProperties(User user, int id)
        {
            var dbUser = await _appDbContext.Users
                .FirstOrDefaultAsync(userId => userId.Id == id);
            if (dbUser == null)
                return NotFound("User with this ID not found!");

            dbUser.Name = user.Name;
            dbUser.Address = user.Address;
            dbUser.Telephone = user.Telephone;
            dbUser.Email = user.Email;

            await _appDbContext.SaveChangesAsync();

            return Ok(await GetUsers());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteProperty(int id)
        {
            var dbUser = await _appDbContext.Users
                .FirstOrDefaultAsync(userId => userId.Id == id);
            if (dbUser == null)
                return NotFound("User with this ID not found!");

            _appDbContext.Users.Remove(dbUser);

            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbUsers());
        }

        private async Task<List<User>> GetDbUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }
    }
}
