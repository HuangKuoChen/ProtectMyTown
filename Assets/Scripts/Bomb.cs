using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public bool isLauch;
    public float directX, directY;
    public float turnAngel;
    public GameObject exploAir, exploLand, fire;

    public void OnTriggerEnter2D(Collider2D col)
    {
        // Touch the wall
        if (col.tag == "Bird")
        {
            Destroy(gameObject);
            Instantiate(exploAir, col.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            Sound.Instance.playExplosion();
        } else if (col.tag == "Wall") {
            directX *= -1;
            transform.Rotate(0, 0, turnAngel*-2);
            turnAngel = turnAngel * -1;
            Sound.Instance.playReflect();
        } else if (col.tag == "BlueSide")
        {
            Destroy(gameObject);
            BlueSide.Instance.DecreaseLife();
            Instantiate(exploLand, gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 0));
            Instantiate(fire, gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 0));
            Sound.Instance.playExplosion();
        } else if (col.tag == "RedSide")
        {
            Destroy(gameObject);
            RedSide.Instance.DecreaseLife();
            Instantiate(exploLand, gameObject.transform.position - new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 180));
            Instantiate(fire, gameObject.transform.position - new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 180));
            Sound.Instance.playExplosion();
        } else if (col.tag == "Bomb")
        {
            if(col.GetComponent<Bomb>().isLauch && isLauch)
            {
                Destroy(gameObject);
                Instantiate(exploAir, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                Sound.Instance.playExplosion();
            }
        }
    }

    public void setAngel(float angel)
    {
        turnAngel = angel;
        transform.Rotate(0, 0, turnAngel);
    }

    public void setDirect(float dirX, float dirY)
    {
        directX = dirX;
        directY = dirY;
    }

    public void Lauch()
    {
        isLauch = true;
        Sound.Instance.playShot();
    }

    void Awake()
    {
        isLauch = false;
    }
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isLauch && GameManager.Instance.isStarted)
            transform.position += new Vector3(directX, directY, 0f);
        if (!GameManager.Instance.isStarted && GameManager.Instance.isOver)
            Destroy(gameObject);
    }
}
