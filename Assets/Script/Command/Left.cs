



public class Left : Command
{
    private PlayerMovement _player;

    public Left(PlayerMovement player)
    {
        _player = player;
    }
    public override void Execute()
    {
        _player.MoveLeft();
    }
}
