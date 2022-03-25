using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject Red;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= Red.GetComponent<FSMContoller>().CurrentState.target;
    }
}
