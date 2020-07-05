using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Neon.Server.Models;

namespace Neon.Server.Commands
{
    public class DeleteAssetCommand : IRequestHandler<DeleteAssetCommand.Input, Asset> {
        public class Input : IRequest<Asset> {
            public string Name { get; }

            public Input(string name) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }
        }

        public Task<Asset> Handle(Input input, CancellationToken cancellationToken) {
            // TODO: Logic, Actions, CRUD, etc.

            throw new NotImplementedException();
        }
    }
}