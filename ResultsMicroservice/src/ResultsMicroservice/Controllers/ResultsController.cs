using Microsoft.AspNetCore.Mvc;
using ResultsMicroservice.Contracts;
using ResultsMicroservice.Database;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using ResultDto = ResultsMicroservice.Contracts.Result;
using ResultPoco = ResultsMicroservice.Database.Result;

namespace ResultsMicroservice.Controllers
{
    [Route("api/requests/{requestId:guid}/result")]
    public class ResultsController : Controller
    {
        private readonly ResultsDbContext _db;

        public ResultsController(ResultsDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            _db = db;
        }
        
        [HttpGet]
        [Route("", Name = nameof(RouteNames.GetResultById))]
        [Produces(typeof(ResultDto))]
        public async Task<IActionResult> GetById([FromRoute] Guid requestId)
        {
            IActionResult result;

            var resultPoco = await _db.Results.FindAsync(requestId);

            if (resultPoco != null)
            {
                result = Ok(new ResultDto
                {
                    RequestId = resultPoco.RequestId,
                    Method = resultPoco.Method,
                    Value = resultPoco.Value,
                    Created = resultPoco.Create
                });
            }
            else
            {
                result = NotFound();
            }

            return result;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromRoute] Guid requestId, [FromBody]NewResult newResult)
        {
            var poco = new ResultPoco
            {
                RequestId = requestId,
                Method = newResult.Method,
                Value = newResult.Value,
                Create = DateTime.UtcNow
            };

            _db.Results.Add(poco);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbException exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);

                return BadRequest(ModelState);
            }


            var uri = new Uri(Url.Link(nameof(RouteNames.GetResultById),
                new { requestId = requestId }));

            return Created(uri, null);
        }
    }
}