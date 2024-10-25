



public class Right : Command
{
    private PlayerMovement _player;

    public Right(PlayerMovement player)
    {
        _player = player;
    }
    public override void Execute()
    {
        _player.MoveRight();
    }
}
