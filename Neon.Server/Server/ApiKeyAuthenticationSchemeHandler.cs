using System.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Neon.Server;

public class ApiKeyAuthenticationHandlerOptions : AuthenticationSchemeOptions {
    public IEnumerable<ApiKeyAuthenticationOptions.ApiKey> ApiKeys { get; set; } = new List<ApiKeyAuthenticationOptions.ApiKey>();
}

public class ApiKeyAuthenticationSchemeHandler : AuthenticationHandler<ApiKeyAuthenticationHandlerOptions>
{
    public ApiKeyAuthenticationSchemeHandler(
        IOptionsMonitor<ApiKeyAuthenticationHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Read the token from request headers/cookies
        // Check that it's a valid session, depending on your implementation
        var requestApiKey = Context.Request.Query["api-key"];
        if(String.IsNullOrEmpty(requestApiKey))
            return AuthenticateResult.Fail("Api-Key is empty.");
        
        var apiKey = Options.ApiKeys.FirstOrDefault(key => key.Value == requestApiKey);
        if(apiKey is null)
            return AuthenticateResult.Fail("Api-Key not found.");
        
        var claims = apiKey.Roles.Select(role => new Claim(ClaimTypes.Role, role));
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
        return AuthenticateResult.Success(ticket);

        // If the token is missing or the session is invalid, return failure:
        // return AuthenticateResult.Fail("Authentication failed");
    }
}