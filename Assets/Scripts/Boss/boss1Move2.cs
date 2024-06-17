using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1Move2 : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeAttack;
    int A = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<EnemyAI>().speed = animator.GetBehaviour<boss1Move1>().DefaultSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if(Timer > timeBeforeAttack)
        {
            if(A<2)
            {
                animator.SetTrigger("NormalAtk");
                Timer = 0;
                A++;
                animator.GetComponentInParent<Boss1Skill>().CloseShield();
            }
            else
            {
                animator.GetComponentInParent<Boss1Skill>().RandomAtk();
                animator.GetComponentInParent<Boss1Skill>().CloseShield();
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
