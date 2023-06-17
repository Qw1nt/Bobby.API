namespace Domain.Common;

public class IdentityKeyValuePair
{
    public string AccessToken { get; set; } = null!;
    
    public string? RefreshToken { get; set; }
}