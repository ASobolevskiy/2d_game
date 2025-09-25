using Entitas;
using Entitas.CodeGeneration.Attributes;

[GameCycle, Unique]
public sealed class PauseComponent : IComponent
{
    public bool PauseRequired;
}