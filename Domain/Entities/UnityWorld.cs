namespace Domain.Entities;

public class UnityWorld : EntityBase
{
    public string Name { get; set; } = null!;
    
    public DateTime DateOfCreation { get; set; }

    public List<UnityWorldGameObject> Objects { get; set; } = new();
    
    public int SceneIndex { get; set; }

    
    public float ExtractedResourcesCost { get; set; }
    
    public TimeSpan ExploitationTime { get; set; }
    
    public bool Active { get; set; }
}