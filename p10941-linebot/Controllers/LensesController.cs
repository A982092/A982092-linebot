using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace p10941_linebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LensesController : ControllerBase
    {
        // GET: api/Lenses
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Lenses/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Lenses
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Lenses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Lenses/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
