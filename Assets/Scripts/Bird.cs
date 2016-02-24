using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

    //public static Bird Instance;
    private int direct;             // 1 = Right    -1 = Left
    private float speed;
    public AudioSource touchBomb;

    void OnTriggerEnter2D(Collider2D col)
    {
        // Touch the wall
        if (col.tag == "Wall")
        {
            ChangeDirect();
        } else if(col.tag == "Bomb")
        {
            Sound.Instance.playBirdDie();
            Destroy(gameObject);
        }
    }

    void Init()
    {
        // Generate the position & speed randomly
        float initX = Random.Range(-2.5f, 2.5f);
        float initY = Random.Range(-1f, 1f);
        speed = Random.Range(0.01f, 0.05f);

        // Check the direct
        direct = 1;
        if (Random.Range(-1f, 1f) < 0)
            ChangeDirect();


        if (initY > 0)
            transform.Rotate(new Vector2(180, 0));
        transform.position = new Vector3(initX, initY, -10);
    }

    void ChangeDirect()
    {
        direct *= -1;
        transform.Rotate(new Vector2(0, 180));
    }

	// Use this for initialization
	void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {
        if(GameManager.Instance.isStarted)
            transform.position += new Vector3(direct  * speed, 0);
        if (GameManager.Instance.isOver)
            Destroy(gameObject);
    }

    void Shotted()
    {

    }
}
