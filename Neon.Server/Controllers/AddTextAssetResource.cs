using Neon.Server.Models;

namespace Neon.Server.Controllers {
    public class AddTextAssetResource : AddAssetResource {

        public string Text { get; }
        public AddTextAssetResource(string name, EAssetType type, string contextName, string text) : base(name, type, contextName)
            => Text = text;
    }
}