using System;
using System.Collections.Generic;

public class Battle
{
    private List<BattlerModel> _battlers;

    public event Action Stopped;

    public Battle(PlayerCharacterModel playerBattler, EnemyModel enemyBattler)
    {
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

        Stopped?.Invoke();
    }
}