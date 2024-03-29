using System;
using Neon.Server.Models;

namespace Neon.Server.Controllers {
    public class DeleteAssetResource {

        public string Id { get; set; }
        public EAssetType Type { get; set; }
        public DeleteAssetResource(string id, EAssetType type) {
            Id = id;
            Type = type;
        }
    }
}