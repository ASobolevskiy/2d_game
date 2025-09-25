using Entitas;
using Entitas.CodeGeneration.Attributes;

[UI, Unique]
public sealed class PlayerHitPointsComponent : IComponent
{
    public int Value;
}

[UI, Unique]
public sealed class MenuOpenRequested : IComponent
{
}