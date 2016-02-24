using UnityEngine;
using System.Collections;

public class ContinueAnima : MonoBehaviour {

    bool preStartState;

	// Use this for initialization
	void Awake () {
        preStartState = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!preStartState && GameManager.Instance.isStarted)
            Destroy(gameObject);

        preStartState = GameManager.Instance.isStarted;
    }
}
