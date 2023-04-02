using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Neon.Application;
using AutoMapper;
using MediatR;
using Neon.Domain;

namespace Neon.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AssetsController : ControllerBase {

    private readonly ILogger<AssetsController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AssetsController(ILogger<AssetsController> logger, IMediator mediator, IMapper mapper)
        => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetDTO>>> List() {
        try {
            var assets = await _mediator.Send(new AssetListQuery());
            return Ok(_mapper.Map<IEnumerable<Asset>, List<AssetDTO>>(assets.ToList()));
        } catch (Exception ex) {
            _logger.LogError(ex, "Cannot list assets.");
            return null;
        }
    }

//    [HttpPatch]
//    [Route("{id}")]
//    public async Task<ActionResult<AssetResource>> Update(string id, [FromBody] JsonPatchDocument<UpdateAssetResource> resource) {
//        try {
//            var command = new UpdateAssetCommand.Input(id, _mapper.Map<JsonPatchDocument<Asset>>(resource));
//            var updatedAsset = await _mediator.Send(command);
//            return Ok(_mapper.Map<AssetResource>(updatedAsset));
//        } catch (Exception ex) {
//            _logger.LogError(ex, "Cannot update asset.");
//            return null;
//        }
//    }

   [HttpDelete]
   [Route("{id}")]
   public async Task<ActionResult<AssetDTO>> Delete(Guid id) {
       try {
           var deletedAsset = await _mediator.Send(new DeleteAssetCommand(id));
           return Ok(_mapper.Map<AssetDTO>(deletedAsset));
       } catch (Exception ex) {
           _logger.LogError(ex, "Cannot delete asset.");
           return null;
       }
   }

}