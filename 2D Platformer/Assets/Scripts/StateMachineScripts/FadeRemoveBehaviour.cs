using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    [SerializeField] float fadeTime = 0.5f;
    float timeElapsed;
    [SerializeField] float fadeDelay = 0f;
    float fadeDelayElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objectToTremove;
    Color startColor;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        objectToTremove = animator.gameObject;
        startColor = spriteRenderer.color;
        timeElapsed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed += Time.deltaTime;

            float newAlpha = startColor.a * 1 - (timeElapsed / fadeTime);

            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if(timeElapsed > fadeTime)
                Destroy(objectToTremove);
        }

    }
}
