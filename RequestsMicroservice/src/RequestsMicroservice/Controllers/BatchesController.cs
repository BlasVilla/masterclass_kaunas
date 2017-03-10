using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestsMicroservice.Contracts.Batches;
using RequestsMicroservice.Database;
using BatchDto = RequestsMicroservice.Contracts.Batches.Batch;
using BatchPoco = RequestsMicroservice.Database.Batch;

namespace RequestsMicroservice.Controllers
{
    [Route("api/batches")]
    public class BatchesController : Controller
    {
        private readonly RequestsDbContext _db;

        public BatchesController(RequestsDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("", Name = nameof(RouteNames.GetAllBatches))]
        public async Task<BatchDto[]> GetAll()
        {
            var batches = await _db.Batches.ToArrayAsync();

            return batches?
                .Select(b => new BatchDto
                {
                    BatchId = b.BatchId,
                    Description = b.Description,
                    Created = b.Created
                })
                .ToArray();
        }

        [HttpGet]
        [Route("{batchId:guid}", Name = nameof(RouteNames.GetBatchById))]
        public async Task<BatchDto> GetById([FromRoute] Guid batchId)
        {
            var batch = await _db.Batches.FindAsync(batchId);
            
            return batch != null ?
                new BatchDto
                {
                    BatchId = batch.BatchId,
                    Description = batch.Description,
                    Created = batch.Created
                } : null;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(NewBatchResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Add([FromBody]NewBatch newBatch)
        {
            var poco = new BatchPoco {Description = newBatch.Description, Created = DateTime.UtcNow };

            _db.Batches.Add(poco);

            await _db.SaveChangesAsync();

            var uri = new Uri(Url.Link(nameof(RouteNames.GetBatchById),
                new { batchId = poco.BatchId }));

            return Created(uri, new NewBatchResponse { BatchId = poco.BatchId });
        }
        
        [HttpDelete]
        [Route("{batchId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid batchId)
        {
            var batch = await _db.Batches.FindAsync(batchId);

            if (batch != null)
            {
                _db.Batches.Remove(batch);

                await _db.SaveChangesAsync();
            }

            return Ok();
        }
    }
}