using System;

namespace Neon.Domain {
    public class Asset : Entity {
        public string Name { get; set; }
        public int DisplayTime { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public EAssetType Type { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
    }
}