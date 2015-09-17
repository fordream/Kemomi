using UnityEngine;
using System.Collections;

public class playerCon : MonoBehaviour {
     
    //==========公開フィールド==========
    public float hpMax = 15;
    public float spMax = 100;
    public float speed = 3.0f;
    public float dashPowerInit = 2.0f;
    public float slowRate = 0.5f;
    public float searchRangeRad = 0.5f;
    public float decrementDash = 0.5f;

    //==========非公開フィールド(インスペクタからは編集可能)==========
    [SerializeField]
    int lv = 1;
    [SerializeField]
    bool closeToEnemy = false;
    //==========非公開フィールド==========
    float hp;
    float sp;
    bool slow = false;
    float angleToEnemy = 0.0f;
    float dashPower = 0.0f;


    //==========コンポーネントのハッシュ==========   
    TouchManager touchManager;
    Animator animator;
    //SearchEnemy searchEnemy;
    //==========基本コード==========
    void Awake () {
        touchManager = GameObject.Find("touchManager").GetComponent<TouchManager>();
        animator = GetComponent<Animator>();
        //searchEnemy = GameObject.Find("attackRange").GetComponent<SearchEnemy>();
	}

    void Update() {

        //移動アニメーションの制御
        animator.SetBool("UP", false);
        animator.SetBool("DOWN", false);
        animator.SetBool("RIGHT", false);
        animator.SetBool("LEFT", false);

        if (touchManager.leftAction) {
            if (touchManager.leftAngle > 45.0f && touchManager.leftAngle < 135.0f)
                animator.SetBool("UP", true);
            else if (touchManager.leftAngle >= -45.0f && touchManager.leftAngle < 45.0f)
                animator.SetBool("RIGHT", true);
            else if (touchManager.leftAngle >= -135.0f && touchManager.leftAngle < -45.0f)
                animator.SetBool("DOWN", true);
            else
                animator.SetBool("LEFT", true);
        }

        //近くの敵を検索
        closeToEnemy = false;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(GetComponent<Rigidbody2D>().position, searchRangeRad);

        float distance = searchRangeRad+1.0f;
        Vector2 enemyPos = new Vector2(0,0);
        foreach (Collider2D coll in enemies)
        {
            if(coll.gameObject.tag == "Enemy")
            {
                closeToEnemy = true;
                Vector2 enemyPosTemp = coll.gameObject.transform.position;
                float distanceTemp = Vector2.Distance(GetComponent<Rigidbody2D>().position, enemyPosTemp);
                if(distanceTemp < distance)
                {
                    distance = distanceTemp;
                    enemyPos = enemyPosTemp;
                }
            }
        }

        Vector2 vectorToEnemy = (enemyPos - GetComponent<Rigidbody2D>().position).normalized;
        angleToEnemy = 360 / (Mathf.PI * 2) * Mathf.Atan2(vectorToEnemy.x,vectorToEnemy.y);

        //攻撃アニメーションの制御
        animator.SetBool("UP_ATTACK", false);
        animator.SetBool("DOWN_ATTACK", false);
        animator.SetBool("RIGHT_ATTACK", false);
        animator.SetBool("LEFT_ATTACK", false);
        animator.SetBool("STOP_ATTACK", false);

        if (closeToEnemy) //近くに敵がいたら
        {
            if (angleToEnemy > 45.0f && angleToEnemy < 135.0f)
            {
                animator.SetBool("RIGHT_ATTACK", true);
            }
            else if (angleToEnemy >= -45.0f && angleToEnemy < 45.0f)
            {
                animator.SetBool("UP_ATTACK", true);
            }
            else if (angleToEnemy >= -135.0f && angleToEnemy < -45.0f)
            {
                animator.SetBool("LEFT_ATTACK", true);
            }
            else
            {
                animator.SetBool("DOWN_ATTACK", true);
            }
        }
        else
        {
            animator.SetBool("STOP_ATTACK", true);
        }

        //座標更新
        //フリックによるダッシュ処理
        
        Vector2 directionFinally = touchManager.leftDirection;

        if (touchManager.flicked)
        {
            dashPower = dashPowerInit;
            directionFinally = touchManager.flickVector.normalized;
        }

        dashPower -= decrementDash;

        dashPower = Mathf.Clamp(dashPower, 0, 20.0f);

        float speedFinally = speed + dashPower;

        if (slow)
            speedFinally *= slowRate;

        GetComponent<Rigidbody2D>().position += speedFinally * directionFinally;

    }

    //他のゲームオブジェクトと衝突したとき
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyAttack")
        {
            hp -= collision.gameObject.GetComponent<AttackColl>().power;

        }
    }
}