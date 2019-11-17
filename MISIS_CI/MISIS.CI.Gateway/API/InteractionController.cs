using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISIS.CI.Gateway.BusinessLogic.Interfaces;

namespace MISIS.CI.Gateway.API
{
    [Route("api/interaction")]
    public class InteractionController : ControllerBase
    {
        private readonly IInteractionLogic _interactionLogic;

        public InteractionController(IInteractionLogic interactionLogic)
        {
            _interactionLogic = interactionLogic;
        }

        [HttpPost("store-from-api")]
        public async Task<IActionResult> SaveForecast(
            [FromQuery]float lat, [FromQuery]float lon,
            CancellationToken cancellationToken)
        {
            try
            {
                await _interactionLogic.SaveForecastAsync(lat, lon, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}