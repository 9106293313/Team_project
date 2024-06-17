using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeControl : StateMachineBehaviour
{
    float Timer = 0;
    [SerializeField] float timeBeforeAtkMin;
    [SerializeField] float timeBeforeAtkMax;
    float timeBeforeAtk;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponentInParent<treeBossSkill>().MainBossInfo.curHealth<=0)
        {
            timeBeforeAtkMin = 5f;
            timeBeforeAtkMax = 8f;
        }
        timeBeforeAtk = Random.Range(timeBeforeAtkMin, timeBeforeAtkMax);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer += Time.deltaTime;
        if (Timer > timeBeforeAtk)
        {
            animator.GetComponentInParent<treeBossSkill>().RandomAtk();
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
