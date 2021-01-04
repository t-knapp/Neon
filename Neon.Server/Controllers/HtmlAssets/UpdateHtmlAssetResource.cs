using System;

namespace Neon.Server.Controllers {
    public class UpdateHtmlAssetResource {
        public string Name { get; set; }
        public int DisplayTime { get; set; }
        public string Content { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
    }
}