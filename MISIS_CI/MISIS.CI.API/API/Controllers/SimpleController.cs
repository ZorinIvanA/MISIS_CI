using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISIS.CI.API.BusinessLogic.Interfaces;

namespace MISIS.CI.API.API.Controllers
{
    [Route("api/simple")]
    [ApiController]
    public class SimpleController : ControllerBase
    {
        private readonly ISimpleLogic _logic;

        public SimpleController(ISimpleLogic logic)
        {
            _logic = logic ?? throw new ArgumentNullException(nameof(logic));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]float lat,
            [FromQuery]float lon, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _logic.GetSimpleForecastAsync(lat, lon, cancellationToken));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}