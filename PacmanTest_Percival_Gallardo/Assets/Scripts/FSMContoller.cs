using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMContoller : MonoBehaviour
{
    

    AbstractGhostState currentState;

    [SerializeField]
    List<AbstractGhostState> validStates;
    Dictionary<FSMStatesAvalable, AbstractGhostState> fmsStates;


    void Awake()
    {
        

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        GhostObjScript ghostobj = this.GetComponent<GhostObjScript>();
        


        fmsStates = new Dictionary<FSMStatesAvalable, AbstractGhostState>();
        foreach(AbstractGhostState state in validStates)
        {
            state.SetFSMContoller(this);
            state.SetGhostObj(ghostobj);
            state.SetRigidBody(rigidbody);
            
            
            fmsStates.Add(state.stateType, state);
        }

    }


    void Start()
    {
        EnterState(FSMStatesAvalable.HOME);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void EnterState(AbstractGhostState nextState)
    {
        if (nextState == null)
        {
            return;
        }
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = nextState;
        currentState.Enter();

        
    }

    public void EnterState(FSMStatesAvalable stateType)
    {

        if (fmsStates.ContainsKey(stateType))
        {
            AbstractGhostState nextstate = fmsStates[stateType];
            
            EnterState(nextstate);

        }
    }

    

    //We use Fixed Because we move Rigid body
    public void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdateStep();
        }
    }


}
