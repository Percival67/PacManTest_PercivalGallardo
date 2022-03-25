using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D),typeof (FSMContoller))]
public class GhostObjScript : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Collider2D collider;
    FSMContoller _FSMController;


    [SerializeField] Vector2 _cornerToscatter;
    [SerializeField] TypeofGhost ghostColor;
    [SerializeField] float _waitToLeave;
    [SerializeField] bool _hasLeftOnce=false;




    void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        _FSMController = GetComponent<FSMContoller>();
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public Vector2 CornerToScatter {
        get { return _cornerToscatter; }
    }
    public float WaitToLeave
    {
        get { return _waitToLeave; }
    }
    public bool HasLeftOnce
    {
        get { return _hasLeftOnce; }
    }
    public TypeofGhost GetGhostColor
    {
        get { return ghostColor; }
    }
    //public Collider2D GetColider
    //{
    //    get { return collider; }
    //}
}
