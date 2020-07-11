using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Neon.Server.Commands;

namespace Neon.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ImageAssetsController : ControllerBase {

        private readonly ILogger<ImageAssetsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ImageAssetsController(ILogger<ImageAssetsController> logger, IMediator mediator, IMapper mapper)
            => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

        [HttpGet]
        public IEnumerable<ImageAssetResource> Get() {
            return Enumerable.Empty<ImageAssetResource>();
        }

        [HttpPost]
        public async Task<ActionResult<ImageAssetResource>> Add([FromForm] AddImageAssetResource addResource) {
            Stream stream = null;
            try {
                stream = addResource.Image.OpenReadStream();
                var command = new AddImageAssetCommand.Input(addResource.Name, addResource.ContextName, 15, stream);
                var result = await _mediator.Send(command);
                return Ok(_mapper.Map<ImageAssetResource>(result));
            } catch (Exception ex) {
                return BadRequest(ex);
            } finally {
                if (stream != null)
                    stream.Close();
            }
        }
    }
}