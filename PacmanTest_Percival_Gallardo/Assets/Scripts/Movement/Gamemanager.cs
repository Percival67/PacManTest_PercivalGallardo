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
    public GameObject PointOutsideBox, PointInsideBox,PacmanStartingArea;

    private float _timer = 0;
    private bool CanWestart = false;
    private int score = 0;

    private List<string> Collectables=new List<string>();
    private GameObject[] _arrayOfDots;
    private int _dotsleft=0;

    private int CurrentLevel=1;

    public int Score { get => score; set => score = value; }
    public GameObject[] ArrayOfDots { get => _arrayOfDots; set => _arrayOfDots = value; }
    public int Dotsleft { get => _dotsleft; set => _dotsleft = value; }
    public float Timer { get => _timer; set => _timer = value; }

    void Start()
    {
        ArrayOfDots= GameObject.FindGameObjectsWithTag("Dot");
        Dotsleft = ArrayOfDots.Length;
        ResetLevel();
    }

    public void AddScore(int amount)
    {
        Score = Score + amount;
    }

    public void AddToList(string Name)
    {
        Collectables.Add(Name);
    }

    public void DotCollected()
    {
        Dotsleft--;
        
    }
    private void IsItNextLevel()
    {
        if (Dotsleft == 0)
        {
            CurrentLevel++;
        }
    }
    void Update()
    {
        if (CanWestart) {
            Timer += Time.deltaTime;

            IsItNextLevel(); 
        }

    }

    public void GameOver()
    {

    }

    public void ResetLevel()
    {
        CanWestart = false;
        PacMan.instance.transform.position = PacmanStartingArea.transform.position;
        Timer = 0;
        foreach (GameObject Ghost in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            Ghost.SetActive(false);
            Ghost.transform.position = PointInsideBox.transform.position;
            Ghost.GetComponent<FSMContoller>().EnterState(FSMStatesAvalable.HOME);
            Ghost.SetActive(true);
            StartCoroutine(DoStuffBeforeWeBeguin());
            
        }  
        
            
    }
    private IEnumerator DoStuffBeforeWeBeguin()
    {
        //Flashboard
        //flash current score

        yield return new WaitForSeconds(5f);
        CanWestart = true;
    }



}
