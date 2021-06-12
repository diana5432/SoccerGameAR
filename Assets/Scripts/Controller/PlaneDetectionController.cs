using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// This example demonstrates how to toggle plane detection,
/// and also hide or show the existing planes.
/// </summary>
[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetectionController : MonoBehaviour, Observer

{
    // Subjects to observe
    [SerializeField] private SeriesController _series;
    
    private ARPlaneManager _ARPlaneManager;

    void Awake()
    {
        _ARPlaneManager = GetComponent<ARPlaneManager>();

        if (_series!=null)
            _series.RegisterObserver(this);
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.SeriesScan)
        {
            SetPlaneDetectionActive(true);
        }
        if (notificationType == NotificationType.SeriesScale)
        {
            SetPlaneDetectionActive(false);
        }
    }




    private void SetPlaneDetectionActive(bool value)
    {
        _ARPlaneManager.enabled = value;
        SetAllPlanesActive(value);
    }

    /// <summary>
    /// Iterates over all the existing planes and activates
    /// or deactivates their <c>GameObject</c>s'.
    /// </summary>
    /// <param name="value">Each planes' GameObject is SetActive with this value.</param>
    private void SetAllPlanesActive(bool value)
    {
        foreach (var plane in _ARPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }

}
