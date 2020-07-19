using System;
using Microsoft.AspNetCore.Http;

namespace Neon.Server.Controllers {
    public class AddImageAssetResource {
        public string Name { get; set; }
        public string ContextName { get; set; }
        public int DisplayTime { get; set; }
        public IFormFile Image { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
    }
}