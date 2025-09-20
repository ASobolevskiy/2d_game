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
}

// [Game]
// public sealed class ReadyToAttackComponent : IComponent
// {
// }

[Game]
public sealed class AttackDelaying : IComponent
{
}

[Game]
public sealed class AttackDelayTimer : IComponent
{
    public float Value;
}