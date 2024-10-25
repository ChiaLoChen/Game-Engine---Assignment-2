



public class Shoot : Command
{
    private PlayerMovement _player;

    public Shoot(PlayerMovement player)
    {
        _player = player;
    }
    public override void Execute()
    {
        _player.Shoot();
    }
}
