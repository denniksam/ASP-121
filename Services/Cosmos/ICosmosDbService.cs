namespace ASP121.Services.Cosmos
{
    public interface ICosmosDbService
    {
        Microsoft.Azure.Cosmos.Container MainContainer { get; }
    }
}
