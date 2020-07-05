using System;
using System.Threading;
using System.Threading.Tasks;
using Neon.Server.Models;
using MediatR;

namespace Neon.Server.Commands {
    public class DeleteAssetContextCommand : IRequestHandler<DeleteAssetContextCommand.Input, AssetContext> {
        public class Input : IRequest<AssetContext> {
            public string Name { get; }

            public Input(string name) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }
        }


        public Task<AssetContext> Handle(Input input, CancellationToken cancellationToken) {
            // TODO: Logic, Actions, CRUD, etc.
            
            throw new NotImplementedException();
        }
    }
}