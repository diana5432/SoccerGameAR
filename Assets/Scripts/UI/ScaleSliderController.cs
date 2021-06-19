using UnityEngine;
using UnityEngine.UI;

public class ScaleSliderController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Slider _slider;
    private float _defaultValue;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _defaultValue = _slider.value;
        _slider.onValueChanged.AddListener(ScaleSliderUpdate);
    }

    void ScaleSliderUpdate(float value)
    {
        _target.localScale = Vector3.one * value;
    }

    public void ResetDefaultValue()
    {
        _slider.value = _defaultValue;
    }
}
