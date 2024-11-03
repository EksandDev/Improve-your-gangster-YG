using UnityEngine;

[CreateAssetMenu(fileName = "MultiLanguageString", menuName = "Data/MultiLanguageString")]
public class MultiLanguageString : ScriptableObject
{
    [SerializeField] private string _russianString;
    [SerializeField] private string _englishString;
    [SerializeField] private string _turkishString;

    public string GetString()
    {
        return _englishString;
    }
}
