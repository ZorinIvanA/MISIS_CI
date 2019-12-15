using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MISIS.CI.API.BusinessLogic.Interfaces;
using MISIS.CI.Storage.API.Models;
using OpenTracing;

namespace MISIS.CI.Storage.API.Controllers
{
    [Route("api/storage")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageLogic _storageLogic;
        private readonly ITracer _tracer;
        private readonly ILogger<StorageController> _logger;

        public StorageController(IStorageLogic storageLogic,
            ITracer tracer, ILogger<StorageController> logger)
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
            var builder = _tracer.BuildSpan("GetStorageData");
            using (var scope = builder.StartActive(true))
            {
                try
                {
                    var span = scope.Span;
                    var log = $"Getting data with params {model}";
                    span.Log(log);

                    await _storageLogic.SaveToFileAsync(model, cancellationToken);
                    return Ok();
                }

                catch (Exception e)
                {
                    _logger.LogError(e, "Error");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        e.Message);
                }
            }
        }
    }
}