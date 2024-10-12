namespace CommandPattern
{
    public class MoveDown : Command
    {
        private CharacterController _controller;
        
        public MoveDown (CharacterController controller)
        {
            _controller = controller;
        }
        
        public override void Execute()
        {
            _controller.Move(CharacterController.Direction.Down);
        }
    }
}