public class Level
{
    private StateMachine _stateMachine;

    public StateMachine StateMachine => _stateMachine;
    public Battle CurrentBattle { get; set; }
    public CameraController CameraController { get; private set; }
    public LevelMover Mover { get; private set; }
    public PlayerCharacterView PlayerView { get; private set; }

    public Level(CameraController cameraController, LevelMover mover, PlayerCharacterView playerView)
    {
        CameraController = cameraController;
        Mover = mover;
        PlayerView = playerView;

        _stateMachine = new();
        _stateMachine.AddState(new IdleLevelState(_stateMachine, this));
        _stateMachine.AddState(new BattleLevelState(_stateMachine, this));
        _stateMachine.SetState<IdleLevelState>();
    }
}