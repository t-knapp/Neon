using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Neon.Server.Models;

namespace Neon.Server.Commands
{
    public class AddAssetContextCommand : IRequestHandler<AddAssetContextCommand.Input, AssetContext> {
        public class Input : IRequest<AssetContext> {
            public string Name { get; }

            public Input(string name) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }
        }

        public Task<AssetContext> Handle(Input request, CancellationToken cancellationToken) {
            // TODO: Logic, Actions, CRUD, etc.

            throw new NotImplementedException();
        }
    }
}