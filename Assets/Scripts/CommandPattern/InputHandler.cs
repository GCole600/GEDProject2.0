using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace CommandPattern
{
    public class InputHandler : MonoBehaviour
    {
        private Invoker _invoker;
        private CharacterController _characterController;
        private Command _buttonW, _buttonA, _buttonS, _buttonD;

        void Start()
        {
            _invoker = gameObject.GetComponent<Invoker>();
            _characterController = FindObjectOfType<CharacterController>();
            _buttonW = new MoveUp(_characterController);
            _buttonA = new MoveLeft(_characterController);
            _buttonS = new MoveDown(_characterController);
            _buttonD = new MoveRight(_characterController);
        }
        
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.W))
                _invoker.ExecuteCommand(_buttonW);

            if (Input.GetKeyUp(KeyCode.A))
                _invoker.ExecuteCommand(_buttonA);

            if (Input.GetKeyUp(KeyCode.S))
                _invoker.ExecuteCommand(_buttonS);

            if (Input.GetKeyUp(KeyCode.D))
                _invoker.ExecuteCommand(_buttonD);
        }
    }
}