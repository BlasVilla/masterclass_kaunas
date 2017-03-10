using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestsMicroservice.Contracts.Requests;
using RequestsMicroservice.Database;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RequestsMicroservice.Contracts.Messaging;
using RequestsMicroservice.MessageBroker;
using RequestDto = RequestsMicroservice.Contracts.Requests.Request;
using RequestPoco = RequestsMicroservice.Database.Request;

namespace RequestsMicroservice.Controllers
{
    [Route("api/batches/{batchId:guid}/requests")]
    public class RequestsByBatchController : Controller
    {
        private readonly RequestsDbContext _db;

        private readonly IMessagingService _messagingService;

        public RequestsByBatchController(RequestsDbContext db, IMessagingService messagingService)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            if (messagingService == null)
            {
                throw new ArgumentNullException(nameof(messagingService));
            }

            _db = db;
            _messagingService = messagingService;
        }

        [HttpGet]
        [Route("", Name = nameof(RouteNames.GetRequestsByBatchId))]
        public async Task<RequestDto[]> GetAll([FromRoute] Guid batchId)
        {
            var requests = await _db.Requests.Where(r => r.BatchId == batchId).ToArrayAsync();

            return requests?
                .Select(r => new RequestDto
                {
                    RequestId = r.RequestId,
                    Index = r.Index,
                    X = r.X,
                    Created = r.Created
                }).ToArray();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(NewRequestResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Add([FromRoute] Guid batchId, [FromBody]NewRequest newRequest)
        {
            var poco = new RequestPoco { BatchId = batchId, Index = newRequest.Index, X = newRequest.X, Created = DateTime.UtcNow };

            _db.Requests.Add(poco);

            await _db.SaveChangesAsync();

            var requestId = poco.RequestId;

            var message = new NewRequestMessage {RequestId = requestId};

            _messagingService.Send(message);

            var uri = new Uri(Url.Link(nameof(RouteNames.GetRequestById),
                new { requestId = requestId }));

            return Created(uri, new NewRequestResponse { RequestId = requestId });
        }
    }
}