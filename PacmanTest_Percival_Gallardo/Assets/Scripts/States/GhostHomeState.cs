using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HomeState", menuName = "GhostSates/Home")]
public class GhostHomeState : AbstractGhostState
{

   
    float timetowait=0;
    float timeatStart = 0;
   

    public void OnEnable()
    {
        stateType = FSMStatesAvalable.HOME;
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            timetowait = ghostObj.WaitToLeave;
            direction = Direction.UP;
            timeatStart = Gamemanager.instance.Timer;
            Debug.Log("Entering Home mode");
            ghostObj.gameObject.layer = LayerMask.NameToLayer("NoColission");




        }
        EnteredState = base.Enter();
        return EnteredState;

    }



    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Home mode");
        
        
    }

    public override void Update()
    {
      

    }

    public override void UpdateStep()

    {
        if(Gamemanager.instance.Timer - timeatStart > timetowait){
            if (EnteredState && ghostObj.transform.position.y < 18.5f)
            {
                MoveGhost();

            }
            else
            {
                ghostObj.gameObject.layer = LayerMask.NameToLayer("Ghost"); ;
                fsmComtroller.EnterState(FSMStatesAvalable.SCATTER);
            }
        }
    }

    public override void SetTarget()
    {
        target = ghostObj.CornerToScatter;
    }
}
