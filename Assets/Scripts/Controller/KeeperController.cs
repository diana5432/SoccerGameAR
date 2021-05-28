using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperController : MonoBehaviour
{
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;
    
    private AnimationClip[] clips;
    private Animator animator;

    private void Awake()
    {
        // Get the animator component
        animator = GetComponent<Animator>();
        // Get all available clips
        clips = animator.runtimeAnimatorController.animationClips;
    }

    private void Start()
    {
        StartCoroutine(PlayRandomly());
    }
    
    private IEnumerator PlayRandomly ()
    {
        // wait a second at the beginning
        yield return new WaitForSeconds(1f);
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
