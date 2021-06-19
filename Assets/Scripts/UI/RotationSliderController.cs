using UnityEngine;
using UnityEngine.UI;

public class RotationSliderController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Slider _slider;
    private float _defaultValue;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _defaultValue = _slider.value;
        _slider.onValueChanged.AddListener(RotationSliderUpdate);
    }

    private void RotationSliderUpdate(float value)
    {
        _target.localEulerAngles = new Vector3(_target.rotation.x, value, _target.rotation.z);
    }

    public void ResetDefaultValue()
    {
        _slider.value = _defaultValue;
    }
}
