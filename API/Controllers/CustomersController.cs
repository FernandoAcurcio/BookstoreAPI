using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController
    {
        [HttpGet]
        public string GetCustomersItems()
        {
            return "List of sales items";
        }

        [HttpGet("{id}")]
        public string GetCustomerItem(int id)
        {
            return "get item";
        }
    }
}
