using System;

namespace Neon.Server.Models {
    public class Asset {
        public string Name { get; }
        public EAssetType Type { get; }
        public AssetContext Context { get; }

        public Asset(string name, EAssetType type, AssetContext context) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}