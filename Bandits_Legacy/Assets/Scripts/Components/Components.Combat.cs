using Entitas;
using UnityEngine;

[Game]
public sealed class AttackRequested : IComponent
{
}

[Game]
public sealed class WeaponComponent : IComponent
{
    public Transform AttackPoint;
    public int Damage;
    public float Delay;
    public float Range;
}

[Game]
public sealed class AttackDelaying : IComponent
{
}

[Game]
public sealed class AttackDelayTimer : IComponent
{
    public float Value;
}

[Game]
public sealed class AttackingComponent : IComponent
{
    public bool IsAttacking;
}

[Game]
public sealed class AttackCollisionCheckComponent : IComponent
{
}