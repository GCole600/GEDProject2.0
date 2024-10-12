using UnityEngine;

namespace CommandPattern
{
    public class MoveRight : Command
    {
        private CharacterController _controller;
        
        public MoveRight (CharacterController controller)
        {
            _controller = controller;
        }
        
        public override void Execute()
        {
            _controller.Move(CharacterController.Direction.Right);
        }
    }
}
