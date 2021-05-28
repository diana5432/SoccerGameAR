using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Notify(1, NotificationType.GoalHit);
        }
    }
}
