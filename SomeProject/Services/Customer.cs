namespace SomeProject.Services
{
    public class Customer : ICustomer
    {
        private readonly IStore store;

        public Customer(IStore store)
        {
            this.store = store;
        }

        public bool Purchase(Product product, int quantity)
        {
            if (!this.store.HasEnoughInventory(product, quantity))
            {
                return false;
            }

            this.store.RemoveInventory(product, quantity);
            return true;
        }
    }
}