using Entitas;
using Utils;

[Game, Events]
public sealed class CollectibleTypeComponent : IComponent
{
    public CollectibleTypeEnum Value;
}

[Game, Events]
public sealed class CollectibleValueComponent : IComponent
{
    public int Amount;
}