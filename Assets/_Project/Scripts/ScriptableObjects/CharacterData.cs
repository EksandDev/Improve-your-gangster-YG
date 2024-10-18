using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private PlayerCharacterView _prefab;
    [SerializeField] private MultiLanguageString _name;
    [SerializeField] private int _cost;

    public PlayerCharacterView Prefab => _prefab;
    public MultiLanguageString Name => _name;
    public int Cost => _cost;
}
