using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    [SerializeField]
    Vector2 leftDirection;

    Vector2 basePoint;
    bool leftAction = false;
  

    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.position.x < Screen.width * 0.5)
            {
                leftCon(touch);
                Debug.Log("touch left");
            }

            if (touch.position.x >= Screen.width * 0.5)
            {
                
                //Debug.Log("touch right");
            }

        }
    }

    public void leftCon(Touch touch)
    {
        if (leftAction == false) {
            basePoint = touch.position;
            leftAction = true;
            leftDirection = Vector2.zero;
        }

        if (leftAction)
        {
            leftDirection = (touch.position - basePoint).normalized;
            if (touch.phase == TouchPhase.Ended)
                leftAction = false;
        }

    }
}
