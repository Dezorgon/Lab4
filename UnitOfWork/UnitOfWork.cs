using DataAccessLayer.Repositories;
using Models;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string connectionString;
        private CustomerRepository customerRepository;
        private ProductRepository productRepository;
        private OrderRepository orderRepository;
        public UnitOfWork()
        {
             connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AdventureWorksLT2019;Data Source=DESKTOP-TMEKROF\SQLEXPRESS";
        }

        public IRepository<Customer> Customers => customerRepository ??= new CustomerRepository(connectionString);

        public IRepository<Order> Orders => orderRepository ??= new OrderRepository(connectionString);

        public IRepository<Product> Products => productRepository ??= new ProductRepository(connectionString);
    }
}