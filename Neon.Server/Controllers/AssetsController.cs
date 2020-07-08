using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neon.Server.Models;
using Neon.Server.Commands;
using MediatR;
using Neon.Server.Extensions;

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
            //return Enumerable.Empty<Asset>();
            return new List<Asset> {
                new Asset("Ausbildungskalender", EAssetType.WebsiteUrl, new AssetContext("TD"), new WebisteUrlContent("https://ddf783hjs.europe-west1.aws.com"))
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddAssetResource addAssetResource) {
            try {
                WebsiteUrlAsset wua;
                if (addAssetResource.Type == EAssetType.WebsiteUrl) {
                    wua = addAssetResource.Content.ToObject<WebsiteUrlAsset>();
                }

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
