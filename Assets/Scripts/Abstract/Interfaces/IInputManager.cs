
using System;

internal interface IInputManager
{
    public event Action FireButtonClickEvent;
    public float HorizontalDirection { get; }
}
