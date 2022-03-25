using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField]private int _pointsGiven = 10;
    Gamemanager manager;
    [SerializeField] private bool _isPowerUPDot=false;

    void Start()
    {
        manager = Gamemanager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
           // Debug.Log("Hit");
            if (_isPowerUPDot)
            {
                foreach (GameObject Ghost in GameObject.FindGameObjectsWithTag("Ghost"))
                {
                    Ghost.GetComponent<FSMContoller>().EnterState(FSMStatesAvalable.FRIGHTENED);
                }

            }
            manager.AddScore(_pointsGiven);
            manager.Dotsleft--;
            StopAllCoroutines();
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
