using Microsoft.AspNetCore.Identity;

namespace ClientPropertyWebApp1.Shared
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public double InitialSumOfUserProperty { get; set; }
        public double CurrentSumOfUserProperty { get; set; }
    }
}
