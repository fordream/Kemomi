using UnityEngine;
using System.Collections;

public class playerCon : MonoBehaviour {

    TouchManager touchManager;

    public float speed = 3.0f;
	
	void Awake () {
        touchManager = GetComponent<TouchManager>();
	}
	
	
	void Update () {
	
	}
}
