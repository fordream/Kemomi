using UnityEngine;
using System.Collections;


public class TouchManager : MonoBehaviour
{
    //==========公開フィールド==========
    //左手操作に使う変数
    public Vector2 leftDirection;
    public float leftAngle;
    public Vector2 basePoint;
    public bool leftAction = false;
    //右手操作に使う変数
    public Vector2 flickVector;
    public float flickTime;
    public bool flicked = false;

    //==========非公開フィールド==========
    //右手操作に使う変数
    Vector2 startPos;
    Vector2 endPos;
    float beganTime = 0;


    //==========基本コード==========
    void Update()
    {
        flicked = false;

        //MULTIタップ管理、左画面タッチと右画面タッチの処理の分け
        foreach (var touch in Input.touches)
        {
            if (touch.position.x < Screen.width * 0.5)
            {
                leftCon(touch);
                Debug.Log("touch left");
            }

            if (touch.position.x >= Screen.width * 0.5)
            {
                rightCon(touch);
                Debug.Log("touch right");
            }

        }
    }

    //左画面操作
    public void leftCon(Touch touch)
    {
        if (leftAction == false)
        {
            basePoint = touch.position;
            leftAction = true;
        }

        if (leftAction)
        {
            leftDirection = (touch.position - basePoint).normalized;
            if (touch.phase == TouchPhase.Ended)
            {
                leftAction = false;
                leftDirection = Vector2.zero;
                Debug.Log("TouchPhase.Ended");
            }
		
			leftAngle = 360 / (Mathf.PI * 2) * Mathf.Atan2(leftDirection.y, leftDirection.x);

        }

    }

    //右画面操作
    public void rightCon(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                beganTime = Time.time;
                startPos = touch.position;
                break;

            case TouchPhase.Ended:
                if (beganTime - Time.time < flickTime)
                    flicked = true;
                endPos = touch.position;
                Vector2 direction = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);
                float radian = Mathf.Atan2(direction.y, direction.x);
                flickVector = new  Vector2( Mathf.Cos(radian), Mathf.Sin(radian));
                break;

        }
    }

}