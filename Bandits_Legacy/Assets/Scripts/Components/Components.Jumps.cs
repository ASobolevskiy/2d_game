using Entitas;

[Game]
public sealed class AbleToJumpComponent : IComponent
{
}

[Game]
public sealed class JumpForceComponent : IComponent
{
    public float Value;
}

[Game]
public sealed class GroundedComponent : IComponent
{
    public bool Value;
}

[Game]
public sealed class JumpRequestedComponent : IComponent
{
}