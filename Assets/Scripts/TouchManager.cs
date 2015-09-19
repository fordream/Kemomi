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

    //========== 非公開フィールド==========
    //右手操作に使う変数
    Vector2 startPos;
    Vector2 endPos;
    float beganTime = 0;
	
	public int fingerID_right;
	public int fingerID_left;

	//==========コンポーネントのハッシュ==========
	InventoryManager inventoryManager;

    //==========基本コード==========

	void Awake () {
		inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
	}

    void Update()
    {
        flicked = false;

        //MULTIタップ管理、左画面タッチと右画面タッチの処理の分け
        foreach (var touch in Input.touches)
        {

			if(touch.phase == TouchPhase.Began){
				if(inventoryManager.inventoryRect.Contains(new Vector2 (touch.position.x, Screen.height - touch.position.y))){
					Debug.Log("touch inventory");
				}
	            else if (touch.position.x < Screen.width * 0.5 && fingerID_left == -1)
	            {
					fingerID_left = touch.fingerId;

	                Debug.Log("touch left start fingerid =" + fingerID_left);
	            }else if (touch.position.x >= Screen.width * 0.5 && fingerID_right == -1)
	            {
					fingerID_right = touch.fingerId;
	                Debug.Log("touch right start fingerid =" + fingerID_right);
	            }
			}

			if(fingerID_left == touch.fingerId) leftCon(touch);
			if(fingerID_right == touch.fingerId) rightCon(touch);
        }
    }

//	void onGUI(){
//	}

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
				fingerID_left = -1;
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
				fingerID_right = -1;
                break;

        }
    }

}