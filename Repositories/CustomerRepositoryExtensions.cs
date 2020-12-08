using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    static class CustomerRepositoryExtensions
    {
        public static DataTable CustomerSummaryByName(this CustomerRepository repository, string firstName, string lastName)
        {
            using (var connection = new SqlConnection(repository.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("CustomerSummaryByName", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@firstName", firstName));
                command.Parameters.Add(new SqlParameter("@lastName", lastName));

                var adapter = new SqlDataAdapter(command);

                var customer = new DataTable("Customer");
                adapter.Fill(customer);

                return customer;
            }
        }
    }
}
