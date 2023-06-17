namespace Domain.Entities;

public class GameUnit : EntityBase
{
    private GameUnit()
    {
        
    }
    
    public float Health { get; set; }
    
    public float MovementSpeed { get; set; }

    public static GameUnit Create()
    {
        return new GameUnit
        {
            Health = 100f,
            MovementSpeed = 7f
        };
    }
}