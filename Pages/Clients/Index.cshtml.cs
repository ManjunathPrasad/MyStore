using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        //Create list of clients
        public List<ClientsInfo> listclients = new List<ClientsInfo>();
        
        //Get Call
        public void OnGet()
        {
            try
            {
                //Connection String
                string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                
                //Establish Connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM clients";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientsInfo clientsInfo = new ClientsInfo();

                                clientsInfo.Id = reader.GetInt32(0);

                                clientsInfo.Name = reader.GetString(1);

                                clientsInfo.Email = reader.GetString(2);

                                clientsInfo.Phone = reader.GetString(3);

                                clientsInfo.Address = reader.GetString(4);

                                clientsInfo.Created_At = reader.GetDateTime(5).ToString();

                                listclients.Add(clientsInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.ToString()}");
            }
        }
    }

    public class ClientsInfo
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Created_At { get; set; }
    }
}
