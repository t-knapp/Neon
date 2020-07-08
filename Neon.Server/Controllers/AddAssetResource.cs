using System.Text.Json;
using Neon.Server.Models;

namespace Neon.Server.Controllers {
    public class AddAssetResource {
        public string Name { get; set; }
        public EAssetType Type { get; set; }
        public string ContextName { get; set; }
        public JsonElement Content { get; set; }

        public AddAssetResource() { }

        public AddAssetResource(string name, EAssetType type, string contextName) {
            Name = name;
            Type = type;
            ContextName = contextName;
        }
    }
}