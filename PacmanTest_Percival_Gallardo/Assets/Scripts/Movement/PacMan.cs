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

    private Vector2 direction,nextDireccion;
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
        if (this.nextDireccion != Vector2.zero)
        {
            MoveCharacterOnDirection(nextDireccion);
        }
    }

    private void FixedUpdate()
    {
        Vector2 translation;
        if (Gamemanager.instance.Timer>0) {
            Vector2 position = rigidbody.position;     
            translation = direction * speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(position + translation);

        }
    }
    private void MoveCharacterOnDirection(Vector2 _direction)
    {
        if (!IsOcupied(_direction))
        {

            direction = _direction;
            nextDireccion = Vector2.zero;
            SetCurrentDirection(direction);
        }
        else
        {
            nextDireccion = direction;
        }
        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public bool IsOcupied(Vector2 _direction)
    {

        //Collider2D colider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + _direction, .4f, stopMovement);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, _direction, 1.5f, stopMovement);
        Debug.Log(hit.collider);
        return hit.collider != null;

    }


    private void MoveCharacter(InputAction.CallbackContext context)
    {
        MoveCharacterOnDirection(context.ReadValue<Vector2>());
          
        
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ghost")
        {
            if (collision.collider.GetComponent<FSMContoller>().CurrentState == collision.collider.GetComponent<FSMContoller>().AfmsStates[FSMStatesAvalable.FRIGHTENED])
            {
                collision.collider.GetComponent<FSMContoller>().EnterState(FSMStatesAvalable.EATEN);
            }
            else
            {
                Gamemanager.instance.Takedamage();
            }
            
        }
    }

}
