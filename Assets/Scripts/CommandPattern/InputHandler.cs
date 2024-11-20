using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace CommandPattern
{
    public class InputHandler : MonoBehaviour
    {
        private Invoker _invoker;
        private CharacterController _characterController;
        private Command _buttonW, _buttonA, _buttonS, _buttonD;

        private PlayerInput _playerInput;
        private InputAction _w, _a, _s, _d;

        private void OnEnable()
        {
            _w = _playerInput.Player.W;
            _a = _playerInput.Player.A;
            _s = _playerInput.Player.S;
            _d = _playerInput.Player.D;
            
            _w.Enable();
            _a.Enable();
            _s.Enable();
            _d.Enable();

            _w.performed += MoveUp;
            _a.performed += MoveLeft;
            _s.performed += MoveDown;
            _d.performed += MoveRight;
        }

        private void OnDisable()
        {
            _w.Disable();
            _a.Disable();
            _s.Disable();
            _d.Disable();
        }

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void Start()
        {
            _invoker = gameObject.GetComponent<Invoker>();
            _characterController = FindObjectOfType<CharacterController>();
            _buttonW = new MoveUp(_characterController);
            _buttonA = new MoveLeft(_characterController);
            _buttonS = new MoveDown(_characterController);
            _buttonD = new MoveRight(_characterController);
        }

        private void MoveUp(InputAction.CallbackContext ctx)
        {
            _invoker.ExecuteCommand(_buttonW);
        }
        
        private void MoveLeft(InputAction.CallbackContext ctx)
        {
            _invoker.ExecuteCommand(_buttonA);
        }
        
        private void MoveDown(InputAction.CallbackContext ctx)
        {
            _invoker.ExecuteCommand(_buttonS);
        }
        
        private void MoveRight(InputAction.CallbackContext ctx)
        {
            _invoker.ExecuteCommand(_buttonD);
        }
    }
}