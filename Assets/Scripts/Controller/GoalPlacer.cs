using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

            if (PhysicRayCastBlockedByUi(touch.position))
            {
                Pose hitPose = _hits[0].pose;

                _goalPanel.SpawnAtPosition(hitPose.position);
            }
        }

    }

    private bool PhysicRayCastBlockedByUi(Vector2 touchPosition)
    {
        if (PointerOverUI.IsPointerOverUIObject(touchPosition))
        {
            return false;
        }

        return _raycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon);
    }
}


// source code from: https://forum.unity.com/threads/ar-foundation-never-blocking-raycaster-on-ui.986688/
public class PointerOverUI
{
    public static bool IsPointerOverUIObject(Vector2 touchPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = touchPosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults.Count > 0;
    }
}

