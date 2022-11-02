using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MediatR;
using Neon.Domain;
using Neon.Application;

namespace Neon.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageAssetsController : ControllerBase {
    private readonly ILogger<ImageAssetsController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ImageAssetsController(ILogger<ImageAssetsController> logger, IMediator mediator, IMapper mapper)
        => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

    [HttpGet]
    public async Task<IActionResult> List() {
        return Ok(await _mediator.Send(new GetImageAssetInfosQuery()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id) {
        return Ok(await _mediator.Send(new GetImageAssetsQuery(id)));
    }
//
//    [HttpGet]
//    [Route("{id}/content")]
//    public async Task<IActionResult> GetImage(string id) {
//        try {
//            var query = new ImageAssetQuery.Input(id, true);
//            var tuple = await _mediator.Send(query); // TODO: Where and when is the stream closed?
//            await tuple.Item2.FlushAsync();
//            return File(tuple.Item2, tuple.Item1.ContentType ?? "application/octet-stream");
//        } catch (ArgumentException ex) {
//            _logger.LogError(ex, $"Asset '{id}' not found.");
//            return NotFound();
//        } catch (Exception ex) {
//            _logger.LogError(ex, $"Cannot get image asset content '{id}'.");
//            return NoContent();
//        }
//    }
//
    [HttpPost]
    public async Task<ActionResult<ImageAssetResource>> Add([FromForm] AddImageAssetResource addResource) {
        Stream stream = null;
        try {
            stream = addResource.Image.OpenReadStream();
            var command = new AddImageAssetCommand.Input(
                addResource.Name,
                addResource.DisplayTime,
                addResource.IsActive,
                addResource.Order,
                stream,
                addResource.Image.ContentType,
                addResource.NotBefore,
                addResource.NotAfter
            );
            var result = await _mediator.Send(command);
            return Ok(_mapper.Map<ImageAssetResource>(result));
        } catch (Exception ex) {
            _logger.LogError(ex, "Cannot add image asset.");
            return BadRequest(ex);
        } finally {
            if (stream != null)
                stream.Close();
        }
    }
//
//    [HttpPatch]
//    [Route("{id}")]
//    public async Task<ActionResult<ImageAssetResource>> Update(string id, [FromBody] JsonPatchDocument<UpdateImageAssetResource> patch) {
//        try {
//            var command = new UpdateImageAssetCommand.Input(id, _mapper.Map<JsonPatchDocument<ImageAsset>>(patch));
//            var result = await _mediator.Send(command);
//            return Ok(_mapper.Map<ImageAssetResource>(result));
//        } catch (Exception ex) {
//            _logger.LogError(ex, $"Cannot update image asset '{id}'.");
//            return null;
//        }
//    }
//
//    [HttpDelete]
//    [Route("{id}")]
//    public async Task<ActionResult<ImageAssetResource>> Delete(string id) {
//        if (string.IsNullOrEmpty(id))
//            return BadRequest();
//
//        try {
//            var command = new DeleteImageAssetCommand.Input(id);
//            var result = await _mediator.Send(command);
//            return Ok(_mapper.Map<ImageAssetResource>(result));
//        } catch (Exception ex) {
//            _logger.LogError(ex, $"Cannot delete image asset '{id}'.");
//            return null;
//        }
//    }
}