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
}