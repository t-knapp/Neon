using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Neon.Server.Models;
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
        public async Task<ActionResult<IEnumerable<ImageAssetResource>>> List() {
            try {
                var query = new ListImageAssetsQuery.Input();
                var assets = await _mediator.Send(query);
                return Ok(_mapper.Map<IEnumerable<ImageAsset>, List<ImageAssetResource>>(assets.ToList()));
            } catch (Exception ex) {
                _logger.LogError(ex, "Cannot list image assets.");
                return null;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ImageAssetResource>> Get(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            try {
                var query = new ImageAssetQuery.Input(id);
                var tuple = await _mediator.Send(query);
                return Ok(_mapper.Map<ImageAssetResource>(tuple.Item1));
            } catch (ArgumentException ex) {
                _logger.LogError(ex, $"Asset '{id}' not found.");
                return NotFound();
            }
             catch (Exception ex) {
                _logger.LogError(ex, $"Cannot get image asset '{id}'.");
                return NoContent();
            }
        }

        [HttpGet]
        [Route("{id}/content")]
        public async Task<IActionResult> GetImage(string id) {
            try {
                var query = new ImageAssetQuery.Input(id, true);
                var tuple = await _mediator.Send(query); // TODO: Where and when is the stream closed?
                await tuple.Item2.FlushAsync();
                return File(tuple.Item2, tuple.Item1.ContentType ?? "application/octet-stream");
            } catch (ArgumentException ex) {
                _logger.LogError(ex, $"Asset '{id}' not found.");
                return NotFound();
            } catch (Exception ex) {
                _logger.LogError(ex, $"Cannot get image asset content '{id}'.");
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ImageAssetResource>> Add([FromForm] AddImageAssetResource addResource) {
            Stream stream = null;
            try {
                stream = addResource.Image.OpenReadStream(); // TODO: Where and when is the stream closed?
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

        [HttpPatch]
        public async Task<ActionResult<ImageAssetResource>> Update(UpdateImageAssetResource updateResource) {
            try {
                var command = new UpdateImageAssetCommand.Input(
                    updateResource.Id,
                    updateResource.Name,
                    updateResource.DisplayTime,
                    updateResource.IsActive,
                    updateResource.Order,
                    updateResource.NotBefore,
                    updateResource.NotAfter
                );
                var result = await _mediator.Send(command);
                return Ok(_mapper.Map<ImageAssetResource>(result));
            } catch (Exception ex) {
                _logger.LogError(ex, $"Cannot update image asset '{updateResource.Id}'.");
                return null;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ImageAssetResource>> Delete(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            try {
                var command = new DeleteImageAssetCommand.Input(id);
                var result = await _mediator.Send(command);
                return Ok(_mapper.Map<ImageAssetResource>(result));
            } catch (Exception ex) {
                _logger.LogError(ex, $"Cannot delete image asset '{id}'.");
                return null;
            }
        }
    }
}