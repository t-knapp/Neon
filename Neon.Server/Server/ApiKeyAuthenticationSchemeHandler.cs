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
    public string ReadOnlyKey { get; set; }
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
        var apiKey = Context.Request.Query["api-key"];
        if(String.IsNullOrEmpty(apiKey))
            return AuthenticateResult.Fail("Api-Key is empty.");
        
        if(!Options.ReadOnlyKey.Equals(apiKey))
            return AuthenticateResult.Fail("Api-Key not allowed.");
        
        // TODO: Use Role configured in handler-options
        var claims = new[] { new Claim(ClaimTypes.Role, "Reader") };
        // var claims = new[] { new Claim(ClaimTypes.Role, "Editor") };
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
        return AuthenticateResult.Success(ticket);

        // If the token is missing or the session is invalid, return failure:
        // return AuthenticateResult.Fail("Authentication failed");
    }
}