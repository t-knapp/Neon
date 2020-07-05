using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neon.Server.Models;
using Neon.Server.Commands;
using MediatR;

namespace Neon.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly ILogger<AssetsController> _logger;
        private readonly IMediator _mediator;

        public AssetsController(ILogger<AssetsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            return Enumerable.Empty<Asset>();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddAssetResource addAssetResource) {
            try {
                var input = new AddAssetCommand.Input(addAssetResource.Name, addAssetResource.Type, addAssetResource.ContextName);
                var asset = await _mediator.Send(input);
                return Ok(asset);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteAssetResource deleteAssetResource) {
            try {
                var input = new DeleteAssetCommand.Input(deleteAssetResource.Name);
                var asset = await _mediator.Send(input);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
