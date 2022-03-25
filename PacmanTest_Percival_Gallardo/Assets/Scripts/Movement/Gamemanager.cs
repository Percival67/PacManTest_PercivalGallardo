using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private int Lives = 3;

    private List<string> Collectables=new List<string>();
    private GameObject[] _arrayOfDots;
    private int _dotsleft=0;

    private int CurrentLevel=1;

    [SerializeField]private TextMeshProUGUI TextBoxScore,TBLevel,TBHeath,TBReady;

    public int Score { get => score; set => score = value; }
    public GameObject[] ArrayOfDots { get => _arrayOfDots; set => _arrayOfDots = value; }
    public int Dotsleft { get => _dotsleft; set => _dotsleft = value; }
    public float Timer { get => _timer; set => _timer = value; }

    void Start()
    {
        UpdateUI();
        ArrayOfDots = GameObject.FindGameObjectsWithTag("Dot");
        Dotsleft = ArrayOfDots.Length;
        ResetGhost();
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

            UpdateUI();
            IsItNextLevel(); 
        }

    }


    private void UpdateUI()
    {
        TextBoxScore.text = ("Score: " + score);
        TBLevel.text = ("Level: " + CurrentLevel);
        TBHeath.text = ("Lives: " + Lives);
        
    }

    private bool GameOver()
    {
        if (Lives == 0)
        {
            return true;
        }
        return false;

    }

    public void Takedamage()
    {
        Lives--;
        UpdateUI();
        if (GameOver())
        {
            HardResetLevel();
            ResetGhost();
        }
        else
        {
            ResetGhost();
            ResetLevel();
        }
        
    }
    private void HardResetLevel()
    {
        score = 0;
        Timer = 0;
        Lives = 3;
        ResetPoints();
        ResetGhost();

    }
    private void nextLevel()
    {
        ResetGhost();
        ResetPoints();
        CurrentLevel++;
          
    }
    private void ResetLevel()
    {
       
    }

    private void ResetPoints()
    {
        foreach (GameObject Obj in ArrayOfDots)
        {
            Obj.SetActive(true);
        }
    }

    private void ResetGhost()
    {
        CanWestart = false;
        PacMan.instance.gameObject.SetActive(false);
        PacMan.instance.transform.position = PacmanStartingArea.transform.position;
        PacMan.instance.gameObject.SetActive(true);
        Timer = 0;
        foreach (GameObject Ghost in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            Ghost.SetActive(false);
            Ghost.transform.position = PointInsideBox.transform.position;
            Ghost.GetComponent<FSMContoller>().EnterState(FSMStatesAvalable.HOME);
            Ghost.SetActive(true);
            StartCoroutine(DoStuffBeforeWeBegin());
            
        }  
        
            
    }


    private IEnumerator DoStuffBeforeWeBegin()
    {
        //Flashboard
        //flash current score
        TBReady.enabled = true;
        yield return new WaitForSeconds(5f);
        TBReady.enabled = false;
        CanWestart = true;
    }



}
