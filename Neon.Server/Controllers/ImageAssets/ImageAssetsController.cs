using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neon.Server.Models;
using Neon.Server.Commands;
using MediatR;

namespace Neon.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ImageAssetsController : ControllerBase {

        private readonly ILogger<ImageAssetsController> _logger;
        private readonly IMediator _mediator;

        public ImageAssetsController(ILogger<ImageAssetsController> logger, IMediator mediator)
            => (_logger, _mediator) = (logger, mediator);

        [HttpGet]
        public IEnumerable<ImageAsset> Get() {
            return Enumerable.Empty<ImageAsset>();
        }

        [HttpPost]
        public async Task<ActionResult<ImageAsset>> Add([FromForm] AddImageAssetResource addResource) {
            Stream stream = null;
            try {
                stream = addResource.Image.OpenReadStream();
                var command = new AddImageAssetCommand.Input(addResource.Name, addResource.ContextName, TimeSpan.FromSeconds(15), stream);
                var result = await _mediator.Send(command);

                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex);
            } finally {
                if (stream != null)
                    stream.Close();
            }
        }
    }
}