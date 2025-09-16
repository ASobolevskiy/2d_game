using Core;
using Entitas;

[Events]
public sealed class PlayerSpawnRequestedComponent : IComponent
{
}

[Events]
public sealed class PlayerPrefabComponent : IComponent
{
    public Player Value;
}

[Events]
public sealed class EnemyPrefabComponent : IComponent
{
}

[Events]
public sealed class PowerUpPrefabComponent : IComponent
{
}
