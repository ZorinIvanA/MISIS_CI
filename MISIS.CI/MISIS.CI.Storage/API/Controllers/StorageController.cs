using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISIS.CI.API.BusinessLogic.Interfaces;
using MISIS.CI.Storage.API.Models;

namespace MISIS.CI.Storage.API.Controllers
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private IStorageLogic _storageLogic;

        public StorageController(IStorageLogic storageLogic)
        {
            _storageLogic = storageLogic ?? throw new ArgumentNullException(nameof(storageLogic));
        }

        [HttpPost("save")]
        public async Task<IActionResult> PostAsync(
            [FromBody]WeatherForecastModel model, CancellationToken cancellationToken)
        {
            try
            {
                await _storageLogic.SaveToFileAsync(model, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.Message);
            }
        }
    }
}