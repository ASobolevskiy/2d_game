using Entitas;

[Game]
public sealed class HitPointsComponent : IComponent
{
    public int Value;
}

[Game]
public sealed class MaxHitpointsComponent : IComponent
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

[Game]
public sealed class HealDamageComponent : IComponent
{
    public int Amount;
}