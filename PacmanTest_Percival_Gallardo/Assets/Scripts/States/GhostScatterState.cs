using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaterState", menuName = "GhostSates/Scater")] 
public class GhostScatterState : AbstractGhostState
{
    float timetowait = 5;
    float timeatStart = 0;

    public void OnEnable()
    {
        stateType = FSMStatesAvalable.SCATTER;
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            Debug.Log("Entered Scater Mode");
            SetTarget();
            direction = GetOpositeDirection();
            timeatStart = Gamemanager.instance.Timer;
        }
        EnteredState = base.Enter();
        return EnteredState;

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Scater mode");
    }
    
    public override void UpdateStep()
    {
        if (EnteredState)
        {
            base.UpdateStep();
            Debug.Log("Fix Updating Scater mode");
            if (Gamemanager.instance.Timer - timeatStart > timetowait)
            {
                fsmComtroller.EnterState(GetChaseState(ghostObj.GetGhostColor));
            }
        }
        
    }

    public override void SetTarget()
    {
        target = ghostObj.CornerToScatter;
    }
}
