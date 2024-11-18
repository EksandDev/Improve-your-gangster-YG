using System.Collections.Generic;
using UnityEngine;

public class DifficultCalculator
{
    private EnemyView[] _enemies;
    private List<EnemyView> _availableEnemies;

    public float EnemyDamageModifier { get; private set; } = 1;
    public float EnemyHealthModifier { get; private set; } = 1;
    public float EnemyFiringRateModifier { get; private set; } = 1;
    public int EnemyPoints { get; private set; }
    public IReadOnlyList<EnemyView> AvailableEnemies => _availableEnemies;

    public DifficultCalculator(EnemyView[] enemies)
    {
        _enemies = enemies;
    }

    public void Calculate(int currentLevel)
    {
        var currentLevelString = $"{currentLevel}";
        var dozens = CalculateDozens(currentLevelString);
        EnemyDamageModifier = Mathf.Clamp(currentLevel / 100 + dozens / 40, 1, 3);
        EnemyHealthModifier = Mathf.Clamp(currentLevel / 100 + dozens / 40, 1, 3);
        EnemyFiringRateModifier = Mathf.Clamp(currentLevel / 500 + dozens / 80, 1, 1.5f);
        _availableEnemies = new();

        for (int i = 0; i <= currentLevel; i++, i++, i++)
        {
            if (i > currentLevel)
                break;

            if (i == 0)
                continue;

            EnemyPoints++;
        }

        foreach (var enemy in _enemies)
        {
            if (enemy.Data.LevelToOpen > currentLevel)
                continue;

            _availableEnemies.Add(enemy);
        }
    }

    private int CalculateDozens(string currentLevelString)
    {
        if (currentLevelString.Length < 2)
            return 0;

        if (currentLevelString.Length == 2)
            return int.Parse(currentLevelString[0].ToString());

        if (currentLevelString.Length == 3)
            return int.Parse(currentLevelString[0].ToString()) + int.Parse(currentLevelString[1].ToString());

        return 100;
    }
}