using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1HealTree : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeHeal;
    bool CanHeal = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer = 0;
        CanHeal = true;
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if (Timer > timeBeforeHeal && CanHeal)
        {
            animator.GetComponentInParent<Boss1Skill>().HealTree();
            Timer = 0;
            CanHeal = false;
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
