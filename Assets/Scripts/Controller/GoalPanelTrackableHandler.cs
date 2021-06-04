using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPanelTrackableHandler : DefaultTrackableEventHandler
{
    [SerializeField] private GameObject _goalPanel;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        _goalPanel.transform.position = transform.position;
        _goalPanel.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        _goalPanel.SetActive(true);

    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        Debug.Log("ontrackinglost");
        //_goalPanel.SetActive(false);
    }
}
