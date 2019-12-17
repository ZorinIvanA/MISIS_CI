using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MISIS.CI.API.BusinessLogic.Interfaces;
using MISIS.CI.Storage.API.Models;

namespace MISIS.CI.Storage.API.Controllers
{
    [Route("api/storage")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageLogic _storageLogic;
        private readonly IManagedTracer _tracer;
        private readonly ILogger<StorageController> _logger;

        public StorageController(IStorageLogic storageLogic,
            IManagedTracer tracer, ILogger<StorageController> logger)
        {
            _storageLogic = storageLogic ?? throw new ArgumentNullException(nameof(storageLogic));
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("save")]
        public async Task<IActionResult> PostAsync(
            [FromBody]WeatherForecastModel model,
            CancellationToken cancellationToken)
        {
            using (_tracer.StartSpan("save"))
            {
                try
                {
                    //These two lines are for example
                    var traceHeaderHandler = new TraceHeaderPropagatingHandler(() => _tracer);
                    var response = TraceOutgoing(traceHeaderHandler);

                    await _storageLogic.SaveToFileAsync(model, cancellationToken);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Service Error, attention!!!!");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        e.Message);
                }
            }
        }

        public async Task<string> TraceOutgoing(TraceHeaderPropagatingHandler traceHeaderHandler)
        {
            // Add a handler to trace outgoing requests and to propagate the trace header.
            using (var httpClient = new HttpClient(traceHeaderHandler))
            {
                string url = "https://www.googleapis.com/discovery/v1/apis";
                using (var response = await httpClient.GetAsync(url))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}