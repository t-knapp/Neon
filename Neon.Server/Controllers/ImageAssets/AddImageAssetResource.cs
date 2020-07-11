using System;
using Microsoft.AspNetCore.Http;

namespace Neon.Server.Controllers {
    public class AddImageAssetResource {
        public string Name { get; set; }
        public string ContextName { get; set; }
        public IFormFile Image { get; set; }
    }
}