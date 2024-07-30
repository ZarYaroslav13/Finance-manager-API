using Finance_manager.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Finance_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<AccountDTO> Get()
        {
            return new List<AccountDTO>();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public AccountDTO Get(int id)
        {
            return new AccountDTO();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post(AccountDTO account)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, AccountDTO value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
