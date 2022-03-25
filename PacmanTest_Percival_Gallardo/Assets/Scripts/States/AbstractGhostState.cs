using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum FSMStatesAvalable
{
    SCATTER,
    EATEN,
    FRIGHTENED,
    CHASERED,
    CHASEPINK,
    CHASEBLUE,
    CHASEYELLOW,
    HOME
}
public enum ExecutionState
{
    ENTER, UPDATE, EXIT
};

public enum TypeofGhost
{
    RED,
    PINK,
    BLUE,
    YELLOW

}


public abstract class AbstractGhostState : ScriptableObject
{
    protected Direction direction=Direction.UP;
    protected float speed=5;

    public ExecutionState executionState { get; protected set; }
    public bool EnteredState { get; protected set; }
    public FSMStatesAvalable stateType { get; protected set; }

    public AbstractGhostState nextState;

    protected Rigidbody2D rigidbody;
    protected GhostObjScript ghostObj;
    protected FSMContoller fsmComtroller;
    protected Vector2 scatterCorner;
    protected Vector2 target;
    
    


    public virtual void Update()
    {

    }

    public virtual bool Enter() {
        executionState = ExecutionState.UPDATE;
        bool succesGhostObj=true;
        succesGhostObj = (ghostObj != null);
        return succesGhostObj;



    }
    public virtual void UpdateStep() { 
        executionState = ExecutionState.UPDATE;
        float tempx;
        float tempy;

        tempx = ghostObj.transform.position.x;
        tempy = ghostObj.transform.position.y;

        tempx = (tempx % 1);
        tempy = (tempy % 1);


        if ((direction == Direction.UP || direction == Direction.DOWN) && (tempy < .5 && tempy > .48)) //ghost is in the middle of a tile thefore he can choose
        {
            SetBestTileDirection();
            Debug.Log("Best Chosen");
        }
        else if ((direction == Direction.LEFT || direction == Direction.RIGHT) && (tempx < .5 && tempx > .48))
        {
            SetBestTileDirection();
            Debug.Log("Best Chosen");
        }

        MoveGhost();

    }
    public virtual void Exit() { executionState = ExecutionState.EXIT; }

    public virtual IEnumerator ExitHomeFirstTime()
    {
        return null;
    }

    #region BasicNavegation
    //abstract as every state has a diferent way of getting the Target
    public abstract void SetTarget();

    //Decide the best next tile avalable depending on distance to Target
    public virtual void SetBestTileDirection()
    {
        Direction opositeDir = GetOpositeDirection();
        List<Direction> possibleDir = new List<Direction>() { Direction.UP, Direction.LEFT, Direction.DOWN, Direction.RIGHT }; // Priority a gost takes when 2 possible tiles are the same distance from target
        possibleDir.Remove(opositeDir);     //Remove the tile that is oposite to the current direction AKA direction the ghost is looking.

        float smallestDistance = -1;
        Direction BestDirection=new Direction();

        foreach (Direction dir in possibleDir)
        {
            Vector2 directionalTile = GetForwardTileDir(dir);
            // int layerMask = 1 << 3;
            bool first = true;
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(ghostObj.transform.position.x, ghostObj.transform.position.y), directionalTile, .6f, 1 << 3);

            Debug.DrawRay(new Vector2(ghostObj.transform.position.x, ghostObj.transform.position.y), directionalTile, Color.red);
            Debug.Log(ray.collider + " " + dir + "x: " + ghostObj.transform.position.x + " y: " + ghostObj.transform.position.y);
           
            //!Physics2D.OverlapCircle(new Vector2(ghostObj.transform.position.x, ghostObj.transform.position.y) + directionalTile, .2f, 1 << 3)

            if (ray.collider==null)
            {
                if (first)
                {
                    smallestDistance = Vector2.Distance((new Vector2(ghostObj.transform.position.x, ghostObj.transform.position.y) + directionalTile), target);
                    BestDirection = dir;
                    Debug.Log(smallestDistance);
                    first = false;
                    continue;
                }

                if (Vector2.Distance(directionalTile, target) < smallestDistance)
                {
                    smallestDistance = Vector2.Distance((new Vector2(ghostObj.transform.position.x, ghostObj.transform.position.y) +directionalTile), target);
                    BestDirection = dir;
                    Debug.Log(smallestDistance);
                }
            }
        }
        direction = BestDirection;

    }
    public void MoveGhost()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = new Vector2();
        //Debug.Log("RigidPoss " + position);

        switch (direction)
        {
            case Direction.UP:

                translation = new Vector2(0, 1) * speed * Time.fixedDeltaTime;
                break;
            case Direction.DOWN:
                translation = new Vector2(0, -1) * speed * Time.fixedDeltaTime;
                break;
            case Direction.LEFT:
                translation = new Vector2(-1, 0) * speed * Time.fixedDeltaTime;
                break;
            case Direction.RIGHT:
                translation = new Vector2(1, 0) * speed * Time.fixedDeltaTime;
                break;
            default:

                break;
        }
        //Debug.Log((position + translation));
        rigidbody.MovePosition(position + translation);

    }


    //public virtual void Get()

    //Helper funtion to get Directions in verctor2
    public Vector2 GetForwardTileDir(Direction _direction)
    {

        switch (_direction)
        {
            case Direction.UP:
                return new Vector2(0, 1);
            case Direction.DOWN:
                return new Vector2(0, -1);
            case Direction.LEFT:
                return new Vector2(-1, 0);
            case Direction.RIGHT:
                return new Vector2(1, 0);
            default:
                return new Vector2(0, 0);
        }

    }
    //Helper funtion to get Oposite Direction
    public Direction GetOpositeDirection()
    {
        switch (direction)
        {
            case Direction.UP:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.UP;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
            default:
                return Direction.UP;

        }
    }
    #endregion

    //public virtual AbstractGhostState Process()
    //{
    //    if (executionState == ExecutionState.ENTER) Enter();
    //    if (executionState == ExecutionState.UPDATE) UpdateStep();
    //    if (executionState == ExecutionState.EXIT)
    //    {
    //        Exit();

    //        return nextState;
    //    }
    //    return this;
    //}

    //public void SwitchToShell(AbstractGhostState state)
    //{
    //    nextState = state;
    //    executionState = ExecutionState.EXIT;
    //    state.executionState = ExecutionState.ENTER;

    //}

    #region SettingUpVariables

    public virtual void SetRigidBody(Rigidbody2D _rigidbody)
    {
        if (_rigidbody != null)
        {
            rigidbody = _rigidbody;
        }
    }


    public virtual void SetGhostObj(GhostObjScript _ghostObj)
    {
        if (_ghostObj != null)
        {
            ghostObj = _ghostObj;
        }
    }
  

    public virtual void SetFSMContoller(FSMContoller _fsmContoller)
    {
        if (_fsmContoller != null)
        {
            fsmComtroller = _fsmContoller;
        }
    }

  


    #endregion


}
