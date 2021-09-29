namespace Command
{
    public class MoveCommand : Command
    {
        private PlayerMovement _playerMovement;
        private float _h, _v;

        public MoveCommand(PlayerMovement playerMovement, float h, float v)
        {
            _playerMovement = playerMovement;
            _h = h;
            _v = v;
        }

        public override void Execute()
        {
            _playerMovement.Move(_h, _v);
            _playerMovement.Animating(_h, _v);
        }

        public override void UnExecute()
        {
            _playerMovement.Move(-_h, -_v);
            _playerMovement.Animating(_h, _v);
        }
    }
}