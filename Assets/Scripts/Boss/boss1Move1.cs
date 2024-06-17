using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1Move1 : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeOpenShield;
    public float DefaultSpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DefaultSpeed = animator.GetComponentInParent<EnemyAI>().speed;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer+=Time.deltaTime;
        if(Timer > timeBeforeOpenShield)
        {
            animator.GetComponentInParent<EnemyAI>().speed = 0;
            animator.SetTrigger("OpenShield");
            Timer = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
