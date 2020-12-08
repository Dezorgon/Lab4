using DataAccessLayer.Repositories;

namespace DataManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AdventureWorksLT2019;Data Source=DESKTOP-TMEKROF\SQLEXPRESS";
            var customerRepository = new CustomerRepository(connectionString);
            var customer = customerRepository.Get(29847);
            var productRepository = new ProductRepository(connectionString);
            var product = productRepository.Get(801);
            var orderRepository = new OrderRepository(connectionString);
            var order = orderRepository.Get(71774);
            customerRepository.CustomerSummaryByName("Catherine", "Abel");
        }
    }
}
