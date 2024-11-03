using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private PlayerCharacterView _prefab;
    [SerializeField] private MultiLanguageString _name;
    [SerializeField] private int _cost;
    [SerializeField] private int _UpgradeCostUp;
    [SerializeField] private bool _isPurchasedOnStart;
    [SerializeField] private int _healthUpgradeMaxLevel;
    [SerializeField] private int _damageUpgradeMaxLevel;
    [SerializeField] private int _firingRateUpgradeMaxLevel;
    [SerializeField] private float[] _healthUpgradeLevelValues;
    [SerializeField] private float[] _damageUpgradeLevelValues;
    [SerializeField] private float[] _firingRateUpgradeLevelValues;

    public PlayerCharacterView Prefab => _prefab;
    public MultiLanguageString Name => _name;
    public int Cost => _cost;
    public int UpgradeCostUp => _UpgradeCostUp;
    public bool IsPurchasedOnStart => _isPurchasedOnStart;
    public int HealthUpgradeMaxLevel => _healthUpgradeMaxLevel;
    public int DamageUpgradeMaxLevel => _damageUpgradeMaxLevel;
    public int FiringRateUpgradeMaxLevel => _firingRateUpgradeMaxLevel;
    public float[] HealthUpgradeLevelValues => _healthUpgradeLevelValues;
    public float[] DamageUpgradeLevelValues => _damageUpgradeLevelValues;
    public float[] FiringRateUpgradeLevelValues => _firingRateUpgradeLevelValues;
}
