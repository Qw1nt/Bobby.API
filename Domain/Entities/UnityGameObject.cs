namespace Domain.Entities;

public class UnityGameObject : EntityBase
{
    public int IdInUnity { get; set; }

    public string Name { get; set; } = null!;
}