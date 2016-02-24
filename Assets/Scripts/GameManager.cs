using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameObject[] birdObject = new GameObject[4];
    private float gameTime;
    public bool isStarted, isOver, isAI;
    public GameObject startBtn, AIBtn, ExitBtn;
    public GameObject counter;
    public GameObject win, lose;

    public void PushStartBtn ()
    {
        Sound.Instance.stopBg();
        Sound.Instance.stopWin();
        isStarted = true;
        startBtn.SetActive(false);
        AIBtn.SetActive(false);
        ExitBtn.SetActive(false);
        Instantiate(counter);
        Sound.Instance.playCountdown();
        BlueSide.Instance.Initial();
        RedSide.Instance.Initial();
        StartCoroutine(Initial());
    }

    public IEnumerator Initial()
    {
        yield return new WaitForSeconds(3);
        Sound.Instance.playFight();
        isOver = false;
        gameTime = 0f;
        GenerateBird();
        GenerateBird();
        GenerateBird();
        GenerateBird();
    }

    public void GameOver(string loser)
    {
        Sound.Instance.stopFight();
        Sound.Instance.playWin();
        Sound.Instance.playBg();
        switch (loser)
        {
            case "Red":
                Instantiate(lose, new Vector3(0, 5.5f, -10), Quaternion.Euler(0, 0, 180));
                Instantiate(win, new Vector3(0, -5.5f, -10), Quaternion.Euler(0, 0, 0));
                break;
            case "Blue":
                Instantiate(win, new Vector3(0, 5.5f, -10), Quaternion.Euler(0, 0, 180));
                Instantiate(lose, new Vector3(0, -5.5f, -10), Quaternion.Euler(0, 0, 0));
                break;
        }
        isStarted = false;
        isOver = true;
        isAI = false;
        gameTime = 0f;
        StartCoroutine(PopBtn());
    }

    public IEnumerator PopBtn()
    {
        yield return new WaitForSeconds(2);
        startBtn.SetActive(true);
        AIBtn.SetActive(true);
        ExitBtn.SetActive(true);
    }

    void GenerateBird()
    {
        Instantiate(birdObject[Random.Range(0,4)]);
    }

	// Use this for initialization
	void Start () {
        Instance = this;
        Input.multiTouchEnabled = true;
        isStarted = false;
        isOver = true;
        isAI = false;
        startBtn.SetActive(true);
        AIBtn.SetActive(true);
        ExitBtn.SetActive(true);
        //Sound.Instance.playBg();
    }
	
	// Update is called once per frame
	void Update () {
        if (isStarted && !isOver)
        {
            // Generate a bird each 5 seconds
            gameTime += Time.deltaTime;
            if (gameTime > 5)
            {
                GenerateBird();
                gameTime = 0f;
            }

            // Input
            if (Input.GetMouseButtonDown(0))
            {
                if (!isAI && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 0)
                    RedSide.Instance.Shot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                else
                    BlueSide.Instance.Shot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

        }
    }

    // Select "Single Player"
    public void openAI()
    {
        isAI = true;
        StartCoroutine(RedSide.Instance.AIMode(3.2f));
    }

    // Exit application
    public void Exit()
    {
        Application.Quit();
    }
}
