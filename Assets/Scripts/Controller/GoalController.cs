using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log(other.ClosestPointOnBounds(transform.position));
            Debug.Log(other.ClosestPointOnBounds(other.transform.position));
            Notify(0, NotificationType.GoalHit);
        }
    }
}
