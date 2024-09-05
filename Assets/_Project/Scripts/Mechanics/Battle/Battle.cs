using System.Collections.Generic;

public class Battle
{
    private List<BattlerModel> _battlers;

    public EnemyModel Enemy { get; private set; }

    public Battle(PlayerCharacterModel playerBattler, EnemyModel enemyBattler)
    {
        Enemy = enemyBattler;
        _battlers = new(2)
        {
            playerBattler,
            enemyBattler
        };

        playerBattler.StartBattle(enemyBattler);
        enemyBattler.StartBattle(playerBattler);
    }

    public void Stop()
    {
        foreach (var battler in _battlers)
            battler.StopBattle();
    }
}