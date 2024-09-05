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
        _level.LevelMover.SetBattleSpeedModificator();

        _level.PlayerView.IsShooting = true;

        if (_level.CurrentBattle.Enemy.IsLeftSide)
        {
            _level.PlayerView.IsStrafingRight = true;
            return;
        }

        _level.PlayerView.IsStrafingLeft = true;
    }
}