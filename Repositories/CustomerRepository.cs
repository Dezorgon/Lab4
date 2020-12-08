using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer.Repositories
{
    class CustomerRepository : IRepository<Customer>
    {
        public readonly string ConnectionString;
        public CustomerRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public void Create(Customer item) => throw new NotImplementedException();
        public void Delete(int id) => throw new NotImplementedException();
        public Customer Get(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("GetCustomerByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@ID", id));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var customer = new Customer
                        {
                            Id = id,
                            LastName = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            EmailAddress = reader.GetString(2),
                            Phone = reader.GetString(3),
                            City = reader.GetString(4),
                            Address = reader.GetString(5),
                            PostalCode = reader.GetString(6)
                        };
                        return customer;
                    }
                }
            }
            throw new KeyNotFoundException();
        }
        public IEnumerable<Customer> GetAll() => throw new NotImplementedException();
        public void Update(Customer item) => throw new NotImplementedException();
    }
}
