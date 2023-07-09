using System.Collections.Generic;

namespace Neon.Server;

public class ApiKeyAuthenticationOptions {
    
    public class ApiKey {
        public string Value { get; set; }
        public string[] Roles { get; set; }
    }
    public ApiKey[] ApiKeys { get; set; }
}
