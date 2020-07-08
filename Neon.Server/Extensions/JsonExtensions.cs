using System;
using System.Text.Json;

namespace Neon.Server.Extensions {
    public static class JsonExtensions {
        public static T ToObject<T>(this JsonElement element)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}