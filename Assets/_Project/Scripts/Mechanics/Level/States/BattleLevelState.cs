public class BattleLevelState : State
{
    private Level _level;

    public BattleLevelState(StateMachine stateMachine, Level level) : base(stateMachine)
    {
        _level = level;
    }

    public override void Enter()
    {
        _level.CameraController.GoToBattleCameraPoint();
        _level.Mover.SetBattleSpeedModificator();
    }
}