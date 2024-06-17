using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class boss1SelfHeal : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeHeal;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("EndHeal", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if (Timer > timeBeforeHeal)
        {
            animator.GetComponentInParent<Boss1Skill>().treeBossSkill.TreeHeal();
            Timer = 0;
        }

        if(animator.GetComponentInParent<BossInfo>().curHealth > 0)
        {
            animator.SetBool("EndHeal",true);
            animator.GetComponentInParent<Boss1Skill>().WaitForHeal = false;
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
