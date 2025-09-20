using Entitas;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Input
{
    public sealed class EmitInputSystem : IInitializeSystem , ICleanupSystem
    {
        private InputContext _context;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        
        public void Initialize()
        {
            _context = Contexts.sharedInstance.input;
            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
            _attackAction = InputSystem.actions.FindAction("Attack");
            SetupInputCallbacks();
        }

        private void SetupInputCallbacks()
        {
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;

            _jumpAction.performed += OnJumpPerformed;
            _jumpAction.canceled += OnJumpCancelled;

            _attackAction.performed += OnAttackPerformed;
            _attackAction.canceled += OnAttackCancelled;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            var inputValue = ctx.ReadValue<Vector2>();
            _context.ReplaceMoveInput(inputValue.normalized);
        }

        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            OnJump(true);
        }        
        
        private void OnJumpCancelled(InputAction.CallbackContext ctx)
        {
            OnJump(false);
        }
        
        private void OnJump(bool state)
        {
            _context.ReplaceJumpInput(state);
        }
        
        private void OnAttackPerformed(InputAction.CallbackContext ctx)
        {
            OnAttack(true);
        }

        private void OnAttackCancelled(InputAction.CallbackContext ctx)
        {
            OnAttack(false);
        }


        private void OnAttack(bool state)
        {
            _context.ReplaceAttackInput(state);
        }

        public void Cleanup()
        {
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMove;
            
            _jumpAction.performed -= OnJumpPerformed;
            _jumpAction.canceled -= OnJumpCancelled;
            
            _attackAction.performed -= OnAttackPerformed;
            _attackAction.canceled -= OnAttackCancelled;
        }
    }
}