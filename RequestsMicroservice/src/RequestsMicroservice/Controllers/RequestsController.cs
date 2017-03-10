using Microsoft.AspNetCore.Mvc;
using RequestsMicroservice.Database;
using System;
using System.Threading.Tasks;
using Request = RequestsMicroservice.Contracts.Requests.Request;

namespace RequestsMicroservice.Controllers
{
    [Route("api/requests")]
    public class RequestsController : Controller
    {
        private readonly RequestsDbContext _db;

        public RequestsController(RequestsDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            _db = db;
        }

        [HttpGet]
        [Route("{requestId:guid}", Name = nameof(RouteNames.GetRequestById))]
        [Produces(typeof(Request))]
        public async Task<IActionResult> GetById([FromRoute] Guid requestId)
        {
            IActionResult result;

            var request = await _db.Requests.FindAsync(requestId);

            if (request != null)
            {
                result = Ok(new Request
                {
                    RequestId = request.RequestId,
                    Index = request.Index,
                    X = request.X,
                    Created = request.Created
                });
            }
            else
            {
                result = NotFound();
            }
            
            return result;
        }
    }
}