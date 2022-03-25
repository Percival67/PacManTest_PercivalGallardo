using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatenState", menuName = "GhostSates/Eaten")]
public class GhostEatenState : AbstractGhostState
{
    public void OnEnable()
    {
        stateType = FSMStatesAvalable.EATEN;
        
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            Debug.Log("Entered Eaten Mode");
            //Change Sprite to eyes
            //change speed
            SetTarget();
            
        }
        EnteredState = base.Enter();
        return EnteredState;

    }

    public override void Exit()
    {
        base.Exit();
        //change Sprite from eyes to ghost
    }

    public override void UpdateStep()
    {
        if (EnteredState)
        {
            base.UpdateStep();
            if(ghostObj.transform.position.x>12.5&& ghostObj.transform.position.x <13.5&& ghostObj.transform.position.y >18.4&& ghostObj.transform.position.y < 18.6)
            {
                //Switch To idle
                //Do Respawn animation
                fsmComtroller.EnterState(GetChaseState(ghostObj.GetGhostColor));

            }
            
        }

    }

    public override void SetTarget()
    {
        target = new Vector2(13f, 18.5f);
    }
}
