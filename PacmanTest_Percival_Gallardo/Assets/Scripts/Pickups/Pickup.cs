using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    protected int _pointsGiven = 0;
    [SerializeField]protected string _name = "Fruit";
    Gamemanager manager;
 
    void Start()
    {
        manager = Gamemanager.instance;
        StartCoroutine(Despawn());
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
            manager.AddToList(_name);
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(8f);
        gameObject.SetActive(false);
    }


}
