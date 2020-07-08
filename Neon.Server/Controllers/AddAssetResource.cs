using Neon.Server.Models;

namespace Neon.Server.Controllers {
    public abstract class AddAssetResource {
        public string Name { get; set; }
        public EAssetType Type { get; set; }
        public string ContextName { get; set; }

        protected AddAssetResource(string name, EAssetType type, string contextName) {
            Name = name;
            Type = type;
            ContextName = contextName;
        }
    }
}