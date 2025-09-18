using Core;
using Entitas;

[Events]
public sealed class PlayerSpawnRequestedComponent : IComponent
{
}

[Events]
public sealed class EnemySpawnRequestedComponent : IComponent
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
    public Enemy Value;
}

[Events]
public sealed class PowerUpPrefabComponent : IComponent
{
    public PowerUp Value;
}
