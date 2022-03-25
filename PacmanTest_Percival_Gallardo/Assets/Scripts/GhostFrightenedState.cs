using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrightenedState", menuName = "GhostSates/Frightened")]
public class GhostFrightenedState : AbstractGhostState
{
    public void OnEnable()
    {
        stateType = FSMStatesAvalable.FRIGHTENED;
    }

    public override bool Enter()
    {
        if (base.Enter())
        {
            Debug.Log("Entered Frigthened Mode");
            direction = GetOpositeDirection();  //When entering Frigthen state ghost do a 180 turn.
        }
        EnteredState = base.Enter();
        return EnteredState;

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Frigthened mode");
    }

    public override void UpdateStep()
    {
        if (EnteredState)
        {
            
            Debug.Log("Fix Updating Frigthened mode");
        }

    }

    //In Frigthened state ghost choose at random from the possible tiles.
    public override void SetBestTileDirection()
    {
        Direction opositeDir = GetOpositeDirection();
        List<Direction> possibleDir = new List<Direction>() { Direction.UP, Direction.LEFT, Direction.DOWN, Direction.RIGHT }; // Priority a gost takes when 2 possible tiles are the same distance from target
        possibleDir.Remove(opositeDir);     //Remove the tile that is oposite to the current direction AKA direction the ghost is looking.

        Direction BestDirection = possibleDir[Random.Range(0, possibleDir.Count)];
        direction = BestDirection;

    }

    public override void SetTarget()
    {
        throw new System.NotImplementedException();
    }
}