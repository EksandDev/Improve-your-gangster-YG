using System;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class MainMenuEntryPoint : MonoBehaviour, ISaveCaller
{
    [Header("UI")]
    [SerializeField] private ChangeCharacterButton[] _changeCharacterButtons;
    [SerializeField] private ToBattleButton _toBattleButton;
    [SerializeField] private CharacterPurchaseButton _characterPurchaseButton;
    [SerializeField] private PurchaseButton _damageUpgradePurchaseButton;
    [SerializeField] private PurchaseButton _firingRateUpgradePurchaseButton;
    [SerializeField] private PurchaseButton _healthUpgradePurchaseButton;
    [SerializeField] private UpgradeUI _damageUpgradeUI;
    [SerializeField] private UpgradeUI _firingRateUpgradeUI;
    [SerializeField] private UpgradeUI _healthUpgradeUI;
    [SerializeField] private MoneyUI _moneyUI;
    [SerializeField] private GameObject _lockImage;

    [Header("Characters")]
    [SerializeField] private CharacterData[] _characterData;
    [SerializeField] private AnimatorOverrideController _characterInMenuController;
    [SerializeField] private Transform _characterSpawnPoint;

    [Header("Other")]
    [SerializeField] private SceneContext _sceneContext;

    private SaveSystem _saveSystem;
    private GameSaves _gameSaves;
    private Shop _shop;
    private UpgradeUIController _upgradeUIController;
    private PlayerStats _playerStats;
    private NeedLoadTracker _needLoadTracker;
    private SceneLoader _sceneLoader;
    private DataForLevel _dataForLevel;
    private List<Character> _sellableCharacters;

    private const string LEADERBORD_NAME = "CountOfLevels";

    public event Action CallingSave;

    #region Zenject initialization
    [Inject]
    private void Construct(SceneLoader sceneLoader, DataForLevel dataForLevel, PlayerStats playerStats,
        NeedLoadTracker firstEnterTracker)
    {
        _sceneLoader = sceneLoader;
        _dataForLevel = dataForLevel;
        _playerStats = playerStats;
        _needLoadTracker = firstEnterTracker;
    }
    #endregion

    private void Start()
    {
        _sceneContext.Run();
        InitializeCharacters();
        _upgradeUIController = new(_damageUpgradeUI, _healthUpgradeUI, _firingRateUpgradeUI,
            _damageUpgradePurchaseButton, _healthUpgradePurchaseButton, _firingRateUpgradePurchaseButton, 
            _lockImage);
        _shop = new(_playerStats, _sellableCharacters, _upgradeUIController, _characterPurchaseButton);
        InitializeButtons();
        InitializeSaveSystem();

        for (int i = 0; i < _sellableCharacters.Count - 1; i++ )
        {
            if (i != _shop.CurrentCharacterIndex)
                continue;

            _sellableCharacters[i].InstantiatedPrefab.gameObject.SetActive(true);
            break;
        }

        LoadData();
        _shop.UpdateUI();
        CallingSave?.Invoke();
        YandexGame.NewLeaderboardScores(LEADERBORD_NAME, _playerStats.CurrentLevel);
        _moneyUI.Inititialize(_playerStats, _shop);
    }

    private void LoadData()
    {
        _gameSaves = _saveSystem.Load();

        if (_needLoadTracker.NeedLoad)
        {
            _gameSaves?.Load(_playerStats, _shop);
            _needLoadTracker.NeedLoad = false;
            YandexGame.GameReadyAPI();
            return;
        }

        _gameSaves.LoadCharacterSaves(_shop);
    }

    private void InitializeSaveSystem()
    {
        List<ISaveCaller> saveCallers = new()
        {
            this,
            _shop,
            _toBattleButton
        };

        _saveSystem = new(_playerStats, _shop, saveCallers);
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

            if (!character.InstantiatedPrefab.TryGetComponent(out animator))
            {
                Debug.LogError("Character doesn't have a Animator component");
                continue;
            }

            animator.runtimeAnimatorController = _characterInMenuController;
        }
    }

    private void InitializeButtons()
    {
        _toBattleButton.Initialize(_sceneLoader, _dataForLevel, _shop);
        _characterPurchaseButton.Initialize(_shop);
        _damageUpgradePurchaseButton.Initialize(_shop);
        _healthUpgradePurchaseButton.Initialize(_shop);
        _firingRateUpgradePurchaseButton.Initialize(_shop);

        foreach (var button in _changeCharacterButtons)
            button.Inititalize(_shop);
    }
}
