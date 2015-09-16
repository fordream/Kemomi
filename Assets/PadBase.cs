using UnityEngine;
using System.Collections;

public class PadBase : MonoBehaviour {

    TouchManager touchManager;
    Renderer ren;

    void Awake()
    {
        touchManager = GameObject.Find("touchManager").GetComponent<TouchManager>();
        ren = gameObject.GetComponent<Renderer>();
    }

    void Start()
    {       
        ren.enabled = false;
    }

    void Update () {

	    if(touchManager.leftAction == false)
        {
            ren.enabled = false;
        }
        else { 
            ren.enabled = true;
        }

        transform.position = touchManager.basePoint;
    }
}
