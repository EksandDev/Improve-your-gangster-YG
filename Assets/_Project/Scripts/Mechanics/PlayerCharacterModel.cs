using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterModel : BattlerModel
{
    public PlayerCharacterModel(Level level, Attacker attacker, Transform currentTransform, 
        int damage, int maxHealth) : base(level, attacker, currentTransform, damage, maxHealth) { }

    public override void Die()
    {
        base.Die();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}