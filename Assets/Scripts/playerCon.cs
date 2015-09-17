using UnityEngine;
using System.Collections;

public class playerCon : MonoBehaviour {
     

    //==========公開フィールド==========
    public float hpMax = 15;
    public float spMax = 100;
    public float speed = 3.0f;
    public float slowRate = 0.5f;

    //==========非公開フィールド(インスペクタからは編集可能)==========
    [SerializeField]
    int lv = 1;

    //==========非公開フィールド==========
    float hp;
    float sp;
    bool slow = false;

    //==========コンポーネントのハッシュ==========   
    TouchManager touchManager;
    Animator animator;

    //==========基本コード==========
    void Awake () {
        touchManager = GameObject.Find("touchManager").GetComponent<TouchManager>();
        animator = GetComponent<Animator>();
	}

    void Update () {

        //座標更新
        float speedFinally = speed;
        if (slow)
            speedFinally *= slowRate;

        GetComponent<Rigidbody2D>().position += speedFinally * touchManager.leftDirection;

        //アニメーション制御
        animator.SetBool("UP", false);
        animator.SetBool("DOWN", false);
        animator.SetBool("RIGHT", false);
        animator.SetBool("LEFT", false);

		if(touchManager.leftAction){
        	if (touchManager.leftAngle > 45.0f && touchManager.leftAngle < 135.0f)
        	    animator.SetBool("UP", true);
        	else if (touchManager.leftAngle >= -45.0f && touchManager.leftAngle < 45.0f)
        	    animator.SetBool("RIGHT", true);
        	else if (touchManager.leftAngle >= -135.0f && touchManager.leftAngle < -45.0f)
        	    animator.SetBool("DOWN", true);
        	else
        	    animator.SetBool("LEFT", true);
		}
    }

    //他のゲームオブジェクトと衝突したとき
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyAttack")
        {
            //collision.gameObject.GetComponent<Bullet>();

        }
    }
}