using ClientPropertyWebApp1.Server.Data;
using ClientPropertyWebApp1.Server.Model.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientPropertyWebApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PropertyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Property>>> GetProperties()
        {
            var properties = await _appDbContext.Properties.ToListAsync();
            return Ok(properties);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _appDbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id)
        {
            var property = await _appDbContext.Properties
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (property == null)
            {
                return NotFound("NotFound");
            }
            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<List<Property>>> CreateProperties(Property property)
        {
            property.User = null;
            _appDbContext.Properties.Add(property);
            await _appDbContext.SaveChangesAsync();
            return Ok(await GetDbProperties());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Property>>> UpdateProperties(Property property, int id)
        {
            var dbProperty = await _appDbContext.Properties
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (dbProperty == null)
                return NotFound("Property with this ID not found!");

            dbProperty.NameProperty = property.NameProperty;
            dbProperty.TypeOfProperty = property.TypeOfProperty;
            dbProperty.InitialValue = property.InitialValue;
            dbProperty.PriceLossSelectedPeriod = property.PriceLossSelectedPeriod;
            dbProperty.UserId = property.UserId;

            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbProperties());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Property>>> DeleteProperty(int id)
        {
            var dbProperty = await _appDbContext.Properties
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (dbProperty == null)
                return NotFound("Property with this ID not found!");

           _appDbContext.Properties.Remove(dbProperty);

            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbProperties());
        }

        private async Task<List<Property>> GetDbProperties()
        {
            return await _appDbContext.Properties.Include(u => u.User).ToListAsync();
        }

        [HttpGet("getDaysOfPropertyOwnership")]
        public async Task<IEnumerable<Property>> GetDaysOfPropertyOwnership()
        {
            var propertyDaysOwned = await _appDbContext.Properties.Select(propertyResponse => new Property
            {
                DaysOfPropertyOwnership = (DateTimeOffset.UtcNow - propertyResponse.PurchaseDate).Days
            }).ToListAsync();

            return propertyDaysOwned;
        }

        [HttpGet("getDaysPeriodsCountByWeek")]
        public async Task<IEnumerable<GetDaysPeriodsCountResponseModel>> GetDaysPeriodsCountByWeek()
        {
            var period = 7;
            var propertyDaysOwned = await _appDbContext.Properties.Select(propertyResponse => new GetDaysPeriodsCountResponseModel
            {
                PeriodsCount = (DateTimeOffset.UtcNow - propertyResponse.PurchaseDate).Days / period
            }).ToListAsync();
            return propertyDaysOwned;
        }

        [HttpGet("getDaysPeriodsCountByMonth")]
        public async Task<IEnumerable<GetDaysPeriodsCountResponseModel>> GetDaysPeriodsCountByMonth()
        {
            var period = 30;
            var propertyDaysOwned = await _appDbContext.Properties.Select(propertyResponse => new GetDaysPeriodsCountResponseModel
            {
                PeriodsCount = (DateTimeOffset.UtcNow - propertyResponse.PurchaseDate).Days / period
            }).ToListAsync();
            return propertyDaysOwned;
        }

        [HttpGet("getDaysPeriodsCountByYear")]
        public async Task<IEnumerable<GetDaysPeriodsCountResponseModel>> GetDaysPeriodsCountByYear()
        {
            var period = 365;
            var propertyDaysOwned = await _appDbContext.Properties.Select(propertyResponse => new GetDaysPeriodsCountResponseModel
            {
                PeriodsCount = (DateTimeOffset.UtcNow - propertyResponse.PurchaseDate).Days / period
            }).ToListAsync();
            return propertyDaysOwned;
        }

        [HttpGet("getDaysPeriodsCountByYear")]
        public async Task<IEnumerable<GetCurrentPeriodResponseModel>> GetCurrentValue()
        {
            var propertyCurrentValue = await _appDbContext.Properties.Select(propertyResponse => new GetCurrentPeriodResponseModel
            {
                CurrentValue = propertyResponse.InitialValue - (propertyResponse.PriceLossSelectedPeriod * (DateTimeOffset.UtcNow - propertyResponse.PurchaseDate).Days)
            }).ToListAsync();
            return propertyCurrentValue;
        }
    }
}
