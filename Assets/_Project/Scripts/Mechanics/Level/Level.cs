public class Level
{
    public Battle CurrentBattle { get; set; }
    public StateMachine StateMachine { get; private set; }
    public CameraController CameraController { get; private set; }
    public LevelMover Mover { get; private set; }
    public PlayerCharacterView PlayerView { get; private set; }

    public Level(CameraController cameraController, LevelMover mover, PlayerCharacterView playerView)
    {
        CameraController = cameraController;
        Mover = mover;
        PlayerView = playerView;

        StateMachine = new();
        StateMachine.AddState(new IdleLevelState(StateMachine, this));
        StateMachine.AddState(new BattleLevelState(StateMachine, this));
        StateMachine.SetState<IdleLevelState>();
    }
}