using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperController : MonoBehaviour, Observer
{
    [SerializeField] private SeriesController _series;

    [SerializeField] private float minWaitTime = 0.5f;
    [SerializeField] private float maxWaitTime = 1.5f;
        
    private AnimationClip[] clips;
    private Animator animator;


    private void Start() 
    {
        if (_series!=null)
            _series.RegisterObserver(this);
        
        // Get the animator component
        animator = GetComponent<Animator>();
        // Get all available clips
        clips = animator.runtimeAnimatorController.animationClips;
    }
    
    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.SeriesScan)
            StopAllCoroutines();
        if (notificationType == NotificationType.SeriesPlay)
            StartCoroutine(PlayRandomly());
        if (notificationType == NotificationType.SeriesDone)
            StopAllCoroutines();
    }

    private IEnumerator PlayRandomly()
    {
        while(true)
        {
            var randInd = Random.Range(0, clips.Length);
            var randClip = clips[randInd];
            animator.Play(randClip.name);

            // Wait until animation finished than pick the next one
            var t = randClip.length;
            yield return new WaitForSeconds(
                Random.Range(minWaitTime + t, maxWaitTime + t));
        }
    }
}
