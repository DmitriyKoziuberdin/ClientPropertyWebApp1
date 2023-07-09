namespace ClientPropertyWebApp1.Client.Services.UserService
{
    public interface IUserService
    {
        List<Property> Properties { get; set; }
        List<User> Users { get; set; }

        Task GetUsers();
        Task<User> GetSingleUser(int id);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
