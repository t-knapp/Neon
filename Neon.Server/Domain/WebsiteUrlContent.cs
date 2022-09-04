using System;

namespace Neon.Domain {
    public class WebisteUrlContent : AssetContent {
        public string Url { get; }

        public WebisteUrlContent(string url)
            => Url = url ?? throw new ArgumentNullException(nameof(url));
    }
}