using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityJWT.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//artık valuescontroller authentication a sahip
    //dolayısıyla yetkilendirilmeyen kullanıcılar buradaki isteklere erişemeyecekler
    public class ValuesController : ControllerBase
    {
        private AppIdentityDbContext context;

        public ValuesController(AppIdentityDbContext context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet]
       [Authorize(Roles ="admin")]//sadece admin rolune sahip kullanıcı tarafından görülebilir
        public ActionResult<IEnumerable<string>> Get()
        {
            //users tablosundan username leri geri doner
            return  context.Users.Select(x => x.UserName).ToArray();
            
        } 

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
