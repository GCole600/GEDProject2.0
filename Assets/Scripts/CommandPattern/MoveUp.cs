namespace CommandPattern
{
    public class MoveUp : Command
    {
        private CharacterController _controller;
        
        public MoveUp (CharacterController controller)
        {
            _controller = controller;
        }
        
        public override void Execute()
        {
            _controller.Move(CharacterController.Direction.Up);
        }
    }
}
