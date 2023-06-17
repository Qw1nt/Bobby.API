namespace Domain.Entities;

public class UnityWorld : EntityBase
{
    public string Name { get; set; } = null!;
    
    public DateTime DateOfCreation { get; set; }
    
    public List<UnityWorldGameObject> Objects { get; set; } = null!;
    
    public int SceneIndex { get; set; }
}