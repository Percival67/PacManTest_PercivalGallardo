using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class GhostBackup : MonoBehaviour
{

    [SerializeField] private Direction direction;
    private Vector2 CornerToscater = new Vector2(0, 0);
    private int TileDimentions = 1;
    private Rigidbody2D rigidbody;
    private Collider2D collider;
    [SerializeField] private float speed = 8f;
    [SerializeField] private LayerMask stopMovement;
    [SerializeField] private Vector2 Target = new Vector2(0, 0);

    [SerializeField] private int controlx;
    [SerializeField] private int controly;

    void Start()
    {
        Debug.Log(LayerMask.LayerToName(stopMovement));
        //Debug.Log(LayerMask.NameToLayer(stopMovement));
    }

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private Direction GetOpositeDirection()
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

    private Vector2 GetForwardTileDir(Direction CurrentDir)
    {

        switch (CurrentDir)
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
                return new Vector2(0, 1);
        }

    }
    public float tempx;
    public float tempy;
    public bool canserch = true;

    private void FixedUpdate()
    {
        tempx = transform.position.x;
        tempy = transform.position.y;

        tempx = (tempx % 1);
        tempy = (tempy % 1);


        if ((direction == Direction.UP || direction == Direction.DOWN) && (tempy < .5 && tempy > .49)) //ghost is in the middle of a tile thefore he can choose
        {

            // if (canserch)
            //  {
            SetDirection();
            //  StartCoroutine(Wait());
            //  }

        }
        else if ((direction == Direction.LEFT || direction == Direction.RIGHT) && (tempx < .5 && tempx > .49))
        {
            // if (canserch)
            // {
            SetDirection();
            //StartCoroutine(Wait());
            // }
        }

        MoveGhost();

    }

    private IEnumerator Wait()
    {
        canserch = false;
        yield return new WaitForSeconds(.4f);
        canserch = true;
    }


    //!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + directionalTile, .3f, stopMovement)

    private void SetDirection()
    {
        Direction opositeDir = GetOpositeDirection();
        List<Direction> possibleDir = new List<Direction>() { Direction.UP, Direction.LEFT, Direction.DOWN, Direction.RIGHT }; // Priority a gost takes when 2 possible tiles are the same distance from target
        possibleDir.Remove(opositeDir);     //Remove the tile that is oposite to the current direction AKA direction the ghost is looking.

        float smallestDistance = -1;
        Direction BestDirection=Direction.UP;

        foreach (Direction dir in possibleDir)
        {
            Vector2 directionalTile = GetForwardTileDir(dir);

            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), directionalTile, .6f, stopMovement);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), directionalTile, Color.red);
            Debug.Log(ray.collider + " " + dir + "x: " + transform.position.x + " y: " + transform.position.y);


            if (ray.collider == null)
            {
                if (smallestDistance==-1)
                {
                    smallestDistance = Vector2.Distance((new Vector2(transform.position.x, transform.position.y) + directionalTile), Target);
                    BestDirection = dir;
                    //Debug.Log(smallestDistance);
                    continue;
                }

                if (Vector2.Distance(directionalTile, Target) < smallestDistance)
                {
                    smallestDistance = Vector2.Distance((new Vector2(transform.position.x, transform.position.y) + directionalTile), Target);
                    BestDirection = dir;
                    //Debug.Log(smallestDistance);
                }
            }
        }
        direction = BestDirection;

    }
    /*
    private void SetBestTileDirection()
    {
        Vector2 ClosestDirection;
        Direction bestDirection = Direction.NONE;



        foreach (Direction dir in GetPossibleDirections())
        {
            if (bestDirection == Direction.NONE)
            {
                bestDirection = direction;
            }
            Vector2.Distance()

            

        }
    
    
    }
    */
    private void MoveGhost()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = new Vector2();


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

        rigidbody.MovePosition(position + translation);

    }



}


