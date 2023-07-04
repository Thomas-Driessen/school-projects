using Kwetter_Post_API.Core.Interfaces;
using Kwetter_Post_API.Core.Services;
using Kwetter_Post_API.DAL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kwetter_Post_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetweetController : ControllerBase
    {
        private readonly IRetweetService _retweetService;
        public RetweetController(IRetweetService retweetService)
        {
            _retweetService = retweetService;
        }

        // GET: api/<RetweetController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RetweetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RetweetController>
        [HttpPost]
        public async Task<int> Post(Retweet retweet)
        {
            int result = await _retweetService.CreateRetweet(HttpContext.User, retweet);
            if (result == 0)
            {
                throw new Exception("No entries created.");
            }

            return result;
        }

        // PUT api/<RetweetController>/5
        [HttpPut("{id}")]
        public async Task<int> Put()
        {
            int result = await _retweetService.UpdateRetweet(null, null);
            if (result == 0)
            {
                throw new Exception("No entries updated.");
            }

            return result;
        }

        // DELETE api/<RetweetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
