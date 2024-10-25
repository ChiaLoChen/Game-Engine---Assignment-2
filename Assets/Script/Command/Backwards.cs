



public class Backwards : Command
{
    private PlayerMovement _player;

    public Backwards(PlayerMovement player)
    {
        _player = player;
    }
    public override void Execute()
    {
        _player.MoveBackwards();
    }
}
