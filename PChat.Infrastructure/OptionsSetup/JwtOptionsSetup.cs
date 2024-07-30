using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PChat.Infrastructure.Authenticaton;

namespace PChat.Infrastructure.OptionsSetup;

public sealed class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {
        configuration.GetSection("Jwt").Bind(options);
    }
}
