using BlogAppAPI.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly string _username;
    private readonly string _password;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                      ILoggerFactory logger,
                                      UrlEncoder encoder,
                                      ISystemClock clock,
                                      IOptions<BasicAuthenticationOptions> authOptions)
        : base(options, logger, encoder, clock)
    {
        // Get username and password from the options
        _username = authOptions.Value.Username;
        _password = authOptions.Value.Password;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers["Authorization"].ToString();

        if (authorizationHeader != null && authorizationHeader.StartsWith("Basic ", StringComparison.InvariantCultureIgnoreCase))
        {
            var encodedCredentials = authorizationHeader.Substring(6).Trim();
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var credentials = decodedCredentials.Split(':');
            var username = credentials[0];
            var password = credentials[1];

            // Validate against configuration
            if (ValidateUserCredentials(username, password))
            {
                var claims = new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username) };
                var identity = new System.Security.Claims.ClaimsIdentity(claims, Scheme.Name);
                var principal = new System.Security.Claims.ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }

        return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
    }

    private bool ValidateUserCredentials(string username, string password)
    {
        // Validate the username and password from the appsettings.json
        return username == _username && password == _password;
    }
}
