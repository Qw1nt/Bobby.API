namespace Application.UnityWorlds.Commands.MockSave;

public class RequestUnityWorldGameObject
{
    public int UnityGameObjectId { get; set; }
    
    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public float PositionZ { get; set; }   
    
    public float RotationX { get; set; }

    public float RotationY { get; set; }

    public float RotationZ { get; set; }
    
    public float Scale { get; set; }
}