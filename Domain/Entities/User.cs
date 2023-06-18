namespace Domain.Entities;

public class User : EntityBase
{
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public List<GameUnit> Units { get; set; } = new();
    
    public float MiningResourceAmount { get; set; }
}