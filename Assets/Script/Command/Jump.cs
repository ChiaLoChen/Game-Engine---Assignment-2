



public class Jump : Command
{
    private PlayerMovement _player;

    public Jump(PlayerMovement player)
    {
        _player = player;
    }
    public override void Execute()
    {
        _player.Jump();
    }
}
