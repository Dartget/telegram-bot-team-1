namespace SomeProject.Services
{
    public interface IStore
    {
        bool HasEnoughInventory(Product product, int quantity);
        bool RemoveInventory(Product product, int quantity);
    }
}