using UnityEngine;
using System.Collections;

public class WinLose : MonoBehaviour {

    float move;

    // Use this for initialization
    void Start() {
        if (transform.position.y < 0)
        {
            move = 0.01f;
            Renderer render = gameObject.GetComponent<Renderer>();
            render.material.color = Color.blue;

        } else
        {
            move = -0.01f;
        }   
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(transform.position.y) > 3)
            transform.position += new Vector3(0, move, 0);
        if (GameManager.Instance.isStarted)
            Destroy(gameObject);
	
	}
}
