namespace SomeProject.Services
{
    public interface ICustomer
    {
        bool Purchase(Product product, int quantity);
    }
}