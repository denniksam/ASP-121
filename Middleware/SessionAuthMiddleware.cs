using ASP121.Data;
using System.Security.Claims;

namespace ASP121.Middleware
{
    public class SessionAuthMiddleware
    {
        /* Ланцюг викликів (pipeline) утворюються через те, що кожен клас
         * Middleware викликає наступний клас. Контейнер ASP передає
         * кожному класу посилання на наступний шар. Клас має зберігти
         * це посилання та використати у своїх кодах
         */
        private readonly RequestDelegate _next;

        public SessionAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /* Хоча класи Middleware не є нащадками якихось базових класів,
         * вони повинні містити саме такий метод: InvokeAsync(HttpContext context)
         * У старих схемах вживається синхронний варіант (Invoke), але
         * він вважається застарілим і не радиться до вживання.
         * Частиною методу має бути виклик наступного шару: await _next(context)
         * Те, що передує _next, виконується на "прямій" ділянці оброблення
         * запиту, після цієї інструкції - "зворотна" ділянка.
         * Якщо _next не виконувати, то це припинить оброблення запиту. Таке
         * може бути корисним, якщо подальша робота неможлива, наприклад,
         * зафіксовано неможливість підключення до БД
         * Оскільки конструктор задіяний для створення ланцюга викликів,
         * інжекція служб (залежностей) здійснюється через метод
         */
        public async Task InvokeAsync(HttpContext context, DataContext dataContext)
        {
            if (context.Session.Keys.Contains("AuthUserId"))
            {
                // є дані збереженої автентифікації
                // шукаємо користувача за збереженим id
                var user = dataContext.Users121.Find(Guid.Parse(
                    context.Session.GetString("AuthUserId")!));
                if(user != null)
                {
                    // користувач знайдений - заповнюємо параметри 
                    // в ASP за це відповідає спеціальна конструкція - Claim
                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Login),
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.UserData, user.Avatar ?? "")
                    };
                    // у контексті є спеціальне поле User, яке є "збірником" цих параметрів
                    context.User = 
                        new ClaimsPrincipal(
                            new ClaimsIdentity(
                                claims, nameof(SessionAuthMiddleware)));
                    // оскільки контекст доступний скрізь у проєкті, дані про користувача
                    // також будуть загальнодоступні
                }
            }
            
            await _next(context);   // передача роботи наступній ланці
        }
    }

    /* Традицією Middleware є створення розширень, які дозволять у
     * Program.cs підкючати цей Middleware за допомогою команди
     * UseXxxx(), де Хххх - це назва Middleware. У нашому випадку - 
     *   app.UseSessionAuth()
     * Без розширення це буде команда
     *   app.UseMiddleware<SessionAuthMiddleware>();
     */
    public static class SessionAuthMiddlewareExtension
    {
        public static IApplicationBuilder 
            UseSessionAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SessionAuthMiddleware>();
        }
    }
}
