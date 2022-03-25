using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YellowChaseState", menuName = "GhostSates/Chase/YellowChase")]
public class GhostYellowChaseState : AbstractGhostState
{
    public void OnEnable()
    {
        stateType = FSMStatesAvalable.CHASEYELLOW;
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            Debug.Log("Entered YellowChase Mode");
            SetTarget();
            direction = GetOpositeDirection();
        }
        EnteredState = base.Enter();
        return EnteredState;

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting YellowChase mode");
    }

    public override void UpdateStep()
    {
        if (EnteredState)
        {
            base.UpdateStep();
            SetTarget();
            Debug.Log("Fix Updating YellowChase mode");

        }

    }

    public override void SetTarget()
    {
        Vector2 pacManPossition = new Vector2(PacMan.instance.transform.position.x, PacMan.instance.transform.position.y);

        if (Vector2.Distance(new Vector2(ghostObj.transform.position.x, ghostObj.transform.position.y), pacManPossition) > 8f)
        {
            target = pacManPossition;
        }
        else
        {
            target= ghostObj.CornerToScatter;
        }
        

    }
}
