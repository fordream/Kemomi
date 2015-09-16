using UnityEngine;
using System.Collections;

public class playerCon : MonoBehaviour {

    TouchManager touchManager;

    public float speed = 3.0f;
	
	void Awake () {
        touchManager = GameObject.Find("touchManager").GetComponent<TouchManager>();
	}
	
	
	void Update () {
        GetComponent<Rigidbody2D>().position += speed * touchManager.leftDirection;

    }
}