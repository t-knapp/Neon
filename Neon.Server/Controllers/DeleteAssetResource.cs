using System;

namespace Neon.Server.Controllers {
    public class DeleteAssetResource {
        public string Name { get; set; }

        public DeleteAssetResource(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}