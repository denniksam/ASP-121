using Microsoft.EntityFrameworkCore;

namespace ASP121.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entity.User> Users { get; set; }
        public DbSet<Entity.Product> Products { get; set; }
        public DbSet<Entity.ProductGroup> ProductGroups { get; set; }
        public DbSet<Entity.Rate> Rates { get; set; }
        

        
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ASP121");
            modelBuilder  // вказуємо композитний ключ для таблиці Rates
                .Entity<Entity.Rate>()
                .HasKey(nameof(Entity.Rate.ProductId), nameof(Entity.Rate.UserId));
        }
    }
}
/* Робота із спільною БД у кількох проєктах.
 * Хостинг часто обмежує використання кількох БД, тому доводиться реалізовувати
 * декілька різних проєктів на одній БД. При цьому збіг імен таблиць може
 * пошкодити нормальну роботу, зокрема, таблиці міграцій скрізь називаються
 * однаково.
 * Для "розділення" проєктів у одній БД вживаються наступні заходи:
 * - якщо БД підтримує схеми, то контекст прив'язується до схеми
 *   якщо ні, то ідея схем залишається, але змінюється механізм 
 *   іменування таблиць за префіксною схемою (схема_таблиця)
 *   Дані про схему додаються у метод контексту OnModelCreating
 * - Для включення префіксної схеми зазначаються опції у Program.cs
 *   при конфігуруванні контексту даних
 */
