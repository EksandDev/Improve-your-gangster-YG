using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothGameProcessSlider : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moneyVFX;
    [SerializeField] private GameObject _skullImage;
    
    private Slider _baseSlider;
    
    public void Initialize()
    {
        _baseSlider = GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        StartCoroutine(SetValueCoroutine(value));
    }

    private IEnumerator SetValueCoroutine(float value)
    {
        float originValue = _baseSlider.value;
        float interpolation = 0;
        
        while (_baseSlider.value < value)
        {
            interpolation += 0.02f;
            _baseSlider.value = Mathf.Lerp(originValue, value, interpolation);
            yield return null;
        }

        if (!Mathf.Approximately(_baseSlider.value, _baseSlider.maxValue)) 
            yield break;
        
        _skullImage.SetActive(false);
        _moneyVFX.Play();
    }
}