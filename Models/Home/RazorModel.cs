namespace ASP121.Models.Home
{
    public class RazorModel
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; } = string.Empty;
        public List<String> ListValue { get; set; } = null!;
        public Boolean BoolValue { get; set; }
    }
}
