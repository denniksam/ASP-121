using ASP121.Data;
using ASP121.Models.User;
using ASP121.Services.Hash;
using Microsoft.AspNetCore.Mvc;

namespace ASP121.Controllers
{
    public class UserController : Controller
    {
        // Підключення БД - інжекція залежності від контексту (зареєстрованого у Program.cs)
        private readonly DataContext _dataContext;
        private readonly IHashService _hashService;

        public UserController(DataContext dataContext, IHashService hashService)
        {
            _dataContext = dataContext;
            _hashService = hashService;
        }

        public IActionResult SignUp(UserSignupModel? model)
        {
            if(HttpContext.Request.Method == "POST")   // є передані з форми дані
            {
                ViewData["form"] = _ValidateModel(model);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult LogIn([FromForm] String login, [FromForm] String password)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Login == login);
            if(user != null)
            {
                if(user.PasswordHash == _hashService.HashString(password))
                {
                    // Автентифікацію пройдено
                    // зберігаємо у сесії id користувача
                    HttpContext.Session.SetString("AuthUserId", user.Id.ToString());
                    return Json(new { status = "OK" });
                }
            }
            return Json(new { status = "NO" });
        }

        // Перевіряє валідність даних у моделі, прийнятої з форми
        // Повертає повідомлення про помилку валідації або String.Empty
        // якщо перевірка успішна
        private String _ValidateModel(UserSignupModel? model)
        {
            if(model == null) { return "Дані не передані"; }
            if(String.IsNullOrEmpty(model.Login)) { return "Логін не може бути порожним"; }
            if(String.IsNullOrEmpty(model.Password)) { return "Пароль не може бути порожним"; }
            if(String.IsNullOrEmpty(model.Email)) { return "Email не може бути порожним"; }
            if( ! model.Agree) { return "Необхідно дотримуватись правил сайту"; }
            // завантажуємо файл-аватарку
            String? newName = null;
            if (model.AvatarFile != null)  // є файл
            {
                // визначаємо тип (розширення) файлу
                String ext = Path.GetExtension(model.AvatarFile.FileName);

                // Д.З. Перевірити тип файлу на допустимий перелік

                // змінюємо ім'я файлу - це запобігає випадковому перезапису
                newName = Guid.NewGuid().ToString() + ext;

                // зберігаємо файл - в окрему папку wwwroot/uploads (створюємо вручну)
                model.AvatarFile.CopyTo(
                    new FileStream(
                        $"wwwroot/uploads/{newName}",
                        FileMode.Create));
            }

            // додаємо користувача до БД
            _dataContext.Users.Add(new Data.Entity.User
            {
                Id = Guid.NewGuid(),
                Login = model.Login,
                PasswordHash = _hashService.HashString(model.Password),
                Email = model.Email,
                Avatar = newName,
                RealName = model.RealName,
                RegisteredDt = DateTime.Now,
            });
            // зберігаємо внесені зміни
            _dataContext.SaveChanges();  // PlanetScale не підтримує асинхронні запити

            return String.Empty;
        }
    }
}
/* Д.З. Валідація: 
 *       - додати перевірку на однаковість паролю та його повтору
 *       - перевірити тип файлу на допустимий перелік
 *       - * перевірити Email регулярним виразом
 *       - * перевірити пароль на складність
 *       - перевірити Логін на те, що такий вже використовується
 */