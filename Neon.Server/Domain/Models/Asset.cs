using System;
using MongoDB.Entities;
using MongoDB.Entities.Core;

namespace Neon.Server.Models {
    public class Asset : Entity {
        public string Name { get; }
        public AssetContext Context { get; }
        public TimeSpan DisplayTime { get; }

        public EAssetType Type { get; }

        public Asset(string name, EAssetType type, AssetContext context, object content = null) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            // Content = content;
        }
    }

    public class WebsiteUrlAsset {
        public string Url { get; set; }

        public WebsiteUrlAsset() { }
        public WebsiteUrlAsset(string url)
            => Url = url;
    }
}