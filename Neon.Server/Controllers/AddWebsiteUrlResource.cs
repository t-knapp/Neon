using Neon.Server.Models;

namespace Neon.Server.Controllers {
    public class AddWebsiteUrlResource : AddAssetResource {

        public string Url { get; }
        public AddWebsiteUrlResource(string name, EAssetType type, string contextName, string url) : base(name, type, contextName)
            => Url = url;
    }
}