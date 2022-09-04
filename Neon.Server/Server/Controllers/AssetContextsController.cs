using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neon.Domain;
using Neon.Application;
using MediatR;

namespace Neon.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetContextsController : ControllerBase
{
    private readonly ILogger<AssetContextsController> _logger;
    private readonly IMediator _mediator;

    public AssetContextsController(ILogger<AssetContextsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public IEnumerable<AssetContext> Get()
    {
        return Enumerable.Empty<AssetContext>();
    }

//    [HttpPost]
//    public async Task<IActionResult> Post([FromBody] AddAssetContextResource addAssetContextResource) {
//        try {
//            var input = new AddAssetContextCommand.Input(addAssetContextResource.Name);
//            var asset = await _mediator.Send(input);
//            return Ok(asset);
//        } catch (Exception ex) {
//            return BadRequest(ex.Message);
//        }
//    }
//
//    [HttpDelete]
//    public async Task<IActionResult> Delete([FromBody] DeleteAssetContextResource deleteAssetContextResource) {
//        try {
//            var input = new DeleteAssetContextCommand.Input(deleteAssetContextResource.Name);
//            var asset = await _mediator.Send(input);
//            return Ok();
//        } catch (Exception ex) {
//            return BadRequest(ex.Message);
//        }
//    }
}
