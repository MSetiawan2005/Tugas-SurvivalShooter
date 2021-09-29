namespace Command
{
    public class ShootCommand : Command
    {
        private PlayerShooting _playerShooting;

        public ShootCommand(PlayerShooting playerShooting)
        {
            _playerShooting = playerShooting;
        }

        public override void Execute()
        {
            _playerShooting.Shoot();
        }

        public override void UnExecute()
        {
        }
    }
}