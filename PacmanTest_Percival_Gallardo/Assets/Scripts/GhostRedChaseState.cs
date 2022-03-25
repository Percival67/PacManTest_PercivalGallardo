using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RedChaseState", menuName = "GhostSates/Chase/RedChase")]
public class GhostRedChaseState : AbstractGhostState
{
    public void OnEnable()
    {
        stateType = FSMStatesAvalable.CHASERED;
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            Debug.Log("Entered RedChase Mode");
            SetTarget();
            direction = GetOpositeDirection();
        }
        EnteredState = base.Enter();
        return EnteredState;

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting RedChase mode");
    }

    public override void UpdateStep()
    {
        if (EnteredState)
        {
            base.UpdateStep();
            SetTarget();
            Debug.Log("Fix Updating RedChase mode");

        }

    }

    public override void SetTarget()
    {

        target = new Vector2(PacMan.instance.transform.position.x, PacMan.instance.transform.position.y);
        
    }
}
