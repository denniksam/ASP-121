namespace ASP121.Data.Entity
{
    public class User
    {
        public Guid     Id           { get; set; }
        public String   Login        { get; set; } = null!;
        public String   PasswordHash { get; set; } = null!;
        public String?  RealName     { get; set; }
        public String   Email        { get; set; } = null!;
        public String?  Avatar       { get; set; }
        public DateTime RegisteredDt { get; set; }

    }
}
