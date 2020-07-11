using System;
using MongoDB.Entities;

namespace Neon.Server.Models {
    public class ImageAsset : FileEntity {
        public string Name { get; set; }
        public AssetContext Context { get; set; }
        public int DisplayTime { get; set; }
        public EAssetType Type { get; } = EAssetType.Image;

        public ImageAsset(string name, AssetContext context, int displayTime) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DisplayTime = displayTime;
        }
    }
}