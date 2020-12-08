using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer.Repositories
{
    class ProductRepository : IRepository<Product>
    {
        public readonly string ConnectionString;
        public ProductRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public void Create(Product item) => throw new NotImplementedException();
        public void Delete(int id) => throw new NotImplementedException();

        public Product Get(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("GetProductByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@ID", id));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            Id = id,
                            Name = reader.GetString(1),
                        };
                        return product;
                    }
                }
            }
            throw new KeyNotFoundException();
        }
        public IEnumerable<Product> GetAll() => throw new NotImplementedException();
        public void Update(Product item) => throw new NotImplementedException();
    }
}
