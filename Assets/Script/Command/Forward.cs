



public class Forward : Command
{
    private PlayerMovement _player;

    public Forward(PlayerMovement player)
    {
        _player = player;
    }
    public override void Execute()
    {
        _player.MoveForward();
    }
}
