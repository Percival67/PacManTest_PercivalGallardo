using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PacMan: MonoBehaviour
{

    [SerializeField] private float speed = 8f;
    private Vector2 initialDirection = new Vector2(1, 0);
    [SerializeField] private LayerMask stopMovement;
    private Rigidbody2D rigidbody;
    private Collider2D collider;

    private Vector2 direction;
    private Vector3 startingPlace;
    #region CharacterController
    private PlayerControls controller;
    private Direction _directionEnum;



    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }
    #endregion

    public static PacMan instance=null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this);
        }
        controller = new PlayerControls();
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        controller.Game.Move.performed += MoveCharacter;
        direction = initialDirection;
        startingPlace = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Gamemanager.instance.Timer>0) {
            Vector2 position = rigidbody.position;
            Vector2 translation = direction * speed * Time.fixedDeltaTime;

            rigidbody.MovePosition(position + translation);

        }
    }




    private void MoveCharacter(InputAction.CallbackContext context)
    {
        if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + context.ReadValue<Vector2>(), .2f, stopMovement))
        {
            this.direction = context.ReadValue<Vector2>();
            SetCurrentDirection(context.ReadValue<Vector2>());
        }


        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    //Helper funtion to translate vector to dir
    public void SetCurrentDirection(Vector2 vector)
    {

        switch (vector)
        {
            case Vector2 v when v.Equals(Vector2.up):
                _directionEnum = Direction.UP;
                break;

            case Vector2 v when v.Equals(Vector2.down):
                _directionEnum = Direction.DOWN;
                break; 
            case Vector2 v when v.Equals(Vector2.left):
                _directionEnum = Direction.LEFT;
                break;
            case Vector2 v when v.Equals(Vector2.right):
                _directionEnum = Direction.RIGHT;
                break;
            default:
                
                break;

        }

    }

    
    public Direction DirectionEnum
    {
        get{
            return _directionEnum;
        }
    }

}
