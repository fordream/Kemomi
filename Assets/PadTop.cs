using UnityEngine;
using System.Collections;

public class PadTop : MonoBehaviour {

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

    void Update()
    {

        if (touchManager.leftAction == false)
        {
            ren.enabled = false;
        }
        else
        {
            ren.enabled = true;
        }

        transform.position = touchManager.basePoint + touchManager.leftDirection * dis;
    }
}
