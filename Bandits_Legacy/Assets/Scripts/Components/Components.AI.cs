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
public sealed class PatrolData : IComponent
{
    public int CurrentTargetIndex;
    public List<Vector2> PatrolPoints;
}