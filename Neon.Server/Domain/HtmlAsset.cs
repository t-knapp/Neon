using System;

namespace Neon.Domain;

public class HtmlAsset : Asset {
    public string HtmlContent { get; set; }

    public HtmlAsset(string name, int displayTime, bool isActive, int order, string htmlContent, DateTime? notBefore, DateTime? notAfter) {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DisplayTime = displayTime;
        IsActive = isActive;
        Order = order;
        NotBefore = notBefore;
        NotAfter = notAfter;
        
        Type = EAssetType.TextHtml;
        HtmlContent = htmlContent ?? throw new ArgumentException(nameof(htmlContent));
    }
}