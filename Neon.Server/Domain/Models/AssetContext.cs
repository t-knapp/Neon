using System;

namespace Neon.Server.Models {
    public class AssetContext {
        public string Name { get; }

        public AssetContext(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}