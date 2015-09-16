using UnityEngine;
using System.Collections;

public class PadBase : MonoBehaviour {

    TouchManager touchManager;
    Renderer ren;
    public float dis;

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

        Vector2 touchPoint = touchManager.basePoint + touchManager.leftDirection * dis;
        transform.position = Camera.main.ScreenToWorldPoint(touchPoint);
    }
}
