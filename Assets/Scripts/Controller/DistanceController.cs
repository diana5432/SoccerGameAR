using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour
{
    [SerializeField] private Transform _penaltySpot;
    [SerializeField] private Transform _baseLineCenter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            float distance = Vector3.Distance(_penaltySpot.position, _baseLineCenter.position);
            Debug.Log(distance);
        }
    }
}
