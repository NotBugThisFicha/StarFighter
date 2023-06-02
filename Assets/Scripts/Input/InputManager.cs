using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager : IInputManager, ITickable
    {
        public float HorizontalDirection { get; private set; }

        public event Action FireButtonClickEvent;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireButtonClickEvent?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.HorizontalDirection = 1;
            }
            else
            {
                this.HorizontalDirection = 0;
            }
        }
    }
}