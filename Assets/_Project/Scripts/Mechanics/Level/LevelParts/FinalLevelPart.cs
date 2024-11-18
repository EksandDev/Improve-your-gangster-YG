using UnityEngine;

public class FinalLevelPart : LevelPart
{
    [SerializeField] private FinishTrigger _finishTrigger;

    public FinishTrigger FinishTrigger => _finishTrigger;
}