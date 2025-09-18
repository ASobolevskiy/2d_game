using Entitas;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Input
{
    public class EmitInputSystem : IInitializeSystem ,IExecuteSystem, ICleanupSystem
    {
        private InputAction _moveAction;
        
        public void Initialize()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;
        }
        
        public void Execute()
        {
            //Movement
            //Get x value, apply move direction to keyboard movables (player)
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            
        }

        public void Cleanup()
        {
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMove;
        }
    }
}