using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Neon.Application;

namespace Neon.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class HtmlAssetsController : ControllerBase {
    private readonly ILogger<HtmlAssetsController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public HtmlAssetsController(ILogger<HtmlAssetsController> logger, IMediator mediator, IMapper mapper)
        => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HtmlAssetInfoDTO>>> List() {
        try {
            return Ok(await _mediator.Send(new GetHtmlAssetListQuery()));
        } catch (Exception ex) {
            _logger.LogError(ex, "Cannot list html assets.");
            return null;
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<HtmlAssetDTO>> Get(Guid id) {
        try {
            var asset = await _mediator.Send(new GetHtmlAssetQuery(id));
            return Ok(asset);
        } catch (ArgumentException ex) {
            _logger.LogError(ex, $"Asset '{id}' not found.");
            return NotFound();
        }
         catch (Exception ex) {
            _logger.LogError(ex, $"Cannot get html asset '{id}'.");
            return NoContent();
        }
    }

    [HttpGet]
    [Route("{id}/content")]
    public async Task<ActionResult<string>> GetHtml(Guid id) {
        try {
            return Ok(await _mediator.Send(new GetHtmlAssetContentQuery(id)));
        } catch (ArgumentException ex) {
            _logger.LogError(ex, $"Asset '{id}' not found.");
            return NotFound();
        } catch (Exception ex) {
            _logger.LogError(ex, $"Cannot get html asset content '{id}'.");
            return NoContent();
        }
    }

    [HttpPost]
    public async Task<ActionResult<HtmlAssetDTO>> Add([FromForm] AddHtmlAssetResource addHtmlAssetResource) {
        try {
            var command = new AddHtmlAssetCommand(
                addHtmlAssetResource.Name,
                addHtmlAssetResource.DisplayTime,
                addHtmlAssetResource.IsActive,
                addHtmlAssetResource.Order,
                addHtmlAssetResource.Content,
                addHtmlAssetResource.NotBefore,
                addHtmlAssetResource.NotAfter
            );
            return Ok(await _mediator.Send(command));
        } catch (Exception ex) {
            _logger.LogError(ex, $"Cannot add html asset");
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<HtmlAssetDTO>> Update(Guid id, UpdateHtmlAssetResource resource) {
        try {
            var command = new UpdateHtmlAssetCommand(
                id,
                resource.Name,
                resource.DisplayTime,
                resource.IsActive,
                resource.Order,
                resource.NotBefore,
                resource.NotAfter,
                resource.Content
            );
            return Ok(await _mediator.Send(command));
        } catch (Exception ex) {
            _logger.LogError(ex, $"Cannot update html asset '{id}'.");
            return null;
        }
    }
}