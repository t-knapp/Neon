using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;
using Neon.Server.Commands;
using AutoMapper;
using MediatR;
using Neon.Server.Models;

namespace Neon.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AssetsController : ControllerBase {
        private readonly ILogger<AssetsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AssetsController(ILogger<AssetsController> logger, IMediator mediator, IMapper mapper)
            => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetResource>>> List() {
            try {
                var query = new AssetsListQuery.Input();
                var assets = await _mediator.Send(query);
                return Ok(_mapper.Map<IEnumerable<Asset>, List<AssetResource>>(assets.ToList()));
            } catch (Exception ex) {
                _logger.LogError(ex, "Cannot list assets.");
                return null;
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<AssetResource>> Update(string id, [FromBody] JsonPatchDocument<UpdateAssetResource> resource) {
            try {
                var command = new UpdateAssetCommand.Input(id, _mapper.Map<JsonPatchDocument<Asset>>(resource));
                var updatedAsset = await _mediator.Send(command);
                return Ok(_mapper.Map<AssetResource>(updatedAsset));
            } catch (Exception ex) {
                _logger.LogError(ex, "Cannot update asset.");
                return null;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<AssetResource>> Delete(string id) {
            try {
                var command = new DeleteAssetCommand.Input(id);
                var deletedAsset = await _mediator.Send(command);
                return Ok(_mapper.Map<AssetResource>(deletedAsset));
            } catch (Exception ex) {
                _logger.LogError(ex, "Cannot delete asset.");
                return null;
            }
        }
    }
}