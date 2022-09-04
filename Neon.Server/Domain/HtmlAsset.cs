using System;

namespace Neon.Domain {
    public class HtmlAsset {
        public string Name { get; set; }
        public int DisplayTime { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public EAssetType Type { get; set; } = EAssetType.TextHtml;
        public string Content { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }

        public HtmlAsset(string name, int displayTime, bool isActive, int order, string content, DateTime? notBefore, DateTime? notAfter) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DisplayTime = displayTime;
            IsActive = isActive;
            Order = order;
            Content = content ?? throw new ArgumentException(nameof(content));
            NotBefore = notBefore;
            NotAfter = notAfter;
        }
    }
}