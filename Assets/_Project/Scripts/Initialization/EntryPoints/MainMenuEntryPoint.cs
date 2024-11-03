using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

public class MainMenuEntryPoint : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ChangeCharacterButton[] _changeCharacterButtons;
    [SerializeField] private ToBattleButton _toBattleButton;
    [SerializeField] private PurchaseButton _damageUpgradePurchaseButton;
    [SerializeField] private PurchaseButton _firingRateUpgradePurchaseButton;
    [SerializeField] private PurchaseButton _healthUpgradePurchaseButton;
    [SerializeField] private UpgradeUI _damageUpgradeUI;
    [SerializeField] private UpgradeUI _firingRateUpgradeUI;
    [SerializeField] private UpgradeUI _healthUpgradeUI;

    [Header("Characters")]
    [SerializeField] private CharacterData[] _characterData;
    [SerializeField] private AnimatorOverrideController _characterInMenuController;
    [SerializeField] private Transform _characterSpawnPoint;

    [Header("Other")]
    [SerializeField] private SceneContext _sceneContext;

    private Shop _shop;
    private UpgradeUIController _upgradeUIController;
    private PlayerStats _playerStats;
    private SceneLoader _sceneLoader;
    private DataForLevel _dataForLevel;
    private List<Character> _sellableCharacters;

    #region Zenject initialization
    [Inject]
    private void Construct(SceneLoader sceneLoader, DataForLevel dataForLevel)
    {
        _sceneLoader = sceneLoader;
        _dataForLevel = dataForLevel;
    }
    #endregion

    private void Start()
    {
        _sceneContext.Run();
        _playerStats = new();
        _playerStats.Money = 100000;
        InitializeCharacters();
        _upgradeUIController = new(_damageUpgradeUI, _healthUpgradeUI, _firingRateUpgradeUI,
            _damageUpgradePurchaseButton, _healthUpgradePurchaseButton, _firingRateUpgradePurchaseButton);
        _shop = new(_playerStats, _sellableCharacters, _upgradeUIController);
        InitializeButtons();

        for (int i = 0; i < _sellableCharacters.Count - 1; i++ )
        {
            if (i != _shop.CurrentCharacterIndex)
                continue;

            _sellableCharacters[i].InstantiatedPrefab.gameObject.SetActive(true);
            break;
        }
    }

    private void InitializeCharacters()
    {
        _sellableCharacters = new();

        foreach (var characterData in _characterData)
        {
            Character character = new(characterData, _characterSpawnPoint);
            _sellableCharacters.Add(character);
            character.InstantiatedPrefab.MoneyEffect.gameObject.SetActive(false);
            Animator animator;

            if (!character.InstantiatedPrefab.TryGetComponent<Animator>(out animator))
            {
                Debug.LogError("Character doesn't has a Animator component");
                continue;
            }

            animator.runtimeAnimatorController = _characterInMenuController;
        }
    }

    private void InitializeButtons()
    {
        _toBattleButton.Initialize(_sceneLoader, _dataForLevel, _shop);
        _damageUpgradePurchaseButton.Initialize(_shop);
        _healthUpgradePurchaseButton.Initialize(_shop);
        _firingRateUpgradePurchaseButton.Initialize(_shop);

        foreach (var button in _changeCharacterButtons)
            button.Inititalize(_shop);
    }
}
