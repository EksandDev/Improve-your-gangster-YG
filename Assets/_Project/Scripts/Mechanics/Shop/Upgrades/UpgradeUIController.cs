public class UpgradeUIController
{
    private UpgradeUI _damageUpgradeUI;
    private UpgradeUI _healthUpgradeUI;
    private UpgradeUI _firingRateUpgradeUI;
    private PurchaseButton _damageUpgradePurchaseButton;
    private PurchaseButton _healthUpgradePurchaseButton;
    private PurchaseButton _firingRateUpgradePurchaseButton;
    private Character _currentCharacter;

    public Character CurrentCharacter
    {
        get => _currentCharacter;
        set
        {
            _currentCharacter = value;
            _damageUpgradeUI.CurrentUpgrade = _currentCharacter.DamageUpgrade;
            _healthUpgradeUI.CurrentUpgrade = _currentCharacter.HealthUpgrade;
            _firingRateUpgradeUI.CurrentUpgrade = _currentCharacter.FiringRateUpgrade;
            _damageUpgradePurchaseButton.Item = _currentCharacter.DamageUpgrade;
            _healthUpgradePurchaseButton.Item = _currentCharacter.HealthUpgrade;
            _firingRateUpgradePurchaseButton.Item = _currentCharacter.FiringRateUpgrade;
        }
    }

    public UpgradeUIController(UpgradeUI damageUpgradeUI, UpgradeUI healthUpgradeUI, 
        UpgradeUI firingRateUpgradeUI, PurchaseButton damageUpgradePurchaseButton, 
        PurchaseButton healthUpgradePurchaseButton, PurchaseButton firingRateUpgradePurchaseButton)
    {
        _damageUpgradeUI = damageUpgradeUI;
        _healthUpgradeUI = healthUpgradeUI;
        _firingRateUpgradeUI = firingRateUpgradeUI;
        _damageUpgradePurchaseButton = damageUpgradePurchaseButton;
        _healthUpgradePurchaseButton = healthUpgradePurchaseButton;
        _firingRateUpgradePurchaseButton = firingRateUpgradePurchaseButton;
    }
}