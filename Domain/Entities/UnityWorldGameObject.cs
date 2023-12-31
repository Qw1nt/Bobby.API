﻿namespace Domain.Entities;

public class UnityWorldGameObject : EntityBase
{
    public UnityGameObject UnityReference { get; set; } = null!;

    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public float PositionZ { get; set; }   
    
    public float RotationX { get; set; }

    public float RotationY { get; set; }

    public float RotationZ { get; set; }
    
    public float Scale { get; set; }
}