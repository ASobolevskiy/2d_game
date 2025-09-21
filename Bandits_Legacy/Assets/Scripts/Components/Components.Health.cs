using Entitas;

[Game]
public sealed class HitPointsComponent : IComponent
{
    public int Value;
}

[Game]
public sealed class DeadComponent : IComponent
{
}

[Game]
public sealed class TakeDamageComponent : IComponent
{
    public int Amount;
}