using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Neon.Server.Models;

namespace Neon.Server.Commands
{
    public class AddAssetCommand : IRequestHandler<AddAssetCommand.Input, Asset> {
        public class Input : IRequest<Asset> {

            public string Name { get; }
            public EAssetType Type { get; }
            public string ContextName { get; }

            public Input(string name, EAssetType type, string contextName) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Type = type;
                ContextName = contextName ?? throw new ArgumentNullException(nameof(contextName));
            }
        }

        public Task<Asset> Handle(Input request, CancellationToken cancellationToken) {
            // TODO: Logic, Actions, CRUD, etc.

            return Task.FromResult(new Asset(request.Name, request.Type, new AssetContext(request.ContextName)));
        }
    }
}
