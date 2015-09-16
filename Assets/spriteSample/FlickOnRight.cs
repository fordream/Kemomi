using UnityEngine;
using System.Collections;

public class FlickOnRight : MonoBehaviour {

	Vector2 startPos;
	Vector2 endPos;
	Vector2 direction;
//	Vector2 prevPos;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.touchCount > 0) {
			var touch = Input.GetTouch(0);

			switch(touch.phase){
				case TouchPhase.Began:
					startPos = touch.position;
//					prevPos = touch.position;
				break;
				
				case TouchPhase.Moved:
//					gameObject.GetComponent<Rigidbody2D>().velocity = touch.position - prevPos;
//					prevPos = touch.position;
				break;
				
				case TouchPhase.Ended:
					endPos = touch.position;
					Vector2 direction = endPos - startPos;
					float radian = Mathf.Atan2 ( direction.y , direction.x );
					Debug.Log("magnitude : " + direction.magnitude);
					if(direction.magnitude > 8) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4*Mathf.Cos(radian),4*Mathf.Sin(radian));
				break;
			}
		}
	}
}
