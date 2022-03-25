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

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }
    #endregion

    public PacMan instance=null;
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
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }




    private void MoveCharacter(InputAction.CallbackContext context)
    {
        if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + context.ReadValue<Vector2>(), .2f, stopMovement))
        {
            this.direction = context.ReadValue<Vector2>();
        }


        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }



}
