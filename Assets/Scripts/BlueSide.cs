using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlueSide : MonoBehaviour {

    public static BlueSide Instance;
    public int LifeNum;
    private int BombNum;
    public GameObject[] LifeBar = new GameObject[3];
    public GameObject[] BombBar = new GameObject[3];
    public Bomb blueBomb;
    Bomb loadBomb;

    public void DecreaseLife()
    {
        LifeBar[3-LifeNum].SetActive(false);
        LifeNum -= 1;
        if (LifeNum == 0)
            GameManager.Instance.GameOver("Blue");
    }

    IEnumerator IncreaseBomb(int second)
    {
        yield return new WaitForSeconds(second);
        if (BombNum < 3 && GameManager.Instance.isStarted)
        {
            BombNum += 1;
            BombBar[3 - BombNum].SetActive(true);
        }
    }

    public void DecreaseBomb()
    {
        BombBar[3 - BombNum].SetActive(false);
        BombNum -= 1;
        StartCoroutine(IncreaseBomb(3));
    }

    IEnumerator LoadBomb(int second)
    {
        yield return new WaitForSeconds(second);
        if (BombNum != 0 && loadBomb == null) {
            loadBomb = Instantiate(blueBomb);
            loadBomb.transform.position = new Vector3(0f, -4.5f, -10f);
        } else if (loadBomb == null)
            StartCoroutine(LoadBomb(1));
    }

    public void Shot(Vector2 pos)
    {
        if (BombNum == 0 || loadBomb == null)
            return;
        DecreaseBomb();

        float angel = Vector2.Angle(new Vector2(1, 0), pos - new Vector2(loadBomb.transform.position.x, loadBomb.transform.position.y));
        loadBomb.setAngel(angel - 90);
        loadBomb.setDirect(0.1f * Mathf.Cos(Mathf.Deg2Rad * angel), 0.1f * Mathf.Sin(Mathf.Deg2Rad * angel));
        loadBomb.Lauch();
        if (GameManager.Instance.isAI)
            RedSide.Instance.blueBombShot(loadBomb);
        loadBomb = null;
        StartCoroutine(LoadBomb(1));
    }

    public void Initial()
    {
        LifeNum = 3;
        BombNum = 3;
        for(int i = 0; i < 3; i++)
        {
            LifeBar[i].SetActive(true);
            BombBar[i].SetActive(true);
        }
        StartCoroutine(LoadBomb(0));
    }

	// Use this for initialization
	void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
