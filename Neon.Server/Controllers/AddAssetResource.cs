using Neon.Server.Models;

namespace Neon.Server.Controllers {
    public class AddAssetResource {
        public string Name { get; set; }
        public EAssetType Type { get; set; }
        public string ContextName { get; set; }
    }
}