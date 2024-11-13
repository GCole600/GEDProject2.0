using UnityEngine;

namespace CommandPattern
{
    public class MoveLeft : Command
    {
        private CharacterController _controller;
        
        public MoveLeft (CharacterController controller)
        {
            _controller = controller;
        }
        
        public override void Execute()
        {
            _controller.Move(CharacterController.Direction.Left);
        }
    }
}