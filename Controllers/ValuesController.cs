using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(local)";
            String connectionString = "Server=<server-name>; Database=StockDatabase; Encrypt=False; Trusted_Connection=True;";
            string[] records = new string[4];
            List<string> listIds = new List<string>();
            List<string> listPrices = new List<string>();
            List<string> listDatesStock = new List<string>();
            List<string> listCategories = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "Select Id, Price, DateStock, Category from Stock";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listIds.Add(reader.GetValue(0).ToString());
                                listPrices.Add(reader.GetValue(1).ToString());
                                listDatesStock.Add(reader.GetValue(2).ToString());
                                listCategories.Add(reader.GetValue(3).ToString());
                            }
                        }
                }
            }

            int i = 0;
            foreach(var record in listIds)
            {
                Product product = new Product();
                product.Id = record;
                product.Price = listPrices[i];
                product.DateStock = listDatesStock[i];
                product.Category = listCategories[i];
                yield return $"{record},{listPrices[i]},{listDatesStock[i]},{listCategories[i]}";
                i++;
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
