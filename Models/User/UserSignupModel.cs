using Microsoft.AspNetCore.Mvc;

namespace ASP121.Models.User
{
    public class UserSignupModel
    {
        [FromForm(Name = "user-login")]   // <input name="user-login" ...
        public String Login { get; set; } = null!;

        [FromForm(Name = "user-password")]
        public String Password { get; set; } = null!;

        [FromForm(Name = "user-repeat")]
        public String RepeatPassword { get; set; } = null!;

        [FromForm(Name = "user-name")]
        public String? RealName { get; set; } = null!;

        [FromForm(Name = "user-email")]
        public String Email { get; set; } = null!;

        [FromForm(Name = "user-avatar")]
        public IFormFile AvatarFile { get; set; } = null!;

        [FromForm(Name = "user-confirm")]
        public Boolean Agree { get; set; }
    }
}
