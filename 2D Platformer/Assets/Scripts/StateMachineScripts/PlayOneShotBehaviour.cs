using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    [SerializeField] AudioClip soundToPlay;
    [SerializeField] float volume = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    [SerializeField] float playDelay = 0.25f;
    float timeSinceEntered = 0f;
    private bool hasDelyaedSoundPlayed = false;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);

        timeSinceEntered = 0;
        hasDelyaedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !hasDelyaedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;

            if(timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                hasDelyaedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
    }

    
}
