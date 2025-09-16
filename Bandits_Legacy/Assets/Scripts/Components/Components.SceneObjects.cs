using Entitas;
using UnityEngine;

[Game, Events]
public sealed class PlayerComponent : IComponent
{
}

[Game, Events]
public sealed class EnemyComponent : IComponent
{
}

[Game, Events]
public sealed class PowerUpComponent : IComponent
{
}

public sealed class SceneViewComponent : IComponent
{
    public GameObject Value;
}

[Game, Events]
public sealed class PositionComponent : IComponent
{
    public Vector2 Value;
}