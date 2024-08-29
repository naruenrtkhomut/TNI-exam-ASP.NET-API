using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using server_api.Models;

namespace server_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllProductController : ControllerBase
    {
        private readonly ILogger<AllProductController> _logger;

        public AllProductController(ILogger<AllProductController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var datas = new List<Product>();
            var conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=pass123;database=shopping_db");
            try
            {
                conn.Open();
                var comm = conn.CreateCommand();
                comm.CommandText = "select * from product_list limit 100";
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    var data = new Product(); ;
                    data.id = reader.GetInt32("id");
                    data.name = reader.GetString("name");
                    data.price = reader.GetDouble("price");
                    data.strock = reader.GetInt32("strock");
                    data.img = reader.GetString("img");
                    datas.Add(data);
                }
                reader.Close();
                comm.Dispose();
            }
            catch { }
            finally
            {
                conn.Close();
            }
            return datas;
        }
        [HttpPost]
        public bool Update(int id, int stock)
        {
            var conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=pass123;database=shopping_db");
            try
            {
                conn.Open();
                var comm = conn.CreateCommand();
                comm.CommandText = $"update product_list set strock={stock} where id={id}";
                comm.ExecuteNonQuery();
                comm.Dispose();
                return true;
            }
            catch { }
            finally
            {
                conn.Close();
            }
            return false;
        }

    }
}
