public class IdleLevelState : State
{
    private Level _level;

    public IdleLevelState(StateMachine stateMachine, Level level) : base(stateMachine)
    {
        _level = level;
    }

    public override void Enter()
    {
        _level.CameraController.GoToIdleCameraPoint();
        _level.Mover.SetIdleSpeedModificator();

        if (_level.CurrentBattle == null)
            return;

        _level.CurrentBattle.Stop();
        _level.CurrentBattle = null;
    }
}
