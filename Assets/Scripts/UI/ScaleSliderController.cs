using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleSliderController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Slider _slider;
    private float _defaultValue;
    private float _currentValue;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _defaultValue = _slider.value;
        _currentValue = _slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentValue != _slider.value)
        {
            _currentValue = _slider.value;
            _target.localScale = Vector3.one * _currentValue;
        }
    }

    public void ResetDefaultValue()
    {
        _slider.value = _defaultValue;
    }
}
