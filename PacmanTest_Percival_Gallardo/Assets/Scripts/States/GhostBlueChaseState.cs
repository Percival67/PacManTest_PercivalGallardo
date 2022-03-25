using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlueChaseState", menuName = "GhostSates/Chase/BlueChase")]
public class GhostBlueChaseState : AbstractGhostState
{
    public void OnEnable()
    {
        stateType = FSMStatesAvalable.CHASEBLUE;
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            Debug.Log("Entered BlueChase Mode");
            SetTarget();
            direction = GetOpositeDirection();
        }
        EnteredState = base.Enter();
        return EnteredState;

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting BlueChase mode");
    }

    public override void UpdateStep()
    {
        if (EnteredState)
        {
            base.UpdateStep();
            SetTarget();
            Debug.Log("Fix Updating BlueChase mode");

        }

    }

    public override void SetTarget()//gets point infront of pacman, then takes the vector angle from red ghost to pacman and it rotates 180, making it the new target
    {
        
        Vector2 DistanceFourTilesAhead = (GetForwardTileDir(PacMan.instance.DirectionEnum) * 2);//four tiles infront on the direction pacman is looking at.
        Vector2 DistanceFourTilesLeft = (GetForwardTileDir(Direction.LEFT) * 2);

        Vector2 CenterPointOfRotation = new Vector2();
        Vector2 PossisionOfRedPoint = new Vector2(Gamemanager.instance.RedGhost.transform.position.x, Gamemanager.instance.RedGhost.transform.position.y);
        if (PacMan.instance.DirectionEnum == Direction.UP)
        {
            CenterPointOfRotation = (new Vector2(PacMan.instance.transform.position.x, PacMan.instance.transform.position.y).normalized + DistanceFourTilesAhead + DistanceFourTilesLeft);
        }
        else
        {
            CenterPointOfRotation = (new Vector2(PacMan.instance.transform.position.x, PacMan.instance.transform.position.y).normalized + DistanceFourTilesAhead);
        }

        Vector2 NewTargetPointRelativeToOrigin=PossisionOfRedPoint- CenterPointOfRotation;
        NewTargetPointRelativeToOrigin= NewTargetPointRelativeToOrigin * new Vector2(-1, -1);//Rotate 180
        target = NewTargetPointRelativeToOrigin + CenterPointOfRotation;//bring back points to center point of rotation
    }
}
