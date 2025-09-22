using System.Collections.Generic;
using Entitas;
using UnityEngine;

[Game]
public sealed class AIAgentComponent : IComponent
{
}

[Game]
public sealed class PatrolStateComponent : IComponent
{
}

[Game]
public sealed class PatrolDataComponent : IComponent
{
    public int CurrentTargetIndex;
    public List<Vector2> PatrolPoints;
}

[Game]
public sealed class SightDistanceComponent : IComponent
{
    public float Value;
}

[Game]
public sealed class EnemySightedComponent : IComponent
{
    public GameEntity Enemy;
}

[Game]
public sealed class ChaseStateComponent : IComponent
{
    public GameEntity Target;
}

[Game]
public sealed class IdleStateComponent : IComponent
{
}

[Game]
public sealed class IdleStateTurnAroundTimerComponent : IComponent
{
    public float Value;
}

[Game]
public sealed class CurrentTurnAroundTimerComponent : IComponent
{
    public float Value;
}

[Game]
public sealed class AttackStateComponent : IComponent
{
}

[Game]
public sealed class SearchingForEnemy : IComponent
{
}

[Game]
public sealed class PeaceTimerComponent : IComponent
{
    public float Value;
}

public sealed class CurrentPeaceTimerComponent : IComponent
{
    public float Value;
}