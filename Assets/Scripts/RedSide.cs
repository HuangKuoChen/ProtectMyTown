using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedSide : MonoBehaviour {

    public static RedSide Instance;
    public int LifeNum;
    private int BombNum;
    public GameObject[] LifeBar = new GameObject[3];
    public GameObject[] BombBar = new GameObject[3];
    public Bomb redBomb;
    Bomb loadBomb;
    public Bomb[] blueBomb = new Bomb[3];
    int blueBombIndex;

    public void DecreaseLife()
    {
        LifeBar[3-LifeNum].SetActive(false);
        LifeNum -= 1;
        if (LifeNum == 0)
            GameManager.Instance.GameOver("Red");
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
        if(GameManager.Instance.isAI)
            StartCoroutine(IncreaseBomb(2));
        else
            StartCoroutine(IncreaseBomb(3));
    }

    IEnumerator LoadBomb(int second)
    {
        yield return new WaitForSeconds(second);
        // If bomb number > 0 and have not loaded bomb
        if (BombNum != 0 && loadBomb == null) {
            loadBomb = Instantiate(redBomb);
            loadBomb.transform.position = new Vector3(0f, 4.5f, -10f);
        } else if(loadBomb == null)
            StartCoroutine(LoadBomb(1));
    }

    public void Shot(Vector2 pos)
    {
        if (loadBomb == null || BombNum == 0)
            return;
        DecreaseBomb();
        
        float angel = Vector2.Angle(new Vector2(1, 0), new Vector2(loadBomb.transform.position.x, loadBomb.transform.position.y)-pos);
        loadBomb.Lauch();
        loadBomb.setAngel(angel - 90);
        loadBomb.setDirect(0.1f * Mathf.Cos(Mathf.Deg2Rad * (angel - 180)), 0.1f * Mathf.Sin(Mathf.Deg2Rad * (angel - 180)));
        loadBomb = null;
        StartCoroutine(LoadBomb(1));
    }

    public void Initial()
    {
        blueBombIndex = 0;
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

    public void blueBombShot(Bomb b)
    {
        blueBomb[blueBombIndex] = b;
        blueBombIndex = (blueBombIndex + 1) % 3;
    }

    public IEnumerator AIMode(float second)
    {
        yield return new WaitForSeconds(second);
        if (GameManager.Instance.isStarted)
        {
            for (int i = 0; i < 3; i++)
            {
                if (blueBomb[i] != null)
                {
                    if (blueBomb[i].transform.position.y > 0)
                    {
                        float shotX = blueBomb[i].transform.position.x + 10 * blueBomb[i].directX;
                        float shotY = blueBomb[i].transform.position.y + 10 * blueBomb[i].directY;
                        if (shotY > 4.5f)
                            shotY = 4.5f;
                        Shot(new Vector2(shotX, shotY));
                        break;
                    }
                }
                if(i == 2)
                {
                    float shotX = Random.Range(-4.5f, 4.5f); ;
                    float shotY = 1;
                    Shot(new Vector2(shotX, shotY));
                }
            }
            StartCoroutine(AIMode(0.4f));
        }
        

    }
}
