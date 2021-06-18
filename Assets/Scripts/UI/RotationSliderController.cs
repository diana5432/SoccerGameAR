using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationSliderController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(RotationSliderUpdate);
    }

    private void RotationSliderUpdate(float value)
    {
        _target.localEulerAngles = new Vector3(_target.rotation.x, value, _target.rotation.z);
    }
}
