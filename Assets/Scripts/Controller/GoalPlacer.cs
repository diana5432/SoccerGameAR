using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GoalPlacer : MonoBehaviour
{
    [SerializeField] private GoalPanelController _goalPanel;

    private ARRaycastManager _raycastManager;
    private List<ARRaycastHit> _hits;

    private void Awake() 
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        _hits = new List<ARRaycastHit>();
    }

    private void Update() 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (_raycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = _hits[0].pose;

                _goalPanel.SpawnAtPosition(hitPose.position);
            }
        }

    }


}
