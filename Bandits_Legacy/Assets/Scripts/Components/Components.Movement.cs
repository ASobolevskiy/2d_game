using Entitas;
using UnityEngine;

[Game]
public sealed class DirectionComponent : IComponent
{
    public Vector2 Value;
}

[Game]
public sealed class SpeedComponent : IComponent
{
    public float Value;
}

[Game]
public sealed class ChasingSpeedComponent : IComponent
{
    public float Value;
}

public sealed class PatrollingSpeedComponent : IComponent
{
    public float Value;
}

[Game]
public sealed class MovableComponent : IComponent
{
}

[Game]
public sealed class AbleToMove : IComponent
{
}

[Game]
public sealed class IsMovingComponent : IComponent
{
    public bool Value;
}