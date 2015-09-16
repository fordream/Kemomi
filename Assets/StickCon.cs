using UnityEngine;
using System.Collections;

public class PadCon : MonoBehaviour {

    TouchManager touchManager;


	void Awake () {
        touchManager = GameObject.Find("TouchManager").GetComponent<TouchManager>();
	}

    void Start()
    {
        GameObject.Find("padTop").GetComponent<Transform>().GetComponent<Renderer>().enabled = false;
        GameObject.Find("padBase").GetComponent<Transform>();
    }

    void Update () {
	    
	}
}
