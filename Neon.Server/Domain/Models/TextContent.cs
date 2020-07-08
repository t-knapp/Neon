using System;

namespace Neon.Server.Models {
    public class TextContent : AssetContent {
        public string Text { get; }

        public TextContent(string text)
            => Text = text ?? throw new ArgumentNullException(nameof(text));
    }
}