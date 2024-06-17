using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1FirePillow : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float TimeBeforeAtk;
    bool CanATk = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CanATk = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if (Timer > TimeBeforeAtk && CanATk == true)
        {
            CanATk = false;
            Timer = 0;
            animator.GetComponentInParent<Boss1Skill>().Skill02();
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
