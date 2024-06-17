using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1State2ULT : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeAttack;
    bool Isatk = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer = 0;
        animator.GetComponentInParent<EnemyAI>().speed = 0f;
        Isatk = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if (Timer > timeBeforeAttack && Isatk == false)
        {
            animator.GetComponentInParent<boss1State2Skill>().ULT();
            Isatk = true;
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
