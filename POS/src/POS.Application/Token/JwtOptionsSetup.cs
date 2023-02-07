using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace POS.Application.Token;

public class JwtOptionsSetup : IConfigureOptions<JwtSettings>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtSettings options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
