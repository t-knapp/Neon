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
        return Ok(await _mediator.Send(new GetImageAssetQuery(id)));
    }

    [HttpGet]
    [Route("{id}/thumbnail")]
    public async Task<IActionResult> GetThumbnail(Guid id) {
        try {
            var query = new GetImageAssetThumbnailQuery(id);
            var imageFile = await _mediator.Send(query);
            return File(imageFile.Data, imageFile.MimeType);
        } catch (ArgumentException ex) {
            _logger.LogError(ex, $"Asset '{id}' not found.");
            return NotFound();
        } catch (Exception ex) {
            _logger.LogError(ex, $"Cannot get image asset content '{id}'.");
            return NoContent();
        }
    }

    [HttpGet]
    [Route("{id}/content")]
    public async Task<IActionResult> GetContent(Guid id) {
        try {
            var query = new GetImageAssetContentQuery(id);
            var imageFile = await _mediator.Send(query);
            return File(imageFile.Data, imageFile.MimeType);
        } catch (ArgumentException ex) {
            _logger.LogError(ex, $"Asset '{id}' not found.");
            return NotFound();
        } catch (Exception ex) {
            _logger.LogError(ex, $"Cannot get image asset content '{id}'.");
            return NoContent();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddImageAssetResource addResource) {
        Stream stream = null;
        try {
            stream = addResource.Image.OpenReadStream();
            var command = new AddImageAssetCommand(
                addResource.Name,
                addResource.DisplayTime,
                addResource.IsActive,
                addResource.Order,
                stream,
                addResource.Image.ContentType,
                addResource.NotBefore,
                addResource.NotAfter
            );
            return Ok(await _mediator.Send(command));
        } catch (Exception ex) {
            _logger.LogError(ex, "Cannot add image asset.");
            return BadRequest(ex);
        } finally {
            if (stream != null)
                stream.Close();
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<ImageAssetDTO>> Update(Guid id, [FromBody] UpdateImageAssetResource resource) {
        try {
            var command = new UpdateImageAssetCommand(
                id,
                resource.Name,
                resource.DisplayTime,
                resource.IsActive,
                resource.Order,
                resource.NotBefore,
                resource.NotAfter
            );
            return Ok(await _mediator.Send(command));
        } catch (Exception ex) {
            _logger.LogError(ex, $"Cannot update image asset '{id}'.");
            return null;
        }
    }
}