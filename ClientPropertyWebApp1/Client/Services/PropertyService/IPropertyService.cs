namespace ClientPropertyWebApp1.Client.Services.PropertyService
{
    public interface IPropertyService
    {
        List<Property> Properties { get; set; }
        List<User> Users { get; set; }

        Task GetProperties();
        Task GetUsers();
        Task<Property> GetSingleProperty(int id);
        Task CreateProperty(Property property);
        Task UpdateProperty(Property property);
        Task DeleteProperty(int id);
    }
}
