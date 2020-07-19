using System;

namespace Neon.Server.Controllers {
    public class ImageAssetResource {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DisplayTime { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
    }
}