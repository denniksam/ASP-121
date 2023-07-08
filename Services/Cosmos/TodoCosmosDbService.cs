using Microsoft.Azure.Cosmos;

namespace ASP121.Services.Cosmos
{
    public class TodoCosmosDbService : ICosmosDbService
    {
        private readonly IConfiguration _configuration;
        private Microsoft.Azure.Cosmos.Database todoDatabase;
        private Microsoft.Azure.Cosmos.Container itemsContainer;

        public TodoCosmosDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            todoDatabase = null!;
            itemsContainer = null!;
        }

        public Container MainContainer
        {
            get
            {
                if(itemsContainer == null)  // перше звернення - треба підключитись
                {
                    var cosmos = _configuration.GetSection("Azure").GetSection("Cosmos");
                    String key = cosmos.GetValue<String>("Key");
                    String endpoint = cosmos.GetValue<String>("Endpoint");
                    String databaseId = cosmos.GetValue<String>("DatabaseId");
                    String containerId = cosmos.GetValue<String>("ContainerId");

                    todoDatabase =
                        new CosmosClient(endpoint, key,
                            new CosmosClientOptions()
                            {
                                ApplicationName = "ASP121"
                            }
                        ).CreateDatabaseIfNotExistsAsync(databaseId).Result;

                    itemsContainer =
                        todoDatabase
                        .CreateContainerIfNotExistsAsync(containerId, "/partitionKey")
                        .Result;
                }
                return itemsContainer;
            }
        }
    }
}
/*
                skip take
SELECT ... LIMIT 10,  20

1) page = 2 (perpage = 20)
skip = (page-1)*perpage

2) skip=10, (take=20)

Кошик ТЗ
 - Можливість додати товар зі сторінки "Крамниця"
 - При додаванні пропонується ввести кількість, за замовчанням - 1
 - Сторінка кошика 
   = Змінити кількість по кожному товару
   = Видалити позицію (як кнопкою, так і встановленням 0 кількості)
   = "Купити" - формується повідомлення-підтвердження з переліком 
      товарів та загальною ціною. Після успішного підтвердження
      кошик очищується, дані переносяться у таблицю "замовлень"

 */
/*
 Д.З. Azure: Додати до форми відгуку можливість оцінювання (зірочки)

Підготовка до Java
IDE: 
 - заходимо на сайт https://www.jetbrains.com/community/education/#students/
 - подаємо заяву на студентську ліцензію !! вказати пошту від Академії 
    (...@student.itstep.org)
 - реєструєтесь, отримаєте ліцензію
 - завантажуєте https://www.jetbrains.com/idea/download/?section=windows
    !! ULTIMATE
 - вказуєте отриману ліцензію

JRE:
 - відкриваєте термінал (командний рядок), вводите
     java -version
   якщо спрацьовує, то JRE вже існує

JDK:
 - після встановлення IDE натиснути "+" Новий проєкт, у полі JDK 
    є випадаючий перелік, серед іншого - "завантажити"
    вибрати версію 1.8.XXX від довільного виробника
 - альтернатива: завантажити на сайті
     https://www.oracle.com/java/technologies/javase/javase8u211-later-archive-downloads.html
    обравши під свій тип ОС та процесора
 */
