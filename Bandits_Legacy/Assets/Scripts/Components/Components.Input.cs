using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input, Unique]
public sealed class MoveInputComponent : IComponent
{
    public Vector2 Value;
}

[Input, Unique]
public sealed class JumpInputComponent : IComponent
{
    public bool IsPressed;
}

[Input, Unique]
public sealed class AttackInputComponent : IComponent
{
    public bool IsPressed;
}

[Input, Unique]
public sealed class OpenMenuComponent : IComponent
{
    public bool IsPressed;
}
