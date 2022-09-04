using System;

namespace Neon.Server.Controllers {
    public class ImageAssetResource {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int DisplayTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
    }
}