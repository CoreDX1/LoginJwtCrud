namespace POS.Application.Token;

public class JwtSettings
{
    public string? Issuer { get; set; }
    public string? Secret { get; set; }
    public string? Expires { get; set; }
}
