using System;
using Neon.Domain;

namespace Neon.Application;

public class HtmlAssetResource : IMapFrom<HtmlAsset> {
    public string Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public int DisplayTime { get; set; }
    public string HtmlContent { get; set; }
    public bool IsActive { get; set; }
    public DateTime? NotBefore { get; set; }
    public DateTime? NotAfter { get; set; }
}