public class LevelUIInitializer
{
    public LevelUIInitializer(SceneLoaderButton[] sceneLoaderButtons, LevelStatsCounterUI[] levelStatsCountersUI,
        SceneLoader sceneLoader, LevelStatsCounter levelStatsCounter, PlayerStats playerStats)
    {
        foreach (var button in sceneLoaderButtons)
            button.Initialize(sceneLoader);

        foreach (var levelStatsCounterUI in levelStatsCountersUI)
            levelStatsCounterUI.Initialize(levelStatsCounter, playerStats);
    }
}