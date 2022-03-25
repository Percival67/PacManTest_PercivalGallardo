using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HomeState", menuName = "GhostSates/Home")]
public class GhostHomeState : AbstractGhostState
{

   
    float timetowait;
   

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
        if (EnteredState &&ghostObj.transform.position.y < 18.5f)
        {
            MoveGhost();
            
        }
        else
        {
            ghostObj.gameObject.layer = LayerMask.NameToLayer("Ghost"); ;
            fsmComtroller.EnterState(FSMStatesAvalable.SCATTER);
        }
    }

    public override void SetTarget()
    {
        target = ghostObj.CornerToScatter;
    }
}
