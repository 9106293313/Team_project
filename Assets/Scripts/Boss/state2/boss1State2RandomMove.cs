using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1State2RandomMove : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeAttack;
    int A = 0;
    float DefaultSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //DefaultSpeed = animator.GetComponentInParent<EnemyAI>().speed;
        //animator.GetComponentInParent<EnemyAI>().speed = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if (Timer > timeBeforeAttack)
        {
            if (A < 1)
            {
                animator.SetTrigger("NormalAtk");
                Timer = 0;
                A++;
            }
            else
            {
                animator.GetComponentInParent<boss1State2Skill>().RandomAtk();
                Timer = 0;
                A = 0;
            }

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
