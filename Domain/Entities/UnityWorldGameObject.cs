namespace Domain.Entities;

public class UnityWorldGameObject : EntityBase
{
    public int UnityGameObjectId { get; set; }
    
    public float X { get; set; }
    
    public float Y { get; set; }
    
    public float Z { get; set; }
}