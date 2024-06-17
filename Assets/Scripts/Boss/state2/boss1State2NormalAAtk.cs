using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1State2NormalAAtk : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeAttack;
    [SerializeField] float AtkRepeatTime;
    float Timer2 = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer = 0;
        Timer2 = 0;
        animator.GetComponentInParent<EnemyAI>().speed = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if (Timer > timeBeforeAttack)
        {
            Timer2 += Time.deltaTime;
            if(Timer2 > AtkRepeatTime)
            {
                animator.GetComponentInParent<boss1State2Skill>().NormalAtk();
                Timer2 = 0;
            }
            
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<boss1State2Skill>().BacktoNormalMoveSpeed(1f);
    }

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
