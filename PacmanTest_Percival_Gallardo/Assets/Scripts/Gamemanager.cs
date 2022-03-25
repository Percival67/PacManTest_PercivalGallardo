using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    #region Singlaton
    public static Gamemanager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public GameObject RedGhost;
    public GameObject PointOutsideBox, PointInsideBox;

    public float Timer = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
    }

    public void GameOver()
    {

    }

    public void NextLevel()
    {

    }




}
