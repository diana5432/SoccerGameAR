using UnityEngine;

public class GoalController : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            float hitDistanceFromCenter = Vector3.Distance(hitPosition, transform.position);
            Notify(hitDistanceFromCenter, NotificationType.GoalHit);
        }
    }
}
