using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I need this script for sending messages from my Animator to my states
//I want to call functions needed for attacks logig as a animation events at a certain points
//But my script is on "Pink" GameObject and my Anmator is on my "Pink.Alive" GameObject

public class AnimationToStateMachine : MonoBehaviour
{
    
    public AttackState attackState;
    public DeadState deadState;

   private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

       
}
