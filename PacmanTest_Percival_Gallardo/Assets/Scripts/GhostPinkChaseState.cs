using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PinkChaseState", menuName = "GhostSates/Chase/PinkChase")]
public class GhostPinkChaseState : AbstractGhostState
{
    public void OnEnable()
    {
        stateType = FSMStatesAvalable.CHASEPINK;
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            Debug.Log("Entered PinkChase Mode");
            SetTarget();
            direction = GetOpositeDirection();
        }
        EnteredState = base.Enter();
        return EnteredState;

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting PinkChase mode");
    }

    public override void UpdateStep()
    {
        if (EnteredState)
        {
            base.UpdateStep();
            SetTarget();
            Debug.Log("Fix Updating PinkChase mode");
        }

    }

    public override void SetTarget()
    {

         
        Vector2 DistanceFourTilesAhead = (GetForwardTileDir(PacMan.instance.DirectionEnum) * 4);//four tiles infront on the direction pacman is looking at.
        Vector2 DistanceFourTilesLeft = (GetForwardTileDir(Direction.LEFT) * 4);
        if (PacMan.instance.DirectionEnum == Direction.UP){
            target = (new Vector2(PacMan.instance.transform.position.x, PacMan.instance.transform.position.y) + DistanceFourTilesAhead+ DistanceFourTilesLeft);
        }
        else
        {
            target = (new Vector2(PacMan.instance.transform.position.x, PacMan.instance.transform.position.y)+ DistanceFourTilesAhead);
        }
        

    }
}
