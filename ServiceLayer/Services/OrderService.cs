using Models;
using DataAccessLayer.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        private IEnumerable<Order> GetOrders() => Database.Orders.GetAll();
        private Customer GetCustomer(int id) => Database.Customers.Get(id);
        private Product GetProduct(int id) => Database.Products.Get(id);
        public IEnumerable<OrderInfo> GetOrdersInfo()
        {
            var orders = GetOrders();
            return (from order in orders
                let customer = GetCustomer(order.CustomerID)
                let product = GetProduct(order.ProductID)
                select new OrderInfo()
                {
                    ProductName = product.Name,
                    CustomerFirstName = customer.FirstName,
                    CustomerLastName = customer.LastName,
                    CustomerEmail = customer.EmailAddress,
                    CustomerAddress = customer.City + customer.Address,
                    CustomerPhone = customer.Phone,
                });
        }
    }
}