namespace ClientPropertyWebApp1.Shared
{
    public class Property
    {
        public int Id { get; set; }
        public string? NameProperty { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string? TypeOfProperty { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public double InitialValue { get; set; }
        public string? PriceLossPeriod { get; set; }
        public double PriceLossSelectedPeriod { get; set; }
        public double CurrentValue { get; set; }
        public int DaysOfPropertyOwnership { get; set; } 
    }
}
