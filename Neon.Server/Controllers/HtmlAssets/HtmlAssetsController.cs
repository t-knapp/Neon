using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neon.Server.Commands;
using AutoMapper;
using MediatR;
using Neon.Server.Models;

namespace Neon.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HtmlAssetsController : ControllerBase {
        private readonly ILogger<HtmlAssetsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HtmlAssetsController(ILogger<HtmlAssetsController> logger, IMediator mediator, IMapper mapper)
            => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HtmlAssetResource>>> List() {
            try {
                var query = new ListHtmlAssetsQuery.Input();
                var assets = await _mediator.Send(query);
                return Ok(_mapper.Map<IEnumerable<HtmlAsset>, List<HtmlAssetResource>>(assets.ToList()));
            } catch (Exception ex) {
                _logger.LogError(ex, "Cannot list html assets.");
                return null;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<HtmlAssetResource>> Get(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            try {
                var query = new HtmlAssetQuery.Input(id);
                var asset = await _mediator.Send(query);
                return Ok(_mapper.Map<HtmlAssetResource>(asset));
            } catch (ArgumentException ex) {
                _logger.LogError(ex, $"Asset '{id}' not found.");
                return NotFound();
            }
             catch (Exception ex) {
                _logger.LogError(ex, $"Cannot get html asset '{id}'.");
                return NoContent();
            }
        }

        public async Task<ActionResult<HtmlAssetResource>> Add([FromForm] AddHtmlAssetResource addHtmlAssetResource) {
            try {
                var result = await _mediator.Send(new AddHtmlAssetCommand.Input(
                    addHtmlAssetResource.Name,
                    addHtmlAssetResource.DisplayTime,
                    addHtmlAssetResource.IsActive,
                    addHtmlAssetResource.Order,
                    addHtmlAssetResource.Content,
                    addHtmlAssetResource.NotBefore,
                    addHtmlAssetResource.NotAfter
                ));
                return Ok(_mapper.Map<HtmlAssetResource>(result));
            } catch (Exception ex) {
                _logger.LogError(ex, $"Cannot add html asset");
                return BadRequest();
            }
        }
    }
}