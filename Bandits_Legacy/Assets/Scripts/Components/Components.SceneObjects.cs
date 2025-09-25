using Core;
using Entitas;
using UnityEngine;
using Utils;

[Game, Events]
public sealed class PlayerComponent : IComponent
{
}

[Game, Events]
public sealed class EnemyComponent : IComponent
{
}

[Game, Events]
public sealed class CollectibleComponent : IComponent
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

[Game]
public sealed class FaceDirectionComponent : IComponent
{
    public FaceDirectionEnum Value;
}

[Game]
public sealed class RigidBody2DComponent : IComponent
{
    public Rigidbody2D Value;
}

[Game]
public sealed class GroundSensorComponent : IComponent
{
    public GroundSensor Value;
}

[Game]
public sealed class MarkedToDestroy : IComponent
{
}

[Game]
public sealed class CollectibleTriggerEntered : IComponent
{
}

[Events]
public sealed class HealthCollectibleTriggered : IComponent
{
}

[Events]
public sealed class MoneyCollectibleTriggered : IComponent
{
}