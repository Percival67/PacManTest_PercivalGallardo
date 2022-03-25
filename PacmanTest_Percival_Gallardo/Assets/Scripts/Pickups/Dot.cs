using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    protected int _pointsGiven = 0;
    Gamemanager manager;

    void Start()
    {
        manager = Gamemanager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.tag == "Player")
        {
            manager.AddScore(_pointsGiven);            
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}
