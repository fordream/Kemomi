using UnityEngine;
using System.Collections;

public class playerCon : MonoBehaviour {

 
    TouchManager touchManager;
    Animator animator;

    public float speed = 3.0f;
	
	void Awake () {
        touchManager = GameObject.Find("touchManager").GetComponent<TouchManager>();
        animator = GetComponent<Animator>();
	}
	
	
	void Update () {
        GetComponent<Rigidbody2D>().position += speed * touchManager.leftDirection;

        if (touchManager.leftAngle < 45.0f || touchManager.leftAngle > 315.0f)
            animator.SetTrigger("UP");
        else if (touchManager.leftAngle >= 45.0f && touchManager.leftAngle < 135.0f)
            animator.SetTrigger("RIGHT");
        else if (touchManager.leftAngle >= 135.0f && touchManager.leftAngle > 225.0f)
            animator.SetTrigger("DOWN");
        else
            animator.SetTrigger("LEFT");
    }       
}